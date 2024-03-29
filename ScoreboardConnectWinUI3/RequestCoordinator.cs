using ScoreboardLiveApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TP;
using TP.VisualXML;
using TPNetwork;

namespace ScoreboardConnectWinUI3 {
  internal class RequestCoordinator {
    public enum StatusMessageLevel {
      Info,
      Warning,
      Error
    }

    public class StatusMessageEventArgs : EventArgs {
      public string Message { get; set; }
      public StatusMessageLevel Level { get; set; }
    }

    private Controls.ScoreboardLiveControl.ScoreboardLiveConnectedEventArgs m_apiInfo;
    private SocketClient m_tpNetwork;
    private TPListener m_tpListener;
    private ICourtCorrelator m_courtCorrelator;

    public event EventHandler<StatusMessageEventArgs> Status;

    public Controls.ScoreboardLiveControl.ScoreboardLiveConnectedEventArgs ApiInfo {
      get {
        return m_apiInfo;
      }
      set {
        InitApi(value);
      }
    }

    public SocketClient TPNetwork {
      get {
        return m_tpNetwork;
      }
      set {
        InitSocket(value);
      }
    }

     public TPListener TPListener {
      get {
        return m_tpListener;
      }
      set {
        InitListener(value);
      }
    }

    public RequestCoordinator(SocketClient tpNetwork, TPListener tpListener, ICourtCorrelator courtCorrelator = null) {
      InitSocket(tpNetwork);
      InitListener(tpListener);
      if (courtCorrelator != null) {
        InitCourtCorrelator(courtCorrelator);
      }
    }

    protected void InitSocket(SocketClient socketClient) {
      SocketClient validatedClient = socketClient ?? throw new ArgumentNullException("SocketClient reference cannot be null");
      if (m_tpNetwork != null) {
      }
      m_tpNetwork = validatedClient;
      _ = ReloadTPCourts();
    }

    protected void InitApi(Controls.ScoreboardLiveControl.ScoreboardLiveConnectedEventArgs apiInfo) {
      Controls.ScoreboardLiveControl.ScoreboardLiveConnectedEventArgs validatedApi = apiInfo ?? throw new ArgumentNullException("ApiHelper reference cannot be null");
      if (m_apiInfo != null) {
      }
      m_apiInfo = validatedApi;
      _ = ReloadSBCourts();
    }

    protected void InitListener(TPListener tpListener) {
      TPListener validatedListener = tpListener ?? throw new ArgumentNullException("TPListener reference cannot be null");
      if (m_tpListener != null) {
        m_tpListener.CourtUpdate -= courtUpdate;
      }
      m_tpListener = validatedListener;
      m_tpListener.CourtUpdate += courtUpdate;
    }

    protected void InitCourtCorrelator(ICourtCorrelator courtCorrelator) {
      ICourtCorrelator validatedCorrelator = courtCorrelator ?? throw new ArgumentNullException("ICourtCorrelator reference cannot be null");
      if (m_courtCorrelator != null) {
      }
      m_courtCorrelator = validatedCorrelator;
      _ = ReloadSBCourts();
      _ = ReloadTPCourts();
    }

    private void courtUpdate(object sender, TPListener.TPCourtUpdateEventArgs e) {
      if (m_courtCorrelator == null) {
        return;
      }
      if (m_apiInfo == null) {
        SendStatusMessage("Court update received, but no Scoreboard API connection", StatusMessageLevel.Warning);
        return;
      }

      m_courtCorrelator.Correlate(e.CourtName);
      
      if (e.Match == null) {
        SendStatusMessage($"Court {e.CourtName} is now free", StatusMessageLevel.Info);
      } else {
        SendStatusMessage($"Court {e.CourtName} is now occupied by {e.Match}", StatusMessageLevel.Info);
      }
    }

    private void SendStatusMessage(string message, StatusMessageLevel level) {
      Status?.Invoke(this, new StatusMessageEventArgs() { Message = message, Level = level });
    }

    private async Task ReloadSBCourts() {
      if (m_apiInfo == null || m_courtCorrelator == null) {
        return;
      }
      await m_apiInfo.Api.GetCourts(m_apiInfo.Device).ContinueWith((courts) => {
        m_courtCorrelator.SetSBCourts(courts.Result);
      });
    }

    private async Task ReloadTPCourts() {
      if (m_tpNetwork == null || m_courtCorrelator == null) {
        return;
      }
      await m_tpNetwork.GetTournamentInfo().ContinueWith(async (tournamentXML) => {
        var tmpNetworkData = new TP.VisualXML.TPNetwork(tournamentXML.Result);
        var tournament = await TP.Tournament.LoadFromVisualXMLAsync(tmpNetworkData);
        m_courtCorrelator.SetTPCourts(tournament.Courts);
      });
    }
  }
}
