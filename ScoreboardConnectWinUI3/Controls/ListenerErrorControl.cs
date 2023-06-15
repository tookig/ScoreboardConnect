using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScoreboardConnectWinUI3 {
  public partial class ListenerErrorControl : UserControl {
    public ListenerErrorControl(string header, Exception e) {
      InitializeComponent();
      labelHeader.Text = header;
      labelMessage.Text = e.Message;
    }
  }
}
