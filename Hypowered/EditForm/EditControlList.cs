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
	public partial class EditControlList : EditForm
	{
		private HyperMainForm? MainForm = null;

		public void SetMainForm(HyperMainForm? mf)
		{
			this.MainForm = mf;
			controlPanel1.SetMainForm(mf);
		}

		public EditControlList()
		{
			this.CanResize = true;
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			InitializeComponent();
		}
		public override void OnButtunClick(EventArgs e)
		{
			base.OnButtunClick(e);
			this.Hide();
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
		private void Mi_Click(object? sender, EventArgs e)
		{
			/*
			if (MainForm != null)
			{
				ToolStripMenuItem? mi = (ToolStripMenuItem?)sender;
				if ((mi != null) && (mi.Tag is HyperBaseForm))
				{
					MainForm.forms.TargetFormIndex = ((HyperBaseForm)mi.Tag).Index;
					controlListBox1.TargetForm = MainForm.forms.TargetForm;
					if (controlListBox1.TargetForm != null)
					{
						btnForm.Text = controlListBox1.TargetForm.Name;
					}
				}
			}
			*/

		}
	}
}
