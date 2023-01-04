using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class EditMenuBar : Control
	{
		public event EventHandler? ButtunClick;
		public virtual void OnButtunClick(EventArgs e)
		{
			if (ButtunClick != null)
			{
				ButtunClick(this, e);
			}
		}
		private Form? m_Parent = null;
		private int m_MenuHeight = 25;
		public EditMenuBar()
		{
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			this.Name = "HyperControl";
			this.Size = new Size(200,m_MenuHeight);
			this.Location = new Point(0, 0);
			InitializeComponent();
			SetParent();
		}
		public void SetParent()
		{
			if ((this.Parent != null) && (this.Parent is Form))
			{
				m_Parent = (Form)this.Parent;
				this.Font = m_Parent.Font;
				this.Text= m_Parent.Text;
				this.ForeColor = m_Parent.ForeColor;
				this.BackColor = m_Parent.BackColor;
				ChkSize();
				m_Parent.Resize -= M_Parent_Resize;
				m_Parent.Resize += M_Parent_Resize;
			}

		}

		private void M_Parent_Resize(object? sender, EventArgs e)
		{
			ChkSize();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			if ((this.Parent != null) && (this.Parent is Form)) m_Parent = (Form)this.Parent;
			using (Pen p= new Pen(ForeColor))
			using(SolidBrush sb = new SolidBrush(BackColor))
			{
				Graphics g = pe.Graphics;

				g.FillRectangle(sb, this.ClientRectangle);




				Color hf = Color.FromArgb(255, ForeColor.R / 2, ForeColor.G / 2, ForeColor.B / 2);
				p.Color = hf;
				for(int i=1; i<=3;i++)
				{
					int he = i*6;
					g.DrawLine(p, 8, he, this.Width - 8, he);
				}

				int h = m_MenuHeight - 1;
				p.Color = hf;

				StringFormat m_sf = new StringFormat();
				m_sf.Alignment = StringAlignment.Center;
				m_sf.LineAlignment= StringAlignment.Center;


				
				string s = "";
				if(m_Parent!= null)
				{
					s = m_Parent.Text;
				}
				else
				{
					s = this.Text;
				}
				SizeF stringSize = g.MeasureString(s, this.Font, 1000, m_sf);
				int ww = (int)stringSize.Width + 10;
				Rectangle rct = new Rectangle(
					this.Width / 2 - ww / 2,
					2,
					ww,
					this.Height - 4
					);
				sb.Color = BackColor;
				g.FillRectangle(sb, rct);
				sb.Color = ForeColor;
				g.DrawString(s, this.Font, sb, rct, m_sf);


				g.DrawLine(p, 0, h, this.Width, h);
				Point[] pt = new Point[]
				{
					new Point(0,m_MenuHeight-1),
					new Point(0,0),
					new Point(this.Size.Width-1,0),
					new Point(this.Size.Width-1,m_MenuHeight-1),
				};
				p.Color = ForeColor;
				g.DrawLines(p, pt);

				int w2 = 20;
				Rectangle f2 = new Rectangle(
					this.Bounds.Right - w2 - 10,
					(this.Height - w2) / 2,
					w2,
					w2);
				sb.Color = BackColor;
				g.FillRectangle(sb, f2);
				f2 = new Rectangle(f2.Left+3,f2.Top+3,f2.Width-6,f2.Height-6);
				sb.Color = Color.FromArgb(
					ForeColor.R/2,
					ForeColor.G / 2,
					ForeColor.B / 2);
				g.FillRectangle(sb, f2);


			}
		}
		public void ChkSize()
		{
			if (m_Parent!=null)
			{
				int w = this.Width;
				int h = this.Height;

				if(this.Location != new Point(0, 0))
				{
					this.Location = new Point(0, 0);
				}

				w = m_Parent.Width;
				if (h != m_MenuHeight) { h = m_MenuHeight; }
				if((this.Width!=w)||(this.Height!=h))
				{
					this.Size = new Size(w,h);
				}
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
		}
		protected override void OnLocationChanged(EventArgs e)
		{
			base.OnLocationChanged(e);
			if (this.Location != new Point(0, 0))
			{
				this.Location = new Point(0, 0);
			}
		}
		private bool m_md = false;
		private Point m_mdpos = new Point(0,0);
		private Point m_mdloc = new Point(0, 0);
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if((e.X>this.Width-30)&&(e.Y<25))
			{
				OnButtunClick(new EventArgs());
				return;
			}

			if(m_Parent!=null)
			{
				m_md = true;
				m_mdpos = new Point(e.X,e.Y);
				m_mdloc = m_Parent.Location;
			}
			else
			{
				base.OnMouseDown(e);
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if((m_Parent!=null)&&(m_md))
			{
				int ax = e.X - m_mdpos.X;
				int ay = e.Y - m_mdpos.Y;

				m_Parent.Location = new Point(m_Parent.Location.X + ax, m_Parent.Location.Y + ay);
			}
			else
			{
				base.OnMouseMove(e);

			}

		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			m_md= false;
			base.OnMouseUp(e);
		}
	}
}
