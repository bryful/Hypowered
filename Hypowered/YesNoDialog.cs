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
	public partial class YesNoDialog : EditForm
	{
		public string Caption
		{
			get { return textBox1.Text;}
			set { textBox1.Text = value; }
		}
		public YesNoDialog()
		{
			InitializeComponent();
		}
	}

	static public class answerDialog
	{
		static public bool Show(string cap,string? tx=null)
		{
			using(YesNoDialog dlg = new YesNoDialog())
			{
				if (tx != null) dlg.Text = tx;
				dlg.Caption = cap;
				return (dlg.ShowDialog() == DialogResult.OK);
			}
		}
	}
}
