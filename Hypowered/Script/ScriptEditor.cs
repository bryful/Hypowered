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
		public object? m_Target = null;
		public object? Target
		{
			get { return m_Target; }
			set { SetTarget(value); }
		}
		private HFType m_HFType = HFType.None;
		private HCType m_HCType = HCType.None;

		public RoslynEdit RoslynEdit { get; } =    new RoslynEdit();
		public Button btnEditStart { get; } = new Button();
		public Button btnEditSave { get; } = new Button();
		public Button btnEditCancel { get; } = new Button();
		public Button btnExecute { get; } = new Button();
		public ComboBox cmbEvent { get; } = new ComboBox();
		private string[] m_Codes = new string[0];

		// ***************************************************************
		private void SetComb(HScriptCode sc)
		{
			cmbEvent.Items.Clear();
			m_Codes = new string[0];
			cmbEvent.Items.AddRange(sc.HScriptTypeNames);
			if(cmbEvent.Items.Count > 0 )
			{
				m_Codes = sc.Codes;
				cmbEvent.SelectedIndex = 0;
			}
		}
		// ***************************************************************
		private void SetComb(HMenuItem mi)
		{
			cmbEvent.Items.Clear();
			m_Codes = new string[1];
			cmbEvent.Items.Add("Menu");
			m_Codes[0] = mi.ScriptItem.Code;
			cmbEvent.SelectedIndex = 0;
		}
		// ***************************************************************
		public void SetTarget(object? tar)
		{
			if(m_EditMode==true) return;
			m_Target = tar;
			if (m_Target is HControl)
			{
				m_HFType = HFType.HControl;
				m_HCType = ((HControl)m_Target).HCType;
				SetComb(((HControl)m_Target).ScriptCode);

			}
			else if (m_Target is HForm)
			{
				m_HFType = HFType.HForm;
				m_HCType = HCType.None;
				SetComb(((HForm)m_Target).ScriptCode);
			}
			else if (m_Target is HMenuItem)
			{
				m_HFType = HFType.HMenuItem;
				m_HCType = HCType.None;
				SetComb(((HMenuItem)m_Target));
			}
			else
			{
				m_HFType = HFType.None;
				m_HCType = HCType.None;
				m_Target = null;
			}
			btnEditStart.Enabled = (m_HFType!=HFType.None);
			cmbEvent.Enabled = btnEditStart.Enabled;
			btnEditSave.Enabled = false;
			btnEditCancel.Enabled = false;
		}
		// ***************************************************************
		private bool m_EditMode = false;
		public bool EditMode
		{
			get
			{ 
				if(m_Target==null) m_EditMode = false;

				return m_EditMode; 
			}
			set 
			{
				if (m_Target != null)
				{
					m_EditMode = value;
					btnEditStart.Enabled = !m_EditMode;
					btnEditSave.Enabled = m_EditMode;
					btnEditCancel.Enabled = m_EditMode;
					cmbEvent.Enabled = true;
				}
				else
				{
					m_EditMode = false;
					btnEditStart.Enabled = false;
					btnEditSave.Enabled = false;
					btnEditCancel.Enabled = false;
					cmbEvent.Enabled = false;
				}
			}
		}

		public ScriptEditor()
		{
			InitializeComponent();
			btnEditSave.Enabled = false;
			btnEditStart.Name = "btnEditStart";
			btnEditStart.Text = "Start";
			btnEditStart.FlatStyle = FlatStyle.Flat;

			btnEditSave.Name = "btnEditSave";
			btnEditSave.Text = "Save";
			btnEditSave.FlatStyle = FlatStyle.Flat;

			btnEditCancel.Name = "btnEditCancel";
			btnEditCancel.Text = "Cancel";
			btnEditCancel.FlatStyle = FlatStyle.Flat;


			btnExecute.Name = "btnExecute";
			btnExecute.Text = "Execute";
			btnExecute.FlatStyle = FlatStyle.Flat;
			cmbEvent.DropDownStyle = ComboBoxStyle.DropDownList;

			ChkLayout();
			this.Controls.Add(btnEditStart);
			this.Controls.Add(cmbEvent);
			this.Controls.Add(btnEditSave);
			this.Controls.Add(btnEditCancel);
			this.Controls.Add(btnExecute);
			this.Controls.Add(RoslynEdit);
			Target = null;
		}
		private void ChkLayout()
		{
			int x = 0;
			btnEditStart.Size = new Size(50, 23);
			btnEditStart.Location = new Point(x,0);
			x += btnEditStart.Width + 2;

			cmbEvent.Size = new Size(100, 23);
			cmbEvent.Location = new Point(x, 0);
			x += cmbEvent.Width + 2;


			btnEditSave.Size = new Size(50, 23);
			btnEditSave.Location = new Point(x, 0);
			x += btnEditSave.Width + 6;

			btnEditCancel.Size = new Size(70, 23);
			btnEditCancel.Location = new Point(x, 0);
			x += btnEditCancel.Width + 6;


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
	}
	
}
