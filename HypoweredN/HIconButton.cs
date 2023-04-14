﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class HIconButton :HControl
	{
		protected Bitmap? m_Bitmap = null;
		protected string m_FileName = "";


		#region Prop
		protected int m_IconLeft = 0;
		protected int m_TextLeft = 0;
		protected int m_IconWidth = 32;
		[Category("Hypowered_Size")]
		public int IconWidth
		{
			get { return m_IconWidth; }
			set 
			{
				m_IconWidth = value;
				if(ChkSize())
				{
					this.Invalidate();
				}
			}
		}
		protected int m_IconHeight = 32;
		[Category("Hypowered_Size")]
		public int IconHeight
		{
			get { return m_IconHeight; }
			set
			{
				m_IconHeight = value;
				if (ChkSize())
				{
					this.Invalidate();
				}
			}
		}
		protected int m_TextWidth = 100;
		[Category("Hypowered_Size")]
		public int TextWidth
		{
			get { return m_TextWidth; }
			set
			{
				m_TextWidth = value;
				if (ChkSize())
				{
					this.Invalidate();
				}
			}
		}
		protected int m_TextHeight = 18;
		[Category("Hypowered_Size")]
		public int TextHeight
		{
			get { return m_TextHeight; }
			set
			{
				m_TextHeight = value;
				if (ChkSize())
				{
					this.Invalidate();
				}
			}
		}
		private bool ChkSize()
		{
			int w = m_IconWidth;
			if (w < m_TextWidth) w = m_TextWidth;
			int h = m_IconHeight + m_TextHeight;
			int w2 = (w / m_GridSize) * m_GridSize;
			if (w != w2) w = w2;
			int h2 = (h / m_GridSize) * m_GridSize;
			if (h != h2) h = h2;

			Size sz = new Size(w + 4, h + 4);
			if(base.Size != sz)
			{
				base.Size = sz;
				base.MinimumSize = new Size(0, 0);
				base.MaximumSize = new Size(0, 0);
				base.MinimumSize = sz;
				base.MaximumSize = sz;
				m_IconLeft = (this.Width-4 - m_IconWidth) / 2 + 2;
				m_TextLeft = (this.Width-4 - m_TextWidth) / 2 +2;
				return true;
			}
			else
			{
				return false;
			}
		}
		protected Color m_DownColor = Color.FromArgb(180, 180, 180);
		[Category("Hypowered_Color")]
		public Color DownColor
		{
			get { return m_DownColor; }
			set { m_DownColor = value; this.Invalidate(); }
		}
		protected Color m_TextBackColor = Color.Transparent;
		[Category("Hypowered_Color")]
		public Color TextBackColor
		{
			get { return m_TextBackColor; }
			set { m_TextBackColor = value; this.Invalidate(); }
		}
		[Category("Hypowered_Size")]
		public new Size Size
		{
			get { return base.Size; }
			set { }
		}
		[Category("Hypowered_Size")]
		public new int Width
		{
			get { return base.Width; }
			set { }
		}
		[Category("Hypowered_Size")]
		public new int Height
		{
			get { return base.Height; }
			set { }
		}
		#endregion
		// *********************************************************
		public HIconButton()
		{
			m_HType = HType.IconButton;
			TextAlign = StringAlignment.Center;
			ChkSize();
		}
		// *********************************************************
		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(base.BackColor))
			using (Pen p = new Pen(base.ForeColor))
			{
				Graphics g = pe.Graphics;
				// 塗り
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);


				if(m_Bitmap!=null)
				{
					g.DrawImage(m_Bitmap, m_IconLeft, 2);
				}
				else
				{
					Rectangle r = new Rectangle(m_IconLeft, 2, m_IconWidth, m_IconHeight);
					sb.Color = BackColor;
					g.FillRectangle(sb,r);
					p.Color = ForeColor;
					DrawFrame(g, p, r);
				}
				//文字
				if (this.Text != "")
				{
					Rectangle r2 = new Rectangle(m_TextLeft, m_IconHeight+2, m_TextWidth, m_TextHeight);
					sb.Color = m_TextBackColor;
					g.FillRectangle(sb,r2);
					sb.Color = ForeColor;
					g.DrawString(this.Text, this.Font, sb, r2, StringFormat);
				}
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
			if (m_IsEdit)
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
