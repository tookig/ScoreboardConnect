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
      labelUpdates = new System.Windows.Forms.Label();
      labelStatus = new System.Windows.Forms.Label();
      textTimer = new System.Windows.Forms.Timer(components);
      labelErrors = new System.Windows.Forms.Label();
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
      groupBox1.Size = new System.Drawing.Size(435, 150);
      groupBox1.TabIndex = 0;
      groupBox1.TabStop = false;
      groupBox1.Text = "Tournament TV";
      // 
      // labelUpdates
      // 
      labelUpdates.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelUpdates.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelUpdates.ForeColor = System.Drawing.Color.RoyalBlue;
      labelUpdates.Location = new System.Drawing.Point(6, 89);
      labelUpdates.Name = "labelUpdates";
      labelUpdates.Size = new System.Drawing.Size(423, 32);
      labelUpdates.TabIndex = 2;
      labelUpdates.Text = "No updates received";
      labelUpdates.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelStatus
      // 
      labelStatus.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelStatus.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelStatus.ForeColor = System.Drawing.Color.Red;
      labelStatus.Location = new System.Drawing.Point(6, 29);
      labelStatus.Name = "labelStatus";
      labelStatus.Size = new System.Drawing.Size(423, 32);
      labelStatus.TabIndex = 1;
      labelStatus.Text = "Not connected";
      labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // textTimer
      // 
      textTimer.Enabled = true;
      textTimer.Interval = 400;
      textTimer.Tick += textTimer_Tick;
      // 
      // labelErrors
      // 
      labelErrors.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelErrors.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelErrors.ForeColor = System.Drawing.Color.OrangeRed;
      labelErrors.Location = new System.Drawing.Point(6, 59);
      labelErrors.Name = "labelErrors";
      labelErrors.Size = new System.Drawing.Size(423, 32);
      labelErrors.TabIndex = 3;
      labelErrors.Text = "Errormessage";
      labelErrors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // TournamentTVControl
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      Controls.Add(groupBox1);
      Name = "TournamentTVControl";
      Size = new System.Drawing.Size(435, 150);
      Load += TournamentTVControl_Load;
      groupBox1.ResumeLayout(false);
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
