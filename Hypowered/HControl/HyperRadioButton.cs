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
			if ((HyperForm != null))
			{
				HyperForm.ExecuteCode(Script_ValueChanged);
			}
		}
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
		[Category("Hypowerd_RadioButton")]
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
		[Category("Hypowerd_RadioButton")]
		public int Value
		{
			get { return m_Value; }
			set
			{
				SetValue(value);
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
		[Category("Hypowerd_RadioButton")]
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
		[Category("Hypowerd_RadioButton")]
		public int Count
		{
			get { return m_Count; }
			set
			{
				m_Count = value;
				ChkButtons();
			}
		}
		[Category("Hypowerd_RadioButton")]
		public string[] Captions
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

		public HyperRadioButton()
		{
			SetMyType(ControlType.RadioButton);
			SetInScript(InScript.ValueChanged);
			//m_ScriptCodes = "//RadioButton";
			ControlName = "HyperRadioButton";
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
				if (this.Focused)
				{
					rr = ReRect(this.ClientRectangle, 1);
					p.Color = m_ForcusColor;
					g.DrawRectangle(p, rr);
				}
				DrawType(g, sb);

			}
		}
		public void CallMouseDown(MouseEventArgs e)
		{
			OnMouseDown(e);
		}
		public void CallMouseUp(MouseEventArgs e)
		{
			OnMouseUp(e);
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
			jf.SetValue(nameof(MyType), (int?)MyType);//Nullable`1
			jf.SetValue(nameof(HorCount), HorCount);
			jf.SetValue(nameof(Count), Count);
			jf.SetValue(nameof(Value), Value);
			jf.SetValue(nameof(CheckSize), CheckSize);//Int32


			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("Count", typeof(Int32).Name);
			if (v != null) Count = (Int32)v;
			v = jf.ValueAuto("HorCount", typeof(Int32).Name);
			if (v != null) HorCount = (Int32)v;
			v = jf.ValueAuto("Value", typeof(Int32).Name);
			if (v != null) Value = (Int32)v;
			v = jf.ValueAuto("CheckSize", typeof(Int32).Name);
			if (v != null) CheckSize = (Int32)v;
		}
	}
}
