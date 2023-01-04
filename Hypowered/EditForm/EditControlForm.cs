using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public enum DROption
	{
		Cancel,
		OK,
		Font,
		Script,
		Icon,
		Content,
		Connect,
		FileOpen
	}
	public partial class EditControlForm : EditForm
	{

		private HyperMainForm? MainForm = null;

		private bool IsNewMode = false;
		static private ControlType m_ct = ControlType.Button;
		[Category("Hypowered")]
		public ControlType ControlType
		{
			get { return editControlComb1.ControlType; }
			set { editControlComb1.ControlType = value;}
		}
		[Category("Hypowered")]
		public string ControlName
		{
			get { return tbName.Text; }
			set { tbName.Text = value; }
		}
		[Category("Hypowered")]
		public string ControlText
		{
			get { return tbText.Text; }
			set { tbText.Text = value; }
		}
		private void ChkEnabled()
		{
			ControlType ct = editControlComb1.ControlType;
			btnFont.Enabled= false;
			btnScript.Enabled = false;
			btnIcon.Enabled = false;
			btnContent.Enabled = false;
			btnConnect.Enabled = false;
			btnOpenFile.Enabled = false;
			switch (ct)
			{
				case ControlType.Label:
				case ControlType.TextBox:
					btnContent.Enabled = true;
					btnFont.Enabled = true;
					break;
				case ControlType.Button:
				case ControlType.CheckBox:
				case ControlType.RadioButton:
				case ControlType.ListBox:
				case ControlType.DropdownList:
					btnContent.Enabled = true;
					btnScript.Enabled = true;
					btnFont.Enabled = true;
					break;
				case ControlType.DriveIcons:
				case ControlType.DirList:
				case ControlType.FileList:
					btnContent.Enabled = true;
					btnContent.Enabled = true;
					btnScript.Enabled = true;
					btnFont.Enabled = true;
					break;
				case ControlType.PictureBox:
					btnOpenFile.Enabled = true;
					break;
				case ControlType.Icon:
					btnIcon.Enabled = true;
					break;

			}
		}
		public void SetMainForm(HyperMainForm? mf,HyperBaseForm bf,HyperControl? c=null)
		{
			MainForm = mf;
			if ((MainForm == null)||(bf==null)) return;
			m_TargetForm = bf;
			m_TargetControl = c;
			IsNewMode = (c == null);
			if (IsNewMode == false)
			{
				if ((m_TargetControl != null) && ((m_TargetControl.MyType != null)))
				{
					editControlComb1.ControlType = (ControlType)m_TargetControl.MyType;
					tbDes.Text = ControlTypeInfos.Disp(editControlComb1.ControlType);
					editControlComb1.Enabled = false;
					tbName.Text = m_TargetControl.Name;
					tbText.Text = m_TargetControl.Text;
					m_OrgName = m_TargetControl.Text;
					ChkEnabled();
				}
				else
				{
					return;
				}
			}
			else
			{
				btnFont.Enabled = !IsNewMode;
				btnIcon.Enabled = !IsNewMode;
				btnScript.Enabled = !IsNewMode;
				btnContent.Enabled = !IsNewMode;
				btnConnect.Enabled = !IsNewMode;
				btnOpenFile.Enabled = !IsNewMode;
			}

			btnCancel.Enabled = true;
			btnOK.Enabled = false;
		}
		private HyperBaseForm? m_TargetForm = null;
		private HyperControl? m_TargetControl = null;

		public DROption DROption = DROption.Cancel;
		public string m_OrgName = "";
		public EditControlForm()
		{
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			InitializeComponent();
			editControlComb1.ControlType= m_ct;
			NameSet();
			tbName.TextChanged += TbName_TextChanged;
			tbText.TextChanged += TbName_TextChanged;
			btnOK.Click += BtnOK_Click;
			this.StartPosition= FormStartPosition.CenterParent;
			editControlComb1.ControlTypeChanged += EditControlComb1_ControlTypeChanged;
		}
		public override void OnButtunClick(EventArgs e)
		{
			base.OnButtunClick(e);
			DialogResult= DialogResult.Cancel;
		}
		private void EditControlComb1_ControlTypeChanged(object sender, ControlTypeEventArgs e)
		{
			NameSet();
			tbDes.Text = ControlTypeInfos.Disp(e.Value);
		}
		private void NameSet()
		{
			string s = Enum.GetName(typeof(ControlType), editControlComb1.ControlType); ;
			s = s.Substring(0,1).ToLower() + s.Substring(1);
			tbName.Text = s;
		}
		private void BtnOK_Click(object? sender, EventArgs e)
		{
			string nm = tbName.Text.Trim();
			if(nm=="")
			{
				tbName.Focus();
				return;
			}
			if(MainForm!=null)
			{
				bool IsN = MainForm.IsNameChk(nm);
				if(IsN ==true)
				{
					if ((m_TargetControl!=null)&&(IsNewMode == false) )
					{
						this.DialogResult = DialogResult.OK;
						DROption = DROption.OK;
					}
					else
					{
						lbInfo.Text = "既に同じ名前があります";
						tbName.Focus();
						return;
					}
				}
				else
				{
					this.DialogResult = DialogResult.OK;
					DROption = DROption.OK;
				}
			}
			else
			{
				this.DialogResult = DialogResult.Cancel;
				DROption = DROption.Cancel;
			}
		}

		private void TbName_TextChanged(object? sender, EventArgs e)
		{
			btnOK.Enabled = (tbName.Text != "");
		}

		#region Mouse
		protected MDPos m_MDPos = MDPos.None;
		protected Point m_MDP = new Point(0, 0);
		protected Point m_MDLoc = new Point(0, 0);
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{

				MDPos p = CU.GetMDPos(e.X, e.Y, this.Size);
				if (p != MDPos.None)
				{
					m_MDPos = p;
					m_MDP = new Point(e.X, e.Y);
					m_MDLoc = this.Location;
					return;
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (m_MDPos != MDPos.None)
			{
				int ax = e.X - m_MDP.X;
				int ay = e.Y - m_MDP.Y;
				if(m_MDPos!= MDPos.None)
				{
					this.Location = new Point(
						this.Location.X + ax,
						this.Location.Y + ay);
				}
				return;
			}
			base.OnMouseMove(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_MDPos != MDPos.None)
			{
				m_MDPos = MDPos.None;
			}
			base.OnMouseUp(e);
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if(e.KeyData== Keys.Escape)
			{
				this.DialogResult= DialogResult.Cancel;
			}
			base.OnKeyDown(e);
		}
		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			using(Pen p = new Pen(ForeColor,1))
			{
				Graphics g = e.Graphics;
				g.DrawRectangle(p,new Rectangle(0,0,Width-1,Height-1));
			}
		}

		private void btnFont_Click(object sender, EventArgs e)
		{
			if (IsNewMode) return;
			DROption = DROption.Font;
			DialogResult= DialogResult.OK;
		}

		private void btnScript_Click(object sender, EventArgs e)
		{
			if (IsNewMode) return;
			DROption = DROption.Script;
			DialogResult = DialogResult.OK;
		}

		private void btnIcon_Click(object sender, EventArgs e)
		{
			if (IsNewMode) return;
			DROption = DROption.Icon;
			DialogResult = DialogResult.OK;
		}

		private void btnContent_Click(object sender, EventArgs e)
		{
			if (IsNewMode) return;
			DROption = DROption.Content;
			DialogResult = DialogResult.OK;
		}

		private void btnOpenFile_Click(object sender, EventArgs e)
		{
			if (IsNewMode) return;
			DROption = DROption.FileOpen;
			DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DROption = DROption.Cancel;
			DialogResult = DialogResult.Cancel;
		}
	}
}
