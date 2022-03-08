using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreboardConnectWinUI3 {
  public partial class FormDeviceActivation : Form {
    private ScoreboardLiveApi.ApiHelper m_api;
    private ScoreboardLiveApi.IKeyStore m_keyStore;
    
    public ScoreboardLiveApi.Device Device { get; private set; }

    public FormDeviceActivation(ScoreboardLiveApi.ApiHelper api, ScoreboardLiveApi.IKeyStore keyStore) {
      m_api = api;
      m_keyStore = keyStore;
      InitializeComponent();
    }

    private void textDeviceCode_TextChanged(object sender, EventArgs e) {
      buttonOK.Enabled = textDeviceCode.TextLength == 6;
    }

    private void buttonOK_Click(object sender, EventArgs e) {
      pictureLoading.Visible = true;
      m_api.RegisterDevice(textDeviceCode.Text).ContinueWith(t => {
        pictureLoading.Visible = false;
        if (t.IsFaulted) {
          MessageBox.Show(string.Format("Could not register device:{1}{0}", t.Exception.Message, Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        } else {
          Device = t.Result;
          try {
            m_keyStore.Set(Device);
            DialogResult = DialogResult.OK;
            Close();
          } catch (Exception e) {
            MessageBox.Show(string.Format("Could not save key data :{1}{0}", e.Message, Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }
      }, TaskScheduler.FromCurrentSynchronizationContext());
    }
  }
}
