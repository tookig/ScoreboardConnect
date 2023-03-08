using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Threading;

namespace ScoreboardConnectWinUI3 {
  public partial class ControlUploadTournament : UserControl {
    private ScoreboardLiveApi.ApiHelper m_helper;
    private ScoreboardLiveApi.Device m_device;
    private ScoreboardLiveApi.Tournament m_tournament;
    private string m_tpFileName;
    private List<TP.TournamentClass> m_tournamentClasses = new List<TP.TournamentClass>();
    private CancellationTokenSource m_doCancel;

    public event EventHandler OperationCompleted;

    public ControlUploadTournament(ScoreboardLiveApi.ApiHelper api, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament tournament) {
      m_helper = api ?? throw new ArgumentNullException("Helper reference cannot be null");
      m_device = device ?? throw new ArgumentNullException("Device reference cannot be null");
      m_tournament = tournament ?? throw new ArgumentNullException("Tournament reference cannot be null");

      InitializeComponent();
    }

    private void SetStatusNotLoaded() {
      labelUploadStatus.Text = "";
      listClasses.Hide();
      buttonAction.Text = "Load TP-file";
    }

    private void SetStatusLoaded() {
      labelUploadStatus.Text = "Select items to upload";
      listClasses.Show();
      listClasses.Enabled = true;
      buttonAction.Text = "Upload";
      progressBar.Value = 0;
    }

    private void SetStatusLoading() {
      labelUploadStatus.Text = "Uploading to server...";
      listClasses.Show();
      listClasses.Enabled = false;
      buttonAction.Text = "Abort";
    }

    private void SetStatusComplete() {
      labelUploadStatus.Text = "Upload complete";
      listClasses.Show();
      listClasses.Enabled = false;
      buttonAction.Text = "Back";
    }

