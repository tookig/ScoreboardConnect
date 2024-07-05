using ScoreboardLiveSocket.Messages;
using ScoreboardLiveWebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using TP;
using TPNetwork;

namespace ScoreboardConnectWinUI3 {
  internal class RequestCoordinator {
    private Controls.ScoreboardLiveControl.ScoreboardLiveConnectedEventArgs m_apiInfo;
    private SocketClient m_tpNetwork;
    private TPListener m_tpListener;
    private ICourtCorrelator m_courtCorrelator;
    private bool m_enableCourtSync = false;

    private object m_optionsLock = new object();
    private TournamentConverter.ConvertOptions m_convertOptions;
    private TournamentConverter m_converter;

    public bool EnableCourtSync {
      get {
        return m_enableCourtSync;
      }
      set {
        if (value == m_enableCourtSync) {
          return;
        }
        m_enableCourtSync = value;
        CheckIfAllReady();
      }
    }

    public Controls.ScoreboardLiveControl.ScoreboardLiveConnectedEventArgs ApiInfo {
      get {
        return m_apiInfo;
      }
      set {
        _ = InitApi(value);
      }
    }

    public SocketClient TPNetwork {
      get {
        return m_tpNetwork;
      }
      set {
        InitSocket(value);
      }
    }

     public TPListener TPListener {
      get {
        return m_tpListener;
      }
      set {
        InitListener(value);
      }
    }

    public TournamentConverter.ConvertOptions ConvertOptions {
      set {
        lock (m_optionsLock) {
          m_convertOptions = value;
          m_converter = null;
        }
      }
    }

    private TournamentConverter Converter {
      get {
        if (m_converter == null) {
          lock (m_optionsLock) {
            m_converter = new TournamentConverter( m_convertOptions);
          }
        }
        return m_converter;
      }
    }

    public RequestCoordinator(SocketClient tpNetwork, TPListener tpListener, ICourtCorrelator courtCorrelator = null, TournamentConverter.ConvertOptions convertOptions = null) {
      InitSocket(tpNetwork);
      InitListener(tpListener);
      if (courtCorrelator != null) {
        _ = InitCourtCorrelator(courtCorrelator);
      }
      ConvertOptions = convertOptions ?? new TournamentConverter.ConvertOptions();
    }

    protected void InitSocket(SocketClient socketClient) {
      SocketClient validatedClient = socketClient ?? throw new ArgumentNullException("SocketClient reference cannot be null");
      if (m_tpNetwork != null) {
        m_tpNetwork.MessageReceived -= ScoreboardSocketMessage;
      }
      m_tpNetwork = validatedClient;
      m_tpNetwork.MessageReceived += ScoreboardSocketMessage;
      CheckIfAllReady();
    }

    private async void ScoreboardSocketMessage(object sender, XmlDocument tournamentXML) {
      if (m_courtCorrelator == null) {
        return;
      }
      if (tournamentXML.SelectSingleNode("//GROUP[@ID='Action']/ITEM[@ID='Result']").InnerText.ToUpper() == "SENDTOURNAMENTINFO") {
        try {
          var tmpNetworkData = new TP.VisualXML.TPNetworkDocument(tournamentXML);
          var tournament = await TP.Tournament.LoadFromVisualXMLAsync(tmpNetworkData);
          m_courtCorrelator.SetTPCourts(tournament.Courts);
        } catch (Exception e) {
          ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Error, $"Failed to parse tournament from TP: {e.Message}", e);
        }
      }
    }

    protected async Task InitApi(Controls.ScoreboardLiveControl.ScoreboardLiveConnectedEventArgs apiInfo) {
      Controls.ScoreboardLiveControl.ScoreboardLiveConnectedEventArgs validatedApi = apiInfo ?? throw new ArgumentNullException("ApiHelper reference cannot be null");

      bool doReload = true;

      if (m_apiInfo != null) {
        m_apiInfo.WebSocket.MessageReceived -= ScoreboardSocketMessage;
        m_apiInfo.WebSocket.StateChanged -= ScoreboardSocketStateChange;
        m_apiInfo.WebSocket.ErrorOccurred -= ScoreboardSocketError;

        if (m_apiInfo.Api == validatedApi.Api && m_apiInfo.Device?.UnitID == validatedApi.Device?.UnitID && validatedApi.Tournament?.TournamentID == m_apiInfo.Tournament?.TournamentID) {
          doReload = false;
        }
      }
      m_apiInfo = validatedApi;

      if (doReload) {
        await ReloadSBCourts();
      }
      m_apiInfo.WebSocket.MessageReceived += ScoreboardSocketMessage;
      m_apiInfo.WebSocket.StateChanged += ScoreboardSocketStateChange;
      m_apiInfo.WebSocket.ErrorOccurred += ScoreboardSocketError;
      CheckIfAllReady();
    }

