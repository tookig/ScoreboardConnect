
namespace ScoreboardConnectWinUI3 {
  partial class FormSettings {
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
      this.label1 = new System.Windows.Forms.Label();
      this.textURL = new System.Windows.Forms.TextBox();
      this.buttonOK = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.comboUnit = new System.Windows.Forms.ComboBox();
      this.comboTournament = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.buttonSetURL = new System.Windows.Forms.Button();
      this.labelDeviceStatus = new System.Windows.Forms.Label();
      this.buttonRegisterDevice = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 37);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(66, 15);
      this.label1.TabIndex = 0;
      this.label1.Text = "Server URL:";
      // 
      // textURL
      // 
      this.textURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textURL.Location = new System.Drawing.Point(107, 34);
      this.textURL.Name = "textURL";
      this.textURL.ReadOnly = true;
      this.textURL.Size = new System.Drawing.Size(279, 23);
      this.textURL.TabIndex = 1;
      // 
      // buttonOK
      // 
      this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonOK.Enabled = false;
      this.buttonOK.Location = new System.Drawing.Point(309, 233);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(126, 34);
      this.buttonOK.TabIndex = 2;
      this.buttonOK.Text = "OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancel.Location = new System.Drawing.Point(177, 233);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(126, 34);
      this.buttonCancel.TabIndex = 3;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 77);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(35, 15);
      this.label2.TabIndex = 4;
      this.label2.Text = "Club:";
      // 
      // comboUnit
      // 
      this.comboUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.comboUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboUnit.FormattingEnabled = true;
      this.comboUnit.Location = new System.Drawing.Point(108, 74);
      this.comboUnit.Name = "comboUnit";
      this.comboUnit.Size = new System.Drawing.Size(328, 23);
      this.comboUnit.TabIndex = 5;
      this.comboUnit.SelectedIndexChanged += new System.EventHandler(this.comboUnit_SelectedIndexChanged);
      // 
      // comboTournament
      // 
      this.comboTournament.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.comboTournament.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboTournament.FormattingEnabled = true;
      this.comboTournament.Location = new System.Drawing.Point(108, 151);
      this.comboTournament.Name = "comboTournament";
      this.comboTournament.Size = new System.Drawing.Size(328, 23);
      this.comboTournament.TabIndex = 7;
      this.comboTournament.SelectedIndexChanged += new System.EventHandler(this.comboTournament_SelectedIndexChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 154);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(74, 15);
      this.label3.TabIndex = 6;
      this.label3.Text = "Tournament:";
      // 
      // buttonSetURL
      // 
      this.buttonSetURL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonSetURL.Location = new System.Drawing.Point(393, 34);
      this.buttonSetURL.Name = "buttonSetURL";
      this.buttonSetURL.Size = new System.Drawing.Size(41, 23);
      this.buttonSetURL.TabIndex = 8;
      this.buttonSetURL.Text = "...";
      this.buttonSetURL.UseVisualStyleBackColor = true;
      this.buttonSetURL.Click += new System.EventHandler(this.buttonSetURL_Click);
      // 
      // labelDeviceStatus
      // 
      this.labelDeviceStatus.Location = new System.Drawing.Point(108, 100);
      this.labelDeviceStatus.Name = "labelDeviceStatus";
      this.labelDeviceStatus.Size = new System.Drawing.Size(222, 48);
      this.labelDeviceStatus.TabIndex = 10;
      this.labelDeviceStatus.Text = "device status";
      this.labelDeviceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // buttonRegisterDevice
      // 
      this.buttonRegisterDevice.Location = new System.Drawing.Point(342, 111);
      this.buttonRegisterDevice.Name = "buttonRegisterDevice";
      this.buttonRegisterDevice.Size = new System.Drawing.Size(93, 27);
      this.buttonRegisterDevice.TabIndex = 11;
      this.buttonRegisterDevice.Text = "Register";
      this.buttonRegisterDevice.UseVisualStyleBackColor = true;
      this.buttonRegisterDevice.Click += new System.EventHandler(this.buttonRegisterDevice_Click);
      // 
      // FormSettings
      // 
      this.AcceptButton = this.buttonOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.buttonCancel;
      this.ClientSize = new System.Drawing.Size(448, 283);
      this.ControlBox = false;
      this.Controls.Add(this.buttonRegisterDevice);
      this.Controls.Add(this.labelDeviceStatus);
      this.Controls.Add(this.buttonSetURL);
      this.Controls.Add(this.comboTournament);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.comboUnit);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonOK);
      this.Controls.Add(this.textURL);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "FormSettings";
      this.ShowInTaskbar = false;
      this.Text = "Settings";
      this.Load += new System.EventHandler(this.FormSettings_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textURL;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox comboUnit;
    private System.Windows.Forms.ComboBox comboTournament;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button buttonSetURL;
    private System.Windows.Forms.Label labelDeviceStatus;
    private System.Windows.Forms.Button buttonRegisterDevice;
  }
}