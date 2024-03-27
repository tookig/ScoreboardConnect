
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
      buttonTPCourtListen = new System.Windows.Forms.Button();
      buttonImportTP = new System.Windows.Forms.Button();
      panelContent = new System.Windows.Forms.Panel();
      scoreboardLiveControl1 = new Controls.ScoreboardLiveControl();
      tpNetworkControl1 = new Controls.TPNetworkControl();
      tournamenttvControl = new Controls.TournamentTVControl();
      groupCourts = new System.Windows.Forms.GroupBox();
      menuMain = new System.Windows.Forms.MenuStrip();
      fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      courtListView = new CourtListView();
      panelContent.SuspendLayout();
      groupCourts.SuspendLayout();
      menuMain.SuspendLayout();
      SuspendLayout();
      // 
      // buttonTPCourtListen
      // 
      buttonTPCourtListen.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
      buttonTPCourtListen.Image = Resource1.calendar;
      buttonTPCourtListen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      buttonTPCourtListen.Location = new System.Drawing.Point(299, 3);
      buttonTPCourtListen.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      buttonTPCourtListen.Name = "buttonTPCourtListen";
      buttonTPCourtListen.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
      buttonTPCourtListen.Size = new System.Drawing.Size(234, 53);
      buttonTPCourtListen.TabIndex = 6;
      buttonTPCourtListen.Text = "Listen for TP court updates";
      buttonTPCourtListen.UseVisualStyleBackColor = true;
      // 
      // buttonImportTP
      // 
      buttonImportTP.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
      buttonImportTP.Enabled = false;
      buttonImportTP.Image = Resource1.copy;
      buttonImportTP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      buttonImportTP.Location = new System.Drawing.Point(5, 3);
      buttonImportTP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      buttonImportTP.Name = "buttonImportTP";
      buttonImportTP.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
      buttonImportTP.Size = new System.Drawing.Size(234, 53);
      buttonImportTP.TabIndex = 7;
      buttonImportTP.Text = "Upload TP tournament";
      buttonImportTP.UseVisualStyleBackColor = true;
      buttonImportTP.Click += buttonImportTP_Click;
      // 
      // panelContent
      // 
      panelContent.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      panelContent.Controls.Add(buttonImportTP);
      panelContent.Controls.Add(buttonTPCourtListen);
      panelContent.Location = new System.Drawing.Point(13, 720);
      panelContent.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      panelContent.Name = "panelContent";
      panelContent.Size = new System.Drawing.Size(538, 62);
      panelContent.TabIndex = 8;
      // 
      // scoreboardLiveControl1
      // 
      scoreboardLiveControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      scoreboardLiveControl1.Location = new System.Drawing.Point(11, 38);
      scoreboardLiveControl1.Name = "scoreboardLiveControl1";
      scoreboardLiveControl1.Size = new System.Drawing.Size(538, 131);
      scoreboardLiveControl1.TabIndex = 9;
      // 
      // tpNetworkControl1
      // 
      tpNetworkControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      tpNetworkControl1.Location = new System.Drawing.Point(10, 175);
      tpNetworkControl1.Name = "tpNetworkControl1";
      tpNetworkControl1.Size = new System.Drawing.Size(536, 123);
      tpNetworkControl1.TabIndex = 10;
      // 
      // tournamenttvControl
      // 
      tournamenttvControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      tournamenttvControl.Location = new System.Drawing.Point(15, 304);
      tournamenttvControl.Name = "tournamenttvControl";
      tournamenttvControl.Size = new System.Drawing.Size(531, 160);
      tournamenttvControl.TabIndex = 11;
      // 
      // groupCourts
      // 
      groupCourts.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      groupCourts.Controls.Add(courtListView);
      groupCourts.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
      groupCourts.Location = new System.Drawing.Point(10, 470);
      groupCourts.Name = "groupCourts";
      groupCourts.Size = new System.Drawing.Size(536, 244);
      groupCourts.TabIndex = 12;
      groupCourts.TabStop = false;
      groupCourts.Text = "Courts";
      // 
      // menuMain
      // 
      menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem });
      menuMain.Location = new System.Drawing.Point(0, 0);
      menuMain.Name = "menuMain";
      menuMain.Size = new System.Drawing.Size(562, 24);
      menuMain.TabIndex = 13;
      menuMain.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { exitToolStripMenuItem });
      fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      fileToolStripMenuItem.Text = "&File";
      // 
      // exitToolStripMenuItem
      // 
      exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      exitToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X;
      exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      exitToolStripMenuItem.Text = "E&xit";
      exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
      // 
      // courtListView
      // 
      courtListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
      courtListView.Dock = System.Windows.Forms.DockStyle.Fill;
      courtListView.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      courtListView.FullRowSelect = true;
      courtListView.Location = new System.Drawing.Point(3, 29);
      courtListView.Name = "courtListView";
      courtListView.Size = new System.Drawing.Size(530, 212);
      courtListView.TabIndex = 0;
      courtListView.UseCompatibleStateImageBehavior = false;
      courtListView.View = System.Windows.Forms.View.Details;
      // 
      // FormMain
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      AutoScroll = true;
      ClientSize = new System.Drawing.Size(562, 797);
      Controls.Add(groupCourts);
      Controls.Add(tournamenttvControl);
      Controls.Add(tpNetworkControl1);
      Controls.Add(scoreboardLiveControl1);
      Controls.Add(panelContent);
      Controls.Add(menuMain);
      Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      MainMenuStrip = menuMain;
      Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      Name = "FormMain";
      Text = "Scoreboard Connect";
      Load += FormMain_Load;
      Shown += FormMain_Shown;
      panelContent.ResumeLayout(false);
      groupCourts.ResumeLayout(false);
      menuMain.ResumeLayout(false);
      menuMain.PerformLayout();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion
    private System.Windows.Forms.Button buttonTPCourtListen;
    private System.Windows.Forms.Button buttonImportTP;
    private System.Windows.Forms.Panel panelContent;
    private Controls.ScoreboardLiveControl scoreboardLiveControl1;
    private Controls.TPNetworkControl tpNetworkControl1;
    private Controls.TournamentTVControl tournamenttvControl;
    private System.Windows.Forms.GroupBox groupCourts;
    private System.Windows.Forms.MenuStrip menuMain;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private CourtListView courtListView;
  }
}

