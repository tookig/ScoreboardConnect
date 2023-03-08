using ScoreboardLiveApi;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreboardConnectWinUI3 {
  partial class FormSettings : Form {
    private class ComboUnit {
      public Unit Unit { get; set; }

      public ComboUnit(Unit unit) {
        Unit = unit;
      }

      public override string ToString() {
        return Unit.Name;
      }
    }

    private Settings m_defaultSettings;
    private ApiHelper m_api;
    private LocalDomainKeyStore m_keyStore;
    private Device m_device;

    public string URL {
      get {
        return textURL.Text;
      }
    }

    public Unit Unit {
      get {
        return ((ComboUnit)comboUnit.SelectedItem).Unit;
      }
    }

    public Tournament Tournament {
      get {
        return (Tournament)comboTournament.SelectedItem;
      }
    }

    public FormSettings(Settings defaults, LocalDomainKeyStore keyStore) {
      InitializeComponent();
      m_defaultSettings = defaults;
      textURL.Text = defaults.URL;
      m_keyStore = keyStore;
      m_keyStore.DefaultDomain = defaults.URL;
      m_api = new ApiHelper(defaults.URL);
      SetDeviceStatusNotConnected();
    }

    private void SetDeviceStatusNotConnected() {
      labelDeviceStatus.Text = "";
      buttonRegisterDevice.Visible = false;
    }

    private void SetDeviceStatusNotRegistered() {
      labelDeviceStatus.Text = "This device is not registered on the server.";
      buttonRegisterDevice.Visible = true;
      buttonOK.Enabled = false;
      comboTournament.Items.Clear();
    }

    private void SetDeviceStatusRegistered() {
      if (m_device == null) return;
      labelDeviceStatus.Text = string.Format("This device is registered on the server as {0}.", m_device.DeviceCode);
      buttonRegisterDevice.Visible = false;
    }

    private async Task<bool> CheckDevice() {
      if ((m_device == null) || !await m_api.CheckCredentials(m_device)) {
        return false;
      }
      return true;
    }

    private async Task LoadUnits() {
      try {
        SetDeviceStatusNotRegistered();
        PopulateUnits(await m_api.GetUnits());
      } catch {
        SetDeviceStatusNotConnected();
      }
    }

    private void PopulateUnits(List<ScoreboardLiveApi.Unit> units) {
      comboUnit.Items.Clear();
      var comboUnits = units.Select(unit => new ComboUnit(unit)).ToArray();
      comboUnit.Items.AddRange(comboUnits);
      var selectedUnit = comboUnits.FirstOrDefault(cu => cu.Unit.UnitID == m_defaultSettings.UnitID);
      if (selectedUnit != null) {
        comboUnit.SelectedItem = selectedUnit;
      }
    }

    private async Task LoadDevice() {
      ComboUnit selectedUnit = comboUnit.SelectedItem as ComboUnit;
      if (selectedUnit == null) {
        return;
      }
      m_device = m_keyStore.Get(selectedUnit.Unit.UnitID);

      try {
        if ((m_device != null) && await CheckDevice()) {
          SetDeviceStatusRegistered();
          await LoadTournaments();
        } else {
          SetDeviceStatusNotRegistered();
          if (m_device != null) {
            m_keyStore.Remove(m_device);
            m_device = null;
          }
        }
      } catch (Exception e) {
        MessageBox.Show(string.Format("Could not connect to server:{1}{0}", e.Message, Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private async Task LoadTournaments() {
      try {
        PopulateTournaments(await m_api.GetTournaments(m_device, 15));
      } catch (Exception e) {
        MessageBox.Show(string.Format("Could not retrieve tournaments from server:{1}{0}", e.Message, Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void PopulateTournaments(List<Tournament> tournaments) {
      comboTournament.Items.Clear();
      comboTournament.Items.AddRange(tournaments.ToArray());

      ComboUnit selectedUnit = comboUnit.SelectedItem as ComboUnit;
      if ((selectedUnit != null) && m_defaultSettings.SelectedTournaments.TryGetValue(selectedUnit.Unit.UnitID, out int defaultTournamentID)) {
        var selectedTournament = tournaments.FirstOrDefault(t => t.TournamentID == defaultTournamentID);
        if (selectedTournament != null) {
          comboTournament.SelectedItem = selectedTournament;
        } else if (comboTournament.Items.Count > 0) {
          comboTournament.SelectedIndex = 0;
        } else {
          buttonOK.Enabled = false;
        }
      }
    }

    private async Task ShowActivationForm() {
      FormDeviceActivation fda = new FormDeviceActivation(m_api, m_keyStore);
      if (fda.ShowDialog() == DialogResult.OK) {
        await LoadDevice();
      }
    }

    private void buttonSetURL_Click(object sender, EventArgs e) {
      FormURL url = new FormURL(textURL.Text);
      if (url.ShowDialog() == DialogResult.OK) {
        textURL.Text = url.URL;
        m_api.BaseUrl = url.URL;
        m_keyStore.DefaultDomain = url.URL;
        PopulateUnits(url.Units);
      }
    }

    private async void FormSettings_Load(object sender, EventArgs e) {
      await LoadUnits();
    }

    private async void comboUnit_SelectedIndexChanged(object sender, EventArgs e) {
      await LoadDevice ();
    }

    private async void buttonRegisterDevice_Click(object sender, EventArgs e) {
      await ShowActivationForm();
    }

    private void comboTournament_SelectedIndexChanged(object sender, EventArgs e) {
      buttonOK.Enabled = comboTournament.SelectedItem != null;
    }

    private void buttonOK_Click(object sender, EventArgs e) {
      DialogResult = DialogResult.OK;
    }
  }
}
