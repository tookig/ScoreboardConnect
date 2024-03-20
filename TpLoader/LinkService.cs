using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TP {
  public class LinkService {
    private SBUploader m_sbUploader;
    private TPListener m_tpListener;

    public event EventHandler ServiceStarted;
    public event EventHandler ServiceStopped;
    public event EventHandler<(string, Exception)> ServiceError;
    public event EventHandler<string> ServiceWarning;
    public event EventHandler<(ScoreboardLiveApi.Court, ScoreboardLiveApi.Match)> CourtUpdate;

    public LinkService(string sourceTPFile,
                       ScoreboardLiveApi.ApiHelper helper, 
                       ScoreboardLiveApi.Device device, 
                       ScoreboardLiveApi.Tournament tournament, 
                       SBUploader.ICourtMapper mapper) {
      m_sbUploader = new SBUploader(helper, device, tournament, mapper);
      m_tpListener = new TPListener();
      InitEvents();
    }

    public void Start() {
      m_tpListener.Start();
    }

    public void Stop() {
      m_tpListener.Stop();
    }

    public bool IsStarted {
      get {
        return m_tpListener.IsListening;
      }
    }

    private void InitEvents() {
      m_tpListener.ServiceStarted += (sender, args) => OnServiceStarted();
      m_tpListener.ServiceStopped += (sender, args) => OnServiceStopped();
      m_tpListener.ServiceError += (sender, args) => OnServiceError(args.Item1, args.Item2);
      m_tpListener.CourtUpdate += tpListener_CourtUpdate;
    }

    private void tpListener_CourtUpdate(object sender, TPListener.TPCourtUpdateEventArgs e) {
      Task.Run(async () => {
        /*
        ScoreboardLiveApi.Match newSbMatch = null;
        ScoreboardLiveApi.Court selectedCourt = null;
        try {
          if (e.Match != null) {
            (selectedCourt, newSbMatch) = await m_sbUploader.AssignMatchToCourt(e.Match, e.Draw, e.Event, e.CourtName);
          } else {
            selectedCourt = await m_sbUploader.ClearCourt(e.CourtName);
          }
        } catch (Exception error) {
          OnServiceError(string.Format("Could not update court \"{0}\"", e.CourtName), error);
        }
        if (selectedCourt != null) {
          OnCourtUpdate(selectedCourt, newSbMatch);
        } else {
          OnServiceWarning(string.Format("No ScoreboardLive court could be mapped for TP court named {1};{0}Court could not be updated.", Environment.NewLine,  e.CourtName));
        }
        */
      });
    }

    protected void OnServiceStarted() {
      ServiceStarted?.Invoke(this, new EventArgs());
    }

    protected void OnServiceStopped() {
      ServiceStopped?.Invoke(this, new EventArgs());
    }

    protected void OnServiceError(string message, Exception e) {
      ServiceError.Invoke(this, (message, e));
    }

    protected void OnServiceWarning(string message) {
      ServiceWarning.Invoke(this, message);
    }

    protected void OnCourtUpdate(ScoreboardLiveApi.Court sbCourt, ScoreboardLiveApi.Match sbMatch) {
      CourtUpdate.Invoke(this, (sbCourt, sbMatch));
    }
  }
}
