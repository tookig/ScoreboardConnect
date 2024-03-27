using ScoreboardConnectWinUI3.Controls;
using ScoreboardConnectWinUI3.Forms;
using ScoreboardLiveApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP;

namespace ScoreboardConnectWinUI3 {
  public partial class FormMain : Form {
    private ScoreboardLiveControl.ScoreboardLiveConnectedEventArgs m_SBConnected;
    private TPNetworkControl.TPNetworkConnectedEventArgs m_TPNetworkConnected;

    private RequestCoordinator m_requestCoordinator;

    public FormMain() {
      InitializeComponent();

      m_requestCoordinator = new RequestCoordinator(tpNetworkControl1.SocketClient, tournamenttvControl.Listener, courtListView);

      UpdateButtons();
    }

    private void UpdateButtons() {
      buttonImportTP.Enabled = (m_SBConnected != null) && (m_TPNetworkConnected != null);
    }


    private void FormMain_Load(object sender, EventArgs e) {
      scoreboardLiveControl1.Connected += ScoreboardLiveControl1_Connected;
      scoreboardLiveControl1.Disconnected += ScoreboardLiveControl1_Disconnected;
      tpNetworkControl1.Connected += TpNetworkControl1_Connected;
      tpNetworkControl1.Disconnected += TpNetworkControl1_Disconnected;
    }

    private void TpNetworkControl1_Disconnected(object sender, System.Runtime.CompilerServices.AsyncVoidMethodBuilder e) {
      m_TPNetworkConnected = null;
      UpdateButtons();

    }

    private void TpNetworkControl1_Connected(object sender, TPNetworkControl.TPNetworkConnectedEventArgs args) {
      m_TPNetworkConnected = args;
      tournamenttvControl.SetInitialState(args.Tournament);
      UpdateButtons();

    }

    private void ScoreboardLiveControl1_Disconnected(object sender, System.Runtime.CompilerServices.AsyncVoidMethodBuilder e) {
      m_SBConnected = null;
      UpdateButtons();

    }

    private void ScoreboardLiveControl1_Connected(object sender, ScoreboardLiveControl.ScoreboardLiveConnectedEventArgs args) {
      m_SBConnected = args;
      m_requestCoordinator.ApiInfo = args;
      UpdateButtons();
    }

    private void buttonImportTP_Click(object sender, EventArgs e) {
      FormUpload formUpload = new FormUpload(m_SBConnected.Api, m_SBConnected.Device, m_SBConnected.Tournament, m_TPNetworkConnected.Tournament);
      formUpload.ShowDialog(this);
    }

    /*
    private void buttonTPCourtListen_Click(object sender, EventArgs e) {
      buttonImportTP.Visible = false;
      buttonTPCourtListen.Visible = false;
      panelContent.Controls.Clear();

      CourtListenControl clc = new CourtListenControl();
      clc.Init(new ScoreboardSetup() {
        Helper = m_api,
        Device = m_device,
        Tournament = m_tournament
      }, m_settings);
      clc.Cancelled += Listen_Cancelled;
      clc.CourtAssignmentChanged += Clc_CourtAssignmentChanged;
      
      panelContent.Controls.Add(clc);
      clc.Dock = DockStyle.Fill;
      panelContent.Show();
      buttonSettings.Hide();
    }

    private void Clc_CourtAssignmentChanged(object sender, (int sbCourtID, string tpCourtName) e) {
      if (!m_settings.CourtSetup.ContainsKey(m_settings.UnitID)) {
        m_settings.CourtSetup[m_settings.UnitID] = new Dictionary<int, string>();
      }
      m_settings.CourtSetup[m_settings.UnitID][e.sbCourtID] = e.tpCourtName;
      m_settings.Save(settingsFile);
    }

    private void Listen_Cancelled(object sender, EventArgs e) {
      buttonImportTP.Visible = true;
      buttonTPCourtListen.Visible = true;
      panelContent.Controls.Clear();
      panelContent.Hide();
      buttonSettings.Show();
    }

    private void buttonImportTP_Click(object sender, EventArgs e) {

    }

    private void Cut_OperationCompleted(object sender, EventArgs e) {
      buttonImportTP.Visible = true;
      buttonTPCourtListen.Visible = true;
      panelContent.Hide();
      buttonSettings.Show();
    }

    private async void buttonRetryConnection_Click(object sender, EventArgs e) {
      await Connect();
    }
        */

    private void FormMain_Shown(object sender, EventArgs e) {
      ScoreboardConnectUpdate.UpdateForm updateForm = new ScoreboardConnectUpdate.UpdateForm();
      updateForm.ShowDialog(this);
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
      Close();
    }
  }
}
