using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public  class FormListBox : EditListBox
	{
		public MainForm? MainForm = null;
		// ********************************************************
		public void SetMainForm(MainForm? hf)
		{
			MainForm = hf;

			if (MainForm != null)
			{
				MainForm.FormChanged -= (seder, e) => { Scan(); };
				MainForm.FormChanged += (seder, e) => { Scan(); };
				MainForm.TargetFormChanged -= (sender, e) => { SetTargetForm(MainForm.TargetForm); };
				MainForm.TargetFormChanged += (sender, e) => { SetTargetForm(MainForm.TargetForm); }; 
			}
			Scan();

		}
		// ********************************************************
		public void SetTargetForm(HForm? hForm)
		{
			int v = -1;
			if(hForm != null) 
			{
				v = hForm.Index;
			}
			if(this.SelectedIndex !=v)
			{
				this.SelectedIndex = v;
			}
		}
		// ********************************************************
		public void Scan()
		{
			this.Items.Clear();
			if (MainForm == null) return;
			if (MainForm.HForms.Count > 0)
			{
				List<string> list = new List<string>();
				foreach(HForm hf in MainForm.HForms)
				{
					list.Add(hf.Name);
				}
				this.Items.AddRange(list.ToArray());
				if(MainForm.TargetForm != null)
				{
					this.SelectedIndex = MainForm.TargetFormIndex;
				}
			}
		}
	}
}
