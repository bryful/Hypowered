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
	public partial class HyperControlList : Form
	{
		private HyperMainForm? m_form = null;
		[Category("Hypowerd")]
		public HyperMainForm? MainForm
		{
			get { return controlListBox1.MainForm; }
			set 
			{
				m_form = value;
				controlListBox1.MainForm = value;
			}
		}
		public HyperControlList()
		{
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			InitializeComponent();
		}

		private void btnHide_Click(object sender, EventArgs e)
		{
			this.Visible= false;
		}
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if (m_form != null)
			{
				m_form.ControlListBounds = this.Bounds;
			}
		}
		protected override void OnLocationChanged(EventArgs e)
		{
			base.OnLocationChanged(e);
			if (m_form != null)
			{
				m_form.ControlListBounds = this.Bounds;
			}
		}
	}
}
