
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
      label1 = new System.Windows.Forms.Label();
      linkLabel1 = new System.Windows.Forms.LinkLabel();
      buttonClose = new System.Windows.Forms.Button();
      labelCritical = new System.Windows.Forms.Label();
      SuspendLayout();
      // 
      // label1
      // 
      label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      label1.Location = new System.Drawing.Point(12, 11);
      label1.Name = "label1";
      label1.Size = new System.Drawing.Size(348, 50);
      label1.TabIndex = 0;
      label1.Text = "There is an update available for the Scoreboard Connect app. For instructions on how to update, visit:";
      label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
      // 
      // linkLabel1
      // 
      linkLabel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      linkLabel1.Location = new System.Drawing.Point(12, 74);
      linkLabel1.Name = "linkLabel1";
      linkLabel1.Size = new System.Drawing.Size(348, 23);
      linkLabel1.TabIndex = 1;
      linkLabel1.TabStop = true;
      linkLabel1.Text = "https://www.scoreboardlive.se/docs/connect-download";
      linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      linkLabel1.LinkClicked += linkLabel1_LinkClicked;
      // 
      // buttonClose
      // 
      buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      buttonClose.Location = new System.Drawing.Point(134, 166);
      buttonClose.Name = "buttonClose";
      buttonClose.Size = new System.Drawing.Size(95, 36);
      buttonClose.TabIndex = 2;
      buttonClose.Text = "Close";
      buttonClose.UseVisualStyleBackColor = true;
      buttonClose.Click += buttonClose_Click;
      // 
      // labelCritical
      // 
      labelCritical.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      labelCritical.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
      labelCritical.Location = new System.Drawing.Point(12, 113);
      labelCritical.Name = "labelCritical";
      labelCritical.Size = new System.Drawing.Size(348, 34);
      labelCritical.TabIndex = 3;
      labelCritical.Text = "The application might not work as expected without this update!";
      labelCritical.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      labelCritical.Visible = false;
      // 
      // UpdateMessageBox
      // 
      AcceptButton = buttonClose;
      AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      ClientSize = new System.Drawing.Size(372, 214);
      ControlBox = false;
      Controls.Add(labelCritical);
      Controls.Add(buttonClose);
      Controls.Add(linkLabel1);
      Controls.Add(label1);
      FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      Name = "UpdateMessageBox";
      StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      Text = "Update software";
      ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.Label labelCritical;
  }
}