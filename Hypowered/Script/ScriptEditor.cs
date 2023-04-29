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
		private object? m_TargetBak = null;
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
		public CheckBox cbSaved { get; } = new CheckBox();
		public Button btnExecute { get; } = new Button();
		public Button btnFont { get; } = new Button();
		public ComboBox cmbEvent { get; } = new ComboBox();
		public TextBox tbName { get; } = new TextBox();

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
			cmbEvent.Items.AddRange(sc.HScriptTypeNames);
			if(cmbEvent.Items.Count > 0 )
			{
				m_SelectedIndexBak = -1;
				cmbEvent.SelectedIndex = 0;
				RoslynEdit.Text = sc.ScriptItems[0].Code;
			}
		}
		// ***************************************************************
		private void SetCombCodes(HMenuItem mi)
		{
			cmbEvent.Items.Clear();
			cmbEvent.Items.Add("Menu");
			cmbEvent.SelectedIndex = 0;
			m_SelectedIndexBak = 0;
			RoslynEdit.Text = mi.ScriptItem.Code;
		}
		// ***************************************************************
		private void BackScript()
		{
			if (m_Target == null) return;
			if (SavedMode==false) return;
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
			if(SavedMode) BackScript();
			m_Target = tar;
			if (m_Target is HControl)
			{
				HControl hc = (HControl)m_Target;
				m_HFType = HFType.HControl;
				m_HCType = hc.HCType;
				SetCombCodes(hc.ScriptCode);
				tbName.Text = hc.Name;
			}
			else if (m_Target is HForm)
			{
				HForm hf = (HForm)m_Target;
				m_HFType = HFType.HForm;
				m_HCType = HCType.None;
				SetCombCodes(hf.ScriptCode);
				tbName.Text = hf.Name;
			}
			else if (m_Target is HMenuItem)
			{
				HMenuItem mi = (HMenuItem)m_Target;
				m_HFType = HFType.HMenuItem;
				m_HCType = HCType.None;
				SetCombCodes(mi);
				tbName.Text = mi.Name;
			}
			else
			{
				m_HFType = HFType.None;
				m_HCType = HCType.None;
				m_Target = null;
				cmbEvent.Items.Clear();
				RoslynEdit.Text = "";
				tbName.Text = "";
			}
		}
		// ***************************************************************
		public bool SavedMode
		{
			get { return cbSaved.Checked; }
			set
			{
				if (cbSaved.Checked != value)
				{
					cbSaved.Checked = value;

				}
			}
		}
		public ScriptEditor()
		{
			InitializeComponent();

			tbName.Name = "tbName";
			tbName.Text = "";
			tbName.ReadOnly = true;

			cbSaved.Name = "cbSaved";
			cbSaved.Text = "Saved";


			btnFont.Name = "btnFont";
			btnFont.Text = "Font";
			btnFont.FlatStyle = FlatStyle.Flat;


			btnExecute.Name = "btnExecute";
			btnExecute.Text = "Execute";
			btnExecute.FlatStyle = FlatStyle.Flat;
			cmbEvent.DropDownStyle = ComboBoxStyle.DropDownList;

			ChkLayout();
			this.Controls.Add(tbName);
			this.Controls.Add(cbSaved);
			this.Controls.Add(btnFont);
			this.Controls.Add(cmbEvent);
			this.Controls.Add(btnExecute);
			this.Controls.Add(RoslynEdit);
			Target = null;
			SavedMode = false;

			cbSaved.CheckedChanged += (sender, e) =>
			{
				SavedMode = cbSaved.Checked;
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
				if ((SavedMode) && (m_Target is HMenuItem))
				{
					//HMenuItem mi = ((HMenuItem)m_Target);
					//mi.ScriptItem.SetCode(RoslynEdit.Text);

				}else if (m_Target is HForm) 
				{
					HForm hf = ((HForm)m_Target);
					if((SavedMode)&&(m_SelectedIndexBak>=0) &&(m_SelectedIndexBak < hf.ScriptCode.Length))
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
					if ((SavedMode) && (m_SelectedIndexBak >= 0) && (m_SelectedIndexBak < hc.ScriptCode.Length))
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
			tbName.Size = new Size(100, 23);
			tbName.Location = new Point(x, 0);
			x += tbName.Width + 2;

			cmbEvent.Size = new Size(100, 23);
			cmbEvent.Location = new Point(x, 0);
			x += cmbEvent.Width + 2;

			cbSaved.Size = new Size(60, 23);
			cbSaved.Location = new Point(x, 0);
			x += cbSaved.Width + 2;

			btnFont.Size = new Size(50, 23);
			btnFont.Location = new Point(x, 0);
			x += btnFont.Width + 6;




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
