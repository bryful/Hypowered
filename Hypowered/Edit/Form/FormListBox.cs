using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public  class FormListBox : EditListBox
	{
		// ******************************************************
		public delegate void SelectObjectsChangedHandler(object sender, SelectObjectsChangedArgs e);
		public event SelectObjectsChangedHandler? SelectObjectsChanged;
		protected virtual void OnSelectObjectChanged(SelectObjectsChangedArgs e)
		{
			if (SelectObjectsChanged != null)
			{
				SelectObjectsChanged(this, e);
			}
		}
		// ******************************************************
		public delegate void TargetFormChangedHandler(object sender, TargetFormChangedArgs e);
		public event TargetFormChangedHandler? TargetFormChanged;
		protected virtual void OnTargetFormChanged(TargetFormChangedArgs e)
		{
			if (TargetFormChanged != null)
			{
				TargetFormChanged(this, e);
			}
		}

		// ******************************************************
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

		}
		private HForm? m_TargetForm = null;
		public HForm? TargetForm
		{
			get { return m_TargetForm; }
		}

		// ********************************************************
		public void SetTargetForm(HForm? hForm)
		{
			int v = -1;
			m_TargetForm = hForm;
			if (m_TargetForm != null) 
			{
				m_TargetForm.FormNameChanged -= HForm_FormNameChanged;
				m_TargetForm.FormNameChanged += HForm_FormNameChanged;
				v = m_TargetForm.Index;
			}
			if (this.SelectedIndex != v)
			{
				this.SelectedIndex = v;
			}
		}

		private void HForm_FormNameChanged(object sender, FormChangedEventArgs e)
		{
			if ((e.Index>=0)&&(e.Index<this.Items.Count))
			{
				this.Items[e.Index] = e.Name;
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
				int idx = this.SelectedIndex;
				if((idx>=0)&&(idx< MainForm.HForms.Count))
				{
					m_TargetForm = MainForm.HForms[idx];
				}
				else
				{
					this.SelectedIndex = -1;
					m_TargetForm = null;
				}
				OnSelectObjectChanged(new SelectObjectsChangedArgs(new object?[] { m_TargetForm }));
				OnTargetFormChanged(new TargetFormChangedArgs(m_TargetForm));
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
