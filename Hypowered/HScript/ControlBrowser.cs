using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class ControlBrowser : Control
	{
		private HyperMainForm? m_MainForm = null;

		private TextBox m_TextBox = new TextBox();
		private Button m_Button = new Button();
		private ComboBox m_FormComp = new ComboBox();
		private ListBox m_ControlListBox = new ListBox();
		private ListBox m_MemberListBox = new ListBox();
		private SplitContainer m_spliter = new SplitContainer();
		private List<string> m_Props = new List<string>();

		public new Color BackColor
		{
			get { return base.BackColor; } 
			set
			{
				base.BackColor = value;
				m_TextBox.BackColor = value;
				m_Button.BackColor = value;
				m_FormComp.BackColor = value;
				m_ControlListBox.BackColor = value;
				m_MemberListBox.BackColor = value;
				m_spliter.BackColor = value;
			}
		}
		public new Color ForeColor
		{
			get { return base.ForeColor; }
			set
			{
				base.ForeColor = value;
				m_TextBox.ForeColor = value;
				m_Button.ForeColor = value;
				m_FormComp.ForeColor = value;
				m_ControlListBox.ForeColor = value;
				m_MemberListBox.ForeColor = value;
				m_spliter.ForeColor = value;
			}
		}
		public int SplitterDistance
		{
			get { return m_spliter.SplitterDistance; }
			set { m_spliter.SplitterDistance = value; }
		}
		public EditPad EditPad { get; set; } = null;
		public ControlBrowser()
		{
			InitializeComponent();
			this.SetStyle(
//ControlStyles.Selectable |
//ControlStyles.UserMouse |
ControlStyles.DoubleBuffer |
ControlStyles.UserPaint |
ControlStyles.AllPaintingInWmPaint |
ControlStyles.SupportsTransparentBackColor,
true);
			this.UpdateStyles();
			this.Size = new Size(200, 200);

			m_FormComp.ForeColor = this.ForeColor;
			m_FormComp.BackColor = this.BackColor;
			m_FormComp.DropDownStyle = ComboBoxStyle.DropDownList;
			m_FormComp.FlatStyle= FlatStyle.Flat;
			m_ControlListBox.ForeColor = this.ForeColor;
			m_ControlListBox.BackColor = this.BackColor;
			m_ControlListBox.IntegralHeight = false;
			m_ControlListBox.Dock= DockStyle.Fill;
			m_ControlListBox.BorderStyle = BorderStyle.FixedSingle;
			//m_ControlListBox.FlatStyle = FlatStyle.Flat;

			m_MemberListBox.ForeColor = this.ForeColor;
			m_MemberListBox.BackColor = this.BackColor;
			m_MemberListBox.IntegralHeight = false;
			m_MemberListBox.Dock = DockStyle.Fill;
			m_MemberListBox.BorderStyle = BorderStyle.FixedSingle;
			//m_MemberListBox.FlatStyle = FlatStyle.Flat;
			ChkSize();
			m_spliter.SplitterDistance = m_spliter.Width / 2;
			this.Controls.Add(m_TextBox);
			this.Controls.Add(m_Button);
			this.Controls.Add(m_FormComp);

			m_spliter.Panel1.Controls.Add(m_ControlListBox);
			m_spliter.Panel2.Controls.Add(m_MemberListBox);

			this.Controls.Add(m_spliter);
			m_FormComp.SelectedIndexChanged += FormComp_SelectedIndexChanged;
			m_ControlListBox.SelectedIndexChanged += ControlListBox_SelectedIndexChanged;
			m_MemberListBox.SelectedIndexChanged += MenberListBox_SelectedIndexChanged;
			m_ControlListBox.DoubleClick += M_ControlListBox_DoubleClick;
			m_MemberListBox.DoubleClick += M_MemberListBox_DoubleClick;
			m_Button.Click += M_Button_Click;
		}

		private void M_Button_Click(object? sender, EventArgs e)
		{
			if ((EditPad != null) )
			{
				EditPad.SetText(m_TextBox.Text);
			}
		}

		private void M_MemberListBox_DoubleClick(object? sender, EventArgs e)
		{
			if ((EditPad != null) && (m_MemberListBox.SelectedIndex >= 0))
			{
				string? s = m_MemberListBox.Items[m_MemberListBox.SelectedIndex].ToString();
				if ((s != null) && (s.Length > 0))
				{
					s = s.Split("\t")[0].Trim();
					EditPad.SetText(s);
				}
			}
		}

		private void M_ControlListBox_DoubleClick(object? sender, EventArgs e)
		{
			if((EditPad != null)&&(m_ControlListBox.SelectedIndex >=0))
			{
				string? s = m_ControlListBox.Items[m_ControlListBox.SelectedIndex].ToString();
				if ((s != null) && (s.Length > 0))
				{
					EditPad.SetText(s);
				}
			}
		}

		private void MenberListBox_SelectedIndexChanged(object? sender, EventArgs e)
		{
			if((m_ControlListBox.SelectedIndex >= 0)&&(m_MemberListBox.SelectedIndex >= 0))
			{
				string? s0 = m_ControlListBox.SelectedItem.ToString();
				if (s0 == null)
				{
					s0 = "";
				}
				else
				{
					s0 = s0.Trim();
				}
				string? s1 = m_MemberListBox.SelectedItem.ToString();
				if (s1 == null)
				{
					s1 = "";
				}
				else
				{
					s1 = s1.Split("\t")[0].Trim();
				}
				m_TextBox.Text = s0+"."+s1;
			}
		}

		private void ControlListBox_SelectedIndexChanged(object? sender, EventArgs e)
		{
			ListupProp();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
		public void ChkSize()
		{
			m_Button.Size = new Size(m_TextBox.Height, m_TextBox.Height);

			m_TextBox.Location = new Point(2, 2);
			m_TextBox.Size = new Size(this.Width - m_Button.Width-6, m_TextBox.Height);
			m_Button.Location = new Point(m_TextBox.Right+2, 2);

			m_FormComp.Location = new Point(2, m_TextBox.Bottom+2);
			m_FormComp.Size = new Size(this.Width-4, m_FormComp.Height);

			m_spliter.Location = new Point(2, m_FormComp.Bottom+2);
			m_spliter.Size = new Size(
				this.Width - 4, 
				this.ClientSize.Height - m_FormComp.Height - m_TextBox.Height- 8);
		}

		private void FormComp_SelectedIndexChanged(object? sender, EventArgs e)
		{
			ListupControls();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
		}
		public void SetMainForm(HyperMainForm? fm)
		{
			m_MainForm = fm;
			if(m_MainForm!=null)
			{
				ListupForms();
				m_MainForm.FontChanged += (sender, e) =>
				{
					ListupForms();
				};
				m_MainForm.ControlChanged += (sender, e) =>
				{
					ListupForms();
				};
			}
		}
		private void ListupForms()
		{
			m_FormComp.Items.Clear();
			if (m_MainForm == null) return;
			m_FormComp.Items.AddRange(m_MainForm.forms.GetForms());
			if(m_MainForm.forms.TargetForm!=null)
			{
				m_FormComp.SelectedIndex = m_MainForm.forms.TargetForm.Index;
			}
		}

		private void ListupControls()
		{
			m_ControlListBox.Items.Clear();
			m_Props.Clear();
			if (m_MainForm == null) return;
			if (m_FormComp.SelectedIndex < 0) return;

			HyperBaseForm? bf = m_MainForm.forms[m_FormComp.SelectedIndex];			
			if(bf==null) return;
			List<string> list = new List<string>();
			list.Add(bf.Name);
			if (bf.Controls.Count>0)
			{
				foreach(Control c in bf.Controls)
				{
					list.Add($"{c.Name}");
				}
			}
			m_ControlListBox.Items.AddRange(list.ToArray());
			if (bf.TargetControl != null)
			{
				m_ControlListBox.SelectedIndex = bf.TargetControl.Index+1;
			}


		}
		private void ListupProp()
		{
			m_MemberListBox.Items.Clear();
			if (m_MainForm == null) return;
			if (m_FormComp.SelectedIndex < 0) return;
			if (m_ControlListBox.SelectedIndex < 0) return;
			int idx = m_ControlListBox.SelectedIndex;

			object? targetObj = null; ;
			HyperBaseForm? bf = m_MainForm.forms[m_FormComp.SelectedIndex];
			if (bf == null) return;
			if (idx==0)
			{
				targetObj = bf;
			}else if(idx>=1)
			{
				targetObj = bf.Controls[idx-1];
			}
			if (targetObj == null) return;
			List<string> list = new List<string>();
			var ps = bf.GetType().GetProperties(/*BindingFlags.Public | BindingFlags.Instance*/);
			foreach (var p in ps)
			{
				list.Add($" {p.Name}\t({p.PropertyType.Name})");
			}
			var ms = bf.GetType().GetMembers(
				/*BindingFlags.Public | BindingFlags.Instance*/
				);
			foreach (var m in ms)
			{
				string s = m.Name;
				if ((s.IndexOf("add_") == 0)
					|| (s.IndexOf("get_") == 0)
					|| (s.IndexOf("remove_") == 0)
					|| (s.IndexOf("set_") == 0))
				{
					continue;
				}
				string pn = m.MemberType.ToString();
				if (pn == "Property") continue;
				list.Add($" {s}\t({pn})");
			}
			list.Sort();
			for(int i= list.Count-1; i>=1;i--)
			{
				if (list[i]== list[i-1]) list.RemoveAt(i);
			}
			m_MemberListBox.Items.AddRange(list.ToArray());

		}

	}
}
