
namespace ScoreboardConnectWinUI3 {
  partial class ListenerUpdateControl {
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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.labelTeam1Score = new System.Windows.Forms.Label();
      this.labelTeam2Score = new System.Windows.Forms.Label();
      this.labelTeam1Names = new System.Windows.Forms.Label();
      this.labelTeam1Team = new System.Windows.Forms.Label();
      this.labelTeam2Names = new System.Windows.Forms.Label();
      this.labelTeam2Team = new System.Windows.Forms.Label();
      this.labelCourtname = new System.Windows.Forms.Label();
      this.labelTime = new System.Windows.Forms.Label();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.61386F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.38614F));
      this.tableLayoutPanel1.Controls.Add(this.labelTeam1Score, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.labelTeam2Score, 1, 3);
      this.tableLayoutPanel1.Controls.Add(this.labelTeam1Names, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.labelTeam1Team, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.labelTeam2Names, 0, 3);
      this.tableLayoutPanel1.Controls.Add(this.labelTeam2Team, 0, 4);
      this.tableLayoutPanel1.Controls.Add(this.labelCourtname, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.labelTime, 1, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 5;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 99);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // labelTeam1Score
      // 
      this.labelTeam1Score.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelTeam1Score.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
      this.labelTeam1Score.Location = new System.Drawing.Point(396, 20);
      this.labelTeam1Score.Name = "labelTeam1Score";
      this.tableLayoutPanel1.SetRowSpan(this.labelTeam1Score, 2);
      this.labelTeam1Score.Size = new System.Drawing.Size(101, 38);
      this.labelTeam1Score.TabIndex = 0;
      this.labelTeam1Score.Text = "XX";
      this.labelTeam1Score.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelTeam2Score
      // 
      this.labelTeam2Score.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelTeam2Score.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
      this.labelTeam2Score.Location = new System.Drawing.Point(396, 58);
      this.labelTeam2Score.Name = "labelTeam2Score";
      this.tableLayoutPanel1.SetRowSpan(this.labelTeam2Score, 2);
      this.labelTeam2Score.Size = new System.Drawing.Size(101, 41);
      this.labelTeam2Score.TabIndex = 1;
      this.labelTeam2Score.Text = "YY";
      this.labelTeam2Score.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelTeam1Names
      // 
      this.labelTeam1Names.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelTeam1Names.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
      this.labelTeam1Names.Location = new System.Drawing.Point(3, 20);
      this.labelTeam1Names.Name = "labelTeam1Names";
      this.labelTeam1Names.Size = new System.Drawing.Size(387, 19);
      this.labelTeam1Names.TabIndex = 2;
      this.labelTeam1Names.Text = "label1";
      this.labelTeam1Names.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
      // 
      // labelTeam1Team
      // 
      this.labelTeam1Team.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelTeam1Team.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
      this.labelTeam1Team.Location = new System.Drawing.Point(3, 39);
      this.labelTeam1Team.Name = "labelTeam1Team";
      this.labelTeam1Team.Size = new System.Drawing.Size(387, 19);
      this.labelTeam1Team.TabIndex = 3;
      this.labelTeam1Team.Text = "label2";
      // 
      // labelTeam2Names
      // 
      this.labelTeam2Names.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelTeam2Names.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
      this.labelTeam2Names.Location = new System.Drawing.Point(3, 58);
      this.labelTeam2Names.Name = "labelTeam2Names";
      this.labelTeam2Names.Size = new System.Drawing.Size(387, 19);
      this.labelTeam2Names.TabIndex = 4;
      this.labelTeam2Names.Text = "label3";
      this.labelTeam2Names.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
      // 
      // labelTeam2Team
      // 
      this.labelTeam2Team.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelTeam2Team.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
      this.labelTeam2Team.Location = new System.Drawing.Point(3, 77);
      this.labelTeam2Team.Name = "labelTeam2Team";
      this.labelTeam2Team.Size = new System.Drawing.Size(387, 22);
      this.labelTeam2Team.TabIndex = 5;
      this.labelTeam2Team.Text = "label4";
      // 
      // labelCourtname
      // 
      this.labelCourtname.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelCourtname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
      this.labelCourtname.ForeColor = System.Drawing.Color.RoyalBlue;
      this.labelCourtname.Location = new System.Drawing.Point(3, 0);
      this.labelCourtname.Name = "labelCourtname";
      this.labelCourtname.Size = new System.Drawing.Size(387, 20);
      this.labelCourtname.TabIndex = 6;
      this.labelCourtname.Text = "label1";
      this.labelCourtname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelTime
      // 
      this.labelTime.Location = new System.Drawing.Point(396, 0);
      this.labelTime.Name = "labelTime";
      this.labelTime.Size = new System.Drawing.Size(100, 20);
      this.labelTime.TabIndex = 7;
      this.labelTime.Text = "labelTime";
      this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ListenerUpdateControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "ListenerUpdateControl";
      this.Size = new System.Drawing.Size(500, 99);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label labelTeam1Score;
    private System.Windows.Forms.Label labelTeam2Score;
    private System.Windows.Forms.Label labelTeam1Names;
    private System.Windows.Forms.Label labelTeam1Team;
    private System.Windows.Forms.Label labelTeam2Names;
    private System.Windows.Forms.Label labelTeam2Team;
    private System.Windows.Forms.Label labelCourtname;
    private System.Windows.Forms.Label labelTime;
  }
}
