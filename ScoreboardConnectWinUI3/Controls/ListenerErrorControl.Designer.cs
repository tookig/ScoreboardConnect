
namespace ScoreboardConnectWinUI3 {
  partial class ListenerErrorControl {
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
      this.labelHeader = new System.Windows.Forms.Label();
      this.labelMessage = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // labelHeader
      // 
      this.labelHeader.AutoSize = true;
      this.labelHeader.ForeColor = System.Drawing.Color.Red;
      this.labelHeader.Location = new System.Drawing.Point(15, 6);
      this.labelHeader.Name = "labelHeader";
      this.labelHeader.Size = new System.Drawing.Size(38, 15);
      this.labelHeader.TabIndex = 0;
      this.labelHeader.Text = "label1";
      // 
      // labelMessage
      // 
      this.labelMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelMessage.AutoSize = true;
      this.labelMessage.Location = new System.Drawing.Point(15, 32);
      this.labelMessage.Name = "labelMessage";
      this.labelMessage.Size = new System.Drawing.Size(38, 15);
      this.labelMessage.TabIndex = 1;
      this.labelMessage.Text = "label2";
      // 
      // ListenerErrorControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.Controls.Add(this.labelMessage);
      this.Controls.Add(this.labelHeader);
      this.Name = "ListenerErrorControl";
      this.Size = new System.Drawing.Size(392, 57);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label labelHeader;
    private System.Windows.Forms.Label labelMessage;
  }
}
