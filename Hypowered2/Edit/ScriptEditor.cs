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

namespace Hpd
{
	public partial class ScriptEditor : Form
	{
		public HpdMainForm? MainForm = null;
		public void SetMainForm(HpdMainForm mf)
		{
			this.MainForm = mf;
		}
		public ScriptEditor()
		{
			InitializeComponent();
		}

		private void btnExecute_Click(object sender, EventArgs e)
		{
		}

		private void btnV8Execute_Click(object sender, EventArgs e)
		{
			if (MainForm != null)
			{
				MainForm.Script.ExecuteCode(roslynEdit1.Text);
			}
		}

		private void btnHide_Click(object sender, EventArgs e)
		{
			this.Hide();
		}
	}
}
