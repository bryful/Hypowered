using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class HyperRadioButton : HyperControl
	{
		public class RButtonChangedEventArgs : EventArgs
		{
			public int Index;
			public RButtonChangedEventArgs(int v)
			{
				Index = v;
			}
		}
		public delegate void RButtonChangedHandler(object sender, RButtonChangedEventArgs e);
		public event RButtonChangedHandler? RButtonChanged;
		protected virtual void OnRButtonChanged(RButtonChangedEventArgs e)
		{
			if (RButtonChanged != null)
			{
				RButtonChanged(this, e);
			}
			if ((HyperForm != null) && (m_ScriptCode != ""))
			{
				HyperForm.ExecuteCode(m_ScriptCode);
			}
		}
		private bool m_Checked = false;
		[Category("Hypowerd_RadioButton")]
		public bool Checked
		{
			get { return m_Checked; }
			set 
			{
				m_Checked = value;
				if (m_Checked == true)
				{
					if (HyperForm != null)
					{
						HyperForm.ChkRadioButton(this);
						OnRButtonChanged(new RButtonChangedEventArgs(this.Index));
					}
				}
				this.Invalidate();
			}
		}
		public void SetCheckedNoEvent(bool b)
		{
			m_Checked = b;
		}
		protected int m_Group = 0;
		[Category("Hypowerd_RadioButton")]
		public int Group 
		{
			get { return m_Group; } 
			set 
			{
				if (value < 0) value = 0;
				m_Group = value; 
				this.Invalidate(); 
			}
		}
		protected int m_GroupIndex = 0;
		[Category("Hypowerd_RadioButton")]
		public int GroupIndex
		{
			get { return m_GroupIndex; }
			set
			{
				if (value < 0) value = 0;
				if(HyperForm!=null)
				{
					if (HyperForm.IsRadioButtonIndex(this, value) ==false)
					{
						m_GroupIndex= value;
					}
				}
				m_GroupIndex = value;
				this.Invalidate();
			}
		}
		private int m_CheckSize;
		[Category("Hypowerd_RadioButton")]
		public int CheckSize
		{
			get { return m_CheckSize; }
			set { m_CheckSize = value; this.Invalidate(); }
		}
		public HyperRadioButton()
		{
			SetMyType(ControlType.RadioButton);
			m_ScriptCode = "//RadioButton";
			ControlName = "HyperRadioButton";
			m_format.Alignment = StringAlignment.Near;
			m_format.LineAlignment = StringAlignment.Center;
			m_UnCheckedColor = ColU.ToColor(HyperColor.Dark);
			m_CheckSize = 16;
			this.Size = ControlDef.DefSize; InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = pe.Graphics;
				// 背景色
				g.FillRectangle(sb, this.ClientRectangle);

				Rectangle r = new Rectangle(3, (this.Height - m_CheckSize) / 2, m_CheckSize, m_CheckSize);
				p.Width = 1;
				g.DrawEllipse(p, r);
				if (m_Checked)
				{
					sb.Color = ForeColor;
				}
				else
				{
					sb.Color = m_UnCheckedColor;
				}
				RectangleF rs = ReRectF(r, 5);
				g.FillEllipse(sb, rs);

				// 外枠
				Rectangle rr = ReRect(this.ClientRectangle, 2);
				//p.Color = ForeColor;
				//g.DrawRectangle(p, rr);
				if (this.Focused)
				{
					rr = ReRect(this.ClientRectangle, 1);
					p.Color = m_ForcusColor;
					g.DrawRectangle(p, rr);
				}
				if (this.Text != "")
				{
					sb.Color = ForeColor;
					rr = new Rectangle(m_CheckSize + 5, 3, this.Width - m_CheckSize - 5, this.Height - 6);
					g.DrawString(this.Text, this.Font, sb, rr, m_format);
				}
				DrawType(g, sb);

			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (m_IsEditMode)
			{
				if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
				{
					ChkTargetSelected();

					MDPos p = CU.GetMDPos(e.X, e.Y, this.Size);
					if (p != MDPos.None)
					{
						m_MDPos = p;
						m_MDP = new Point(e.X, e.Y);
						m_MDLoc = this.Location;
						m_MDSize = this.Size;
						return;
					}
				}
			}
			else
			{
				if(m_Checked==false)
				{
					if(HyperForm!=null)
					{
						HyperForm.ChkRadioButton(this);
					}
				}
				this.Invalidate();
				return;

			}
			base.OnMouseDown(e);
		}
	}
}
