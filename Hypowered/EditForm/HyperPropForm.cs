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
				MainForm.FormChanged -= MainForm_FormChanged;
				MainForm.FormChanged += MainForm_FormChanged;
				SetTargetControl(MainForm, MainForm.TargetControl);
			}
		}

		private void MainForm_FormChanged(object sender, HyperChangedEventArgs e)
		{
			if (e.Form != null)
			{
				SetTargetControl(e.Form, e.Control);
			}
		}

		protected HyperBaseForm? m_TargetForm = null;

		private HyperControl? m_TargetControl = null;

		private void SetTargetControl(HyperBaseForm? bf, HyperControl? c)
		{
			if ((bf != null))
			{
				m_TargetForm = bf;
				btnForm.Text = bf.Name;
				bf.ControlChanged -= Bf_ControlChanged;
				bf.ControlChanged += Bf_ControlChanged;
				bf.CreatedControl -= Bf_CreatedControl;
				bf.CreatedControl += Bf_CreatedControl;
				bf.DeletetedControl -= Bf_CreatedControl;
				bf.DeletetedControl += Bf_CreatedControl;
				if (c != null)
				{
					m_TargetControl = c;
					btnControl.Text = c.Name;
					try
					{
						propertyGrid1.SelectedObject = c;
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
					}
					catch
					{

					}
				}
			}
		}

		private void Bf_CreatedControl(object sender, HyperChangedEventArgs e)
		{
			SetTargetControl(e.Form, e.Control);
		}

		private void Bf_ControlChanged(object sender, HyperChangedEventArgs e)
		{
			SetTargetControl(e.Form, e.Control);
		}

		public HyperPropForm()
		{
			InitializeComponent();
			propertyGrid1.CollapseAllGridItems();
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

		private void btnControl_Click(object sender, EventArgs e)
		{
			if ((m_TargetForm != null)&&(m_TargetControl!=null))
			{
				ToolStripMenuItem[] m = m_TargetForm.GetControlsForMenu(m_TargetControl, Mi_Click);
				btnControl.DropDownItems.Clear();
				btnControl.DropDownItems.AddRange(m);

			}

		}

		private void Mi_Click(object? sender, EventArgs e)
		{
			if ((m_TargetForm != null) && (m_TargetControl != null))
			{
				ToolStripMenuItem? mi = (ToolStripMenuItem?)sender;
				if ((mi != null) &&(mi.Tag!=null)&& (mi.Tag is HyperControl))
				{
					HyperControl c = (HyperControl)mi.Tag;
					m_TargetForm.TargetIndex = c.Index;
					SetTargetControl(m_TargetForm, c);
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

		private void btnForm_Click(object sender, EventArgs e)
		{
			if(MainForm!= null)
			{
				ToolStripMenuItem[] m = MainForm.GetFormsForMenu(m_TargetForm, Form_Click);
				btnForm.DropDownItems.Clear();
				btnForm.DropDownItems.AddRange(m);
			}
		}

		private void Form_Click(object? sender, EventArgs e)
		{
			if (MainForm != null)
			{
				ToolStripMenuItem? mi = (ToolStripMenuItem?)sender;
				if ((mi != null) && (mi.Tag is HyperBaseForm))
				{
					HyperBaseForm t= (HyperBaseForm)mi.Tag;
					MainForm.FormList.TargetIndex = t.Index;
					SetTargetControl(t, t.TargetControl);
				}
			}

		}
	}
}
