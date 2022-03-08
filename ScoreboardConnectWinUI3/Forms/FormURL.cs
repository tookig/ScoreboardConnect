using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreboardConnectWinUI3 {
  public partial class FormURL : Form {
    public string URL {
      get {
        return textURL.Text;
      }
      set {
        textURL.Text = value;
        buttonOK.Enabled = !string.IsNullOrEmpty(URL);
      }
    }

    public List<ScoreboardLiveApi.Unit> Units { get; } = new List<ScoreboardLiveApi.Unit>();

    public FormURL(string url) {
      InitializeComponent();
      textURL.Text = url;
      buttonOK.Enabled = !string.IsNullOrEmpty(URL);
    }

    private void buttonOK_Click(object sender, EventArgs e) {
      // Try and get units from server
      ScoreboardLiveApi.ApiHelper api = new ScoreboardLiveApi.ApiHelper(URL);
      textURL.Enabled = false;
      pictureLoading.Visible = true;
      api.GetUnits().ContinueWith(t => {
        pictureLoading.Visible = false;
        textURL.Enabled = true;
        if (t.IsFaulted) {
          StatusError("Could not connect to this URL.");
        } else {
          Units.AddRange(t.Result);
          DialogResult = DialogResult.OK;
          Close();
        }
      }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void StatusError(string text) {
      labelStatus.Text = text;
    }

    private void textURL_TextChanged(object sender, EventArgs e) {
      StatusError("");
      buttonOK.Enabled = !string.IsNullOrEmpty(URL);
    }
  }
}
