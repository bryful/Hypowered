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
		public void SetHyperForm(HyperForm fm)
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
				editPad1.Text = m_HyperControl.ScriptCode;
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
				List<ToolStripMenuItem> list = new List<ToolStripMenuItem>();
				if (m_HyperForm.Controls.Count>0)
				{
					foreach(Control c in m_HyperForm.Controls)
					{

						if( c is HyperControl)
						{
							HyperControl hc = (HyperControl)c;
							ToolStripMenuItem mi = new ToolStripMenuItem();
							if (m_HyperControl != null) 
							{
								mi.Checked = (hc.Index == m_HyperControl.Index);
							}
							mi.Text = hc.Name;
							mi.Tag = (object)hc.Index;
							mi.Click += Mi_Click;
							list.Add(mi);
						}
					}
				}
				menuControl.DropDownItems.Clear();
				menuControl.DropDownItems.AddRange(list.ToArray());

			}

		}

		private void Mi_Click(object? sender, EventArgs e)
		{
			if ((m_HyperForm!=null)&&(m_HyperControl!=null))
			{
				ToolStripMenuItem? mi = (ToolStripMenuItem?)sender;
				if (mi != null)
				{
					SetHyperControl((HyperControl)m_HyperForm.Controls[(int)mi.Tag]);
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
				m_HyperControl.ScriptCode = editPad1.Text;
			}
		}
	}
}
