using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class HyperPictureBox : HyperControl
	{
		[Category("Hypowered")]
		public new bool IsEditMode
		{
			get { return base.m_IsEditMode; }
			set
			{
				base.IsEditMode = value;
				this.Invalidate();
			}
		}
		[Category("Hypowered_PictureBox")]
		public new String FileName
		{
			get { return base.FileName; }
			set
			{
				base.FileName = value;
				if (File.Exists(base.FileName))
				{
					OpenFile(base.FileName);
				}
				else
				{
					ClearOffscr();
				}
			}
		}
		protected Color m_BaseColor = Color.Transparent;
		protected Color m_LineColor = Color.DimGray;

		[Category("Hypowered_Color")]
		public Color BaseColor
		{
			get { return m_BaseColor; }
			set { m_BaseColor = value;}
		}
		[Category("Hypowered_Color")]
		public Color LineColor
		{
			get { return m_LineColor; }
			set 
			{
				m_LineColor = value;
				Invalidate();
			}
		}
		
		[Category("Hypowered_PictureBox")]
		public Bitmap? Bitmap
		{
			get { return m_OffScr; }
			set
			{
				if (value == null)
				{
					ClearOffscr();
					return;
				}
				m_OffScr = new Bitmap(value.Width, value.Height, value.PixelFormat);
				Graphics g = Graphics.FromImage(m_OffScr);
				g.DrawImage(value, 0, 0);
				ChkSize();
				FileName= "";
			}
		}
		public void Reload()
		{
			if (FileName!="")
			{
				OpenFile(FileName);
				Invalidate();
			}
		}
		
		[Category("Hypowered_PictureBox")]
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
		protected bool m_AutoFit = true;
		[Category("Hypowered_PictureBox")]
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

		protected Bitmap? m_OffScr = null;
		protected float m_ratio = 1F;
		protected Point m_MDPosA = new Point(0, 0);
		protected Point m_MDLocA = new Point(0, 0);
		protected bool m_MDA = false;
		protected Rectangle imgRect = new Rectangle(0, 0, 0, 0);
		protected Rectangle imgRectBak = new Rectangle(0, 0, 0, 0);

		public HyperPictureBox()
		{
			AllowDrop= true;
			SetMyType(ControlType.PictureBox);
			ScriptCode.SetInScript(InScriptBit.MouseDoubleClick);
			BackColor = Color.Transparent;
			this.Size = new Size(306, 306);
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
		private void ChkSize()
		{
			if (m_OffScr != null)
			{
				imgRect.Width = (int)Math.Round(m_OffScr.Width * m_ratio);
				imgRect.Height = (int)Math.Round(m_OffScr.Height * m_ratio);
			}
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			using (Pen p = new Pen(LineColor))
			using (SolidBrush b = new SolidBrush(BaseColor))
			{
				b.Color = BaseColor;
				g.FillRectangle(b, new Rectangle(0, 0, this.Width, this.Height));
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
					//画像を指定された範囲に描画する
					g.DrawImage(m_OffScr, imgRect);
					//if (BorderStyle != BorderStyle.None)
					//{
						p.Width = 1;
						p.Color = LineColor;
						g.DrawRectangle(p, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
					//}
				}
				if (m_IsEditMode)
				{
					base.OnPaint(pe);
				}
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
				ChkSize();
				base.FileName = path;
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
			FileName = "";
			this.Invalidate();
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
			if (m_OffScr != null)
			{
				if (m_AutoFit) Fit();
				this.Invalidate();
			}
		}
		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			if (drgevent == null) return;
			if ((drgevent.Data.GetDataPresent(DataFormats.FileDrop))&&(m_IsEditMode==false))
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
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (m_IsEditMode == false)
			{
				if (m_OffScr != null)
				{
					if (m_MDA == true) return;
					if (m_AutoFit == false)
					{
						m_MDA = true;
						m_MDPosA = new Point(e.X, e.Y);
						m_MDLocA = imgRect.Location;
					}
				}
			}

			base.OnMouseDown(e);
		}
		//**************************************************************************
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (IsEditMode == false)
			{
				if (m_MDA == false) return;
				if (m_OffScr != null)
				{
					imgRect.X = m_MDLocA.X + e.X - m_MDPosA.X;
					imgRect.Y = m_MDLocA.Y + e.Y - m_MDPosA.Y;

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
			base.OnMouseMove(e);
		}
		//**************************************************************************
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_IsEditMode == false)
			{
				if (m_MDA == true)
				{
					m_MDA = false;
					this.Invalidate();
				}
			}
			base.OnMouseUp(e);
		}

		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			if ((drgevent == null)||(m_IsEditMode==true)) return;
			string[] fileName =
			(string[])drgevent.Data.GetData(DataFormats.FileDrop, false);

			foreach (var s in fileName)
			{
				if (OpenFile(s))
				{
					break;
				}
			}
			base.OnDragDrop(drgevent);
		}
		protected override void OnDoubleClick(EventArgs e)
		{
			if (m_IsEditMode == false)
			{
				Fit();
			}
			base.OnDoubleClick(e);
		}



		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(MyType), (int?)MyType);//Nullable`1
			jf.SetValue(nameof(BaseColor), BaseColor);//Color
			jf.SetValue(nameof(LineColor), BaseColor);//Color
			if (IsSaveFileName)
			{
				jf.SetValue(nameof(FileName), FileName);//Color
			}
			jf.SetValue(nameof(AutoFit), AutoFit);//Color
			jf.SetValue(nameof(Ratio), Ratio);//Color
			//jf.SetValue(nameof(BorderStyle), (int)BorderStyle);//Color

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("BaseColor", typeof(Color).Name);
			if (v != null) BaseColor = (Color)v;
			if (IsSaveFileName)
			{
				v = jf.ValueAuto("FileName", typeof(string).Name);
				if (v != null) FileName = (string)v;
			}
			v = jf.ValueAuto("LineColor", typeof(Color).Name);
			if (v != null) LineColor = (Color)v;
			v = jf.ValueAuto("AutoFit", typeof(Boolean).Name);
			if (v != null) AutoFit = (Boolean)v;
			v = jf.ValueAuto("Ratio", typeof(float).Name);
			if (v != null) Ratio = (float)v;
			//v = jf.ValueAuto("BorderStyle", typeof(Int32).Name);
			//if (v != null) BorderStyle = (BorderStyle)v;
			
		}
	}
}
