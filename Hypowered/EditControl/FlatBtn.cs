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
	public partial class FlatBtn : Control
	{
		public FlatBtn()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			using(SolidBrush sb = new SolidBrush(base.BackColor))
			{
				Graphics g = pe.Graphics;
				if (m_MD)
				{
					sb.Color = Color.FromArgb(
						BackColor.R + 30,
						BackColor.G + 30,
						BackColor.B + 30);
				}
				else
				{
					sb.Color = BackColor;
				}
				g.FillRectangle(sb, ClientRectangle);
				sb.Color= base.ForeColor;
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;
				g.DrawString(this.Text, this.Font,sb, this.ClientRectangle, sf);
			}
		}
		private bool m_MD=false;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			m_MD=true;
			Invalidate();
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			m_MD=false;
			Invalidate();
		}
	}
}
