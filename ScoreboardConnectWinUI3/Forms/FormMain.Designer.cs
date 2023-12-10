
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
      buttonExit = new System.Windows.Forms.Button();
      buttonTPCourtListen = new System.Windows.Forms.Button();
      buttonImportTP = new System.Windows.Forms.Button();
      panelContent = new System.Windows.Forms.Panel();
      scoreboardLiveControl1 = new Controls.ScoreboardLiveControl();
      tpNetworkControl1 = new Controls.TPNetworkControl();
      panelContent.SuspendLayout();
      SuspendLayout();
      // 
      // buttonExit
      // 
      buttonExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      buttonExit.Location = new System.Drawing.Point(720, 589);
      buttonExit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      buttonExit.Name = "buttonExit";
      buttonExit.Size = new System.Drawing.Size(97, 38);
      buttonExit.TabIndex = 3;
      buttonExit.Text = "Exit";
      buttonExit.UseVisualStyleBackColor = true;
      buttonExit.Click += buttonExit_Click;
      // 
      // buttonTPCourtListen
      // 
      buttonTPCourtListen.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
      buttonTPCourtListen.Image = Resource1.calendar;
      buttonTPCourtListen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      buttonTPCourtListen.Location = new System.Drawing.Point(565, 3);
      buttonTPCourtListen.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      buttonTPCourtListen.Name = "buttonTPCourtListen";
      buttonTPCourtListen.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
      buttonTPCourtListen.Size = new System.Drawing.Size(234, 120);
      buttonTPCourtListen.TabIndex = 6;
      buttonTPCourtListen.Text = "Listen for TP court updates";
      buttonTPCourtListen.UseVisualStyleBackColor = true;
      // 
      // buttonImportTP
      // 
      buttonImportTP.Enabled = false;
      buttonImportTP.Image = Resource1.copy;
      buttonImportTP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      buttonImportTP.Location = new System.Drawing.Point(5, 3);
      buttonImportTP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      buttonImportTP.Name = "buttonImportTP";
      buttonImportTP.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
      buttonImportTP.Size = new System.Drawing.Size(234, 120);
      buttonImportTP.TabIndex = 7;
      buttonImportTP.Text = "Upload TP tournament";
      buttonImportTP.UseVisualStyleBackColor = true;
      buttonImportTP.Click += buttonImportTP_Click;
      // 
      // panelContent
      // 
      panelContent.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      panelContent.Controls.Add(buttonImportTP);
      panelContent.Controls.Add(buttonTPCourtListen);
      panelContent.Location = new System.Drawing.Point(13, 453);
      panelContent.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      panelContent.Name = "panelContent";
      panelContent.Size = new System.Drawing.Size(804, 129);
      panelContent.TabIndex = 8;
      // 
      // scoreboardLiveControl1
      // 
      scoreboardLiveControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      scoreboardLiveControl1.Location = new System.Drawing.Point(11, 12);
      scoreboardLiveControl1.Name = "scoreboardLiveControl1";
      scoreboardLiveControl1.Size = new System.Drawing.Size(804, 195);
      scoreboardLiveControl1.TabIndex = 9;
      // 
      // tpNetworkControl1
      // 
      tpNetworkControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      tpNetworkControl1.Location = new System.Drawing.Point(13, 213);
      tpNetworkControl1.Name = "tpNetworkControl1";
      tpNetworkControl1.Size = new System.Drawing.Size(802, 209);
      tpNetworkControl1.TabIndex = 10;
      // 
      // FormMain
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      ClientSize = new System.Drawing.Size(828, 639);
      Controls.Add(tpNetworkControl1);
      Controls.Add(scoreboardLiveControl1);
      Controls.Add(buttonExit);
      Controls.Add(panelContent);
      Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      Name = "FormMain";
      Text = "Scoreboard Connect";
      Load += FormMain_Load;
      Shown += FormMain_Shown;
      panelContent.ResumeLayout(false);
      ResumeLayout(false);
    }

    #endregion
    private System.Windows.Forms.Button buttonExit;
    private System.Windows.Forms.Button buttonTPCourtListen;
    private System.Windows.Forms.Button buttonImportTP;
    private System.Windows.Forms.Panel panelContent;
    private Controls.ScoreboardLiveControl scoreboardLiveControl1;
    private Controls.TPNetworkControl tpNetworkControl1;
  }
}

