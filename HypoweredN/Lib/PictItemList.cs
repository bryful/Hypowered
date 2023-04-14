using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Hypowered
{
	public class PictItemEventArgs : EventArgs
	{
		public PictItem Item = new PictItem();
		public PictItemEventArgs(PictItem pi)
		{
			Item = pi;
		}
	}

	public partial class PictItemList : Control
	{


		// ****************************************************************
		public delegate void PictItemChangedHandler(object sender, PictItemEventArgs e);
		public event PictItemChangedHandler? PictItemChanged;
		protected virtual void OnPictItemChanged(PictItemEventArgs e)
		{
			if (PictItemChanged != null)
			{
				PictItemChanged(this, e);
			}
		}
		// ****************************************************************
		private MainForm? m_MainForm = null;
		public void SetMainForm(MainForm? mainForm)
		{
			m_MainForm=mainForm;
			if(m_MainForm != null)
			{

			}
		}
		private ItemsLib? m_ItemsLib = null;
		public ItemsLib? ItemsLib
		{
			get { return m_ItemsLib; }
		}
		public void SetItemsLib(ItemsLib? m)
		{
			m_ItemsLib = m;
			if (m_ItemsLib != null)
			{
				GetBitmaps(m_ItemsLib);
			}
		}
		public readonly int ThumWidth  = 64;
		public readonly int ThumHeight  = 64;
		private int m_ThumCol = 20;
		private int m_ThumRow = 15;
		private int m_SideWidth = 20;

		private Color m_LineColor = Color.FromArgb(150,150,150);
		[Category("Color")]
		public Color LineColor
		{
			get { return m_LineColor; }
			set { m_LineColor = value; this.Invalidate(); }
		}
		private Color m_IndexColor = Color.FromArgb(150, 200, 200);
		[Category("Color")]
		public Color IndexColor
		{
			get { return m_IndexColor; }
			set { m_IndexColor = value; this.Invalidate(); }
		}
		private Color m_SidePushColor = Color.FromArgb(200, 200, 200);
		[Category("Color")]
		public Color SidePushColor
		{
			get { return m_SidePushColor; }
			set { m_SidePushColor = value; this.Invalidate(); }
		}
		private List<PictItem> m_PictIems = new List<PictItem>();
		public PictItem? PictIems(int idx)
		{
			if((idx>=0)&&(idx< m_PictIems.Count))
			{
				return m_PictIems[idx];
			}
			else
			{
				return null;
			}
		}

		// ***************************************************************
		public string TargetPictName
		{
			get 
			{
				string ret = "";
				if((m_Index>=0)&&(m_Index<m_PictIems.Count))
				{
					ret = m_PictIems[m_Index].Name;
				}
				return ret;
			}
			set
			{
				m_Index = IndexOf(value);
				this.Invalidate();
			}
		}
		// ***************************************************************
		public int IndexOf(string n)
		{
			int ret = -1;
			for(int i=0;i<m_PictIems.Count;i++)
			{
				if (n.Equals(m_PictIems[i].Name,StringComparison.OrdinalIgnoreCase)==true)
				{
					ret = i; 
					break;
				}
			}
			return ret;
		}
		// ***************************************************************
		private int m_Index = -1;
		private int m_PageIndex = 0;
		private int m_PageMax = 0;
		private int m_PageCount = 20 * 15;

		private int m_SidePush = -1;
		public PictItemList()
		{
			this.Size = new Size(1000, 500);
			InitializeComponent();
			base.BackColor = Color.FromArgb(64, 64, 64);
			base.ForeColor = Color.FromArgb(230, 230, 230);
			this.DoubleBuffered = true;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.UpdateStyles();
			ChkSize();
		}
		private void ChkSize()
		{
			m_ThumCol = (this.Width-m_SideWidth*2) / ThumWidth;
			m_ThumRow = this.Height / ThumHeight;
			m_PageCount = m_ThumCol * m_ThumRow;
			if(m_PictIems.Count>0)
			{
				m_PageMax = m_PictIems.Count / m_PageCount;
				if ((m_PictIems.Count % m_PageCount) > 0) m_PageMax++;
			}
			if (m_PageIndex >= m_PageMax) m_PageIndex = m_PageMax - 1;
		}
		private void GetBitmaps(ItemsLib il)
		{
			m_PictIems.Clear();
			m_PageIndex = 0;
			m_Index = -1;
			string tp = il.TargetPath;
			string[] itns = il.GetItemNamesAtPict();
			if (itns.Length > 0)
			{
				foreach (string itn in itns)
				{
					Bitmap? bmp = il.GetBitmap(itn);
					if (bmp != null)
					{
						PictItem pictItem = new PictItem();
						pictItem.OrgSize = bmp.Size;
						pictItem.Name = itn;
						pictItem.Bitmap = new Bitmap(
							ThumWidth,
							ThumHeight,
							PixelFormat.Format32bppArgb);

						ItemsLib.ResizaDraw(bmp, pictItem.Bitmap);
						m_PictIems.Add(pictItem);
					}
				}
			}
			ChkSize();
			this.Invalidate();

		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			using(SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = pe.Graphics;
				g.FillRectangle(sb, this.ClientRectangle);

				int pp = m_PageIndex * m_PageCount;
				for (int j=0; j<m_ThumRow;j++)
				{
					int top = j * ThumHeight;
					for (int i = 0; i < m_ThumCol; i++)
					{
						int idx =  pp + i + j*m_ThumCol;
						int left = i * ThumWidth + m_SideWidth;
						if((idx >= 0)&&(idx<m_PictIems.Count))
						{
							if (m_PictIems[idx].Bitmap != null)
							{
								g.DrawImage(m_PictIems[idx].Bitmap, left, top);
							}
						}
						p.Width = 1;
						p.Color = m_LineColor;
						Point[] ps2 = new Point[3];
						ps2[0] = new Point(left +ThumWidth - 1, top);
						ps2[1] = new Point(ps2[0].X, top+ThumHeight-1);
						ps2[2] = new Point(left, ps2[1].Y);
						g.DrawLines(p, ps2);
						if(idx==m_Index)
						{
							p.Color = m_IndexColor;
							p.Width = 2;
							g.DrawRectangle(p, new Rectangle(
								left,
								top,
								ThumWidth - 2,
								ThumHeight - 2
								));

						}
					}

				}
				// Leftside
				if(m_SidePush==0)
				{
					sb.Color = m_SidePushColor;
					g.FillRectangle(sb,
						new Rectangle(0, 0, m_SideWidth, this.Height));
				}
				PointF []ps = new PointF[3];
				ps[0] = new PointF(3, this.Height / 2);

				ps[1] = new PointF(m_SideWidth-3, this.Height / 2 - 10);
				ps[2] = new PointF(m_SideWidth - 3, this.Height / 2 + 10);
				sb.Color = ForeColor;
				g.FillPolygon(sb, ps);

				p.Color = ForeColor;
				g.DrawRectangle(p, 
					new Rectangle(0, 0, m_SideWidth-1, this.Height - 1));

				// Rigtside
				if (m_SidePush == 1)
				{
					sb.Color = m_SidePushColor;
					g.FillRectangle(sb,
						new Rectangle(this.Width-m_SideWidth, 0, m_SideWidth, this.Height));
				}
				PointF[] ps3 = new PointF[3];
				ps3[0] = new PointF(this.Width-m_SideWidth+3, this.Height / 2 - 10);
				ps3[1] = new PointF(this.Width - 3, this.Height / 2);
				ps3[2] = new PointF(this.Width - m_SideWidth + 4, this.Height / 2 + 10);
				sb.Color = ForeColor;
				g.FillPolygon(sb, ps3);
				p.Color = ForeColor;
				g.DrawRectangle(p, 
					new Rectangle(this.Width-m_SideWidth, 0, m_SideWidth - 1, this.Height - 1));

				p.Color = ForeColor;
				g.DrawRectangle(p, new Rectangle(0,0,this.Width-1,this.Height-1));
			}
		}
		protected override void OnResize(EventArgs e)
		{
			ChkSize();
			this.Refresh();
			base.OnResize(e);
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				if (e.X < m_SideWidth)
				{
					m_SidePush = 0;
					m_PageIndex -= 1;
					if (m_PageIndex < 0) m_PageIndex = 0;
				}
				else if (e.X > this.Width - m_SideWidth)
				{
					m_SidePush = 1;
					m_PageIndex += 1;
					if (m_PageIndex >= m_PageMax) m_PageIndex = m_PageMax-1;
				}
				else
				{
					int x = (e.X- m_SideWidth)/ThumWidth;
					if (x >= m_ThumCol) x = m_ThumCol - 1;
					int y = (e.Y ) / ThumHeight;
					if (y >= m_ThumRow) y = m_ThumRow - 1;

					int idx = m_PageIndex * m_PageCount + x + y * m_ThumCol;
					if ((idx < 0) || (idx >= m_PictIems.Count)) idx = -1;
					if(m_Index!=idx)
					{
						m_Index = idx;
						this.Invalidate();
						OnPictItemChanged(new PictItemEventArgs(m_PictIems[m_Index]));
					}


					m_SidePush = -1;
				}
				if(m_SidePush>=0) this.Invalidate();
			}
			else
			{
				base.OnMouseDown(e);
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if(m_SidePush>=0)
			{
				m_SidePush = -1;
				this.Invalidate();
			}
			else
			{
				base.OnMouseUp(e);
			}
		}
	}
	public class PictItem
	{
		public Bitmap? Bitmap = null;
		public Size OrgSize = new Size(0, 0);
		public string Name = "";
		public PictItem() 
		{
		}
	}

}
