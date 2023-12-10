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

namespace ScoreboardConnectWinUI3.Controls {
  public partial class TPNetworkControl : UserControl {
    private DateTime m_lastCheck = DateTime.MinValue;
    private Tournament m_tournament;
    private TPNetwork.SocketClient m_socketClient;

    public event EventHandler<TPNetworkConnectedEventArgs> Connected;
    public event EventHandler<AsyncVoidMethodBuilder> Disconnected;

    public class TPNetworkConnectedEventArgs : EventArgs {
      public Tournament Tournament { get; set; }
      public TPNetworkConnectedEventArgs(Tournament tournament) {
        Tournament = tournament;
      }
    }

    public TPNetworkControl() {
      InitializeComponent();
      m_socketClient = new TPNetwork.SocketClient();
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
      if (DateTime.Now - m_lastCheck < TimeSpan.FromSeconds(10)) {
        return;
      }
      m_lastCheck = DateTime.Now;
      try {
        var xml = await m_socketClient.GetTournamentInfo();
        m_tournament = Tournament.LoadFromVisualXML(new TP.VisualXML.TPNetwork(xml));
        SetStatusConnected();
      } catch (Exception e) {
        SetStatusNotConnected();
      }
    }

    private async void TPNetworkControl_Load(object sender, EventArgs e) {
      SetStatusNotConnected();
      await CheckConnection();
      connectionCheckTimer.Start();
    }

    private async void connectionCheckTimer_Tick(object sender, EventArgs e) {
      await CheckConnection();
    }
  }
}
