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
    private ScoreboardLiveApi.Tournament m_sbTournament;
    private string m_tpFileName;
    private TP.Tournament m_tpTournament;

    public event EventHandler OperationCompleted;

    public ControlUploadTournament(ScoreboardLiveApi.ApiHelper api, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament tournament) {
      m_helper = api ?? throw new ArgumentNullException("Helper reference cannot be null");
      m_device = device ?? throw new ArgumentNullException("Device reference cannot be null");
      m_sbTournament = tournament ?? throw new ArgumentNullException("Tournament reference cannot be null");

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
        m_tpTournament = await TP.Tournament.LoadFromTP(tpFile);
        m_tpTournament.ProgressUpload += tpTournament_ProgressUpload;
        tpFile.Close();
        listClasses.Populate(m_tpTournament.Events);
        SetStatusLoaded();
      } catch (Exception e) {
        MessageBox.Show(string.Format("Could not load TP file:{1}{0}", e.Message, Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        SetStatusNotLoaded();
      }
    }

    private void tpTournament_ProgressUpload(object sender, TP.Tournament.TournamentUploadEventArgs e) {
      progressBar.Invoke(new Action(() => progressBar.Value = (int)e.Progress));
    }

    private async Task UploadTournamentClasses() {
      SetStatusLoading();
      var tournamentEventsToUpload = listClasses.GetSelectedTournamentEvents();
      progressBar.Maximum = 100;
      
      try {
        await m_tpTournament.Upload(tournamentEventsToUpload, m_helper, m_device, m_sbTournament);
        m_tpTournament = null;
        SetStatusComplete();
      } catch (OperationCanceledException) {
        SetStatusLoaded();
        MessageBox.Show(string.Format("Upload aborted{0}{0}Note that any data that was uploaded to the server{0}before the abort-button was clicked will have{0}to be removed manually from the webpage.", Environment.NewLine), "Upload aborted", MessageBoxButtons.OK, MessageBoxIcon.Information);
      } catch (Exception e) {
        SetStatusLoaded();
        MessageBox.Show(string.Format("An error occured while uploading:{1}{0}", e.Message, Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    /*
    private async Task<ScoreboardLiveApi.TournamentClass> UploadTournamentClass(TP.TournamentClass tpClass) {
      // Save classes so that links can lookup correct ids
      Dictionary<TP.TournamentClass, int> tpClassIDS = new Dictionary<TP.TournamentClass, int>();

      var serverClass = await m_helper.CreateTournamentClass(m_device, m_sbTournament, tpClass.ScoreboardTournamentClass);
      tpClassIDS.Add(tpClass, serverClass.ID);
      m_doCancel.Token.ThrowIfCancellationRequested();
      ++progressBar.Value;

      if (tpClass.ScoreboardTournamentClass.ClassType == "roundrobin") {
        foreach (ScoreboardLiveApi.MatchExtended match in tpClass.Matches) {
          ScoreboardLiveApi.Match m = await m_helper.CreateMatch(m_device, m_sbTournament, serverClass, match);
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
        await m_helper.CreateLink(m_device, m_sbTournament, sbLink);
        m_doCancel.Token.ThrowIfCancellationRequested();
        ++progressBar.Value;
      }

      return serverClass;
    }
    */

    private async void buttonAction_Click(object sender, EventArgs e) {
      if (m_tpFileName == null) {
        await LoadTPFile();
      } else if (buttonAction.Text == "Abort") {
        m_tpTournament.Abort();
      } else if (buttonAction.Text == "Back") {
        OperationCompleted?.Invoke(this, EventArgs.Empty);
      } else if (buttonAction.Text == "Upload") {
        await UploadTournamentClasses();
      }
    }

    private async void ControlUploadTournament_Load(object sender, EventArgs e) {
      await LoadTPFile();
    }
  }
}
