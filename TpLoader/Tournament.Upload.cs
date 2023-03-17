using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace TP {
  public partial class Tournament {
    public class TournamentUploadEventArgs {
      public double Progress { get; }
      public string Message { get; }

      public TournamentUploadEventArgs(double progress, string message) {
        Progress = progress;
        Message = message;
      }
    }
    public event EventHandler<TournamentUploadEventArgs> BeginUpload;
    public event EventHandler<TournamentUploadEventArgs> ProgressUpload;
    public event EventHandler<TournamentUploadEventArgs> EndUpload;

    private CancellationTokenSource m_doCancel;
    private int progress;
    private int totalNumberOfUploads;

    public void Abort() {
      if (m_doCancel == null) {
        throw new Exception("Upload not started");
      }
      m_doCancel.Cancel();
    }

    private void CheckIfToCancel() {
      if (m_doCancel == null) {
        return;
      }
      m_doCancel.Token.ThrowIfCancellationRequested();
    }


    public async Task Upload(IEnumerable<Event> eventsToUpload, ScoreboardLiveApi.ApiHelper apiHelper, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament tournament) {
      if (m_doCancel != null) {
        throw new Exception("Upload in progress");
      }
      
      var convertedData = ExportClasses(ev => eventsToUpload.Any(ev2 => ev.ID == ev2.ID));
      if (convertedData.Count() < 1) {
        throw new Exception("Nothing to upload");
      }

      totalNumberOfUploads = CountOperations(convertedData);
      progress = 0;

      OnBeginUpload();

      foreach (ExportClassItem eci in convertedData) {
        await UploadClass(eci, apiHelper, device, tournament);
      }
      m_doCancel = null;
      OnEndUpload();
    }

    private async Task UploadClass(ExportClassItem eci, ScoreboardLiveApi.ApiHelper apiHelper, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament tournament) {
      if (eci.MainClass.ClassType == "roundrobin") {
        await UploadRoundRobin(eci, apiHelper, device, tournament);
      } else {
        await UploadCup(eci, apiHelper, device, tournament);
      }
      if (eci.SubClasses?.Count() > 0) {
        foreach(ExportClassItem subEci in eci.SubClasses) {
          subEci.MainClass.ParentClassID = eci.MainClass.ID;
          await UploadClass(subEci, apiHelper, device, tournament);
        }
      }
    }

    private async Task UploadCup(ExportClassItem eci, ScoreboardLiveApi.ApiHelper apiHelper, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament tournament) {
      // Create cup class
      var createdClass = await apiHelper.CreateTournamentClass(device, tournament, eci.MainClass);
      eci.MainClass.ID = createdClass.ID;
      // Download cup matches
      List<ScoreboardLiveApi.Match> cupMatches = await apiHelper.FindMatchesByClass(device, createdClass.ID);
      OnProgressUpload(++progress, totalNumberOfUploads, eci.MainClass.Description);
      CheckIfToCancel();

      // Update match IDs and upload matches 
      foreach (ScoreboardLiveApi.MatchExtended match in eci.Matches) {
        match.MatchID = cupMatches.FirstOrDefault(sm => sm.Place == match.Place)?.MatchID ?? 0;
        await apiHelper.UpdateMatch(device, match);
        if (ExtendedMatchNeedsScoreUpdate(match)) {
          await apiHelper.SetScore(device, match);
        }
        OnProgressUpload(++progress, totalNumberOfUploads, eci.MainClass.Description);
        CheckIfToCancel();
      }
    }

    private async Task UploadRoundRobin(ExportClassItem eci, ScoreboardLiveApi.ApiHelper apiHelper, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament tournament) {
      // Create roundrobin class
      var createdClass = await apiHelper.CreateTournamentClass(device, tournament, eci.MainClass);
      eci.MainClass.ID = createdClass.ID;
      OnProgressUpload(++progress, totalNumberOfUploads, eci.MainClass.Description);
      CheckIfToCancel();
      // Upload matches
      foreach (ScoreboardLiveApi.MatchExtended match in eci.Matches) {
        await apiHelper.CreateMatch(device, tournament, eci.MainClass, match);
        if (ExtendedMatchNeedsScoreUpdate(match)) {
          await apiHelper.SetScore(device, match);
        }
        OnProgressUpload(++progress, totalNumberOfUploads, eci.MainClass.Description);
        CheckIfToCancel();
      }
    }

      private int CountOperations(IEnumerable<ExportClassItem> ecis) {
      int count = 0;
      foreach (ExportClassItem eci in ecis) {
        count += 1 + eci.Matches.Count();
        if (eci.SubClasses?.Count() > 0) {
          count += CountOperations(eci.SubClasses);
        }
      }
      return count;
    }

    private bool ExtendedMatchNeedsScoreUpdate(ScoreboardLiveApi.MatchExtended m) {
      int score = m.Team1Set1 + m.Team1Set2 + m.Team1Set3 + m.Team1Set4 + m.Team1Set5 + m.Team2Set1 + m.Team2Set2 + m.Team2Set3 + m.Team2Set4 + m.Team2Set5;
      return (score > 0) || (!string.IsNullOrWhiteSpace(m.Status) && (m.Status != "none"));
    }

    private void OnBeginUpload() {
      Task.Factory.StartNew(() => BeginUpload?.Invoke(this, new TournamentUploadEventArgs(0.0, "")));
    }
    private void OnProgressUpload(int requestsComplete, int totalNumberOfRequests, string message) {
      Task.Factory.StartNew(() => ProgressUpload?.Invoke(this, new TournamentUploadEventArgs((double)requestsComplete * 100.0 / (double)totalNumberOfRequests, message)));
    }
    private void OnEndUpload() {
      Task.Factory.StartNew(() => EndUpload?.Invoke(this, new TournamentUploadEventArgs(100.0, "")));
    }
  }
}
