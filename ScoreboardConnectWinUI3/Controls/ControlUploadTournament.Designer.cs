
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
      openTPFile = new System.Windows.Forms.OpenFileDialog();
      buttonAction = new System.Windows.Forms.Button();
      listClasses = new TournamentClassView();
      progressBar = new System.Windows.Forms.ProgressBar();
      labelUploadStatus = new System.Windows.Forms.Label();
      buttonCancel = new System.Windows.Forms.Button();
      SuspendLayout();
      // 
      // openTPFile
      // 
      openTPFile.DefaultExt = "tp";
      openTPFile.Filter = "TP-files (*.tp)|*.tp|All files (*.*)|*.*";
      // 
      // buttonAction
      // 
      buttonAction.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      buttonAction.Location = new System.Drawing.Point(272, 370);
      buttonAction.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      buttonAction.Name = "buttonAction";
      buttonAction.Size = new System.Drawing.Size(160, 48);
      buttonAction.TabIndex = 0;
      buttonAction.Text = "Load TP-file";
      buttonAction.UseVisualStyleBackColor = true;
      buttonAction.Click += buttonAction_Click;
      // 
      // listClasses
      // 
      listClasses.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      listClasses.CheckBoxes = true;
      listClasses.FullRowSelect = true;
      listClasses.GridLines = true;
      listClasses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
      listClasses.Location = new System.Drawing.Point(0, 4);
      listClasses.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      listClasses.Name = "listClasses";
      listClasses.Size = new System.Drawing.Size(431, 320);
      listClasses.TabIndex = 1;
      listClasses.UseCompatibleStateImageBehavior = false;
      listClasses.View = System.Windows.Forms.View.Details;
      listClasses.Visible = false;
      // 
      // progressBar
      // 
      progressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      progressBar.Location = new System.Drawing.Point(0, 384);
      progressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      progressBar.Name = "progressBar";
      progressBar.Size = new System.Drawing.Size(265, 33);
      progressBar.TabIndex = 3;
      // 
      // labelUploadStatus
      // 
      labelUploadStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelUploadStatus.Location = new System.Drawing.Point(0, 344);
      labelUploadStatus.Name = "labelUploadStatus";
      labelUploadStatus.Size = new System.Drawing.Size(265, 32);
      labelUploadStatus.TabIndex = 4;
      labelUploadStatus.Text = "status";
      labelUploadStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // buttonCancel
      // 
      buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      buttonCancel.Location = new System.Drawing.Point(272, 331);
      buttonCancel.Name = "buttonCancel";
      buttonCancel.Size = new System.Drawing.Size(157, 32);
      buttonCancel.TabIndex = 5;
      buttonCancel.Text = "Cancel";
      buttonCancel.UseVisualStyleBackColor = true;
      buttonCancel.Click += buttonCancel_Click;
      // 
      // ControlUploadTournament
      // 
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
      Controls.Add(buttonCancel);
      Controls.Add(labelUploadStatus);
      Controls.Add(progressBar);
      Controls.Add(listClasses);
      Controls.Add(buttonAction);
      Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      Name = "ControlUploadTournament";
      Size = new System.Drawing.Size(432, 418);
      Load += ControlUploadTournament_Load;
      ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.OpenFileDialog openTPFile;
    private System.Windows.Forms.Button buttonAction;
    private TournamentClassView listClasses;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label labelUploadStatus;
    private System.Windows.Forms.Button buttonCancel;
  }
}
