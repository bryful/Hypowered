using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Hypowered
{
	

	public partial class HyperControl : Control
	{
		private ControlType? m_MyType = null;
		protected void SetMyType(ControlType? c) { m_MyType = c; }

		[Category("Hypowerd")]
		public ControlType? MyType { get { return m_MyType; } }

		[Category("Hypowerd")]
		public new string Name
		{
			get { return base.Name; }
			set { base.Name = value;this.Invalidate(); }
		}
		[Category("Hypowerd")]
		public  string ControlName
		{
			get { return base.Name; }
			set { base.Name = value; this.Invalidate(); }
		}
		[Category("Hypowerd")]
		public new Point Location
		{
			get { return base.Location; }
			set { base.Location = value; this.Invalidate(); }
		}
		[Category("Hypowerd")]
		public new Size Size
		{
			get { return base.Size; }
			set { base.Size = value; this.Invalidate(); }
		}
		[Category("Hypowerd")]
		public new Font Font
		{
			get { return base.Font; }
			set
			{
				base.Font = value;
			}
		}
		[Category("Hypowerd")]
		protected int m_Index = -1;
		[Category("Hypowerd")]
		public int Index
		{
			get { return m_Index; }
		}
		public void SetIndex(int idx) { m_Index = idx; }

		[Category("Hypowerd_Text")]
		public new string Text
		{
			get { return base.Text; }
			set { base.Text = value; this.Invalidate(); }
		}
		[Browsable(false)]
		public HyperForm? HyperForm { get; set; }
			public int ParentIndex = -1;
		/// <summary>
		/// ターゲットのコントロールからの相対位置
		/// </summary>
		public Point ParentLocation = new Point(0,0);
		protected bool m_Selected =false;
		[Browsable(false)]
		public bool Selected
		{
			get { return m_Selected; }
			set { m_Selected = value;}
		}

		protected bool m_IsEditMode = false;
		/// <summary>
		/// 編集モード
		/// </summary>
		[Browsable(false)]
		public bool IsEditMode
		{
			get { return m_IsEditMode; }
			set
			{
				SetIsEditMode(value);
				this.Invalidate();
			}
		}
		public virtual void SetIsEditMode(bool value)
		{
			m_IsEditMode = value;
		}
		/*
		protected int m_GroupID = -1;
		[Category("Hypowerd_Group")]
		public int GroupID { get { return m_GroupID; } set { m_GroupID = value; this.Invalidate(); } }	
		protected int m_Group = -1;
		[Category("Hypowerd_Group")]
		public int Group { get { return m_Group; } set { m_Group = value; this.Invalidate(); } }
		*/
		protected Color m_ForcusColor = Color.White;
		[Category("Hypowerd_Color")]
		public Color ForcusColor
		{
			get { return m_ForcusColor; }
			set { m_ForcusColor = value; this.Invalidate(); }
		}
		[Category("Hypowerd_Color")]
		public new Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; this.Invalidate(); }
		}
		[Category("Hypowerd_Color")]
		public new Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; this.Invalidate(); }
		}
		protected string m_ScriptCode = "";
		[Category("Hypowerd_Script")]
		public string ScriptCode
		{
			get { return m_ScriptCode; }
			set
			{
				m_ScriptCode = value;
			}
		}
		public HyperControl()
		{
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			m_ForcusColor = ColU.ToColor(HyperColor.Forcus);
			this.Name = "HyperControl";
			this.Size = ControlDef.DefSize;
			this.Location = new Point(100, 100);
			InitializeComponent();
			this.SetStyle(
	ControlStyles.Selectable |
	ControlStyles.UserMouse |
	ControlStyles.DoubleBuffer |
	ControlStyles.UserPaint |
	ControlStyles.AllPaintingInWmPaint |
	ControlStyles.SupportsTransparentBackColor,
	true);
			this.UpdateStyles();
		}

		// ****************************************************************************
		protected override void OnPaint(PaintEventArgs pe)
		{
			using(SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = pe.Graphics;
				// 背景色
				g.FillRectangle(sb, this.ClientRectangle);

				// 外枠
				Rectangle rr = ReRect(this.ClientRectangle, 2);
				p.Color = ForeColor;
				g.DrawRectangle(p, rr);
				if(this.Focused)
				{
					rr = ReRect(this.ClientRectangle, 1);
					p.Color = m_ForcusColor;
					g.DrawRectangle(p, rr);
				}
				DrawType(g, sb);
			}
		}
		protected virtual void DrawType(Graphics g ,SolidBrush sb)
		{
			if (m_IsEditMode)
			{
				sb.Color = m_ForcusColor;
				string s = "Control";
				if (m_MyType != null)
				{
					s = Enum.GetName(typeof(ControlType), m_MyType);
				}
				g.DrawString(s, this.Font, sb, this.ClientRectangle);
			}
		}
		// ****************************************************************************
		public RectangleF PenRect(Rectangle r, Pen p)
		{
			float pw = (float)p.Width;
			float pw2 = pw / 2;
			return new RectangleF(r.Left + pw2, r.Top + pw2, r.Width - pw, r.Height - pw);
		}
		public Rectangle ReRect(Rectangle r, int v)
		{
	
			return new Rectangle(r.Left + v, r.Top + v, r.Width - v*2, r.Height - v*2);
		}
		public RectangleF ReRectF(Rectangle r, float v)
		{

			return new RectangleF((float)r.Left + v/2, (float)r.Top + v/2, (float)r.Width - v, (float)r.Height - v);
		}
		// ****************************************************************************
		protected void ChkTargetSelected()
		{
			if((this.Parent !=null)&&(this.Parent is HyperForm))
			{
				((HyperForm)this.Parent).ChkTargetSelected(this);
			}
		}
		// ****************************************************************************
		public new Rectangle Bounds(int sz=0)
		{
			return new Rectangle(
				this.Left-sz,
				this.Top - sz,
				this.Width +sz*2,
				this.Height + sz * 2
				);
		}
		// ****************************************************************************
		protected MDPos m_MDPos = MDPos.None;
		protected Point m_MDP = new Point(0, 0);
		protected Point m_MDLoc = new Point(0, 0);
		protected Size m_MDSize= new Size(0, 0);
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if(m_IsEditMode)
			{
				if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
				{
					ChkTargetSelected();

					MDPos p = CU.GetMDPos(e.X, e.Y,this.Size);
					if (p != MDPos.None)
					{
						m_MDPos = p;
						m_MDP = new Point(e.X, e.Y);
						m_MDLoc = this.Location;
						m_MDSize = this.Size;
						return;
					}
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (m_IsEditMode)
			{
				if (m_MDPos != MDPos.None)
				{
					int ax = e.X - m_MDP.X;
					int ay = e.Y - m_MDP.Y;
					switch(m_MDPos)
					{
						case MDPos.BottomRight:
							this.Size = new Size(
								m_MDSize.Width + ax, 
								m_MDSize.Height + ay);
							break;
						case MDPos.Right:
							this.Size = new Size(
								m_MDSize.Width + ax,
								m_MDSize.Height);
							break;
						case MDPos.Center:
						default:
							this.Location = new Point(
								this.Location.X + ax,
								this.Location.Y + ay);
							if((this.Parent!=null)&&(this.Parent is HyperForm))
							{
								((HyperForm)this.Parent).MoveSelected();
							}
							break;
					}
					return;
				}
			}
			base.OnMouseMove(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_IsEditMode)
			{
				if (m_MDPos != MDPos.None)
				{
					m_MDPos = MDPos.None;
				}
			}
			base.OnMouseUp(e);
		}
		protected override void OnDoubleClick(EventArgs e)
		{
			if (m_IsEditMode)
			{
				if(HyperForm!=null)
				{
					HyperForm.EditControl();
				}
				/*
				HyperScriptEditor ed = new HyperScriptEditor();
				ed.ScriptCode = m_ScriptCode;
				if (ed.ShowDialog() == DialogResult.OK)
				{
					m_ScriptCode = ed.ScriptCode;
					CreateScrits(typeof(App));
				}*/
			}
			else
			{
				base.OnDoubleClick(e);
			}
		}
		// ****************************************************************************
		
		// ****************************************************************************
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.Invalidate();
			if(m_IsEditMode)
			{
				if((this.Parent!=null)&&(this.Parent is HyperForm))
				{
					((HyperForm)this.Parent).Invalidate();
				}
			}

		}
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
	}
}
