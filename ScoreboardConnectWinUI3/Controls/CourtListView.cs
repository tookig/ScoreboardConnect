using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace ScoreboardConnectWinUI3 {
  // https://docs.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/use-combobox-edit-listview
  public class CourtListView : ListView, ICourtCorrelator {
    private class TPCourtComparer : IEqualityComparer<TP.Court> {
      public bool Equals(TP.Court x, TP.Court y) {
        return (x != null) && (y!= null) && (x.Name == y.Name);
      }

      public int GetHashCode(TP.Court obj) {
        return obj.Name.GetHashCode();
      }
    }

    private class SBCourtComparer : IEqualityComparer<ScoreboardLiveApi.Court> {
      public bool Equals(ScoreboardLiveApi.Court x, ScoreboardLiveApi.Court y) {
        return (x != null) && (y != null) && (x.CourtID == y.CourtID);
      }

      public int GetHashCode([DisallowNull] ScoreboardLiveApi.Court obj) {
        return obj.CourtID.GetHashCode();
      }
    }

    private string m_NOT_SET = "<not set>";

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public event EventHandler<(int, string)> CourtAssignmentChanged;

    private List<TP.Court> m_tpCourts = new();
    private List<ScoreboardLiveApi.Court> m_sbCourts = new();
    private Dictionary<int, string> m_defaultSetup = new();

    private ComboBox m_combo = new ComboBox();
    private ListViewItem m_currentItem;

    private TPCourtComparer m_TPCourtComparer = new TPCourtComparer();
    private SBCourtComparer m_SBCourtComparer = new SBCourtComparer();

    private static string FormatCourtName(ScoreboardLiveApi.Court sbCourt) {
      return sbCourt.Venue != null ? string.Format("{0} ({1})", sbCourt.Name, sbCourt.Venue.Name) : sbCourt.Name;
    }

    private static string FormatCourtName(TP.Court tpCourt) {
      return tpCourt.ToString();
    }

    public CourtListView() {
      InitializeComponent();
      InitColumns();
      View = View.Details;
      FullRowSelect = true;

      m_combo.SelectedValueChanged += combo_SelectedValueChanged;
      m_combo.Leave += combo_SelectedValueChanged;
      m_combo.KeyUp += (sender, e) => {
        if (e.KeyCode == Keys.Escape) {
          m_combo.Visible = false;
        }
      };
      m_combo.DropDownStyle = ComboBoxStyle.DropDownList;
    }

    public void SetDefaultSetup(Dictionary<int, string> defaultSetup) {
      if (defaultSetup == null) throw new ArgumentNullException("defaultSetup", "Default setup dictionary reference cannot be null");
      m_defaultSetup.Clear();
      defaultSetup.ToList().ForEach(kvp => m_defaultSetup.Add(kvp.Key, kvp.Value));
    }

    public void SetTPCourts(List<TP.Court> tpCourts) {
      if (tpCourts == null) throw new ArgumentNullException("tpCourts", "TP courts list reference cannot be null");
      if (tpCourts.Exists(tpCourt => tpCourt == null)) throw new ArgumentNullException("tpCourts", "TP courts list cannot contain a null value");

      Invoke((MethodInvoker)delegate {
        m_tpCourts.ForEach(RemoveTPCourt);
        m_tpCourts.Clear();
        tpCourts.ForEach(AddTPCourt);
       });
    }

    public void AddTPCourt(TP.Court tpCourt) {
      if (tpCourt == null) throw new ArgumentNullException("tpCourt", "TP court reference cannot be null");
      if (m_tpCourts.Contains(tpCourt, m_TPCourtComparer)) return;
            
      m_tpCourts.Add(tpCourt);
      m_combo.Items.Add(tpCourt);

      CheckAllDefaultValues();

      foreach (ListViewItem item in Items) {
        if (item.SubItems[1].Tag == tpCourt) {
          OnCourtAssignmentChanged(((ScoreboardLiveApi.Court)item.Tag).CourtID, tpCourt.Name);
        }
      }
    }

    public void RemoveTPCourt(TP.Court tpCourt) {
      if (tpCourt == null) throw new ArgumentNullException("tpCourt", "TP court reference cannot be null");
      if (!m_tpCourts.Contains(tpCourt, m_TPCourtComparer)) return;

      m_tpCourts.Remove(tpCourt);
      m_combo.Items.Remove(tpCourt);

      foreach (ListViewItem item in Items) {
        if (m_TPCourtComparer.Equals(item.SubItems[1].Tag as TP.Court, tpCourt)) {
          item.SubItems[1].Text = m_NOT_SET;
          item.SubItems[1].Tag = null;
        }
      }
    }

    public void SetSBCourts(List<ScoreboardLiveApi.Court> sbCourts) {
      if (sbCourts == null) throw new ArgumentNullException("sbCourts", "Court list reference cannot be null");
      if (sbCourts.Exists(sbCourt => sbCourt == null)) throw new ArgumentNullException("sbCourts", "Courts list cannot contain a null value");

      Invoke((MethodInvoker)delegate {
        m_sbCourts.ForEach(RemoveSBCourt);
        m_sbCourts.Clear();
        sbCourts.ForEach(AddSBCourt);
      });
    }

    public void AddSBCourt(ScoreboardLiveApi.Court sbCourt) {
      if (sbCourt == null) throw new ArgumentNullException("sbCourt", "Court reference cannot be null");
      if (m_sbCourts.Contains(sbCourt, m_SBCourtComparer)) return;

      m_sbCourts.Add(sbCourt);
      var lvi = Items.Add(new ListViewItem(FormatCourtName(sbCourt)) {
        Tag = sbCourt
      });
      lvi.SubItems.Add(m_NOT_SET);

      CheckDefaultValue(sbCourt.CourtID);

      if (lvi.SubItems[1].Tag != null) {
        OnCourtAssignmentChanged(sbCourt.CourtID, (lvi.SubItems[1].Tag as TP.Court)?.Name);
      }
    }

    public void RemoveSBCourt(ScoreboardLiveApi.Court sbCourt) {
      if (sbCourt == null) throw new ArgumentNullException("sbCourt", "Court reference cannot be null");
      if (!m_sbCourts.Contains(sbCourt, m_SBCourtComparer)) return;

      foreach (ListViewItem item in Items) {
        if (m_SBCourtComparer.Equals(item.Tag as ScoreboardLiveApi.Court, sbCourt)) {
          Items.Remove(item);
          break;
        }
      }
    }

    private void CheckAllDefaultValues() {
      m_sbCourts.Select(sbCourt => sbCourt.CourtID).ToList().ForEach(CheckDefaultValue);
    }

    private void CheckDefaultValue(int sbCourtID) {
      // Make sure this item has a default value that is in the tp court list
      if (!m_defaultSetup.TryGetValue(sbCourtID, out string defaultTPCourtName)) return;
      var selectedCourt = m_tpCourts.FirstOrDefault(tpCourt => tpCourt.Name == defaultTPCourtName);
      if (selectedCourt == null) return;

      // Find item in list and set the default value, unless a value is already set
      foreach (ListViewItem item in Items) {
        if (((ScoreboardLiveApi.Court)item.Tag).CourtID != sbCourtID) continue;
        if (item.SubItems[1].Tag != null) break;
        item.SubItems[1].Tag = selectedCourt;
        item.SubItems[1].Text = FormatCourtName(selectedCourt);
        break;
      }
    }

    private void combo_SelectedValueChanged(object sender, EventArgs e) {
      m_combo.Visible = false;
      if (m_currentItem == null) return;
      m_currentItem.SubItems[1].Text = m_combo.SelectedItem != null ? m_combo.Text : m_NOT_SET;
      m_currentItem.SubItems[1].Tag = m_combo.SelectedItem;
      OnCourtAssignmentChanged((((ScoreboardLiveApi.Court)m_currentItem.Tag).CourtID), (m_combo.SelectedItem as TP.Court)?.Name);
    }

    private void OnCourtAssignmentChanged(int sbCourtID, string tpCourtName) {
      CourtAssignmentChanged?.Invoke(this, (sbCourtID, tpCourtName));
    }

    /*
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
    */

    public List<ScoreboardLiveApi.Court> GetScoreboardCourtsAssignedToTPCourt(string tpCourtName) {
      List<ScoreboardLiveApi.Court> scoreboardCourtsFound = new List<ScoreboardLiveApi.Court>();
      foreach (ListViewItem item in Items) {
        if ((item.SubItems[1].Tag is TP.Court court) && court.Name == tpCourtName) {
          scoreboardCourtsFound.Add((ScoreboardLiveApi.Court)item.Tag);
        }
      }
      return scoreboardCourtsFound;
    }

    public Dictionary<ScoreboardLiveApi.Court, TP.Court> GetSnapshot() {
      var snapshot = new Dictionary<ScoreboardLiveApi.Court, TP.Court>();
      foreach (ListViewItem item in Items) {
        snapshot.Add((ScoreboardLiveApi.Court)item.Tag, item.SubItems[1].Tag as TP.Court);
      }
      return snapshot;
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
      m_combo.DroppedDown = true;
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

    public List<ScoreboardLiveApi.Court> Correlate(string tpCourtName) {
      List<ScoreboardLiveApi.Court> foundCourts = new();
      Invoke((MethodInvoker)delegate {
        foreach (ListViewItem item in Items) {
          if (item.SubItems[1].Tag == null) continue;
          if ((item.SubItems[1].Tag is TP.Court court) && (court.Name == tpCourtName)) {
            foundCourts.Add((ScoreboardLiveApi.Court)item.Tag);
          }
        }
      });
      return foundCourts;
    }
  }
}
