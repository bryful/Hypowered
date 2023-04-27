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
	public partial class YesNoDialog : BaseForm
	{
		public string Caption
		{
			get { return label1.Text; }
			set { label1.Text = value; }
		}
		public YesNoDialog()
		{
			InitializeComponent();
			this.AcceptButton = btnOK;
			this.CancelButton = btnCancel;
			StartPosition = FormStartPosition.CenterParent;
		}

		static public bool Exec(string cap,string tl="")
		{
			bool ret = false;
			using (YesNoDialog dlg = new YesNoDialog())
			{
				if (tl != "") dlg.Text = tl;
				dlg.Caption = cap;
				ret = (dlg.ShowDialog() == DialogResult.OK);
			}
			return ret;
		}
	}
}
