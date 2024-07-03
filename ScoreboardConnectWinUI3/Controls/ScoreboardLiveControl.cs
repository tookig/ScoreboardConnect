using ScoreboardLiveApi;
using ScoreboardLiveWebSockets;
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
using static ScoreboardLiveWebSockets.ScoreboardWebSocketClient;

namespace ScoreboardConnectWinUI3.Controls {
  public partial class ScoreboardLiveControl : UserControl {
    private static readonly int RECONNECT_INTERVAL = 10000;
    private static readonly string CONNECTED = "Connected";
    private static readonly string DISCONNECTED = "Not connected";
    private static readonly string PARTIALLY_CONNECTED = "Partially connected";
    private static readonly string LOADING = "Loading...";

    private Settings m_settings;
    private ApiHelper m_api;
    private Device m_device;
    private IKeyStore m_keyStore;
    private Tournament m_tournament;
    private Unit m_unit;
    private ScoreboardWebSocketClient m_webSocket = new();

    private object m_lock = new();
    private bool m_apiChecked = false;
    private bool m_isConnecting = false;
    private bool m_isConnected = false;

    public event EventHandler<ScoreboardLiveConnectedEventArgs> Connected;
    public event EventHandler<AsyncVoidMethodBuilder> Disconnected;

    public class ScoreboardLiveConnectedEventArgs : EventArgs {
      public Tournament Tournament { get; set; }
      public Device Device { get; set; }
      public ApiHelper Api { get; set; }
      public ScoreboardWebSocketClient WebSocket { get; set; }
      public ScoreboardLiveConnectedEventArgs(Tournament tournament, Device device, ApiHelper api, ScoreboardWebSocketClient webSocket) {
        Tournament = tournament;
        Device = device;
        Api = api;
        WebSocket = webSocket;
      }
    }

    public ScoreboardLiveControl() {
      InitializeComponent();
      InitSocket();
      SetStatusDisconnected();
    }

    public void SetSettings(Settings settings, IKeyStore keyStore) {
      m_settings = settings ?? throw new ArgumentNullException("settings", "Settings reference cannot be null");
      m_keyStore = keyStore ?? throw new ArgumentNullException("keyStore", "Key store reference cannot be null");
      _ = Connect();
     }

    private void SetStatusDisconnected() {
      labelStatus.Text = DISCONNECTED;
      labelStatus.ForeColor = Color.Red;
      labelUnit.Text = "";
      labelTournament.Text = "";
      labelUnit.Visible = false;
      labelTournament.Visible = false;
      pictureLoading.Visible = false;
      Disconnected?.Invoke(this, default);
      ScheduleReconnectAttempt();
    }

    private void SetStatusLoading() {
      labelStatus.Text = LOADING;
      labelStatus.ForeColor = Color.Blue;
      labelUnit.Text = "";
      labelTournament.Text = "";
      labelUnit.Visible = false;
      labelTournament.Visible = false;
      pictureLoading.Visible = true;
    }

    private void SetStatusPartiallyConnected() {
      labelUnit.Text = string.Format(m_unit.Name);
      labelTournament.Text = m_tournament.Name;
      labelUnit.Visible = true;
      labelTournament.Visible = true;
      labelStatus.Text = PARTIALLY_CONNECTED;
      labelStatus.ForeColor = Color.Orange;
      pictureLoading.Visible = false;
      Connected?.Invoke(this, new ScoreboardLiveConnectedEventArgs(m_tournament, m_device, m_api, m_webSocket));
    }

    private void SetStatusConnected() {
      labelUnit.Text = string.Format(m_unit.Name);
      labelTournament.Text = m_tournament.Name;
      labelUnit.Visible = true;
      labelTournament.Visible = true;
      labelStatus.Text = CONNECTED;
      labelStatus.ForeColor = Color.Green;
      pictureLoading.Visible = false;
      Connected?.Invoke(this, new ScoreboardLiveConnectedEventArgs(m_tournament, m_device, m_api, m_webSocket));
    }

    private void InitSocket() {
      m_webSocket.StateChanged += Socket_StateChanged;
      m_webSocket.MessageReceived += Socket_MessageReceived;
    }


