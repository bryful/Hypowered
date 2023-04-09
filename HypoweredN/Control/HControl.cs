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
	public enum HType
	{
		None,
		Button,
		Label,
		TextBox
	}
	public partial class HControl : Control
	{
		#region Prop
		protected bool m_IsEdit = false;
		[Category("_Hypowered")]
		public bool IsEdit
		{
			get { return m_IsEdit; }
			set { SetIsEdit(value); }
		}
		public void SetIsEdit(bool b)
		{
			m_IsEdit = b;
			this.Invalidate();
		}
		protected HType m_HType = HType.None;
		[Category("_Hypowered")]
		public HType HType
		{
			get { return m_HType; }
		}
		[Category("_Hypowered")]
		public int Index { get; set; } = -1;
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean AllowDrop
		{
			get { return base.AllowDrop; }
			set { base.AllowDrop = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.String Name
		{
			get { return base.Name; }
			set { base.Name = value; }
		}

		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.AnchorStyles Anchor
		{
			get { return base.Anchor; }
			set { base.Anchor = value; }
		}
		[Category("Hypowered_Color"), Browsable(true)]
		public new System.Drawing.Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; this.Invalidate(); }
		}
		[Category("Hypowered_Draw"), Browsable(true)]
		public new System.Drawing.Font Font
		{
			get { return base.Font; }
			set { base.Font = value; this.Invalidate(); }
		}
		[Category("Hypowered_Color"), Browsable(true)]
		public new System.Drawing.Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; this.Invalidate(); }
		}
		protected Color m_IsEditColor = Color.Red;
		[Category("Hypowered_Color"), Browsable(true)]
		public System.Drawing.Color IsEditColor
		{
			get { return m_IsEditColor; }
			set { m_IsEditColor = value; this.Invalidate(); }
		}

		public int m_GridSize = 2;
		[Category("Hypowered_Size"), Browsable(true)]
		public  int GridSize
		{
			get { return m_GridSize; }
			set 
			{
				if(value<=1) value = 1;
				m_GridSize = value;
				ChkGridSize();
			}
		}
		private void ChkGridSize()
		{
			if(m_GridSize <= 1) return;
			base.Location = new Point(
				(base.Location.X/ m_GridSize)* m_GridSize,
				(base.Location.Y / m_GridSize) * m_GridSize);
			base.Size = new Size(
				(base.Size.Width / m_GridSize) * m_GridSize,
				(base.Size.Height / m_GridSize) * m_GridSize);
		}
		[Category("Hypowered_Size"), Browsable(true)]
		public new System.Drawing.Point Location
		{
			get { return base.Location; }
			set { base.Location = value; ChkGridSize(); }
		}
		[Category("Hypowered_Size"), Browsable(true)]
		public new System.Windows.Forms.Padding Margin
		{
			get { return base.Margin; }
			set { base.Margin = value; }
		}
		[Category("Hypowered_Size"), Browsable(true)]
		public new System.Windows.Forms.Padding Padding
		{
			get { return base.Padding; }
			set { base.Padding = value; }
		}
		[Category("Hypowered_Size"), Browsable(true)]
		public new System.Drawing.Size MaximumSize
		{
			get { return base.MaximumSize; }
			set { base.MaximumSize = value; }
		}
		[Category("Hypowered_Size"), Browsable(true)]
		public new System.Drawing.Size MinimumSize
		{
			get { return base.MinimumSize; }
			set { base.MinimumSize = value; }
		}
		[Category("Hypowered_Size"), Browsable(true)]
		public new System.Drawing.Size Size
		{
			get { return base.Size; }
			set { base.Size = value;ChkGridSize(); }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Object Tag
		{
			get { return base.Tag; }
			set { base.Tag = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.String Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean Visible
		{
			get { return base.Visible; }
			set { base.Visible = value; }
		}
		#endregion
		protected StringFormat StringFormat { get; set; } = new StringFormat();  
		// ************************************************************
		public HControl()
		{
			StringFormat.Alignment = StringAlignment.Center;
			StringFormat.LineAlignment = StringAlignment.Center;

			this.Location = new Point(20, 50);
			this.Size = new Size(100, 100);
			InitializeComponent();
			base.BackColor = Color.FromArgb(64, 64, 64);
			base.ForeColor = Color.FromArgb(230, 230, 230);
			this.DoubleBuffered = true;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw|
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.UpdateStyles();
			//HUtils.PropToClipboard(typeof(HControl));
		}
		// ************************************************************

		protected override void OnPaint(PaintEventArgs pe)
		{
			using(SolidBrush sb = new SolidBrush(base.BackColor))
			using (Pen p = new Pen(base.ForeColor))
			{
				Graphics g = pe.Graphics;
				// 塗り
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);

				sb.Color = BackColor;
				Rectangle r = new Rectangle(2, 2, this.Width - 4, this.Height - 4);
				g.FillRectangle(sb, r);
				p.Color = ForeColor;
				r = new Rectangle(2, 2, this.Width - 4-1, this.Height - 4-1);
				g.DrawRectangle(p, r);


				// IsEdit
				if(m_IsEdit)
				{
					r = new Rectangle(0, 0, this.Width  - 1, this.Height - 1);
					p.Color = m_IsEditColor;
					p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
					g.DrawRectangle(p, r);
				}
			}
		}
		// ************************************************************
		protected bool m_MD = false;
		protected Point m_MDP = new Point(0,0);
		protected Point m_MDLoc = new Point(0, 0);
		// ************************************************************
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if(m_IsEdit==true)
			{
				if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
				{
					m_MD = true;
					m_MDLoc = this.Location;
					m_MDP = this.PointToScreen(new Point(e.X,e.Y));
					return;
				}

			}
			base.OnMouseDown(e);
		}
		// ************************************************************
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if((m_IsEdit==true)&&(m_MD))
			{
				Point p = this.PointToScreen(new Point(e.X, e.Y));
				int dx = p.X - m_MDP.X;
				int dy = p.Y - m_MDP.Y;
				this.Location = new Point(m_MDLoc.X +dx, m_MDLoc.Y + dy);
			}
			base.OnMouseMove(e);
		}
		// ************************************************************
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if(m_MD)
			{
				m_MD = false;
			}
			base.OnMouseUp(e);
		}
	}
}
