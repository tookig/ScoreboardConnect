using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace ScoreboardConnectWinUI3 {
  public partial class ControlCourtListen : UserControl {
    private ScoreboardLiveApi.ApiHelper m_helper;
    private ScoreboardLiveApi.Device m_device;
    private ScoreboardLiveApi.Tournament m_tournament;
    private List<TP.Court> m_tpCourts;

    private object m_tpEventsLock = new object();
    private List<TP.Event> m_tpEvents;
    // private List<TP.TournamentClass> m_tpTournamentClasses;
    private string m_tpFileName;

    private Dictionary<int, string> m_defaultSetup = new Dictionary<int, string>();

    public event EventHandler<(int, string)> CourtAssignmentChanged;

    // public TP.TPListener Listener { get; private set; }

    public ControlCourtListen(ScoreboardLiveApi.ApiHelper api, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament tournament) {
      m_helper = api ?? throw new ArgumentNullException("Helper reference cannot be null");
      m_device = device ?? throw new ArgumentNullException("Device reference cannot be null");
      m_tournament = tournament ?? throw new ArgumentNullException("Tournament reference cannot be null");

      /*
      Listener = new TP.TPListener();
      Listener.CourtUpdate += listener_CourtUpdate;
      Listener.EventUpdate += Listener_EventUpdate;
      Listener.ServiceError += listener_ServiceError;
      Listener.ServiceStarted += listener_ServiceStarted;
      Listener.ServiceStopped += listener_ServiceStopped;
      */

      InitializeComponent();
      SetStatusSelectFile();
      ShowError("");

      listCourts.CourtAssignmentChanged += ListCourts_CourtAssignmentChanged;
      listCourts.Enabled = false;
    }

    public void SetDefaultSetup(Dictionary<int, string> defaultCourtSetup) {
      if (defaultCourtSetup == null) throw new ArgumentNullException("defaultCourtSetup", "defaultCourtSetup cannot be null");
      m_defaultSetup = defaultCourtSetup;
      UpdateDefaultSetup();
    }

    private async Task LoadCourts() {
    }

    private async Task<bool> LoadTP() {
      if (openTPFile.ShowDialog() != DialogResult.OK) {
        return false;
      }
      m_tpFileName = openTPFile.FileName;
      await ReloadTP();
      return true;
    }

    private async Task<bool> ReloadTP() {
      TP.TPFile tpFile;
      try {
        tpFile = new TP.TPFile(m_tpFileName);
      } catch {
        ShowError("Could not open TP file");
        return false;
      }
      /*
      try {
        m_tpCourts = await tpFile.GetCourts();
        var tpEvents = await tpFile.GetEvents();
        lock (m_tpEventsLock) {
          m_tpEvents = tpEvents;
        }
        m_tpTournamentClasses = TP.Converter.Extract(m_tpEvents);
      } catch {
        ShowError("Could not get court info from TP file");
        return false;
      } finally {
        tpFile.Close();
      }
      */

      return true;
    }

    private void PopulateTP() {
      // listCourts.PopulateTPCourts(m_tpCourts);
      // listCourts.Enabled = true;
      UpdateDefaultSetup();
    }

    private void UpdateDefaultSetup() {
      foreach (KeyValuePair<int, string> kvp in m_defaultSetup) {
        listCourts.AssignTPCourtToScoreboardCourt(kvp.Key, kvp.Value);
      }
    }

    private async Task UpdateCourt(string tpCourtName, int tpMatchId) {
      /*
      var sbCourts = listCourts.GetScoreboardCourtsAssignedToTPCourt(tpCourtName);
      if (tpMatchId == 0) {
        sbCourts.ForEach(sbCourt => m_helper.ClearCourt(m_device, sbCourt));
      } else {
        try {
          TP.Event matchEvent = null;
          lock (m_tpEventsLock) {
            matchEvent = m_tpEvents.Find(tpEvent => tpEvent.FindMatchAndEntries(playerMatch => playerMatch.ID == tpMatchId) != (null, null, null));
          }

          List<ScoreboardLiveApi.Match> sbMatch = null;
          if (matchEvent != null) {
            sbMatch = await m_helper.FindMatchByTag(m_device, TP.Converter.HashMatch(matchEvent.TournamentInformation?.TournamentName, tpMatchId));
          } else {
            sbMatch = await m_helper.FindMatchBySequenceNumber(m_device, m_tournament, tpMatchId);
          }
          if (sbMatch?.Count > 0) {
            ScoreboardLiveApi.Match bestFit = sbMatch[0];
            if (tpMatchId > 0) {
              await CheckIfScoreboardServerNeedsMatchUpdate(bestFit, tpMatchId);
            }
            sbCourts.ForEach(async sbCourt => await m_helper.AssignMatchToCourt(m_device, bestFit, sbCourt));
          } else if (sbMatch?.Count == 0) {
            // The TP match could not be found on the SB server. Load the match from TP, and
            // create a new match for SB.
            TP.PlayerMatch newTPMatch = null;
            TP.Event newMatchEvent = null;
            TP.Entry entry1 = null;
            TP.Entry entry2 = null;
            lock (m_tpEventsLock) {
              foreach (TP.Event tpEvent in m_tpEvents) {
                (newTPMatch, entry1, entry2) = tpEvent.FindMatchAndEntries(tpMatch => tpMatch.ID == tpMatchId);
                if (newTPMatch != null) {
                  newMatchEvent = tpEvent;
                  break;
                }
              }
            }
            // If match was not found in the TP data, something has happened since the TP file was loaded. TODO: Reload TP data?
            if (newTPMatch == null) {
              throw new Exception("Tournament planner sent court with match that doesn't exist.");
            }
            if ((entry1 == null) || (entry2 == null)) {
              throw new Exception("Tournament planner sent court with match that is missing entries.");
            }
            // Create match on SB server
            ScoreboardLiveApi.MatchExtended newSBMatch = newTPMatch.MakeScoreboardMatch(entry1, entry2);
            newSBMatch.Category = newMatchEvent.CreateMatchCategoryString();
            newSBMatch.Tag = TP.Converter.HashMatch(matchEvent.TournamentInformation?.TournamentName, newTPMatch.ID);
            ScoreboardLiveApi.Match createdSBMatch = await m_helper.CreateOnTheFlyMatch(m_device, m_tournament, newSBMatch);
            // Assign to selected courts
            sbCourts.ForEach(async sbCourt => await m_helper.AssignMatchToCourt(m_device, createdSBMatch, sbCourt));
          }
        } catch (Exception e) {
          ShowError(e.Message);
        }
      }
      */
    }

    private async Task CheckIfScoreboardServerNeedsMatchUpdate(ScoreboardLiveApi.Match match, int tpMatchID) {
      /*
      bool sendUpdate = false;
      lock(m_tpEventsLock) {
        var tpMatches = new List<ScoreboardLiveApi.Match>();
        foreach (TP.TournamentClass tc in m_tpTournamentClasses) {
          tpMatches.AddRange(tc.Matches);
          foreach (TP.TournamentClass subTc in tc.ChildClasses) {
            tpMatches.AddRange(subTc.Matches);
          }
        }

        var tpMatch = tpMatches.Find(m => m.TournamentMatchNumber == tpMatchID);
        if ((tpMatch != null) && PlayerNameOnServerIsOld(match, tpMatch)) {
          match.Team1Player1Name = tpMatch.Team1Player1Name;
          match.Team1Player1Team = tpMatch.Team1Player1Team;
          match.Team1Player2Name = tpMatch.Team1Player2Name;
          match.Team1Player2Team = tpMatch.Team1Player2Team;
          match.Team2Player1Name = tpMatch.Team2Player1Name;
          match.Team2Player1Team = tpMatch.Team2Player1Team;
          match.Team2Player2Name = tpMatch.Team2Player2Name;
          match.Team2Player2Team = tpMatch.Team2Player2Team;
          sendUpdate = true;
        }
      }
      if (sendUpdate) {
        await m_helper.UpdateMatch(m_device, match);
      }
      */
    }

    private bool PlayerNameOnServerIsOld(ScoreboardLiveApi.Match serverMatch, ScoreboardLiveApi.Match localMatch) {
      if (string.IsNullOrWhiteSpace(localMatch.Team1Player1Name) || string.IsNullOrWhiteSpace(localMatch.Team2Player1Name)) {
        return false;
      }
      return (serverMatch.Team1Player1Name != localMatch.Team1Player1Name) || (serverMatch.Team2Player1Name != localMatch.Team2Player1Name);
    }


    private void UpdateTPEventsWithXMLData(List<TP.Event> xmlEvents) {
      /*
      List<TP.PlayerMatch> xmlMatches = new List<TP.PlayerMatch>();
      xmlEvents.ForEach(xmlEvent => xmlMatches.AddRange(xmlEvent.ExtractMatches()));

      lock (m_tpEventsLock) {
        List<TP.PlayerMatch> tpMatches = new List<TP.PlayerMatch>();
        m_tpEvents.ForEach(tpEvent => tpMatches.AddRange(tpEvent.ExtractMatches()));

        tpMatches.ForEach(tpMatch => {
          TP.PlayerMatch xmlMatch = xmlMatches.Find(xmlMatch => xmlMatch.ID == tpMatch.ID);
          if (xmlMatch != null) {
            tpMatch.EntryID = xmlMatch.EntryID;
            tpMatch.Entry = xmlMatch.Entry;
          }
        });

        m_tpTournamentClasses = TP.Converter.Extract(m_tpEvents);
      }
      */
    }

    private async Task InitCourts() {
      if (m_tpFileName == null) return;
      if (!await ReloadTP()) return;
      foreach (TP.Court court in m_tpCourts) {
        await UpdateCourt(court.Name, court.TpMatchID);
      }
    }

    private void SetStatusSelectFile() {
      buttonAction.Text = "Select TP-file";
    }

    private void SetStatusNotListening() {
      buttonAction.Text = "Start listening";
      progressBar.Style = ProgressBarStyle.Blocks;
    }

    private void SetStatusListening() {
      buttonAction.Text = "Stop listening";
      progressBar.Style = ProgressBarStyle.Marquee;
    }

    private void ShowError(string errorText) {
      labelStatus.Text = errorText;
      labelStatus.ForeColor = Color.Red;
    }

    private async void ControlCourtListen_Load(object sender, EventArgs e) {
      await LoadCourts();
    }

    private async void buttonAction_Click(object sender, EventArgs e) {
      if (m_tpCourts == null) {
        if (await LoadTP()) {
          PopulateTP();
          SetStatusNotListening();
        }
      } /* else if (!Listener.IsListening) {
        Listener.Start(m_tpCourts);
      } else {
        Listener.Stop();
      } */
    }

    private void listener_ServiceStopped(object sender, EventArgs e) {
      Invoke((MethodInvoker)delegate {
        SetStatusNotListening();
      });
    }

    private void listener_ServiceStarted(object sender, EventArgs e) {
      Invoke((MethodInvoker) async delegate {
        SetStatusListening();
        await InitCourts();
      });
    }

    private void listener_ServiceError(object sender, (string text, Exception exception) e) {
      Invoke((MethodInvoker)delegate {
        ShowError(e.text);
      });
    }

    private void listener_CourtUpdate(object sender, (string tpCourtName, int tpMatchId) e) {
      Invoke((MethodInvoker)async delegate {
        await UpdateCourt(e.tpCourtName, e.tpMatchId);
      });
    }

    private void Listener_EventUpdate(object sender, List<TP.Event> e) {
      UpdateTPEventsWithXMLData(e);
    }

    private void ListCourts_CourtAssignmentChanged(object sender, (int, string) e) {
      Invoke((MethodInvoker) delegate {
        CourtAssignmentChanged?.Invoke(this, e);
      });
    }
  }
}
