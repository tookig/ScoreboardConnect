namespace ScoreboardConnectWinUI3.Controls {
  partial class TournamentTVControl {
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
      components = new System.ComponentModel.Container();
      groupBox1 = new System.Windows.Forms.GroupBox();
      labelErrors = new System.Windows.Forms.Label();
      labelUpdates = new System.Windows.Forms.Label();
      labelStatus = new System.Windows.Forms.Label();
      textTimer = new System.Windows.Forms.Timer(components);
      groupBox1.SuspendLayout();
      SuspendLayout();
      // 
      // groupBox1
      // 
      groupBox1.Controls.Add(labelErrors);
      groupBox1.Controls.Add(labelUpdates);
      groupBox1.Controls.Add(labelStatus);
      groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      groupBox1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
      groupBox1.ForeColor = System.Drawing.Color.SeaGreen;
      groupBox1.Location = new System.Drawing.Point(0, 0);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new System.Drawing.Size(435, 90);
      groupBox1.TabIndex = 0;
      groupBox1.TabStop = false;
      groupBox1.Text = "Tournament TV";
      // 
      // labelErrors
      // 
      labelErrors.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelErrors.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelErrors.ForeColor = System.Drawing.Color.OrangeRed;
      labelErrors.Location = new System.Drawing.Point(6, 29);
      labelErrors.Name = "labelErrors";
      labelErrors.Size = new System.Drawing.Size(423, 27);
      labelErrors.TabIndex = 3;
      labelErrors.Text = "Errormessage";
      labelErrors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelUpdates
      // 
      labelUpdates.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelUpdates.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelUpdates.ForeColor = System.Drawing.Color.RoyalBlue;
      labelUpdates.Location = new System.Drawing.Point(6, 52);
      labelUpdates.Name = "labelUpdates";
      labelUpdates.Size = new System.Drawing.Size(423, 26);
      labelUpdates.TabIndex = 2;
      labelUpdates.Text = "No updates received";
      labelUpdates.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelStatus
      // 
      labelStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
      labelStatus.AutoSize = true;
      labelStatus.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelStatus.ForeColor = System.Drawing.Color.Red;
      labelStatus.Location = new System.Drawing.Point(293, 0);
      labelStatus.Name = "labelStatus";
      labelStatus.Size = new System.Drawing.Size(136, 25);
      labelStatus.TabIndex = 1;
      labelStatus.Text = "Not connected";
      labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // textTimer
      // 
      textTimer.Enabled = true;
      textTimer.Interval = 400;
      textTimer.Tick += textTimer_Tick;
      // 
      // TournamentTVControl
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      Controls.Add(groupBox1);
      Name = "TournamentTVControl";
      Size = new System.Drawing.Size(435, 90);
      Load += TournamentTVControl_Load;
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label labelStatus;
    private System.Windows.Forms.Label labelUpdates;
    private System.Windows.Forms.Timer textTimer;
    private System.Windows.Forms.Label labelErrors;
  }
}
