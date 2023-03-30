using System;
using System.Collections.Generic;
using System.Data;
using ScoreboardLiveApi;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace TP {
  public class CourtListener {
    private class CourtListenItem {
      public int SBCourtID { get; set; }
      public int TPCourtID { get; set; }
    }

    private ApiHelper m_sbApi;
    private Device m_sbDevice;
    private List<CourtListenItem> m_items = new List<CourtListenItem>();

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

    public CourtListener(ApiHelper api, Device sbDevice) {
      m_sbDevice = sbDevice;
      m_sbApi = api;
    }

    public static async Task<IEnumerable<TP.Court>> LoadFromTP(TPFile file) {
      var locations = await file.LoadLocations();
      return (await file.LoadCourts()).Select(raw => Court.Parse(raw, locations));
    }

    public static async Task<IEnumerable<ScoreboardLiveApi.Court>> LoadFromSB(ApiHelper apiHelper, Device device) {
      return await apiHelper.GetCourts(device);
    }

    public void Start(IEnumerable<(ScoreboardLiveApi.Court, TP.Court)> courtMappings) {
      // Save mappings
      lock (m_items) {
        m_items.Clear();
        m_items.AddRange(courtMappings.Select(mapping => new CourtListenItem() {
          SBCourtID = mapping.Item1.CourtID,
          TPCourtID = mapping.Item2.ID
        }));
      }
      m_doCancel = new CancellationTokenSource();
    }

    public void Stop() {
      if (m_doCancel != null) {
        m_doCancel.Cancel();
      }
    }

    // protected void 

    private async Task<(List<TP.Court>, List<ScoreboardLiveApi.Court>)> Load() {
      return (null, null); 
      // Open the connection
      // m_tpConnection.Open();
      // Load courts
      // var tpCourts = TpLoader.Loader.LoadCourts(m_tpConnection.CreateCommand());
      // var tpCourts = await Task.Run(() => {
      //   return Loader.LoadCourts(m_tpConnection.CreateCommand());
      // });
      // Close connection
      // m_tpConnection.Close();
      // Load sb courts
      // var sbCourts = await m_sbApi.GetCourts(m_sbDevice);
      // Return
      // return (tpCourts, sbCourts);
    }
  }
}
