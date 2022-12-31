using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{

	public partial class HyperPropForm : Form
	{
		public bool IsShow = true;
		protected HyperMainForm? MainForm = null;
		public void SetMainForm(HyperMainForm? mf)
		{
			this.MainForm = mf;
			if (MainForm != null)
			{
				MainForm.TargetChanged -= MainForm_TargetChanged;
				MainForm.TargetChanged += MainForm_TargetChanged;
				SetTargetControl(mf, mf.TargetControl);
			}
		}

		private void MainForm_TargetChanged(object sender, TargetChangedEventArgs e)
		{
			if (e.Form != null)
			{
				SetTargetControl(e.Form, e.Control);
			}
		}

		protected HyperBaseForm? m_TargetForm = null;

		private HyperControl? m_TargetControl = null;

		private void SetTargetControl(HyperBaseForm bf, HyperControl? c)
		{
			m_TargetForm = bf;
			m_TargetControl = c;
			if ((bf != null))
			{
				if (c != null)
				{
					try
					{
						propertyGrid1.SelectedObject = c;
						lbCaption.Text = c.Name;
					}
					catch
					{

					}
				}
				else
				{
					try
					{
						propertyGrid1.SelectedObject = bf;
						lbCaption.Text = bf.Name;
					}
					catch
					{

					}
				}
			}
		}
	
		public new bool TopMost
		{
			get { return base.TopMost; }
			set
			{
				base.TopMost = value;
				btnTopMost.Checked= value;
			}
		}

		public HyperPropForm()
		{
			InitializeComponent();
			propertyGrid1.CollapseAllGridItems();
			btnTopMost.Checked = this.TopMost;
		}

		private void btnHide_Click(object sender, EventArgs e)
		{
			this.Visible = false;
		}

		private void btnActive_Click(object sender, EventArgs e)
		{
			if(m_TargetForm!=null)
			{
				m_TargetForm.Activate();
			}
		}

		private void menuControl_Click(object sender, EventArgs e)
		{
			if (m_TargetForm != null)
			{
				ToolStripMenuItem[] m = m_TargetForm.GetControlsForMenu(m_TargetControl, Mi_Click);
				menuControl.DropDownItems.Clear();
				menuControl.DropDownItems.AddRange(m);

			}

		}

		private void Mi_Click(object? sender, EventArgs e)
		{
			if ((m_TargetForm != null) && (m_TargetControl != null))
			{
				ToolStripMenuItem? mi = (ToolStripMenuItem?)sender;
				if ((mi != null) && (mi.Tag is HyperControl))
				{
					m_TargetForm.TargetIndex = ((HyperControl)mi.Tag).Index;
				}
			}

		}
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if (MainForm != null)
			{
				MainForm.PropFormBounds = this.Bounds;
			}
		}
		protected override void OnLocationChanged(EventArgs e)
		{
			base.OnLocationChanged(e);
			if (MainForm != null)
			{
				MainForm.PropFormBounds = this.Bounds;
			}
		}

		private void btnTopMost_Click(object sender, EventArgs e)
		{
			this.TopMost = ! this.TopMost;
		}
	}
}
