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
    public partial class HyperRadioButton : HyperControl
	{

		public delegate void RBValueChangedHandler(object sender, RBValueChangedEventArgs e);
		public event RBValueChangedHandler? RBValueChanged;
		protected virtual void OnRBValueChanged(RBValueChangedEventArgs e)
		{
			if (RBValueChanged != null)
			{
				RBValueChanged(this, e);
			}
			if ((MainForm != null))
			{
				MainForm.ExecuteCode(Script_ValueChanged);
			}
		}
		[Category("Hypowered")]
		public new bool IsDrawFocuse
		{
			get { return base.IsDrawFocuse; }
			set
			{
				base.IsDrawFocuse = value;
				if (this.Controls.Count > 0)
				{
					foreach (Control control in this.Controls)
					{
						if(control is RadioButtonChild)
						{
							((RadioButtonChild)control).IsDrawFocuse = value;
						}
					}
				}
			}
		}
		[Category("Hypowered")]
		public new Font Font
		{
			get { return base.Font; }
			set 
			{
				base.Font = value; 
				if(this.Controls.Count > 0) 
				{
					foreach(Control control in this.Controls)
					{
						control.Font = value;
					}
				}
			}
		}
		[Category("Hypowered_Color")]
		public new Color ForeColor
		{
			get { return base.ForeColor; }
			set
			{
				base.ForeColor = value;
				if (this.Controls.Count > 0)
				{
					foreach (Control control in this.Controls)
					{
						control.ForeColor = value;
					}
				}
			}
		}
		[Category("Hypowered_Color")]
		public new Color BackColor
		{
			get { return base.BackColor; }
			set
			{
				base.BackColor = value;
				if (this.Controls.Count > 0)
				{
					foreach (Control control in this.Controls)
					{
						control.BackColor = value;
					}
				}
			}
		}
		[Category("Hypowered")]
		public new bool IsEditMode
		{
			get { return base.m_IsEditMode; }
			set
			{
				base.SetIsEditMode(value);
				if(this.Controls.Count>0)
				{
					foreach(var item in this.Controls)
					{
						if(item is RadioButtonChild)
						{
							((RadioButtonChild)item).IsEditMode = value;
						}
					}
				}

				this.Invalidate();
			}
		}
		private int m_CheckSize;
		[Category("Hypowered_RadioButton")]
		public int CheckSize
		{
			get { return m_CheckSize; }
			set 
			{ 
				m_CheckSize = value;
				if(this.Controls.Count > 0)
				{
					foreach(Control control in this.Controls)
					{
						if(control is RadioButtonChild)
						{
							RadioButtonChild rb = (RadioButtonChild)control;
							rb.CheckSize= m_CheckSize;
						}
					}
				}
				;
			}
		}
		private int m_Value = -1;
		[Category("Hypowered_RadioButton")]
		public int Value
		{
			get { return m_Value; }
			set
			{
				SetValue(value);
			}
		}
		public int SelectedIndex
		{
			get { return m_Value; }
			set
			{
				SetValue(value);
			}
		}
		[Browsable(false)]
		public Object?[] Tags
		{
			get
			{
				List<Object?> list = new List<Object?>();
				try
				{
					foreach (var c in this.Controls)
					{
						if (c == null) continue;
						list.Add(((Control)c).Tag);
					}
				}
				catch
				{

				}
				return list.ToArray();
			}
			set
			{
				int cnt = value.Length;
				if (cnt > this.Controls.Count) cnt = this.Controls.Count;
				try
				{
					for (int i = 0; i < cnt; i++)
					{
						this.Controls[i].Tag = value[i];
					}
				}
				catch
				{

				}
			}
		}
		public void SetTag(int idx,Object? c)
		{
			if((idx>=0)&&(idx<this.Controls.Count))
			{
				this.Controls[idx].Tag = c;
			}
		}
		public Object? GetTag(int idx)
		{
			if ((idx >= 0) && (idx < this.Controls.Count))
			{
				return this.Controls[idx].Tag;
			}
			else
			{
				return null;
			}
		}
		public bool SetValue(int value,bool IsEvent =true)
		{
			if (m_Value != value)
			{
				if ((value >= 0) && (value < this.Controls.Count))
				{
					m_Value = value;
					if (IsEvent) CheckAllOff();
					((RadioButtonChild)this.Controls[m_Value]).Checked = true;
					this.Invalidate();
					OnRBValueChanged(new RBValueChangedEventArgs(m_Value));
					return true;
				}

			}
			return false;
		}
		private int m_HorCount = 1;
		[Category("Hypowered_RadioButton")]
		public int HorCount
		{
			get { return m_HorCount; }
			set
			{
				m_HorCount = value;
				if (m_HorCount < 1) m_HorCount = 1;
				ChkButtons();
			}
		}
		private int m_Count = 1;
		[Category("Hypowered_RadioButton")]
		public int Count
		{
			get { return m_Count; }
			set
			{
				m_Count = value;
				ChkButtons();
			}
		}
		[Category("Hypowered_RadioButton")]
		public new string[] Lines
		{
			get
			{
				string[]ret = new string[this.Controls.Count];
				for(int i=0; i<this.Controls.Count;i++)
				{
					ret[i] = this.Controls[i].Text;
				}
				return ret;
			}
			set
			{
				int cnt = value.Length;
				if(cnt > this.Controls.Count) cnt= this.Controls.Count;
				for (int i = 0; i < cnt; i++)
				{
					this.Controls[i].Text =value[i];
				}

			}
		}
		public string Caption(int index)
		{
			string ret = "";
			if((index>=0)&&(index<this.Controls.Count))
			{
				ret = this.Controls[index].Text;
			}
			return ret;
		}
		public bool SetCaption(int index,string v)
		{
			bool ret = false;
			if ((index >= 0) && (index < this.Controls.Count))
			{
				this.Controls[index].Text = v;
				ret =true;
			}
			return ret;
		}
		[Category("Hypowered_Text")]
		public new StringAlignment TextAligiment
		{
			get { return base.TextAligiment; }
			set
			{
				base.TextAligiment = value;
				if (this.Controls.Count > 0)
				{
					foreach (var item in this.Controls)
					{
						if (item is RadioButtonChild)
						{
							((RadioButtonChild)item).TextAligiment = value;
						}
					}
				}
			}
		}
		[Category("Hypowered_Text")]
		public new StringAlignment TextLineAligiment
		{
			get { return base.TextLineAligiment; }
			set
			{
				base.TextLineAligiment = value;
				if (this.Controls.Count > 0)
				{
					foreach (var item in this.Controls)
					{
						if (item is RadioButtonChild)
						{
							((RadioButtonChild)item).TextLineAligiment = value;
						}
					}
				}
			}
		}
		public HyperRadioButton()
		{
			SetControlType(Hypowered.ControlType.RadioButton);
			SetInScript(InScriptBit.ValueChanged | InScriptBit.DragDrop);
			//m_ScriptCodes = "//RadioButton";
			SetControlName("HyperRadioButton");
			m_format.Alignment = StringAlignment.Near;
			m_format.LineAlignment = StringAlignment.Center;
			m_UnCheckedColor = ColU.ToColor(HyperColor.Dark);
			m_CheckSize = 16;
			this.Size = new Size(100,100); 
			
			InitializeComponent();
			m_Count= 1;
			this.Controls.Add(CreateRB());
			ChkButtons();
		}
		public void CheckAllOff()
		{
			if (this.Controls.Count > 0)
			{
				for (int i = 0; i < this.Controls.Count; i++)
				{
					if (this.Controls[i] is RadioButtonChild)
					{
						((RadioButtonChild)this.Controls[i]).Checked = false;
					}
				}
			}
		}
		private void ChkButtons()
		{
			int motocnt = this.Controls.Count;
			if (motocnt > m_Count)
			{
				for (int i = motocnt - 1; i >= m_Count; i--)
				{
					this.Controls.RemoveAt(i);
				}
			}
			else if(motocnt< m_Count) 
			{
				for (int i = motocnt ; i < m_Count; i++)
				{
					this.Controls.Add(CreateRB());
				}
			}
			for (int i = 0; i < this.Controls.Count; i++)
			{
				((RadioButtonChild)this.Controls[i]).SetIndex(i);

			}
			LayoutButton();
		}
		private void LayoutButton()
		{
			if (m_Count != this.Controls.Count) return;
			int w = (this.Size.Width-16) / m_HorCount;
			int h = this.Controls[0].Height;
			int x = 0;
			int y = 0;
			for (int i=0; i<m_Count;i++)
			{
				x = (i % m_HorCount) * w +8;
				y=  (i / m_HorCount) * h + 8;
				this.Controls[i].Location= new Point(x, y);
				this.Controls[i].Size = new Size(w,h);

			}
		}
		private RadioButtonChild CreateRB()
		{
			RadioButtonChild child = new RadioButtonChild();
			int i = this.Controls.Count;
			child.Text = $"RadioButton{i}";
			if((this.Parent!= null)&&(this.Parent is HyperRadioButton))
			{
				m_IsEditMode = ((HyperRadioButton)this.Parent).IsEditMode;
			}
			child.IsEditMode = m_IsEditMode;
			return child;
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			using (Pen p = new Pen(ForeColor))
			using (SolidBrush sb = new SolidBrush(BackColor))
			{
				Graphics g = pe.Graphics;
				// 背景色
				g.FillRectangle(sb, this.ClientRectangle);
				// 外枠
				Rectangle rr = ReRect(this.ClientRectangle, 2);
				//p.Color = ForeColor;
				//g.DrawRectangle(p, rr);
				if ((this.Focused)&&(m_IsDrawFocuse))
				{
					rr = ReRect(this.ClientRectangle, 1);
					p.Color = m_ForcusColor;
					g.DrawRectangle(p, rr);
				}
				DrawEditMode(g, p, sb);


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
						m_MDP = MousePos(e);
						m_MDLoc = this.Location;
						m_MDSize = this.Size;
						return;
					}
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			LayoutButton();
		}
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(ControlType), (int?)ControlType);//Nullable`1
			jf.SetValue(nameof(HorCount), HorCount);
			jf.SetValue(nameof(Count), Count);
			jf.SetValue(nameof(Value), Value);
			jf.SetValue(nameof(Lines), Lines);
			jf.SetValue(nameof(CheckSize), CheckSize);//Int32
			jf.SetValue(nameof(Font), Font);
			jf.SetValue(nameof(ForeColor), ForeColor);
			jf.SetValue(nameof(BackColor), BackColor);
			jf.SetValue(nameof(TextAligiment), (int)TextAligiment);
			jf.SetValue(nameof(TextLineAligiment), (int)TextLineAligiment);


			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("Count", typeof(Int32).Name);
			if (v != null) Count = (Int32)v;
			v = jf.ValueAuto("Lines", typeof(String[]).Name);
			if (v != null) Lines = (String[])v;
			v = jf.ValueAuto("HorCount", typeof(Int32).Name);
			if (v != null) HorCount = (Int32)v;
			v = jf.ValueAuto("Value", typeof(Int32).Name);
			if (v != null) Value = (Int32)v;
			v = jf.ValueAuto("CheckSize", typeof(Int32).Name);
			if (v != null) CheckSize = (Int32)v;
			v = jf.ValueAuto("Font", typeof(Font).Name);
			if (v != null) Font = (Font)v;
			v = jf.ValueAuto("ForeColor", typeof(Color).Name);
			if (v != null) ForeColor = (Color)v;
			v = jf.ValueAuto("BackColor", typeof(Color).Name);
			if (v != null) BackColor = (Color)v;
			v = jf.ValueAuto("TextAligiment", typeof(Int32).Name);
			if (v != null) TextAligiment = (StringAlignment)v;
			v = jf.ValueAuto("TextLineAligiment", typeof(Int32).Name);
			if (v != null) TextLineAligiment = (StringAlignment)v;
		}
	}
}
