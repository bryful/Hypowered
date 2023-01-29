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
	public partial class PropertyForm : Form
	{
		public Form? Form
		{
			get { return controlTree1.Form; }
			set { controlTree1.Form = value; }
		}
		public PropertyForm()
		{
			InitializeComponent();
			controlTree1.AfterSelect += (sender, e) =>
			{
				if (controlTree1.SelectedNode != null)
				{
					propertyGrid1.SelectedObject = controlTree1.SelectedNode.Tag;
				}
			};
		}

		private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.Hide();
		}
	}
}
