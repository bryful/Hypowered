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

namespace Hypowered
{
	public partial class HyperListBox : HyperControl
	{
		protected ListBox m_ListBox = new ListBox();
		public delegate void SelectedIndexChangedHandler(object sender, SelectedIndexChangedEventArgs e);
		
		public event SelectedIndexChangedHandler? SelectedIndexChanged;
		protected virtual void OnSelectedIndexChanged(SelectedIndexChangedEventArgs e)
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, e);
			}
			ExecScript(ScriptKind.SelectedIndexChanged);
		}
		public override void ExecScript(ScriptKind sk)
		{
			if (MainForm != null)
			{
				if (ScriptCode.IsScriptCode(sk))
				{
					switch (sk)
					{
						case ScriptKind.SelectedIndexChanged:
							MainForm.Script.AddScriptObject("value", SelectedItem);
							break;
						case ScriptKind.MouseDoubleClick:
							MainForm.Script.AddScriptObject("value", SelectedItem);
							break;
						case ScriptKind.DragDrop:
							MainForm.Script.AddScriptObject("value", m_DragDropItems);
							break;
						default:
							MainForm.Script.AddScriptObjectNull("value");
							break;
					}
					MainForm.Script.ExecuteScript(ScriptCode, sk);
					MainForm.Script.DeleteScriptObject("value");
				}
			}
		}
		public override void SetIsEditMode(bool value)
		{
			m_IsEditMode = value;
			m_ListBox.Visible = !value;
		}
		[Category("Hypowered")]
		public new Font Font
		{
			get { return m_ListBox.Font; }
			set
			{
				m_ListBox.Font = value;
				base.Font = value;
			}
		}
		[Category("Hypowered_ListBox")]
		public  ListBox ListBox
		{
			get { return m_ListBox; }
			set
			{
				m_ListBox = value;
			}
		}
		[Category("Hypowered_ListBox")]
		public BorderStyle BorderStyle
		{
			get { return m_ListBox.BorderStyle; }
			set
			{
				m_ListBox.BorderStyle = value;
			}
		}
		[Category("Hypowered_ListBox")]
		public bool IntegralHeight
		{
			get { return m_ListBox.IntegralHeight; }
			set
			{
				m_ListBox.IntegralHeight = value;
			}
		}
		[Category("Hypowered_ListBox")]
		public int ItemHeight
		{
			get { return m_ListBox.ItemHeight; }
			set
			{
				m_ListBox.ItemHeight = value;
			}
		}
		[Category("Hypowered_ListBox")]
		public int SelectedIndex
		{
			get { return m_ListBox.SelectedIndex; }
			set
			{
				m_ListBox.SelectedIndex = value;
			}
		}
		[Category("Hypowered_ListBox")]
		public string SelectedItem
		{
			get
			{
				string ret = "";
				int si = m_ListBox.SelectedIndex;
				if ((si >= 0) && (si < m_ListBox.Items.Count))
				{
					string? s = m_ListBox.Items[si].ToString();
					if (s == null)
					{
						ret = "";
					}
					else
					{
						ret = s;
					}
				}
				return ret;
			}
		}

		[Category("Hypowered_ListBox")]
		public ListBox.ObjectCollection Items
		{
			get { return m_ListBox.Items; }
		}
		[Category("Hypowered_ListBox")]
		public new string[] Lines
		{
			get 
			{
				List<string> list = new List<string>();
				if(m_ListBox.Items.Count> 0)
				{
					foreach(var item in m_ListBox.Items)
					{
						if (item is string)
						{
							list.Add((string)item);
						}
					}
				}
				return list.ToArray();
			}
			set
			{
				m_ListBox.Items.Clear();
				m_ListBox.Items.AddRange(value);
			}
		}
		[Category("Hypowered_ListBox")]
		public int Count
		{
			get { return m_ListBox.Items.Count; }
		}
		[Category("Hypowered_Color")]
		public new Color ForeColor
		{
			get { return m_ListBox.ForeColor; }
			set
			{
				base.ForeColor = value;
				m_ListBox.ForeColor = value;
			}
		}
		[Category("Hypowered_Color")]
		public new Color BackColor
		{
			get { return m_ListBox.BackColor; }
			set
			{
				base.BackColor = value;
				m_ListBox.BackColor = value;
			}
		}
		[Category("Hypowered")]
		public new DragDropFileType DragDropFileType
		{
			get { return m_DragDropFileType; }
			set
			{
				m_DragDropFileType = value;
				base.AllowDrop = (m_DragDropFileType != DragDropFileType.None);
				m_ListBox.AllowDrop = base.AllowDrop;
			}
		}
		[Category("Hypowered")]
		public new bool AllowDrop
		{
			get { return base.AllowDrop; }
			set
			{
				base.AllowDrop = value;
				m_ListBox.AllowDrop = value;
			}
		}
		public HyperListBox()
		{
			SetControlType(Hypowered.ControlType.ListBox);
			SetInScript(InScriptBit.ValueChanged|InScriptBit.SelectedIndexChanged | InScriptBit.DragDrop);
			this.Size = new Size(150, 150);
			m_ListBox.Location = new Point(2, 2);
			m_ListBox.Size = new Size(this.Width,this.Height);
			if(Height!= m_ListBox.Height)
			{
				this.Size = new Size(this.Width, m_ListBox.Height);
			}

			m_ListBox.BackColor =base.BackColor;
			m_ListBox.ForeColor = base.ForeColor;
			m_ListBox.BorderStyle= BorderStyle.FixedSingle;
			m_ListBox.IntegralHeight = false;
			InitializeComponent();
			this.Controls.Add(m_ListBox);
			m_ListBox.SelectedIndexChanged += ListBoxSelectedIndexChanged;
			m_ListBox.MouseDoubleClick += ListBoxMouseDoubleClick;
			m_ListBox.DragEnter += (sender, e) => { this.OnDragEnter(e); };
			m_ListBox.DragDrop += (sender, e) => { this.OnDragDrop(e); };
		}

		protected virtual void ListBoxMouseDoubleClick(object? sender, MouseEventArgs e)
		{
			ExecScript(ScriptKind.MouseDoubleClick);
		}
		protected virtual void ListBoxSelectedIndexChanged(object? sender, EventArgs e)
		{
			string s = "";
			if ((m_ListBox.SelectedIndex >= 0) && (m_ListBox.SelectedIndex < m_ListBox.Items.Count))
			{
				if (m_ListBox.Items[m_ListBox.SelectedIndex] != null)
				{
					string? ss = m_ListBox.Items[m_ListBox.SelectedIndex].ToString();
					if (ss != null)
					{
						s = ss;
					}
				}
				OnSelectedIndexChanged(new SelectedIndexChangedEventArgs(m_ListBox.SelectedIndex, s));
			}
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			if (m_IsEditMode)
			{
				base.OnPaint(pe);
			}
			else
			{
				pe.Graphics.Clear(BackColor);
				if (IsDrawFrame)
				{
					using (Pen p = new Pen(ForeColor))
					{
						DrawFrame(pe.Graphics, p, this.ClientRectangle);
					}
				}
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			m_ListBox.Size = new Size(this.Width-4, this.Height-4);
		}
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(ControlType), (int?)ControlType);//Nullable`1
			jf.SetValue(nameof(IntegralHeight), IntegralHeight);//Boolean
			jf.SetValue(nameof(ItemHeight), ItemHeight);//Int32
			jf.SetValue(nameof(Lines), Lines);//ObjectCollection
			jf.SetValue(nameof(ForeColor), ForeColor);//Color
			jf.SetValue(nameof(BackColor), BackColor);//Color
			jf.SetValue(nameof(Font), Font);//Font
			jf.SetValue(nameof(BorderStyle), (int)BorderStyle);//Font

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("IntegralHeight", typeof(Boolean).Name);
			if (v != null) IntegralHeight = (Boolean)v;
			v = jf.ValueAuto("ItemHeight", typeof(Int32).Name);
			if (v != null) ItemHeight = (Int32)v;
			v = jf.ValueAuto("Lines", typeof(string[]).Name);
			if (v != null) Items.AddRange((string[])v);
			v = jf.ValueAuto("ForeColor", typeof(Color).Name);
			if (v != null) ForeColor = (Color)v;
			v = jf.ValueAuto("BackColor", typeof(Color).Name);
			if (v != null) BackColor = (Color)v;
			v = jf.ValueAuto("Font", typeof(Font).Name);
			if (v != null) Font = (Font)v;
			v = jf.ValueAuto("BorderStyle", typeof(int).Name);
			if (v != null) BorderStyle = (BorderStyle)v;

		}

	}
}
