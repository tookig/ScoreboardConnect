using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreboardConnectWinUI3.Controls {
  public class OnOffControl : Control {
    private bool isChecked = false;
    private Color onColor = Color.FromArgb(76, 175, 80);
    private Color offColor = Color.FromArgb(255, 100, 100);
    private Color disabledColor = Color.FromArgb(200, 200, 200);
    private Color knobColor = Color.White;
    private double knobSize = 0.5;
    private double textSize = 0.3;
    private string onText = "ON";
    private string offText = "OFF";

    public string OnText {
      get { return onText; }
      set {
        onText = value;
        Invalidate();
      }
    }

    public string OffText {
      get { return offText; }
      set {
        offText = value;
        Invalidate();
      }
    }


    public event EventHandler CheckedChanged;

    public OnOffControl() {
      this.Size = new Size(70, 40); // Adjust size as needed
      this.BackColor = isChecked ? onColor : offColor;
      this.Click += OnOffControl_Click;
      Cursor = Cursors.Hand;
    }

    public bool Checked {
      get { return isChecked; }
      set {
        if (isChecked != value) {
          isChecked = value;
          this.BackColor = isChecked ? onColor : offColor;
          CheckedChanged?.Invoke(this, EventArgs.Empty);
          Invalidate();
        }
      }
    }

    public new bool Enabled {
      get { return base.Enabled; }
      set {
        base.Enabled = value;
        Invalidate();
      }
    }

    protected override void OnPaint(PaintEventArgs e) {
      base.OnPaint(e);

      Graphics g = e.Graphics;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

      // Draw parent control background
      g.Clear(Parent.BackColor);

      // Pens and brushed
      Brush backgroundBrush = new SolidBrush(!Enabled ? disabledColor : isChecked ? onColor : offColor);
      Brush knobBrush = new SolidBrush(knobColor);
      Font font = new Font("Arial", (int)(Height * textSize), FontStyle.Bold);

      // Draw the outer shape
      using (GraphicsPath path = GetRoundedRectangle(rect, Height / 2)) {
        g.FillPath(backgroundBrush, path);
      }

      // Draw the inner shape
      int radius = (int)(Height / 2.0 * knobSize);
      int knobPadding = Height / 2 - radius;
      g.FillEllipse(knobBrush, isChecked ? Width - 2*radius - knobPadding : knobPadding, knobPadding, radius * 2, radius * 2);

      // Draw the text
      string text = isChecked ? OnText : OffText;
      SizeF onSize = g.MeasureString(text, font);
      int textX = isChecked ? knobPadding : Width - (int)onSize.Width - knobPadding;
      g.DrawString(text, font, Brushes.White, textX, (Height - onSize.Height) / 2);

      // Dispose
      backgroundBrush.Dispose();
      knobBrush.Dispose();
      font.Dispose();
    }

    private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius) {
      GraphicsPath path = new GraphicsPath();
      path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
      path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
      path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
      path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
      path.CloseFigure();
      return path;
    }

    private void OnOffControl_Click(object sender, EventArgs e) {
      if (!Enabled) return;
      this.Checked = !this.Checked;
    }
  }
}
