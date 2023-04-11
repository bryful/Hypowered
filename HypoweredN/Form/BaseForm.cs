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
	public enum CloseAction
	{
		None = 0,
		Hide,
		Close,
		DROK,
		DRCancel,

	}
	public partial class BaseForm : Form
	{
		#region Props
		[Category("Hypowered"), Browsable(true)]
		public new System.String Text
		{
			get { return base.Text; }
			set { base.Text = value; this.Invalidate(); }
		}
		[Category("Hypowered_Draw")]
		public StringFormat SFormat { get; set; } = new StringFormat();
		[Category("Hypowered")]
		public new System.String Name
		{
			get { return base.Name; }
			set { base.Name = value; }
		}
		protected CloseAction m_CloseAction = CloseAction.Close;
		[Category("Hypowered")]
		public CloseAction CloseAction
		{
			get { return m_CloseAction; }
			set { m_CloseAction = value; }
		}
		[Category("Hypowered_Draw")]
		public new System.Int32 DeviceDpi
		{
			get { return base.DeviceDpi; }
		}
		[Category("Hypowered")]
		public new bool Enabled
		{
			get { return base.Enabled; }
			set { base.Enabled = value; }
		}
		[Category("Hypowered_Draw")]
		public new Font Font
		{
			get { return base.Font; }
			set { base.Font = value; }
		}

		[Category("Hypowered")]
		public new System.Object Tag
		{
			get { return base.Tag; }
			set { base.Tag = value; }
		}
		[Category("Hypowered")]
		public new bool Visible
		{
			get { return base.Visible; }
			set { base.Visible = value; }
		}
		protected bool m_CanResize = true;
		[Category("Hypowered_Size")]
		public bool CanResize
		{
			get { return m_CanResize; }
			set { m_CanResize = value; }
		}
		[Category("Hypowered_Size")]
		public new Point Location
		{
			get { return base.Location; }
			set { base.Location = value; }
		}
		[Category("Hypowered_Size")]
		public new Size MaximumSize
		{
			get { return base.MaximumSize; }
			set { base.MaximumSize = value; }
		}
		[Category("Hypowered_Size")]
		public new Size MinimumSize
		{
			get { return base.MinimumSize; }
			set { base.MinimumSize = value; }
		}
		[Category("Hypowered_Size")]
		public new System.Drawing.Size PreferredSize
		{
			get { return base.PreferredSize; }
		}
		[Category("Hypowered_Size")]
		public new System.Windows.Forms.Padding Padding
		{
			get { return base.Padding; }
			set { base.Padding = value; }
		}
		[Category("Hypowered")]
		public new bool TopMost
		{
			get { return base.TopMost; }
			set { base.TopMost = value; this.Invalidate(); }
		}


		[Category("Hypowered_Size")]
		public int BarHeight
		{
			get { return m_BarHeight; }
			set
			{
				m_BarHeight = value;
				int w = m_BarHeight - 8;
				m_TopMostRect = new Rectangle(10, 4, w, w);
				CalcCloseRect();
				Invalidate();
			}
		}
		protected Color m_BarBackColor = Color.FromArgb(80, 80, 80);
		[Category("Hypowered_Color"),Browsable(true)]
		public Color BarBackColor
		{
			get { return m_BarBackColor; }
			set { m_BarBackColor = value; this.Invalidate(); }
		}
		[Category("Hypowered_Color")]
		public new System.Drawing.Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; }
		}
		[Category("Hypowered_Color")]
		public new System.Drawing.Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}

		[Category("Hypowered_Draw")]
		public new double Opacity
		{
			get { return base.Opacity; }
			set { base.Opacity = value; }
		}
		[Category("Hypowered_Draw")]
		public new bool DoubleBuffered
		{
			get { return base.DoubleBuffered; }
			set { base.DoubleBuffered = value; }
		}

		#endregion

		protected int m_BarHeight = 20;
		protected Rectangle m_TopMostRect = new Rectangle(10, 4, 12, 12);
		protected Rectangle m_CloseRect = new Rectangle(10, 4, 12, 12);
		protected void CalcCloseRect()
		{
			int w = m_BarHeight - 8;
			m_CloseRect = new Rectangle(this.Width - w -10, 4, w, w);

		}
		// ************************************************************
		public BaseForm()
		{
			SFormat.Alignment = StringAlignment.Near;
			SFormat.LineAlignment = StringAlignment.Center;
			InitializeComponent();
			base.BackColor = Color.FromArgb(64, 64, 64);
			base.ForeColor = Color.FromArgb(230, 230, 230);
			this.DoubleBuffered = true;
			base.AutoScaleMode = AutoScaleMode.None;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw,
				true);
			this.UpdateStyles();



			//HUtils.PropListToClipboard(typeof(BaseForm),"BaseForm");
		}
		// ************************************************************
		// ************************************************************
		protected override void OnResize(EventArgs e)
		{
			CalcCloseRect();

			base.OnResize(e);
		}
		// ************************************************************
		protected override void OnPaint(PaintEventArgs e)
		{
			using (SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = e.Graphics;
				g.FillRectangle(sb, this.ClientRectangle);
				// TopBar
				Rectangle rct = new Rectangle(0,0,this.Width,m_BarHeight);
				sb.Color = m_BarBackColor;
				g.FillRectangle(sb, rct);
				// TopBar TopMost
				if (this.TopMost)
				{
					sb.Color = ForeColor;
					g.FillRectangle(sb, m_TopMostRect);
				}
				else
				{
					p.Color = ForeColor;
					g.DrawRectangle(p, m_TopMostRect);
				}
				// TopBar Title
				rct = new Rectangle(m_TopMostRect.Right+2,0,
					this.Width-m_TopMostRect.Right,m_BarHeight);
				sb.Color = ForeColor;
				g.DrawString(this.Text, this.Font, sb, rct, SFormat);
				// TopBar Close
				p.Color = ForeColor;
				g.DrawRectangle(p, m_CloseRect);
				g.DrawLine(p, m_CloseRect.Left, m_CloseRect.Top, m_CloseRect.Right, m_CloseRect.Bottom);
				g.DrawLine(p, m_CloseRect.Left, m_CloseRect.Bottom, m_CloseRect.Right, m_CloseRect.Top);

				// 外枠
				rct = new Rectangle(0,0,this.Width-1,this.Height-1);
				p.Color = m_BarBackColor;
				g.DrawRectangle (p, rct);

			}

			base.OnPaint(e);
		}
		// ************************************************************
		private bool m_MD = false;
		private Point m_MDPoint = new Point(0,0);
		private Point m_MDLocation = new Point(0, 0);
		private bool m_MDResize = false;
		private Size m_MDSize = new Size(0, 0);

		// ************************************************************
		private bool InRect(int x, int y, Rectangle r)
		{
			return ((x >= r.Left) && (x < r.Right) && (y >= r.Top) && (y < r.Bottom));
		}
		// ************************************************************
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				if (InRect(e.X, e.Y, m_TopMostRect))
				{
					TopMost = !TopMost;
				} else if (InRect(e.X, e.Y, m_CloseRect))
				{
					switch (m_CloseAction)
					{
						case CloseAction.Hide:
							this.Hide();
							break;
						case CloseAction.Close:
							this.Close();
							break;
						case CloseAction.DROK:
							this.DialogResult = DialogResult.OK;
							break;
						case CloseAction.DRCancel:
							this.DialogResult = DialogResult.Cancel;
							break;
					}
				}
				else if (e.Y < m_BarHeight)
				{
					m_MD = true;
					m_MDPoint = new Point(e.X, e.Y);
					m_MDLocation = new Point(this.Location.X, this.Location.Y);
				} else if ((m_CanResize == true) 
					&& (e.X > this.Width - 20)
					&& (e.Y > this.Height - 20)
					)
				{
					m_MDResize = true;
					m_MDPoint = new Point(e.X, e.Y);
					m_MDSize = this.Size;
				}
			}
			else{
				base.OnMouseDown(e);
			}
		}
		// ************************************************************
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (m_MD)
			{
				int dx = e.X - m_MDPoint.X;
				int dy = e.Y - m_MDPoint.Y;
				this. Location =new Point(this.Location.X + dx, this.Location.Y + dy);
			}else if(m_MDResize)
			{
				int dx = e.X - m_MDPoint.X;
				int dy = e.Y - m_MDPoint.Y;
				this.Size = new Size(m_MDSize.Width + dx, m_MDSize.Height + dy);
			}
			else
			{
				base.OnMouseMove(e);
			}
		}
		// ************************************************************
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if ((m_MD)||(m_MDResize))
			{
				m_MD=false;
				m_MDResize=false;
			}
			else
			{
				base.OnMouseUp(e);
			}
		}
		// ************************************************************

	}
}
