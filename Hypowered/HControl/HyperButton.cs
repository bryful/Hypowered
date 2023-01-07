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
	public partial class HyperButton : HyperControl
	{
		protected Color m_PushedColor = Color.White;
		[Category("Hypowered_Color")]
		public Color PushedColor
		{
			get { return m_PushedColor; }
			set { m_PushedColor = value; this.Invalidate(); }
		}
		public HyperButton()
		{
			SetMyType(ControlType.Button);
			SetInScript(InScriptBit.MouseClick);
			m_FrameWeight = new Padding(1, 1, 1, 1);
			this.Location = new Point(100, 100);
			this.Size = ControlDef.DefSize;
			InitializeComponent();
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

				if (this.Text != "")
				{
					StringFormat sf = new StringFormat();
					sf.Alignment = StringAlignment.Center;
					sf.LineAlignment = StringAlignment.Center;
					sb.Color = ForeColor;
					g.DrawString(this.Text, this.Font, sb, ReRect(this.ClientRectangle, 3), sf);
				}



				// 外枠
				Rectangle rr = ReRect(this.ClientRectangle, 2);
				p.Color = ForeColor;
				DrawFrame(g, p, rr);

				if (this.Focused)
				{
					rr = ReRect(this.ClientRectangle, 1);
					p.Color = m_ForcusColor;
					g.DrawRectangle(p, rr);
				}
				DrawType(g, sb);

			}
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
			jf.SetValue(nameof(PushedColor), PushedColor);//Color
			jf.SetValue(nameof(MyType), (int?)MyType);//Nullable`1

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("PushedColor", typeof(Color).Name);
			if (v != null) PushedColor = (Color)v;
		}
	}
}
