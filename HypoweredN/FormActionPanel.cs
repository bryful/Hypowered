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
		Load,
		Save,
		SaveAs,
		Close

	}
	public partial class FormActionPanel : Control
	{
		public class FormActionClickEventArgs : EventArgs
		{
			public FormAction Mode;
			public FormActionClickEventArgs(FormAction v)
			{
				Mode = v;
			}
		}
		public delegate void ControlActionClickHandler(object sender, FormActionClickEventArgs e);
		public event ControlActionClickHandler? FormActionClick;
		protected virtual void OnFormActionClick(FormActionClickEventArgs e)
		{
			if (FormActionClick != null)
			{
				FormActionClick(this, e);
			}
		}
		protected Bitmap[] Action = new Bitmap[6];
		protected FormAction m_ActionMode = FormAction.None;
		public FormActionPanel()
		{
			base.BackColor = Color.FromArgb(64, 64, 64);
			base.ForeColor = Color.FromArgb(230, 230, 230);
			this.Location = new Point(0, 0);
			this.Size = new Size(100, 20);
			this.MinimumSize = new Size(100, 20);
			this.MaximumSize = new Size(100, 20); 
			InitializeComponent();
			Action[0] = Properties.Resources.FAction0;
			Action[1] = Properties.Resources.FAction1;
			Action[2] = Properties.Resources.FAction2;
			Action[3] = Properties.Resources.FAction3;
			Action[4] = Properties.Resources.FAction4;
			Action[5] = Properties.Resources.FAction5;
			BackColor = Color.FromArgb(64, 64, 64);
			ForeColor = Color.FromArgb(230, 230, 230);
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
			int x = e.X / 20;
			if ((x >= 0) && (x <= 5))
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
				g.DrawImage(Action[(int)m_ActionMode], 0, 0);
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				int pos = GetPos(e);
				if (pos >= 0)
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
				OnFormActionClick(new FormActionClickEventArgs(mode));
			}
			else
			{
				base.OnMouseUp(e);
			}
		}
	}
}
