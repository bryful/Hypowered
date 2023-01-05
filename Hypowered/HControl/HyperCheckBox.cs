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
using static System.Windows.Forms.DataFormats;

namespace Hypowered
{

    public partial class HyperCheckBox : HyperControl
	{
		public delegate void CheckedChangedHandler(object sender, CheckedChangedEventArgs e);
		public event CheckedChangedHandler? CheckedChanged;
		protected virtual void OnCheckedChanged(CheckedChangedEventArgs e)
		{
			if (CheckedChanged != null)
			{
				CheckedChanged(this, e);
			}
			if((MainForm != null))
			{
				MainForm.ExecuteCode(Script_ValueChanged);
			}
		}
		private bool m_Checked = true;
		[Category("Hypowered_CheckBox")]
		public bool Checked
		{
			get { return m_Checked; }
			set 
			{ 
				if(m_Checked != value)
				{
					m_Checked = value;
					OnCheckedChanged(new CheckedChangedEventArgs(m_Checked));
				}
				this.Invalidate(); 
			}
		}
		private int m_CheckSize;
		[Category("Hypowered_CheckBox")]
		public int CheckSize
		{
			get { return m_CheckSize; }
			set { m_CheckSize = value; this.Invalidate(); }
		}

		public HyperCheckBox()
		{
			SetMyType(ControlType.CheckBox);
			SetInScript(InScriptBit.ValueChanged);
			m_CheckSize = 16;
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
				g.FillRectangle(sb, this.ClientRectangle);

				Rectangle r = new Rectangle(3,(this.Height-m_CheckSize)/2, m_CheckSize,m_CheckSize);
				p.Width = 1;
				g.DrawRectangle(p, r);
				if(m_Checked)
				{
					sb.Color= ForeColor;
				}
				else
				{
					sb.Color = m_UnCheckedColor;
				}
				RectangleF rs = ReRectF(r, 5);
				g.FillRectangle(sb, rs);

				// 外枠
				Rectangle rr = ReRect(this.ClientRectangle, 2);
				//p.Color = ForeColor;
				//g.DrawRectangle(p, rr);
				if (this.Focused)
				{
					rr = ReRect(this.ClientRectangle, 1);
					p.Color = m_ForcusColor;
					g.DrawRectangle(p, rr);
				}
				if (this.Text != "")
				{
					sb.Color = ForeColor;
					rr = new Rectangle(m_CheckSize + 5, 3, this.Width - m_CheckSize - 5, this.Height - 6);
					g.DrawString(this.Text, this.Font, sb, rr, m_format);
				}
				DrawType(g, sb);

			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (m_IsEditMode)
			{
				if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
				{
					ChkTargetSelected();

					MDPos p = CU.GetMDPos(e.X, e.Y, this.Size);
					if (p != MDPos.None)
					{
						m_MDPos = p;
						m_MDP = new Point(e.X, e.Y);
						m_MDLoc = this.Location;
						m_MDSize = this.Size;
						return;
					}
				}
			}
			else
			{
				
				m_Checked = !m_Checked;
				OnCheckedChanged(new CheckedChangedEventArgs(m_Checked));
				this.Invalidate();
				return;
				
			}
			base.OnMouseDown(e);
		}
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(MyType), (int?)MyType);//Nullable`1
			jf.SetValue(nameof(Checked), Checked);//Boolean
			jf.SetValue(nameof(CheckSize), CheckSize);//Int32

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("Checked", typeof(bool).Name);
			if (v != null) Checked = (bool)v;
			v = jf.ValueAuto("CheckSize", typeof(Int32).Name);
			if (v != null) CheckSize = (int)v;
		}
	}
}
