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

	public partial class PropertyForm : Form
	{
		public HyperForm? HyperForm = null;
		public object SelectedObject
		{
			get { return propertyGrid1.SelectedObject; }
			set 
			{
				try
				{
					if(value!=null)
						propertyGrid1.SelectedObject = value;
				}
				catch
				{

				}
			}
		}
		public PropertyForm()
		{
			InitializeComponent();
			propertyGrid1.CollapseAllGridItems();
		}

		private void ToolStripButton1_Click(object sender, EventArgs e)
		{
			this.Hide();
		}
	}
}
