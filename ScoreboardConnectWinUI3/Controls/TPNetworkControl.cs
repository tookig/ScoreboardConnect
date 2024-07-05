using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP;
using TPNetwork;

namespace ScoreboardConnectWinUI3.Controls {
  public partial class TPNetworkControl : UserControl {
    private static readonly int CONNECTION_CHECK_INTERVAL = 10000;

    private DateTime m_lastCheck = DateTime.MinValue;
    private bool m_lastCheckSuccess = false;
    private Tournament m_tournament;
    private TPNetwork.SocketClient m_socketClient;

    public event EventHandler<TPNetworkConnectedEventArgs> Connected;
    public event EventHandler<AsyncVoidMethodBuilder> Disconnected;

    public SocketClient SocketClient => m_socketClient;

    public class TPNetworkConnectedEventArgs : EventArgs {
      public Tournament Tournament { get; set; }
      public TPNetworkConnectedEventArgs(Tournament tournament) {
        Tournament = tournament;
      }
    }

    public TPNetworkControl() {
      InitializeComponent();
      m_socketClient = new TPNetwork.SocketClient();
      m_socketClient.Error += (sender, e) => ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Verbose, e.Message, e);
      m_socketClient.MessageReceived += (sender, xml) => ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Verbose, "Received XML from TP Network");
      m_socketClient.MessageSent += (sender, message) => ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Verbose, $"Sent {message.ActionID} message to TP Network");
    }

    private void SetStatusNotConnected() {
      labelStatus.Text = "Not connected";
      labelStatus.ForeColor = Color.Red;
      labelInstructions.Visible = true;
      labelTournament.Visible = false;
      Disconnected?.Invoke(this, default);
    }

    private void SetStatusConnected() {
      labelStatus.Text = "Connected";
      labelStatus.ForeColor = Color.Green;
      labelInstructions.Visible = false;
      labelTournament.Text = "Tournament: " + m_tournament.TournamentSettings.TournamentName;
      labelTournament.Visible = true;
      Connected?.Invoke(this, new TPNetworkConnectedEventArgs(m_tournament));
    }

    public async Task CheckConnection() {
      if (DateTime.Now - m_lastCheck < TimeSpan.FromMilliseconds(CONNECTION_CHECK_INTERVAL)) {
        return;
      }
      m_lastCheck = DateTime.Now;
      if (!m_lastCheckSuccess) {
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Verbose, "Trying to connect to TP Network");
      }
      try {
        var xml = await m_socketClient.GetTournamentInfo();
        m_tournament = Tournament.LoadFromVisualXML(new TP.VisualXML.TPNetworkDocument(xml));
        SetStatusConnected();
        if (!m_lastCheckSuccess) {
          ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Verbose, $"Connected to TP Network on port {m_socketClient.Port}");
        }
        m_lastCheckSuccess = true;
      } catch (Exception e) {
        SetStatusNotConnected();
        ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Verbose, $"Failed to connect to TP Network on port {m_socketClient.Port}", e);
        m_lastCheckSuccess = false;
      }
    }

    private async void TPNetworkControl_Load(object sender, EventArgs e) {
      SetStatusNotConnected();
      if (!DesignMode) {
        await CheckConnection();
        connectionCheckTimer.Start();
      }
    }

    private async void connectionCheckTimer_Tick(object sender, EventArgs e) {
      await CheckConnection();
    }
  }
}
