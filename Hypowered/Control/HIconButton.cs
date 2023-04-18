using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Hypowered
{
	public class HIconButton :HControl
	{
		#region Prop
		protected Bitmap? m_Bitmap = null;
		protected string m_PictName = "";
		[Category("Hypowered")]
		public string PictName
		{
			get { return m_PictName; }
			set 
			{ 
				m_PictName = value;
				LoadBmp();
				this.Invalidate();
			}
		}

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
		public void LoadBmp()
		{
			if(m_PictName=="")
			{
				m_Bitmap = null;
				return;
			}
			if(HForm==null) { return; }
			Bitmap? bmp = HForm.GetBitmapFromLib(m_PictName);
			if(bmp == null) { m_Bitmap = null; return; }
			m_Bitmap = new Bitmap(m_IconWidth, m_IconHeight,PixelFormat.Format32bppArgb);
			ItemsLib.ResizaDraw(bmp, m_Bitmap);
		}
		// *********************************************************
		private int TryCount = 0;
		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(base.BackColor))
			using (Pen p = new Pen(base.ForeColor))
			{
				Graphics g = pe.Graphics;
				// 塗り
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);

				if((m_Bitmap != null)&&(m_PictName!=""))
				{
					if (TryCount > 100)
					{
						m_PictName = "";
						TryCount = 0;
					}
					else
					{
						LoadBmp();
						TryCount++;
					}
				}
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
				// IsEdit
				DrawIsEdit(g, p);
			}
		}
		protected bool m_MDPush = false;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (IsAltKey)
			{
				SetIsEdit(true);
				this.Invalidate();
				if (HForm != null)
				{
					HForm.TargetIndex = this.Index;
					HForm.Invalidate();
				}
				return;
			}
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
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			if (m_IsEdit)
			{
				if((this.HForm!=null)&&(this.HForm.MainForm!=null))
				{
					string s =this.HForm.MainForm.ShowPictItemDialog(this.HForm,PictName);
					if(s != "") 
					{
						PictName = s;
					}
				}
			}
			else
			{
				base.OnMouseDoubleClick(e);
			}
		}
		// ************************************************************
		// ***************************************************************
		public override JsonObject? ToJson()
		{
			JsonFile? jf = new JsonFile(base.ToJson());


			jf.SetValue(nameof(PictName), (String)PictName);//System.String
			jf.SetValue(nameof(IconWidth), (Int32)IconWidth);//System.Int32
			jf.SetValue(nameof(IconHeight), (Int32)IconHeight);//System.Int32
			jf.SetValue(nameof(TextWidth), (Int32)TextWidth);//System.Int32
			jf.SetValue(nameof(TextHeight), (Int32)TextHeight);//System.Int32
			jf.SetValue(nameof(DownColor), (Color)DownColor);//System.Drawing.Color
			jf.SetValue(nameof(TextBackColor), (Color)TextBackColor);//System.Drawing.Color

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("PictName", typeof(String).Name);
			if (v != null) PictName = (String)v;
			v = jf.ValueAuto("IconWidth", typeof(Int32).Name);
			if (v != null) IconWidth = (Int32)v;
			v = jf.ValueAuto("IconHeight", typeof(Int32).Name);
			if (v != null) IconHeight = (Int32)v;
			v = jf.ValueAuto("TextWidth", typeof(Int32).Name);
			if (v != null) TextWidth = (Int32)v;
			v = jf.ValueAuto("TextHeight", typeof(Int32).Name);
			if (v != null) TextHeight = (Int32)v;
			v = jf.ValueAuto("DownColor", typeof(Color).Name);
			if (v != null) DownColor = (Color)v;
			v = jf.ValueAuto("TextBackColor", typeof(Color).Name);
			if (v != null) TextBackColor = (Color)v;
		}
	}
}
