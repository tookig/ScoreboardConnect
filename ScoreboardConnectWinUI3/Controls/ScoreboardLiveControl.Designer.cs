namespace ScoreboardConnectWinUI3.Controls {
  partial class ScoreboardLiveControl {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScoreboardLiveControl));
      labelStatus = new System.Windows.Forms.Label();
      pictureLoading = new System.Windows.Forms.PictureBox();
      labelTournament = new System.Windows.Forms.Label();
      labelUnit = new System.Windows.Forms.Label();
      labelHeading = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)pictureLoading).BeginInit();
      SuspendLayout();
      // 
      // labelStatus
      // 
      labelStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
      labelStatus.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelStatus.ForeColor = System.Drawing.Color.Red;
      labelStatus.Location = new System.Drawing.Point(343, 14);
      labelStatus.Name = "labelStatus";
      labelStatus.Size = new System.Drawing.Size(250, 25);
      labelStatus.TabIndex = 0;
      labelStatus.Text = "Not connected";
      labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // pictureLoading
      // 
      pictureLoading.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      pictureLoading.Image = (System.Drawing.Image)resources.GetObject("pictureLoading.Image");
      pictureLoading.Location = new System.Drawing.Point(24, 48);
      pictureLoading.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      pictureLoading.Name = "pictureLoading";
      pictureLoading.Size = new System.Drawing.Size(548, 55);
      pictureLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      pictureLoading.TabIndex = 3;
      pictureLoading.TabStop = false;
      // 
      // labelTournament
      // 
      labelTournament.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelTournament.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelTournament.ForeColor = System.Drawing.Color.Black;
      labelTournament.Location = new System.Drawing.Point(15, 39);
      labelTournament.Name = "labelTournament";
      labelTournament.Size = new System.Drawing.Size(578, 32);
      labelTournament.TabIndex = 2;
      labelTournament.Text = "Not connected";
      labelTournament.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelUnit
      // 
      labelUnit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelUnit.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelUnit.ForeColor = System.Drawing.Color.Black;
      labelUnit.Location = new System.Drawing.Point(15, 71);
      labelUnit.Name = "labelUnit";
      labelUnit.Size = new System.Drawing.Size(578, 32);
      labelUnit.TabIndex = 1;
      labelUnit.Text = "Not connected";
      labelUnit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelHeading
      // 
      labelHeading.AutoSize = true;
      labelHeading.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
      labelHeading.ForeColor = System.Drawing.Color.SteelBlue;
      labelHeading.Location = new System.Drawing.Point(15, 14);
      labelHeading.Name = "labelHeading";
      labelHeading.Size = new System.Drawing.Size(143, 25);
      labelHeading.TabIndex = 4;
      labelHeading.Text = "ScoreboardLive";
      // 
      // ScoreboardLiveControl
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      BackColor = System.Drawing.Color.White;
      Controls.Add(labelHeading);
      Controls.Add(labelStatus);
      Controls.Add(labelTournament);
      Controls.Add(labelUnit);
      Controls.Add(pictureLoading);
      Name = "ScoreboardLiveControl";
      Size = new System.Drawing.Size(596, 122);
      Load += ScoreboardLiveControl_Load;
      ((System.ComponentModel.ISupportInitialize)pictureLoading).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion
    private System.Windows.Forms.Label labelStatus;
    private System.Windows.Forms.Label labelTournament;
    private System.Windows.Forms.Label labelUnit;
    private System.Windows.Forms.PictureBox pictureLoading;
    private System.Windows.Forms.Label labelHeading;
  }
}