    private async Task Connect() {
      if ((m_settings == null) || (m_keyStore == null)) {
        throw new InvalidOperationException("Settings and key store must be set before connecting");
      }

      lock (m_lock) {
        if (m_isConnecting || m_isConnected) return;
        m_apiChecked = false;
        m_isConnecting = true;
        m_isConnected = false;
      }

      SetStatusLoading();
      m_api = new ApiHelper(m_settings.URL, acceptAnyCertificates: true); // TODO: Remove acceptAnyCertificates in production
      m_api.Error += OnApiError;
      if ((m_settings.UnitID < 1) || !m_settings.SelectedTournaments.ContainsKey(m_settings.UnitID)) {
        SetStatusDisconnected();
        lock (m_lock) m_isConnecting = false;
        return;
      }
      m_device = m_keyStore.Get(m_settings.UnitID);
      if (m_device == null) {
        SetStatusDisconnected();
        lock (m_lock) m_isConnecting = false;
        return;
      }

      m_unit = null;
      ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Info, $"Connecting to {m_api?.BaseUrl}");
      try {
        m_unit = (await m_api.GetUnits()).Find(u => u.UnitID == m_device.UnitID);
      } catch {
        SetStatusDisconnected();
      }
      if (m_unit == null) {
        lock (m_lock) m_isConnecting = false;
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Warning, $"Could not connect to the ScoreboardLive network: unit {m_device.UnitID} not found. Check the settings and select another club.");
        return;
      }
      

      if (m_settings.SelectedTournaments.TryGetValue(m_unit.UnitID, out int selectedTournamentID)) {
        try {
          m_tournament = await m_api.GetTournament(selectedTournamentID, m_device);
        }
        catch { 
          SetStatusDisconnected();
        }
      }
      if (m_tournament == null) {
        lock (m_lock) m_isConnecting = false;
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Warning, $"Could not connect to the ScoreboardLive network: Tournament not found. Check the settings and select another tournament.");
        return;
      }

      try {
        if (await m_api.CheckCredentials(m_device)) {
          SetStatusPartiallyConnected();
        } else {
          SetStatusDisconnected();
          ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Warning, "Device activation code has expired. Enter a new activation code.");
          MessageBoxError(string.Format("Device activation code has expired. Enter a new activation code."));
          return;
        }
      } catch (Exception e) {
        SetStatusDisconnected();
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Warning, string.Format("Could not verify activation code, make sure settings{1}are correct.{1}{1}{0}", e.Message, Environment.NewLine));
        MessageBoxError(string.Format("Could not verify activation code, make sure settings{1}are correct.{1}{1}{0}", e.Message, Environment.NewLine));
        return;
      } finally {
        lock (m_lock) m_isConnecting = false;
      }

      lock (m_lock) {
        m_apiChecked = true;
        m_isConnected = true;
      }

      ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Info, $"Connected to {m_api?.BaseUrl}");

      await ConnectSocket();
    }

    private void Disconnect() {
      lock (m_lock) {
        if (!m_isConnected) return;
        m_isConnected = false;
        m_apiChecked = false;
        m_api = null;
        m_device = null;
        m_unit = null;
        m_tournament = null;
      }
      if (m_webSocket != null) {
        m_webSocket.Stop();
      }
      ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Info, "Disconnected from the ScoreboardLive network.");
      Invoke((MethodInvoker)delegate {
        SetStatusDisconnected();
      });
    }

    private void OnApiError(object sender, ApiHelperErrorEventArgs e) {
      if (e.Sender != m_api) return;
      if (e.Stage == ApiHelperErrorEventArgs.ErrorStage.ConnectionError) {
        // Connection failed, disconnect
        Disconnect();
      }
    }

    private async Task ConnectSocket() {
      // Get socket URL
      string socketURL;
      try {
        socketURL = await m_api.GetSocketURL();
      } catch {
        return;
      }
      // Try to connect to socket
      m_webSocket.Initialize(socketURL);
      m_webSocket.Start(); 
    }

    private void ScheduleReconnectAttempt() {
      if ((m_settings == null) || (m_keyStore == null)) {
        return;
      }
      ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Verbose, $"Reconnecting in {RECONNECT_INTERVAL / 1000} seconds...");
      Task.Delay(RECONNECT_INTERVAL).ContinueWith(_ => {
        Invoke((MethodInvoker)delegate {
          _ = Connect();
        });
      });
    }

    private void MessageBoxError(string text) {
      MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void ScoreboardLiveControl_Load(object sender, EventArgs e) {
    }

    private void Socket_MessageReceived(object sender, MessageEventArgs e) {

    }

    private void Socket_StateChanged(object sender, StateEventArgs e) {
      bool apiConnected = false;
      lock (m_lock) {
        apiConnected = m_apiChecked && m_isConnected;
      }

      Invoke((MethodInvoker)delegate {
        if (e.State == ClientState.Connected) {
          ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Info, "WebSocket connected.");
          SetStatusConnected();
        } else if (apiConnected) {
          ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Info, "WebSocket disconnected.");
          SetStatusPartiallyConnected();
        } else {
          ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Info, "WebSocket disconnected.");
          SetStatusDisconnected();
        }
      });
    }
  }
}
