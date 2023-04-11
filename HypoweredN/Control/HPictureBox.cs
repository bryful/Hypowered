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
		protected Bitmap? m_Buffer = null;
		// *****************************************************************************

		public HPictureBox()
		{
			m_HType = HType.PictureBox;
			this.AllowDrop = true;
			ChkGridSize();
			InitOffScr();
		}
		// *****************************************************************************
		public void InitOffScr()
		{
			int w = this.Width - 4;
			if (w < 8) w = 8;
			int h = this.Height - 4;
			if (h < 8) h = 8;

			if ((w  != m_OffScr.Width)|| (h != m_OffScr.Height))
			{
				m_OffScr = new Bitmap(w, h, PixelFormat.Format32bppArgb);
				using(SolidBrush sb = new SolidBrush(Color.Transparent))
				{
					Graphics g = Graphics.FromImage(m_OffScr);
					g.FillRectangle(sb, new Rectangle(0, 0, m_OffScr.Width, m_OffScr.Height));

				}
			}
			if (m_Buffer != null)
			{
				Graphics g = Graphics.FromImage(m_OffScr);
				g.DrawImage(m_Buffer, 0, 0);
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

				g.DrawImage(m_OffScr, 2, 2);

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
						if (LoadFile(d) == true)
							break;
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
		private void ColorAt(Bitmap bitmap,Color col)
		{
			BitmapData data = bitmap.LockBits(
				new Rectangle(0, 0, bitmap.Width, bitmap.Height),
				ImageLockMode.ReadWrite,
				PixelFormat.Format32bppArgb);
			int bytes = bitmap.Width * bitmap.Height * 4;
			Int32 a = ((Int32)col.B) << 16 | ((Int32)col.G) << 8 | ((Int32)col.R);
			for (int i = 0; i < bytes; i += 4)
			{
				Int32 value = Marshal.ReadInt32(data.Scan0, i);

				Color c = Color.FromArgb(value);
				c = Color.FromArgb(c.A,col.R,col.G,col.B);
				Marshal.WriteInt32(data.Scan0, i, c.ToArgb());
			}
			bitmap.UnlockBits(data);
		}
		// *****************************************************************************
		public bool LoadFile(string filename)
		{
			bool ret = false;
			if(File.Exists(filename)==false) return ret;
			try
			{
				string e = Path.GetExtension(filename).ToLower();
				m_Buffer = null;
				bool IsSvg = (e == ".svg");
				if (IsSvg)
				{
					SvgDocument svgDoc = SvgDocument.Open(filename);
					m_Buffer = svgDoc.Draw();
					ColorAt(m_Buffer, Color.Red);
				}
				else
				{
					using (var myMagick = new ImageMagick.MagickImage(filename))
					{
						if (IsSvg) myMagick.Transparent(MagickColors.White);
						m_Buffer = myMagick.ToBitmap(); //Bitmapへ変換
					
					}
				}
				if(m_Buffer!=null)
				{
					Graphics g = Graphics.FromImage(m_OffScr);
					g.Clear(Color.Transparent);
					g.DrawImage(m_Buffer, 0, 0);
					this.Invalidate();
					ret = true;
				}
			}
			catch
			{
				ret = false;
			}

			return ret;
		}
		// *****************************************************************************
	}
}
