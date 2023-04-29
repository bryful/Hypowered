using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Drawing.Text;
namespace Hypowered
{
	public class HButton : HControl
	{
		#region Prop
		protected Color m_DownColor = Color.FromArgb(180, 180, 180);
		[Category("Hypowered_Color")]
		public Color DownColor
		{
			get { return m_DownColor; }
			set { m_DownColor = value; this.Invalidate(); }
		}
		protected Color m_CheckedColor = Color.FromArgb(160, 160, 160);
		[Category("Hypowered_Color")]
		public Color CheckedColor
		{
			get { return m_CheckedColor; }
			set { m_CheckedColor = value; this.Invalidate(); }
		}
		protected bool m_Checked = false;
		[Category("Hypowered_Color")]
		public bool Checked
		{
			get { return m_Checked; }
			set { m_Checked = value; this.Invalidate(); }
		}
		protected bool m_IsCheckMode = false;
		[Category("Hypowered_Button")]
		public bool IsCheckMode
		{
			get { return m_IsCheckMode; }
			set { m_IsCheckMode = value; this.Invalidate(); }
		}

		#endregion

		public HButton()
		{
			m_HCType = HCType.Button;
			ScriptCode.Setup(HScriptType.Click);
			TextAlign = StringAlignment.Center;
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(base.BackColor))
			using (Pen p = new Pen(base.ForeColor))
			{
				Graphics g = pe.Graphics;
				if (m_IsAnti)
				{
					g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
					g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
				}

				// 塗り
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);

				if (m_MDPush==true)
				{
					sb.Color = m_DownColor;
				}else if (m_Checked==true)
				{
					sb.Color = m_CheckedColor;
				}
				else
				{
					sb.Color = BackColor;
				}
				Rectangle r = RectInc(this.ClientRectangle, 2);
				g.FillRectangle(sb, r);
				//文字
				if(this.Text!="")
				{
					sb.Color= ForeColor;
					g.DrawString(this.Text, this.Font, sb, r, StringFormat);
				}
				p.Color = ForeColor;
				DrawFrame(g, p, r);

				DrawCtrlRect(g, p);
			}
		}
		protected bool m_MDPush = false;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if(m_IsEdit)
			{
				base.OnMouseDown(e);
			}
			else
			{
				m_MDPush = true;
				if(m_IsCheckMode)
				{
					m_Checked = !m_Checked;
				}
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
				if(HForm!=null)
				{
					HForm.Script.ExecuteCode(ScriptCode.Items(HScriptType.Click));
				}
			}
		}
		// ***************************************************************
		public override JsonObject? ToJson()
		{
			JsonFile? jf = new JsonFile(base.ToJson());

			jf.SetValue(nameof(DownColor), (Color)DownColor);//System.Drawing.Color
			jf.SetValue(nameof(CheckedColor), (Color)CheckedColor);//System.Drawing.Color
			jf.SetValue(nameof(Checked), (Boolean)Checked);//System.Boolean
			jf.SetValue(nameof(IsCheckMode), (Boolean)IsCheckMode);//System.Boolean
			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("DownColor", typeof(Color).Name);
			if (v != null) DownColor = (Color)v;
			v = jf.ValueAuto("CheckedColor", typeof(Color).Name);
			if (v != null) CheckedColor = (Color)v;
			v = jf.ValueAuto("Checked", typeof(Boolean).Name);
			if (v != null) Checked = (Boolean)v;
			v = jf.ValueAuto("IsCheckMode", typeof(Boolean).Name);
			if (v != null) IsCheckMode = (Boolean)v;
		}
	}
}
