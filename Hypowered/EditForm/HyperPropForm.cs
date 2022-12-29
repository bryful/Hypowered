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

	public partial class HyperPropForm : Form
	{
		protected HyperMainForm? m_HyperForm = null;
		public HyperMainForm? HyperForm
		{
			get { return m_HyperForm; }
			set 
			{ 
				m_HyperForm = value;
				if(m_HyperForm!=null)
				{
					SetHyperControl(m_HyperForm.TargetControl);
					m_HyperForm.TargetChanged += M_HyperForm_TargetChanged;

				}
			}
		}
		private HyperControl? m_HyperControl = null;


		private void M_HyperForm_TargetChanged(object sender, TargetChangedEventArgs e)
		{
			if (m_HyperForm != null)
			{
				SetHyperControl(m_HyperForm.TargetControl);
			}
		}
		private void SetHyperControl(HyperControl? c)
		{
			if (m_HyperForm != null)
			{
				m_HyperControl = m_HyperForm.TargetControl;
				if (m_HyperControl != null)
				{
					try
					{
						propertyGrid1.SelectedObject = m_HyperControl;
						lbCaption.Text = m_HyperControl.Name;
					}
					catch(Exception ex)
					{

					}
				}
				else
				{
					propertyGrid1.SelectedObject = m_HyperForm;
					lbCaption.Text = m_HyperForm.Name;
				}
			}
		}

		public HyperPropForm()
		{
			InitializeComponent();
			propertyGrid1.CollapseAllGridItems();
		}

		private void ToolStripButton1_Click(object sender, EventArgs e)
		{
			this.Visible = false;
		}

		private void BtnMainForm_Click(object sender, EventArgs e)
		{
			if(m_HyperForm!=null)
			{
				m_HyperForm.Activate();
			}
		}

		private void toolStripDropDownButton1_Click(object sender, EventArgs e)
		{
			if (m_HyperForm != null)
			{
				ToolStripMenuItem[] m = m_HyperForm.GetMenuControls(m_HyperControl, Mi_Click);
				menuControl.DropDownItems.Clear();
				menuControl.DropDownItems.AddRange(m);

			}

		}

		private void Mi_Click(object? sender, EventArgs e)
		{
			if ((m_HyperForm != null) && (m_HyperControl != null))
			{
				ToolStripMenuItem? mi = (ToolStripMenuItem?)sender;
				if ((mi != null) && (mi.Tag is HyperControl))
				{
					m_HyperForm.TargetIndex = ((HyperControl)mi.Tag).Index;
				}
			}

		}
	}
}
