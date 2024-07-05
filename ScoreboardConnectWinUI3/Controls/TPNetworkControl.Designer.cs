namespace ScoreboardConnectWinUI3.Controls {
  partial class TPNetworkControl {
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
      connectionCheckTimer = new System.Windows.Forms.Timer(components);
      labelTournament = new System.Windows.Forms.Label();
      labelInstructions = new System.Windows.Forms.Label();
      labelStatus = new System.Windows.Forms.Label();
      labelHeader = new System.Windows.Forms.Label();
      SuspendLayout();
      // 
      // connectionCheckTimer
      // 
      connectionCheckTimer.Interval = 1000;
      connectionCheckTimer.Tick += connectionCheckTimer_Tick;
      // 
      // labelTournament
      // 
      labelTournament.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelTournament.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelTournament.ForeColor = System.Drawing.Color.RoyalBlue;
      labelTournament.Location = new System.Drawing.Point(3, 34);
      labelTournament.Name = "labelTournament";
      labelTournament.Size = new System.Drawing.Size(574, 31);
      labelTournament.TabIndex = 6;
      labelTournament.Text = "Tournament";
      labelTournament.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelInstructions
      // 
      labelInstructions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelInstructions.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
      labelInstructions.ForeColor = System.Drawing.Color.Black;
      labelInstructions.Location = new System.Drawing.Point(6, 34);
      labelInstructions.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
      labelInstructions.Name = "labelInstructions";
      labelInstructions.Padding = new System.Windows.Forms.Padding(20);
      labelInstructions.Size = new System.Drawing.Size(571, 91);
      labelInstructions.TabIndex = 5;
      labelInstructions.Text = "- Open the tournament in Tournament Planner\r\n- Select the menu Extra > Tournament Planner Network\r\n- Click the \"Enable\"-button";
      // 
      // labelStatus
      // 
      labelStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
      labelStatus.AutoSize = true;
      labelStatus.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelStatus.ForeColor = System.Drawing.Color.Red;
      labelStatus.Location = new System.Drawing.Point(440, 9);
      labelStatus.Name = "labelStatus";
      labelStatus.Size = new System.Drawing.Size(136, 25);
      labelStatus.TabIndex = 4;
      labelStatus.Text = "Not connected";
      labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelHeader
      // 
      labelHeader.AutoSize = true;
      labelHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
      labelHeader.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
      labelHeader.Location = new System.Drawing.Point(5, 9);
      labelHeader.Name = "labelHeader";
      labelHeader.Size = new System.Drawing.Size(264, 25);
      labelHeader.TabIndex = 7;
      labelHeader.Text = "Tournament Planner Network";
      // 
      // TPNetworkControl
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      BackColor = System.Drawing.Color.White;
      Controls.Add(labelHeader);
      Controls.Add(labelInstructions);
      Controls.Add(labelStatus);
      Controls.Add(labelTournament);
      Name = "TPNetworkControl";
      Size = new System.Drawing.Size(583, 134);
      Load += TPNetworkControl_Load;
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion
    private System.Windows.Forms.Timer connectionCheckTimer;
    private System.Windows.Forms.Label labelTournament;
    private System.Windows.Forms.Label labelInstructions;
    private System.Windows.Forms.Label labelStatus;
    private System.Windows.Forms.Label labelHeader;
  }
}
