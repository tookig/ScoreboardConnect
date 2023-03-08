
namespace ScoreboardConnectWinUI3 {
  partial class ControlCourtListen {
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
      this.buttonAction = new System.Windows.Forms.Button();
      this.labelStatus = new System.Windows.Forms.Label();
      this.listCourts = new ScoreboardConnectWinUI3.CourtListView();
      this.openTPFile = new System.Windows.Forms.OpenFileDialog();
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.SuspendLayout();
      // 
      // buttonAction
      // 
      this.buttonAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonAction.Location = new System.Drawing.Point(334, 248);
      this.buttonAction.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.buttonAction.Name = "buttonAction";
      this.buttonAction.Size = new System.Drawing.Size(184, 55);
      this.buttonAction.TabIndex = 0;
      this.buttonAction.Text = "Start listening";
      this.buttonAction.UseVisualStyleBackColor = true;
      this.buttonAction.Click += new System.EventHandler(this.buttonAction_Click);
      // 
      // labelStatus
      // 
      this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelStatus.Location = new System.Drawing.Point(0, 251);
      this.labelStatus.Name = "labelStatus";
      this.labelStatus.Size = new System.Drawing.Size(326, 55);
      this.labelStatus.TabIndex = 1;
      this.labelStatus.Text = "label1";
      this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // listCourts
      // 
      this.listCourts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.listCourts.FullRowSelect = true;
      this.listCourts.GridLines = true;
      this.listCourts.HideSelection = false;
      this.listCourts.Location = new System.Drawing.Point(0, 13);
      this.listCourts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.listCourts.Name = "listCourts";
      this.listCourts.Size = new System.Drawing.Size(517, 187);
      this.listCourts.TabIndex = 2;
      this.listCourts.UseCompatibleStateImageBehavior = false;
      this.listCourts.View = System.Windows.Forms.View.Details;
      // 
      // openTPFile
      // 
      this.openTPFile.DefaultExt = "tp";
      this.openTPFile.Filter = "TP-files (*.tp)|*.tp|All files (*.*)|*.*";
      // 
      // progressBar
      // 
      this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar.Location = new System.Drawing.Point(0, 209);
      this.progressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.progressBar.MarqueeAnimationSpeed = 30;
      this.progressBar.Maximum = 1000;
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(518, 31);
      this.progressBar.Step = 1;
      this.progressBar.TabIndex = 3;
      // 
      // ControlCourtListen
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
      this.Controls.Add(this.progressBar);
      this.Controls.Add(this.listCourts);
      this.Controls.Add(this.labelStatus);
      this.Controls.Add(this.buttonAction);
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "ControlCourtListen";
      this.Size = new System.Drawing.Size(518, 309);
      this.Load += new System.EventHandler(this.ControlCourtListen_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buttonAction;
    private System.Windows.Forms.Label labelStatus;
    private CourtListView listCourts;
    private System.Windows.Forms.OpenFileDialog openTPFile;
    private System.Windows.Forms.ProgressBar progressBar;
  }
}
