using Microsoft.CodeAnalysis.Scripting;
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

	public partial class HyperMenuBar : HyperControl
	{
		static public readonly int MenuHeight = 25;
		protected HyperMenuItems m_menus = new HyperMenuItems();
		[Category("Hypowerd_Menu")]
		public HyperMenuItems Menus
		{
			get { return m_menus; }
		}
		protected Color m_MenuFourcusColor = Color.White;
		[Category("Hypowerd_Color")]
		public Color MenuFourcusColor
		{
			get { return m_MenuFourcusColor; }
			set { m_MenuFourcusColor = value; this.Invalidate(); }
		}
		protected Color m_MenuWakuColor = Color.White;
		[Category("Hypowerd_Color")]
		public Color MenuWakuColor
		{
			get { return m_MenuWakuColor; }
			set { m_MenuWakuColor = value; this.Invalidate(); }
		}
		protected Color m_MenuWakuEditColor = Color.White;
		[Category("Hypowerd_Color")]
		public Color MenuWakuEditColor
		{
			get { return m_MenuWakuEditColor; }
			set { m_MenuWakuEditColor = value; this.Invalidate(); }
		}
		public HyperMenuBar()
		{
			SetMyType(null);

			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			m_MenuFourcusColor = ColU.ToColor(HyperColor.MenuFourcus);
			m_MenuWakuColor = ColU.ToColor(HyperColor.Line);
			m_MenuWakuEditColor = ColU.ToColor(HyperColor.LineRed);
			this.Location= new Point(0, 0);
			this.Size = new Size(200, MenuHeight);
			this.Name = "HyperMenuBar";
			this.SetStyle(
//ControlStyles.Selectable |
//ControlStyles.UserMouse |
ControlStyles.DoubleBuffer |
ControlStyles.UserPaint |
ControlStyles.AllPaintingInWmPaint |
ControlStyles.SupportsTransparentBackColor,
true);
			this.UpdateStyles();
			InitializeComponent();
			ChkSize();
		}

		private void HyperMenuBar_SizeChanged(object? sender, EventArgs e)
		{
			ChkSize();
		}

		protected override void InitLayout()
		{
			base.InitLayout();
			if (this.Parent is HyperForm)
			{
				((HyperForm)this.Parent).SizeChanged += HyperMenuBar_SizeChanged;
			}
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
			using (SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = pe.Graphics;
				// 背景色
				g.FillRectangle(sb, this.ClientRectangle);



				if (m_menus.Count > 0)
				{
					StringFormat sf = new StringFormat();
					sf.Alignment = StringAlignment.Center;
					sf.LineAlignment = StringAlignment.Center;
					int x = HyperMenuItems.Leftmargin;
					foreach (HyperMenuItem? m in m_menus.Items)
					{
						Rectangle rct = new Rectangle(m.Left, 2, m.Width, this.Height-4);
						if(m_menuDown==m.Index)
						{
							sb.Color = m_MenuFourcusColor;
							g.FillRectangle(sb, rct);
						}
						sb.Color = ForeColor;
						g.DrawString(m.Caption, this.Font, sb, rct, sf);
					}
				}


				// 外枠
				Rectangle rr = ReRect(this.ClientRectangle, 1);
				if (m_IsEditMode)
				{
					p.Color = m_MenuWakuEditColor;
				}
				else
				{
					p.Color = m_MenuWakuColor;
				}
				g.DrawRectangle(p, rr);
			}
		}
		private int m_menuDown = -1;
		private int GetMenuDown(int x)
		{
			int ret = -1;
			if(m_menus.Count > 0)
			{
				for (int i=0;i< m_menus.Count;i++)
				{
					HyperMenuItem? m = m_menus[i];
					if (m == null) continue;
					if ((x>=m.Left)&&(x <m.Right))
					{
						ret = i;
						break;
					}
				}
			}
			return ret;
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				if(m_IsEditMode)
				{
					if (HyperForm != null)
					{
						HyperForm.ChkTarget(this);
					}
				}
				int idx = GetMenuDown(e.X);
				if (idx >= 0)
				{
					m_menuDown= idx;
					this.Invalidate();

					ContextMenuStrip? menu = m_menus[idx].MakeMenu();
					if (menu != null)
					{
						menu.Show(this, new Point(m_menus[idx].Left, MenuHeight));
						return;
					}
				}
			}
			if(this.Parent is HyperForm) ((HyperForm)this.Parent).DoMouseDown(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			int idx = GetMenuDown(e.X);
			if(m_menuDown!=idx)
			{
				m_menuDown = idx;
				this.Invalidate();
			}
			if (this.Parent is HyperForm) ((HyperForm)this.Parent).DoMouseMove(e);
			//base.OnMouseMove(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if(m_menuDown>=0)
			{
				m_menuDown = -1;
				this.Invalidate();
			}
			if (this.Parent is HyperForm) ((HyperForm)this.Parent).DoMouseUp(e);

		}
		protected override void OnMouseLeave(EventArgs e)
		{
			if (m_menuDown >= 0)
			{
				m_menuDown = -1;
				this.Invalidate();
			}
			base.OnMouseLeave(e);
		}
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			//this.Invalidate();
		}
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			if (m_menuDown >= 0)
			{
				m_menuDown = -1;
				this.Invalidate();
			}
		}
		// ****************************************************************************
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
		}
		protected override void OnLocationChanged(EventArgs e)
		{
			base.OnLocationChanged(e);
			ChkSize();
		}
		public void ChkSize()
		{
			if (this.Parent is Form)
			{
				this.Location = new Point(0, 0);
				this.Size = new Size(((Form)this.Parent).Width, MenuHeight);
				this.Invalidate();
			}
		}
	}
}
