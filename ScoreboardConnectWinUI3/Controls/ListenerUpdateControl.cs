using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScoreboardConnectWinUI3 {
  public partial class ListenerUpdateControl : UserControl {
    public ListenerUpdateControl(string courtName, TP.Match tpMatch) {
      InitializeComponent();
      labelCourtname.Text = courtName;
      labelTeam1Names.Text = tpMatch?.Entries.Item1?.ToString() ?? "";
      labelTeam2Names.Text = tpMatch?.Entries.Item2?.ToString() ?? "";
      for (int i=5; i>0; i--) {
        int team1 = (int)tpMatch.GetType().GetProperty(string.Format("Team1Set{0}", i)).GetValue(tpMatch, null);
        int team2 = (int)tpMatch.GetType().GetProperty(string.Format("Team2Set{0}", i)).GetValue(tpMatch, null);
        if (team1 + team2 > 0) {
          labelTeam1Score.Text = team1.ToString();
          labelTeam2Score.Text = team2.ToString();
          break;
        }
      }
      labelTime.Text = DateTime.Now.ToString("HH:mm");
    }



  }
}
