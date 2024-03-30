using ScoreboardLiveApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TP;
using TP.VisualXML;
using TPNetwork;

namespace ScoreboardConnectWinUI3 {
  internal class RequestCoordinator {
    public enum StatusMessageLevel {
      Info,
      Warning,
      Error
    }

    public class StatusMessageEventArgs : EventArgs {
      public string Message { get; set; }
      public StatusMessageLevel Level { get; set; }
    }

    private Controls.ScoreboardLiveControl.ScoreboardLiveConnectedEventArgs m_apiInfo;
    private SocketClient m_tpNetwork;
    private TPListener m_tpListener;
    private ICourtCorrelator m_courtCorrelator;

    public event EventHandler<StatusMessageEventArgs> Status;

    public Controls.ScoreboardLiveControl.ScoreboardLiveConnectedEventArgs ApiInfo {
      get {
        return m_apiInfo;
      }
      set {
        InitApi(value);
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

    public RequestCoordinator(SocketClient tpNetwork, TPListener tpListener, ICourtCorrelator courtCorrelator = null) {
      InitSocket(tpNetwork);
      InitListener(tpListener);
      if (courtCorrelator != null) {
        InitCourtCorrelator(courtCorrelator);
      }
    }

    protected void InitSocket(SocketClient socketClient) {
      SocketClient validatedClient = socketClient ?? throw new ArgumentNullException("SocketClient reference cannot be null");
      if (m_tpNetwork != null) {
      }
      m_tpNetwork = validatedClient;
      _ = ReloadTPCourts();
    }

    protected void InitApi(Controls.ScoreboardLiveControl.ScoreboardLiveConnectedEventArgs apiInfo) {
      Controls.ScoreboardLiveControl.ScoreboardLiveConnectedEventArgs validatedApi = apiInfo ?? throw new ArgumentNullException("ApiHelper reference cannot be null");
      if (m_apiInfo != null) {
      }
      m_apiInfo = validatedApi;
      _ = ReloadSBCourts();
    }

    protected void InitListener(TPListener tpListener) {
      TPListener validatedListener = tpListener ?? throw new ArgumentNullException("TPListener reference cannot be null");
      if (m_tpListener != null) {
        m_tpListener.CourtUpdate -= courtUpdate;
      }
      m_tpListener = validatedListener;
      m_tpListener.CourtUpdate += courtUpdate;
    }

    protected void InitCourtCorrelator(ICourtCorrelator courtCorrelator) {
      ICourtCorrelator validatedCorrelator = courtCorrelator ?? throw new ArgumentNullException("ICourtCorrelator reference cannot be null");
      if (m_courtCorrelator != null) {
      }
      m_courtCorrelator = validatedCorrelator;
      _ = ReloadSBCourts();
      _ = ReloadTPCourts();
    }

    #region Court Update Handling
    private async void courtUpdate(object sender, TPListener.TPCourtUpdateEventArgs e) {
      if (m_courtCorrelator == null) {
        return;
      }
      if (m_apiInfo == null) {
        SendStatusMessage("Court update received, but no Scoreboard API connection", StatusMessageLevel.Warning);
        return;
      }

      var sbCourts = m_courtCorrelator.Correlate(e.CourtName);

      if (sbCourts.Count == 0) {
        SendStatusMessage($"An update was received for unassigned TP court {e.CourtName}.", StatusMessageLevel.Warning);
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
          SendStatusMessage($"Failed to clear court {sbCourt.Name} ({sbCourt.Venue?.Name}): {e.Message}", StatusMessageLevel.Error);
          continue;
        }
        SendStatusMessage($"Court {sbCourt.Name} ({sbCourt.Venue?.Name}) is now free", StatusMessageLevel.Info);
      }
    }

    private async Task MatchOnCourts(List<ScoreboardLiveApi.Court> sbCourts, TP.Match tpMatch) {
      var tpTournament = await GetTPTournament();
      if (tpTournament == null) {
        return;
      }

      var updatedTPMatch = tpTournament.FindMatchByID(tpMatch.ID);
      if (updatedTPMatch == null) {
        SendStatusMessage($"TP match {tpMatch.ID} not found in tournament", StatusMessageLevel.Warning);
        return;
      }

      var tpDraw = tpTournament.FindDrawByID(updatedTPMatch.DrawID);
      if (tpDraw == null) {
        SendStatusMessage($"Draw {updatedTPMatch.DrawID} not found in tournament", StatusMessageLevel.Warning);
        return;
      }

      var tpEvent = tpTournament.FindEventByID(tpDraw.EventID);
      if (tpEvent == null) {
        SendStatusMessage($"Event {tpDraw.EventID} not found in tournament", StatusMessageLevel.Warning);
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
        SendStatusMessage(ex.Message, StatusMessageLevel.Error);
        return;
      }

      if (sbMatch == null) {
        try {
          sbMatch = await CreateOnTheFlySBMatch(updatedTPMatch, tpEvent, sbTag);
        } catch (Exception ex) {
          SendStatusMessage(ex.Message, StatusMessageLevel.Error);
          return;
        }
      } else {
        try {
          sbMatch = await CheckIfScoreboardServerNeedsMatchUpdate(updatedTPMatch, sbMatch);
        } catch (Exception ex) {
          SendStatusMessage(ex.Message, StatusMessageLevel.Error);
          return;
        }
      }

      foreach (var sbCourt in sbCourts) {
        await AssignMatchToCourt(sbMatch, sbCourt);
        SendStatusMessage($"Court {sbCourt.Name} ({sbCourt.Venue?.Name}) is now occupied by {updatedTPMatch}", StatusMessageLevel.Info);
      }
    }

    private async Task<TP.Tournament> GetTPTournament() {
      try {
        var tpXML = await m_tpNetwork.GetTournamentInfo();
        var tmpNetworkData = new TP.VisualXML.TPNetwork(tpXML);
        return await TP.Tournament.LoadFromVisualXMLAsync(tmpNetworkData);
      } catch (Exception e) {
        SendStatusMessage($"Failed to load tournament from TP: {e.Message}", StatusMessageLevel.Error);
        return null;
      }
    }

    private string GetMatchTag(TP.Match tpMatch, TP.Event tpEvent, TP.Draw tpDraw) {
      return TP.Tournament.CreateMatchTag(m_apiInfo.Tournament, tpMatch, tpEvent, tpDraw);
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
      string category = TP.Tournament.CreateMatchCategoryString(tpEvent);
      var sbMatch = TP.Tournament.ConvertMatch(tpMatch, category);
      sbMatch.Tag = tag;
      return await m_apiInfo.Api.CreateOnTheFlyMatch(m_apiInfo.Device, m_apiInfo.Tournament, sbMatch);
    }

    private async Task<ScoreboardLiveApi.Match> CheckIfScoreboardServerNeedsMatchUpdate(TP.Match tpMatch, ScoreboardLiveApi.Match sbMatch) {
      (string team1player1name, string team1player1team, string team1player2name, string team1player2team) = TP.Tournament.ConvertEntry(tpMatch.Entries.Item1);
      (string team2player1name, string team2player1team, string team2player2name, string team2player2team) = TP.Tournament.ConvertEntry(tpMatch.Entries.Item2);
      
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
        SendStatusMessage($"Failed to assign match {sbMatch.MatchID} to court {sbCourt.Name}: {e.Message}", StatusMessageLevel.Error);
        return false;
      }
      return true;
    }
    #endregion

    private void SendStatusMessage(string message, StatusMessageLevel level) {
      Status?.Invoke(this, new StatusMessageEventArgs() { Message = message, Level = level });
    }

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
        var tmpNetworkData = new TP.VisualXML.TPNetwork(tournamentXML.Result);
        var tournament = await TP.Tournament.LoadFromVisualXMLAsync(tmpNetworkData);
        m_courtCorrelator.SetTPCourts(tournament.Courts);
      });
    }
  }
}
