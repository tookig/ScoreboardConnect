using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScoreboardConnectUpdate {
  public partial class UpdateMessageBox : Form {
    public UpdateMessageBox(bool isCritical = false) {
      InitializeComponent();
      labelCritical.Visible = isCritical;
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
      Process.Start(new ProcessStartInfo(linkLabel1.Text) { UseShellExecute = true });
    }

    private void buttonClose_Click(object sender, EventArgs e) {
      Close();
    }
  }
}
