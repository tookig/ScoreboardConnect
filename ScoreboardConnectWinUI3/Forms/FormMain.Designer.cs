
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
      panelCourtChanges = new System.Windows.Forms.Panel();
      labelCourtListen = new System.Windows.Forms.Label();
      onOffCourtChanges = new Controls.OnOffControl();
      label1 = new System.Windows.Forms.Label();
      groupCourts.SuspendLayout();
      menuMain.SuspendLayout();
      panelCourtChanges.SuspendLayout();
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
      menuMain.Size = new System.Drawing.Size(1041, 24);
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
      statusListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
      statusListView.FullRowSelect = true;
      statusListView.Location = new System.Drawing.Point(520, 451);
      statusListView.Name = "statusListView";
      statusListView.Size = new System.Drawing.Size(502, 260);
      statusListView.TabIndex = 14;
      statusListView.UseCompatibleStateImageBehavior = false;
      statusListView.View = System.Windows.Forms.View.Details;
      // 
      // panelCourtChanges
      // 
      panelCourtChanges.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      panelCourtChanges.Controls.Add(labelCourtListen);
      panelCourtChanges.Controls.Add(onOffCourtChanges);
      panelCourtChanges.Location = new System.Drawing.Point(520, 38);
      panelCourtChanges.Name = "panelCourtChanges";
      panelCourtChanges.Size = new System.Drawing.Size(509, 51);
      panelCourtChanges.TabIndex = 15;
      // 
      // labelCourtListen
      // 
      labelCourtListen.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelCourtListen.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
      labelCourtListen.Location = new System.Drawing.Point(7, 3);
      labelCourtListen.Name = "labelCourtListen";
      labelCourtListen.Size = new System.Drawing.Size(375, 45);
      labelCourtListen.TabIndex = 1;
      labelCourtListen.Text = "Synchronize courts";
      labelCourtListen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // onOffCourtChanges
      // 
      onOffCourtChanges.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      onOffCourtChanges.BackColor = System.Drawing.Color.FromArgb(255, 100, 100);
      onOffCourtChanges.Checked = false;
      onOffCourtChanges.Location = new System.Drawing.Point(415, 3);
      onOffCourtChanges.Name = "onOffCourtChanges";
      onOffCourtChanges.OffText = "OFF";
      onOffCourtChanges.OnText = "ON";
      onOffCourtChanges.Size = new System.Drawing.Size(91, 45);
      onOffCourtChanges.TabIndex = 0;
      onOffCourtChanges.Text = "onOffControl1";
      onOffCourtChanges.CheckedChanged += onOffCourtChanges_CheckedChanged;
      // 
      // label1
      // 
      label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
      label1.ForeColor = System.Drawing.Color.Gray;
      label1.Location = new System.Drawing.Point(520, 423);
      label1.Name = "label1";
      label1.Size = new System.Drawing.Size(502, 25);
      label1.TabIndex = 16;
      label1.Text = "Event log";
      label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // FormMain
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      AutoScroll = true;
      ClientSize = new System.Drawing.Size(1041, 739);
      Controls.Add(label1);
      Controls.Add(panelCourtChanges);
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
      panelCourtChanges.ResumeLayout(false);
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
    private System.Windows.Forms.Panel panelCourtChanges;
    private Controls.OnOffControl onOffCourtChanges;
    private System.Windows.Forms.Label labelCourtListen;
    private System.Windows.Forms.Label label1;
  }
}

