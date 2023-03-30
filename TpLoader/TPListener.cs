using System;
using System.Collections.Generic;
using System.Data;
using ScoreboardLiveApi;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.IO.Compression;
using System.IO;
using System.Text;
using System.Xml;

namespace TP {
  public class TPListener {
    private CancellationTokenSource m_doCancel;
    private string m_debugFileDestinationDirectory;
    private Dictionary<string, int> m_activeCourts = new Dictionary<string, int>();
    private string m_tpFile;

    public event EventHandler ServiceStarted;
    public event EventHandler ServiceStopped;
    public event EventHandler<(string, Exception)> ServiceError;
    public event EventHandler<(string, int)> CourtUpdate;

    public bool IsListening {
      get {
        return (m_doCancel != null) && !m_doCancel.IsCancellationRequested;
      }
    }
    public TPListener(string sourceTPFile, string debugDirectory = null) {
      m_debugFileDestinationDirectory = debugDirectory;
      if ((m_debugFileDestinationDirectory != null) && !m_debugFileDestinationDirectory.EndsWith("\\")) {
        m_debugFileDestinationDirectory += "\\";
      }
      m_tpFile = sourceTPFile;
    }
    public void Start() {
      if (m_doCancel != null) {
        throw new Exception("Listener already running");
      }
      m_doCancel = new CancellationTokenSource();
      _ = SetInitialState();
      Run();
    }

    public void Stop() {
      if (m_doCancel != null) {
        m_doCancel.Cancel();
      }
    }

    private void Run() {
      Task.Run(() => {
        ServiceStarted?.Invoke(this, EventArgs.Empty);
        TcpListener tcp = new TcpListener(IPAddress.Any, 13333);
        tcp.Start();
        bool runSignal = true;
        while (runSignal) {
          try {
            tcp.AcceptTcpClientAsync().ContinueWith(r => {
              if (r.IsCompleted) {
                HandleConnection(r.Result);
              }
            }, m_doCancel.Token).Wait();
          } catch (AggregateException e) {
            e.Handle(innerException => {
              if (innerException is TaskCanceledException) {
                tcp.Stop();
                OnServiceStop();
                runSignal = false;
              } else {
                OnServiceError(innerException);
              }
              return true;
            });
          } catch (Exception e) {
            OnServiceError(e);
          }
        }
      });
    }

    private void HandleConnection(TcpClient connection) {
      var connectionStream = connection.GetStream();
      connectionStream.Read(new byte[4], 0, 4);
      using (var unzip = new GZipStream(connectionStream, CompressionMode.Decompress))
      using (MemoryStream ms = new MemoryStream()) {
        Stream streamXMLReaderShouldUse = unzip;

        if (Directory.Exists(m_debugFileDestinationDirectory)) {
          unzip.CopyTo(ms);
          string debugFile = string.Format("{1}{0}.xml", DateTime.Now.ToString().Replace(':', '-'), m_debugFileDestinationDirectory);
          File.WriteAllText(debugFile, Encoding.UTF8.GetString(ms.ToArray()));
          ms.Position = 0;
          streamXMLReaderShouldUse = ms;
        }

        XmlReaderSettings xmlSettings = new XmlReaderSettings() {
          // Async = true
        };

        using (XmlReader xmlReader = XmlReader.Create(streamXMLReaderShouldUse, xmlSettings)) {
          if (xmlReader.ReadToFollowing("ONCOURT")) {
            using (XmlReader onCourt = xmlReader.ReadSubtree()) {
              ReadOnCourt(onCourt);
            }
          }
        }
      }
    }
    private void ReadOnCourt(XmlReader reader) {
      List<string> courtsWithMatchesAssigned = new List<string>();
      while (reader.ReadToFollowing("MATCH")) {
        int.TryParse(reader.GetAttribute("ID"), out int tpMatchId);
        if (tpMatchId == 0) continue;

        string tpCourtName = reader.GetAttribute("CT");

        lock (m_activeCourts) {
          m_activeCourts.TryGetValue(tpCourtName, out int currentMatchId);
          if ((currentMatchId == 0) || (currentMatchId != tpMatchId)) {
            m_activeCourts[tpCourtName] = tpMatchId;
            OnCourtUpdate(tpCourtName, tpMatchId);
          }
        }
        courtsWithMatchesAssigned.Add(tpCourtName);
      }

      lock (m_activeCourts) {
        foreach (string nowEmptyCourt in m_activeCourts.Select(kvp => kvp.Key).Except(courtsWithMatchesAssigned)) {
          m_activeCourts.Remove(nowEmptyCourt);
          OnCourtUpdate(nowEmptyCourt, 0);
        }
      }
    }

    private async Task SetInitialState() {
      lock (m_activeCourts) { 
        m_activeCourts.Clear();
      }
      TPFile tpFile = null;
      try {
        tpFile = new TPFile(m_tpFile);
        var courts = await tpFile.LoadCourts();
        lock (m_activeCourts) {
          foreach (Data.CourtData court in courts) {
            if (court.TpMatchID > 0) {
              m_activeCourts.Add(court.Name, court.TpMatchID);
            }
            OnCourtUpdate(court.Name, court.TpMatchID);
          }
        }
      } catch (Exception e) {
        OnServiceError(e, "Could not load initial state for courts from TP file.");
      } finally {
        if (tpFile != null) {
          tpFile.Close();
        }
      }
    }

    private void OnServiceError(Exception error, string message = "An error occured while listening for TP changes") {
      ServiceError?.Invoke(this, (message, error));
    }

    private void OnServiceStop() {
      m_doCancel = null;
      ServiceStopped?.Invoke(this, EventArgs.Empty);
    }

    private void OnCourtUpdate(string tpCourtName, int tpMatchId) {
      CourtUpdate?.Invoke(this, (tpCourtName, tpMatchId));
    }
  }
}
