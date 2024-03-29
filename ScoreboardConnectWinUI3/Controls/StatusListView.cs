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
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public StatusListView() {
      InitializeComponent();
      InitColumns();
      View = View.Details;
      FullRowSelect = true;
    }

    internal void Listen(RequestCoordinator coordinator) {
      coordinator.Status += OnStatus;
    }

    private void OnStatus(object sender, RequestCoordinator.StatusMessageEventArgs message) {
      ListViewItem item = new ListViewItem();
      item.Text = message.Level.ToString();
      item.SubItems.Add(message.Message);
      item.BackColor = message.Level switch {
        RequestCoordinator.StatusMessageLevel.Error => Color.Red,
        RequestCoordinator.StatusMessageLevel.Warning => Color.FromArgb(255, 255, 150),
        _ => Color.White
      };
      item.ForeColor = message.Level switch {
        RequestCoordinator.StatusMessageLevel.Error => Color.White,
        _ => Color.Black
      };
      Invoke((MethodInvoker)delegate {
        Items.Add(item);
        EnsureVisible(Items.Count - 1);
      });
    }

    protected void InitColumns() {
      Columns.Clear();
      Columns.Add(new ColumnHeader() {
        Text = "Type",
        Width = 10
      });
      Columns.Add(new ColumnHeader() {
        Text = "Message",
        Width = 90
      });
      AdjustColumnWidths();
    }

    protected void AdjustColumnWidths() {
      int useableWidth = Width - SystemInformation.VerticalScrollBarWidth - 1;
      int relationWidth = 0;
      foreach (ColumnHeader header in Columns) {
        relationWidth += header.Width;
      }
      foreach (ColumnHeader header in Columns) {
        header.Width = useableWidth * header.Width / relationWidth;
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
