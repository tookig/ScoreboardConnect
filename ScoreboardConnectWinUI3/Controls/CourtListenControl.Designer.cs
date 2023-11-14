
namespace ScoreboardConnectWinUI3 {
  partial class CourtListenControl {
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
      this.tabPages = new System.Windows.Forms.TabControl();
      this.tabPageSelectTPFile = new System.Windows.Forms.TabPage();
      this.buttonCancelTPFile = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.buttonSelectTPFile = new System.Windows.Forms.Button();
      this.tabPageLoadScoreboardCourts = new System.Windows.Forms.TabPage();
      this.buttonCancelScoreboardCourts = new System.Windows.Forms.Button();
      this.labelLoadingScoreboardCourts = new System.Windows.Forms.Label();
      this.progressLoadCourtsFromServer = new System.Windows.Forms.ProgressBar();
      this.label3 = new System.Windows.Forms.Label();
      this.buttonLoadScoreboardCourts = new System.Windows.Forms.Button();
      this.tabPageSetupCourts = new System.Windows.Forms.TabPage();
      this.buttonCancelCourtSetup = new System.Windows.Forms.Button();
      this.buttonStartListening = new System.Windows.Forms.Button();
      this.courtList = new ScoreboardConnectWinUI3.CourtListView();
      this.label2 = new System.Windows.Forms.Label();
      this.tabPageListening = new System.Windows.Forms.TabPage();
      this.flowListener = new System.Windows.Forms.FlowLayoutPanel();
      this.labelListeningStatus = new System.Windows.Forms.Label();
      this.buttonListeningAction = new System.Windows.Forms.Button();
      this.openTPFile = new System.Windows.Forms.OpenFileDialog();
      this.tabPages.SuspendLayout();
      this.tabPageSelectTPFile.SuspendLayout();
      this.tabPageLoadScoreboardCourts.SuspendLayout();
      this.tabPageSetupCourts.SuspendLayout();
      this.tabPageListening.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabPages
      // 
      this.tabPages.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
      this.tabPages.Controls.Add(this.tabPageSelectTPFile);
      this.tabPages.Controls.Add(this.tabPageLoadScoreboardCourts);
      this.tabPages.Controls.Add(this.tabPageSetupCourts);
      this.tabPages.Controls.Add(this.tabPageListening);
      this.tabPages.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabPages.Location = new System.Drawing.Point(0, 0);
      this.tabPages.Name = "tabPages";
      this.tabPages.SelectedIndex = 0;
      this.tabPages.Size = new System.Drawing.Size(438, 412);
      this.tabPages.TabIndex = 0;
      this.tabPages.SelectedIndexChanged += new System.EventHandler(this.tabPages_SelectedIndexChanged);
      // 
      // tabPageSelectTPFile
      // 
      this.tabPageSelectTPFile.Controls.Add(this.buttonCancelTPFile);
      this.tabPageSelectTPFile.Controls.Add(this.label1);
      this.tabPageSelectTPFile.Controls.Add(this.buttonSelectTPFile);
      this.tabPageSelectTPFile.Location = new System.Drawing.Point(4, 27);
      this.tabPageSelectTPFile.Name = "tabPageSelectTPFile";
      this.tabPageSelectTPFile.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageSelectTPFile.Size = new System.Drawing.Size(430, 381);
      this.tabPageSelectTPFile.TabIndex = 0;
      this.tabPageSelectTPFile.Text = "Select TP-file";
      this.tabPageSelectTPFile.UseVisualStyleBackColor = true;
      // 
      // buttonCancelTPFile
      // 
      this.buttonCancelTPFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancelTPFile.Location = new System.Drawing.Point(117, 232);
      this.buttonCancelTPFile.Name = "buttonCancelTPFile";
      this.buttonCancelTPFile.Size = new System.Drawing.Size(189, 32);
      this.buttonCancelTPFile.TabIndex = 2;
      this.buttonCancelTPFile.Text = "Cancel";
      this.buttonCancelTPFile.UseVisualStyleBackColor = true;
      this.buttonCancelTPFile.Click += new System.EventHandler(this.buttonCancelTPFile_Click);
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.Location = new System.Drawing.Point(50, 75);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(335, 54);
      this.label1.TabIndex = 1;
      this.label1.Text = "Step 1: Load tournament data from TP-file.";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // buttonSelectTPFile
      // 
      this.buttonSelectTPFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonSelectTPFile.Location = new System.Drawing.Point(50, 140);
      this.buttonSelectTPFile.Name = "buttonSelectTPFile";
      this.buttonSelectTPFile.Size = new System.Drawing.Size(335, 78);
      this.buttonSelectTPFile.TabIndex = 0;
      this.buttonSelectTPFile.Text = "Select TP-file";
      this.buttonSelectTPFile.UseVisualStyleBackColor = true;
      this.buttonSelectTPFile.Click += new System.EventHandler(this.buttonSelectTPFile_Click);
      // 
      // tabPageLoadScoreboardCourts
      // 
      this.tabPageLoadScoreboardCourts.Controls.Add(this.buttonCancelScoreboardCourts);
      this.tabPageLoadScoreboardCourts.Controls.Add(this.labelLoadingScoreboardCourts);
      this.tabPageLoadScoreboardCourts.Controls.Add(this.progressLoadCourtsFromServer);
      this.tabPageLoadScoreboardCourts.Controls.Add(this.label3);
      this.tabPageLoadScoreboardCourts.Controls.Add(this.buttonLoadScoreboardCourts);
      this.tabPageLoadScoreboardCourts.Location = new System.Drawing.Point(4, 27);
      this.tabPageLoadScoreboardCourts.Name = "tabPageLoadScoreboardCourts";
      this.tabPageLoadScoreboardCourts.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageLoadScoreboardCourts.Size = new System.Drawing.Size(430, 381);
      this.tabPageLoadScoreboardCourts.TabIndex = 2;
      this.tabPageLoadScoreboardCourts.Text = "Scoreboard courts";
      this.tabPageLoadScoreboardCourts.UseVisualStyleBackColor = true;
      // 
      // buttonCancelScoreboardCourts
      // 
      this.buttonCancelScoreboardCourts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancelScoreboardCourts.Location = new System.Drawing.Point(118, 309);
      this.buttonCancelScoreboardCourts.Name = "buttonCancelScoreboardCourts";
      this.buttonCancelScoreboardCourts.Size = new System.Drawing.Size(189, 32);
      this.buttonCancelScoreboardCourts.TabIndex = 6;
      this.buttonCancelScoreboardCourts.Text = "Cancel";
      this.buttonCancelScoreboardCourts.UseVisualStyleBackColor = true;
      this.buttonCancelScoreboardCourts.Click += new System.EventHandler(this.buttonCancelScoreboardCourts_Click);
      // 
      // labelLoadingScoreboardCourts
      // 
      this.labelLoadingScoreboardCourts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.labelLoadingScoreboardCourts.Location = new System.Drawing.Point(48, 175);
      this.labelLoadingScoreboardCourts.Name = "labelLoadingScoreboardCourts";
      this.labelLoadingScoreboardCourts.Size = new System.Drawing.Size(338, 22);
      this.labelLoadingScoreboardCourts.TabIndex = 5;
      this.labelLoadingScoreboardCourts.Text = "Loading...";
      this.labelLoadingScoreboardCourts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // progressLoadCourtsFromServer
      // 
      this.progressLoadCourtsFromServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.progressLoadCourtsFromServer.Location = new System.Drawing.Point(48, 148);
      this.progressLoadCourtsFromServer.MarqueeAnimationSpeed = 50;
      this.progressLoadCourtsFromServer.Name = "progressLoadCourtsFromServer";
      this.progressLoadCourtsFromServer.Size = new System.Drawing.Size(338, 24);
      this.progressLoadCourtsFromServer.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.progressLoadCourtsFromServer.TabIndex = 4;
      // 
      // label3
      // 
      this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.label3.Location = new System.Drawing.Point(47, 81);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(339, 54);
      this.label3.TabIndex = 3;
      this.label3.Text = "Step 2: Load courts from the ScoreboardLive server.";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // buttonLoadScoreboardCourts
      // 
      this.buttonLoadScoreboardCourts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonLoadScoreboardCourts.Location = new System.Drawing.Point(47, 225);
      this.buttonLoadScoreboardCourts.Name = "buttonLoadScoreboardCourts";
      this.buttonLoadScoreboardCourts.Size = new System.Drawing.Size(339, 78);
      this.buttonLoadScoreboardCourts.TabIndex = 2;
      this.buttonLoadScoreboardCourts.Text = "Retry";
      this.buttonLoadScoreboardCourts.UseVisualStyleBackColor = true;
      this.buttonLoadScoreboardCourts.Click += new System.EventHandler(this.buttonLoadScoreboardCourts_Click);
      // 
      // tabPageSetupCourts
      // 
      this.tabPageSetupCourts.Controls.Add(this.buttonCancelCourtSetup);
      this.tabPageSetupCourts.Controls.Add(this.buttonStartListening);
      this.tabPageSetupCourts.Controls.Add(this.courtList);
      this.tabPageSetupCourts.Controls.Add(this.label2);
      this.tabPageSetupCourts.Location = new System.Drawing.Point(4, 27);
      this.tabPageSetupCourts.Name = "tabPageSetupCourts";
      this.tabPageSetupCourts.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageSetupCourts.Size = new System.Drawing.Size(430, 381);
      this.tabPageSetupCourts.TabIndex = 1;
      this.tabPageSetupCourts.Text = "Setup courts";
      this.tabPageSetupCourts.UseVisualStyleBackColor = true;
      // 
      // buttonCancelCourtSetup
      // 
      this.buttonCancelCourtSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonCancelCourtSetup.Location = new System.Drawing.Point(21, 330);
      this.buttonCancelCourtSetup.Name = "buttonCancelCourtSetup";
      this.buttonCancelCourtSetup.Size = new System.Drawing.Size(108, 39);
      this.buttonCancelCourtSetup.TabIndex = 5;
      this.buttonCancelCourtSetup.Text = "Cancel";
      this.buttonCancelCourtSetup.UseVisualStyleBackColor = true;
      this.buttonCancelCourtSetup.Click += new System.EventHandler(this.buttonCancelCourtSetup_Click);
      // 
      // buttonStartListening
      // 
      this.buttonStartListening.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonStartListening.Location = new System.Drawing.Point(285, 330);
      this.buttonStartListening.Name = "buttonStartListening";
      this.buttonStartListening.Size = new System.Drawing.Size(118, 39);
      this.buttonStartListening.TabIndex = 4;
      this.buttonStartListening.Text = "Start listening";
      this.buttonStartListening.UseVisualStyleBackColor = true;
      this.buttonStartListening.Click += new System.EventHandler(this.buttonStartListening_Click);
      // 
      // courtList
      // 
      this.courtList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.courtList.FullRowSelect = true;
      this.courtList.HideSelection = false;
      this.courtList.Location = new System.Drawing.Point(21, 111);
      this.courtList.Name = "courtList";
      this.courtList.Size = new System.Drawing.Size(382, 213);
      this.courtList.TabIndex = 3;
      this.courtList.UseCompatibleStateImageBehavior = false;
      this.courtList.View = System.Windows.Forms.View.Details;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.label2.Location = new System.Drawing.Point(21, 28);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(382, 80);
      this.label2.TabIndex = 2;
      this.label2.Text = "Step 3: For each ScoreboardLive court, select the corresponding TP-court.";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // tabPageListening
      // 
      this.tabPageListening.Controls.Add(this.flowListener);
      this.tabPageListening.Controls.Add(this.labelListeningStatus);
      this.tabPageListening.Controls.Add(this.buttonListeningAction);
      this.tabPageListening.Location = new System.Drawing.Point(4, 27);
      this.tabPageListening.Name = "tabPageListening";
      this.tabPageListening.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageListening.Size = new System.Drawing.Size(430, 381);
      this.tabPageListening.TabIndex = 3;
      this.tabPageListening.Text = "Listening...";
      this.tabPageListening.UseVisualStyleBackColor = true;
      // 
      // flowListener
      // 
      this.flowListener.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.flowListener.AutoScroll = true;
      this.flowListener.BackColor = System.Drawing.Color.White;
      this.flowListener.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
      this.flowListener.Location = new System.Drawing.Point(33, 57);
      this.flowListener.Name = "flowListener";
      this.flowListener.Size = new System.Drawing.Size(361, 257);
      this.flowListener.TabIndex = 2;
      this.flowListener.WrapContents = false;
      this.flowListener.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.flowListener_ControlAdded);
      // 
      // labelListeningStatus
      // 
      this.labelListeningStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelListeningStatus.Location = new System.Drawing.Point(6, 13);
      this.labelListeningStatus.Name = "labelListeningStatus";
      this.labelListeningStatus.Size = new System.Drawing.Size(418, 41);
      this.labelListeningStatus.TabIndex = 1;
      this.labelListeningStatus.Text = "status";
      this.labelListeningStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // buttonListeningAction
      // 
      this.buttonListeningAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonListeningAction.Location = new System.Drawing.Point(116, 320);
      this.buttonListeningAction.Name = "buttonListeningAction";
      this.buttonListeningAction.Size = new System.Drawing.Size(208, 44);
      this.buttonListeningAction.TabIndex = 0;
      this.buttonListeningAction.Text = "Stop listening";
      this.buttonListeningAction.UseVisualStyleBackColor = true;
      this.buttonListeningAction.Click += new System.EventHandler(this.buttonStopListening_Click);
      // 
      // openTPFile
      // 
      this.openTPFile.DefaultExt = "tp";
      this.openTPFile.Filter = "TP-files (*.tp)|*.tp|All files (*.*)|*.*";
      // 
      // CourtListenControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tabPages);
      this.Name = "CourtListenControl";
      this.Size = new System.Drawing.Size(438, 412);
      this.tabPages.ResumeLayout(false);
      this.tabPageSelectTPFile.ResumeLayout(false);
      this.tabPageLoadScoreboardCourts.ResumeLayout(false);
      this.tabPageSetupCourts.ResumeLayout(false);
      this.tabPageListening.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabPages;
    private System.Windows.Forms.TabPage tabPageSelectTPFile;
    private System.Windows.Forms.TabPage tabPageSetupCourts;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button buttonSelectTPFile;
    private System.Windows.Forms.Label label2;
    private CourtListView courtList;
    private System.Windows.Forms.Button buttonStartListening;
    private System.Windows.Forms.OpenFileDialog openTPFile;
    private System.Windows.Forms.TabPage tabPageLoadScoreboardCourts;
    private System.Windows.Forms.ProgressBar progressLoadCourtsFromServer;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button buttonLoadScoreboardCourts;
    private System.Windows.Forms.Label labelLoadingScoreboardCourts;
    private System.Windows.Forms.Button buttonCancelTPFile;
    private System.Windows.Forms.Button buttonCancelScoreboardCourts;
    private System.Windows.Forms.Button buttonCancelCourtSetup;
    private System.Windows.Forms.TabPage tabPageListening;
    private System.Windows.Forms.Button buttonListeningAction;
    private System.Windows.Forms.Label labelListeningStatus;
    private System.Windows.Forms.FlowLayoutPanel flowListener;
  }
}
