using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Linq;
using ScoreboardLiveApi;

namespace ScoreboardConnectWinUI3 {
  partial class CourtListenControl : UserControl, TP.SBUploader.ICourtMapper {
    private ScoreboardSetup m_serverSetup;
    private Settings m_settings;
    private TabPage m_forceTab;

    private List<TP.Court> m_tpCourts = new List<TP.Court>();
    private List<ScoreboardLiveApi.Court> m_sbCourts = new List<ScoreboardLiveApi.Court>();

    private TP.LinkService m_listener;
    private object m_listenerLock = new object();

    private bool m_isCancelled = false;
    private object m_isCancelledLock = new object();

    private Dictionary<ScoreboardLiveApi.Court, TP.Court> m_currentCourtSetup;
    private object m_currentCourtSetupLock = new object();

    public event EventHandler Cancelled;
    public event EventHandler<(int, string)> CourtAssignmentChanged;




    public CourtListenControl() {
      InitializeComponent();
      m_forceTab = tabPageSelectTPFile;
      courtList.CourtAssignmentChanged += ListCourts_CourtAssignmentChanged;
    }

    public void Init(ScoreboardSetup setup, Settings settings) {
      m_serverSetup = setup;
      m_settings = settings;
    }

    private void tabPages_SelectedIndexChanged(object sender, EventArgs e) {
      if (tabPages.SelectedTab != m_forceTab) {
        tabPages.SelectedTab = m_forceTab;
        return;
      }

      if (tabPages.SelectedTab == tabPageLoadScoreboardCourts) {
        buttonLoadScoreboardCourts.PerformClick();
      } else if (tabPages.SelectedTab == tabPageSetupCourts) {
        UpdateDefaultSetup();
      } else if (tabPages.SelectedTab == tabPageListening) {
        if (!m_listener.IsStarted) {
          m_listener.Start();
        }
      }
    }

    private async void buttonSelectTPFile_Click(object sender, EventArgs e) {
      if (openTPFile.ShowDialog() != DialogResult.OK) {
        return;
      }
      buttonSelectTPFile.Enabled = false;
      if (await LoadTP(openTPFile.FileName)) {
        m_forceTab = tabPageLoadScoreboardCourts;
        tabPages.SelectedTab = tabPageLoadScoreboardCourts;
      }
      CreateListener(openTPFile.FileName);
      buttonSelectTPFile.Enabled = true;
    }

    private async Task<bool> LoadTP(string filename) {
      TP.TPFile tpFile;
      try {
        tpFile = new TP.TPFile(filename);

        var rawCourts = await tpFile.LoadCourts();
        var locations = await tpFile.LoadLocations();

        m_tpCourts.Clear();
        m_tpCourts.AddRange(rawCourts.Select(raw => TP.Court.Parse(raw, locations)));
      } catch (Exception e) {
        ShowError("Could not open TP file", e);
        return false;
      }
      return true;
    }

    private async Task<bool> LoadScoreboard() {
      m_sbCourts.Clear();
      try {
        m_sbCourts.AddRange(await m_serverSetup.Helper.GetCourts(m_serverSetup.Device));
      } catch (Exception e) {
        ShowError("Could not load courts from the ScoreboardLive server.", e);
        return false;
      }
      // courtList.Populate(m_sbCourts, m_tpCourts);
      return true;
    }

    private void UpdateDefaultSetup() {
      if (m_settings.CourtSetup.TryGetValue(m_serverSetup.Device.UnitID, out var courtSetup)) {
        foreach (KeyValuePair<int, string> kvp in courtSetup) {
          // courtList.AssignTPCourtToScoreboardCourt(kvp.Key, kvp.Value);
        }
      }
    }

    private void CreateListener(string tpFileName) {
      lock (m_listenerLock) {
        if (m_listener != null) {
          m_listener.ServiceStarted -= listener_ServiceStarted;
          m_listener.ServiceStopped -= listener_ServiceStopped;
          m_listener.ServiceError -= listener_ServiceError;
          m_listener.ServiceWarning -= listener_ServiceWarning;
          m_listener.CourtUpdate -= listener_CourtUpdate;
          m_listener.Stop();
        }
        m_listener = new TP.LinkService(tpFileName, m_serverSetup.Helper, m_serverSetup.Device, m_serverSetup.Tournament, this);
        m_listener.ServiceStarted += listener_ServiceStarted;
        m_listener.ServiceStopped += listener_ServiceStopped;
        m_listener.ServiceError += listener_ServiceError;
        m_listener.ServiceWarning += listener_ServiceWarning;
        m_listener.CourtUpdate += listener_CourtUpdate;
      }
    }

    private void CreateCurrentCourtSetupSnapshot() {
      lock (m_currentCourtSetupLock) {
        m_currentCourtSetup = courtList.GetSnapshot();
      }
    }

