using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Hypowered.HArgs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace Hypowered
{

    public partial class HyperDropdownList : HyperControl
	{
		public delegate void SelectedIndexChangedHandler(object sender, SelectedIndexChangedEventArgs e);
		public event SelectedIndexChangedHandler? SelectedIndexChanged;
		protected virtual void OnSelectedIndexChanged(SelectedIndexChangedEventArgs e)
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, e);
			}
			if ((MainForm != null)&&(Script_SelectedIndexChanged!=""))
			{
				MainForm.Script.AddScriptObject("value", e.Value);
				MainForm.ExecuteScript(ScriptCode, ScriptKind.SelectedIndexChanged);
				MainForm.Script.DeleteScriptObject("value");
			}
		}
		public string[] ToArray()
		{
			List<string> list = new List<string>();
			if(m_Items.Count>0)
			{
				foreach(string s in m_Items)
				{
					list.Add(s);
				}
			}
			return list.ToArray();
		}
		public void FromArray(string[] arr)
		{
			m_Items.Clear();
			m_Items.AddRange(arr);
		}
		[Category("Hypowered_DropdownList")]
		public new string [] Lines
		{
			get 
			{ 
				return ToArray(); 
			}
			set
			{
				SelectedIndex = -1;
				FromArray(value);
			}
		}
		private int m_SelectedIndex = -1;
		[Category("Hypowered_DropdownList")]
		public int SelectedIndex
		{
			get { return m_SelectedIndex; }
			set 
			{ 
				SetSelectedIndex(value);
			}
		}
		[Category("Hypowered_DropdownList")]
		public string SelectedItem
		{
			get 
			{
				string ret = string.Empty;
				if((m_SelectedIndex>=0)&&(m_SelectedIndex<m_Items.Count))
				{
					ret  = m_Items[m_SelectedIndex].ToString();
				}
				return ret; 
			}
		}
		public void SetSelectedIndex(int idx)
		{
			if (idx < -1) idx = -1;
			if (idx >= m_Items.Count) idx = m_Items.Count - 1;
			if (m_SelectedIndex != idx)
			{
				m_SelectedIndex = idx;
				string s = "";
				if((m_SelectedIndex>=0)&&(m_SelectedIndex < m_Items.Count))
				{
					s = m_Items[m_SelectedIndex].ToString();
				}
				OnSelectedIndexChanged(new SelectedIndexChangedEventArgs(idx,s));
			}
			this.Invalidate();
		}
		private StringCollection m_Items = new StringCollection();
		[Category("Hypowered_DropdownList")]
		public StringCollection Items
		{
			get { return m_Items; }
			set
			{
				m_Items = value;
			}
		}

		public HyperDropdownList()
		{
			SetControlType(Hypowered.ControlType.DropdownList);
			SetInScript(InScriptBit.SelectedIndexChanged | InScriptBit.DragDrop);
			//m_ScriptCodes = "//DropdownList";
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

				if (m_SelectedIndex>=0)
				{
					StringFormat sf = new StringFormat();
					sf.Alignment = StringAlignment.Near;
					sf.LineAlignment = StringAlignment.Center;
					sb.Color = ForeColor;
					g.DrawString(
						m_Items[m_SelectedIndex], 
						this.Font, 
						sb, 
						ReRect(this.ClientRectangle, 3), sf);
				}

				// 外枠
				Rectangle rr = ReRect(this.ClientRectangle, 2);
				p.Color = ForeColor;
				g.DrawRectangle(p, rr);

				if ((this.Focused)&&(m_IsDrawFocuse))
				{
					rr = ReRect(this.ClientRectangle, 1);
					p.Color = m_ForcusColor;
					g.DrawRectangle(p, rr);
				}
				DrawEditMode(g, p, sb);


			}
		}
		public ContextMenuStrip MakeMenu()
		{
			ContextMenuStrip ret = new ContextMenuStrip();
			if (m_Items.Count > 0)
			{
				int idx = 0;
				foreach (string? mi in m_Items)
				{
					if (mi == null) continue;
					ToolStripMenuItem mc = new ToolStripMenuItem();
					mc.BackColor = this.BackColor;
					mc.ForeColor = this.ForeColor;
					mc.Text = mi;
					mc.Click += Mc_Click;
					mc.Tag = (Object?)idx;
					ret.Items.Add(mc);
					idx++;
				}
			}
			return ret;

		}

		private void Mc_Click(object? sender, EventArgs e)
		{
			ToolStripMenuItem? m = (ToolStripMenuItem?)sender;
			if(m!=null)
			{
				if(m.Tag is int)
				{
					SetSelectedIndex((int)m.Tag);
				}
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (m_IsEditMode==false)
			{
				ContextMenuStrip m = MakeMenu();
				if (m!=null)
				{
					m.SetBounds(0, this.Height, this.Width, m.Height);
					m.Show(this, 0, this.Height);
				}
			}
			
		}
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(ControlType), (int?)ControlType);//Nullable`1
			jf.SetValue(nameof(Lines), Lines);//StringCollection
			jf.SetValue(nameof(ForeColor), ForeColor);//Color
			jf.SetValue(nameof(BackColor), BackColor);//Color
			jf.SetValue(nameof(Font), Font);//Font

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("Lines", typeof(String[]).Name);
			if (v != null) Lines = (string[])v;
			v = jf.ValueAuto("ForeColor", typeof(Color).Name);
			if (v != null) ForeColor = (Color)v;
			v = jf.ValueAuto("BackColor", typeof(Color).Name);
			if (v != null) BackColor = (Color)v;
			v = jf.ValueAuto("Font", typeof(Font).Name);
			if (v != null) Font = (Font)v;


		}
	}
}
