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
	public enum ControlAction
	{
		None,
		Add,
		Up,
		Down,
		Top,
		Bottom,
		Delete
	}
	public partial class ControlActionPanel : Control
	{
		
		public delegate void ControlActionClickHandler(object sender, ControlActionClickEventArgs e);
		public event ControlActionClickHandler? ControlActionClick;
		protected virtual void OnControlActionClick(ControlActionClickEventArgs e)
		{
			if (ControlActionClick != null)
			{
				ControlActionClick(this, e);
			}
		}
		protected Bitmap[] Action = new Bitmap[7];
		protected ControlAction m_ActionMode = ControlAction.None;
		public ControlActionPanel()
		{
			this.Location = new Point(0, 0);
			this.Size = new Size(120, 20);
			this.MinimumSize = new Size(120, 20);
			this.MaximumSize = new Size(120, 20);
			Action[0] = Properties.Resources.Action0;
			Action[1] = Properties.Resources.Action1;
			Action[2] = Properties.Resources.Action2;
			Action[3] = Properties.Resources.Action3;
			Action[4] = Properties.Resources.Action4;
			Action[5] = Properties.Resources.Action5;
			Action[6] = Properties.Resources.Action6;
			InitializeComponent();
			base.BackColor = Color.FromArgb(64, 64, 64);
			base.ForeColor = Color.FromArgb(230, 230, 230);
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
				ret = x+1;
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
					m_ActionMode = (ControlAction)pos;
					this.Invalidate();
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_ActionMode != ControlAction.None)
			{
				ControlAction mode = m_ActionMode;
				m_ActionMode = ControlAction.None;
				this.Invalidate();
				OnControlActionClick(new ControlActionClickEventArgs(mode));
			}
			else
			{
				base.OnMouseUp(e);
			}
		}
	}
	public class ControlActionClickEventArgs : EventArgs
	{
		public ControlAction Mode;
		public ControlActionClickEventArgs(ControlAction v)
		{
			Mode = v;
		}
	}
}
