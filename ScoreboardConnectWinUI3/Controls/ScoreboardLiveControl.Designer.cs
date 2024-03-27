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
      groupBox1 = new System.Windows.Forms.GroupBox();
      buttonSettings = new System.Windows.Forms.Button();
      labelStatus = new System.Windows.Forms.Label();
      pictureLoading = new System.Windows.Forms.PictureBox();
      labelTournament = new System.Windows.Forms.Label();
      labelUnit = new System.Windows.Forms.Label();
      groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)pictureLoading).BeginInit();
      SuspendLayout();
      // 
      // groupBox1
      // 
      groupBox1.Controls.Add(buttonSettings);
      groupBox1.Controls.Add(labelStatus);
      groupBox1.Controls.Add(pictureLoading);
      groupBox1.Controls.Add(labelTournament);
      groupBox1.Controls.Add(labelUnit);
      groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      groupBox1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
      groupBox1.ForeColor = System.Drawing.Color.SteelBlue;
      groupBox1.Location = new System.Drawing.Point(0, 0);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new System.Drawing.Size(558, 138);
      groupBox1.TabIndex = 0;
      groupBox1.TabStop = false;
      groupBox1.Text = "Scoreboard Live";
      // 
      // buttonSettings
      // 
      buttonSettings.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      buttonSettings.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      buttonSettings.ForeColor = System.Drawing.Color.Black;
      buttonSettings.Location = new System.Drawing.Point(391, 94);
      buttonSettings.Name = "buttonSettings";
      buttonSettings.Size = new System.Drawing.Size(161, 38);
      buttonSettings.TabIndex = 3;
      buttonSettings.Text = "Settings";
      buttonSettings.UseVisualStyleBackColor = true;
      buttonSettings.Click += buttonSettings_Click;
      // 
      // labelStatus
      // 
      labelStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
      labelStatus.AutoSize = true;
      labelStatus.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelStatus.ForeColor = System.Drawing.Color.Red;
      labelStatus.Location = new System.Drawing.Point(416, 0);
      labelStatus.Name = "labelStatus";
      labelStatus.Size = new System.Drawing.Size(136, 25);
      labelStatus.TabIndex = 0;
      labelStatus.Text = "Not connected";
      labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // pictureLoading
      // 
      pictureLoading.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      pictureLoading.Image = (System.Drawing.Image)resources.GetObject("pictureLoading.Image");
      pictureLoading.Location = new System.Drawing.Point(9, 36);
      pictureLoading.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      pictureLoading.Name = "pictureLoading";
      pictureLoading.Size = new System.Drawing.Size(543, 55);
      pictureLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      pictureLoading.TabIndex = 3;
      pictureLoading.TabStop = false;
      // 
      // labelTournament
      // 
      labelTournament.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelTournament.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelTournament.ForeColor = System.Drawing.Color.SeaGreen;
      labelTournament.Location = new System.Drawing.Point(6, 59);
      labelTournament.Name = "labelTournament";
      labelTournament.Size = new System.Drawing.Size(546, 32);
      labelTournament.TabIndex = 2;
      labelTournament.Text = "Not connected";
      labelTournament.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelUnit
      // 
      labelUnit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelUnit.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      labelUnit.ForeColor = System.Drawing.Color.Black;
      labelUnit.Location = new System.Drawing.Point(6, 27);
      labelUnit.Name = "labelUnit";
      labelUnit.Size = new System.Drawing.Size(546, 32);
      labelUnit.TabIndex = 1;
      labelUnit.Text = "Not connected";
      labelUnit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ScoreboardLiveControl
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      Controls.Add(groupBox1);
      Name = "ScoreboardLiveControl";
      Size = new System.Drawing.Size(558, 138);
      Load += ScoreboardLiveControl_Load;
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)pictureLoading).EndInit();
      ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label labelStatus;
    private System.Windows.Forms.Label labelTournament;
    private System.Windows.Forms.Label labelUnit;
    private System.Windows.Forms.Button buttonSettings;
    private System.Windows.Forms.PictureBox pictureLoading;
  }
}
