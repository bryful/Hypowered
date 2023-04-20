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
		public HForm? HForm = null;
		// ********************************************************
		public void SetHForm(HForm? hf)
		{
			HForm = hf;

			if(HForm!=null)
			{
				HForm.ControlChanged -= (seder, e) => { Scan(); };
				HForm.ControlChanged += (seder, e) => { Scan(); };
				HForm.SelectedChanged -= (seder, e) => { SetSelectArray(e.Selecteds); };
				HForm.SelectedChanged += (seder, e) => { SetSelectArray(e.Selecteds); };
				HForm.ControlNameChanged -= HForm_ControlNameChanged;
				HForm.ControlNameChanged += HForm_ControlNameChanged;
			}
			Scan();

		}

		private void HForm_ControlNameChanged(object sender, ControlChangedEventArgs e)
		{
			if (HForm == null) return;
			if ((e.FormIndex == HForm.Index))
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
			if (HForm == null) return;
			if(HForm.Controls.Count > 0) 
			{
				List<string> list = new List<string>();
				List<bool> slist = new List<bool>();
				foreach (Control c in HForm.Controls)
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

		}
	}
}
