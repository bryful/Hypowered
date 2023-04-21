using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public  class FormListBox : EditListBox
	{
		public ControlListBox? ControlListBox { get; set; } = null;
		public PropertyGrid? PropertyGrid { get; set; } = null;

		private MainForm? m_MainForm = null;
		public MainForm? MainForm
		{
			get { return m_MainForm; }
			set { SetMainForm(value); }
		}
		// ********************************************************
		public void SetMainForm(MainForm? hf)
		{
			m_MainForm = hf;
			Scan();
			if (MainForm != null)
			{
				MainForm.FormChanged -= (seder, e) => { Scan(); };
				MainForm.FormChanged += (seder, e) => { Scan(); };
				MainForm.TargetFormChanged -= (sender, e) => { SetTargetForm(MainForm.TargetForm); };
				MainForm.TargetFormChanged += (sender, e) => { SetTargetForm(MainForm.TargetForm); }; 

			}

			if(ControlListBox != null)
			{
				if (MainForm != null)
				{
					ControlListBox.SetHForm(MainForm.TargetForm);
				}
			}

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
				if (MainForm.TargetControl == null) MainForm.TargetFormIndex = 0;
				SetTargetForm(MainForm.TargetForm);
			}
		}
		// ********************************************************
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			if(MainForm != null)
			{
				if(ControlListBox != null)
				{
					ControlListBox.SetHForm(MainForm.TargetForm);
				}
				if(PropertyGrid != null)
				{
					PropertyGrid.SelectedObject = MainForm.TargetForm;
				}
			}
			else
			{
				base.OnSelectedIndexChanged(e);
			}
		}
		// ********************************************************
		public FormListBox()
		{
			this.BorderStyle = BorderStyle.FixedSingle;
			this.ScrollAlwaysVisible = true;
		}
	}
}
