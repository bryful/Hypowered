using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hpd
{
    public enum HpdType
	{
		None = 0,
		Button,
		ComboBox,
		TextBox,
		Panel,
		ControlTree,
	}
	public enum MDPos
	{
		None =-1,
		TopLeft=0,
		Top,
		TopRight,
		Left,
		Center,
		Right,
		BottomLeft,
		Bottom,
		BottomRight
	}

	public partial class HpdControl : Control
	{
		static public void PropListToClipboard(Type t, string nm)
		{
			string s = "";
			foreach (MemberInfo mi in t.GetMembers())
			{
				if (mi.MemberType == MemberTypes.Event)
				{
					s += $"//{nm}.{mi.Name}+=(sender,e)=>{{ On{mi.Name}(e);}};\r\n";
				}
			}
			s += "********************************\r\n";
			foreach (var pi in t.GetProperties())
			{

				s += $"[Category(\"Hypowered\")]\r\n";
				s += $"public {pi.PropertyType.FullName} {pi.Name}\r\n";
				s += $"{{\r\n";
				s += $"\tget {{ return {nm}.{pi.Name}; }}\r\n";
				s += $"\tset {{ {nm}.{pi.Name} = value; }}\r\n";
				s += $"}}\r\n";
			}


			Clipboard.SetText(s);
		}
		static public HpdControl CreateControl(
			string name,
			string txt
			, HpdType ht)
		{
			HpdControl ret;
			switch (ht)
			{
				case HpdType.Panel:
					HpdPanel hp = new HpdPanel();
					hp.Name = name;
					hp.Text = txt;
					hp.Size = new Size(120, 300);
					hp.Location = new Point(80, 80);
					hp.MaximumSize = new Size(0, 0);
					hp.Size = hp.PreferredSize;
					ret = (HpdControl)hp;
					break;
				case HpdType.TextBox:
					HpdTextBox htb = new HpdTextBox();
					htb.Name = name;
					htb.Text = txt;
					htb.Size = new Size(120, 23);
					htb.Location = new Point(80, 100);
					htb.MaximumSize = new Size(0, 0);
					htb.Size = htb.PreferredSize;
					ret = (HpdControl)htb;
					break;
				case HpdType.Button:
				default:
					HpdButton hb = new HpdButton();
					hb.Name = name;
					hb.Text = txt;
					hb.Size = new Size(120, 23);
					hb.Location = new Point(100, 100);
					hb.MaximumSize = new Size(0, 0);
					hb.Size = hb.PreferredSize;
					ret = (HpdControl)hb;
					break;
			}
			return ret;
		}
		public delegate void NameChangedHandler(object sender, EventArgs e);
		public event NameChangedHandler? NameChanged;
		protected virtual void OnNameChanged(EventArgs e)
		{
			if (NameChanged != null)
			{
				NameChanged(this, e);
			}
		}
		
		#region Prop
		[Category("Hypowered")]
		public HpdScriptCode ScriptCode { get; set; }= new HpdScriptCode();

		protected HpdForm? m_Root = null;
		[Category("Hypowered")]
		public HpdForm? Root
		{
			get
			{
				Control? ret = m_Root;
				if (m_Root == null)
				{
					ret  = (Control?)this.Parent;
					while ((ret != null) && (ret.Parent != null))
					{
						if (ret is HpdForm) break;
						ret = ret.Parent;
					}
					m_Root = (HpdForm?)ret;
				}

				return m_Root;
			}

		}
		protected HpdType m_HpdType = HpdType.None;
		[Category("Hypowered")]
		public HpdType HpdType { get { return m_HpdType; } }
		public virtual void SetHpdType(HpdType ht) { m_HpdType = ht; }

		protected int m_Index = -1;
		[Category("Hypowered")]
		public int Index { get { return m_Index; } }
		public void SetIndex(int v) { m_Index = v;if (m_Index < 0) m_Index = -1; }
		protected bool m_Selected = false;
		[Category("Hypowered")]
		public bool Selected { get { return m_Selected; } }
		public  void SetSelected(bool b) { m_Selected = b; }
		protected bool m_IsEdit = false;
		[Category("Hypowered")]
		public bool IsEdit { get { return m_IsEdit; } }
		public virtual void SetIsEdit(bool b) { m_IsEdit = b; }
		[Category("Hypowered")]
		protected bool Locked { get; set; } = false;

		[Category("Hypowered"), Browsable(true)]
		public new string Name
		{
			get { return base.Name; }
			set 
			{
				if (base.Name != value)
				{
					base.Name = value;
					OnNameChanged(new EventArgs());
				}
			}
		}

		[Category("Hypowered"), Browsable(true)]
		public new string Text
		{
			get { return base.Text; }
			set 
			{
				base.Text = value; 
				this.Invalidate(); 
			}
		}
		/// <summary>
		/// Textを配列として
		/// </summary>
		[Category("Hypowered_Text")]
		public string[] Lines
		{
			get { return base.Text.Split("\r\n"); }
			set
			{
				base.Text = string.Join("\r\n", value);
				this.Invalidate();
			}
		}
		protected bool m_IsDrawFocuse = true;
		[Category("Hypowered")]
		public bool IsDrawFocuse
		{
			get { return m_IsDrawFocuse; }
			set { m_IsDrawFocuse = value; this.Invalidate(); }
		}
		protected bool m_IsDrawFrame = true;
		/// <summary>
		/// 基本枠を描画するかどうか
		/// </summary>
		[Category("Hypowered")]
		public bool IsDrawFrame
		{
			get { return m_IsDrawFrame; }
			set { m_IsDrawFrame = value; this.Invalidate(); }
		}
		protected bool m_IsSaveFileName = false;
		/// <summary>
		/// ファイル名を保存するかどうか.
		/// </summary>
		[Category("Hypowered")]
		public bool IsSaveFileName
		{
			get { return m_IsSaveFileName; }
			set { m_IsSaveFileName = value; }
		}
		private string m_FileName = "";
		[Category("Hypowered")]
		public String FileName
		{
			get { return m_FileName; }
			set
			{
				m_FileName = value;

			}
		}
		[Category("Hypowered_layout")]
		public new Point Location
		{
			get { return base.Location; }
			set { base.Location = value; this.Invalidate(); }
		}
		[Category("Hypowered_layout")]
		public new Size Size
		{
			get { return base.Size; }
			set 
			{ 
				bool b= (base.Size != value);
				base.Size = value;
				int w = this.m_SizeDef.Width;
				if (m_Algnment!= HpdAlgnment.Fill)
				{
					w = value.Width;
				}
				int h = this.m_SizeDef.Height;
				if (m_LineAlgnment != HpdAlgnment.Fill)
				{
					h = value.Height;
				}
				m_SizeDef= new Size(w, h);
				if (b) { if (Root != null) Root.AutoLayout(); }
				this.Invalidate();
			}
		}
		public void SetSize(Size sz) { base.Size= sz; }
		public void SetWidth(int w) { base.Width = w; }
		public void SetHeight(int h) { base.Height = h; }
		public new int Width
		{
			get { return base.Width; }
			set
			{
				bool b = (base.Width != value);
				base.Width = value;
				if(m_Algnment != HpdAlgnment.Fill) m_SizeDef.Width = value;
				if (b) { if (Root != null) Root.AutoLayout(); }
			}
		}
		public new int Height
		{
			get { return base.Height; }
			set
			{
				bool b = (base.Height != value);
				base.Height = value;
				if (m_LineAlgnment != HpdAlgnment.Fill) m_SizeDef.Height = value;
				if (b) { if (Root != null) Root.AutoLayout(); }
			}
		}
		protected Size m_SizeDef = new Size(0, 0);
		[Category("Hypowered_layout"), Browsable(false)]
		public Size SizeDef
		{
			get { return m_SizeDef; }
			set { m_SizeDef = value; }
		}
		public void PopSizeDef() { base.Size = m_SizeDef; }
		protected HpdAlgnment m_Algnment = HpdAlgnment.Near;
		[Category("Hypowered_layout")]
		public HpdAlgnment Algnment
		{
			get { return m_Algnment; }
			set
			{
				bool b = (m_Algnment != value);
				if( (b)&&(m_Algnment== HpdAlgnment.Fill)) SetSize(m_SizeDef);
				m_Algnment = value;
				if ((b)&&(Root != null)) Root.AutoLayout();
			}
		}

		protected HpdAlgnment m_LineAlgnment = HpdAlgnment.Near;
		[Category("Hypowered_layout")]
		public HpdAlgnment LineAlgnment
		{
			get { return m_LineAlgnment; }
			set
			{
				bool b = (m_LineAlgnment != value);
				if ((b) && (m_LineAlgnment == HpdAlgnment.Fill)) SetSize(m_SizeDef);
				m_LineAlgnment = value;
				if (Root != null) Root.AutoLayout();
			}
		}
		[Category("Hypowered_Text")]
		public new Font Font
		{
			get { return base.Font; }
			set
			{
				base.Font = value;
			}
		}
		protected Padding m_FrameWeight = new Padding(1, 1, 1, 1);
		/// <summary>
		/// フレームの太さ
		/// </summary>
		[Category("Hypowered")]
		public Padding FrameWeight
		{
			get { return m_FrameWeight; }
			set { m_FrameWeight = value; this.Invalidate(); }
		}
		protected bool m_CanColorCustum = false;
		[Category("Hypowered_Color")]
		public bool CanColorCustum
		{
			get { return m_CanColorCustum; }
			set { m_CanColorCustum = value; }
		}
		protected Color m_ForcusColor = Color.White;
		[Category("Hypowered_Color")]
		public Color ForcusColor
		{
			get { return m_ForcusColor; }
			set { m_ForcusColor = value; this.Invalidate(); }
		}
		[Category("Hypowered_Color")]
		public new Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; this.Invalidate(); }
		}
		[Category("Hypowered_Color")]
		public new Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; this.Invalidate(); }
		}
		protected Color m_UnCheckedColor = Color.White;
		[Category("Hypowered_Color")]
		public Color UnCheckedColor
		{
			get { return m_UnCheckedColor; }
			set { m_UnCheckedColor = value; this.Invalidate(); }
		}
		protected StringFormat m_format = new StringFormat();
		[Category("Hypowered_Text")]
		public StringAlignment TextAligiment
		{
			get { return m_format.Alignment; }
			set { m_format.Alignment = value; this.Invalidate(); }
		}
		[Category("Hypowered_Text")]
		public StringAlignment TextLineAligiment
		{
			get { return m_format.LineAlignment; }
			set { m_format.LineAlignment = value; this.Invalidate(); }
		}

		[Browsable(false)]
		public new System.Windows.Forms.ControlBindingsCollection DataBindings
		{
			get { return base.DataBindings; }
		}
		[Browsable(false)]
		public new System.Drawing.Image? BackgroundImage
		{
			get { return base.BackgroundImage; }
			set { base.BackgroundImage = value; }
		}
		[Browsable(false)]
		public new ImageLayout BackgroundImageLayout
		{
			get { return base.BackgroundImageLayout; }
			set { base.BackgroundImageLayout = value; }
		}
		[Browsable(false)]
		public new ContextMenuStrip ContextMenuStrip
		{
			get { return base.ContextMenuStrip; }
			set { base.ContextMenuStrip = value; }
		}
		[Category("Hypowered")]
		public new Object? Tag
		{
			get { return base.Tag; }
			set { base.Tag = value; }
		}
		[Browsable(false)]
		public new Cursor Cursor
		{
			get { return base.Cursor; }
			set { base.Cursor = value; }
		}
		[Browsable(false)]
		public new bool CausesValidation
		{
			get { return base.CausesValidation; }
			set { base.CausesValidation = value; }
		}
		[Browsable(false)]
		public new string AccessibleDescription
		{
			get { return base.AccessibleDescription; }
			set { base.AccessibleDescription = value; }
		}
		[Browsable(false)]
		public new string AccessibleName
		{
			get { return base.AccessibleName; }
			set { base.AccessibleName = value; }
		}
		[Browsable(false)]
		public new AccessibleRole AccessibleRole
		{
			get { return base.AccessibleRole; }
			set { base.AccessibleRole = value; }
		}
		#endregion
		public HpdControl()
		{
			ScriptCode.SetSTypes(ScriptTypeBit.None);
			this.Size= new Size(80, 36);
			this.SizeDef = this.Size;
			//Margin
			//base.BackColor = Color.FromArgb(32, 32, 32);
			//base.ForeColor = Color.FromArgb(220, 220, 220);
			this.Size= new Size(100, 23);
			this.SetStyle(
				ControlStyles.Selectable |
				ControlStyles.UserMouse |
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.UpdateStyles();
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			if (m_IsEdit)
			{
				using (Pen p = new Pen(ForeColor))
				using (SolidBrush sb = new SolidBrush(BackColor))
				{
					Graphics g = pe.Graphics;
					g.FillRectangle(sb, this.ClientRectangle);


					string? n = Enum.GetName<HpdType>(m_HpdType);
					if(n != null)
					{
						StringFormat sf = new StringFormat();
						sf.Alignment= StringAlignment.Near;
						sf.LineAlignment= StringAlignment.Near;
						sb.Color = ForeColor;
						g.DrawString(n, this.Font, sb, this.ClientRectangle, sf);
					}
					Rectangle r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
					g.DrawRectangle(p,r);
				}
			}
		}
		protected MDPos m_mdpos = MDPos.None;
		protected Point m_mdlocg = new Point(0,0);
		protected Point m_mdloc = new Point(0, 0);
		protected Size m_mdsize = new Size(0, 0);

		private MDPos GetPos(MouseEventArgs e)
		{
			MDPos ret = MDPos.None;
			int w = 10;
			int h = 10;

			if(e.Y<h)
			{
				if (e.Y < w)
				{
					ret = MDPos.TopLeft;
				}else if(e.Y>this.Width-w)
				{
					ret = MDPos.TopRight;
				}
				else
				{
					ret = MDPos.Top;
				}
			}else if (e.Y> this.Height - h) 
			{
				if (e.Y < w)
				{
					ret = MDPos.BottomLeft;
				}
				else if (e.Y > this.Width - w)
				{
					ret = MDPos.BottomRight;
				}
				else
				{
					ret = MDPos.BottomRight;
				}
			}
			else
			{
				if (e.Y < w)
				{
					ret = MDPos.Left;
				}
				else if (e.Y > this.Width - w)
				{
					ret = MDPos.Right;
				}
				else
				{
					ret = MDPos.Center;
				}
			}
			return ret;
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if(m_IsEdit)
			{
				if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
				{
					m_mdpos = GetPos(e);
					if (m_mdpos != MDPos.None)
					{
						m_mdlocg = new Point(e.X + this.Left, e.Y + this.Top);
						m_mdloc = this.Location;
						m_mdsize = this.Size;
					}
				}
				else if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
				{

				}
			}
			else
			{
				base.OnMouseDown(e);
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if(m_mdpos!= MDPos.None)
			{
				int ax = (e.X + this.Left) -m_mdlocg.X;
				int ay = (e.Y + this.Top) - m_mdlocg.Y;
				switch (m_mdpos)
				{
					case MDPos.BottomRight:
						this.Size = new Size(
							m_mdsize.Width + ax,
							m_mdsize.Height + ay
							);
						break;
					case MDPos.Center:
						this.Location = new Point( m_mdloc.X + ax, m_mdloc.Y + ay );
						break;
				}
				this.Invalidate();
			}
			else
			{
				base.OnMouseMove(e);
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_mdpos != MDPos.None)
			{
				m_mdpos = MDPos.None;
				this.Invalidate();
			}

			base.OnMouseUp(e);
		}
	}
}
