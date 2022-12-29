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
	public partial class PictLibBox : Control
	{
		private HyperMainForm? m_form = null;
		private HyperPictLib? m_PictLib = null;
		public void SetMainForm(HyperMainForm? mf)
		{
			m_form= mf;
			if(m_form!=null)
			{
				m_PictLib = mf.PictLib;
				ChkSize();
				this.Invalidate();
			}
		}
		private Size m_IconSize = new Size(48+4, 48+4);
		private int m_WCount = 1;
		private int m_HCount = 1;
		private int m_TargetIndex = -1;
		private int m_PageMax = 1;
		private int m_PageCount = 1;
		private int m_PageIndex = 0;
		public int TargetIndex
		{
			get { return m_TargetIndex; }
			set { m_TargetIndex = value; }
		}
		private Button? m_LeftBtn = null;
		public Button? LeftBtn
		{
			get { return m_LeftBtn; }
			set 
			{ 
				m_LeftBtn = value;
				ChkSize();
				if(m_LeftBtn!=null)
				{
					m_LeftBtn.Click += M_LeftBtn_Click;
				}
			}
		}

		private void M_LeftBtn_Click(object? sender, EventArgs e)
		{
			int idx = m_PageIndex - 1;
			if (idx < 0) idx = 0;
			if(idx!=m_PageIndex)
			{
				m_PageIndex = idx;
				this.Invalidate();
			}
		}

		private Button? m_RightBtn = null;
		public Button? RightBtn
		{
			get { return m_RightBtn; }
			set 
			{ 
				m_RightBtn = value;
				ChkSize();
				if(m_RightBtn!=null)
				{
					m_RightBtn.Click += M_RightBtn_Click;

				}
			}
		}

		private void M_RightBtn_Click(object? sender, EventArgs e)
		{
			int idx = m_PageIndex + 1;
			if (idx >=m_PageMax) idx = m_PageMax-1;
			if (idx != m_PageIndex)
			{
				m_PageIndex = idx;
				this.Invalidate();
			}
		}
		private TextBox? m_TextBox = null;
		public TextBox? TextBox
		{
			get { return m_TextBox; }
			set { m_TextBox = value; }
		}
		public void ChkSize()
		{
			m_WCount = this.Width / m_IconSize.Width;
			if (m_WCount < 1) m_WCount = 1;
			m_HCount = this.Height / m_IconSize.Height;
			if (m_HCount < 1) m_HCount = 1;
			/*this.Size = new Size(
				m_WCount * m_IconSize.Width, 
				m_HCount * m_IconSize.Height);*/
			m_PageCount = m_WCount * m_HCount;
			if(m_PictLib!= null)
			{
				m_PageMax = m_PictLib.Count / m_PageCount;
				if ((m_PictLib.Count % m_PageCount) != 0) m_PageMax += 1;
				if (m_PageIndex >= m_PageMax) m_PageIndex = m_PageMax - 1;
			}
			else
			{
				m_PageMax = 1;
				m_PageIndex = 0;
			}
			if (m_LeftBtn != null)
			{
				m_LeftBtn.Location = new Point(this.Left - 30, this.Top);
				m_LeftBtn.Size = new Size(25, this.Height);

			}
			if (m_RightBtn != null)
			{
				m_RightBtn.Location = new Point(this.Right +5, this.Top);
				m_RightBtn.Size = new Size(25, this.Height);
			}
		}
		private Color m_ForcusColor= Color.White;
		[Category("Hypowerd_Color")]
		public Color ForcusColor
		{
			get { return m_ForcusColor; }
			set { m_ForcusColor = value; this.Invalidate(); }
		}
		public PictLibBox()
		{
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			m_ForcusColor = ColU.ToColor(HyperColor.Forcus);
			InitializeComponent();
			this.SetStyle(
//ControlStyles.Selectable |
//ControlStyles.UserMouse |
ControlStyles.DoubleBuffer |
ControlStyles.UserPaint |
ControlStyles.AllPaintingInWmPaint |
ControlStyles.SupportsTransparentBackColor,
true);
			this.UpdateStyles();
			ChkSize();
		}

		protected void DrawIcon(Graphics g, SolidBrush sb, Pen p,Point pnt,int idx)
		{
			if (m_PictLib == null) return;
			Rectangle rct = new Rectangle(pnt, m_IconSize);
			p.Color = ForeColor;
			p.Width = 1;
			g.DrawRectangle(p, rct);
			Bitmap? bmp = m_PictLib.Thum(idx);
			if (bmp!=null)
			{
				g.DrawImage(bmp, new Rectangle(pnt.X+2, pnt.Y + 2,48,48));
			}
			if(idx== m_TargetIndex)
			{
				rct = new Rectangle(rct.Left+3,rct.Top+3,rct.Width-6,rct.Height-6);
				p.Color = ForeColor;
				p.Width = 3;
				g.DrawRectangle(p, rct);
			}

		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = pe.Graphics;
				g.FillRectangle(sb, this.ClientRectangle);

				int idx = m_PageIndex * m_PageCount;
				for (int y = 0; y < m_HCount; y++)
				{
					for (int x = 0; x < m_WCount; x++)
					{
						Point loc = new Point(
							x * m_IconSize.Width, 
							y * m_IconSize.Height);
						DrawIcon(g, sb, p, loc, idx);
						idx++;
					}
				}
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
			this.Invalidate();
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			int cx = e.X / m_IconSize.Width;
			if (cx >= m_WCount) cx = m_WCount - 1;
			int cy = e.Y / m_IconSize.Height;
			if (cy >= m_HCount) cy = m_HCount - 1;
			int idx = cx + m_WCount * cy;
			idx += m_PageIndex * m_PageCount;
			if(m_TargetIndex != idx)
			{
				m_TargetIndex = idx;

				if((m_PictLib!=null)&&(m_TextBox!=null))
				{
					m_TextBox.Text = m_PictLib.BitmapName(m_TargetIndex);
				}

				this.Invalidate();
			}
		}
	}
}
