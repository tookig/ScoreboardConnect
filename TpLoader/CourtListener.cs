using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Data;
using ScoreboardLiveApi;
using TP;
using System.Threading.Tasks;
using System.Threading;

namespace TP {
  public class CourtListener {
    private IDbConnection m_tpConnection;
    private ApiHelper m_sbApi;
    private Device m_sbDevice;

    private CancellationTokenSource m_doCancel;

    public event EventHandler ServiceStarted;
    public event EventHandler ServiceStopped;
    public event EventHandler<(string, Exception)> ServiceError;
    public event EventHandler<(int, int)> CourtUpdate;

    public bool IsListening {
      get {
        return (m_doCancel != null) && !m_doCancel.IsCancellationRequested; 
      }
    }

    public CourtListener(IDbConnection tpConnection, ApiHelper api, Device sbDevice) {
      m_tpConnection = tpConnection;
      m_sbDevice = sbDevice;
      m_sbApi = api;
    }

    public Task<(List<TP.Court>, List<Exception>)> GetTpCourts() {
      return TP.Converter.ExtractCourts(m_tpConnection);
    }

    public Task<List<ScoreboardLiveApi.Court>> GetSbCourts() {
      return m_sbApi.GetCourts(m_sbDevice);
    }

    public void Start(List<KeyValuePair<ScoreboardLiveApi.Court, TP.Court>> courtPairs) {
      m_doCancel = new CancellationTokenSource();
      Task.Run(async () => {
        while (true) {
          if (m_doCancel.IsCancellationRequested) break;
          var tpCourts = await GetTpCourts();
          
          try {
            await Task.Delay(5000, m_doCancel.Token);
          } catch (TaskCanceledException) {
            break;
          }
        }
      });
    }

    public void Stop() {
      if (m_doCancel != null) {
        m_doCancel.Cancel();
      }
    }

    private async Task<(List<TP.Court>, List<ScoreboardLiveApi.Court>)> Load() {
      // Open the connection
      // m_tpConnection.Open();
      // Load courts
      // var tpCourts = TpLoader.Loader.LoadCourts(m_tpConnection.CreateCommand());
      var tpCourts = await Task.Run(() => {
        return Loader.LoadCourts(m_tpConnection.CreateCommand());
      });
      // Close connection
      // m_tpConnection.Close();
      // Load sb courts
      var sbCourts = await m_sbApi.GetCourts(m_sbDevice);
      // Return
      return (tpCourts, sbCourts);
    }
  }
}
