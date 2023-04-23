using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Svg;
using ImageMagick;
using System.Runtime.InteropServices;
using System.Drawing.Text;
namespace Hypowered
{
	public class HPictureBox : HControl
	{
		#region Prop
		// *****************************************************************************
		protected Bitmap m_OffScr = new Bitmap(100,100,PixelFormat.Format32bppArgb);
		protected Bitmap? m_Bitmap = null;
		// *****************************************************************************
		private string m_PictName = "";
		private string m_FileName = "";
		[Category("Hypowered"), Browsable(true)]
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
		[Category("Hypowered"), Browsable(true)]
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
		#endregion
		private void InitOffScr()
		{
			int w = this.Width - 6;
			int h = this.Height - 6;
			if ((m_OffScr.Width!=w)|| (m_OffScr.Height != w))
			{
				m_OffScr = new Bitmap(w, h, PixelFormat.Format32bppArgb);
				using (Graphics g = Graphics.FromImage(m_OffScr))
				{
					g.Clear(Color.Transparent);
				}
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
			ScriptCode.Setup(HScriptType.ValueChanged);
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
				if (m_IsAnti)
				{
					g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
					g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				}

				// 塗り
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);

				ChkBitmap();
				g.DrawImage(m_OffScr, 3, 3);


				p.Color = ForeColor;
				Rectangle rr = RectInc(this.ClientRectangle, 2);
				DrawFrame(g, p, rr, 1);

				DrawCtrlRect(g, p);
			}
		}
		// *****************************************************************************

		// *****************************************************************************
		protected override void OnResize(EventArgs e)
		{
			if (_RefFlag) return;
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
		// ***************************************************************
		public override JsonObject? ToJson()
		{
			JsonFile? jf = new JsonFile(base.ToJson());
			if(m_PictName !="")
				jf.SetValue(nameof(PictName), (String)PictName);//System.String
			if (m_FileName != "")
				jf.SetValue(nameof(FileName), (String)FileName);//System.String

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("PictName", typeof(String).Name);
			if (v != null) m_PictName = (String)v;
			v = jf.ValueAuto("FileName", typeof(String).Name);
			if (v != null) m_FileName = (String)v;

		}
	}
}