    private void listener_CourtUpdate(object sender, (ScoreboardLiveApi.Court, ScoreboardLiveApi.Match) e) {
      Invoke((MethodInvoker)delegate {
        string header = e.Item1?.Name ?? "Unknown court";
        string message = e.Item2 == null ?
                         "Court cleared" :
                         string.Format("Match {0} assigned to court", e.Item2.MatchID);
        flowListener.Controls.Add(new ListenerMessageControl(header, message));
      });
    }

    private void listener_ServiceError(object sender, (string, Exception) e) {
      Invoke((MethodInvoker)delegate {
        if (m_listener.IsStarted) {
          // Add error to flow
          flowListener.Controls.Add(new ListenerMessageControl(e.Item1, e.Item2.Message, ListenerMessageControl.MessageType.Error));
        } else {
          // Show error box
          ShowError(e.Item1, e.Item2);
          m_forceTab = tabPageSetupCourts;
          tabPages.SelectedTab = tabPageSetupCourts;
        }
      });
    }

    private void listener_ServiceWarning(object sender, string e) {
      Invoke((MethodInvoker)delegate {
        flowListener.Controls.Add(new ListenerMessageControl("Warning", e, ListenerMessageControl.MessageType.Warning));
      });
    }

    private void listener_ServiceStopped(object sender, EventArgs e) {
      Invoke((MethodInvoker)delegate {
        labelListeningStatus.Text = "Not listening";
        labelListeningStatus.ForeColor = Color.Red;
        m_forceTab = tabPageSetupCourts;
        tabPages.SelectedTab = tabPageSetupCourts;
      });
    }

    private void listener_ServiceStarted(object sender, EventArgs e) {
      Invoke((MethodInvoker)delegate {
        labelListeningStatus.Text = "Listening...";
        labelListeningStatus.ForeColor = Color.DarkGreen;
      });
    }

    private void ShowError(string errorText, Exception e = null) {
      lock (m_isCancelledLock) {
        if (m_isCancelled) return;
      }

      StringBuilder sb = new StringBuilder();
      sb.AppendLine(errorText);
      if (e != null) {
        sb.AppendLine();
        sb.AppendLine(e.Message);
      }

      MessageBox.Show(this, sb.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void doCancel() {
      lock (m_isCancelledLock) {
        m_isCancelled = true;
      }
      Cancelled?.Invoke(this, new EventArgs());
    }

    private async void buttonLoadScoreboardCourts_Click(object sender, EventArgs e) {
      buttonLoadScoreboardCourts.Visible = false;
      labelLoadingScoreboardCourts.Visible = true;
      progressLoadCourtsFromServer.Visible = true;
      if (await LoadScoreboard()) {
        m_forceTab = tabPageSetupCourts;
        tabPages.SelectedTab = tabPageSetupCourts;
      } else {
        buttonLoadScoreboardCourts.Visible = true;
        labelLoadingScoreboardCourts.Visible = false;
        progressLoadCourtsFromServer.Visible = false;
      }
    }

    private void buttonCancelTPFile_Click(object sender, EventArgs e) {
      doCancel();
    }
    private void buttonCancelScoreboardCourts_Click(object sender, EventArgs e) {
      doCancel();
    }
    private void buttonCancelCourtSetup_Click(object sender, EventArgs e) {
      doCancel();
    }
    private void ListCourts_CourtAssignmentChanged(object sender, (int, string) e) {
      Invoke((MethodInvoker)delegate {
        CourtAssignmentChanged?.Invoke(this, e);
      });
    }

    private void buttonStopListening_Click(object sender, EventArgs e) {
      m_forceTab = tabPageSetupCourts;
      tabPages.SelectedTab = tabPageSetupCourts;
      m_listener.Stop();
    }

    private void buttonStartListening_Click(object sender, EventArgs e) {
      CreateCurrentCourtSetupSnapshot();
      m_forceTab = tabPageListening;
      tabPages.SelectedTab = tabPageListening;
    }

    private void flowListener_ControlAdded(object sender, ControlEventArgs e) {
      e.Control.Width = flowListener.Width - SystemInformation.VerticalScrollBarWidth * 2;
      flowListener.ScrollControlIntoView(e.Control);
    }

    delegate Court CourtInvoker();
    public Task<Court> MapTPCourtToSBCourt(string tpCourtName) {
      Court result = null;
      lock (m_currentCourtSetupLock) {
        foreach (var kvp in m_currentCourtSetup) {
          if (kvp.Value.Name == tpCourtName) {
            result = kvp.Key;
            break;
          }
        }
      }
      return Task<Court>.Factory.StartNew(() => result);

      /*var sbCourtObject = Invoke((CourtInvoker)delegate {
        return courtList.GetScoreboardCourtsAssignedToTPCourt(tpCourtName).FirstOrDefault();
      }) as Court; 
      return new Task<Court>(() => sbCourtObject);
      */
    }
  }
}
