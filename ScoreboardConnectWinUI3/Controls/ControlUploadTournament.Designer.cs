
namespace ScoreboardConnectWinUI3 {
  partial class ControlUploadTournament {
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
      this.openTPFile = new System.Windows.Forms.OpenFileDialog();
      this.buttonAction = new System.Windows.Forms.Button();
      this.listClasses = new ScoreboardConnectWinUI3.TournamentClassView();
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.labelUploadStatus = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // openTPFile
      // 
      this.openTPFile.DefaultExt = "tp";
      this.openTPFile.Filter = "TP-files (*.tp)|*.tp|All files (*.*)|*.*";
      // 
      // buttonAction
      // 
      this.buttonAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonAction.Location = new System.Drawing.Point(272, 284);
      this.buttonAction.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.buttonAction.Name = "buttonAction";
      this.buttonAction.Size = new System.Drawing.Size(160, 59);
      this.buttonAction.TabIndex = 0;
      this.buttonAction.Text = "Load TP-file";
      this.buttonAction.UseVisualStyleBackColor = true;
      this.buttonAction.Click += new System.EventHandler(this.buttonAction_Click);
      // 
      // listClasses
      // 
      this.listClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.listClasses.CheckBoxes = true;
      this.listClasses.FullRowSelect = true;
      this.listClasses.GridLines = true;
      this.listClasses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
      this.listClasses.HideSelection = false;
      this.listClasses.Location = new System.Drawing.Point(0, 4);
      this.listClasses.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.listClasses.Name = "listClasses";
      this.listClasses.Size = new System.Drawing.Size(431, 260);
      this.listClasses.TabIndex = 1;
      this.listClasses.UseCompatibleStateImageBehavior = false;
      this.listClasses.View = System.Windows.Forms.View.Details;
      this.listClasses.Visible = false;
      // 
      // progressBar
      // 
      this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar.Location = new System.Drawing.Point(0, 309);
      this.progressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(265, 33);
      this.progressBar.TabIndex = 3;
      // 
      // labelUploadStatus
      // 
      this.labelUploadStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelUploadStatus.Location = new System.Drawing.Point(0, 269);
      this.labelUploadStatus.Name = "labelUploadStatus";
      this.labelUploadStatus.Size = new System.Drawing.Size(265, 32);
      this.labelUploadStatus.TabIndex = 4;
      this.labelUploadStatus.Text = "status";
      this.labelUploadStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ControlUploadTournament
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
      this.Controls.Add(this.labelUploadStatus);
      this.Controls.Add(this.progressBar);
      this.Controls.Add(this.listClasses);
      this.Controls.Add(this.buttonAction);
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "ControlUploadTournament";
      this.Size = new System.Drawing.Size(432, 343);
      this.Load += new System.EventHandler(this.ControlUploadTournament_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.OpenFileDialog openTPFile;
    private System.Windows.Forms.Button buttonAction;
    private TournamentClassView listClasses;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label labelUploadStatus;
  }
}
