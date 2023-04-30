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
	public enum MenuActionMode
	{
		None,
		Add,
		Up,
		Down,
		Delete
	}
	// *******************************************************
	public partial class MenuActionPanel : Control
	{
		
		public delegate void MenuActionClickHandler(object sender, MenuActionClickEventArgs e);
		protected event MenuActionClickHandler? MenuActionClick = null;
		protected virtual void OnControlActionClick(MenuActionClickEventArgs e)
		{
			if (MenuActionClick != null)
			{
				MenuActionClick(this, e);
			}
		}
		protected Bitmap[] Action = new Bitmap[5];
		protected MenuActionMode m_ActionMode = MenuActionMode.None;
		public MenuActionPanel()
		{
			this.Location = new Point(0, 0);
			this.Size = new Size(80, 20);
			this.MinimumSize = new Size(80, 20);
			this.MaximumSize = new Size(80, 20);
			Action[0] = Properties.Resources.MAction0;
			Action[1] = Properties.Resources.MAction1;
			Action[2] = Properties.Resources.MAction2;
			Action[3] = Properties.Resources.MAction3;
			Action[4] = Properties.Resources.MAction4;
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
					m_ActionMode = (MenuActionMode)pos;
					this.Invalidate();
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_ActionMode != MenuActionMode.None)
			{
				MenuActionMode mode = m_ActionMode;
				m_ActionMode = MenuActionMode.None;
				this.Invalidate();
				OnControlActionClick(new MenuActionClickEventArgs(mode));
			}
			else
			{
				base.OnMouseUp(e);
			}
		}
	}
	// *******************************************************
	public class MenuActionClickEventArgs : EventArgs
	{
		public MenuActionMode Mode;
		public MenuActionClickEventArgs(MenuActionMode v)
		{
			Mode = v;
		}
	}
}
