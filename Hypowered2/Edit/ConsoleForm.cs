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
	public partial class ConsoleForm : Form
	{
		public HpdMainForm? MainForm = null;
		public void SetMainForm(HpdMainForm mf)
		{
			this.MainForm = mf;
		}
		public ConsoleForm()
		{
			InitializeComponent();
			Clear();
		}
		public void WriteLine(object? o)
		{

			string s = HpdForm.ToStr(o) + "\r\n";
			try
			{
				tbOutput.AppendText(s);
				tbOutput.Focus();
			}catch
			{

			}
		}
		public void Write(object? o)
		{
			string s = HpdForm.ToStr(o);
			try
			{
				tbOutput.AppendText(s);
				tbOutput.Focus();
			}
			catch { }
		}
		public void Clear()
		{
			tbOutput.Text = "";
			tbOutput.Focus();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			Clear();
		}

		private void btnHide_Click(object sender, EventArgs e)
		{
			this.Hide();
		}
	}
}
