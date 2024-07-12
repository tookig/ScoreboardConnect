
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
      onOffCourtChanges = new Controls.OnOffControl();
      menuMain = new System.Windows.Forms.MenuStrip();
      fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      uploadTournamentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      logLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      warningsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      errorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      openTPFileDialog = new System.Windows.Forms.OpenFileDialog();
      panel1 = new System.Windows.Forms.Panel();
      courtListView = new CourtListView();
      labelCourtListen = new System.Windows.Forms.Label();
      panel2 = new System.Windows.Forms.Panel();
      label3 = new System.Windows.Forms.Label();
      onOffUpdateMatchResult = new Controls.OnOffControl();
      label2 = new System.Windows.Forms.Label();
      curtainLogs = new Controls.CurtainControl();
      menuMain.SuspendLayout();
      panel1.SuspendLayout();
      panel2.SuspendLayout();
      SuspendLayout();
      // 
      // scoreboardLiveControl1
      // 
      scoreboardLiveControl1.BackColor = System.Drawing.Color.White;
      scoreboardLiveControl1.Location = new System.Drawing.Point(10, 38);
      scoreboardLiveControl1.Name = "scoreboardLiveControl1";
      scoreboardLiveControl1.Size = new System.Drawing.Size(497, 115);
      scoreboardLiveControl1.TabIndex = 9;
      // 
      // tpNetworkControl1
      // 
      tpNetworkControl1.BackColor = System.Drawing.Color.White;
      tpNetworkControl1.Location = new System.Drawing.Point(10, 159);
      tpNetworkControl1.Name = "tpNetworkControl1";
      tpNetworkControl1.Size = new System.Drawing.Size(497, 119);
      tpNetworkControl1.TabIndex = 10;
      // 
      // tournamenttvControl
      // 
      tournamenttvControl.BackColor = System.Drawing.Color.White;
      tournamenttvControl.Location = new System.Drawing.Point(10, 284);
      tournamenttvControl.Name = "tournamenttvControl";
      tournamenttvControl.Size = new System.Drawing.Size(497, 116);
      tournamenttvControl.TabIndex = 11;
      // 
      // onOffCourtChanges
      // 
      onOffCourtChanges.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
      onOffCourtChanges.BackColor = System.Drawing.Color.FromArgb(255, 100, 100);
      onOffCourtChanges.Checked = false;
      onOffCourtChanges.Location = new System.Drawing.Point(403, 3);
      onOffCourtChanges.MinimumSize = new System.Drawing.Size(10, 10);
      onOffCourtChanges.Name = "onOffCourtChanges";
      onOffCourtChanges.OffText = "OFF";
      onOffCourtChanges.OnText = "ON";
      onOffCourtChanges.Size = new System.Drawing.Size(91, 45);
      onOffCourtChanges.TabIndex = 1;
      onOffCourtChanges.Text = "onOffControl1";
      onOffCourtChanges.CheckedChanged += onOffCourtChanges_CheckedChanged;
      // 
      // menuMain
      // 
      menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, settingsToolStripMenuItem1 });
      menuMain.Location = new System.Drawing.Point(0, 0);
      menuMain.Name = "menuMain";
      menuMain.Size = new System.Drawing.Size(1039, 24);
      menuMain.TabIndex = 13;
      menuMain.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { uploadTournamentToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
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
      // settingsToolStripMenuItem1
      // 
      settingsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { settingsToolStripMenuItem, logLevelToolStripMenuItem });
      settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
      settingsToolStripMenuItem1.Size = new System.Drawing.Size(61, 20);
      settingsToolStripMenuItem1.Text = "&Settings";
      // 
      // settingsToolStripMenuItem
      // 
      settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
      settingsToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
      settingsToolStripMenuItem.Text = "Scoreboard Live Settings";
      settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
      // 
      // logLevelToolStripMenuItem
      // 
      logLevelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { allToolStripMenuItem, infoToolStripMenuItem, warningsToolStripMenuItem, errorsToolStripMenuItem });
      logLevelToolStripMenuItem.Name = "logLevelToolStripMenuItem";
      logLevelToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
      logLevelToolStripMenuItem.Text = "Log level";
      // 
      // allToolStripMenuItem
      // 
      allToolStripMenuItem.Name = "allToolStripMenuItem";
      allToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      allToolStripMenuItem.Text = "All";
      allToolStripMenuItem.Click += logLevelToolStripChange_Click;
      // 
      // infoToolStripMenuItem
      // 
      infoToolStripMenuItem.Name = "infoToolStripMenuItem";
      infoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      infoToolStripMenuItem.Text = "Info";
      infoToolStripMenuItem.Click += logLevelToolStripChange_Click;
      // 
      // warningsToolStripMenuItem
      // 
      warningsToolStripMenuItem.Name = "warningsToolStripMenuItem";
      warningsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      warningsToolStripMenuItem.Text = "Warnings";
      warningsToolStripMenuItem.Click += logLevelToolStripChange_Click;
      // 
      // errorsToolStripMenuItem
      // 
      errorsToolStripMenuItem.Name = "errorsToolStripMenuItem";
      errorsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      errorsToolStripMenuItem.Text = "Errors";
      errorsToolStripMenuItem.Click += logLevelToolStripChange_Click;
      // 
      // openTPFileDialog
      // 
      openTPFileDialog.Filter = "TP-files (*.tp)|*.tp|All files (*.*)|*.*";
      openTPFileDialog.Title = "Open TP file";
      // 
      // panel1
      // 
      panel1.BackColor = System.Drawing.Color.White;
      panel1.Controls.Add(courtListView);
      panel1.Controls.Add(onOffCourtChanges);
      panel1.Controls.Add(labelCourtListen);
      panel1.Location = new System.Drawing.Point(531, 38);
      panel1.Name = "panel1";
      panel1.Size = new System.Drawing.Size(497, 362);
      panel1.TabIndex = 17;
      // 
      // courtListView
      // 
      courtListView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      courtListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
      courtListView.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      courtListView.FullRowSelect = true;
      courtListView.Location = new System.Drawing.Point(3, 57);
      courtListView.Name = "courtListView";
      courtListView.Size = new System.Drawing.Size(487, 302);
      courtListView.TabIndex = 4;
      courtListView.UseCompatibleStateImageBehavior = false;
      courtListView.View = System.Windows.Forms.View.Details;
      // 
      // labelCourtListen
      // 
      labelCourtListen.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelCourtListen.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
      labelCourtListen.Location = new System.Drawing.Point(3, 3);
      labelCourtListen.Name = "labelCourtListen";
      labelCourtListen.Size = new System.Drawing.Size(375, 42);
      labelCourtListen.TabIndex = 3;
      labelCourtListen.Text = "Synchronize courts";
      labelCourtListen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // panel2
      // 
      panel2.BackColor = System.Drawing.Color.White;
      panel2.Controls.Add(label3);
      panel2.Controls.Add(onOffUpdateMatchResult);
      panel2.Controls.Add(label2);
      panel2.Location = new System.Drawing.Point(531, 406);
      panel2.Name = "panel2";
      panel2.Size = new System.Drawing.Size(497, 301);
      panel2.TabIndex = 18;
      // 
      // label3
      // 
      label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      label3.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
      label3.Location = new System.Drawing.Point(3, 62);
      label3.Name = "label3";
      label3.Size = new System.Drawing.Size(488, 101);
      label3.TabIndex = 6;
      label3.Text = "Please note that this feature is still in an experimental stage. To avoid data loss, backup the Tournament Planner file before proceeding.";
      // 
      // onOffUpdateMatchResult
      // 
      onOffUpdateMatchResult.BackColor = System.Drawing.Color.FromArgb(255, 100, 100);
      onOffUpdateMatchResult.Checked = false;
      onOffUpdateMatchResult.Location = new System.Drawing.Point(403, 3);
      onOffUpdateMatchResult.MinimumSize = new System.Drawing.Size(10, 10);
      onOffUpdateMatchResult.Name = "onOffUpdateMatchResult";
      onOffUpdateMatchResult.OffText = "OFF";
      onOffUpdateMatchResult.OnText = "ON";
      onOffUpdateMatchResult.Size = new System.Drawing.Size(87, 43);
      onOffUpdateMatchResult.TabIndex = 5;
      onOffUpdateMatchResult.Text = "onOffControl1";
      onOffUpdateMatchResult.Click += onOffUpdateMatchResult_Click;
      // 
      // label2
      // 
      label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      label2.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
      label2.Location = new System.Drawing.Point(3, 4);
      label2.Name = "label2";
      label2.Size = new System.Drawing.Size(375, 42);
      label2.TabIndex = 4;
      label2.Text = "Update match results";
      label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // curtainLogs
      // 
      curtainLogs.ForeColor = System.Drawing.Color.Gray;
      curtainLogs.IsOpen = false;
      curtainLogs.Location = new System.Drawing.Point(10, 410);
      curtainLogs.Name = "curtainLogs";
      curtainLogs.Size = new System.Drawing.Size(497, 297);
      curtainLogs.TabIndex = 19;
      // 
      // FormMain
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      AutoScroll = true;
      ClientSize = new System.Drawing.Size(1039, 741);
      Controls.Add(curtainLogs);
      Controls.Add(panel2);
      Controls.Add(panel1);
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
      menuMain.ResumeLayout(false);
      menuMain.PerformLayout();
      panel1.ResumeLayout(false);
      panel2.ResumeLayout(false);
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion
    private Controls.ScoreboardLiveControl scoreboardLiveControl1;
    private Controls.TPNetworkControl tpNetworkControl1;
    private Controls.TournamentTVControl tournamenttvControl;
    private System.Windows.Forms.MenuStrip menuMain;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem uploadTournamentToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog openTPFileDialog;
    private Controls.OnOffControl onOffCourtChanges;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label labelCourtListen;
    private CourtListView courtListView;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Label label2;
    private Controls.OnOffControl onOffUpdateMatchResult;
    private System.Windows.Forms.Label label3;
    private Controls.CurtainControl curtainLogs;
    private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem logLevelToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem warningsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem errorsToolStripMenuItem;
  }
}

