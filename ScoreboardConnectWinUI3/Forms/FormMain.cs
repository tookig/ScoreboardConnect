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
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP;

namespace ScoreboardConnectWinUI3 {
  public partial class FormMain : Form {
    private static readonly string keyStoreFile = string.Format("{0}\\scoreboardConnectAppKeys.bin", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
    private static readonly string settingsFile = string.Format("{0}\\scoreboardConnectSettings.bin", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));


    private ScoreboardLiveControl.ScoreboardLiveConnectedEventArgs m_SBConnected;
    private TPNetworkControl.TPNetworkConnectedEventArgs m_TPNetworkConnected;
    private Settings m_settings;
    private LocalDomainKeyStore m_keyStore;

    private RequestCoordinator m_requestCoordinator;

    public FormMain() {
      InitializeComponent();

      m_requestCoordinator = new RequestCoordinator(tpNetworkControl1.SocketClient, tournamenttvControl.Listener, courtListView);


      UpdateButtons();
    }

    private void LoadSettings() {
      try {
        m_settings = Settings.Load(settingsFile);
      } catch {
        if (File.Exists(settingsFile)) {
          File.Delete(settingsFile);
        }
        m_settings = new Settings("https://www.scoreboardlive.se");
      }
    }

    private void LoadKeyStore() {
      if (m_settings == null) {
        throw new InvalidOperationException("Settings must be loaded before key store");
      }

      try {
        m_keyStore = ScoreboardLiveApi.LocalDomainKeyStore.Load(keyStoreFile);
      } catch (Exception e) {
        if (MessageBox.Show(this, string.Format("Could not load key store file: {1}{0}{0}Would you like to create a new key store?", e.Message, Environment.NewLine), "Could not load key store", MessageBoxButtons.YesNo) == DialogResult.Yes) {
          m_keyStore = new ScoreboardLiveApi.LocalDomainKeyStore(m_settings.URL);
        } else {
          Application.Exit();
        }
      }
      m_keyStore.DefaultDomain = m_settings.URL;
    }

    private void UpdateButtons() {
      uploadTournamentToolStripMenuItem.Enabled = m_SBConnected != null;
    }

    private void FormMain_Load(object sender, EventArgs e) {
      if (!DesignMode) {
        LoadSettings();
        LoadKeyStore();
        SettingsChanged();
      }
      scoreboardLiveControl1.Connected += ScoreboardLiveControl1_Connected;
      scoreboardLiveControl1.Disconnected += ScoreboardLiveControl1_Disconnected;
      tpNetworkControl1.Connected += TpNetworkControl1_Connected;
      tpNetworkControl1.Disconnected += TpNetworkControl1_Disconnected;
      courtListView.CourtAssignmentChanged += CourtListView_CourtAssignmentChanged;
    }

    private void CourtListView_CourtAssignmentChanged(object sender, (int sbCourtID, string tpCourtName) e) {
      if (!m_settings.CourtSetup.ContainsKey(m_settings.UnitID)) {
        m_settings.CourtSetup[m_settings.UnitID] = new Dictionary<int, string>();
      }
      m_settings.CourtSetup[m_settings.UnitID][e.sbCourtID] = e.tpCourtName;
      m_settings.Save(settingsFile);
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

    private void SettingsChanged() {
      if (m_settings.CourtSetup.TryGetValue(m_settings.UnitID, out Dictionary<int, string> courtSetup)) {
        courtListView.SetDefaultSetup(courtSetup);
      }
      scoreboardLiveControl1.SetSettings(m_settings, m_keyStore);
    }

    private void FormMain_Shown(object sender, EventArgs e) {
      ScoreboardConnectUpdate.UpdateForm updateForm = new ScoreboardConnectUpdate.UpdateForm();
      updateForm.ShowDialog(this);
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
      Close();
    }

    private void settingsToolStripMenuItem_Click(object sender, EventArgs e) {
      FormSettings settings = new FormSettings(m_settings, m_keyStore);
      if (settings.ShowDialog() == DialogResult.OK) {
        m_settings.URL = settings.URL;
        m_settings.UnitID = settings.Unit.UnitID;
        m_settings.SelectedTournaments[m_settings.UnitID] = settings.Tournament.TournamentID;
        m_settings.Save(settingsFile);
        m_keyStore.DefaultDomain = m_settings.URL;
        m_keyStore.Save(keyStoreFile);
        SettingsChanged();
      }
    }

    private async void uploadTournamentToolStripMenuItem_Click(object sender, EventArgs e) {
      TP.Tournament tournament = m_TPNetworkConnected?.Tournament ?? await LoadTournamentFromFile();
      if (tournament == null) return;
      FormUpload formUpload = new FormUpload(m_SBConnected.Api, m_SBConnected.Device, m_SBConnected.Tournament, tournament);
      formUpload.ShowDialog(this);
    }

    private async Task<TP.Tournament> LoadTournamentFromFile() {
      if (openTPFileDialog.ShowDialog() != DialogResult.OK) return null;
      try {
        TPFile tPFile = new TPFile(openTPFileDialog.FileName);
        return await TP.Tournament.LoadFromTP(tPFile);
      } catch (Exception e) {
        MessageBox.Show(this, string.Format("Could not open file: {1}{0}{0}{2}", e.Message, Environment.NewLine, openTPFileDialog.FileName), "Could not open file", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return null;
      }
    }

    private void onOffCourtChanges_CheckedChanged(object sender, EventArgs e) {
      m_requestCoordinator.EnableCourtSync = onOffCourtChanges.Checked;
    }

    private void onOffUpdateMatchResult_Click(object sender, EventArgs e) {
      m_requestCoordinator.EnableMatchResultUpdates = onOffUpdateMatchResult.Checked;
    }
  }
}
