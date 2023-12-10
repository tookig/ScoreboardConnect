using ScoreboardLiveApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreboardConnectWinUI3.Forms {
  public partial class FormUpload : Form {
    ApiHelper m_api;
    Device m_device;
    Tournament m_sbTournament;
    TP.Tournament m_tpTournament;

    bool m_uploading = false;

    public FormUpload(ApiHelper api, Device device, Tournament sbTournament, TP.Tournament tpTournament) {
      InitializeComponent();
      m_api = api;
      m_device = device;
      m_sbTournament = sbTournament;
      m_tpTournament = tpTournament;

      m_tpTournament.ProgressUpload += M_tpTournament_ProgressUpload;
      m_tpTournament.BeginUpload += M_tpTournament_BeginUpload;
      m_tpTournament.EndUpload += M_tpTournament_EndUpload;
    }

    private void FillList() {
      if (m_tpTournament != null) {
        tournamentClassView1.Populate(m_tpTournament.Events);
      }
    }

    private void SetStatusLoading() {
      labelStatus.Text = "Uploading to server...";
      progressBar.Value = 0;
      tournamentClassView1.Enabled = false;
      buttonUpload.Enabled = false;
      labelStatus.Visible = true;
      progressBar.Visible = true;
    }

    private void SetStatusComplete() {
      labelStatus.Text = "Upload complete";
      progressBar.Value = 100;
      tournamentClassView1.Enabled = true;
      buttonUpload.Enabled = true;
    }

    private void SetStatusFailed() {
      labelStatus.Text = "Upload failed";
      progressBar.Value = 0;
      tournamentClassView1.Enabled = true;
      buttonUpload.Enabled = true;
    }

    private async Task UploadTournamentClasses() {
      SetStatusLoading();
      var tournamentEventsToUpload = tournamentClassView1.GetSelectedTournamentEvents();
      progressBar.Maximum = 100;

      try {
        m_uploading = true;
        await m_tpTournament.Upload(tournamentEventsToUpload, m_api, m_device, m_sbTournament);
        m_uploading = false;
        SetStatusComplete();
        MessageBox.Show("Upload complete", "Upload complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        Close();
      } catch (OperationCanceledException) {
        SetStatusFailed();
        MessageBox.Show(string.Format("Upload aborted{0}{0}Note that any data that was uploaded to the server{0}before the abort-button was clicked will have{0}to be removed manually from the webpage.", Environment.NewLine), "Upload aborted", MessageBoxButtons.OK, MessageBoxIcon.Information);
      } catch (Exception e) {
        SetStatusFailed();
        MessageBox.Show(string.Format("An error occured while uploading:{1}{0}", e.Message, Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void buttonCancel_Click(object sender, EventArgs e) {
      if (m_uploading) {
        m_uploading = false;
        m_tpTournament.Abort();
      }
      Close();
    }

    private void FormUpload_Load(object sender, EventArgs e) {
      FillList();
    }

    private void tournamentClassView1_ItemChecked(object sender, ItemCheckedEventArgs e) {
      buttonUpload.Enabled = tournamentClassView1.CheckedItems.Count > 0;
    }

    private async void buttonUpload_Click(object sender, EventArgs e) {
      await UploadTournamentClasses();
    }

    private void M_tpTournament_EndUpload(object sender, TP.Tournament.TournamentUploadEventArgs e) {
    }

    private void M_tpTournament_BeginUpload(object sender, TP.Tournament.TournamentUploadEventArgs e) {
      progressBar.Value = 0;
      labelStatus.Invoke(new Action(() => labelStatus.Text = e.Message));
    }

    private void M_tpTournament_ProgressUpload(object sender, TP.Tournament.TournamentUploadEventArgs e) {
      if (m_uploading) {
        progressBar.Invoke(new Action(() => progressBar.Value = (int)e.Progress));
        labelStatus.Invoke(new Action(() => labelStatus.Text = e.Message));
      }
    }
  }
}
