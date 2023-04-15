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
		public PictItem? Item = new PictItem();
		public PictItemEventArgs(PictItem? pi)
		{
			Item = pi;
		}
	}

	public partial class PictItemList : Control
	{
		private VScrol VScrol = new VScrol();

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
		private int m_ThumRowMax = 15;
		private int m_DispY = 0;
		private int m_DispYMax = 0;
		private int m_Index = -1;
		public int Index
		{
			get { return m_Index; }
			set
			{
				if((value>=0)&&(value<m_PictIems.Count))
				{
					m_Index = value;
				}
				else
				{
					m_Index = -1;
				}
				this.Invalidate();
			}
		}

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
		public PictItem? TargetPictItem
		{
			get
			{
				PictItem? ret = null;
				if ((m_Index >= 0) && (m_Index < m_PictIems.Count))
				{
					ret = m_PictIems[m_Index];
				}
				return ret;
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

		public PictItemList()
		{
			this.Size = new Size(1000, 500);
			VScrol.Location = new Point(1000-20, 500);
			VScrol.Size = new Size(20, 500);
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

			this.Controls.Add(VScrol);
			ChkSize();
			VScrol.ValueChanged += (sender, e) =>
			{
				if (m_DispY != e.Value)
				{
					m_DispY = e.Value;
					this.Invalidate();
				}
			};
		}
		private void ChkSize()
		{
			m_ThumCol = (this.Width) / ThumWidth;
			m_ThumRow = this.Height / ThumHeight;
			m_ThumRowMax = m_ThumRow;
			if (m_PictIems.Count>0)
			{
				m_ThumRowMax = m_PictIems.Count / m_ThumCol;
				if ((m_PictIems.Count % m_ThumCol) > 0) m_ThumRowMax++;
				m_DispYMax = (m_ThumRowMax * ThumHeight) - this.Height;
				if (m_DispYMax < 0) m_DispYMax = 0;
				if (m_DispY > m_DispYMax) m_DispY = m_DispYMax;
			}
			if (VScrol.ValueMax != m_DispYMax) VScrol.ValueMax = m_DispYMax;
			if (VScrol.Value != m_DispY) VScrol.Value = m_DispY;
		}
		private void GetBitmaps(ItemsLib il)
		{
			m_PictIems.Clear();
			m_Index = -1;
			string tp = il.FileName;
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

				if (m_PictIems.Count > 0)
				{
					for (int i = 0; i < m_PictIems.Count; i++)
					{
						int x = i % m_ThumCol;
						int px = x * ThumWidth;
						int y = i / m_ThumCol;
						int py = y * ThumHeight -m_DispY;
						if (py >= this.Height) break;
						if(py>=-ThumHeight)
						{
							if (m_PictIems[i].Bitmap != null)
							{
								g.DrawImage(m_PictIems[i].Bitmap, px, py);
							}
						}
						if((i == m_Index) && (m_Index >= 0))
						{
							Rectangle rectangle = new Rectangle(px,py,ThumWidth-2,ThumHeight-2);
							p.Color = ForeColor;
							p.Width = 3;
							g.DrawRectangle(p, rectangle);
							p.Width = 1;
						}

					}
				}
				//縦線
				p.Color = ForeColor;
				for (int i=0; i<=m_ThumCol;i++)
				{
					int x = i * ThumWidth;
					if (x < this.Width)
					{
						g.DrawLine(p, x, 0, x, this.Height);
					}
				}
				int yoffset = m_DispY % ThumHeight;
				for (int i = 0; i <= m_ThumRow+1; i++)
				{
					int y = i * ThumHeight - yoffset;
					if ((y>=0)&&(y < this.Height))
					{
						g.DrawLine(p, 0, y, this.Width, y);
					}
				}
				p.Color = ForeColor;
				g.DrawRectangle(p, new Rectangle(0,0,this.Width-1-20,this.Height-1));
			}
		}
		protected override void OnResize(EventArgs e)
		{
			ChkSize();
			VScrol.Location = new Point(this.Width - VScrol.Width, 0);
			VScrol.Size = new Size(20,this.Height);
			this.Refresh();
			base.OnResize(e);
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				if (m_PictIems.Count > 0)
				{
					int x = (e.X) / ThumWidth;
					if (x >= m_ThumCol) x = m_ThumCol - 1;
					int y = (e.Y + m_DispY) / ThumHeight;

					int idx = x + y * m_ThumCol;
					if (idx < 0) idx = 0;
					else if (idx >= m_PictIems.Count) idx = m_PictIems.Count-1;
					if (m_Index != idx)
					{
						m_Index = idx;
						this.Invalidate();
						OnPictItemChanged(new PictItemEventArgs(m_PictIems[m_Index]));
					}
				}
			}
			else
			{
				base.OnMouseDown(e);
			}
		}
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			int idx = e.Delta / 120;

			int v = m_DispY;
			v -= idx * (m_DispYMax/20);
			if (v < 0) v = 0;
			else if (v >m_DispYMax) v = m_DispYMax;

			if(v!=m_DispY)
			{
				m_DispY = v;
				this.Invalidate();
				if (VScrol.Value != v) VScrol.Value = v;
			}

			base.OnMouseWheel(e);
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
