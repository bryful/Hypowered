using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class HyperScriptEditor : Form
	{
		private HyperForm? m_HyperForm = null;
		public HyperForm? HyperForm
		{
			get { return m_HyperForm; }
			set { SetHyperForm(value); }
		}
		public void SetHyperForm(HyperForm? fm)
		{
			m_HyperForm = fm;
			if(m_HyperForm!=null)
			{
				m_HyperForm.TargetChanged += M_HyperForm_TargetChanged;
			}
		}
		public void SetHyperControl(HyperControl? c)
		{
			m_HyperControl = c;
			if (m_HyperControl != null)
			{
				editPad1.Text = m_HyperControl.ScriptCodes;
				this.Text = m_HyperControl.Name;
			}
		}
		private void M_HyperForm_TargetChanged(object sender, TargetChangedEventArgs e)
		{
			SetHyperControl(e.control);
		}

		private HyperControl? m_HyperControl = null;
		[Category("Hypowerd")]
		public string ScriptCode
		{
			get { return editPad1.Text; }
		}
		public HyperScriptEditor()
		{
			this.Name = "HyperScriptEditor";
			this.SetStyle(
//ControlStyles.Selectable |
//ControlStyles.UserMouse |
ControlStyles.DoubleBuffer |
ControlStyles.UserPaint |
ControlStyles.AllPaintingInWmPaint |
ControlStyles.SupportsTransparentBackColor,
true);
			this.UpdateStyles();
			//MakeMenu();
			InitializeComponent();

			menuHide.Click += MenuHide_Click;
			menuControl.Click += MenuControl_Click;
		}

		private void MenuControl_Click(object? sender, EventArgs e)
		{
			if (m_HyperForm != null)
			{
				ToolStripMenuItem[] m =  m_HyperForm.GetMenuControls(m_HyperControl, Mi_Click);
				menuControl.DropDownItems.Clear();
				menuControl.DropDownItems.AddRange(m);

			}

		}

		private void Mi_Click(object? sender, EventArgs e)
		{
			if ((m_HyperForm!=null)&&(m_HyperControl!=null))
			{
				ToolStripMenuItem? mi = (ToolStripMenuItem?)sender;
				if ((mi != null)&&(mi.Tag is HyperControl))
				{
					SetHyperControl((HyperControl)mi.Tag);
				}
			}
			
		}

		private void MenuHide_Click(object? sender, EventArgs e)
		{
			if(m_HyperForm != null)
			{
				this.Visible= false;
			}
		}

		private void BtnOK_Click(object sender, EventArgs e)
		{
			this.Visible = false;
		}

		private void btnWrite_Click(object sender, EventArgs e)
		{
			if(m_HyperControl !=null)
			{
				m_HyperControl.ScriptCodes = editPad1.Text;
			}
		}
	}
}
