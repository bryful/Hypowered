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
	public partial class EditControlForm : Form
	{
		

		private bool IsNewMode = false;
		static private ControlType m_ct = ControlType.Button;
		[Category("Hypowerd")]
		public ControlType ControlType
		{
			get { return editControlComb1.ControlType; }
			set { editControlComb1.ControlType = value;}
		}
		[Category("Hypowerd")]
		public string ControlName
		{
			get { return tbName.Text; }
			set { tbName.Text = value; }
		}
		[Category("Hypowerd")]
		public string ControlText
		{
			get { return tbText.Text; }
			set { tbText.Text = value; }
		}
		private string m_ScriptCode = "";
		[Category("Hypowerd")]
		public string ScriptCode
		{
			get { return m_ScriptCode; }
			set { m_ScriptCode = value; }
		}
		private Font m_Font = new Font("System",9);
		public new Font Font
		{
			get { return m_Font;}
			set { m_Font = value;}
		}
		private HyperForm? Form = null;
		private HyperControl? TargetControl = null; 
		public EditControlForm()
		{
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			m_Font = this.Font;
			InitializeComponent();
			editControlComb1.ControlType= m_ct;
			NameSet();
			toolStrip1.MouseDown += ToolStrip1_MouseDown;
			toolStrip1.MouseMove += ToolStrip1_MouseMove;
			toolStrip1.MouseUp += ToolStrip1_MouseUp;

			tbName.TextChanged += TbName_TextChanged;
			tbText.TextChanged += TbName_TextChanged;
			btnOK.Click += BtnOK_Click;
			this.StartPosition= FormStartPosition.CenterParent;
			editControlComb1.ControlTypeChanged += EditControlComb1_ControlTypeChanged;
		}

		private void EditControlComb1_ControlTypeChanged(object sender, ControlTypeEventArgs e)
		{
			NameSet();
		}
		private void NameSet()
		{
			tbName.Text = Enum.GetName(typeof(ControlType), editControlComb1.ControlType);
		}
		private void BtnOK_Click(object? sender, EventArgs e)
		{
			string nm = tbName.Text.Trim();
			if(nm=="")
			{
				tbName.Focus();
				return;
			}
			if(Form!=null)
			{
				int idx = Form.FindControlIndex(nm);
				if(idx>=0)
				{
					if ((TargetControl!=null)&&(IsNewMode == false) && (idx == TargetControl.Index))
					{
						this.DialogResult = DialogResult.OK;
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
				}
			}
			else
			{
				this.DialogResult = DialogResult.Cancel;
			}
		}

		private void TbName_TextChanged(object? sender, EventArgs e)
		{
			btnOK.Enabled = (tbName.Text != "");
		}

		public void SetTarget(HyperForm? fm,HyperControl? cnt=null)
		{
			Form= fm;
			IsNewMode = (cnt == null);
			if (IsNewMode==false)
			{
				TargetControl = cnt;
				if ((TargetControl != null)&&(TargetControl.MyType!=null))
				{
					editControlComb1.ControlType = (ControlType)TargetControl.MyType;
					editControlComb1.Enabled= false;
					m_Font = TargetControl.Font;
					tbName.Text = TargetControl.Name;
					tbText.Text = TargetControl.Text;

				}
			}
		}
		private void ToolStrip1_MouseUp(object? sender, MouseEventArgs e)
		{
			this.OnMouseUp(e);
		}

		private void ToolStrip1_MouseMove(object? sender, MouseEventArgs e)
		{
			this.OnMouseMove(e);
		}

		private void ToolStrip1_MouseDown(object? sender, MouseEventArgs e)
		{
			this.OnMouseDown(e);
		}
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

		private void BtnFont_Click(object sender, EventArgs e)
		{
			Font? f = FntU.Dialog(this, this.Font);
			if(f!=null) this.Font = f;
		}
	}
}
