﻿
namespace ScoreboardConnectWinUI3 {
  partial class FormURL {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormURL));
      this.buttonCancel = new System.Windows.Forms.Button();
      this.buttonOK = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.textURL = new System.Windows.Forms.TextBox();
      this.labelStatus = new System.Windows.Forms.Label();
      this.pictureLoading = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureLoading)).BeginInit();
      this.SuspendLayout();
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancel.Location = new System.Drawing.Point(137, 200);
      this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(144, 45);
      this.buttonCancel.TabIndex = 5;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      // 
      // buttonOK
      // 
      this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonOK.Location = new System.Drawing.Point(288, 200);
      this.buttonOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(144, 45);
      this.buttonOK.TabIndex = 4;
      this.buttonOK.Text = "OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(14, 32);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(119, 20);
      this.label1.TabIndex = 6;
      this.label1.Text = "Enter server URL:";
      // 
      // textURL
      // 
      this.textURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textURL.Location = new System.Drawing.Point(14, 69);
      this.textURL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.textURL.Name = "textURL";
      this.textURL.Size = new System.Drawing.Size(418, 27);
      this.textURL.TabIndex = 7;
      this.textURL.TextChanged += new System.EventHandler(this.textURL_TextChanged);
      // 
      // labelStatus
      // 
      this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelStatus.ForeColor = System.Drawing.Color.Red;
      this.labelStatus.Location = new System.Drawing.Point(14, 108);
      this.labelStatus.Name = "labelStatus";
      this.labelStatus.Size = new System.Drawing.Size(417, 75);
      this.labelStatus.TabIndex = 8;
      this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // pictureLoading
      // 
      this.pictureLoading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureLoading.Image = ((System.Drawing.Image)(resources.GetObject("pictureLoading.Image")));
      this.pictureLoading.Location = new System.Drawing.Point(14, 188);
      this.pictureLoading.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.pictureLoading.Name = "pictureLoading";
      this.pictureLoading.Size = new System.Drawing.Size(117, 57);
      this.pictureLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureLoading.TabIndex = 9;
      this.pictureLoading.TabStop = false;
      this.pictureLoading.Visible = false;
      // 
      // FormURL
      // 
      this.AcceptButton = this.buttonOK;
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
      this.CancelButton = this.buttonCancel;
      this.ClientSize = new System.Drawing.Size(446, 261);
      this.ControlBox = false;
      this.Controls.Add(this.pictureLoading);
      this.Controls.Add(this.labelStatus);
      this.Controls.Add(this.textURL);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "FormURL";
      this.ShowInTaskbar = false;
      this.Text = "Server URL";
      ((System.ComponentModel.ISupportInitialize)(this.pictureLoading)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textURL;
    private System.Windows.Forms.Label labelStatus;
    private System.Windows.Forms.PictureBox pictureLoading;
  }
}