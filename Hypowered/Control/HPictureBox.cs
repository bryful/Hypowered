using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Svg;
using ImageMagick;
using System.Runtime.InteropServices;
namespace Hypowered
{
	public class HPictureBox : HControl
	{
		// *****************************************************************************
		protected Bitmap m_OffScr = new Bitmap(100,100,PixelFormat.Format32bppArgb);
		protected Bitmap? m_Bitmap = null;
		// *****************************************************************************
		private string m_PictName = "";
		private string m_FileName = "";
		public string PictName
		{
			get { return m_PictName; }
			set
			{
				if (this.HForm != null)
				{
					Bitmap? b = this.HForm.GetBitmapFromLib(value);
					if (b != null)
					{
						m_PictName = value;
						m_FileName = "";
						m_Bitmap = b;
						ItemsLib.ResizaDraw(m_Bitmap, m_OffScr);
						this.Invalidate();
					}
				}
			}
		}
		public string FileName
		{
			get { return m_FileName; }
			set
			{
				Bitmap? b = ItemsLib.LoadBitmap(value);
				if(b != null)
				{
					m_FileName = value;
					m_PictName = "";
					m_Bitmap = b;
					ItemsLib.ResizaDraw(m_Bitmap, m_OffScr);
					this.Invalidate();
				}
			}
		}
		private void InitOffScr()
		{
			int w = this.Width - 6;
			int h = this.Height - 6;
			if ((m_OffScr.Width!=w)|| (m_OffScr.Height != w))
			{
				m_OffScr = new Bitmap(w, h, PixelFormat.Format32bppArgb);
				Graphics g = Graphics.FromImage(m_OffScr);
				g.Clear(Color.Transparent);
			}
			ChkBitmap();
			if (m_Bitmap!=null)
			{
				ItemsLib.ResizaDraw(m_Bitmap, m_OffScr);
			}
			this.Invalidate();
		}
		public HPictureBox()
		{
			m_HType = HType.PictureBox;
			this.AllowDrop = true;
			ChkGridSize();
			InitOffScr();
		}
		// *****************************************************************************
		private void ChkBitmap()
		{
			if(m_Bitmap==null)
			{
				if(m_FileName!="")
				{
					FileName = m_FileName;
				}else if (m_PictName!=null)
				{
					PictName = m_PictName;
				}
			}
		}
		// *****************************************************************************
		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(base.BackColor))
			using (Pen p = new Pen(base.ForeColor))
			{
				Graphics g = pe.Graphics;
				// 塗り
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);

				ChkBitmap();
				g.DrawImage(m_OffScr, 3, 3);


				p.Color = ForeColor;
				Rectangle rr = RectInc(this.ClientRectangle, 2);
				DrawFrame(g, p, rr, 1);

				if (Focused) 
				{
					p.Color = m_ForcusColor;
					DrawFrame(g, p, this.ClientRectangle, 2);
				}
				DrawIsEdit(g, p);
			}
		}
		// *****************************************************************************

		// *****************************************************************************
		protected override void OnResize(EventArgs e)
		{
			ChkGridSize();
			InitOffScr();
			this.Invalidate();
		}
		// *****************************************************************************
		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			if ((drgevent != null) && (drgevent.Data != null))
			{
				if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
				{
					drgevent.Effect = DragDropEffects.Copy;
				}
			}
			else
			{
				base.OnDragEnter(drgevent);
			}
		}
		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			if ((drgevent != null) && (drgevent.Data != null))
			{
				if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
				{

					// ドラッグ中のファイルやディレクトリの取得
					string[] drags = (string[])drgevent.Data.GetData(DataFormats.FileDrop);
					foreach (string d in drags)
					{
						FileName = d;
						if (m_FileName != "") break;
					}
					drgevent.Effect = DragDropEffects.Copy;
				}
			}
			else
			{
				base.OnDragDrop(drgevent);
			}
		}

		// *****************************************************************************
	}
}
