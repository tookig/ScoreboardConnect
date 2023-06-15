using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScoreboardConnectWinUI3 {
  public partial class ListenerEmptyCourt : UserControl {
    public ListenerEmptyCourt(string courtName) {
      InitializeComponent();
      labelCourtname.Text = courtName;
      labelTime.Text = DateTime.Now.ToString("HH:mm");
    }
  }
}
