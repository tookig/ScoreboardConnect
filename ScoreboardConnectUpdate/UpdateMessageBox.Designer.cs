
namespace ScoreboardConnectUpdate {
  partial class UpdateMessageBox {
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
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.buttonClose = new System.Windows.Forms.Button();
      this.labelCritical = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.Location = new System.Drawing.Point(12, 11);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(348, 50);
      this.label1.TabIndex = 0;
      this.label1.Text = "There is an update available for the Scoreboard Connect app. For instructions on " +
    "how to update, visit:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
      // 
      // linkLabel1
      // 
      this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.linkLabel1.Location = new System.Drawing.Point(12, 74);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(348, 23);
      this.linkLabel1.TabIndex = 1;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "https://www.scoreboardlive.se/docs/connect";
      this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      // 
      // buttonClose
      // 
      this.buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.buttonClose.Location = new System.Drawing.Point(134, 166);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(95, 36);
      this.buttonClose.TabIndex = 2;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // labelCritical
      // 
      this.labelCritical.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelCritical.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.labelCritical.Location = new System.Drawing.Point(12, 113);
      this.labelCritical.Name = "labelCritical";
      this.labelCritical.Size = new System.Drawing.Size(348, 34);
      this.labelCritical.TabIndex = 3;
      this.labelCritical.Text = "The application might not work as expected without this update!";
      this.labelCritical.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.labelCritical.Visible = false;
      // 
      // UpdateMessageBox
      // 
      this.AcceptButton = this.buttonClose;
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(372, 214);
      this.ControlBox = false;
      this.Controls.Add(this.labelCritical);
      this.Controls.Add(this.buttonClose);
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "UpdateMessageBox";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Update software";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.Label labelCritical;
  }
}