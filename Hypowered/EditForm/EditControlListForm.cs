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
	public partial class EditControlListForm : Form
	{
		[Category("Hypowerd")]
		public HyperForm? HyperForm
		{
			get { return controlListBox1.HyperForm; }
			set { controlListBox1.HyperForm = value;}
		}
		public EditControlListForm()
		{
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			InitializeComponent();
		}
	}
}
