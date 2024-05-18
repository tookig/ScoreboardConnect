namespace ScoreboardConnectWinUI3.Forms {
  partial class FormUpload {
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      tournamentClassView1 = new TournamentClassView();
      label1 = new System.Windows.Forms.Label();
      buttonUpload = new System.Windows.Forms.Button();
      buttonCancel = new System.Windows.Forms.Button();
      progressBar = new System.Windows.Forms.ProgressBar();
      labelStatus = new System.Windows.Forms.Label();
      panelOptions = new System.Windows.Forms.Panel();
      label2 = new System.Windows.Forms.Label();
      checkCountry = new Controls.OnOffControl();
      panelOptions.SuspendLayout();
      SuspendLayout();
      // 
      // tournamentClassView1
      // 
      tournamentClassView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      tournamentClassView1.CheckBoxes = true;
      tournamentClassView1.FullRowSelect = true;
      tournamentClassView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
      tournamentClassView1.Location = new System.Drawing.Point(12, 58);
      tournamentClassView1.Name = "tournamentClassView1";
      tournamentClassView1.Size = new System.Drawing.Size(320, 221);
      tournamentClassView1.TabIndex = 0;
      tournamentClassView1.UseCompatibleStateImageBehavior = false;
      tournamentClassView1.View = System.Windows.Forms.View.Details;
      tournamentClassView1.ItemChecked += tournamentClassView1_ItemChecked;
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Location = new System.Drawing.Point(12, 40);
      label1.Name = "label1";
      label1.Size = new System.Drawing.Size(129, 15);
      label1.TabIndex = 1;
      label1.Text = "Select draws to upload:";
      // 
      // buttonUpload
      // 
      buttonUpload.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      buttonUpload.Enabled = false;
      buttonUpload.Location = new System.Drawing.Point(230, 392);
      buttonUpload.Name = "buttonUpload";
      buttonUpload.Size = new System.Drawing.Size(102, 36);
      buttonUpload.TabIndex = 2;
      buttonUpload.Text = "Upload";
      buttonUpload.UseVisualStyleBackColor = true;
      buttonUpload.Click += buttonUpload_Click;
      // 
      // buttonCancel
      // 
      buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      buttonCancel.Location = new System.Drawing.Point(122, 392);
      buttonCancel.Name = "buttonCancel";
      buttonCancel.Size = new System.Drawing.Size(102, 36);
      buttonCancel.TabIndex = 3;
      buttonCancel.Text = "Cancel";
      buttonCancel.UseVisualStyleBackColor = true;
      buttonCancel.Click += buttonCancel_Click;
      // 
      // progressBar
      // 
      progressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      progressBar.Location = new System.Drawing.Point(12, 352);
      progressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      progressBar.Name = "progressBar";
      progressBar.Size = new System.Drawing.Size(320, 33);
      progressBar.TabIndex = 4;
      progressBar.Visible = false;
      // 
      // labelStatus
      // 
      labelStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelStatus.Location = new System.Drawing.Point(12, 320);
      labelStatus.Name = "labelStatus";
      labelStatus.Size = new System.Drawing.Size(320, 28);
      labelStatus.TabIndex = 5;
      labelStatus.Text = "status";
      labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      labelStatus.Visible = false;
      // 
      // panelOptions
      // 
      panelOptions.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      panelOptions.Controls.Add(label2);
      panelOptions.Controls.Add(checkCountry);
      panelOptions.Location = new System.Drawing.Point(12, 286);
      panelOptions.Name = "panelOptions";
      panelOptions.Size = new System.Drawing.Size(320, 31);
      panelOptions.TabIndex = 6;
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      label2.Location = new System.Drawing.Point(3, 2);
      label2.Name = "label2";
      label2.Size = new System.Drawing.Size(188, 20);
      label2.TabIndex = 1;
      label2.Text = "Use country instead of club";
      // 
      // checkCountry
      // 
      checkCountry.BackColor = System.Drawing.Color.FromArgb(255, 100, 100);
      checkCountry.Checked = false;
      checkCountry.Location = new System.Drawing.Point(259, 2);
      checkCountry.Name = "checkCountry";
      checkCountry.OffText = "OFF";
      checkCountry.OnText = "ON";
      checkCountry.Size = new System.Drawing.Size(58, 26);
      checkCountry.TabIndex = 0;
      checkCountry.Text = "onOffControl1";
      // 
      // FormUpload
      // 
      AcceptButton = buttonUpload;
      AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      CancelButton = buttonCancel;
      ClientSize = new System.Drawing.Size(344, 440);
      ControlBox = false;
      Controls.Add(panelOptions);
      Controls.Add(labelStatus);
      Controls.Add(progressBar);
      Controls.Add(buttonCancel);
      Controls.Add(buttonUpload);
      Controls.Add(label1);
      Controls.Add(tournamentClassView1);
      FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      Name = "FormUpload";
      Text = "Upload Tournament";
      Load += FormUpload_Load;
      panelOptions.ResumeLayout(false);
      panelOptions.PerformLayout();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private TournamentClassView tournamentClassView1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button buttonUpload;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label labelStatus;
    private System.Windows.Forms.Panel panelOptions;
    private Controls.OnOffControl checkCountry;
    private System.Windows.Forms.Label label2;
  }
}