    private void ScoreboardSocketError(object sender, ErrorEventArgs e) {
      ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Error, e.Error.Message, e.Error);
    }

    private void ScoreboardSocketStateChange(object sender, StateEventArgs e) {
      // ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Info, e.State.ToString());
    }

    private void ScoreboardSocketInfo(object sender, InfoEventArgs e) {
      ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Info, e.Info);
    }

    private void ScoreboardSocketMessage(object sender, MessageEventArgs e) {
      ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Info, e.Message.ToString());
      if (e.Message is MatchUpdate matchUpdate) {
        _ = MatchUpdate(matchUpdate);
      }
    }

    protected void InitListener(TPListener tpListener) {
      TPListener validatedListener = tpListener ?? throw new ArgumentNullException("TPListener reference cannot be null");
      if (m_tpListener != null) {
        m_tpListener.CourtUpdate -= courtUpdate;
        m_tpListener.ServiceStarted -= tpListenerServiceStarted;
      }
      m_tpListener = validatedListener;
      m_tpListener.CourtUpdate += courtUpdate;
      m_tpListener.ServiceStarted += tpListenerServiceStarted;
    }

    protected async Task InitCourtCorrelator(ICourtCorrelator courtCorrelator) {
      ICourtCorrelator validatedCorrelator = courtCorrelator ?? throw new ArgumentNullException("ICourtCorrelator reference cannot be null");
      if (m_courtCorrelator != null) {
      }
      m_courtCorrelator = validatedCorrelator;
      await ReloadSBCourts();
      await ReloadTPCourts();
      CheckIfAllReady();
    }

    private void tpListenerServiceStarted(object sender, EventArgs e) {
      CheckIfAllReady();
    }

    private void CheckIfAllReady() {
      if (m_enableCourtSync && m_apiInfo != null && m_tpNetwork != null && m_tpListener != null && m_courtCorrelator != null) {
        _ = UploadStartState();
      }
    }

    #region Court Update Handling
    private async Task UploadStartState() {
      var courtSetup = m_courtCorrelator.GetSnapshot();

      var tpTournament = await GetTPTournament();
      if (tpTournament == null) {
        return;
      }

      foreach (var kvp in courtSetup) {
        TP.Match tpMatch = null;
        if (kvp.Value != null) {
          TP.Court tpCourt = tpTournament.FindCourtByID(kvp.Value.ID);
          if (tpCourt?.TpMatchID > 0) {
            tpMatch = tpTournament.FindMatchByID(tpCourt.TpMatchID);
          }
        }

        if (tpMatch != null) {
          await MatchOnCourts(new List<ScoreboardLiveApi.Court>() { kvp.Key }, tpMatch, tpTournament);
        } else {
          await ClearCourts(new List<ScoreboardLiveApi.Court>() { kvp.Key });
        }
      }
    }

    private async void courtUpdate(object sender, TPListener.TPCourtUpdateEventArgs e) {
      if (!m_enableCourtSync) {
        return;
      }

      if (m_courtCorrelator == null) {
        return;
      }
      if (m_apiInfo == null) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Warning, "Court update received, but no Scoreboard API connection");
        return;
      }

      var sbCourts = m_courtCorrelator.Correlate(e.CourtName);

      if (sbCourts.Count == 0) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Warning, $"An update was received for unassigned TP court {e.CourtName}.");
        return;
      }

      if (e.Match == null) {
        await ClearCourts(sbCourts);
      } else {
        await MatchOnCourts(sbCourts, e.Match);
      }
    }

    private async Task ClearCourts(List<ScoreboardLiveApi.Court> sbCourts) {
      foreach (var sbCourt in sbCourts) {
        try {
          await m_apiInfo.Api.ClearCourt(m_apiInfo.Device, sbCourt);
        } catch (Exception e) {
          ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Error, $"Failed to clear court {sbCourt.Name} ({sbCourt.Venue?.Name}): {e.Message}");
          continue;
        }
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Info, $"Court {sbCourt.Name} ({sbCourt.Venue?.Name}) is now free");
      }
    }

    private async Task MatchOnCourts(List<ScoreboardLiveApi.Court> sbCourts, TP.Match tpMatch, TP.Tournament presetTournament = null) {
      var tpTournament = presetTournament ?? await GetTPTournament();
      if (tpTournament == null) {
        return;
      }

      var updatedTPMatch = tpTournament.FindMatchByID(tpMatch.ID);
      if (updatedTPMatch == null) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Warning, $"TP match {tpMatch.ID} not found in tournament");
        return;
      }

      var tpDraw = tpTournament.FindDrawByID(updatedTPMatch.DrawID);
      if (tpDraw == null) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Warning, $"Draw {updatedTPMatch.DrawID} not found in tournament");
        return;
      }

      var tpEvent = tpTournament.FindEventByID(tpDraw.EventID);
      if (tpEvent == null) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Warning, $"Event {tpDraw.EventID} not found in tournament");
        return;
      }

      string sbTag = GetMatchTag(updatedTPMatch, tpEvent, tpDraw);
      if (sbTag == null) {
        return;
      }

      ScoreboardLiveApi.Match sbMatch;
      try {
        sbMatch = await GetSBMatch(updatedTPMatch, sbTag);
      } catch (Exception ex) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Error, ex.Message, ex);
        return;
      }

      if (sbMatch == null) {
        try {
          sbMatch = await CreateOnTheFlySBMatch(updatedTPMatch, tpEvent, sbTag);
        } catch (Exception ex) {
          ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Error, ex.Message, ex);
          return;
        }
      } else {
        try {
          sbMatch = await CheckIfScoreboardServerNeedsMatchUpdate(updatedTPMatch, sbMatch);
        } catch (Exception ex) {
          ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Error, ex.Message, ex);
          return;
        }
      }

      foreach (var sbCourt in sbCourts) {
        await AssignMatchToCourt(sbMatch, sbCourt);
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Info, $"Match {sbMatch.MatchID} is now on court {sbCourt.Name} ({sbCourt.Venue?.Name})");
      }
    }

    private async Task<TP.Tournament> GetTPTournament() {
      try {
        var tpXML = await m_tpNetwork.GetTournamentInfo();
        var tmpNetworkData = new TP.VisualXML.TPNetworkDocument(tpXML);
        return await TP.Tournament.LoadFromVisualXMLAsync(tmpNetworkData);
      } catch (Exception e) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Error, $"Failed to load tournament from TP: {e.Message}", e);
        return null;
      }
    }

    private string GetMatchTag(TP.Match tpMatch, TP.Event tpEvent, TP.Draw tpDraw) {
      return TP.TournamentConverter.CreateMatchTag(m_apiInfo.Tournament, tpMatch, tpEvent, tpDraw);
    }

    private async Task<ScoreboardLiveApi.Match> GetSBMatch(TP.Match tpMatch, string sbTag) {    
      var sbMatches = await m_apiInfo.Api.FindMatchByTag(m_apiInfo.Device, sbTag);
      if (sbMatches.Count == 0) {
        return null;
      } else if (sbMatches.Count > 1) {
        throw new Exception($"Multiple scoreboard matches found for TP match tag {sbTag}");
      }
      return sbMatches[0];
    }

    private async Task<ScoreboardLiveApi.Match> CreateOnTheFlySBMatch(TP.Match tpMatch, TP.Event tpEvent, string tag) {
      string category = Converter.CreateMatchCategoryString(tpEvent);
      var sbMatch = Converter.ConvertMatch(tpMatch, category);
      sbMatch.Tag = tag;
      return await m_apiInfo.Api.CreateOnTheFlyMatch(m_apiInfo.Device, m_apiInfo.Tournament, sbMatch);
    }

    private async Task<ScoreboardLiveApi.Match> CheckIfScoreboardServerNeedsMatchUpdate(TP.Match tpMatch, ScoreboardLiveApi.Match sbMatch) {
      (string team1player1name, string team1player1team, string team1player2name, string team1player2team) = Converter.ConvertEntry(tpMatch.Entries.Item1);
      (string team2player1name, string team2player1team, string team2player2name, string team2player2team) = Converter.ConvertEntry(tpMatch.Entries.Item2);
      
      if (sbMatch.Team1Player1Name != team1player1name || sbMatch.Team1Player1Team != team1player1team ||
                 sbMatch.Team1Player2Name != team1player2name || sbMatch.Team1Player2Team != team1player2team ||
                          sbMatch.Team2Player1Name != team2player1name || sbMatch.Team2Player1Team != team2player1team ||
                                   sbMatch.Team2Player2Name != team2player2name || sbMatch.Team2Player2Team != team2player2team) {
        return await m_apiInfo.Api.UpdateMatch(m_apiInfo.Device, sbMatch);
      }
      return sbMatch;
    }

    private async Task<bool> AssignMatchToCourt(ScoreboardLiveApi.Match sbMatch, ScoreboardLiveApi.Court sbCourt) {
      try {
        await m_apiInfo.Api.AssignMatchToCourt(m_apiInfo.Device, sbMatch, sbCourt);
      } catch (Exception e) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Error, $"Failed to assign match {sbMatch.MatchID} to court {sbCourt.Name}: {e.Message}", e);
        return false;
      }
      return true;
    }
    #endregion

    #region  Match update handling
    private static readonly string[] finishedMatchStatus = ["team1won", "team2won"];

    private async Task MatchUpdate(MatchUpdate matchUpdate) {
      // Send info message
      ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Verbose, $"Match update received for {matchUpdate.Match?.ToString()}");
      // Check if match is complete
      if (matchUpdate.Match == null || !finishedMatchStatus.Contains(matchUpdate.Match.Status)) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Verbose, "Match not finished; no update done");
        return;
      }
      // Look up the match in the TP tournament
      TP.Tournament tpTournament = await GetTPTournament();
      if (tpTournament == null) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Error, "Cannot update match: Failed to load tournament from TP");
        return;
      }
      // Extract the TP match id from the SB match tag
      int tpMatchID;
      try { 
        tpMatchID = TP.TournamentConverter.ExtractMatchIDFromTag(matchUpdate.Match.Tag);
      } catch (Exception e) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Verbose, $"Cannot update match: Failed to extract match ID from tag {matchUpdate.Match.Tag}: {e.Message}");
        return;
      }
      // Find the match in the TP tournament
      TP.Match tpMatch = tpTournament.FindMatchByID(tpMatchID);
      if (tpMatch == null) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Verbose, $"Cannot update match: Match {tpMatchID} not found in tournament");
        return;
      }
      // Find draw
      Draw draw = tpTournament.FindDrawByID(tpMatch.DrawID);
      if (draw == null) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Warning, $"Cannot update match: Draw {tpMatch.DrawID} not found in tournament");
        return;
      }
      // Find event
      Event ev = tpTournament.FindEventByID(draw.EventID);
      if (ev == null) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Warning, $"Cannot update match: Event {draw.EventID} not found in tournament");
        return;
      }
      // Check that the tags match
      if (matchUpdate.Match.Tag != TP.TournamentConverter.CreateMatchTag(m_apiInfo.Tournament, tpMatch, ev, draw)) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Warning, $"Match ID's match but tags do not.");
        return;
      }
      // Check if the tp match is already finished
      if ((tpMatch.Winner == TP.Data.PlayerMatchData.Winners.Entry1 && matchUpdate.Match.Status == "team1won") ||
          (tpMatch.Winner == TP.Data.PlayerMatchData.Winners.Entry2 && matchUpdate.Match.Status == "team2won")) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Verbose, $"Match {tpMatchID} already has correct winner");
        return;
      }
      // Update the TP match
      UpdateTpMatch(tpMatch, matchUpdate.Match);
      // Send update message
      try {
        await m_tpNetwork.SendUpdate(tpMatch);
      } catch (Exception e) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Error, $"Failed to send update to TP: {e.Message}");
        return;
      }
    }
    #endregion

    private async Task ReloadSBCourts() {
      if (m_apiInfo == null || m_courtCorrelator == null) {
        return;
      }
      await m_apiInfo.Api.GetCourts(m_apiInfo.Device).ContinueWith((courts) => {
        m_courtCorrelator.SetSBCourts(courts.Result);
      });
    }

    private async Task ReloadTPCourts() {
      if (m_tpNetwork == null || m_courtCorrelator == null) {
        return;
      }
      await m_tpNetwork.GetTournamentInfo().ContinueWith(async (tournamentXML) => {
        var tmpNetworkData = new TP.VisualXML.TPNetworkDocument(tournamentXML.Result);
        var tournament = await TP.Tournament.LoadFromVisualXMLAsync(tmpNetworkData);
        m_courtCorrelator.SetTPCourts(tournament.Courts);
      });
    }

    private void UpdateTpMatch(TP.Match tpMatch, ScoreboardLiveApi.MatchExtended sbMatch) {
      for (int i = 1; i <= 5; i++) {
        tpMatch.SetScore(i, 1, sbMatch.Sets[i][1]);
        tpMatch.SetScore(i, 2, sbMatch.Sets[i][2]);
      }
      tpMatch.Winner = sbMatch.Status == "team1won" ? TP.Data.PlayerMatchData.Winners.Entry1 : TP.Data.PlayerMatchData.Winners.Entry2;
      if (sbMatch.Special == ScoreboardLiveApi.Special.Retired) {
        tpMatch.ScoreStatus = TP.Data.PlayerMatchData.ScoreStatusValue.Retired;
      } else if (sbMatch.Special == ScoreboardLiveApi.Special.WalkOver) {
        tpMatch.ScoreStatus = TP.Data.PlayerMatchData.ScoreStatusValue.WalkOver;
      } else if (sbMatch.Special == ScoreboardLiveApi.Special.Disqualified) {
        tpMatch.ScoreStatus = TP.Data.PlayerMatchData.ScoreStatusValue.Disqualified;
      }
    }
  }
}
