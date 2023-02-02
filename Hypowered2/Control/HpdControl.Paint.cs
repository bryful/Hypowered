using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hpd
{
	partial class HpdControl
	{
		#region Paint
		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(BackColor))
			{
				//背景塗りつぶし
				Graphics g = pe.Graphics;
				g.FillRectangle(sb, this.ClientRectangle);

				//ラベル時の描画
				if (m_HpdType== HpdType.Label)
				{
					Rectangle sr = new Rectangle(0, 0, this.Width, this.Height);
					sb.Color= ForeColor;
					SizeF stringSize = g.MeasureString(this.Text, this.Font, 1000, m_StringFormat);
					m_PreferredSize = new Size((int)(stringSize.Width + 0.5), (int)(stringSize.Height + 0.5));
					m_BaseSize = m_PreferredSize;
					g.DrawString(this.Text, this.Font, sb, sr, m_StringFormat);
				}else if (m_CaptionWidth > 0)
				{
					Rectangle sr = new Rectangle(0, 0, m_CaptionWidth, this.Height);
					sb.Color = ForeColor;
					g.DrawString(this.Text, this.Font, sb, sr, m_StringFormat);

				}


				//枠線
				if (m_IsDrawFrame)
				{
					using (Pen p = new Pen(ForeColor))
					{
						Rectangle r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
						g.DrawRectangle(p, r);
					}
				}
			}
		}
		#endregion
	}
}
