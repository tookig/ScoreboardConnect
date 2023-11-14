
namespace ScoreboardConnectWinUI3 {
  partial class ListenerMessageControl {
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
      this.labelMessage = new System.Windows.Forms.Label();
      this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
      this.labelHeader = new System.Windows.Forms.Label();
      this.labelTime = new System.Windows.Forms.Label();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
      this.tableLayoutPanel1.Controls.Add(this.labelMessage, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.pictureBoxIcon, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.labelHeader, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.labelTime, 2, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(392, 77);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // labelMessage
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.labelMessage, 2);
      this.labelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
      this.labelMessage.Location = new System.Drawing.Point(63, 25);
      this.labelMessage.Name = "labelMessage";
      this.labelMessage.Size = new System.Drawing.Size(326, 52);
      this.labelMessage.TabIndex = 2;
      this.labelMessage.Text = "label2";
      this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pictureBoxIcon
      // 
      this.pictureBoxIcon.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBoxIcon.Image = global::ScoreboardConnectWinUI3.Resource1.check2;
      this.pictureBoxIcon.Location = new System.Drawing.Point(3, 3);
      this.pictureBoxIcon.Name = "pictureBoxIcon";
      this.tableLayoutPanel1.SetRowSpan(this.pictureBoxIcon, 2);
      this.pictureBoxIcon.Size = new System.Drawing.Size(54, 71);
      this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBoxIcon.TabIndex = 0;
      this.pictureBoxIcon.TabStop = false;
      // 
      // labelHeader
      // 
      this.labelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
      this.labelHeader.Location = new System.Drawing.Point(63, 0);
      this.labelHeader.Name = "labelHeader";
      this.labelHeader.Size = new System.Drawing.Size(266, 25);
      this.labelHeader.TabIndex = 1;
      this.labelHeader.Text = "label1";
      this.labelHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelTime
      // 
      this.labelTime.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelTime.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
      this.labelTime.Location = new System.Drawing.Point(335, 0);
      this.labelTime.Name = "labelTime";
      this.labelTime.Size = new System.Drawing.Size(54, 25);
      this.labelTime.TabIndex = 3;
      this.labelTime.Text = "label3";
      this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // ListenerMessageControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "ListenerMessageControl";
      this.Size = new System.Drawing.Size(392, 77);
      this.tableLayoutPanel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.PictureBox pictureBoxIcon;
    private System.Windows.Forms.Label labelMessage;
    private System.Windows.Forms.Label labelHeader;
    private System.Windows.Forms.Label labelTime;
  }
}
