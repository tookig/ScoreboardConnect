namespace ScoreboardConnectWinUI3.Controls {
  partial class CurtainControl {
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
      label1 = new System.Windows.Forms.Label();
      buttonExpand = new System.Windows.Forms.Button();
      panelContent = new System.Windows.Forms.Panel();
      SuspendLayout();
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
      label1.Location = new System.Drawing.Point(12, 13);
      label1.Name = "label1";
      label1.Size = new System.Drawing.Size(61, 25);
      label1.TabIndex = 0;
      label1.Text = "label1";
      // 
      // buttonExpand
      // 
      buttonExpand.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
      buttonExpand.FlatAppearance.BorderSize = 0;
      buttonExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      buttonExpand.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      buttonExpand.Location = new System.Drawing.Point(420, 1);
      buttonExpand.Margin = new System.Windows.Forms.Padding(0);
      buttonExpand.Name = "buttonExpand";
      buttonExpand.Size = new System.Drawing.Size(56, 45);
      buttonExpand.TabIndex = 1;
      buttonExpand.Text = "+";
      buttonExpand.UseVisualStyleBackColor = true;
      // 
      // panelContent
      // 
      panelContent.Location = new System.Drawing.Point(12, 51);
      panelContent.Name = "panelContent";
      panelContent.Size = new System.Drawing.Size(449, 247);
      panelContent.TabIndex = 2;
      // 
      // CurtainControl
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      Controls.Add(panelContent);
      Controls.Add(buttonExpand);
      Controls.Add(label1);
      Name = "CurtainControl";
      Size = new System.Drawing.Size(476, 314);
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button buttonExpand;
    private System.Windows.Forms.Panel panelContent;
  }
}
