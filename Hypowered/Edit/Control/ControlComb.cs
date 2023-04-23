using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class ControlComb : ToolStripComboBox
	{
		private HForm? m_HForm = null;
		public HForm? HForm
		{
			get { return m_HForm; }
			set
			{
				m_HForm = value;
				MakeCombo();
				if (m_HForm != null)
				{
					m_HForm.ControlChanged += (sender, e) => { MakeCombo(); };
					m_HForm.TargetControlChanged += (sender, e) =>
					{
						if (m_HForm.TargetControl != null)
						{
							if (this.SelectedIndex != m_HForm.TargetControl.Index)
							{
								this.SelectedIndex = m_HForm.TargetControl.Index;
							}

						}
					};
					m_HForm.ControlNameChanged += (sender, e) =>
					{
						if (m_HForm.TargetControl != null)
						{
							if ((e.CtrlIndex >= 0) && (e.CtrlIndex < this.Items.Count))
							{
								this.Items[e.CtrlIndex] = e.Name;
							}
						}
					};
				}
			}
		}
		public ControlComb()
		{
			this.DropDownStyle = ComboBoxStyle.DropDownList;

		}
		private void MakeCombo()
		{
			this.Items.Clear();
			if (m_HForm == null) return;
			if (m_HForm.Controls.Count > 0)
			{
				List<string> items = new List<string>();
				foreach (Control c in m_HForm.Controls)
				{
					if ((c is HMainMenu) || (c is HControl))
					{
						items.Add(c.Name);
					}
				}
				this.Items.AddRange(items.ToArray());

				if (m_HForm.TargetControl != null)
				{
					if (this.SelectedIndex != m_HForm.TargetControl.Index)
					{
						this.SelectedIndex = m_HForm.TargetControl.Index;
					}
				}

			}
		}
	}
}
