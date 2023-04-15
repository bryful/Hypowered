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
    public partial class ScriptEditForm : EditForm
	{
		private bool EditNow = false;
		private HyperMainForm? m_MainForm = null;
		private HyperBaseForm? m_TargetForm = null;
		private HyperControl? m_TargetControl = null;
		private HyperScriptCode? m_HyperScriptCode = null;
		private ScriptKind m_TargetScriptKind = ScriptKind.MouseClick;
		private string[] m_texts = new string[0];
		public void SetMainForm(HyperMainForm? fm)
		{
			m_MainForm = fm;
			controlBrowser1.SetMainForm(fm);

			if(m_MainForm!= null)
			{
				if (m_MainForm.Script.app != null)
				{
					cmbWord1.Items.Clear();
					cmbWord1.Items.AddRange(m_MainForm.Script.app.members());
				}
				m_MainForm.FormChanged += (sender, e) =>
				{
					controlBrowser1.SetMainForm(fm);
				};
				m_MainForm.ControlChanged += (sender, e) =>
				{
					controlBrowser1.SetMainForm(fm);
				};
			}
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
				btnScriptEdit1.Enabled = false;
				btnExec1.Enabled = true;
				btnEditEnd1.Enabled = true;
				cmbScript1.Enabled = true;
				if (m_TargetControl != null)
				{
					Type t = m_TargetControl.GetType();
					SetScriptCode(m_TargetControl.ScriptCode);
					this.Text = m_TargetControl.Name;

				}
				else
				{
					if (m_TargetForm is HyperMainForm)
					{
						SetScriptCode(((HyperMainForm)m_TargetForm).ScriptCode);
						this.Text = m_TargetForm.Name;
					}
				}
			}
		}
		protected void SetScriptCode(HyperScriptCode sc)
		{

			cmbScript1.Items.Clear();
			editPad1.Text = "";
			if (sc == null) return;
			m_HyperScriptCode = sc;
			string[] aa = m_HyperScriptCode.ValidSprictNames;
			cmbScript1.Items.AddRange(m_HyperScriptCode.ValidSprictNames);
			m_texts = m_HyperScriptCode.GetScriptCodes();
			if(m_texts.Length > 0)
			{
				cmbScript1.SelectedIndex = 0;
				editPad1.Text = m_texts[0];
				m_TargetScriptKind = m_HyperScriptCode.ScriptKinds[cmbScript1.SelectedIndex];
				oldIndex = 0;
			}
			cmbScript1.SelectedIndexChanged += CmbScript_SelectedIndexChanged;
		}

		private int oldIndex = -1;
		private void CmbScript_SelectedIndexChanged(object? sender, EventArgs e)
		{
			if(oldIndex>=0)
			{
				m_texts[oldIndex] = editPad1.Text;
				oldIndex = cmbScript1.SelectedIndex;
			}
			if(cmbScript1.SelectedIndex>=0)
			{
				editPad1.Text= m_texts[cmbScript1.SelectedIndex];
				if (m_HyperScriptCode != null)
				{
					m_TargetScriptKind = m_HyperScriptCode.ScriptKinds[cmbScript1.SelectedIndex];
				}
			}
			else
			{
				editPad1.Text = "";
			}
		}

		public ScriptEditForm()
		{
			this.Name = "HyperScriptEditForm";
			//controlBrowser1.ForeColor = ForeColor;
			//controlBrowser1.BackColor= BackColor;

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
			controlBrowser1.EditPad = editPad1;
			cmbWord1.SelectedIndexChanged += (sender, e) =>
			{
				ComboBox ? cmb = (ComboBox?)sender;
				if (cmb == null) return;
				if (cmb.SelectedIndex >= 0)
				{
					if (cmb.SelectedItem != null)
					{
						editPad1.SetText(cmb.SelectedItem.ToString());
					}
				}
			};

		}

		private void btnExec_Click(object sender, EventArgs e)
		{
			if (EditNow == false) return;
			if ((m_MainForm!=null))
			{
				if (cmbScript1.SelectedIndex >= 0)
				{
					WriteCode();
					if (m_TargetControl != null)
					{
						m_TargetControl.ExecScript(m_TargetScriptKind);
					}
					else
					{
						m_MainForm.ExecScript(m_TargetScriptKind);
					}
				}
			}
		}
		private void WriteCode()
		{
			if (EditNow == false) return;
			if (m_HyperScriptCode != null)
			{
				if (cmbScript1.SelectedIndex >= 0)
				{
					m_texts[cmbScript1.SelectedIndex] = editPad1.Text;
					m_HyperScriptCode.SetScriptCodes(m_texts);
				}
			}
		}
		public override void OnCloseButtunClick(EventArgs e)
		{
			if (m_MainForm == null) return;
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
			this.Text = "(None)";
			editPad1.Text = "";
			btnScriptEdit1.Enabled = true;
			btnExec1.Enabled = false;
			btnEditEnd1.Enabled = false;
			cmbScript1.Enabled = false;
			cmbScript1.Items.Clear();
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
