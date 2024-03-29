
namespace ScoreboardConnectWinUI3
{
  partial class FormMain
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
      scoreboardLiveControl1 = new Controls.ScoreboardLiveControl();
      tpNetworkControl1 = new Controls.TPNetworkControl();
      tournamenttvControl = new Controls.TournamentTVControl();
      groupCourts = new System.Windows.Forms.GroupBox();
      courtListView = new CourtListView();
      menuMain = new System.Windows.Forms.MenuStrip();
      fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      uploadTournamentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      openTPFileDialog = new System.Windows.Forms.OpenFileDialog();
      statusListView = new StatusListView();
      groupCourts.SuspendLayout();
      menuMain.SuspendLayout();
      SuspendLayout();
      // 
      // scoreboardLiveControl1
      // 
      scoreboardLiveControl1.Location = new System.Drawing.Point(11, 38);
      scoreboardLiveControl1.Name = "scoreboardLiveControl1";
      scoreboardLiveControl1.Size = new System.Drawing.Size(496, 131);
      scoreboardLiveControl1.TabIndex = 9;
      // 
      // tpNetworkControl1
      // 
      tpNetworkControl1.Location = new System.Drawing.Point(10, 175);
      tpNetworkControl1.Name = "tpNetworkControl1";
      tpNetworkControl1.Size = new System.Drawing.Size(497, 123);
      tpNetworkControl1.TabIndex = 10;
      // 
      // tournamenttvControl
      // 
      tournamenttvControl.Location = new System.Drawing.Point(15, 304);
      tournamenttvControl.Name = "tournamenttvControl";
      tournamenttvControl.Size = new System.Drawing.Size(492, 160);
      tournamenttvControl.TabIndex = 11;
      // 
      // groupCourts
      // 
      groupCourts.Controls.Add(courtListView);
      groupCourts.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
      groupCourts.Location = new System.Drawing.Point(10, 470);
      groupCourts.Name = "groupCourts";
      groupCourts.Size = new System.Drawing.Size(497, 244);
      groupCourts.TabIndex = 12;
      groupCourts.TabStop = false;
      groupCourts.Text = "Courts";
      // 
      // courtListView
      // 
      courtListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
      courtListView.Dock = System.Windows.Forms.DockStyle.Fill;
      courtListView.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      courtListView.FullRowSelect = true;
      courtListView.Location = new System.Drawing.Point(3, 29);
      courtListView.Name = "courtListView";
      courtListView.Size = new System.Drawing.Size(491, 212);
      courtListView.TabIndex = 0;
      courtListView.UseCompatibleStateImageBehavior = false;
      courtListView.View = System.Windows.Forms.View.Details;
      // 
      // menuMain
      // 
      menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem });
      menuMain.Location = new System.Drawing.Point(0, 0);
      menuMain.Name = "menuMain";
      menuMain.Size = new System.Drawing.Size(1010, 24);
      menuMain.TabIndex = 13;
      menuMain.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { uploadTournamentToolStripMenuItem, settingsToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
      fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      fileToolStripMenuItem.Text = "&File";
      // 
      // uploadTournamentToolStripMenuItem
      // 
      uploadTournamentToolStripMenuItem.Name = "uploadTournamentToolStripMenuItem";
      uploadTournamentToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
      uploadTournamentToolStripMenuItem.Text = "&Upload tournament";
      uploadTournamentToolStripMenuItem.Click += uploadTournamentToolStripMenuItem_Click;
      // 
      // settingsToolStripMenuItem
      // 
      settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
      settingsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
      settingsToolStripMenuItem.Text = "&Settings";
      settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
      // 
      // toolStripSeparator1
      // 
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
      // 
      // exitToolStripMenuItem
      // 
      exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      exitToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X;
      exitToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
      exitToolStripMenuItem.Text = "E&xit";
      exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
      // 
      // openTPFileDialog
      // 
      openTPFileDialog.Filter = "TP-files (*.tp)|*.tp|All files (*.*)|*.*";
      openTPFileDialog.Title = "Open TP file";
      // 
      // statusListView
      // 
      statusListView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      statusListView.FullRowSelect = true;
      statusListView.Location = new System.Drawing.Point(527, 38);
      statusListView.Name = "statusListView";
      statusListView.Size = new System.Drawing.Size(471, 260);
      statusListView.TabIndex = 14;
      statusListView.UseCompatibleStateImageBehavior = false;
      statusListView.View = System.Windows.Forms.View.Details;
      // 
      // FormMain
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      AutoScroll = true;
      ClientSize = new System.Drawing.Size(1010, 742);
      Controls.Add(statusListView);
      Controls.Add(groupCourts);
      Controls.Add(tournamenttvControl);
      Controls.Add(tpNetworkControl1);
      Controls.Add(scoreboardLiveControl1);
      Controls.Add(menuMain);
      Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      MainMenuStrip = menuMain;
      Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      Name = "FormMain";
      Text = "Scoreboard Connect";
      Load += FormMain_Load;
      Shown += FormMain_Shown;
      groupCourts.ResumeLayout(false);
      menuMain.ResumeLayout(false);
      menuMain.PerformLayout();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion
    private Controls.ScoreboardLiveControl scoreboardLiveControl1;
    private Controls.TPNetworkControl tpNetworkControl1;
    private Controls.TournamentTVControl tournamenttvControl;
    private System.Windows.Forms.GroupBox groupCourts;
    private System.Windows.Forms.MenuStrip menuMain;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private CourtListView courtListView;
    private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem uploadTournamentToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog openTPFileDialog;
    private StatusListView statusListView;
  }
}

