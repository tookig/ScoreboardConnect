using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace TP {
  public class TournamentUploader {
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

    public Tournament Tournament { get; init; }

    private CancellationTokenSource m_doCancel;
    private int progress;
    private int totalNumberOfUploads;

    public TournamentUploader(Tournament tournament) {
      Tournament = tournament;
    }

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


    public async Task Upload(IEnumerable<Event> eventsToUpload, ScoreboardLiveApi.ApiHelper apiHelper, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament sbTournament, TournamentConverter.ConvertOptions convertOptions) {
      if (m_doCancel != null) {
        throw new Exception("Upload in progress");
      }

      m_doCancel = new CancellationTokenSource();

      OnBeginUpload("Converting tournament...");

      TournamentConverter converter = new TournamentConverter(convertOptions);
      var convertedData = await Task.Run(() => converter.ExportClasses(Tournament, ev => eventsToUpload.Any(ev2 => ev.ID == ev2.ID)));
      if (convertedData.Count() < 1) {
        throw new Exception("Nothing to upload");
      }

      totalNumberOfUploads = CountOperations(convertedData);
      progress = 0;

      OnProgressUpload(progress, totalNumberOfUploads, "Uploading data...");

      foreach (TournamentConverter.ExportClassItem eci in convertedData) {
        await UploadClass(eci, apiHelper, device, sbTournament);
      }

      var links = converter.CreateLinks(convertedData);
      foreach (ScoreboardLiveApi.Link link in links) {
        await UploadLink(link, apiHelper, device, sbTournament);
      }
      
      m_doCancel = null;
      OnEndUpload();
    }

    private async Task UploadLink(ScoreboardLiveApi.Link link, ScoreboardLiveApi.ApiHelper apiHelper, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament tournament) {
      await apiHelper.CreateLink(device, tournament, link);
      CheckIfToCancel();
    }

    private async Task UploadClass(TournamentConverter.ExportClassItem eci, ScoreboardLiveApi.ApiHelper apiHelper, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament tournament) {
      if (eci.SBClass.ClassType == "roundrobin") {
        await UploadRoundRobin(eci, apiHelper, device, tournament);
      } else {
        await UploadCup(eci, apiHelper, device, tournament);
      }
      if (eci.SubClasses?.Count() > 0) {
        foreach(TournamentConverter.ExportClassItem subEci in eci.SubClasses) {
          subEci.SBClass.ParentClassID = eci.SBClass.ID;
          await UploadClass(subEci, apiHelper, device, tournament);
        }
      }
    }

    private async Task UploadCup(TournamentConverter.ExportClassItem eci, ScoreboardLiveApi.ApiHelper apiHelper, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament tournament) {
      // Create cup class
      var createdClass = await apiHelper.CreateTournamentClass(device, tournament, eci.SBClass);
      eci.SBClass.ID = createdClass.ID;
      // Download cup matches
      List<ScoreboardLiveApi.Match> cupMatches = await apiHelper.FindMatchesByClass(device, createdClass.ID);
      OnProgressUpload(++progress, totalNumberOfUploads, eci.SBClass.Description);
      CheckIfToCancel();

      // Update match IDs and upload matches 
      foreach (TournamentConverter.ExportMatchItem emi in eci.Matches) {
        emi.SB.MatchID = cupMatches.FirstOrDefault(sm => sm.Place == emi.SB.Place)?.MatchID ?? 0;
        emi.SB.Tag = TournamentConverter.CreateMatchTag(tournament, emi.TP, eci.TPEvent, eci.TPDraw);
        await apiHelper.UpdateMatch(device, emi.SB);
        if (ExtendedMatchNeedsScoreUpdate(emi.SB)) {
          await apiHelper.SetScore(device, emi.SB);
        }
        OnProgressUpload(++progress, totalNumberOfUploads, eci.SBClass.Description);
        CheckIfToCancel();
      }
    }

    private async Task UploadRoundRobin(TournamentConverter.ExportClassItem eci, ScoreboardLiveApi.ApiHelper apiHelper, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament tournament) {
      // Create roundrobin class
      var createdClass = await apiHelper.CreateTournamentClass(device, tournament, eci.SBClass);
      eci.SBClass.ID = createdClass.ID;
      OnProgressUpload(++progress, totalNumberOfUploads, eci.SBClass.Description);
      CheckIfToCancel();
      // Upload matches
      foreach (TournamentConverter.ExportMatchItem emi in eci.Matches) {
        emi.SB.Tag = TournamentConverter.CreateMatchTag(tournament, emi.TP, eci.TPEvent, eci.TPDraw);
        emi.SB.MatchID = (await apiHelper.CreateMatch(device, tournament, eci.SBClass, emi.SB)).MatchID;
        if (ExtendedMatchNeedsScoreUpdate(emi.SB)) {
          await apiHelper.SetScore(device, emi.SB);
        }
        OnProgressUpload(++progress, totalNumberOfUploads, eci.SBClass.Description);
        CheckIfToCancel();
      }
    }

      private int CountOperations(IEnumerable<TournamentConverter.ExportClassItem> ecis, IEnumerable<ScoreboardLiveApi.Link> links = null) {
      int count = 0;
      foreach (TournamentConverter.ExportClassItem eci in ecis) {
        count += 1 + eci.Matches.Count();
        if (eci.SubClasses?.Count() > 0) {
          count += CountOperations(eci.SubClasses);
        }
      }
      return count + (links?.Count() ?? 0);
    }

    private bool ExtendedMatchNeedsScoreUpdate(ScoreboardLiveApi.MatchExtended m) {
      int score = m.Team1Set1 + m.Team1Set2 + m.Team1Set3 + m.Team1Set4 + m.Team1Set5 + m.Team2Set1 + m.Team2Set2 + m.Team2Set3 + m.Team2Set4 + m.Team2Set5;
      return (score > 0) || (!string.IsNullOrWhiteSpace(m.Status) && (m.Status != "none"));
    }

    private void OnBeginUpload(string message = "") {
      Task.Factory.StartNew(() => BeginUpload?.Invoke(this, new TournamentUploadEventArgs(0.0, message)));
    }
    private void OnProgressUpload(int requestsComplete, int totalNumberOfRequests, string message) {
      Task.Factory.StartNew(() => ProgressUpload?.Invoke(this, new TournamentUploadEventArgs((double)requestsComplete * 100.0 / (double)totalNumberOfRequests, message)));
    }
    private void OnEndUpload() {
      Task.Factory.StartNew(() => EndUpload?.Invoke(this, new TournamentUploadEventArgs(100.0, "")));
    }
  }
}
