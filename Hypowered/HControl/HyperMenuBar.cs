using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace Hypowered
{

	public partial class HyperMenuBar : HyperControl
	{
		static public readonly int MenuHeight = 25;
		protected HyperMenuItems m_Items = new HyperMenuItems();
		[Category("Hypowerd_Menu")]
		public HyperMenuItems Items
		{
			get { return m_Items; }
		}
		public bool GetMenuVisibled(int index)
		{
			return m_Items.GetMenuVisibled(index);
		}
		public void SetMenuVisibled(int index,bool on)
		{
			m_Items.SetMenuVisibled(index,on);
			this.Invalidate();
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



				if (m_Items.Count > 0)
				{
					StringFormat sf = new StringFormat();
					sf.Alignment = StringAlignment.Center;
					sf.LineAlignment = StringAlignment.Center;
					int x = HyperMenuItems.Leftmargin;
					foreach (HyperMenuItem? m in m_Items.Items)
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
				Rectangle rr = new Rectangle(0,0,this.Width-1,this.Height-1);
				if (m_IsEditMode)
				{
					p.Color = m_MenuWakuEditColor;
					g.DrawRectangle(p, rr);
				}
				else
				{
					Point[] pt = new Point[]
					{
						new Point(0,this.Height),
						new Point(0,0),
						new Point(this.Width-1,0),
						new Point(this.Width-1,this.Height)

					};
					DrawMenuFrame(g, p, this.ClientRectangle);
				}
				DrawType(g, sb);

			}
		}
		// *************************************************************
		public void DrawMenuFrame(Graphics g, Pen p, Rectangle rct)
		{
			float pw2;
			if (m_FrameWeight.Bottom > 0)
			{
				p.Color = m_MenuWakuColor;
				p.Width = (float)m_FrameWeight.Bottom;
				pw2 = rct.Bottom - (float)m_FrameWeight.Bottom / 2;
				g.DrawLine(p, rct.Left, pw2, rct.Right, pw2);
			}
			p.Color = ForeColor;
			if (m_FrameWeight.Top > 0)
			{
				p.Width = (float)m_FrameWeight.Top;
				pw2 = rct.Top + (float)m_FrameWeight.Top / 2;
				g.DrawLine(p, rct.Left, pw2, rct.Right, pw2);
			}

			if (m_FrameWeight.Left > 0)
			{
				p.Width = (float)m_FrameWeight.Left;
				pw2 = rct.Left + (float)m_FrameWeight.Left / 2;
				g.DrawLine(p, pw2, rct.Top, pw2, rct.Bottom);
			}
			if (m_FrameWeight.Right > 0)
			{
				p.Width = (float)m_FrameWeight.Right;
				pw2 = rct.Right - (float)m_FrameWeight.Right / 2;
				g.DrawLine(p, pw2, rct.Top, pw2, rct.Bottom);
			}


		}
		// *************************************************************
		private int m_menuDown = -1;
		private int GetMenuDown(int x)
		{
			int ret = -1;
			if(m_Items.Count > 0)
			{
				for (int i=0;i< m_Items.Count;i++)
				{
					HyperMenuItem? m = m_Items[i];
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
						//HyperForm.ChkTarget(this);
					}
				}
				int idx = GetMenuDown(e.X);
				if (idx >= 0)
				{
					m_menuDown= idx;
					this.Invalidate();

					ContextMenuStrip? menu = m_Items[idx].MakeMenu();
					if (menu != null)
					{
						menu.Show(this, new Point(m_Items[idx].Left, MenuHeight));
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
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile();
			jf.SetValue(nameof(MenuFourcusColor), MenuFourcusColor);//Color
			jf.SetValue(nameof(MenuWakuColor), MenuWakuColor);//Color
			jf.SetValue(nameof(MenuWakuEditColor), MenuWakuEditColor);//Color
			jf.SetValue(nameof(Font), Font);//Font
			jf.SetValue(nameof(CanColorCustum), CanColorCustum);//Boolean
			jf.SetValue(nameof(ForcusColor), ForcusColor);//Color
			jf.SetValue(nameof(ForeColor), ForeColor);//Color
			jf.SetValue(nameof(BackColor), BackColor);//Color

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("MenuFourcusColor", typeof(Color).Name);
			if (v != null) MenuFourcusColor = (Color)v;
			v = jf.ValueAuto("MenuWakuColor", typeof(Color).Name);
			if (v != null) MenuWakuColor = (Color)v;
			v = jf.ValueAuto("MenuWakuEditColor", typeof(Color).Name);
			if (v != null) MenuWakuEditColor = (Color)v;
			v = jf.ValueAuto("ForeColor", typeof(Color).Name);
			if (v != null) ForeColor = (Color)v;
			v = jf.ValueAuto("BackColor", typeof(Color).Name);
			if (v != null) BackColor = (Color)v;
			v = jf.ValueAuto("Font", typeof(Font).Name);
			if (v != null) Font = (Font)v;
			v = jf.ValueAuto("CanColorCustum", typeof(Boolean).Name);
			if (v != null) CanColorCustum = (Boolean)v;
			v = jf.ValueAuto("ForcusColor", typeof(Color).Name);
			if (v != null) ForcusColor = (Color)v;


		}
	}
}
