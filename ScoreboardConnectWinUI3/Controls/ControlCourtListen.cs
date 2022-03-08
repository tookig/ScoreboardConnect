using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreboardConnectWinUI3 {
  public partial class ControlCourtListen : UserControl {
    private ScoreboardLiveApi.ApiHelper m_helper;
    private ScoreboardLiveApi.Device m_device;
    private ScoreboardLiveApi.Tournament m_tournament;
    private List<TP.Court> m_tpCourts;

    private Dictionary<int, string> m_defaultSetup = new Dictionary<int, string>();

    public event EventHandler<(int, string)> CourtAssignmentChanged;

    public TP.TPListener Listener { get; private set; }

    public ControlCourtListen(ScoreboardLiveApi.ApiHelper api, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament tournament) {
      m_helper = api ?? throw new ArgumentNullException("Helper reference cannot be null");
      m_device = device ?? throw new ArgumentNullException("Device reference cannot be null");
      m_tournament = tournament ?? throw new ArgumentNullException("Tournament reference cannot be null");

      Listener = new TP.TPListener();
      Listener.CourtUpdate += listener_CourtUpdate;
      Listener.ServiceError += listener_ServiceError;
      Listener.ServiceStarted += listener_ServiceStarted;
      Listener.ServiceStopped += listener_ServiceStopped;

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
      try {
        PopulateCourts(await m_helper.GetCourts(m_device));
      } catch {
        ShowError("Could not get court info from server");
      }
    }

    private void PopulateCourts(List<ScoreboardLiveApi.Court> courts) {
      listCourts.PopulateScoreboardCourts(courts);
    }

    private async Task LoadTP() {
      if (openTPFile.ShowDialog() != DialogResult.OK) {
        return;
      }
      TP.TPFile tpFile;
      try {
        tpFile = new TP.TPFile(openTPFile.FileName);
      } catch {
        ShowError("Could not open TP file");
        return;
      }

      try {
        PopulateTP(await tpFile.GetCourts());
        SetStatusNotListening();
      } catch {
        ShowError("Could not get court info from TP file");
      }
    }

    private void PopulateTP(List<TP.Court> courts) {
      m_tpCourts = courts;
      listCourts.PopulateTPCourts(courts);
      listCourts.Enabled = true;
      UpdateDefaultSetup();
    }

    private void UpdateDefaultSetup() {
      foreach (KeyValuePair<int, string> kvp in m_defaultSetup) {
        listCourts.AssignTPCourtToScoreboardCourt(kvp.Key, kvp.Value);
      }
    }

    private async Task UpdateCourt(string tpCourtName, int tpMatchId) {
      var sbCourts = listCourts.GetScoreboardCourtsAssignedToTPCourt(tpCourtName);
      if (tpMatchId == 0) {
        sbCourts.ForEach(sbCourt => m_helper.ClearCourt(m_device, sbCourt));
      } else {
        try {
          var sbMatch = await m_helper.FindMatchBySequenceNumber(m_device, m_tournament, tpMatchId);
          if (sbMatch.Count == 1) {
            sbCourts.ForEach(async sbCourt => await m_helper.AssignMatchToCourt(m_device, sbMatch[0], sbCourt));
          }
        } catch (Exception e) {
          ShowError(e.Message);
        }
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
        await LoadTP();
      } else if (!Listener.IsListening) {
        Listener.Start();
      } else {
        Listener.Stop();
      }
    }

    private void listener_ServiceStopped(object sender, EventArgs e) {
      Invoke((MethodInvoker)delegate {
        SetStatusNotListening();
      });
    }

    private void listener_ServiceStarted(object sender, EventArgs e) {
      Invoke((MethodInvoker)delegate {
        SetStatusListening();
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

    private void ListCourts_CourtAssignmentChanged(object sender, (int, string) e) {
      Invoke((MethodInvoker) delegate {
        CourtAssignmentChanged?.Invoke(this, e);
      });
    }
  }
}