    private async Task LoadTPFile() {
      if (m_tpFileName == null) {
        if (openTPFile.ShowDialog() != DialogResult.OK) {
          return;
        }
        m_tpFileName = openTPFile.FileName;
      }
      try {
        TP.TPFile tpFile = new TP.TPFile(m_tpFileName);
        m_tournamentClasses = await tpFile.GetTournamentClasses();
        listClasses.Populate(m_tournamentClasses);
        tpFile.Close();
        SetStatusLoaded();
      } catch (Exception e) {
        MessageBox.Show(string.Format("Could not load TP file:{1}{0}", e.Message, Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        SetStatusNotLoaded();
      }
    }

    private async Task UploadTournamentClasses() {
      SetStatusLoading();
      var tournamentClassesToUpload = listClasses.GetSelectedTournamentClasses();
      int totalNumberOfMatches = tournamentClassesToUpload.Sum(tpClass => CountMatches(tpClass));
      int totalNumberOfClasses = tournamentClassesToUpload.Sum(tpClass => CountClasses(tpClass));
      int totalNumberOfLinks = tournamentClassesToUpload.Sum(tpCLass => CountLinks(tpCLass));
      progressBar.Maximum = totalNumberOfClasses + totalNumberOfMatches + totalNumberOfLinks;
      m_doCancel = new CancellationTokenSource();
      try {
        foreach (TP.TournamentClass tpClass in tournamentClassesToUpload) {
          await UploadTournamentClass(tpClass);
        }
        SetStatusComplete();
      } catch (OperationCanceledException) {
        SetStatusLoaded();
        MessageBox.Show(string.Format("Upload aborted{0}{0}Note that any data that was uploaded to the server{0}before the abort-button was clicked will have{0}to be removed manually from the webpage.", Environment.NewLine), "Upload aborted", MessageBoxButtons.OK, MessageBoxIcon.Information);
      } catch (Exception e) {
        SetStatusLoaded();
        MessageBox.Show(string.Format("An error occured while uploading:{1}{0}", e.Message, Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      m_doCancel = null;
    }

    private async Task<ScoreboardLiveApi.TournamentClass> UploadTournamentClass(TP.TournamentClass tpClass) {
      // Save classes so that links can lookup correct ids
      Dictionary<TP.TournamentClass, int> tpClassIDS = new Dictionary<TP.TournamentClass, int>();

      var serverClass = await m_helper.CreateTournamentClass(m_device, m_tournament, tpClass.ScoreboardTournamentClass);
      tpClassIDS.Add(tpClass, serverClass.ID);
      m_doCancel.Token.ThrowIfCancellationRequested();
      ++progressBar.Value;

      if (tpClass.ScoreboardTournamentClass.ClassType == "roundrobin") {
        foreach (ScoreboardLiveApi.MatchExtended match in tpClass.Matches) {
          ScoreboardLiveApi.Match m = await m_helper.CreateMatch(m_device, m_tournament, serverClass, match);
          match.MatchID = m.MatchID;
          if (ExtendedMatchNeedsScoreUpdate(match)) {
            await m_helper.SetScore(m_device, match);
          }
          m_doCancel.Token.ThrowIfCancellationRequested();
          ++progressBar.Value;
        }
      } else if (tpClass.ScoreboardTournamentClass.ClassType == "cup") {
        var serverMatches = await m_helper.FindMatchesByClass(m_device, serverClass.ID);
        tpClass.Matches.ForEach(m => m.MatchID = serverMatches.Find(sm => sm.Place == m.Place)?.MatchID ?? 0);
        foreach (ScoreboardLiveApi.MatchExtended match in tpClass.Matches) {
          if (match.MatchID > 0) {
            var serverMatch = await m_helper.UpdateMatch(m_device, match);
            if (ExtendedMatchNeedsScoreUpdate(match)) {
              await m_helper.SetScore(m_device, match);
            }
            m_doCancel.Token.ThrowIfCancellationRequested();
          }
          ++progressBar.Value;
        }
      }

      foreach (TP.TournamentClass childClass in tpClass.ChildClasses) {
        childClass.ScoreboardTournamentClass.ParentClassID = serverClass?.ID ?? 0;
        var serverChildClass = await UploadTournamentClass(childClass);
        tpClassIDS.Add(childClass, serverChildClass.ID);
      }

      foreach (TP.Link tpLink in tpClass.Links) {
        ScoreboardLiveApi.Link sbLink = new ScoreboardLiveApi.Link() {
          SourceClassID = tpClassIDS[tpLink.SourceClass],
          SourcePlace = tpLink.SourcePosition,
          TargetMatchID = tpClass.Matches.Find(m => m == tpLink.TargetMatch).MatchID,
          TargetTeam = tpLink.TeamIdentifier
        };
        await m_helper.CreateLink(m_device, m_tournament, sbLink);
        m_doCancel.Token.ThrowIfCancellationRequested();
        ++progressBar.Value;
      }

      return serverClass;
    }

    private bool ExtendedMatchNeedsScoreUpdate(ScoreboardLiveApi.MatchExtended m) {
      int score = m.Team1Set1 + m.Team1Set2 + m.Team1Set3 + m.Team1Set4 + m.Team1Set5 + m.Team2Set1 + m.Team2Set2 + m.Team2Set3 + m.Team2Set4 + m.Team2Set5;
      return (score > 0) || (!string.IsNullOrWhiteSpace(m.Status) && (m.Status != "none"));
    }

    private int CountMatches(TP.TournamentClass tpClass) {
      return tpClass.Matches.Count + tpClass.ChildClasses.Sum(childClass => CountMatches(childClass));
    }

    private int CountClasses(TP.TournamentClass tpClass) {
      return 1 + tpClass.ChildClasses.Sum(childClass => CountClasses(childClass));
    }

    private int CountLinks(TP.TournamentClass tpClass) {
      return tpClass.Links.Count + tpClass.ChildClasses.Sum(childClass => CountLinks(childClass));
    }

    private async void buttonAction_Click(object sender, EventArgs e) {
      if (m_tpFileName == null) {
        await LoadTPFile();
      } else if (buttonAction.Text == "Abort") {
        m_doCancel.Cancel();
      } else if (buttonAction.Text == "Back") {
        OperationCompleted?.Invoke(this, EventArgs.Empty);
      } else if (m_doCancel == null) {
        await UploadTournamentClasses();
      }
    }

    private async void ControlUploadTournament_Load(object sender, EventArgs e) {
      await LoadTPFile();
    }
  }
}
