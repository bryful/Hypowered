using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class FormComb :ComboBox
	{
		private MainForm? m_MainForm = null;
		public MainForm? MainForm
		{
			get { return m_MainForm; }
			set
			{
				m_MainForm = value;
				MakeCombo();
				if(m_MainForm != null)
				{
					m_MainForm.FormChanged += (sender, e) => { MakeCombo(); };
					m_MainForm.TargetFormChanged += (sender, e) =>
					{
						if (m_MainForm.TargetForm != null)
						{
							if ((this.SelectedIndex != m_MainForm.TargetForm.Index)
							&&(m_MainForm.TargetForm.Index<this.Items.Count))
							{
								this.SelectedIndex = m_MainForm.TargetForm.Index;
							}

						}
					};
				}
			}
		}
		public HForm? TargetForm
		{
			get
			{
				HForm? ret = null;
				if((m_MainForm!=null)&&(this.SelectedIndex>=0))
				{
					ret = m_MainForm.HForms[this.SelectedIndex];
				}
				return ret;
			}
			set
			{
				if (m_MainForm == null) return;
				if(value==null)
				{
					this.SelectedIndex = -1;
				}
				else
				{
					if(value.Index!= this.SelectedIndex)
					{
						this.SelectedIndex = value.Index;
					}
				}
			}
		}
		public FormComb()
		{
			this.DropDownStyle = ComboBoxStyle.DropDownList;

		}
		private void MakeCombo()
		{
			this.Items.Clear();
			if (m_MainForm == null) return;
			if (m_MainForm.HForms.Count>0)
			{
				List<string> items = new List<string>();
				foreach(HForm hf in m_MainForm.HForms)
				{
					items.Add(hf.Name);
				}
				this.Items.AddRange(items.ToArray());

				if(m_MainForm.TargetForm!=null)
				{
					if (this.SelectedIndex != m_MainForm.TargetForm.Index)
					{
						this.SelectedIndex = m_MainForm.TargetForm.Index;
					}
				}

			}
		}
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			if(m_MainForm != null)
			{
				if(m_MainForm.TargetFormIndex != this.SelectedIndex)
				{
					m_MainForm.TargetFormIndex = this.SelectedIndex;
				}
			}
			//base.OnSelectedIndexChanged(e);
		}
	}
}
