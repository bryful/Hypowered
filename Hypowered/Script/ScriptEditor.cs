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

namespace Hypowered
{
	public partial class ScriptEditor : BaseForm
	{
		public MainForm? MainForm = null;
		private HForm? m_TargetForm = null;
		public void SetMainForm(MainForm mf)
		{
			this.MainForm = mf;
			MakeFormComb();
			if (this.MainForm != null)
			{
				SetTargetForms(m_TargetForm);
				this.MainForm.FormChanged += (sender, e) =>
				{
					MakeFormComb();
				};
				this.MainForm.TargetFormChanged += (sender, e) =>
				{
					SetTargetForm();
				};
			}
		}
		private void MakeFormComb()
		{
			if (this.MainForm != null)
			{
				cmbTargetForm.Items.Clear();
				if (MainForm.HForms.Count > 0)
				{
					cmbTargetForm.Items.AddRange(MainForm.HFormsNames());
				}
			}
		}
		private void MakeControlComb()
		{
			if (this.m_TargetForm != null)
			{
				cmbTargetForm.Items.Clear();
				if (cmbTargetForm.Items.Count > 0)
				{
					cmbTargetForm.Items.AddRange(MainForm.HFormsNames());
				}
			}
		}
		private void SetTargetForms(HForm? hf )
		{
			m_TargetForm = hf;
			if (m_TargetForm != null)
			{

			}
		}
		private void SetTargetForm()
		{
			if (this.MainForm != null)
			{
				if (m_TargetForm != null)
				{
					if (m_TargetForm == MainForm.TargetForm) return;
				}
				m_TargetForm = MainForm.TargetForm;
				if (m_TargetForm != null)
				{
					if (MainForm.HForms.Count > 0)
					{
						int v = -1;
						if (MainForm.TargetForm != null)
						{
							v = MainForm.TargetForm.Index;
						}
						if ((v >= 0) && (v < cmbTargetForm.Items.Count))
						{
							if (cmbTargetForm.SelectedIndex != v)
							{
								cmbTargetForm.SelectedIndex = v;
							}
						}
					}
					m_TargetForm.FormNameChanged += (sender, e) =>
					{
						cmbTargetForm.Items[e.Index] = e.Name;
					};
					m_TargetForm.TargetControlChanged += (sender, e) =>
					{

					};
				}
			}
		}

		private HControl? m_TargetControl = null;
		public bool SetEdit(bool b)
		{/*
			if (m_Target == null) return false;
			//m_ScriptCode = null;
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
			*/
			return true;
		}
		public void GetControlList()
		{
			/*
			string n = "";
			int oidx = cmbTarget.SelectedIndex;
			if (oidx>=2)
			{
				n = cmbTarget.Text;
			}
			cmbTarget.Items.Clear();
			cmbTarget.Items.Add("Global");
			if (this.MainForm == null) return;
			cmbTarget.Items.Add(this.MainForm.Name);
			if(this.MainForm.Items.Count>0)
			{
				cmbTarget.Items.AddRange(this.MainForm.Items.Names);
			}
			if (oidx < 2) 
			{
				cmbTarget.SelectedIndex = oidx;
			}
			else if(n!="")
			{
				int idx = this.MainForm.Items.IndexOf(n);
				if(idx>=0)
				{
					cmbTarget.SelectedIndex = idx+2;
				}
			}
			else
			{
				cmbTarget.SelectedIndex = 0;
			}*/
		}
		public ScriptEditor()
		{
			InitializeComponent();
			btnEditStart.Click += (sender, e) => { SetEdit(true); };
			btnEditEnd.Click += (sender, e) => { SetEdit(false); };
			ChkSize();
		}
		private void ChkSize()
		{
			int h = m_BarHeight + 2;
			if (toolStrip1 != null)
			{
				toolStrip1.Location = new Point(5, h);
				toolStrip1.Size = new Size(this.Width - 10, 25);
			}
			h += 25 + 2;
			if (roslynEdit1 != null)
			{
				roslynEdit1.Location = new Point(5, h);
				roslynEdit1.Size = new Size(this.Width - 10, this.Height - h - 20);
			}
		}
		protected override void OnResize(EventArgs e)
		{
			ChkSize();
			base.OnResize(e);
			Refresh();
		}
	}
}
