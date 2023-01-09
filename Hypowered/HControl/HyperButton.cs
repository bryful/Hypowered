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
	public enum ButtonIconPos
	{
		TopLeft=0,
		Top,
		TopRight,
		Left,
		Center,
		Right,
		BottomLeft,
		Bottom,
		BottomRight
	}
	public partial class HyperButton : HyperControl
	{
		private string m_PictName = "";
		private string m_PictName_Down = "";
		private Bitmap? m_Bitmap = null;
		private Bitmap? m_Bitmap_Down = null;
		[Category("Hypowered_Button")]
		public string PictName
		{
			get { return m_PictName; }
			set
			{
				if (MainForm == null)
				{
					m_PictName = value;
				}
				else
				{
					SetPictName(value,false);
				}
			}

		}
		[Category("Hypowered_Button")]
		public string PictName_Down
		{
			get { return m_PictName_Down; }
			set
			{
				if (MainForm == null)
				{
					m_PictName_Down = value;
				}
				else
				{
					SetPictName(value,true);
				}
			}

		}
		public void SetBitmap(int idx,bool IsDown=false)
		{
			if (MainForm == null) return;
			if (IsDown == false)
			{
				m_Bitmap = MainForm.Lib.GetPictItem(idx).Bitmap;
				if (m_Bitmap != null)
				{
					m_PictName = MainForm.Lib.PictName(idx);
					ChkSize();
				}
			}
			else
			{
				m_Bitmap_Down = MainForm.Lib.GetPictItem(idx).Bitmap;
				if (m_Bitmap_Down != null)
				{
					m_PictName_Down = MainForm.Lib.PictName(idx);
					ChkSize();
				}
			}
		}
		public void SetPictName(string value,bool IsDown=false)
		{
			if ((MainForm == null) || (value == ""))
			{
				if (IsDown == false)
				{
					m_PictName = "";
				}
				else
				{
					m_PictName_Down = "";
				}
				return;
			}
			int idx = MainForm.Lib.IndexOfBitmap(value);
			if (idx >= 0)
			{
				SetBitmap(idx,IsDown);
				ChkSize();
				this.Invalidate();
			}
			else
			{
				int index = -1;
				if (int.TryParse(value, out index))
				{
					if ((index >= 0) && (index < MainForm.Lib.BitmapCount))
					{
						SetBitmap(index,IsDown);
						this.Invalidate();
					}
				}
			}
		}
		private Rectangle m_BitmapRect = new Rectangle(0, 0, 0, 0);
		private Rectangle m_BitmapRect_Down = new Rectangle(0, 0, 0, 0);

		private ButtonIconPos m_ButtonIconPos = ButtonIconPos.Center;
		[Category("Hypowered_Button")]
		public ButtonIconPos ButtonIconPos
		{
			get { return m_ButtonIconPos; }
			set 
			{
				m_ButtonIconPos = value; 
				ChkSize();
				this.Invalidate();
			}
		}

		protected Color m_PushedColor = Color.White;
		[Category("Hypowered_Color")]
		public Color PushedColor
		{
			get { return m_PushedColor; }
			set { m_PushedColor = value; this.Invalidate(); }
		}
		public HyperButton()
		{
			SetControlType(Hypowered.ControlType.Button);
			SetInScript(InScriptBit.MouseClick);
			this.TextAligiment = StringAlignment.Center;
			this.TextLineAligiment = StringAlignment.Center;

			m_FrameWeight = new Padding(1, 1, 1, 1);
			this.Location = new Point(100, 100);
			this.Size = ControlDef.DefSize;
			InitializeComponent();
		}
		public void ChkSize()
		{
			if (m_Bitmap_Down != null) ChkSizeSub(true);
			if (m_Bitmap != null) ChkSizeSub(false);
		}
		public void ChkSizeSub(bool IsDown)
		{
			Bitmap? bmp = null;
			Rectangle? rect = null;
			if (IsDown)
			{
				bmp = m_Bitmap_Down;
				rect = m_BitmapRect_Down;
			}
			else
			{
				bmp = m_Bitmap;
				rect = m_BitmapRect;
			}
			if (bmp != null)
			{
				Rectangle rr = ReRect(this.ClientRectangle, 3);
				if ((rr.Width >= bmp.Width) && (rr.Height >= bmp.Height))
				{
					switch (m_ButtonIconPos)
					{
						case ButtonIconPos.TopLeft:
							rect = new Rectangle(
								rr.Left,
								rr.Top,
								bmp.Width, bmp.Height);
							break;
						case ButtonIconPos.Top:
							rect = new Rectangle(
								rr.Left + rr.Width / 2 - bmp.Width / 2,
								rr.Top,
								bmp.Width, bmp.Height);
							break;
						case ButtonIconPos.TopRight:
							rect = new Rectangle(
								rr.Left+rr.Width - bmp.Width,
								rr.Top,
								bmp.Width, bmp.Height);
							break;
						case ButtonIconPos.Left:
							rect = new Rectangle(
								rr.Left,
								rr.Top + rr.Height / 2 - bmp.Height / 2,
								bmp.Width, bmp.Height);
							break;
						case ButtonIconPos.Center:
							rect = new Rectangle(
								rr.Left + rr.Width / 2 - bmp.Width / 2,
								rr.Top + rr.Height / 2 - bmp.Height / 2,
								bmp.Width, bmp.Height);
							break;
						case ButtonIconPos.Right:
							rect = new Rectangle(
								rr.Left + rr.Width - bmp.Width,
								rr.Top + rr.Height / 2 - bmp.Height / 2,
								bmp.Width, bmp.Height);
							break;
						case ButtonIconPos.BottomLeft:
							rect = new Rectangle(
								rr.Left,
								rr.Top + rr.Height - bmp.Height,
								bmp.Width, bmp.Height);
							break;
						case ButtonIconPos.Bottom:
							rect = new Rectangle(
								rr.Left + rr.Width / 2 - bmp.Width / 2,
								rr.Top + rr.Height - bmp.Height,
								bmp.Width, bmp.Height);
							break;
						case ButtonIconPos.BottomRight:
							rect = new Rectangle(
								rr.Left + rr.Width - bmp.Width,
								rr.Top + rr.Height - bmp.Height,
								bmp.Width, bmp.Height);
							break;
					}
				}
				else
				{
					float scale = (float)rr.Width / (float)bmp.Width;
					float scaleH = (float)rr.Height / (float)bmp.Height;
					if (scale > scaleH) scale = scaleH;
					int nW = (int)((float)bmp.Width * scale);
					int nH = (int)((float)bmp.Height * scale);
					rect = new Rectangle(
						rr.Left + rr.Width / 2 - nW / 2,
						rr.Top + rr.Height / 2 - nH / 2,
						nW,
						nH
						);
				}
				if (rect != null)
				{
					if (IsDown)
					{
						//bmp = m_Bitmap_Down;
						m_BitmapRect_Down = (Rectangle)rect;
					}
					else
					{
						//bmp = m_Bitmap;
						m_BitmapRect = (Rectangle)rect;
					}

				}
			}
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			

			using (SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = pe.Graphics;
				// 背景色
				sb.Color = BackColor;
				g.FillRectangle(sb, this.ClientRectangle);
				if (m_MPush)
				{
					sb.Color = m_PushedColor;
					g.FillRectangle(sb, ReRect(this.ClientRectangle, 4));
				}
				if ((m_MPush == false)||((m_PictName_Down == "")&&(m_PictName!="")))
				{
					if ((m_Bitmap == null) && (m_PictName != ""))
					{
						SetPictName(m_PictName);
					}
					if (m_Bitmap != null)
					{
						g.DrawImage(m_Bitmap, m_BitmapRect);
					}
				}
				if(m_MPush == true)
				{
					if ((m_Bitmap_Down == null) && (m_PictName_Down != ""))
					{
						SetPictName(m_PictName_Down,true);
					}
					if (m_Bitmap_Down != null)
					{
						g.DrawImage(m_Bitmap_Down, m_BitmapRect);
					}
				}
				if (this.Text != "")
				{
					sb.Color = ForeColor;
					g.DrawString(this.Text, this.Font, sb, ReRect(this.ClientRectangle, 6), m_format);
				}



				// 外枠
				if (m_IsDrawFrame)
				{
					Rectangle rr = ReRect(this.ClientRectangle, 2);
					p.Color = ForeColor;
					DrawFrame(g, p, rr);
				}

				if ((this.Focused)&&(m_IsDrawFocuse))
				{
					Rectangle rr = ReRect(this.ClientRectangle, 1);
					p.Color = m_ForcusColor;
					g.DrawRectangle(p, rr);
				}
				DrawEditMode(g, p, sb);


			}
		}
		// *********************************************************************
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
			this.Invalidate();
		}       
		// *********************************************************************
		protected bool m_MPush = false;
		// *********************************************************************
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (m_IsEditMode == false)
			{
				m_MPush = true;
				this.Invalidate();
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_MPush == true)
			{
				m_MPush = false;
				this.Invalidate(true);
			}
			base.OnMouseUp(e);
		}
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			if ((MainForm != null) && (m_IsEditMode == false))
			{
				MainForm.ExecuteCode(GetScriptCode(ScriptKind.MouseClick));
			}
		}
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(ControlType), (int?)ControlType);//Nullable`1
			jf.SetValue(nameof(PushedColor), PushedColor);//Color
			jf.SetValue(nameof(PictName), PictName);//Color
			jf.SetValue(nameof(PictName_Down), PictName_Down);//Color
			jf.SetValue(nameof(ButtonIconPos), (Int32)ButtonIconPos);//Color

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("PushedColor", typeof(Color).Name);
			if (v != null) PushedColor = (Color)v;
			v = jf.ValueAuto("PictName", typeof(String).Name);
			if (v != null) PictName = (String)v;
			v = jf.ValueAuto("PictName_Down", typeof(String).Name);
			if (v != null) PictName_Down = (String)v;
			v = jf.ValueAuto("ButtonIconPos", typeof(Int32).Name);
			if (v != null) ButtonIconPos = (ButtonIconPos)v;
		}
	}
}
