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
using TP.VisualXML;

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
      buttonAction.Hide();
      buttonCancel.Show();
    }

    private void SetStatusLoaded() {
      labelUploadStatus.Text = "Select items to upload";
      listClasses.Show();
      listClasses.Enabled = true;
      buttonAction.Show();
      buttonAction.Text = "Upload";
      progressBar.Value = 0;
      buttonCancel.Show();
    }

    private void SetStatusLoading() {
      labelUploadStatus.Text = "Uploading to server...";
      progressBar.Value = 0;
      listClasses.Show();
      listClasses.Enabled = false;
      buttonAction.Text = "Abort";
      buttonCancel.Hide();
    }

    private void SetStatusComplete() {
      labelUploadStatus.Text = "Upload complete";
      listClasses.Show();
      listClasses.Enabled = false;
      buttonAction.Text = "Back";
      buttonCancel.Hide();
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
        m_tpTournament.BeginUpload += M_tpTournament_BeginUpload;
        m_tpTournament.ProgressUpload += tpTournament_ProgressUpload;
        m_tpTournament.EndUpload += M_tpTournament_EndUpload;
        tpFile.Close();
        listClasses.Populate(m_tpTournament.Events);
        SetStatusLoaded();
      } catch (Exception e) {
        MessageBox.Show(string.Format("Could not load TP file:{1}{0}", e.Message, Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        SetStatusNotLoaded();
      }
    }

    private async Task LoadTPNetwork() {
      SetStatusNotLoaded();
      try {
        TPNetwork.SocketClient client = new TPNetwork.SocketClient();
        var visualXml = new TP.VisualXML.TPNetwork(await client.GetTournamentInfo());
        m_tpTournament = TP.Tournament.LoadFromVisualXML(visualXml);
        m_tpTournament.BeginUpload += M_tpTournament_BeginUpload;
        m_tpTournament.ProgressUpload += tpTournament_ProgressUpload;
        m_tpTournament.EndUpload += M_tpTournament_EndUpload;
        listClasses.Populate(m_tpTournament.Events);
        SetStatusLoaded();
      } catch (Exception e) {
        if (MessageBox.Show(string.Format("Could not connect to TP network:{1}{0}", e.Message, Environment.NewLine), "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel) {
          OperationCompleted?.Invoke(this, EventArgs.Empty);
        } else {
          await LoadTPNetwork();
        }
      }
    }

    private void tpTournament_ProgressUpload(object sender, TP.Tournament.TournamentUploadEventArgs e) {
      progressBar.Invoke(new Action(() => progressBar.Value = (int)e.Progress));
      labelUploadStatus.Invoke(new Action(() => labelUploadStatus.Text = e.Message));
    }

    private void M_tpTournament_EndUpload(object sender, TP.Tournament.TournamentUploadEventArgs e) {
    }
    private void M_tpTournament_BeginUpload(object sender, TP.Tournament.TournamentUploadEventArgs e) {
      progressBar.Value = 0;
      labelUploadStatus.Invoke(new Action(() => labelUploadStatus.Text = e.Message));
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

    private async void buttonAction_Click(object sender, EventArgs e) {
      if (buttonAction.Text == "Abort") {
        m_tpTournament.Abort();
      } else if (buttonAction.Text == "Back") {
        OperationCompleted?.Invoke(this, EventArgs.Empty);
      } else if (buttonAction.Text == "Upload") {
        await UploadTournamentClasses();
      }
    }

    private async void ControlUploadTournament_Load(object sender, EventArgs e) {
      // await LoadTPFile();
      await LoadTPNetwork();
    }

    private void buttonCancel_Click(object sender, EventArgs e) {
      OperationCompleted?.Invoke(this, EventArgs.Empty);
    }
  }
}
