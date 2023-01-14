using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class JSOutputForm : EditForm
	{
		public HyperMainForm? MainForm { get; set; } = null;
		public Font OutputFont
		{
			get { return textBox1.Font; }
			set { textBox1.Font = value; }
		}
		public JSOutputForm()
		{
			InitializeComponent();
		}
		public override void OnButtunClick(EventArgs e)
		{
			base.OnButtunClick(e);
			this.Hide();
		}
		
		public void writeLine(object? o)
		{
			textBox1.Focus();
			textBox1.AppendText(HyperScript.toString(o) + "\r\n");
		}
		public void write(object? o)
		{
			textBox1.Focus();
			textBox1.AppendText(HyperScript.toString(o));
		}
		public void clear()
		{
			textBox1.Text = "";
		}

		private void BtnFont_Click(object sender, EventArgs e)
		{
			using (FontDialog dlg = new FontDialog())
			{
				dlg.Font = OutputFont;
				if(dlg.ShowDialog()==DialogResult.OK)
				{
					OutputFont = dlg.Font;
					if(MainForm!=null) MainForm.OutputFormFont = dlg.Font;
				}
			}
		}

		private void BtnCLS_Click(object sender, EventArgs e)
		{
			textBox1.Text = "";
		}
	}
}
