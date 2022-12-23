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

	public partial class PropertyForm : Form
	{
		protected HyperForm? m_HyperForm = null;
		public HyperForm? HyperForm
		{
			get { return m_HyperForm; }
			set 
			{ 
				m_HyperForm = value;
				if(m_HyperForm!=null)
				{
					GetPropInfo();
					m_HyperForm.TargetChanged += M_HyperForm_TargetChanged;

				}
			}
		}

		private void M_HyperForm_TargetChanged(object sender, TargetChangedEventArgs e)
		{
			GetPropInfo();
		}
		private void GetPropInfo()
		{
			if (m_HyperForm != null)
			{
				HyperControl? m = m_HyperForm.TargetControl;
				if (m != null)
				{
					propertyGrid1.SelectedObject = m;
				}
				else
				{
					propertyGrid1.SelectedObject = m_HyperForm;
				}
			}
		}
		/*
		public object SelectedObject
		{
			get { return propertyGrid1.SelectedObject; }
			set 
			{
				try
				{
					if(value!=null)
						propertyGrid1.SelectedObject = value;
				}
				catch
				{

				}
			}
		}
		*/
		public PropertyForm()
		{
			InitializeComponent();
			propertyGrid1.CollapseAllGridItems();
		}

		private void ToolStripButton1_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void BtnMainForm_Click(object sender, EventArgs e)
		{
			if(m_HyperForm!=null)
			{
				m_HyperForm.Activate();
			}
		}
	}
}
