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
	
	public enum FormAction
	{
		None,
		New,
		Open,
		Rename,
		Dup,
		Close

	}
	
	public partial class FormActionPanel : Control
	{
		public MainForm? MainForm = null;
		public void SetMainForm(MainForm? mf)
		{
			MainForm = mf;
		}
		// ******************************************************
		public class FormActionClickEventArgs : EventArgs
		{
			public FormAction Mode;
			public FormActionClickEventArgs(FormAction v)
			{
				Mode = v;
			}
		}
		public delegate void FormActionClickHandler(object sender, FormActionClickEventArgs e);
		protected event FormActionClickHandler? FormActionClick = null;
		protected virtual void OnFormActionClick(FormActionClickEventArgs e)
		{
			if (FormActionClick != null)
			{
				FormActionClick(this, e);
			}
		}
		// ******************************************************
		protected Bitmap[] FormActionIcon = new Bitmap[7];
		protected FormAction m_ActionMode = FormAction.None;
		
		public FormActionPanel()
		{
			base.BackColor = Color.FromArgb(64, 64, 64);
			base.ForeColor = Color.FromArgb(230, 230, 230);
			this.Location = new Point(0, 0);
			this.Size = new Size(150, 20);
			this.MinimumSize = new Size(150, 20);
			this.MaximumSize = new Size(150, 20); 
			InitializeComponent();
			FormActionIcon[0] = Properties.Resources.FAction0;
			FormActionIcon[1] = Properties.Resources.FAction1;
			FormActionIcon[2] = Properties.Resources.FAction2;
			FormActionIcon[3] = Properties.Resources.FAction3;
			FormActionIcon[4] = Properties.Resources.FAction4;
			FormActionIcon[5] = Properties.Resources.FAction5;
			FormActionIcon[6] = Properties.Resources.FAction6;

			this.DoubleBuffered = true;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.UpdateStyles();
		}
		private int GetPos(MouseEventArgs e)
		{
			int ret = -1;
			int x = e.X / 30;
			if ((x >= 0) && (x <= 4))
			{
				ret = x + 1;
			}
			return ret;
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(BackColor))
			{
				Graphics g = pe.Graphics;
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);
				g.DrawImage(FormActionIcon[(int)m_ActionMode], 0, 0);
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				int pos = GetPos(e);
				if (pos >= 1)
				{
					m_ActionMode = (FormAction)pos;
					this.Invalidate();
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_ActionMode != FormAction.None)
			{
				FormAction mode = m_ActionMode;
				m_ActionMode = FormAction.None;
				this.Invalidate();
				Exec(mode);
				OnFormActionClick(new FormActionClickEventArgs(mode));
			}
			else
			{
				base.OnMouseUp(e);
			}
		}
		public void Exec(FormAction ac)
		{
			if (MainForm != null)
			{
				switch (ac)
				{
					case FormAction.New:
						MainForm.NewForm();
						break;
					case FormAction.Open:
						MainForm.OpenForm();
						break;
					case FormAction.Rename:
						MainForm.RenameForm();
						break;
					case FormAction.Dup:
						break;
					case FormAction.Close:
						MainForm.CloseForm();
						break;

				}
			}
		}
	}
}
