﻿using System;
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
		TextBox,
		PictureBox
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
		public virtual void SetIsEdit(bool b)
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
			set { base.Name = value; this.Invalidate(); }
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
		protected Color m_ForcusColor = Color.Blue;
		[Category("Hypowered_Color"), Browsable(true)]
		public System.Drawing.Color ForcusColor
		{
			get { return m_ForcusColor; }
			set { m_ForcusColor = value; this.Invalidate(); }
		}
		[Category("Hypowered_Color"), Browsable(true)]
		public new System.Drawing.Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; this.Invalidate(); }
		}
		protected Color m_IsEditColor = Color.Yellow;
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
		protected void ChkGridSize()
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
		public double CenterWidth
		{
			get { return (double)base.Left + (double)base.Width/2; }
			set
			{
				base.Left =  (int)(value - (double)base.Width/2);
			}
		}
		[Category("Hypowered_Size"), Browsable(true)]
		public double CenterHeight
		{
			get { return (double)base.Top + (double)base.Height / 2; }
			set
			{
				base.Top = (int)(value - (double)base.Height / 2);
			}
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
		[Category("Hypowered_Text"), Browsable(true)]
		public new System.String Text
		{
			get { return base.Text; }
			set { base.Text = value; this.Invalidate(); }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean Visible
		{
			get { return base.Visible; }
			set { base.Visible = value; this.Invalidate(); }
		}
		[Category("Hypowered_Text"), Browsable(true)]
		public new System.Drawing.Font Font
		{
			get { return base.Font; }
			set { base.Font = value; this.Invalidate(); }
		}
		public StringFormat StringFormat = new StringFormat();
		[Category("Hypowered_Text"), Browsable(true)]
		public StringAlignment TextAlign
		{
			get { return StringFormat.Alignment; }
			set 
			{ 
				StringFormat.Alignment = value;
				this.Invalidate();
			}
		}
		[Category("Hypowered_Text"), Browsable(true)]
		public StringAlignment TextLineAlign
		{
			get { return StringFormat.LineAlignment; }
			set
			{
				StringFormat.LineAlignment = value;
				this.Invalidate();
			}
		}
		public Control? Ctrl { get;set; }= null;
		#endregion
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
				ControlStyles.Selectable|
				ControlStyles.UserMouse|
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
				Rectangle r = RectInc(this.ClientRectangle, 2);
				g.FillRectangle(sb, r);
				p.Color = ForeColor;
				DrawFrame(g,p,r,1);
				// IsEdit
				bool b = ((Ctrl != null) && (Ctrl.Focused));
				if((Focused)||(b))
				{
					p.Color = m_ForcusColor;
					DrawFrame(g, p, this.ClientRectangle, 2);
				}
				DrawIsEdit(g, p);
			}
		}
		// ************************************************************
		protected bool m_MD = false;
		protected bool m_MDResize = false;
		protected Point m_MDP = new Point(0,0);
		protected Point m_MDLoc = new Point(0, 0);
		protected Size m_MDSize = new Size(0, 0);
		// ************************************************************
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if(m_IsEdit==true)
			{
				if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
				{
					m_MD = true;
					m_MDLoc = this.Location;
					m_MDSize = this.Size;
					m_MDP = this.PointToScreen(new Point(e.X,e.Y));
					m_MDResize = ((e.X > this.Width - 10) && (e.Y > this.Height - 10));
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
				if (m_MDResize)
				{
					this.Size = new Size(m_MDSize.Width + dx, m_MDSize.Height + dy);
				}
				else
				{
					this.Location = new Point(m_MDLoc.X + dx, m_MDLoc.Y + dy);
				}
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
		// ************************************************************
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.Invalidate();
		}
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.Invalidate();
		}
		// ************************************************************
		public void MovePos(ArrowDown ad,int MoveScale)
		{
			int v = m_GridSize * MoveScale;
			switch(ad)
			{
				case ArrowDown.Top:
					this.Top -= v;
					break;
				case ArrowDown.Left:
					this.Left -= v;
					break;
				case ArrowDown.Bottom:
					this.Top += v;
					break;
				case ArrowDown.Right:
					this.Left += v;
					break;
			}
		}
		public void ResizeLeftTop(ArrowDown ad, int MoveScale)
		{
			int v = m_GridSize * MoveScale;
			switch (ad)
			{
				case ArrowDown.Top:
					this.Top -= v;
					this.Height += v;
					break;
				case ArrowDown.Left:
					this.Left -= v;
					this.Width += v;
					break;
				case ArrowDown.Bottom:
					if (this.Height > 10)
					{
						this.Top += v;
						this.Height -= v;
					}
					break;
				case ArrowDown.Right:
					if (this.Width > 10)
					{
						this.Left += v;
						this.Width -= v;
					}
					break;
			}
		}
		public void ResizeRightBottom(ArrowDown ad, int MoveScale)
		{
			int v = m_GridSize * MoveScale;
			switch (ad)
			{
				case ArrowDown.Top:
					if (this.Height > 10)
					{
						this.Height -= v;
					}
					break;
				case ArrowDown.Left:
					if (this.Width > 10)
					{
						this.Width -= v;
					}
					break;
				case ArrowDown.Bottom:
					this.Height += v;
					break;
				case ArrowDown.Right:
					this.Width += v;
					break;
			}
		}
		// ************************************************************
		public Rectangle RectInc(Rectangle r, int a)
		{
			return new Rectangle(
				r.Left + a,
				r.Top + a,
				r.Width - a*2,
				r.Height - a*2
				);
		}
		public void DrawFrame( Graphics g ,Pen p, Rectangle r,int w=1)
		{
			p.Width = 1;
			Rectangle r2 = new Rectangle(r.Left, r.Top, r.Width - 1, r.Height - 1);
			for (int i=0; i<w;i++)
			{
				g.DrawRectangle(p,r2);
				r2 = new Rectangle(r2.Left,r2.Top,r2.Width-1 , r2.Height-1 );
			}
		}
		public enum DotStyle
		{
			None,
			Square,
			Circle,
			TriangleTop,
			TriangleRight,
			TriangleBottom,
			Triangleleft,
			Rhombus
		}
		public void DrawDot(Graphics g, DotStyle ds,SolidBrush sb, Rectangle r)
		{
			switch(ds)
			{
				case DotStyle.None:
					return;
				case DotStyle.Square:
					g.FillRectangle(sb, r); 
					break;
				case DotStyle.Circle:
					g.FillEllipse(sb, r);
					break;
				case DotStyle.TriangleTop:
					PointF[] pnts0 = new PointF[3];
					pnts0[0] = new PointF((float)r.Left + (float)r.Width / 2, (float)r.Top);
					pnts0[1] = new PointF((float)r.Right, (float)r.Bottom);
					pnts0[2] = new PointF((float)r.Left, (float)r.Bottom);
					g.FillPolygon(sb, pnts0);
					break;
				case DotStyle.TriangleRight:
					PointF[] pnts1 = new PointF[3];
					pnts1[0] = new PointF((float)r.Left , (float)r.Top);
					pnts1[1] = new PointF((float)r.Right, (float)r.Top+(float)r.Height/2);
					pnts1[2] = new PointF((float)r.Left, (float)r.Bottom);
					g.FillPolygon(sb, pnts1);
					break;
				case DotStyle.TriangleBottom:
					PointF[] pnts2 = new PointF[3];
					pnts2[0] = new PointF((float)r.Left, (float)r.Top);
					pnts2[1] = new PointF((float)r.Right, (float)r.Top );
					pnts2[2] = new PointF((float)r.Left +(float)r.Width/2, (float)r.Bottom);
					g.FillPolygon(sb, pnts2);
					break;
				case DotStyle.Triangleleft:
					PointF[] pnts3 = new PointF[3];
					pnts3[0] = new PointF((float)r.Left, (float)r.Top+(float)r.Height/2);
					pnts3[1] = new PointF((float)r.Right, (float)r.Top);
					pnts3[2] = new PointF((float)r.Right, (float)r.Bottom);
					g.FillPolygon(sb, pnts3);
					break;
				case DotStyle.Rhombus:
					PointF[] pnts = new PointF[4];
					pnts[0] = new PointF((float)r.Left + (float)r.Width / 2, (float)r.Top);
					pnts[1] = new PointF((float)r.Right, (float)r.Top + (float)r.Height/2);
					pnts[2] = new PointF(pnts[0].X,r.Bottom);
					pnts[3] = new PointF((float)r.Left, pnts[1].Y);
					g.FillPolygon(sb, pnts);
					break;
			}
		}
		public void DrawIsEdit(Graphics g, Pen p)
		{
			if (m_IsEdit)
			{
				Rectangle r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
				p.Color = m_IsEditColor;
				p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
				g.DrawRectangle(p, r);
			}
		}
		protected override void OnResize(EventArgs e)
		{
			ChkGridSize();
			base.OnResize(e);
			this.Invalidate();
		}
	}
}
