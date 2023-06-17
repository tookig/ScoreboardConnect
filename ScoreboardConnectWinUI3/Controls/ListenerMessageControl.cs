using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScoreboardConnectWinUI3 {
  public partial class ListenerMessageControl : UserControl {
    public enum MessageType { Success, Warning, Error }
    public ListenerMessageControl(string header, string message, MessageType messageType = MessageType.Success) {
      InitializeComponent();
      labelHeader.Text = header;
      labelMessage.Text = message;
      labelTime.Text = DateTime.Now.ToString("HH:mm");
      if (messageType == MessageType.Error) {
        labelHeader.ForeColor = Color.Red;
        pictureBoxIcon.Image = global::ScoreboardConnectWinUI3.Resource1.exclamation_octagon;
      } else if (messageType == MessageType.Warning) {
        labelHeader.ForeColor = Color.Blue;
        pictureBoxIcon.Image = global::ScoreboardConnectWinUI3.Resource1.exclamation_circle;
      }
    }
  }
}
