using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class ControlBrowser : Control
	{
		private HyperMainForm? m_HyperForm = null;
		private TextBox m_TextBox = new TextBox();
		private Button m_Button = new Button();
		private ComboBox m_Comp = new ComboBox();
		private ListBox m_ListBox = new ListBox();
		private List<string> m_Props = new List<string>();
		public ControlBrowser()
		{
			InitializeComponent();
			this.Size = new Size(200, 200);

			m_Comp.ForeColor = this.ForeColor;
			m_Comp.BackColor = this.BackColor;
			m_Comp.DropDownStyle = ComboBoxStyle.DropDownList;
			m_ListBox.ForeColor = this.ForeColor;
			m_ListBox.BackColor = this.BackColor;
			m_ListBox.IntegralHeight = false;
			ChkSize();
			this.Controls.Add(m_TextBox);
			this.Controls.Add(m_Button);
			this.Controls.Add(m_Comp);
			this.Controls.Add(m_ListBox);
			m_Comp.SelectedIndexChanged += M_Comp_SelectedIndexChanged;
			m_ListBox.SelectedIndexChanged += M_ListBox_SelectedIndexChanged;
		}

		private void M_ListBox_SelectedIndexChanged(object? sender, EventArgs e)
		{
			if ((m_Comp.SelectedIndex >= 0) && (m_ListBox.SelectedIndex >= 0))
			{
				m_TextBox.Text = m_Comp.SelectedItem.ToString()+"."+ m_Props[m_ListBox.SelectedIndex];
			}
			else
			{
				m_TextBox.Text = "";
			}
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

			m_Comp.Location = new Point(2, m_TextBox.Bottom+2);
			m_Comp.Size = new Size(this.Width-4, m_Comp.Height);

			m_ListBox.Location = new Point(2, m_Comp.Bottom+2);
			m_ListBox.Size = new Size(
				this.Width - 4, 
				this.ClientSize.Height - m_Comp.Height - m_TextBox.Height- 8);
		}

		private void M_Comp_SelectedIndexChanged(object? sender, EventArgs e)
		{
			Listup();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
		}
		public void SetHyperForm(HyperMainForm? fm)
		{
			m_HyperForm = fm;
			if(m_HyperForm!=null)
			{
				m_Comp.Items.Clear();
				m_Comp.Items.AddRange(ListupControls());
				m_HyperForm.ControlAdded += M_HyperForm_ControlAdded;
				m_HyperForm.ControlChanged += M_HyperForm_ControlChanged;
				m_HyperForm.ControlRemoved += M_HyperForm_ControlAdded; ;
			}
		}

		private void M_HyperForm_ControlChanged(object? sender, EventArgs e)
		{
			m_Comp.Items.Clear();
			m_Comp.Items.AddRange(ListupControls());
		}

		private void M_HyperForm_ControlAdded(object? sender, ControlEventArgs e)
		{
			m_Comp.Items.Clear();
			m_Comp.Items.AddRange(ListupControls());
		}

		private string[] ListupControls()
		{
			m_ListBox.Items.Clear();
			if (m_HyperForm == null) return new string[0];

			List<string> list = new List<string>();
			list.Add(m_HyperForm.Name);

			if(m_HyperForm.Controls.Count>1)
			{
				foreach(Control c in m_HyperForm.Controls)
				{
					if (c is HyperMenuBar) continue;
					if(c is HyperControl)
					{
						list.Add(c.Name);
					}
				}
			}
			return list.ToArray();
		}
		private void Listup()
		{
			m_ListBox.Items.Clear();
			m_Props.Clear();
			if (m_Comp.SelectedIndex < 0) return;
			Object? obj = null;
			if(m_Comp.SelectedIndex==0)
			{
				obj = m_HyperForm;
			}
			else
			{
				obj = m_HyperForm.Controls[m_Comp.SelectedIndex];
			}
			var ps = obj.GetType().GetProperties();
			if(ps.Length>0)
			{
				List<string> list = new List<string>();
				foreach(var p in ps)
				{
					list.Add($" {p.Name}\t( {p.PropertyType.Name} )");
					m_Props.Add(p.Name);
				}
				m_ListBox.Items.AddRange(list.ToArray());
			}
		}

	}
}
