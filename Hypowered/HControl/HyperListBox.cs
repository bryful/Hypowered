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
	public partial class HyperListBox : HyperControl
	{
		private ListBox m_ListBox = new ListBox();
		public delegate void SelectedIndexChangedHandler(object sender, SelectedIndexChangedEventArgs e);
		public event SelectedIndexChangedHandler? SelectedIndexChanged;
		protected virtual void OnSelectedIndexChanged(SelectedIndexChangedEventArgs e)
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, e);
			}
			if (MainForm != null)
			{

				MainForm.ExecuteCode(GetScriptCode(ScriptKind.SelectedIndexChanged));
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
		public ListBox.ObjectCollection Items
		{
			get { return m_ListBox.Items; }
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
		public HyperListBox()
		{
			SetControlType(Hypowered.ControlType.ListBox);
			SetInScript(InScriptBit.ValueChanged|InScriptBit.SelectedIndexChanged);
			this.Size = new Size(150, 150);
			m_ListBox.Location = new Point(0, 0);
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
			m_ListBox.SelectedIndexChanged += M_ListBox_SelectedIndexChanged;
		}

		private void M_ListBox_SelectedIndexChanged(object? sender, EventArgs e)
		{
			string s = "";
			if ((m_ListBox.SelectedIndex >= 0) && (m_ListBox.SelectedIndex < m_ListBox.Items.Count))
			{
				s = m_ListBox.Items[m_ListBox.SelectedIndex].ToString();
			}
			OnSelectedIndexChanged(new SelectedIndexChangedEventArgs(m_ListBox.SelectedIndex,s));	
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
			m_ListBox.Size = new Size(this.Width, this.Height);
			if (Height != m_ListBox.Height)
			{
				this.Size = new Size(this.Width, m_ListBox.Height);
			}
		}
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(ControlType), (int?)ControlType);//Nullable`1
			jf.SetValue(nameof(IntegralHeight), IntegralHeight);//Boolean
			jf.SetValue(nameof(ItemHeight), ItemHeight);//Int32
			jf.SetValue(nameof(Items), Items);//ObjectCollection
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
			v = jf.ValueAuto("IntegralHeight", typeof(Boolean).Name);
			if (v != null) IntegralHeight = (Boolean)v;
			v = jf.ValueAuto("ItemHeight", typeof(Int32).Name);
			if (v != null) ItemHeight = (Int32)v;
			v = jf.ValueAuto("Items", typeof(ListBox.ObjectCollection).Name);
			if (v != null) Items.AddRange((string[])v);
			v = jf.ValueAuto("ForeColor", typeof(Color).Name);
			if (v != null) ForeColor = (Color)v;
			v = jf.ValueAuto("BackColor", typeof(Color).Name);
			if (v != null) BackColor = (Color)v;
			v = jf.ValueAuto("Font", typeof(Font).Name);
			if (v != null) Font = (Font)v;


		}

	}
}
