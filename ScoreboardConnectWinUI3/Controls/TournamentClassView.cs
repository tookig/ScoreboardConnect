using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace ScoreboardConnectWinUI3 {
  public class TournamentClassView : ListView {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public TournamentClassView() {
      InitializeComponent();
      InitColumns();
      FullRowSelect = true;
      HeaderStyle = ColumnHeaderStyle.None;
      CheckBoxes = true;
      View = View.Details;
    }

    public List<TP.Event> GetSelectedTournamentEvents() {
      List<TP.Event> checkedTournamentClasses = new List<TP.Event>();
      foreach (ListViewItem lvi in Items) {
        if (lvi.Checked) {
          checkedTournamentClasses.Add((TP.Event)lvi.Tag);
        }
      }
      return checkedTournamentClasses;
    }

    public void Populate(IEnumerable<TP.Event> tpClasses) {
      foreach (TP.Event tpClass in tpClasses) {
        Items.Add(new ListViewItem(tpClass.Name) {
          Tag = tpClass
        });
      }
    }

    protected void InitColumns() {
      Columns.Add("Fixture");
    }

    protected void AdjustColumnWidths() {
      int columnWidth = Width - SystemInformation.VerticalScrollBarWidth - 2;
      foreach (ColumnHeader header in Columns) {
        header.Width = columnWidth;
      }
    }

    protected override void OnSizeChanged(EventArgs e) {
      base.OnSizeChanged(e);
      AdjustColumnWidths();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing) {
      if (disposing) {
        if (components != null)
          components.Dispose();
      }
      base.Dispose(disposing);
    }


    #region Component Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      components = new System.ComponentModel.Container();
    }
    #endregion
  }
}
