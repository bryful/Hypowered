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
	public enum ArrowDown
	{
		None,
		Top,
		Right,
		Bottom,
		Left
	}

	public partial class ArrowPanel : Control
	{

		public delegate void ArrowChangedHandler(object sender, ArrowChangedEventArgs e);
		public event ArrowChangedHandler? ArrowChanged;
		protected virtual void OnArrowChanged(ArrowChangedEventArgs e)
		{
			if (ArrowChanged != null)
			{
				ArrowChanged(this, e);
			}
		}
		protected Bitmap[] Arrows = new Bitmap[5];
		protected ArrowDown m_ArrowDown = ArrowDown.None;
		public ArrowPanel()
		{
			this.Location = new Point(0, 0);
			this.Size = new Size(80, 40);
			this.MinimumSize = new Size(80, 40);
			this.MaximumSize = new Size(80, 40);
			Arrows[0] = Properties.Resources.Arrow_Mode0;
			Arrows[1] = Properties.Resources.Arrow_Mode1;
			Arrows[2] = Properties.Resources.Arrow_Mode2;
			Arrows[3] = Properties.Resources.Arrow_Mode3;
			Arrows[4] = Properties.Resources.Arrow_Mode4;
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
			if (x < 0) { x = 0; } else if (x > 3) { x = 3; }
			int y = e.Y / 20;
			if (y < 0) { y = 0; } else if (y > 1) { y = 1; }
			ret = x + y * 4;
			return ret;
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(BackColor))
			{
				Graphics g = pe.Graphics;
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);
				g.DrawImage(Arrows[(int)m_ArrowDown], 0, 0);
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				int pos = GetPos(e);
				switch (pos)
				{
					case 0:
					case 4:
						m_ArrowDown = ArrowDown.Left;
						break;
					case 1:
					case 2:
						m_ArrowDown = ArrowDown.Top;
						break;
					case 3:
					case 7:
						m_ArrowDown = ArrowDown.Right;
						break;
					case 5:
					case 6:
						m_ArrowDown = ArrowDown.Bottom;
						break;
				}
				this.Invalidate();
				OnArrowChanged(new ArrowChangedEventArgs(m_ArrowDown));
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if(m_ArrowDown!= ArrowDown.None)
			{
				m_ArrowDown = ArrowDown.None;
				this.Invalidate();
			}
			base.OnMouseUp(e);
		}
	}
	// *****************************************
	public class ArrowChangedEventArgs : EventArgs
	{
		public ArrowDown Arrow;
		public ArrowChangedEventArgs(ArrowDown v)
		{
			Arrow = v;
		}
	}
}
