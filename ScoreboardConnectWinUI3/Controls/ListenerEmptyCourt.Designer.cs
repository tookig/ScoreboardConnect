
namespace ScoreboardConnectWinUI3 {
  partial class ListenerEmptyCourt {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.labelTime = new System.Windows.Forms.Label();
      this.labelCourtname = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
      this.tableLayoutPanel1.Controls.Add(this.labelTime, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.labelCourtname, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(285, 37);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // labelTime
      // 
      this.labelTime.Location = new System.Drawing.Point(238, 0);
      this.labelTime.Name = "labelTime";
      this.labelTime.Size = new System.Drawing.Size(44, 18);
      this.labelTime.TabIndex = 9;
      this.labelTime.Text = "labelTime";
      this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelCourtname
      // 
      this.labelCourtname.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelCourtname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
      this.labelCourtname.ForeColor = System.Drawing.Color.RoyalBlue;
      this.labelCourtname.Location = new System.Drawing.Point(3, 0);
      this.labelCourtname.Name = "labelCourtname";
      this.labelCourtname.Size = new System.Drawing.Size(229, 18);
      this.labelCourtname.TabIndex = 7;
      this.labelCourtname.Text = "label1";
      this.labelCourtname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(3, 18);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(229, 19);
      this.label1.TabIndex = 8;
      this.label1.Text = "Court cleared";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ListenerEmptyCourt
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "ListenerEmptyCourt";
      this.Size = new System.Drawing.Size(285, 37);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label labelCourtname;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label labelTime;
  }
}
