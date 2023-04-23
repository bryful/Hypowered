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
	public partial class AlignPanel : Control
	{
	
		public delegate void AlignClickHandler(object sender, AlignClickEventArgs e);
		public event AlignClickHandler? AlignClick;
		protected virtual void OnAlignClick(AlignClickEventArgs e)
		{
			if (AlignClick != null)
			{
				AlignClick(this, e);
			}
		}
		protected Bitmap[] Align = new Bitmap[7];
		protected AlignMode m_AlignMode = AlignMode.None;
		public AlignPanel()
		{
			this.Location = new Point(0, 0);
			this.Size = new Size(120, 20);
			this.MinimumSize = new Size(120, 20);
			this.MaximumSize = new Size(120, 20);
			Align[0] = Properties.Resources.Aligin0;
			Align[1] = Properties.Resources.Aligin1;
			Align[2] = Properties.Resources.Aligin2;
			Align[3] = Properties.Resources.Aligin3;
			Align[4] = Properties.Resources.Aligin4;
			Align[5] = Properties.Resources.Aligin5;
			Align[6] = Properties.Resources.Aligin6;
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
			if((x>=0)&&(x<=5))
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
				g.DrawImage(Align[(int)m_AlignMode], 0, 0);
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				int pos = GetPos(e);
				if(pos>=0)
				{
					m_AlignMode = (AlignMode)pos;
					this.Invalidate();
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_AlignMode != AlignMode.None)
			{
				AlignMode am = m_AlignMode;
				m_AlignMode = AlignMode.None;
				this.Invalidate();
				OnAlignClick(new AlignClickEventArgs(am));
			}
			base.OnMouseUp(e);
		}
	}
	// **************************
	public enum AlignMode
	{
		None,
		VurTop,
		VurCenter,
		VurBottom,
		HorLeft,
		HorCenter,
		HorRight
	}
	public class AlignClickEventArgs : EventArgs
	{
		public AlignMode Mode;
		public AlignClickEventArgs(AlignMode v)
		{
			Mode = v;
		}
	}
}
