using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP;

namespace ScoreboardConnectWinUI3.Controls {
  public partial class TournamentTVControl : UserControl {
    private const int RETRY_INTERVAL = 10;

    private TP.TPListener m_listener = new TP.TPListener();
    private DateTime? m_lastUpdate = null;
    private DateTime? m_lastConnectionTry = null;
    private string m_lastError = null;

    public TPListener Listener => m_listener;

    public TournamentTVControl() {
      InitializeComponent();
      m_listener.CourtUpdate += M_listener_CourtUpdate;
      m_listener.ServiceStarted += M_listener_ServiceStarted;
      m_listener.ServiceStopped += M_listener_ServiceStopped;
      m_listener.ServiceError += M_listener_ServiceError;
    }

    public void SetInitialState(Tournament tournament) {
      m_listener.SetInitialState(tournament);
    }

    private void SetStatusNotConnected() {
      labelStatus.Text = "Not connected";
      labelStatus.ForeColor = Color.Red;
      m_lastUpdate = null;
      m_lastError = null;
      labelUpdates.Hide();
      labelErrors.Hide();
    }

    private void SetStatusListening() {
      labelStatus.Text = "Listening";
      labelStatus.ForeColor = Color.Green;
      labelUpdates.Text = "No updates received";
      labelUpdates.Show();
      m_lastError = null;
      labelErrors.Hide();
    }

    private void M_listener_ServiceError(object sender, (string, Exception) e) {
      ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Error, e.Item1, e.Item2);
      Invoke((MethodInvoker)delegate {
        m_lastUpdate = null;
        m_lastError = "Error: " + e.Item1 + ";" + e.Item2.Message;
      });
    }

    private void M_listener_ServiceStopped(object sender, EventArgs e) {
      ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Info, "Tournament TV service stopped", null);
      Invoke((MethodInvoker)delegate {
        SetStatusNotConnected();
      });
      // Try to restart
      StartListening();
    }

    private void M_listener_ServiceStarted(object sender, EventArgs e) {
      ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Info, "Tournament TV service started", null);
      Invoke((MethodInvoker)delegate {
        SetStatusListening();
      });
    }

    private void M_listener_CourtUpdate(object sender, TPListener.TPCourtUpdateEventArgs e) {
      ConnectLogger.Singleton.Log(ConnectLogger.LogLevels.Info, $"Court update received for TP court {e.CourtName}", null);
      Invoke((MethodInvoker)delegate {
          m_lastUpdate = DateTime.Now;
          m_lastError = null;
        });
    }

    private void TournamentTVControl_Load(object sender, EventArgs e) {
      if (!DesignMode) {
        StartListening();
      }
    }

    private void StartListening() {
      if (!m_listener.IsListening && !m_listener.IsCancelling) {
        m_lastConnectionTry = DateTime.Now;
        m_listener.Start();
      }
    }

    private string FormatTimeSpan(TimeSpan span) {
      StringBuilder sb = new StringBuilder();
      if (span.Days >= 1) {
        sb.AppendFormat("{0} days ", span.Days);
      }
      if (span.Hours >= 1) {
        sb.AppendFormat("{0} hours ", span.Hours);
      }
      if (span.Minutes >= 1) {
        sb.AppendFormat("{0} minutes ", span.Minutes);
      }
      sb.AppendFormat("{0} seconds", span.Seconds);
      
      return sb.ToString();
    }

    private void textTimer_Tick(object sender, EventArgs e) {
      if (m_listener.IsListening) {
        if (m_lastUpdate.HasValue) {
          var span = DateTime.Now - m_lastUpdate.Value;
          labelUpdates.Text = string.Format("Last update received {0} ago", FormatTimeSpan(span));
        } else {
          labelUpdates.Text = "No updates received";
        }
      } else {
        if (m_lastConnectionTry.HasValue) {
          var span = new TimeSpan(0, 0, RETRY_INTERVAL - (DateTime.Now - m_lastConnectionTry.Value).Seconds);
          labelUpdates.Text = string.Format("Retrying in {0}", FormatTimeSpan(span));
          if (span.TotalSeconds <= 1) {
            StartListening();
          }
        } else {
          labelUpdates.Text = "";
        }
      }

      if (m_lastError != null) {
        labelErrors.Text = m_lastError;
        labelErrors.Show();
      } else {
        labelErrors.Hide();
      }
    }
  }
}
