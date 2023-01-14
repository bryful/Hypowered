using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class HyperIcon : HyperControl
	{
		private string m_PictName = "";
		private Bitmap? m_Bitmap = null;
		[Category("Hypowered_Icon")]
		public string PictName
		{
			get { return m_PictName;}
			set
			{
				if(MainForm==null)
				{
					m_PictName=value;
				}
				else
				{
					SetPictName(value);
				}
			}

		}
		public void SetBitmap(int idx)
		{
			if (MainForm == null) return;
			m_Bitmap = MainForm.Lib.GetPictItem(idx).Bitmap;
			if (m_Bitmap != null)
			{
				m_PictName = MainForm.Lib.PictName(idx);
				this.Size = new Size(m_Bitmap.Width + 4, m_Bitmap.Height + 4);
				ChkSize();
			}
		}
		public void SetPictName(string value)
		{
			if ((MainForm == null) || (value == ""))
			{
				m_PictName = "";
				return;
			}
			int idx = MainForm.Lib.IndexOfBitmap(value);
			if (idx >= 0)
			{
				SetBitmap(idx);
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
						SetBitmap(index);
						this.Invalidate();
					}
				}
			}
		}
		private Rectangle m_BitmapRect = new Rectangle(0,0,0,0);
		/*
		protected bool m_IsDrawFrame = false;
		[Category("Hypowered")]
		public bool IsDrawFrame
		{
			get { return m_IsDrawFrame; }
			set { m_IsDrawFrame = value; this.Invalidate(); }
		}
		*/
		public HyperIcon()
		{
			SetControlType(Hypowered.ControlType.Icon);
			ScriptCode.SetInScript(InScriptBit.MouseClick | InScriptBit.MouseDoubleClick | InScriptBit.DragDrop);
			BackColor = Color.Transparent;
			FrameWeight = new Padding(1,1,1,1);
			this.Location = new Point(150, 150);
			this.Size = new Size(32+4,32+4);
			this.TabStop= false;
			//this.MinimumSize = new Size(32 + 4, 32 + 4);
			//this.MaximumSize = new Size(32 + 4, 32 + 4);
			this.TabStop= false;
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

		public void ChkSize()
		{
			if (m_Bitmap != null)
			{
				m_BitmapRect = new Rectangle(
					this.Width / 2 - m_Bitmap.Width / 2,
					this.Height / 2 - m_Bitmap.Height / 2,
					m_Bitmap.Width,
					m_Bitmap.Height
					);
			}
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			if((m_Bitmap == null)&&(m_PictName!=""))
			{
				SetPictName(m_PictName);
			}
			using (SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = pe.Graphics;
				// 背景色
				sb.Color = BackColor;
				g.FillRectangle(sb, this.ClientRectangle);

				if(m_Bitmap != null)
				{
					g.DrawImage(m_Bitmap, m_BitmapRect);
				}



				// 外枠
				if (m_IsDrawFrame) {
					Rectangle r1 = ReRect(this.ClientRectangle, 2);
					p.Color = ForeColor;
					DrawFrame(g, p, r1);
				}
				if ((this.Focused) && (m_IsDrawFocuse))
				{
					Rectangle r2 = ReRect(this.ClientRectangle, 1);
					p.Color = m_ForcusColor;
					g.DrawRectangle(p, r2);
				}
				if(m_IsEditMode)
				{
					p.Color = ForeColor;
					p.Width = 1;
					p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
					g.DrawRectangle(p,new Rectangle(0,0,this.Width-1,this.Height-1));
				}
				DrawEditMode(g, p, sb);


			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
		}
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			ExecScript(ScriptKind.MouseClick);
		}
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			ExecScript(ScriptKind.MouseDoubleClick);
		}
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(ControlType), (int?)ControlType);//Nullable`1
			jf.SetValue(nameof(PictName), PictName);//Color
			jf.SetValue(nameof(IsDrawFrame), IsDrawFrame);//Color

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("PictName", typeof(String).Name);
			if (v != null) PictName = (String)v;
			v = jf.ValueAuto("IsDrawFrame", typeof(Boolean).Name);
			if (v != null) IsDrawFrame = (Boolean)v;
		}
	}
}
