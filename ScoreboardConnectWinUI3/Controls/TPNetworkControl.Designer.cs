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
      groupBox1 = new System.Windows.Forms.GroupBox();
      labelTournament = new System.Windows.Forms.Label();
      labelInstructions = new System.Windows.Forms.Label();
      labelStatus = new System.Windows.Forms.Label();
      connectionCheckTimer = new System.Windows.Forms.Timer(components);
      groupBox1.SuspendLayout();
      SuspendLayout();
      // 
      // groupBox1
      // 
      groupBox1.Controls.Add(labelTournament);
      groupBox1.Controls.Add(labelInstructions);
      groupBox1.Controls.Add(labelStatus);
      groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      groupBox1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
      groupBox1.ForeColor = System.Drawing.Color.IndianRed;
      groupBox1.Location = new System.Drawing.Point(0, 0);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new System.Drawing.Size(557, 159);
      groupBox1.TabIndex = 0;
      groupBox1.TabStop = false;
      groupBox1.Text = "Tournament Planner Network";
      // 
      // labelTournament
      // 
      labelTournament.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelTournament.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelTournament.ForeColor = System.Drawing.Color.RoyalBlue;
      labelTournament.Location = new System.Drawing.Point(4, 63);
      labelTournament.Name = "labelTournament";
      labelTournament.Size = new System.Drawing.Size(548, 32);
      labelTournament.TabIndex = 3;
      labelTournament.Text = "Tournament";
      labelTournament.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelInstructions
      // 
      labelInstructions.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
      labelInstructions.ForeColor = System.Drawing.Color.Black;
      labelInstructions.Location = new System.Drawing.Point(6, 61);
      labelInstructions.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
      labelInstructions.Name = "labelInstructions";
      labelInstructions.Padding = new System.Windows.Forms.Padding(20);
      labelInstructions.Size = new System.Drawing.Size(545, 103);
      labelInstructions.TabIndex = 2;
      labelInstructions.Text = "- Open the tournament in Tournament Planner\r\n- Select the menu Extra > Tournament Planner Network\r\n- Click the \"Enable\"-button";
      // 
      // labelStatus
      // 
      labelStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelStatus.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelStatus.ForeColor = System.Drawing.Color.Red;
      labelStatus.Location = new System.Drawing.Point(3, 29);
      labelStatus.Name = "labelStatus";
      labelStatus.Size = new System.Drawing.Size(548, 32);
      labelStatus.TabIndex = 1;
      labelStatus.Text = "Not connected";
      labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // connectionCheckTimer
      // 
      connectionCheckTimer.Interval = 1000;
      connectionCheckTimer.Tick += connectionCheckTimer_Tick;
      // 
      // TPNetworkControl
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      Controls.Add(groupBox1);
      Name = "TPNetworkControl";
      Size = new System.Drawing.Size(557, 159);
      Load += TPNetworkControl_Load;
      groupBox1.ResumeLayout(false);
      ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label labelStatus;
    private System.Windows.Forms.Label labelInstructions;
    private System.Windows.Forms.Timer connectionCheckTimer;
    private System.Windows.Forms.Label labelTournament;
  }
}
