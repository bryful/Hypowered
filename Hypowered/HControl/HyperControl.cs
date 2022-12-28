using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Windows.Forms.Layout;
using System.Windows.Documents;

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
			set { base.Name = value; this.Invalidate(); }
		}
		[Category("Hypowerd")]
		public string ControlName
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
		public Point ParentLocation = new Point(0, 0);
		protected bool m_Selected = false;
		[Browsable(false)]
		public bool Selected
		{
			get { return m_Selected; }
			set { m_Selected = value; }
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
		// **************************************************************************
		// **************************************************************************
		protected InScript m_InScript = InScript.None;
		public InScript InScript
		{
			get { return m_InScript; }
		}
		protected int m_ScriptCount = 0;
		public int ScriptCount
		{
			get { return m_ScriptCount; }
		}
		protected string[] m_ScriptNames = new string[0];
		public string[] ScriptNames
		{
			get { return m_ScriptNames; }
		}
		protected string[] m_ScriptCodes = new string[0];
		public string[] ScriptCodes
		{
			get { return m_ScriptCodes; }
			set { m_ScriptCodes= value; }
		}
		protected InScript[] m_InScripts = new InScript[0];
		public InScript[] InScripts { get { return m_InScripts; } }
		public int FindScriptCode(InScript ist)
		{
			int ret = -1;
			for(int i=0; i< m_InScripts.Length; i++)
			{
				if (m_InScripts[i]==ist)
				{
					ret = i;
					break;
				}
			}
			return ret;
		}
		public string GetScriptCode(InScript ist)
		{
			string ret = "";
			int idx = FindScriptCode(ist);
			if(idx >=0)
			{
				ret = m_ScriptCodes[idx];
			}
			return ret;
		}
		public void SetScriptCode(InScript ist,string code)
		{
			string ret = "";
			int idx = FindScriptCode(ist);
			if (idx >= 0)
			{
				m_ScriptCodes[idx] = code;
			}
		}

		protected void SetInScript(InScript sc)
		{
			m_InScript = sc;
			int c = GetScriptCount();
			m_ScriptCodes = new string[c];
			for(int i=0; i<c; i++)
			{
				m_ScriptCodes[i] = "";
			}
			GetScriptNames();
		}
		private string m_Script_MouseClick = "";
		private string m_Script_MouseDoubleClick = "";
		private string m_Script_SelectedIndexChanged = "";
		// **************************************************************************
		protected void GetScriptNames()
		{
			List<string> list = new List<string>();
			List<InScript> slist = new List<InScript>();
			string[] names = Enum.GetNames(typeof(InScript));
			for(int i=0; i<16;i++)
			{
				InScript v = (InScript)(0x01 << i);
				if ( (m_InScript &  v) != 0)
				{
					list.Add(names[i]);
					slist.Add(v);
				}

			}
			m_ScriptNames=  list.ToArray();
			m_InScripts = slist.ToArray();
		}
		// **************************************************************************
		protected int GetScriptCount()
		{
			int ret = 0;
			for (int i = 0; i < 16; i++)
			{
				if ((((int)m_InScript >> i) & 0x01) == 0x01)
				{
					ret++;
				}

			}
			return ret;
		}
		// **************************************************************************
		// **************************************************************************
		protected bool m_CanColorCustum = false;
		[Category("Hypowerd_Color")]
		public bool CanColorCustum
		{
			get { return m_CanColorCustum; }
			set { m_CanColorCustum = value; }
		}
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
		protected Color m_UnCheckedColor = Color.White;
		[Category("Hypowerd_Color")]
		public Color UnCheckedColor
		{
			get { return m_UnCheckedColor; }
			set { m_UnCheckedColor = value; this.Invalidate(); }
		}
		protected StringFormat m_format = new StringFormat();
		[Category("Hypowerd_Text")]
		public StringAlignment TextAligiment
		{
			get { return m_format.Alignment; }
			set { m_format.Alignment = value; this.Invalidate(); }
		}
		[Category("Hypowerd_Text")]
		public StringAlignment TextLineAligiment
		{
			get { return m_format.LineAlignment; }
			set { m_format.LineAlignment = value; this.Invalidate(); }
		}
		public HyperControl()
		{

			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			m_ForcusColor = ColU.ToColor(HyperColor.Forcus);
			m_UnCheckedColor = ColU.ToColor(HyperColor.Dark);
			m_format.Alignment = StringAlignment.Near;
			m_format.LineAlignment = StringAlignment.Center;

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
			using (SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = pe.Graphics;
				// 背景色
				g.FillRectangle(sb, this.ClientRectangle);

				// 外枠
				Rectangle rr = ReRect(this.ClientRectangle, 2);
				p.Color = ForeColor;
				g.DrawRectangle(p, rr);
				if (this.Focused)
				{
					rr = ReRect(this.ClientRectangle, 1);
					p.Color = m_ForcusColor;
					g.DrawRectangle(p, rr);
				}
				DrawType(g, sb);
			}
		}
		protected virtual void DrawType(Graphics g, SolidBrush sb)
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

			return new Rectangle(r.Left + v, r.Top + v, r.Width - v * 2, r.Height - v * 2);
		}
		public RectangleF ReRectF(Rectangle r, float v)
		{

			return new RectangleF((float)r.Left + v / 2, (float)r.Top + v / 2, (float)r.Width - v, (float)r.Height - v);
		}
		// ****************************************************************************
		protected void ChkTargetSelected()
		{
			if ((this.Parent != null) && (this.Parent is HyperForm))
			{
				((HyperForm)this.Parent).ChkTargetSelected(this);
			}
		}
		// ****************************************************************************
		public new Rectangle Bounds(int sz = 0)
		{
			return new Rectangle(
				this.Left - sz,
				this.Top - sz,
				this.Width + sz * 2,
				this.Height + sz * 2
				);
		}
		// ****************************************************************************
		protected MDPos m_MDPos = MDPos.None;
		protected Point m_MDP = new Point(0, 0);
		protected Point m_MDLoc = new Point(0, 0);
		protected Size m_MDSize = new Size(0, 0);
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (m_IsEditMode)
			{
				if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
				{
					ChkTargetSelected();

					MDPos p = CU.GetMDPos(e.X, e.Y, this.Size);
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
					switch (m_MDPos)
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
							if ((this.Parent != null) && (this.Parent is HyperForm))
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
				if (HyperForm != null)
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
			if (m_IsEditMode)
			{
				if ((this.Parent != null) && (this.Parent is HyperForm))
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

		public virtual JsonObject ToJson()
		{
			JsonObject jo = new JsonObject();
			JsonFile jf = new JsonFile(jo);
			jf.SetValue(nameof(MyType), MyType);//Nullable`1
			jf.SetValue(nameof(Name), Name);//String
			jf.SetValue(nameof(Location), Location);//Point
			jf.SetValue(nameof(Size), Size);//Size
			jf.SetValue(nameof(Font), Font);//Font
			jf.SetValue(nameof(Text), Text);//String
			jf.SetValue(nameof(CanColorCustum), CanColorCustum);//Boolean
			jf.SetValue(nameof(ForcusColor), ForcusColor);//Color
			jf.SetValue(nameof(ForeColor), ForeColor);//Color
			jf.SetValue(nameof(BackColor), BackColor);//Color
			jf.SetValue(nameof(UnCheckedColor), UnCheckedColor);//Color
			jf.SetValue(nameof(ScriptCodes), ScriptCodes);//String
			jf.SetValue(nameof(TextAligiment), TextAligiment);//StringAlignment
			jf.SetValue(nameof(TextLineAligiment), TextLineAligiment);//StringAlignment
			jf.SetValue(nameof(AllowDrop), AllowDrop);//Boolean
			jf.SetValue(nameof(Anchor), Anchor);//AnchorStyles
			jf.SetValue(nameof(AutoSize), AutoSize);//Boolean
			jf.SetValue(nameof(AutoScrollOffset), AutoScrollOffset);//Point
			jf.SetValue(nameof(CanFocus), CanFocus);//Boolean
			jf.SetValue(nameof(CanSelect), CanSelect);//Boolean
			jf.SetValue(nameof(CausesValidation), CausesValidation);//Boolean
			jf.SetValue(nameof(ContainsFocus), ContainsFocus);//Boolean
			jf.SetValue(nameof(Dock), Dock);//DockStyle
			jf.SetValue(nameof(Enabled), Enabled);//Boolean
			jf.SetValue(nameof(IsHandleCreated), IsHandleCreated);//Boolean
			jf.SetValue(nameof(InvokeRequired), InvokeRequired);//Boolean
			jf.SetValue(nameof(IsMirrored), IsMirrored);//Boolean
			jf.SetValue(nameof(Margin), Margin);//Padding
			jf.SetValue(nameof(MaximumSize), MaximumSize);//Size
			jf.SetValue(nameof(MinimumSize), MinimumSize);//Size
			jf.SetValue(nameof(TabIndex), TabIndex);//Int32
			jf.SetValue(nameof(TabStop), TabStop);//Boolean
			jf.SetValue(nameof(Top), Top);//Int32
			jf.SetValue(nameof(UseWaitCursor), UseWaitCursor);//Boolean
			jf.SetValue(nameof(Visible), Visible);//Boolean
			jf.SetValue(nameof(Padding), Padding);//Padding
			jf.SetValue(nameof(ImeMode), ImeMode);//ImeMode

			return jf.Obj;
		}
		public virtual string ToJsonCode()
		{
			return ToJson().ToJsonString();
		}
		public virtual void FromJson(JsonObject jo)
		{
			JsonFile jf = new JsonFile(jo);
			object? v=null;
			v = jf.ValueAuto("MyType", typeof(Int32).Name);
			if (v != null) SetMyType ( (ControlType)v);
			v = jf.ValueAuto("Name", typeof(String).Name);
			if (v != null) Name = (String)v;
			v = jf.ValueAuto("Location", typeof(Point).Name);
			if (v != null) Location = (Point)v;
			v = jf.ValueAuto("Size", typeof(Size).Name);
			if (v != null) Size = (Size)v;
			v = jf.ValueAuto("Font", typeof(Font).Name);
			if (v != null) Font = (Font)v;
			v = jf.ValueAuto("Text", typeof(String).Name);
			if (v != null) Text = (String)v;
			v = jf.ValueAuto("CanColorCustum", typeof(Boolean).Name);
			if (v != null) CanColorCustum = (Boolean)v;
			v = jf.ValueAuto("ForcusColor", typeof(Color).Name);
			if (v != null) ForcusColor = (Color)v;
			v = jf.ValueAuto("ForeColor", typeof(Color).Name);
			if (v != null) ForeColor = (Color)v;
			v = jf.ValueAuto("BackColor", typeof(Color).Name);
			if (v != null) BackColor = (Color)v;
			v = jf.ValueAuto("UnCheckedColor", typeof(Color).Name);
			if (v != null) UnCheckedColor = (Color)v;
			v = jf.ValueAuto("ScriptCode", typeof(String[]).Name);
			if (v != null) ScriptCodes = (String[])v;
			v = jf.ValueAuto("TextAligiment", typeof(StringAlignment).Name);
			if (v != null) TextAligiment = (StringAlignment)v;
			v = jf.ValueAuto("TextLineAligiment", typeof(StringAlignment).Name);
			if (v != null) TextLineAligiment = (StringAlignment)v;
			if (v != null) AccessibleRole = (AccessibleRole)v;
			v = jf.ValueAuto("AllowDrop", typeof(Boolean).Name);
			if (v != null) AllowDrop = (Boolean)v;
			v = jf.ValueAuto("Anchor", typeof(AnchorStyles).Name);
			if (v != null) Anchor = (AnchorStyles)v;
			v = jf.ValueAuto("AutoSize", typeof(Boolean).Name);
			if (v != null) AutoSize = (Boolean)v;
			v = jf.ValueAuto("AutoScrollOffset", typeof(Point).Name);
			if (v != null) AutoScrollOffset = (Point)v;
			v = jf.ValueAuto("Capture", typeof(Boolean).Name);
			if (v != null) Capture = (Boolean)v;
			v = jf.ValueAuto("CausesValidation", typeof(Boolean).Name);
			if (v != null) CausesValidation = (Boolean)v;
			v = jf.ValueAuto("Dock", typeof(DockStyle).Name);
			if (v != null) Dock = (DockStyle)v;
			v = jf.ValueAuto("Enabled", typeof(Boolean).Name);
			if (v != null) Enabled = (Boolean)v;
			v = jf.ValueAuto("Margin", typeof(Padding).Name);
			if (v != null) Margin = (Padding)v;
			v = jf.ValueAuto("MaximumSize", typeof(Size).Name);
			if (v != null) MaximumSize = (Size)v;
			v = jf.ValueAuto("MinimumSize", typeof(Size).Name);
			if (v != null) MinimumSize = (Size)v;
			v = jf.ValueAuto("TabIndex", typeof(Int32).Name);
			if (v != null) TabIndex = (Int32)v;
			v = jf.ValueAuto("TabStop", typeof(Boolean).Name);
			if (v != null) TabStop = (Boolean)v;
			v = jf.ValueAuto("UseWaitCursor", typeof(Boolean).Name);
			if (v != null) UseWaitCursor = (Boolean)v;
			v = jf.ValueAuto("Visible", typeof(Boolean).Name);
			if (v != null) Visible = (Boolean)v;
			v = jf.ValueAuto("Padding", typeof(Padding).Name);
			if (v != null) Padding = (Padding)v;
			v = jf.ValueAuto("ImeMode", typeof(ImeMode).Name);
			if (v != null) ImeMode = (ImeMode)v;

		}
	}
}
