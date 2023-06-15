
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
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
      this.labelConnectionStatus = new System.Windows.Forms.Label();
      this.labelSelectedUnit = new System.Windows.Forms.Label();
      this.pictureLoading = new System.Windows.Forms.PictureBox();
      this.buttonExit = new System.Windows.Forms.Button();
      this.buttonSettings = new System.Windows.Forms.Button();
      this.labelTournament = new System.Windows.Forms.Label();
      this.buttonTPCourtListen = new System.Windows.Forms.Button();
      this.buttonImportTP = new System.Windows.Forms.Button();
      this.panelContent = new System.Windows.Forms.Panel();
      this.buttonRetryConnection = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.pictureLoading)).BeginInit();
      this.SuspendLayout();
      // 
      // labelConnectionStatus
      // 
      this.labelConnectionStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelConnectionStatus.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
      this.labelConnectionStatus.Location = new System.Drawing.Point(11, 10);
      this.labelConnectionStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.labelConnectionStatus.Name = "labelConnectionStatus";
      this.labelConnectionStatus.Size = new System.Drawing.Size(478, 51);
      this.labelConnectionStatus.TabIndex = 0;
      this.labelConnectionStatus.Text = "Scoreboard Connect";
      this.labelConnectionStatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      // 
      // labelSelectedUnit
      // 
      this.labelSelectedUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelSelectedUnit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
      this.labelSelectedUnit.Location = new System.Drawing.Point(11, 61);
      this.labelSelectedUnit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.labelSelectedUnit.Name = "labelSelectedUnit";
      this.labelSelectedUnit.Size = new System.Drawing.Size(478, 22);
      this.labelSelectedUnit.TabIndex = 1;
      this.labelSelectedUnit.Text = "Scoreboard Connect";
      this.labelSelectedUnit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // pictureLoading
      // 
      this.pictureLoading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureLoading.Image = ((System.Drawing.Image)(resources.GetObject("pictureLoading.Image")));
      this.pictureLoading.Location = new System.Drawing.Point(11, 124);
      this.pictureLoading.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.pictureLoading.Name = "pictureLoading";
      this.pictureLoading.Size = new System.Drawing.Size(478, 55);
      this.pictureLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureLoading.TabIndex = 2;
      this.pictureLoading.TabStop = false;
      // 
      // buttonExit
      // 
      this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonExit.Location = new System.Drawing.Point(391, 527);
      this.buttonExit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.buttonExit.Name = "buttonExit";
      this.buttonExit.Size = new System.Drawing.Size(97, 38);
      this.buttonExit.TabIndex = 3;
      this.buttonExit.Text = "Exit";
      this.buttonExit.UseVisualStyleBackColor = true;
      this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
      // 
      // buttonSettings
      // 
      this.buttonSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonSettings.Location = new System.Drawing.Point(11, 527);
      this.buttonSettings.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.buttonSettings.Name = "buttonSettings";
      this.buttonSettings.Size = new System.Drawing.Size(97, 38);
      this.buttonSettings.TabIndex = 4;
      this.buttonSettings.Text = "Settings";
      this.buttonSettings.UseVisualStyleBackColor = true;
      this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
      // 
      // labelTournament
      // 
      this.labelTournament.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelTournament.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
      this.labelTournament.ForeColor = System.Drawing.Color.RoyalBlue;
      this.labelTournament.Location = new System.Drawing.Point(11, 83);
      this.labelTournament.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.labelTournament.Name = "labelTournament";
      this.labelTournament.Size = new System.Drawing.Size(478, 22);
      this.labelTournament.TabIndex = 5;
      this.labelTournament.Text = "Scoreboard Connect";
      this.labelTournament.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // buttonTPCourtListen
      // 
      this.buttonTPCourtListen.Image = global::ScoreboardConnectWinUI3.Resource1.calendar;
      this.buttonTPCourtListen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.buttonTPCourtListen.Location = new System.Drawing.Point(12, 191);
      this.buttonTPCourtListen.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.buttonTPCourtListen.Name = "buttonTPCourtListen";
      this.buttonTPCourtListen.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
      this.buttonTPCourtListen.Size = new System.Drawing.Size(234, 121);
      this.buttonTPCourtListen.TabIndex = 6;
      this.buttonTPCourtListen.Text = "Listen for TP court updates";
      this.buttonTPCourtListen.UseVisualStyleBackColor = true;
      this.buttonTPCourtListen.Click += new System.EventHandler(this.buttonTPCourtListen_Click);
      // 
      // buttonImportTP
      // 
      this.buttonImportTP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonImportTP.Image = global::ScoreboardConnectWinUI3.Resource1.copy;
      this.buttonImportTP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.buttonImportTP.Location = new System.Drawing.Point(254, 191);
      this.buttonImportTP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.buttonImportTP.Name = "buttonImportTP";
      this.buttonImportTP.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
      this.buttonImportTP.Size = new System.Drawing.Size(234, 121);
      this.buttonImportTP.TabIndex = 7;
      this.buttonImportTP.Text = "Upload TP tournament";
      this.buttonImportTP.UseVisualStyleBackColor = true;
      this.buttonImportTP.Click += new System.EventHandler(this.buttonImportTP_Click);
      // 
      // panelContent
      // 
      this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panelContent.Location = new System.Drawing.Point(13, 191);
      this.panelContent.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.panelContent.Name = "panelContent";
      this.panelContent.Size = new System.Drawing.Size(475, 330);
      this.panelContent.TabIndex = 8;
      // 
      // buttonRetryConnection
      // 
      this.buttonRetryConnection.ForeColor = System.Drawing.Color.Green;
      this.buttonRetryConnection.Location = new System.Drawing.Point(191, 124);
      this.buttonRetryConnection.Name = "buttonRetryConnection";
      this.buttonRetryConnection.Size = new System.Drawing.Size(115, 55);
      this.buttonRetryConnection.TabIndex = 9;
      this.buttonRetryConnection.Text = "Retry";
      this.buttonRetryConnection.UseVisualStyleBackColor = true;
      this.buttonRetryConnection.Visible = false;
      this.buttonRetryConnection.Click += new System.EventHandler(this.buttonRetryConnection_Click);
      // 
      // FormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(499, 577);
      this.Controls.Add(this.buttonRetryConnection);
      this.Controls.Add(this.buttonImportTP);
      this.Controls.Add(this.buttonTPCourtListen);
      this.Controls.Add(this.labelTournament);
      this.Controls.Add(this.buttonSettings);
      this.Controls.Add(this.buttonExit);
      this.Controls.Add(this.pictureLoading);
      this.Controls.Add(this.labelSelectedUnit);
      this.Controls.Add(this.labelConnectionStatus);
      this.Controls.Add(this.panelContent);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.Name = "FormMain";
      this.Text = "Scoreboard Connect";
      this.Load += new System.EventHandler(this.FormMain_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureLoading)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label labelConnectionStatus;
    private System.Windows.Forms.Label labelSelectedUnit;
    private System.Windows.Forms.PictureBox pictureLoading;
    private System.Windows.Forms.Button buttonExit;
    private System.Windows.Forms.Button buttonSettings;
    private System.Windows.Forms.Label labelTournament;
    private System.Windows.Forms.Button buttonTPCourtListen;
    private System.Windows.Forms.Button buttonImportTP;
    private System.Windows.Forms.Panel panelContent;
    private System.Windows.Forms.Button buttonRetryConnection;
  }
}

