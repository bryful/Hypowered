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
		private string ToS(object? o)
		{
			string ret = "";
			if(o ==null)
			{
				ret = "(null)";
			}else if (o is Array)
			{
				foreach(object o1 in (Array)o)
				{
					if (o1 == null) continue;
					if (ret != "") ret += ",";
					ret += o1.ToString();
				}
				ret= "[" + ret+"]";
			}
			else 
			{
				ret = o.ToString();
			}
			return ret;
		}
		public void writeLine(object? o)
		{
			textBox1.Focus();
			textBox1.AppendText(ToS(o)+"\r\n");
		}
		public void write(object? o)
		{
			textBox1.Focus();
			textBox1.AppendText(ToS(o));
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
					if(MainForm!=null) MainForm.OutputFormFont = dlg.Font; ;
				}
			}
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			textBox1.Text = "";
		}
	}
}
