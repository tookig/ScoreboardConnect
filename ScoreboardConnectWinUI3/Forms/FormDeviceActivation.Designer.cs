
namespace ScoreboardConnectWinUI3 {
  partial class FormDeviceActivation {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDeviceActivation));
      this.buttonCancel = new System.Windows.Forms.Button();
      this.buttonOK = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.textDeviceCode = new System.Windows.Forms.TextBox();
      this.pictureLoading = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureLoading)).BeginInit();
      this.SuspendLayout();
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancel.Location = new System.Drawing.Point(15, 163);
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
      this.buttonOK.Enabled = false;
      this.buttonOK.Location = new System.Drawing.Point(166, 163);
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
      this.label1.Location = new System.Drawing.Point(74, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(200, 20);
      this.label1.TabIndex = 6;
      this.label1.Text = "Enter a valid activation code:";
      // 
      // textDeviceCode
      // 
      this.textDeviceCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
      this.textDeviceCode.Location = new System.Drawing.Point(93, 55);
      this.textDeviceCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.textDeviceCode.MaxLength = 6;
      this.textDeviceCode.Name = "textDeviceCode";
      this.textDeviceCode.Size = new System.Drawing.Size(133, 27);
      this.textDeviceCode.TabIndex = 7;
      this.textDeviceCode.TextChanged += new System.EventHandler(this.textDeviceCode_TextChanged);
      // 
      // pictureLoading
      // 
      this.pictureLoading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureLoading.Image = ((System.Drawing.Image)(resources.GetObject("pictureLoading.Image")));
      this.pictureLoading.Location = new System.Drawing.Point(93, 93);
      this.pictureLoading.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.pictureLoading.Name = "pictureLoading";
      this.pictureLoading.Size = new System.Drawing.Size(134, 57);
      this.pictureLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureLoading.TabIndex = 10;
      this.pictureLoading.TabStop = false;
      this.pictureLoading.Visible = false;
      // 
      // FormDeviceActivation
      // 
      this.AcceptButton = this.buttonOK;
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
      this.CancelButton = this.buttonCancel;
      this.ClientSize = new System.Drawing.Size(323, 224);
      this.ControlBox = false;
      this.Controls.Add(this.pictureLoading);
      this.Controls.Add(this.textDeviceCode);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "FormDeviceActivation";
      this.ShowInTaskbar = false;
      this.Text = "Register Device";
      ((System.ComponentModel.ISupportInitialize)(this.pictureLoading)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textDeviceCode;
    private System.Windows.Forms.PictureBox pictureLoading;
  }
}