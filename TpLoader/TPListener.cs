using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.IO.Compression;
using System.IO;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace TP {
  public class TPListener {
    public class TPCourtUpdateEventArgs {
      public string CourtName { get; set; }
      public Match Match { get; set; }
    }

    private CancellationTokenSource m_doCancel;
    private List<Data.CourtData> m_activeCourts = new List<Data.CourtData>();

    public event EventHandler ServiceStarted;
    public event EventHandler ServiceStopped;
    public event EventHandler<(string, Exception)> ServiceError;
    public event EventHandler<TPCourtUpdateEventArgs> CourtUpdate;


    public bool IsListening {
      get {
        return (m_doCancel != null) && !m_doCancel.IsCancellationRequested;
      }
    }

    public bool IsCancelling {
      get {
        return (m_doCancel != null) && m_doCancel.IsCancellationRequested;
      }
    }

    public TPListener() {
    }

    public void Start() {
      if (m_doCancel != null) {
        throw new Exception("Listener already running");
      }
      m_doCancel = new CancellationTokenSource();
      m_activeCourts.Clear();
      Run();
    }

    public void Stop() {
      if (m_doCancel != null) {
        m_doCancel.Cancel();
      }
    }

    private void Run() {
      Task.Run(() => {
        TcpListener tcp = new TcpListener(IPAddress.Any, 13333);
        try {
          tcp.Start();
        } catch (Exception e) {
          OnServiceError(e, "Failed to start TCP listener");
          m_doCancel = null;
          return;
        }
        bool runSignal = true;
        ServiceStarted?.Invoke(this, EventArgs.Empty);
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

        XmlReaderSettings xmlSettings = new XmlReaderSettings() {
          // Async = true,
          IgnoreWhitespace = true
        };

        List<Data.CourtData> courtsToUpdate = null;
        Tournament tournament = null;
        using (XmlReader xmlReader = XmlReader.Create(streamXMLReaderShouldUse, xmlSettings)) {
          if (xmlReader.ReadToFollowing("ONCOURT")) {
            using (XmlReader onCourt = xmlReader.ReadSubtree()) {
              courtsToUpdate = ReadOnCourt(onCourt);
            }
          }
          if (xmlReader.ReadToFollowing("EVENTS")) {
            tournament = Tournament.LoadFromXML(xmlReader.ReadSubtree());
          }
        }

        // Send update event
        courtsToUpdate.ForEach(item => OnCourtUpdate(item.Name, tournament.FindMatchByID(item.TpMatchID)));
      }
    }

    private List<Data.CourtData> ReadOnCourt(XmlReader reader) {
      // Read from XML
      List<string> courtsWithMatchesAssigned = new List<string>();
      List<Data.CourtData> onCourts = new List<Data.CourtData>();
      while (reader.ReadToFollowing("MATCH")) {
        onCourts.Add(new Data.CourtData(reader));
      }

      List<Data.CourtData> sendUpdates = new List<Data.CourtData>();
      lock (m_activeCourts) {
        // Find courts with new matches
        foreach (Data.CourtData court in onCourts) {
          if (court.TpMatchID == 0) continue;
          Data.CourtData activeCourt = m_activeCourts.FirstOrDefault(activeCourt => activeCourt.Name == court.Name);
          if (activeCourt == null) {
            // Court was previously unassigned, but now has a match
            m_activeCourts.Add(court);
            sendUpdates.Add(court);
          } else if (activeCourt?.TpMatchID != court.TpMatchID) {
            // Court has got a new match
            activeCourt.TpMatchID = court.TpMatchID;
            sendUpdates.Add(court);
          }
          courtsWithMatchesAssigned.Add(court.Name);
        }

        // Find previously assigned but now empty courts
        sendUpdates.AddRange(
          m_activeCourts.FindAll(activeCourt => !courtsWithMatchesAssigned.Contains(activeCourt.Name)).Select(activeCourt => {
            activeCourt.TpMatchID = 0;
            return activeCourt;
          })
        );
        m_activeCourts.RemoveAll(activeCourt => !courtsWithMatchesAssigned.Contains(activeCourt.Name));
      }

      return sendUpdates;
    }

    public void SetInitialState(Tournament tournament) {
      lock (m_activeCourts) {
        m_activeCourts.Clear();
        tournament.Courts.Where(court => court.TpMatchID != 0).ToList().ForEach(court => m_activeCourts.Add(new Data.CourtData(court)));
      }
    }

    private void OnServiceError(Exception error, string message = "An error occured while listening for TP changes") {
      ServiceError?.Invoke(this, (message, error));
    }

    private void OnServiceStop() {
      m_doCancel = null;
      ServiceStopped?.Invoke(this, EventArgs.Empty);
    }

    private void OnCourtUpdate(string tpCourtName, Match tpMatch) {
      CourtUpdate?.Invoke(this, new TPCourtUpdateEventArgs() {
        CourtName = tpCourtName,
        Match = tpMatch
      });
    }
  }
}
