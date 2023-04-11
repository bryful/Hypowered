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
	public class HLabel : HControl
	{
		protected DotStyle m_LeftDot = DotStyle.None;
		[Category("_Hypowered")]
		public DotStyle LeftDot
		{
			get { return m_LeftDot; }
			set { m_LeftDot=value; this.Invalidate(); }
		}
		protected DotStyle m_RightDot = DotStyle.None;
		[Category("_Hypowered")]
		public DotStyle RightDot
		{
			get { return m_RightDot; }
			set { m_RightDot = value; this.Invalidate(); }
		}
		public HLabel()
		{
			m_HType = HType.Label;
			TextAlign = StringAlignment.Near;
			StringFormat.Alignment = StringAlignment.Center;
			StringFormat.LineAlignment = StringAlignment.Center;

			this.DoubleBuffered = true;
			this.SetStyle(
				//ControlStyles.Selectable |
				//ControlStyles.UserMouse |
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.UpdateStyles();
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

				sb.Color = BackColor;
				Rectangle r = RectInc(this.ClientRectangle, 2);
				g.FillRectangle(sb, r);


				int w = 0;
				int t = 0;
				if ((m_LeftDot != DotStyle.None) || (m_RightDot != DotStyle.None))
				{
					using (StringFormat sf = new StringFormat(StringFormat.GenericTypographic))
					{
						var size2 = g.MeasureString("8", this.Font, this.Width, sf);
						w = (int)(size2.Height -4);
					}
					t = (r.Height - w) / 2 + r.Top;
				}

				if (m_LeftDot != DotStyle.None)
				{
					Rectangle r1 = new Rectangle(r.Left, t, w, w);
					r = new Rectangle(r.Left + w + 2, r.Top, r.Width - w - 2, r.Height);
					sb.Color = ForeColor;
					DrawDot(g, m_LeftDot, sb, r1);
				}
				if (m_RightDot != DotStyle.None)
				{
					Rectangle r2 = new Rectangle(r.Right - w, t, w, w);
					r = new Rectangle(r.Left, r.Top, r.Width - w - 2, r.Height);
					sb.Color = ForeColor;
					DrawDot(g, m_RightDot, sb, r2);
				}

				//文字
				if (this.Text != "")
				{
					sb.Color = ForeColor;
					Rectangle r2 = r;
					g.DrawString(this.Text, this.Font, sb, r2, StringFormat);
				}
				// IsEdit
				DrawIsEdit(g, p);
			}
		}
	}
}
