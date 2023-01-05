using Accessibility;
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
		private bool EditNow = false;
		private HyperMainForm? m_MainForm = null;
		private HyperBaseForm? m_TargetForm = null;
		private HyperControl? m_TargetControl = null;
		private HyperScriptCode? m_HyperScriptCode = null;
		private string[] m_texts = new string[0];
		public void SetMainForm(HyperMainForm? fm)
		{
			m_MainForm = fm;
			controlBrowser1.SetMainForm(fm);
			/*
			if ((m_MainForm!=null)&&(m_MainForm.forms.TargetForm!=null))
			{
				m_TargetControl = m_MainForm.forms.TargetForm.TargetControl;
				if (m_TargetControl!=null)
				{
					Type t = m_TargetControl.GetType();
					SetScriptCode( m_TargetControl.ScriptCode);
					this.Text = m_TargetControl.Name;
				}
				else
				{
					SetScriptCode(((HyperMainForm)m_MainForm.forms.TargetForm).ScriptCode);
					this.Text = m_MainForm.forms.TargetForm.Name;
				}
			}
			*/
		}
		public void SetTargetControl(HyperBaseForm? tf, HyperControl? tc)
		{
			if (EditNow) return;
			EditNow = false;
			if (m_MainForm == null) return;
			if (m_MainForm.IsEditMode == false) return;
			if (tf != null)
			{
				m_TargetForm = tf;
				m_TargetControl = tc;
				m_MainForm.CanEditMode = false;
				EditNow = true;
				btnScriptEdit.Enabled = false;
				btnExec.Enabled = true;
				btnEditEnd.Enabled = true;
				cmbScript.Enabled = true;
				if (m_TargetControl != null)
				{
					Type t = m_TargetControl.GetType();
					SetScriptCode(m_TargetControl.ScriptCode);
					lbControl.Text =
					this.Text = m_TargetControl.Name;

				}
				else
				{
					if(m_TargetForm is HyperMainForm)
					{
						SetScriptCode(((HyperMainForm)m_TargetForm).ScriptCode);
						lbControl.Text =
								this.Text = m_TargetForm.Name;
					}
				}
			}
		}
		protected void SetScriptCode(HyperScriptCode sc)
		{

			cmbScript.Items.Clear();
			editPad1.Text = "";
			if (sc == null) return;
			m_HyperScriptCode = sc;
			string[] aa = m_HyperScriptCode.ValidSprictNames;
			cmbScript.Items.AddRange(m_HyperScriptCode.ValidSprictNames);
			m_texts = m_HyperScriptCode.GetScriptCodes();
			if(m_texts.Length > 0)
			{
				cmbScript.SelectedIndex = 0;
				editPad1.Text = m_texts[0];
				oldIndex = 0;
			}
			cmbScript.SelectedIndexChanged += CmbScript_SelectedIndexChanged;
		}

		private int oldIndex = -1;
		private void CmbScript_SelectedIndexChanged(object? sender, EventArgs e)
		{
			if(oldIndex>=0)
			{
				m_texts[oldIndex] = editPad1.Text;
				oldIndex = cmbScript.SelectedIndex;
			}
			if(cmbScript.SelectedIndex>=0)
			{
				editPad1.Text= m_texts[cmbScript.SelectedIndex];
			}
			else
			{
				editPad1.Text = "";
			}
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

		}

		private void btnExec_Click(object sender, EventArgs e)
		{
			if (EditNow == false) return;
			if ((m_MainForm!=null) &&(m_HyperScriptCode != null))
			{
				if (cmbScript.SelectedIndex >= 0)
				{
					WriteCode(); 
					m_MainForm.ExecuteCode(m_texts[cmbScript.SelectedIndex]);
				}
			}
		}
		private void WriteCode()
		{
			if (EditNow == false) return;
			if (m_HyperScriptCode != null)
			{
				if (cmbScript.SelectedIndex >= 0)
				{
					m_texts[cmbScript.SelectedIndex] = editPad1.Text;
					m_HyperScriptCode.SetScriptCodes(m_texts);
				}
			}
		}
		private void MenuHide_Click(object? sender, EventArgs e)
		{
			if(m_MainForm==null) return;
			EditEnd();
			this.Visible = false;
		}
		protected override void OnLocationChanged(EventArgs e)
		{
			base.OnLocationChanged(e);
			if(m_MainForm!=null)
			{
				m_MainForm.ScriptEditBounds = this.Bounds;
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (m_MainForm != null)
			{
				m_MainForm.ScriptEditBounds = this.Bounds;
			}
		}

		private void BtnEditEnd_Click(object sender, EventArgs e)
		{
			EditEnd();
		}
		private void EditEnd()
		{
			if (m_MainForm == null) return;
			if (EditNow == false) return;
			WriteCode();
			m_MainForm.CanEditMode = true;
			m_MainForm.SetIsEditMode(true);
			EditNow = false;
			lbControl.Text = "(None)";
			this.Text = lbControl.Text;
			editPad1.Text = "";
			btnScriptEdit.Enabled = true;
			btnExec.Enabled = false;
			btnEditEnd.Enabled = false;
			cmbScript.Enabled = false;
			cmbScript.Items.Clear();
			oldIndex = -1;
		}

		private void BtnScriptEdit_Click(object sender, EventArgs e)
		{
			if(m_MainForm==null) return;
			if(EditNow == true) return;
			if (m_MainForm.targetForm == null) return;
			SetTargetControl(m_MainForm.targetForm, m_MainForm.TargetControl);
		}
	}
}
