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
	public enum SizeMoveMode
	{
		Move = 0,
		ResizeLeftTop,
		ResizeRightBottom
	}

	public class ModeChangedEventArgs : EventArgs
	{
		public SizeMoveMode Mode;
		public ModeChangedEventArgs(SizeMoveMode v)
		{
			Mode = v;
		}
	}
	public partial class SizeMoveModePanel : Control
	{
		public delegate void ModeChangedHandler(object sender, ModeChangedEventArgs e);
		public event ModeChangedHandler? ModeChanged;
		protected virtual void OnModeChanged(ModeChangedEventArgs e)
		{
			if (ModeChanged != null)
			{
				ModeChanged(this, e);
			}
		}
		private SizeMoveMode m_SizeMoveMode = SizeMoveMode.Move;
		[Category("Hypowered")]
		public SizeMoveMode SizeMoveMode
		{
			get { return m_SizeMoveMode; }
			set 
			{
				bool b = (m_SizeMoveMode != value);
				m_SizeMoveMode = value;
				this.Invalidate(); 
				if(b) OnModeChanged(new ModeChangedEventArgs(m_SizeMoveMode));
			}
		}

		protected Bitmap [] SM_Mode = new Bitmap[3];

		public SizeMoveModePanel()
		{
			this.Size = new Size(40, 40);
			this.MinimumSize = new Size(40, 40);
			this.MaximumSize = new Size(40, 40);

			SM_Mode[0] = Properties.Resources.MS_Mode0;
			SM_Mode[1] = Properties.Resources.MS_Mode1;
			SM_Mode[2] = Properties.Resources.MS_Mode2;


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

		protected override void OnPaint(PaintEventArgs pe)
		{
			using(SolidBrush sb = new SolidBrush(BackColor))
			{
				Graphics g = pe.Graphics;
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);
				g.DrawImage(SM_Mode[(int)m_SizeMoveMode], 0, 0);
			}
		}
		private int GetPos(MouseEventArgs e)
		{
			int ret = -1;
			int x = e.X/20;
			if(x < 0) { x = 0;}else if(x > 1) { x = 1; }
			int y = e.Y / 20;
			if (y < 0) { y = 0; } else if (y > 1) { y = 1; }
			ret = x + y * 2;
			return ret;
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				int pos = GetPos(e);
				SizeMoveMode? md = null;
				switch (pos)
				{
					case 0:
					case 1:
						md = SizeMoveMode.Move; 
						break;
					case 2:
						md = SizeMoveMode.ResizeLeftTop;
						break;
					case 3:
						md = SizeMoveMode.ResizeRightBottom;
						break;
				}
				if(md != null)
				{
					if (m_SizeMoveMode != md)
					{
						m_SizeMoveMode = (SizeMoveMode)md;
						this.Invalidate();
						OnModeChanged(new ModeChangedEventArgs(m_SizeMoveMode));
						return;
					}

				}
			}
			base.OnMouseDown(e);
		}
	}
}
