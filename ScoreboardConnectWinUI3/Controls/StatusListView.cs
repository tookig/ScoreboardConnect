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
  public class StatusListView : ListView {
    const int MAX_NUMBER_OF_ITEMS = 1000;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public StatusListView() {
      InitializeComponent();
      InitColumns();
      View = View.Details;
      FullRowSelect = true;
      ConnectLogger.Singleton.Message += OnLog;
    }

    private void OnLog(object sender, ConnectLogger.LogEntry entry) {
      ListViewItem item = new ListViewItem();
      item.Text = entry.Timestamp.ToString("HH:mm");
      item.SubItems.Add(entry.Message);
      item.BackColor = entry.Level switch {
        ConnectLogger.LogLevels.Error => Color.Red,
        ConnectLogger.LogLevels.Warning => Color.FromArgb(255, 255, 150),
        _ => Color.White
      };
      item.ForeColor = entry.Level switch {
        ConnectLogger.LogLevels.Error => Color.White,
        _ => Color.Black
      };
      Invoke((MethodInvoker)delegate {
        Items.Add(item);
        EnsureVisible(Items.Count - 1);
        while (Items.Count > MAX_NUMBER_OF_ITEMS) {
          Items.RemoveAt(0);
        }
      });
    }

    protected void InitColumns() {
      Columns.Clear();
      Columns.Add(new ColumnHeader() {
        Text = "Time"
      });
      Columns.Add(new ColumnHeader() {
        Text = "Message"
      });
      AdjustColumnWidths();
    }

    protected void AdjustColumnWidths() {
      int useableWidth = Width - SystemInformation.VerticalScrollBarWidth - 1;
      Columns[0].Width = 60;
      Columns[1].Width = useableWidth - Columns[0].Width;
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
