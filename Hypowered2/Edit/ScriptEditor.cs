using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hpd
{
	public partial class ScriptEditor : Form
	{
		private HpdScriptCode? m_ScriptCode = null;
		private Control? m_Target = null;
		public Control? Target
		{
			get { return m_Target; }
			set
			{ 
				if( value != null )
				{
					if((value is HpdControl)||(value is HpdForm))
					{
						m_Target = value;
					}
				}
			}
		}
		public bool SetEdit(bool b)
		{
			if (m_Target == null) return false;
			m_ScriptCode = null;
			if (b == true)
			{
				btnEditStart.Enabled = false;
				btnEditEnd.Enabled = true;
				cmbScriptType.Items.Clear();
				
				if (m_Target is HpdMainForm)
				{
					m_ScriptCode = ((HpdMainForm)m_Target).ScriptCode;
				} else if (m_Target is HpdControl)
				{
					m_ScriptCode = ((HpdControl)m_Target).ScriptCode;
				}
				if(m_ScriptCode != null)
				{
					cmbScriptType.Items.AddRange(m_ScriptCode.ScriptTypeNames);
					cmbScriptType.SelectedIndex = 0;
				}
			}
			else
			{
				btnEditStart.Enabled = true;
				btnEditEnd.Enabled = false;
				cmbScriptType.Items.Clear();

			}
			return true;
		}
		public HpdMainForm? MainForm = null;
		public void SetMainForm(HpdMainForm mf)
		{
			this.MainForm = mf;
			if(this.MainForm!=null)
			{
				this.MainForm.Items.TargetControlChanged += (sender, e) =>
				{
					Target= e.ctrl;
				};
			}
		}

		public ScriptEditor()
		{
			InitializeComponent();
			btnEditStart.Click += (sender, e) => { SetEdit(true); };
			btnEditEnd.Click += (sender, e) => { SetEdit(false); };

		}

		private void btnExecute_Click(object sender, EventArgs e)
		{
		}

		private void btnV8Execute_Click(object sender, EventArgs e)
		{
			if (MainForm != null)
			{
				MainForm.Script.ExecuteCode(roslynEdit1.Text);
			}
		}

		private void btnHide_Click(object sender, EventArgs e)
		{
			this.Hide();
		}
	}
}
