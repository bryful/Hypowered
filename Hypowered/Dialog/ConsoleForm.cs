using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ClearScript;
using System.Reflection;
namespace Hypowered
{
	public partial class ConsoleForm : BaseForm
	{
		public MainForm? MainForm = null;
		public void SetMainForm(MainForm mf)
		{
			this.MainForm = mf;
		}
		public ConsoleForm()
		{
			InitializeComponent();
			Clear();
			btnClear.Click += (sender, e) => { Clear(); };
			btnFont.Click += (sender, e) => { FontDialog(); };
		}
		public void WriteLine(object? o)
		{

			string s = HUtils.ToStr(o) + "\r\n";
			try
			{
				tbOutput.AppendText(s);
				tbOutput.Focus();
			}
			catch
			{

			}
		}
		public void Write(object? o)
		{
			string s = HUtils.ToStr(o);
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
		public void FontDialog()
		{
			using (FontDialog dlg = new FontDialog())
			{
				dlg.Font = tbOutput.Font;
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					tbOutput.Font = dlg.Font;
				}
			}
		}
	}
}
