
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
      ((System.ComponentModel.ISupportInitialize)(this.pictureLoading)).BeginInit();
      this.SuspendLayout();
      // 
      // labelConnectionStatus
      // 
      this.labelConnectionStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelConnectionStatus.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
      this.labelConnectionStatus.Location = new System.Drawing.Point(12, 9);
      this.labelConnectionStatus.Name = "labelConnectionStatus";
      this.labelConnectionStatus.Size = new System.Drawing.Size(522, 48);
      this.labelConnectionStatus.TabIndex = 0;
      this.labelConnectionStatus.Text = "Scoreboard Connect";
      this.labelConnectionStatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      // 
      // labelSelectedUnit
      // 
      this.labelSelectedUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelSelectedUnit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
      this.labelSelectedUnit.Location = new System.Drawing.Point(12, 57);
      this.labelSelectedUnit.Name = "labelSelectedUnit";
      this.labelSelectedUnit.Size = new System.Drawing.Size(522, 21);
      this.labelSelectedUnit.TabIndex = 1;
      this.labelSelectedUnit.Text = "Scoreboard Connect";
      this.labelSelectedUnit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // pictureLoading
      // 
      this.pictureLoading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureLoading.Image = ((System.Drawing.Image)(resources.GetObject("pictureLoading.Image")));
      this.pictureLoading.Location = new System.Drawing.Point(12, 116);
      this.pictureLoading.Name = "pictureLoading";
      this.pictureLoading.Size = new System.Drawing.Size(522, 52);
      this.pictureLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureLoading.TabIndex = 2;
      this.pictureLoading.TabStop = false;
      // 
      // buttonExit
      // 
      this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonExit.Location = new System.Drawing.Point(428, 494);
      this.buttonExit.Name = "buttonExit";
      this.buttonExit.Size = new System.Drawing.Size(106, 35);
      this.buttonExit.TabIndex = 3;
      this.buttonExit.Text = "Exit";
      this.buttonExit.UseVisualStyleBackColor = true;
      this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
      // 
      // buttonSettings
      // 
      this.buttonSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonSettings.Location = new System.Drawing.Point(12, 494);
      this.buttonSettings.Name = "buttonSettings";
      this.buttonSettings.Size = new System.Drawing.Size(106, 35);
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
      this.labelTournament.Location = new System.Drawing.Point(12, 78);
      this.labelTournament.Name = "labelTournament";
      this.labelTournament.Size = new System.Drawing.Size(522, 21);
      this.labelTournament.TabIndex = 5;
      this.labelTournament.Text = "Scoreboard Connect";
      this.labelTournament.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // buttonTPCourtListen
      // 
      this.buttonTPCourtListen.Image = global::ScoreboardConnectWinUI3.Resource1.calendar;
      this.buttonTPCourtListen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.buttonTPCourtListen.Location = new System.Drawing.Point(13, 179);
      this.buttonTPCourtListen.Name = "buttonTPCourtListen";
      this.buttonTPCourtListen.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
      this.buttonTPCourtListen.Size = new System.Drawing.Size(256, 113);
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
      this.buttonImportTP.Location = new System.Drawing.Point(278, 179);
      this.buttonImportTP.Name = "buttonImportTP";
      this.buttonImportTP.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
      this.buttonImportTP.Size = new System.Drawing.Size(256, 113);
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
      this.panelContent.Location = new System.Drawing.Point(14, 179);
      this.panelContent.Name = "panelContent";
      this.panelContent.Size = new System.Drawing.Size(520, 309);
      this.panelContent.TabIndex = 8;
      // 
      // FormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(546, 541);
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
  }
}

