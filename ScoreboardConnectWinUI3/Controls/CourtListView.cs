using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace ScoreboardConnectWinUI3 {
  // https://docs.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/use-combobox-edit-listview
  public class CourtListView : ListView {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public event EventHandler<(int, string)> CourtAssignmentChanged;

    private ComboBox m_combo = new ComboBox();
    private ListViewItem m_currentItem;

    public CourtListView() {
      InitializeComponent();
      InitColumns();
      View = View.Details;
      FullRowSelect = true;

      m_combo.SelectedValueChanged += combo_SelectedValueChanged;
      m_combo.Leave += combo_SelectedValueChanged;
    }

    private void combo_SelectedValueChanged(object sender, EventArgs e) {
      if (m_currentItem == null) return;
      m_currentItem.SubItems[1].Text = m_combo.Text;
      m_currentItem.SubItems[1].Tag = m_combo.SelectedItem;
      m_combo.Visible = false;
      CourtAssignmentChanged?.Invoke(this, ((((ScoreboardLiveApi.Court)m_currentItem.Tag).CourtID), (m_combo.SelectedItem as TP.Court)?.Name));
    }

    public void PopulateScoreboardCourts(List<ScoreboardLiveApi.Court> courts) {
      Items.Clear();
      foreach (ScoreboardLiveApi.Court court in courts) {
        var lvi = Items.Add(new ListViewItem(court.Name) {
          Tag = court
        });
        lvi.SubItems.Add("<not set>");
      }
    }

    public void PopulateTPCourts(List<TP.Court> courts) {
      m_combo.Items.Clear();
      m_combo.Items.AddRange(courts.ToArray());
      m_combo.DropDownStyle = ComboBoxStyle.DropDownList;
    }

    public void AssignTPCourtToScoreboardCourt(int scoreboardCourtID, string tpCourtName) {
      foreach (ListViewItem item in Items) {
        if (((ScoreboardLiveApi.Court)item.Tag).CourtID == scoreboardCourtID) {
          foreach (object o in m_combo.Items) {
            TP.Court tpCourt = (TP.Court)o;
            if (tpCourt?.Name == tpCourtName) {
              item.SubItems[1].Text = tpCourt.ToString();
              item.SubItems[1].Tag = tpCourt;
            }
          }
        }
      }
    }

    public List<ScoreboardLiveApi.Court> GetScoreboardCourtsAssignedToTPCourt(string tpCourtName) {
      List<ScoreboardLiveApi.Court> scoreboardCourtsFound = new List<ScoreboardLiveApi.Court>();
      foreach (ListViewItem item in Items) {
        if ((item.SubItems[1].Tag is TP.Court court) && court.Name == tpCourtName) {
          scoreboardCourtsFound.Add((ScoreboardLiveApi.Court)item.Tag);
        }
      }
      return scoreboardCourtsFound;
    }

    protected void InitColumns() {
      Columns.Clear();
      Columns.Add(new ColumnHeader() {
        Text = "Scoreboard court"
      });
      Columns.Add(new ColumnHeader() {
        Text = "TP court"
      });
      AdjustColumnWidths();
    }

    protected void AdjustColumnWidths() {
      int columnWidth = (Width - SystemInformation.VerticalScrollBarWidth - 1) / 2;
      foreach (ColumnHeader header in Columns) {
        header.Width = columnWidth;
      }
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

    protected override void OnSizeChanged(EventArgs e) {
      base.OnSizeChanged(e);
      AdjustColumnWidths();
    }

    protected override void OnMouseUp(MouseEventArgs e) {
      base.OnMouseUp(e);
      m_currentItem = GetItemAt(e.X, e.Y);
      if (m_currentItem == null) return;
      Rectangle clickedItemBounds = m_currentItem.SubItems[1].Bounds;
      if (!clickedItemBounds.Contains(e.X, e.Y)) return;
      m_combo.Bounds = clickedItemBounds;
      m_combo.SelectedIndex = m_combo.FindStringExact(m_currentItem.SubItems[1].Text);
      m_combo.Parent = this;
      m_combo.Visible = true;
      m_combo.BringToFront();
      m_combo.Focus();
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

    private const int WM_HSCROLL = 0x114;
    private const int WM_VSCROLL = 0x115;

    protected override void WndProc(ref Message msg) {
      // Look for the WM_VSCROLL or the WM_HSCROLL messages.
      if ((msg.Msg == WM_VSCROLL) || (msg.Msg == WM_HSCROLL)) {
        // Move focus to the ListView to cause ComboBox to lose focus.
        this.Focus();
      }
      // Pass message to default handler.
      base.WndProc(ref msg);
    }
  }
}
