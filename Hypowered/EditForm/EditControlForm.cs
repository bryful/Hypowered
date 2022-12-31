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
	public partial class EditControlForm : Form
	{

		private HyperMainForm? MainForm = null;

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
		public void SetMainForm(HyperMainForm? mf,HyperBaseForm bf,HyperControl? c=null)
		{
			this.MainForm = mf;
			if(bf != null)
			{
				m_TargetForm = bf;
				m_TargetControl = c;
				IsNewMode = (c == null);
				if (IsNewMode == false)
				{
					if ((m_TargetControl != null) && (m_TargetControl.MyType != null))
					{
						editControlComb1.ControlType = (ControlType)m_TargetControl.MyType;
						editControlComb1.Enabled = false;
						tbName.Text = m_TargetControl.Name;
						tbText.Text = m_TargetControl.Text;
					}
				}
			}
		}
		private HyperBaseForm? m_TargetForm = null;
		private HyperControl? m_TargetControl = null;
				
		public EditControlForm()
		{
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			InitializeComponent();
			editControlComb1.ControlType= m_ct;
			NameSet();
			toolStrip.MouseDown += ToolStrip1_MouseDown;
			toolStrip.MouseMove += ToolStrip1_MouseMove;
			toolStrip.MouseUp += ToolStrip1_MouseUp;

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
			if(m_TargetForm!=null)
			{
				int idx = m_TargetForm.FindControlIndex(nm);
				if(idx>=0)
				{
					if ((m_TargetControl!=null)&&(IsNewMode == false) && (idx == m_TargetControl.Index))
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

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			using(Pen p = new Pen(ForeColor,1))
			{
				Graphics g = e.Graphics;
				g.DrawRectangle(p,new Rectangle(0,0,Width-1,Height-1));
			}
		}

		private void btnOK_Click_1(object sender, EventArgs e)
		{

		}
	}
}
