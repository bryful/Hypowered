using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExCSS;

namespace Hypowered
{
	public partial class AddMenuDialog : BaseForm
	{
		public MainForm? MainForm = null;
		public HForm? HForm = null;
		public string MenuName
		{
			get { return tbName.Text; }
			set { tbName.Text = value; }
		}
		public string MenuText
		{
			get { return tbText.Text; }
			set { tbText.Text = value; }
		}
		public bool AtSubMenu
		{
			get { return cbAtSubmenu.Checked; }
			set
			{
				cbAtSubmenu.Checked = value;
				cmbSubMenu.Visible = cbAtSubmenu.Checked;
			}
		}
		public void SetMainForm(MainForm? mf)
		{
			MainForm = mf;
			if ((MainForm != null) && (MainForm.TargetForm != null))
			{
				HForm = MainForm.TargetForm;
				cmbSubMenu.Items.Clear();
				if (HForm.MainMenu.Items.Count > 0)
				{
					List<string> list = new List<string>();
					foreach (var m in HForm.MainMenu.Items)
					{
						if (m is HMenuItem)
						{
							list.Add(((HMenuItem)m).Name);
						}
					}
					cmbSubMenu.Items.AddRange(list.ToArray());
				}
			}
		}
		public AddMenuDialog()
		{
			InitializeComponent();
			cmbSubMenu.DropDownStyle = ComboBoxStyle.DropDownList;
			cbAtSubmenu.CheckedChanged += (sender, e) => { cmbSubMenu.Visible = cbAtSubmenu.Checked; };
			btnOK.Click += BtnOK_Click;
		}

		private void BtnOK_Click(object? sender, EventArgs e)
		{
			if (HForm == null) return;
			if ((tbName.Text == "") || (tbText.Text == "")) return;
			if(AtSubMenu == false)
			{
				int idx = HForm.MainMenu.IndexOfMenuName(tbName.Text);
				if (idx >= 0) return;
			}
			else
			{
				return;
			}
			this.DialogResult = DialogResult.OK;
		}
	}
}
