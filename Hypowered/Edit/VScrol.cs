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

	public partial class VScrol : Control
	{
		public class ValueChangedEventArgs : EventArgs
		{
			public int Value = 0;
			public ValueChangedEventArgs(int pi)
			{
				Value = pi;
			}
		}
		public delegate void ValueChangedHandler(object sender, ValueChangedEventArgs e);
		public event ValueChangedHandler? ValueChanged;
		protected virtual void OnValueChanged(ValueChangedEventArgs e)
		{
			if (ValueChanged != null)
			{
				ValueChanged(this, e);
			}
		}
		// ************************************************
		private int m_Value = 0;
		public int Value
		{
			get { return m_Value; }
			set
			{
				bool b = (m_Value != value);
				m_Value = value;
				if(m_Value < 0) m_Value = 0;
				else if (m_Value>m_ValueMax) m_ValueMax = m_Value;
				ChkSize();
				this.Invalidate();
				if(b) OnValueChanged(new ValueChangedEventArgs(m_Value));
			}
		}
		private int m_ValueMax = 100;
		public int ValueMax
		{
			get { return m_ValueMax; }
			set
			{
				double v = (double)m_Value/ (double)m_ValueMax;
				m_ValueMax= value;
				if (m_ValueMax < 1) m_ValueMax = 1;
				int vv = (int)((double)m_ValueMax * v);
				bool b = (vv != m_Value);
				m_Value = vv;

				ChkSize();
				this.Invalidate();
				OnValueChanged(new ValueChangedEventArgs(m_Value));
			}
		}
		private int m_IconWidth = 12;

		private int m_ValueTrue =0;
		public int ValueTrue { get { return m_ValueTrue; } }
		private int m_ValueMaxTrue =100;
		public int ValueMaxTrue { get { return m_ValueMaxTrue; } }
		private double m_Rate = 1;
		private double m_RateRev = 1;
		public double Rate { get { return m_Rate; } }

		private Color m_BaseColor = Color.FromArgb(160,160,160);
		public Color BaseColor
		{
			get { return m_BaseColor; }
			set { m_BaseColor = value; this.Invalidate(); }
		}
		// ************************************************
		private void ChkSize()
		{
			if (this.Width != 20) this.Width = 20;
			m_ValueMaxTrue = this.Height - (m_IconWidth + 3) * 2 - m_IconWidth;
			if (m_ValueMaxTrue <= 0){m_ValueMaxTrue = 1;}
			if (m_ValueMax <= 0) { m_ValueMax = 1; }

			m_Rate = (double)m_ValueMaxTrue / (double)m_ValueMax;
			m_RateRev = (double)m_ValueMax / (double)m_ValueMaxTrue;

			m_ValueTrue = (int)((double)m_Value * m_Rate);
			if (m_ValueTrue < 0) m_ValueTrue = 0;
			else if (m_ValueTrue > m_ValueMaxTrue) m_ValueTrue = m_ValueMaxTrue;
		}

		public VScrol()
		{
			this.Size = new Size(20, 150);
			//this.MinimumSize = new Size(80, 40);
			//this.MaximumSize = new Size(80, 40);
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
			ChkSize();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			using(SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				//init
				Graphics g = pe.Graphics;
				sb.Color = BackColor;
				g.FillRectangle(sb, this.ClientRectangle);
				// line
				p.Color = m_BaseColor;
				int x = this.Width / 2;
				g.DrawLine(p, x, 10, x,this.Height - 10);

				//Top Bottom
				int l = (this.Width - m_IconWidth)/2;
				Rectangle rct = new Rectangle(l, 3, m_IconWidth, m_IconWidth);
				sb.Color = m_BaseColor;
				g.FillRectangle(sb, rct);
				rct = new Rectangle(l, this.Height - m_IconWidth - 3, m_IconWidth, m_IconWidth);
				g.FillRectangle(sb, rct);

				//Cursor
				m_ValueTrue = (int)((double)m_Value * m_Rate);
				Rectangle rr = new Rectangle(l, m_ValueTrue+m_IconWidth+3, m_IconWidth, m_IconWidth);
				sb.Color = ForeColor;
				g.FillEllipse(sb, rr);

			}
		}
		protected override void OnResize(EventArgs e)
		{
			ChkSize();
			this.Refresh();
			base.OnResize(e);
		}
		private bool m_MD= false;
		private int m_MDY = 0;
		private int m_MDVT = 0;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			bool b = false;
			if ((e.Button  & MouseButtons.Left) == MouseButtons.Left)
			{
				if(e.Y < m_IconWidth+3) 
				{
					b = (m_Value != 0);
					m_Value = 0;
					m_ValueTrue = 0;
					this.Invalidate();
					if (b) OnValueChanged(new ValueChangedEventArgs(m_Value));

				}else if (e.Y > this.Height - m_IconWidth - 3)
				{
					b = (m_Value != m_ValueMax);
					m_Value = m_ValueMax;
					m_ValueTrue = m_ValueMaxTrue;
					this.Invalidate();
					if (b) OnValueChanged(new ValueChangedEventArgs(m_Value));
				}
				else
				{
					int y = e.Y - m_IconWidth - 3;
					if((y>=m_ValueTrue)&&(y <= m_ValueTrue+m_IconWidth))
					{
						m_MD = true;
						m_MDY = e.Y;
						m_MDVT = m_ValueTrue;
					}else if (y < m_ValueTrue)
					{
						m_Value -= m_ValueTrue / 20;
						if (m_Value < 0) m_Value = 0;
						m_ValueTrue = (int)((double)m_Value * m_Rate);
						this.Invalidate ();
						OnValueChanged(new ValueChangedEventArgs(m_Value));
					}
					else
					{
						m_Value += m_ValueTrue / 20;
						if (m_Value > m_ValueMax) m_Value = m_ValueMax;
						m_ValueTrue = (int)((double)m_Value * m_Rate);
						this.Invalidate();
						OnValueChanged(new ValueChangedEventArgs(m_Value));
					}
				}
			}
			else
			{
				base.OnMouseDown(e);
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if(m_MD==true)
			{
				int dy = e.Y - m_MDY;
				m_ValueTrue = m_MDVT + dy;
				if (m_ValueTrue < 0) m_ValueTrue = 0;
				else if (m_ValueTrue > m_ValueMaxTrue) m_ValueTrue = m_ValueMaxTrue;
				m_Value = (int)((double)m_ValueTrue * m_RateRev);
				this.Invalidate ();
				OnValueChanged(new ValueChangedEventArgs(m_Value));
			}
			base.OnMouseMove(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if(m_MD==true)
			{
				m_MD = false;
			}
			base.OnMouseUp(e);
		}
	}
}
