using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public class HLabel : HControl
	{
		protected Size m_LeftDotSize = new Size(12, 12);
		[Category("Hypowered_Label")]
		public Size LeftDotSize
		{
			get { return m_LeftDotSize; }
			set 
			{ 
				m_LeftDotSize = value;
				if (m_LeftDotSize.Height > base.Height-4) m_LeftDotSize.Height = base.Height-4;

				this.Invalidate(); 
			}
		}
		protected Size m_RightDotSize = new Size(12, 12);
		[Category("Hypowered_Label")]
		public Size RightDotSize
		{
			get { return m_RightDotSize; }
			set
			{
				m_RightDotSize = value;
				if (m_RightDotSize.Height > base.Height - 4) m_RightDotSize.Height = base.Height - 4;
				this.Invalidate();
			}
		}
		protected DotStyle m_LeftDot = DotStyle.None;
		[Category("Hypowered_Label")]
		public DotStyle LeftDot
		{
			get { return m_LeftDot; }
			set { m_LeftDot=value; this.Invalidate(); }
		}
		protected DotStyle m_RightDot = DotStyle.None;
		[Category("Hypowered_Label")]
		public DotStyle RightDot
		{
			get { return m_RightDot; }
			set { m_RightDot = value; this.Invalidate(); }
		}
		public HLabel()
		{
			m_HType = HType.Label;
			TextAlign = StringAlignment.Near;
			StringFormat.Alignment = StringAlignment.Center;
			StringFormat.LineAlignment = StringAlignment.Center;
			this.Size = new Size(100, 20);
			this.DoubleBuffered = true;
			this.SetStyle(
				//ControlStyles.Selectable |
				//ControlStyles.UserMouse |
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
			using (SolidBrush sb = new SolidBrush(base.BackColor))
			using (Pen p = new Pen(base.ForeColor))
			{
				Graphics g = pe.Graphics;
				// 塗り
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);

				sb.Color = BackColor;
				Rectangle r = RectInc(this.ClientRectangle, 2);
				g.FillRectangle(sb, r);


				int t = 0;

				if (m_LeftDot != DotStyle.None)
				{
					t = (this.Height - m_LeftDotSize.Height) / 2;
					Rectangle r1 = new Rectangle(
						r.Left, t, 
						m_LeftDotSize.Width,
						m_LeftDotSize.Height);
					r = new Rectangle(r1.Right + 2, r.Top, r.Width - m_LeftDotSize.Width - 2, r.Height);
					sb.Color = ForeColor;
					DrawDot(g, m_LeftDot, sb, r1);
				}
				if (m_RightDot != DotStyle.None)
				{
					t = (this.Height - m_RightDotSize.Height) / 2;
					Rectangle r2 = new Rectangle(
						r.Right - m_RightDotSize.Width,
						t,
						m_RightDotSize.Width,
						m_RightDotSize.Height);
					r = new Rectangle(r.Left, r.Top, r.Width - m_RightDotSize.Width - 2, r.Height);
					sb.Color = ForeColor;
					DrawDot(g, m_RightDot, sb, r2);
				}

				//文字
				if (this.Text != "")
				{
					sb.Color = ForeColor;
					g.DrawString(this.Text, this.Font, sb, r, StringFormat);
					//p.Color = ForeColor;	
					//g.DrawRectangle(p, r);
				}
				// IsEdit
				DrawIsEdit(g, p);
			}
		}
		// ********************************************************
		public override JsonObject? ToJson()
		{
			JsonFile? jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(LeftDotSize), (Size)LeftDotSize);//System.Drawing.Size
			jf.SetValue(nameof(RightDotSize), (Size)RightDotSize);//System.Drawing.Size
			jf.SetValue(nameof(LeftDot), (int)LeftDot);//Hypowered.HControl+DotStyle
			jf.SetValue(nameof(RightDot), (int)RightDot);//Hypowered.HControl+DotStyle
			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("LeftDotSize", typeof(Size).Name);
			if (v != null) LeftDotSize = (Size)v;
			v = jf.ValueAuto("RightDotSize", typeof(Size).Name);
			if (v != null) RightDotSize = (Size)v;
			v = jf.ValueAuto("LeftDot", typeof(Int32).Name);
			if (v != null) LeftDot = (DotStyle)v;
			v = jf.ValueAuto("RightDot", typeof(Int32).Name);
			if (v != null) RightDot = (DotStyle)v;
		}
	}
}
