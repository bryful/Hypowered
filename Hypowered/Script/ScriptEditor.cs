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
	public partial class ScriptEditor : Control
	{
		private int m_SelectedIndexBak = -1;
		public MainForm? MainForm= null;
		public object? m_Target = null;
		public object? Target
		{
			get { return m_Target; }
			set { SetTarget(value); }
		}
		private HFType m_HFType = HFType.None;
		private HCType m_HCType = HCType.None;

		public RoslynEdit RoslynEdit { get; } =    new RoslynEdit();
		public CheckBox cbGlobal { get; } = new CheckBox();
		public Button btnExecute { get; } = new Button();
		public Button btnEditSave { get; } = new Button();
		public Button btnFont { get; } = new Button();
		public ComboBox cmbEvent { get; } = new ComboBox();

		public Font EditorFont
		{
			get { return RoslynEdit.Font; }
			set { RoslynEdit.Font = value; }
		}

		// ***************************************************************
		//private string[] m_Codes = new string[0];

		// ***************************************************************
		private void SetCombCodes(HScriptCode sc)
		{
			cmbEvent.Items.Clear();
			//m_Codes = new string[0];
			cmbEvent.Items.AddRange(sc.HScriptTypeNames);
			if(cmbEvent.Items.Count > 0 )
			{
				//m_Codes = sc.Codes;
				m_SelectedIndexBak = -1;
				cmbEvent.SelectedIndex = 0;
			}
		}
		// ***************************************************************
		private void SetCombCodes(HMenuItem mi)
		{
			cmbEvent.Items.Clear();
			//m_Codes = new string[1];
			cmbEvent.Items.Add("Menu");
			//m_Codes[0] = mi.ScriptItem.Code;
			cmbEvent.SelectedIndex = 0;
			m_SelectedIndexBak = 0;
			RoslynEdit.Text = mi.ScriptItem.Code;
		}
		// ***************************************************************
		private void BackScript()
		{
			if (m_ScriptMode == true) return; 
			if (m_Target == null) return;
			int idx = cmbEvent.SelectedIndex;
			if (idx < 0) return;
			if (m_Target is HControl)
			{
				HControl hc = (HControl)m_Target;
				if ((idx >= 0) && (idx < hc.ScriptCode.Length))
				{
					hc.ScriptCode.ScriptItems[idx].SetCode(RoslynEdit.Text);
				}
			}
			else if (m_Target is HForm)
			{
				HForm hf = (HForm)m_Target;
				if ((idx >= 0) && (idx < hf.ScriptCode.Length))
				{
					hf.ScriptCode.ScriptItems[idx].SetCode(RoslynEdit.Text);
				}
			}
			else if (m_Target is HMenuItem)
			{
				HMenuItem mi = (HMenuItem)m_Target;
				mi.ScriptItem.SetCode(RoslynEdit.Text);
			}
		}
		// ***************************************************************
		public void SetTarget(object? tar)
		{
			if(m_ScriptMode==true) return;
			if (GlobalMode) return;
			BackScript();
			m_Target = tar;
			if (m_Target is HControl)
			{
				HControl hc = (HControl)m_Target;
				m_HFType = HFType.HControl;
				m_HCType = hc.HCType;
				SetCombCodes(hc.ScriptCode);
			}
			else if (m_Target is HForm)
			{
				m_HFType = HFType.HForm;
				m_HCType = HCType.None;
				SetCombCodes(((HForm)m_Target).ScriptCode);
			}
			else if (m_Target is HMenuItem)
			{
				m_HFType = HFType.HMenuItem;
				m_HCType = HCType.None;
				SetCombCodes(((HMenuItem)m_Target));
			}
			else
			{
				m_HFType = HFType.None;
				m_HCType = HCType.None;
				m_Target = null;
				RoslynEdit.Text = "";
			}
			btnEditSave.Enabled = (m_Target !=null);
		}
		// ***************************************************************
		private bool m_ScriptMode = false;
		public bool ScriptMode
		{
			get
			{ 
				if(m_Target==null) m_ScriptMode = false;

				return m_ScriptMode; 
			}
			set 
			{
				if (m_Target != null)
				{
					m_ScriptMode = value;
					btnEditSave.Enabled = m_ScriptMode;
					cmbEvent.Enabled = m_ScriptMode;
					if(m_ScriptMode==false)
					{
						BackScript();
					}

				}
				else
				{
					m_ScriptMode = false;
					btnEditSave.Enabled = false;
					cmbEvent.Enabled = false;
				}
			}
		}
		public bool GlobalMode
		{
			get { return cbGlobal.Checked; }
			set
			{
				cbGlobal.Checked = value;
				btnEditSave.Visible = !cbGlobal.Checked;
				cmbEvent.Visible = !cbGlobal.Checked;
			}
		}
		public ScriptEditor()
		{
			InitializeComponent();
			cbGlobal.Name = "cbGlobal";
			cbGlobal.Text = "Global";

			btnEditSave.Name = "btnEditSave";
			btnEditSave.Text = "Save";
			btnEditSave.FlatStyle = FlatStyle.Flat;

			btnFont.Name = "btnFont";
			btnFont.Text = "Font";
			btnFont.FlatStyle = FlatStyle.Flat;


			btnExecute.Name = "btnExecute";
			btnExecute.Text = "Execute";
			btnExecute.FlatStyle = FlatStyle.Flat;
			cmbEvent.DropDownStyle = ComboBoxStyle.DropDownList;

			ChkLayout();
			this.Controls.Add(cbGlobal);
			this.Controls.Add(btnFont);
			this.Controls.Add(cmbEvent);
			this.Controls.Add(btnEditSave);
			this.Controls.Add(btnExecute);
			this.Controls.Add(RoslynEdit);
			Target = null;

			cbGlobal.CheckedChanged += (sender, e) =>
			{
				GlobalMode = cbGlobal.Checked;
			};
			btnExecute.Click += (sender, e) =>
			{
				ExecuteScript();
			};
			btnFont.Click += (sender, e) =>
			{
				ShowFontDialog();
			};
			cmbEvent.SelectedIndexChanged += (sender, e) =>
			{
				int idx = cmbEvent.SelectedIndex;
				if (m_Target == null) return;
				if (m_Target is HMenuItem)
				{

					HMenuItem mi = ((HMenuItem)m_Target);
					mi.ScriptItem.SetCode(RoslynEdit.Text);
				}else if (m_Target is HForm) 
				{
					HForm hf = ((HForm)m_Target);
					if((m_SelectedIndexBak>=0) &&(m_SelectedIndexBak < hf.ScriptCode.Length))
					{
						hf.ScriptCode.ScriptItems[m_SelectedIndexBak].SetCode(RoslynEdit.Text);
					}
					if((idx>=0)&&(idx< hf.ScriptCode.Length))
					{
						RoslynEdit.Text = hf.ScriptCode.ScriptItems[idx].Code;
					}
				}
				else if (m_Target is HControl)
				{
					HControl hc = ((HControl)m_Target);
					if ((m_SelectedIndexBak >= 0) && (m_SelectedIndexBak < hc.ScriptCode.Length))
					{
						hc.ScriptCode.ScriptItems[m_SelectedIndexBak].SetCode(RoslynEdit.Text);
					}
					if ((idx >= 0) && (idx < hc.ScriptCode.Length))
					{
						RoslynEdit.Text = hc.ScriptCode.ScriptItems[idx].Code;
					}
				}
				m_SelectedIndexBak = cmbEvent.SelectedIndex;
			};
		}
		private void ExecuteScript()
		{
			if (RoslynEdit.Text == "") return;
			if(MainForm !=null)
			{
				MainForm.ExecScript(RoslynEdit.Text);
			}
		}
		private void ChkLayout()
		{
			int x = 0;
			cbGlobal.Size = new Size(60, 23);
			cbGlobal.Location = new Point(x, 0);
			x += cbGlobal.Width + 2;

			btnFont.Size = new Size(50, 23);
			btnFont.Location = new Point(x, 0);
			x += btnFont.Width + 6;


			cmbEvent.Size = new Size(100, 23);
			cmbEvent.Location = new Point(x, 0);
			x += cmbEvent.Width + 2;

			btnEditSave.Size = new Size(50, 23);
			btnEditSave.Location = new Point(x, 0);
			x += btnEditSave.Width + 6;



			btnExecute.Size = new Size(70, 23);
			btnExecute.Location = new Point(this.Width-75, 0);
			
			RoslynEdit.Size = new Size(this.Width, this.Height - 25);
			RoslynEdit.Location = new Point(0, 25);
		}
		protected override void OnResize(EventArgs e)
		{
			ChkLayout();
			base.OnResize(e);
		}
		private void ShowFontDialog()
		{
			using(FontDialog dlg = new FontDialog())
			{
				dlg.Font = EditorFont;
				if(dlg.ShowDialog() == DialogResult.OK)
				{
					EditorFont = dlg.Font;
				}
			}
		}
	}
	
}
