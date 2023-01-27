using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hpd
{
	public partial class NewControlDialog : HpdForm
	{
		private HpdForm? m_MainForm = null;
		public void SetMainForm(HpdForm? mf)
		{
			m_MainForm = mf;
		}
		public HpdType HpdType
		{
			get{return cmbType.HpdType;}
			set{cmbType.HpdType=value;NameSet(); }
		}
		public string HpdName
		{
			get { return tbName.Text; }
			set { tbName.Text = value; }
		}
		public NewControlDialog()
		{
			InitializeComponent();
			cmbType.SelectedIndexChanged += (sender, e) =>
			{
				NameSet();
			};
			NameSet();
		}
		private void NameSet()
		{
				string s = cmbType.DefName;
				if (m_MainForm != null)
				{
					s = m_MainForm.NewName(s);
				}
				tbName.Text = s;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult= DialogResult.Cancel;
		}

		private void hpdButton1_Click(object sender, EventArgs e)
		{
			if(m_MainForm!= null)
			{
				string nm = tbName.Text;
				HpdControl[] sa = m_MainForm.FindControl(nm);
				if (sa.Length > 0)
				{
					tbName.Text = m_MainForm.NewName(nm);
					return;
				}
				DialogResult = DialogResult.OK;
			}

		}
	}
}
