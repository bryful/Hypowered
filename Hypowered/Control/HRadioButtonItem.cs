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
	public partial class HRadioButtonItem : Control
	{
		public HRadioButton? HRadioButton { get; set; } = null;
		public int Index = -1;
		private int m_ButtonWidth = 12;
		private bool m_Checked = false;
		public bool Checked
		{
			get { return m_Checked; }
			set { m_Checked = value; this.Invalidate(); }
		}
		public HRadioButtonItem()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			using (StringFormat sf = new StringFormat())
			using (Pen p = new Pen(ForeColor))
			using (SolidBrush sb = new SolidBrush(BackColor))
			{
				Graphics g = pe.Graphics;
				g.FillRectangle(sb, this.ClientRectangle);

				Rectangle r = new Rectangle(5, (this.Height - m_ButtonWidth) / 2, m_ButtonWidth-1, m_ButtonWidth-1);
				p.Color = ForeColor;
				g.DrawRectangle(p, r);

				if (m_Checked)
				{
					sb.Color = ForeColor;
					Rectangle r2 = new Rectangle(r.Left + 2, r.Top + 2, r.Width - 3, r.Height - 3);
					g.FillRectangle(sb, r2);
				}
				r = new Rectangle(r.Right + 5, 0, this.Width - (r.Right + 5), this.Height);
				sf.Alignment = StringAlignment.Near;
				sf.LineAlignment = StringAlignment.Center;
				sb.Color = ForeColor;
				g.DrawString(this.Text,this.Font, sb, r, sf);

			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if(HRadioButton!=null)
			{
				HRadioButton.Index = Index;
			}
			base.OnMouseDown(e);
		}
	}
}
