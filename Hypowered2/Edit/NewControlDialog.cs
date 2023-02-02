using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hpd
{
	public partial class NewControlDialog : Form
	{
		private HpdForm? m_MainForm = null;
		public void SetMainForm(HpdForm? mf)
		{
			m_MainForm = mf;
		}
		public HpdType HpdType
		{
			get{return cmbType.HpdType;}
			set{ cmbType.HpdType=value;NameSet(); }
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
			btnCancel.Click += (sender, e) => { DialogResult = DialogResult.Cancel; };
			btnOK.Click += (sender, e) =>
			{
				if (m_MainForm != null)
				{
					string nm = tbName.Text;
					HpdControl? sa = m_MainForm.Items.Find(nm);
					if (sa != null)
					{
						tbName.Text = m_MainForm.NewName(nm);
						return;
					}
					DialogResult = DialogResult.OK;
				}
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
	}
}
