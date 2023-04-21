using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class ControlListBox : EditListBox
	{
		// ********************************************************
		private HForm? m_HForm = null;
		public HForm? HForm
		{
			get { return m_HForm; }
			set { SetHForm(value); }
		}
		public PropertyGrid? PropertyGrid { get; set; } = null;
		// ********************************************************
		public void SetHForm(HForm? hf)
		{
			m_HForm = hf;

			if(m_HForm!=null)
			{
				m_HForm.ControlChanged -= (seder, e) => { Scan(); };
				m_HForm.ControlChanged += (seder, e) => { Scan(); };
				m_HForm.SelectedChanged -= (seder, e) => { SetSelectArray(e.Selecteds); };
				m_HForm.SelectedChanged += (seder, e) => { SetSelectArray(e.Selecteds); };
				m_HForm.ControlNameChanged -= HForm_ControlNameChanged;
				m_HForm.ControlNameChanged += HForm_ControlNameChanged;
			}
			Scan();

		}

		// ********************************************************
		private void HForm_ControlNameChanged(object sender, ControlChangedEventArgs e)
		{
			if (m_HForm == null) return;
			if ((e.FormIndex == m_HForm.Index))
			{
				if ((e.CtrlIndex >= 0) && (e.CtrlIndex < this.Items.Count))
				{
					this.Items[e.CtrlIndex] = e.Name;
				}
			}
		}

		// ********************************************************
		public void Scan()
		{
			this.Items.Clear();
			if (m_HForm == null) return;
			if(m_HForm.Controls.Count > 0) 
			{
				List<string> list = new List<string>();
				List<bool> slist = new List<bool>();
				foreach (Control c in m_HForm.Controls)
				{
					if (c is HMainMenu) 
					{
						list.Add(c.Name);
						slist.Add(false);
					}
					else if (c is HControl)
					{
						list.Add(c.Name);
						slist.Add(((HControl)c).Selected);
					}

				}
				this.Items.AddRange(list.ToArray());
				this.SetSelectArray(slist.ToArray());	
			}
		}
		// ********************************************************
		public ControlListBox()
		{
			this.BorderStyle = BorderStyle.FixedSingle;
			this.ScrollAlwaysVisible = true;
			this.SelectionMode = SelectionMode.MultiExtended;
		}
		// ********************************************************
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			if (m_HForm == null) return;
			int[] sels = this.SelectedIndexArray;

			if (sels.Length >= 1)
			{
				List<object> list = new List<object>();
				foreach (int c in sels)
				{
					if ((c >= 0) && (c < m_HForm.Controls.Count))
					{
						list.Add(m_HForm.Controls[c]);
					}
				}
				if (list.Count > 0)
				{
					if (PropertyGrid != null)
					{
						PropertyGrid.SelectedObjects = list.ToArray();
					}
				}
			}
			if (sels.Length == 0)
			{
				m_HForm.SetSelectedAll();
			}
			else
			{
				if (sels.Length == 1)
				{
					m_HForm.TargetIndex = sels[0];
				}
				m_HForm.SetSelecteds(sels);
			}
		}
	}
}
