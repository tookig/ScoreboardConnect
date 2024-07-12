using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreboardConnectWinUI3.Controls {
  public partial class CurtainControl : UserControl {

    private bool m_isOpen = false;

    public bool IsOpen {
      get { return m_isOpen; }
      set {
        if (value == m_isOpen) return;
        m_isOpen = value;
        if (m_isOpen) {
          OpenCurtain();
        } else {
          CloseCurtain();
        }
      }
    }

    public override Color ForeColor {
      get => base.ForeColor;
      set  {
        base.ForeColor = value;
        label1.ForeColor = value;
      }
    }

    public override string Text {
      get => base.Text;
      set {
        base.Text = value;
        label1.Text = value;
      }
    }

    public Panel ContentsPanel { get { return panelContent; } }

    public CurtainControl() {
      InitializeComponent();
      buttonExpand.Click += ButtonExpand_Click;
      label1.ForeColor = ForeColor;
      if (m_isOpen) {
        OpenCurtain();
      } else {
        CloseCurtain();
      }
    }

    private void ButtonExpand_Click(object sender, EventArgs e) {
      IsOpen = !m_isOpen;
    }

    public void OpenCurtain() {
      panelContent.Visible = true;
      buttonExpand.Text = "-";
    }

    public void CloseCurtain() {
      panelContent.Visible = false;
      buttonExpand.Text = "+";
    }
  }
}
