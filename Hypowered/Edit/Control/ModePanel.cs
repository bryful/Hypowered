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
	public partial class ModePanel : Control
	{
		public delegate void ModeChangedHandler(object sender, ModeChangedEventArgs e);
		public event ModeChangedHandler? ModeChanged = null;
		protected virtual void OnModeChanged(ModeChangedEventArgs e)
		{
			if (ModeChanged != null)
			{
				ModeChanged(this, e);
			}
		}
		private bool m_Mode = false;
		public bool Mode
		{
			get { return m_Mode; }
			set
			{
				bool b = (m_Mode != value);
				m_Mode = value;
				this.Invalidate();
				if(b) { OnModeChanged(new ModeChangedEventArgs(m_Mode)); }
			}
		}
		//protected Bitmap[] Action = new Bitmap[2];
		public ModePanel()
		{
			this.Location = new Point(0, 0);
			this.Size = new Size(100, 20);
			//Action[0] = Properties.Resources.EditMode0;
			//Action[1] = Properties.Resources.EditMode1;
			InitializeComponent();
			base.BackColor = Color.FromArgb(64, 64, 64);
			base.ForeColor = Color.FromArgb(180, 180, 180);
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

		protected override void OnPaint(PaintEventArgs pe)
		{
			using (StringFormat sf = new StringFormat())
			using (Pen p = new Pen(ForeColor))
			using (SolidBrush sb = new SolidBrush(BackColor))
			{
				Graphics g = pe.Graphics;
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);
				
				Rectangle r = new Rectangle(1,1,this.Width-3,this.Height-3);
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;
				if (m_Mode)
				{
					sb.Color = ForeColor;
					g.FillRectangle(sb, r);
					sb.Color = BackColor;
				}
				else
				{
					p.Color = ForeColor;
					g.DrawRectangle(p, r);
					sb.Color = ForeColor;
				}
				g.DrawString(this.Text, this.Font, sb, r, sf);



			}
		}
		private bool m_MD = false;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				m_Mode = ! m_Mode;

				this.Invalidate();
				OnModeChanged(new ModeChangedEventArgs(m_Mode));
			}
			base.OnMouseDown(e);
		}
	}
	public class ModeChangedEventArgs : EventArgs
	{
		public bool Mode;
		public ModeChangedEventArgs(bool v)
		{
			Mode = v;
		}
	}
}
