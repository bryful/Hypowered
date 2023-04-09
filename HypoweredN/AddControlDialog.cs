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
	public partial class AddControlDialog : BaseForm
	{
		public HForm? HForm = null;
		public HType HType
		{
			get { return hTypeCombo1.HType; }
			set { hTypeCombo1.HType = value; }
		}
		public string CName
		{
			get { return textBox1.Text; }
			set { textBox1.Text = value; }
		}
		public AddControlDialog()
		{
			this.StartPosition = FormStartPosition.CenterParent;
			InitializeComponent();
			textBox1.TextChanged += (sender, e) =>
			{
				btnOK.Enabled = (textBox1.Text != "");
			};
			btnOK.Click += (sender, e) =>
			{
				this.DialogResult = DialogResult.OK;
			};
		}
	}
}
