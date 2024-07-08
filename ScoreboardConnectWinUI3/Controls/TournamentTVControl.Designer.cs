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
      textTimer = new System.Windows.Forms.Timer(components);
      labelErrors = new System.Windows.Forms.Label();
      labelUpdates = new System.Windows.Forms.Label();
      labelStatus = new System.Windows.Forms.Label();
      labelHeader = new System.Windows.Forms.Label();
      SuspendLayout();
      // 
      // textTimer
      // 
      textTimer.Enabled = true;
      textTimer.Interval = 400;
      textTimer.Tick += textTimer_Tick;
      // 
      // labelErrors
      // 
      labelErrors.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelErrors.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelErrors.ForeColor = System.Drawing.Color.OrangeRed;
      labelErrors.Location = new System.Drawing.Point(3, 45);
      labelErrors.Name = "labelErrors";
      labelErrors.Size = new System.Drawing.Size(427, 27);
      labelErrors.TabIndex = 6;
      labelErrors.Text = "Errormessage";
      labelErrors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelUpdates
      // 
      labelUpdates.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelUpdates.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelUpdates.ForeColor = System.Drawing.Color.RoyalBlue;
      labelUpdates.Location = new System.Drawing.Point(3, 68);
      labelUpdates.Name = "labelUpdates";
      labelUpdates.Size = new System.Drawing.Size(427, 26);
      labelUpdates.TabIndex = 5;
      labelUpdates.Text = "No updates received";
      labelUpdates.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelStatus
      // 
      labelStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
      labelStatus.AutoSize = true;
      labelStatus.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelStatus.ForeColor = System.Drawing.Color.Red;
      labelStatus.Location = new System.Drawing.Point(294, 9);
      labelStatus.Name = "labelStatus";
      labelStatus.Size = new System.Drawing.Size(136, 25);
      labelStatus.TabIndex = 4;
      labelStatus.Text = "Not connected";
      labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelHeader
      // 
      labelHeader.AutoSize = true;
      labelHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
      labelHeader.ForeColor = System.Drawing.Color.Teal;
      labelHeader.Location = new System.Drawing.Point(3, 9);
      labelHeader.Name = "labelHeader";
      labelHeader.Size = new System.Drawing.Size(142, 25);
      labelHeader.TabIndex = 7;
      labelHeader.Text = "Tournament TV";
      // 
      // TournamentTVControl
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      BackColor = System.Drawing.Color.White;
      Controls.Add(labelHeader);
      Controls.Add(labelErrors);
      Controls.Add(labelUpdates);
      Controls.Add(labelStatus);
      Name = "TournamentTVControl";
      Size = new System.Drawing.Size(433, 109);
      Load += TournamentTVControl_Load;
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion
    private System.Windows.Forms.Timer textTimer;
    private System.Windows.Forms.Label labelErrors;
    private System.Windows.Forms.Label labelUpdates;
    private System.Windows.Forms.Label labelStatus;
    private System.Windows.Forms.Label labelHeader;
  }
}
