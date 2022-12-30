using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class PictureBoxTarge : PictureBox
	{
		private bool m_IsEditMode = false;
		public bool IsEditMode
		{
			get { return m_IsEditMode; }
			set
			{
				m_IsEditMode = value;
				this.Visible = !m_IsEditMode;
				this.Invalidate();
			}
		}
		protected bool m_AutoFit = true;
		public bool AutoFit
		{
			get { return m_AutoFit; }
			set
			{
				m_AutoFit = value;
				if (m_AutoFit) Fit();
				this.Invalidate();
			}
		}
		protected string m_Path = "";
		protected Bitmap? m_OffScr = null;
		protected float m_ratio = 1F;
		protected Point m_MDPos = new Point(0, 0);
		protected Point m_MDLoc = new Point(0, 0);
		protected bool m_MD = false;
		protected Rectangle imgRect = new Rectangle(0, 0, 0, 0);
		protected Rectangle imgRectBak = new Rectangle(0, 0, 0, 0);
		protected Color m_BaseColor = Color.Transparent;
		public Color BaseColor
		{
			get { return m_BaseColor; }
			set { m_BaseColor = value; this.Invalidate(); }
		}
		//**************************************************************************
		public string FileName
		{
			get { return m_Path; }
			set
			{
				if (File.Exists(value))
				{
					OpenFile(value);
				}
			}
		}
		public Bitmap? Bitmap
		{
			get { return m_OffScr; }
			set
			{
				if (value == null)
				{
					if (m_OffScr != null) { m_OffScr.Dispose(); m_OffScr = null; }
					m_Path = "";
					return;
				}
				m_OffScr = new Bitmap(value.Width, value.Height, value.PixelFormat);
				Graphics g = Graphics.FromImage(m_OffScr);
				g.DrawImage(value, 0, 0);
				chkSize();
				m_Path = "";
			}
		}
		public float Ratio
		{
			get { return m_ratio; }
			set
			{
				if (m_AutoFit == false)
				{
					if (value < 0.1f) value = 0.1f;

					SetRatio(value);
				}
			}
		}
		public PictureBoxTarge()
		{
			AllowDrop = true;
			InitializeComponent();
			chkSize();
			//ダブルバッファー表示
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		}
		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			if(drgevent==null) return;
			if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
			{
				drgevent.Effect = DragDropEffects.Copy;
			}
			else
			{
				//ファイル以外は受け付けない
				drgevent.Effect = DragDropEffects.None;
				base.OnDragEnter(drgevent);
			}
		}
		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			if(drgevent==null) return;
			string[] fileName =
			(string[])drgevent.Data.GetData(DataFormats.FileDrop, false);
			
			foreach(var s in fileName)
			{
				if(OpenFile(s))
				{
					break;
				}
			}
			base.OnDragDrop(drgevent);
		}
		protected override void OnDoubleClick(EventArgs e)
		{
			if(m_IsEditMode==false)
			{
				Fit();
			}
			base.OnDoubleClick(e);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			SolidBrush b = new SolidBrush(m_BaseColor);
			try
			{
				if (m_IsEditMode == false)
				{
					if (m_OffScr != null)
					{
						if (m_ratio < 1)
						{
							g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
						}
						else
						{
							g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
						}
						b.Color = Color.Transparent;
						g.FillRectangle(b, new Rectangle(0, 0, this.Width, this.Height));
						//画像を指定された範囲に描画する
						g.DrawImage(m_OffScr, imgRect);
					}
				}
				else
				{
					b.Color = Color.Black;
					g.FillRectangle(b, new Rectangle(0, 0, this.Width, this.Height));
				}
			}
			finally
			{
				b.Dispose();
			}

		}
		//**************************************************************************
		public void calcImgRect(float r)
		{
			if (m_OffScr == null) return;

			int cx = imgRect.X + imgRect.Width / 2;
			int cy = imgRect.Y + imgRect.Height / 2;

			//倍率を変更する
			m_ratio = r;

			//画像の表示範囲を計算する
			imgRect.Width = (int)Math.Round(m_OffScr.Width * m_ratio);
			imgRect.Height = (int)Math.Round(m_OffScr.Height * m_ratio);
			imgRect.X = cx - imgRect.Width / 2;
			imgRect.Y = cy - imgRect.Height / 2;

			this.Invalidate();

		}
		//**************************************************************************
		public void Fit()
		{
			if (m_OffScr == null) return;

			double xr = (double)this.Width / (double)m_OffScr.Width;
			double yr = (double)this.Height / (double)m_OffScr.Height;
			m_ratio = (float)xr;
			if (m_ratio > (float)yr) m_ratio = (float)yr;
			imgRect.Width = (int)Math.Round(m_OffScr.Width * m_ratio);
			imgRect.Height = (int)Math.Round(m_OffScr.Height * m_ratio);
			imgRect.X = this.Width / 2 - imgRect.Width / 2;
			imgRect.Y = this.Height / 2 - imgRect.Height / 2;


		}
		//**************************************************************************
		public void SetRatio(float r)
		{
			if (r < 0.1) r = 0.1f;
			calcImgRect(r);

		}
		//**************************************************************************
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (m_IsEditMode == false)
			{
				if (m_OffScr != null)
				{
					if (m_MD == true) return;
					if (m_AutoFit == false)
					{
						m_MD = true;
						m_MDPos = new Point(e.X, e.Y);
						m_MDLoc = imgRect.Location;
					}
				}
			}
			else
			{
				if (this.Parent != null)
				{
					((HyperControl)this.Parent).CallMouseDown(e);
				}
			}
			base.OnMouseDown(e);
		}
		//**************************************************************************
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (IsEditMode == false)
			{
				if (m_MD == false) return;
				if (m_OffScr != null)
				{
					imgRect.X = m_MDLoc.X + e.X - m_MDPos.X;
					imgRect.Y = m_MDLoc.Y + e.Y - m_MDPos.Y;

					if ((imgRect.Right) <= 8)
					{
						imgRect.X = -imgRect.Width + 8;
					}
					else if (imgRect.X >= (this.Width - 8))
					{
						imgRect.X = (this.Width - 8);
					}
					if ((imgRect.Bottom) <= 8)
					{
						imgRect.Y = -imgRect.Height + 8;
					}
					else if (imgRect.Y >= (this.Height - 8))
					{
						imgRect.Y = (this.Height - 8);
					}
					this.Invalidate();
				}
			}
			else
			{
				if (this.Parent != null)
				{
					((HyperControl)this.Parent).CallMouseMove(e);
				}

			}
			base.OnMouseMove(e);
		}
		//**************************************************************************
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_IsEditMode == false)
			{
				if (m_MD == true)
				{
					m_MD = false;
					this.Invalidate();
				}
			}
			else
			{
				if (this.Parent != null)
				{
					((HyperControl)this.Parent).CallMouseUp(e);
				}

			}
			base.OnMouseUp(e);
		}

		//**************************************************************************
		private void chkSize()
		{
			if (m_OffScr != null)
			{
				imgRect.Width = (int)Math.Round(m_OffScr.Width * m_ratio);
				imgRect.Height = (int)Math.Round(m_OffScr.Height * m_ratio);
			}
		}
		//**************************************************************************
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (m_OffScr != null)
			{
				if (m_AutoFit) Fit();
				this.Invalidate();
			}
		}
		//**************************************************************************
		public bool OpenFile(string path)
		{
			bool ret = false;
			try
			{
				Targa tga = new Targa();
				if (tga.LoadHeader(path) == true)
				{
					m_OffScr = tga.loadTGA(path);
				}
				else
				{
					m_OffScr = new Bitmap(path);
				}
				chkSize();
				m_Path = path;
				if (m_AutoFit) Fit();
				ret = true;
				this.Invalidate();
			}
			catch
			{
				ClearOffscr();
				ret = false;
			}
			return ret;
		}
		//**************************************************************************
		public void ClearOffscr()
		{
			if (m_OffScr == null) return;
			m_OffScr.Dispose();
			m_OffScr = null;
			m_Path = "";
		}

		//**************************************************************************

	}
}
