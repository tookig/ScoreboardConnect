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
        var cupServerMatches = new Dictionary<ScoreboardLiveApi.TournamentClass, List<ScoreboardLiveApi.Match>>();
        // Upload all classes, and save any cup matches created in the process
        cupServerMatches.Add(eci.MainDraw, await UploadClass(eci.MainDraw, apiHelper, device, tournament));
        foreach (ScoreboardLiveApi.TournamentClass qualifier in eci.Qualifiers) {
          qualifier.ParentClassID = eci.MainDraw.ID;
          cupServerMatches.Add(qualifier, await UploadClass(qualifier, apiHelper, device, tournament));
          CheckIfToCancel();
        }
        // Update match IDs with those downloaded from the created cup classes. 
        foreach (ExportMatchItem emi in eci.Matches.Where(match => match.BelongsTo.ClassType != "roundrobin")) {
          emi.MatchData.MatchID = cupServerMatches[emi.BelongsTo].FirstOrDefault(sm => sm.Place == emi.MatchData.Place)?.MatchID ?? 0;
          CheckIfToCancel();
        }
        // Create all matches
        foreach (ExportMatchItem emi in eci.Matches) {
          UploadMatch(emi, apiHelper, device, tournament);
          CheckIfToCancel();
        }
      }
      m_doCancel = null;
      OnEndUpload();
    }

    private async void UploadMatch(ExportMatchItem emi, ScoreboardLiveApi.ApiHelper apiHelper, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament tournament) {
      if (emi.BelongsTo.ClassType == "roundrobin") {
        var createdMatch = await apiHelper.CreateMatch(device, null, emi.BelongsTo, emi.MatchData);
        emi.MatchData.MatchID = createdMatch.MatchID;
      } else {
        await apiHelper.UpdateMatch(device, emi.MatchData);
      }
      if (ExtendedMatchNeedsScoreUpdate(emi.MatchData)) {
        await apiHelper.SetScore(device, emi.MatchData);
      }
      OnProgressUpload(++progress, totalNumberOfUploads, emi.BelongsTo.Description);
    }

    private async Task<List<ScoreboardLiveApi.Match>> UploadClass(ScoreboardLiveApi.TournamentClass sbClass, ScoreboardLiveApi.ApiHelper apiHelper, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament tournament) {
      var createdClass = await apiHelper.CreateTournamentClass(device, tournament, sbClass);
      sbClass.ID = createdClass.ID;
      var serverMatches = new List<ScoreboardLiveApi.Match>();
      if (sbClass.ClassType != "roundrobin") {
        serverMatches = await apiHelper.FindMatchesByClass(device, createdClass.ID);
      }
      OnProgressUpload(++progress, totalNumberOfUploads, sbClass.Description);
      return serverMatches;
    }

    private int CountOperations(IEnumerable<ExportClassItem> ecis) {
      int count = 0;
      foreach (ExportClassItem eci in ecis) {
        count += 1 + eci.Qualifiers.Count();
        count += eci.Matches.Count();
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
      Task.Factory.StartNew(() => ProgressUpload?.Invoke(this, new TournamentUploadEventArgs((double)requestsComplete / (double)totalNumberOfRequests, message)));
    }
    private void OnEndUpload() {
      Task.Factory.StartNew(() => EndUpload?.Invoke(this, new TournamentUploadEventArgs(100.0, "")));
    }
  }
}
