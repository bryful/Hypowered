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
				if (m_MainForm.Script.app != null)
				{
					cmbWord.Items.Clear();
					cmbWord.Items.AddRange(m_MainForm.Script.app.members());
				}
				m_MainForm.FormChanged -= (sender, e) =>{};
				m_MainForm.FormChanged += (sender, e) =>
				{
					controlBrowser1.SetMainForm(fm);

				};
				m_MainForm.ControlChanged -= (sender, e) => { };
				m_MainForm.ControlChanged += (sender, e) =>
				{
					controlBrowser1.SetMainForm(fm);
				};
			}

		}
		public Font InputFont
		{
			get { return editPad1.Font; }
			set { editPad1.Font = value; }
		}
		private List<string> m_back = new List<string>();
		private int m_backCount = -1;
		public override void OnCloseButtunClick(EventArgs e)
		{
			base.OnCloseButtunClick(e);
			this.Visible=false;
		}
		public JSInputForm()
		{
			InitializeComponent();

			btnAlert.Click += (sender, e) => { editPad1.SetText("alert("); };
			btnWriteln.Click += (sender, e) => { editPad1.SetText("writeln("); };
			btnCLS.Click += (sender, e) => { editPad1.Text=""; };
			cmbWord.SelectedIndexChanged += (sender, e) =>
			{
				ComboBox? cmb = (ComboBox?)sender;
				if (cmb == null) return;
				if(cmb.SelectedIndex>=0)
				{
					if (cmb.SelectedItem != null)
					{
						editPad1.SetText(cmb.SelectedItem.ToString());
						editPad1.Focus();
					}
				}
			};
		}
		protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
		{
			if(e.KeyData == (Keys.Control|Keys.E)) 
			{
				exec();
			}else if (e.KeyData == (Keys.Control | Keys.Z))
			{
				Undo();
			
			}else if (e.KeyData == (Keys.Control | Keys.W))
			{
				this.Visible = false;
			}
			else
			{
				base.OnPreviewKeyDown(e);
			}
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
		public void exec()
		{
			if (MainForm != null)
			{
				string str = editPad1.Text.Trim();
				AddBack(str);
				MainForm.Script.ExecuteCode(str);
				editPad1.Text = "";
				editPad1.Focus();
			}
		}
		private void Button1_Click(object sender, EventArgs e)
		{
			exec();
		}
		public void Undo()
		{
			if ((m_backCount >= 0)&&(m_backCount<m_back.Count))
			{
				editPad1.Text = m_back[m_backCount];
				m_backCount--;
				if (m_backCount < 0) m_backCount = 0;
				editPad1.Focus();

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


		private void BtnFont_Click(object sender, EventArgs e)
		{
			using (FontDialog dlg = new FontDialog())
			{
				dlg.Font = InputFont;
				if(dlg.ShowDialog()==DialogResult.OK)
				{
					InputFont=dlg.Font;
					if(MainForm!=null) MainForm.Font = dlg.Font;
					editPad1.Focus();

				}
			}
		}
	}
}
