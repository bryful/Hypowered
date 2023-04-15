using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class HButton : HControl
	{
		#region Prop
		protected Color m_DownColor = Color.FromArgb(180, 180, 180);
		[Category("Hypowered_Color")]
		public Color DownColor
		{
			get { return m_DownColor; }
			set { m_DownColor = value; this.Invalidate(); }
		}
		protected Color m_CheckedColor = Color.FromArgb(160, 160, 160);
		[Category("Hypowered_Color")]
		public Color CheckedColor
		{
			get { return m_CheckedColor; }
			set { m_CheckedColor = value; this.Invalidate(); }
		}
		protected bool m_Checked = false;
		[Category("Hypowered_Color")]
		public bool Checked
		{
			get { return m_Checked; }
			set { m_Checked = value; this.Invalidate(); }
		}
		#endregion

		public HButton()
		{
			m_HType = HType.Button;
			TextAlign = StringAlignment.Center;
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(base.BackColor))
			using (Pen p = new Pen(base.ForeColor))
			{
				Graphics g = pe.Graphics;
				// 塗り
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);

				if (m_MDPush==true)
				{
					sb.Color = m_DownColor;
				}else if (m_Checked==true)
				{
					sb.Color = m_CheckedColor;
				}
				else
				{
					sb.Color = BackColor;
				}
				Rectangle r = RectInc(this.ClientRectangle, 2);
				g.FillRectangle(sb, r);
				//文字
				if(this.Text!="")
				{
					sb.Color= ForeColor;
					g.DrawString(this.Text, this.Font, sb, r, StringFormat);
				}
				p.Color = ForeColor;
				DrawFrame(g, p, r);
				if (Focused)
				{
					p.Color = m_ForcusColor;
					DrawFrame(g, p, this.ClientRectangle, 2);
				}
				// IsEdit
				DrawIsEdit(g, p);
			}
		}
		protected bool m_MDPush = false;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if(m_IsEdit)
			{
				base.OnMouseDown(e);
			}
			else
			{
				m_MDPush = true;
				this.Invalidate();
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_IsEdit)
			{
				base.OnMouseUp(e);
			}
			else
			{
				m_MDPush = false;
				this.Invalidate();
			}

		}
	}
}
