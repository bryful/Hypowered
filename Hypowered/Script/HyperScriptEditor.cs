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
		private HyperControl? m_HyperControl = null;
		private HyperScriptCode? m_HyperScriptCode = null;
		private string[] m_texts = new string[0];
		public void SetHyperForm(HyperForm? fm)
		{
			m_HyperForm = fm;
			controlBrowser1.SetHyperForm(fm);
			if (m_HyperForm!=null)
			{
				m_HyperControl = m_HyperForm.TargetControl;
				if (m_HyperControl!=null)
				{
					Type t = m_HyperControl.GetType();
					SetScriptCode( m_HyperControl .ScriptCode);
					this.Text = m_HyperControl.Name;
				}
				else
				{
					SetScriptCode(m_HyperForm.ScriptCode);
					this.Text = m_HyperForm.Name;
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

		private void btnOK_Click_1(object sender, EventArgs e)
		{
			if (m_HyperScriptCode != null)
			{
				if (cmbScript.SelectedIndex >= 0)
				{
					m_texts[cmbScript.SelectedIndex] = editPad1.Text;
				}
				m_HyperScriptCode.SetScriptCodes(m_texts);
			}
			this.DialogResult = DialogResult.OK;
		}
		private void MenuHide_Click(object? sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
	}
}
