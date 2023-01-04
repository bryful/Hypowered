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
	public partial class EditForm : Form
	{
		public event EventHandler? ButtunClick;
		public virtual void OnButtunClick(EventArgs e)
		{
			if (ButtunClick != null)
			{
				ButtunClick(this, e);
			}
		}
		protected bool m_CanResize = false;
		public bool CanResize
		{
			get { return m_CanResize; }
			set { m_CanResize = value; }
		}
		private HyperBaseForm? m_TargetForm = null;
		private HyperControl? m_TargetControl = null;
		private EditMenuBar m_MenuBar = new EditMenuBar();
		public new string Text
		{
			get { return base.Text; }
			set 
			{
				base.Text = value;
			}
		}
		public EditForm()
		{
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			this.StartPosition = FormStartPosition.CenterParent;
			this.FormBorderStyle= FormBorderStyle.None;
			InitializeComponent();
			this.SetStyle(
		//ControlStyles.Selectable |
		//ControlStyles.UserMouse |
		ControlStyles.DoubleBuffer |
		ControlStyles.UserPaint |
		ControlStyles.AllPaintingInWmPaint,
		//ControlStyles.SupportsTransparentBackColor,
		true);
			this.UpdateStyles(); Controls.Add(this.m_MenuBar);
			Controls.SetChildIndex(this.m_MenuBar, 0);
			m_MenuBar.SetParent();
			m_MenuBar.ButtunClick += M_MenuBar_ButtunClick;
		}

		private void M_MenuBar_ButtunClick(object? sender, EventArgs e)
		{
			OnButtunClick(e);
		}
		#region Mouse
		protected MDPos m_MDPos = MDPos.None;
		protected Point m_MDP = new Point(0, 0);
		protected Point m_MDLoc = new Point(0, 0);
		protected Size m_MDSize = new Size(0, 0);

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{

				MDPos p = CU.GetMDPos(e.X, e.Y, this.Size);
				if ((m_CanResize)&&(p== MDPos.BottomRight))
				{
					m_MDPos = p;
					m_MDP = new Point(e.X, e.Y);
					m_MDLoc = this.Location;
					m_MDSize = this.Size;

				}
				else if (p != MDPos.None)
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
			if((m_MDPos == MDPos.BottomRight)&&(m_CanResize))
			{
				int ax = e.X - m_MDP.X;
				int ay = e.Y - m_MDP.Y;
				this.Size = new Size(
				m_MDSize.Width + ax,
				m_MDSize.Height + ay);
				return;

			}
			else if (m_MDPos != MDPos.None)
			{
				int ax = e.X - m_MDP.X;
				int ay = e.Y - m_MDP.Y;
				if (m_MDPos != MDPos.None)
				{
					this.Location = new Point(
						this.Location.X + ax,
						this.Location.Y + ay);
				}
				return;
			}
			base.OnMouseMove(e);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.Refresh();
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
			if (e.KeyData == Keys.Escape)
			{
				this.DialogResult = DialogResult.Cancel;
			}
			base.OnKeyDown(e);
		}
		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			using(SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = e.Graphics;
				g.FillRectangle(sb, this.ClientRectangle);
				g.DrawRectangle(p, new Rectangle(0,0,this.Width-1,this.Height-1));
			}
		}
	}
}
