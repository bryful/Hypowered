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
		private HyperMainForm? MainForm = null;

		public void SetMainForm(HyperMainForm? mf)
		{
			this.MainForm = mf;
			controlListBox1.SetMainForm(mf);
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
			if (MainForm != null)
			{
				MainForm.ControlListBounds = this.Bounds;
			}
		}
		protected override void OnLocationChanged(EventArgs e)
		{
			base.OnLocationChanged(e);
			if (MainForm != null)
			{
				MainForm.ControlListBounds = this.Bounds;
			}
		}
		private void btnForm_Click(object sender, EventArgs e)
		{
			if (MainForm != null)
			{

				ToolStripMenuItem[] m = MainForm.FormList.GetFormsForMenu(Mi_Click);
				btnForm.DropDownItems.Clear();
				btnForm.DropDownItems.AddRange(m);

			}

		}

		private void Mi_Click(object? sender, EventArgs e)
		{
			if (MainForm != null)
			{
				ToolStripMenuItem? mi = (ToolStripMenuItem?)sender;
				if ((mi != null) && (mi.Tag is HyperBaseForm))
				{
					MainForm.FormList.TargetIndex = ((HyperBaseForm)mi.Tag).Index;
					controlListBox1.TargetForm = MainForm.FormList.TargetForm;
					if (controlListBox1.TargetForm != null)
					{
						btnForm.Text = controlListBox1.TargetForm.Name;
					}
				}
			}

		}

		private void btnActive_Click(object sender, EventArgs e)
		{
			if(controlListBox1.TargetForm!= null)
			{
				controlListBox1.TargetForm.Activate();
			}
		}
	}
}
