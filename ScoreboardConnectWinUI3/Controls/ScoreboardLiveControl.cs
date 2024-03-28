using ScoreboardLiveApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreboardConnectWinUI3.Controls {
  public partial class ScoreboardLiveControl : UserControl {
   
    private Settings m_settings;
    private ApiHelper m_api;
    private Device m_device;
    private IKeyStore m_keyStore;
    private Tournament m_tournament;

    public event EventHandler<ScoreboardLiveConnectedEventArgs> Connected;
    public event EventHandler<AsyncVoidMethodBuilder> Disconnected;

    public class ScoreboardLiveConnectedEventArgs : EventArgs {
      public Tournament Tournament { get; set; }
      public Device Device { get; set; }
      public ApiHelper Api { get; set; }
      public ScoreboardLiveConnectedEventArgs(Tournament tournament, Device device, ApiHelper api) {
        Tournament = tournament;
        Device = device;
        Api = api;
      }
    }

    public ScoreboardLiveControl() {
      InitializeComponent();
      SetStatusDisconnected();
    }

    public void SetSettings(Settings settings, IKeyStore keyStore) {
      m_settings = settings ?? throw new ArgumentNullException("settings", "Settings reference cannot be null");
      m_keyStore = keyStore ?? throw new ArgumentNullException("keyStore", "Key store reference cannot be null");
      _ = Connect();
     }

    private void SetStatusDisconnected() {
      labelStatus.Text = "Not connected";
      labelStatus.ForeColor = Color.Red;
      labelUnit.Text = "";
      labelTournament.Text = "";
      pictureLoading.Visible = false;
      Disconnected?.Invoke(this, default);
    }

    private void SetStatusLoading() {
      labelStatus.Text = "Loading...";
      labelStatus.ForeColor = Color.Blue;
      labelUnit.Text = "";
      labelTournament.Text = "";
      pictureLoading.Visible = true;
    }

    private async Task SetStatusConnected(Device device) {
      Unit unit = (await m_api.GetUnits()).Find(u => u.UnitID == m_device.UnitID);
      if (unit == null) {
        return;
      }

      if (m_settings.SelectedTournaments.TryGetValue(unit.UnitID, out int selectedTournamentID)) {
        m_tournament = await m_api.GetTournament(selectedTournamentID, device);
      }
      if (m_tournament == null) {
        return;
      }

      labelUnit.Text = string.Format(unit.Name);
      labelTournament.Text = m_tournament.Name;
      labelStatus.Text = "Connected";
      labelStatus.ForeColor = Color.Green;
      pictureLoading.Visible = false;

      Connected?.Invoke(this, new ScoreboardLiveConnectedEventArgs(m_tournament, m_device, m_api));
    }

    private async Task Connect() {
      if ((m_settings == null) || (m_keyStore == null)) {
        throw new InvalidOperationException("Settings and key store must be set before connecting");
      }

      SetStatusLoading();
      m_api = new ApiHelper(m_settings.URL, acceptAnyCertificates: true); // TODO: Remove acceptAnyCertificates in production
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
      }
    }

    private void MessageBoxError(string text) {
      MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void ScoreboardLiveControl_Load(object sender, EventArgs e) {
    }
  }
}
