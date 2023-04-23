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
	public enum ArrangMode
	{
		None,
		HorLeft,
		HorCenter,
		HorRight,
		VurTop,
		VurCenter,
		VurBottom,
	}
	public partial class ArrangPanel : Control
	{
		
		public delegate void ArrangClickHandler(object sender, ArrangClickEventArgs e);
		public event ArrangClickHandler? ArrangClick;
		protected virtual void OnArrangClick(ArrangClickEventArgs e)
		{
			if (ArrangClick != null)
			{
				ArrangClick(this, e);
			}
		}
		protected Bitmap[] Arrang = new Bitmap[7];
		protected ArrangMode m_ArrangMode = ArrangMode.None; 
		public ArrangPanel()
		{
			this.Location = new Point(0, 0);
			this.Size = new Size(120, 20);
			this.MinimumSize = new Size(120, 20);
			this.MaximumSize = new Size(120, 20);
			Arrang[0] = Properties.Resources.Arrang0;
			Arrang[1] = Properties.Resources.Arrang1;
			Arrang[2] = Properties.Resources.Arrang2;
			Arrang[3] = Properties.Resources.Arrang3;
			Arrang[4] = Properties.Resources.Arrang4;
			Arrang[5] = Properties.Resources.Arrang5;
			Arrang[6] = Properties.Resources.Arrang6;
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
				g.DrawImage(Arrang[(int)m_ArrangMode], 0, 0);
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				int pos = GetPos(e);
				if (pos >= 0)
				{
					m_ArrangMode = (ArrangMode)pos;
					this.Invalidate();
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_ArrangMode != ArrangMode.None)
			{
				ArrangMode am = m_ArrangMode;
				m_ArrangMode = ArrangMode.None;
				this.Invalidate();
				OnArrangClick(new ArrangClickEventArgs(am));
			}
			base.OnMouseUp(e);
		}
	}
	// *************************************
	public class ArrangClickEventArgs : EventArgs
	{
		public ArrangMode Mode;
		public ArrangClickEventArgs(ArrangMode v)
		{
			Mode = v;
		}
	}
}
