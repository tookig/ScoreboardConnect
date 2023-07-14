using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreboardConnectWinUI3 {
  public partial class FormMain : Form {
    private static readonly string keyStoreFile = string.Format("{0}\\scoreboardConnectAppKeys.bin", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
    private static readonly string settingsFile = string.Format("{0}\\scoreboardConnectSettings.bin", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

    private Settings m_settings;
    private ScoreboardLiveApi.ApiHelper m_api;
    private ScoreboardLiveApi.Device m_device;
    private ScoreboardLiveApi.LocalDomainKeyStore m_keyStore;
    private ScoreboardLiveApi.Tournament m_tournament;

    public FormMain() {
      InitializeComponent();
      SetStatusDisconnected();
    }

    private void SetStatusDisconnected() {
      labelConnectionStatus.Text = "Not connected";
      labelConnectionStatus.ForeColor = Color.Red;
      labelSelectedUnit.Text = "";
      pictureLoading.Visible = false;
      buttonImportTP.Visible = false;
      buttonTPCourtListen.Visible = false;
      panelContent.Hide();
    }

    private void SetStatusLoading() {
      labelConnectionStatus.Text = "Loading...";
      labelConnectionStatus.ForeColor = Color.Blue;
      labelSelectedUnit.Text = "";
      pictureLoading.Visible = true;
      buttonImportTP.Visible = false;
      buttonTPCourtListen.Visible = false;
      panelContent.Hide();
      buttonRetryConnection.Visible = false;
    }

    private async Task SetStatusConnected(ScoreboardLiveApi.Device device) {
      ScoreboardLiveApi.Unit unit = (await m_api.GetUnits()).Find(u => u.UnitID == m_device.UnitID);
      if (unit == null) {
        return;
      }
      labelSelectedUnit.Text = string.Format("as {0}.", unit.Name);
      
      if (m_settings.SelectedTournaments.TryGetValue(unit.UnitID, out int selectedTournamentID)) {
        m_tournament = await m_api.GetTournament(selectedTournamentID, device);
        if (m_tournament == null) {
          return;
        }
        labelTournament.Text = m_tournament.Name;
      }

      labelConnectionStatus.Text = "Connected";
      labelConnectionStatus.ForeColor = Color.Green;
      pictureLoading.Visible = false;
      buttonImportTP.Visible = true;
      buttonTPCourtListen.Visible = true;
      panelContent.Hide();
      buttonRetryConnection.Visible = false;
    }

    private async Task Connect() {
      SetStatusLoading();
      m_api = new ScoreboardLiveApi.ApiHelper(m_settings.URL);
      if ((m_settings.UnitID < 1) || !m_settings.SelectedTournaments.ContainsKey(m_settings.UnitID)) {
        SetStatusDisconnected();
        return;
      }
      m_device = m_keyStore.Get(m_settings.UnitID);
      if (m_device == null) {
        SetStatusDisconnected();
        return;
      }

      try {
        if (await m_api.CheckCredentials(m_device)) {
          await SetStatusConnected(m_device);
        } else {
          SetStatusDisconnected();
          MessageBoxError(string.Format("Device activation code has expired. Enter a new activation code."));
        }
      } catch (Exception e) {
        SetStatusDisconnected();
        MessageBoxError(string.Format("Could not verify activation code, make sure settings{1}are correct.{1}{1}{0}", e.Message, Environment.NewLine));
        buttonRetryConnection.Visible = true;
      }
    }

    private void LoadKeyStore() {
      try {
        m_keyStore = ScoreboardLiveApi.LocalDomainKeyStore.Load(keyStoreFile);
      } catch (Exception e) {
        if (MessageBox.Show(this, string.Format("Could not load key store file: {1}{0}{0}Would you like to create a new key store?", e.Message, Environment.NewLine), "Could not load key store", MessageBoxButtons.YesNo) == DialogResult.Yes) {
          m_keyStore = new ScoreboardLiveApi.LocalDomainKeyStore(m_settings.URL);
        } else {
          Close();
        }
      }
      m_keyStore.DefaultDomain = m_settings.URL;
    }

    private void MessageBoxError(string text) {
      MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private async void FormMain_Load(object sender, EventArgs e) {
      m_settings = Settings.Load(settingsFile);
      LoadKeyStore();
      await Connect();
    }

    private void buttonExit_Click(object sender, EventArgs e) {
      Close();
    }

    private async void buttonSettings_Click(object sender, EventArgs e) {
      FormSettings settings = new FormSettings(m_settings, m_keyStore);
      if (settings.ShowDialog() == DialogResult.OK) {
        m_settings.URL = settings.URL;
        m_settings.UnitID = settings.Unit.UnitID;
        m_settings.SelectedTournaments[m_settings.UnitID] = settings.Tournament.TournamentID;
        m_settings.Save(settingsFile);
        m_keyStore.DefaultDomain = m_settings.URL;
        m_keyStore.Save(keyStoreFile);
        await Connect();
      }
    }

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
      buttonImportTP.Visible = false;
      buttonTPCourtListen.Visible = false;
      panelContent.Controls.Clear();
      ControlUploadTournament cut = new ControlUploadTournament(m_api, m_device, m_tournament);
      cut.OperationCompleted += Cut_OperationCompleted;
      panelContent.Controls.Add(cut);
      cut.Dock = DockStyle.Fill;
      panelContent.Show();
      buttonSettings.Hide();
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

    private void FormMain_Shown(object sender, EventArgs e) {
      ScoreboardConnectUpdate.UpdateForm updateForm = new ScoreboardConnectUpdate.UpdateForm();
      updateForm.ShowDialog(this);
    }
  }
}
