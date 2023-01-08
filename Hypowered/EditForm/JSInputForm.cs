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
	public partial class JSInputForm : EditForm
	{
		private HyperMainForm? m_MainForm = null;
		public HyperMainForm? MainForm
		{
			get { return m_MainForm; }
			set 
			{
				m_MainForm = value;
				controlBrowser1.SetMainForm(value);
			}
		}
		public void SetMainForm(HyperMainForm? fm)
		{
			m_MainForm = fm;
			controlBrowser1.SetMainForm(fm);
			if (m_MainForm != null)
			{
				m_MainForm.FormChanged += (sender, e) =>
				{
					controlBrowser1.SetMainForm(fm);
				};
				m_MainForm.ControlChanged += (sender, e) =>
				{
					controlBrowser1.SetMainForm(fm);
				};
			}
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
		private List<string> m_back = new List<string>();
		private int m_backCount = -1;
		public override void OnButtunClick(EventArgs e)
		{
			base.OnButtunClick(e);
			this.Visible=false;
		}
		public JSInputForm()
		{
			InitializeComponent();
		}
		private void AddBack(string s)
		{
			s = s.Trim();
			if (s == "") return;
			if (m_back.Count <= 0)
			{
				m_back.Add(s);
			}
			else
			{
				if (m_back[m_back.Count - 1] != s) 
				{
					m_back.Add(s);
				}
			}
			if (m_back.Count > 100) m_back.RemoveAt(0);
			m_backCount = m_back.Count-1;
		}
		private void Button1_Click(object sender, EventArgs e)
		{
			if(MainForm != null)
			{
				string str = editPad1.Text.Trim();
				AddBack(str);
				MainForm.Script.ExecuteCode(str);
				editPad1.Text = "";
			}
		}
		public void Undo()
		{
			if ((m_backCount >= 0)&&(m_backCount<m_back.Count))
			{
				editPad1.Text = m_back[m_backCount];
				m_backCount--;
				if (m_backCount < 0) m_backCount = 0;
			}
		}
		public void Redo()
		{
			if ((m_backCount >= 0) && (m_backCount < m_back.Count))
			{
				editPad1.Text = m_back[m_backCount];
				m_backCount++;
				if (m_backCount >= m_back.Count) m_backCount = m_back.Count - 1;
			}
		}

		private void BtnUndo_Click(object sender, EventArgs e)
		{
			Undo();
		}

		private void BtnRedo_Click(object sender, EventArgs e)
		{
			Redo();
		}
	}
}
