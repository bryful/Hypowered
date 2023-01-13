using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
using System.Diagnostics;
using System.Collections.Specialized;
using System.Dynamic;

namespace Hypowered
{

    public partial class HyperControl : Control
	{
		[Category("Hypowered")]
		public StringCollection strings { get; set; } = new StringCollection();
		[Category("Hypowered")]
		public ExpandoObject eo { get; set; } = new ExpandoObject();
		private ControlType? m_ControlType = null;
		protected void SetControlType(ControlType? c) { m_ControlType = c; }

		/// <summary>
		/// コントロールのタイプ識別
		/// </summary>
		[Category("Hypowered")]
		public ControlType? ControlType { get { return m_ControlType; } }
		/// <summary>
		/// 移動、リサイズの固定
		/// </summary>
		[Category("Hypowered")]
		public bool Locked { get; set; } = false;

		/// <summary>
		/// コントロールの名前。変更はSetName()を使用
		/// </summary>
		[Category("Hypowered")]
		public new string Name
		{
			get { return base.Name; }
			set {  }
		}
		public void SetName(string n){base.Name = n;}
		protected bool m_IsDrawFocuse = true;
		/// <summary>
		/// フォーカス時の線の描画するかどうか
		/// </summary>
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
		private FileNameEX m_FileName = new FileNameEX();
		[Category("Hypowered")]
		public String FileName
		{
			get { return m_FileName.Path; }
			set
			{
				m_FileName.Path = value;

			}
		}
		/// <summary>
		/// Nameと同じ
		/// </summary>
		[Category("Hypowered")]
		public string ControlName
		{
			get { return base.Name; }
			
		}
		public void SetControlName(string n) { base.Name = n; }
		[Category("Hypowered")]
		public new Point Location
		{
			get { return base.Location; }
			set { base.Location = value; this.Invalidate(); }
		}
		[Category("Hypowered")]
		public new Size Size
		{
			get { return base.Size; }
			set { base.Size = value; this.Invalidate(); }
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
		[Category("Hypowered")]
		protected int m_Index = -1;
		/// <summary>
		/// CHkControlで作成されるインデックス番号
		/// </summary>
		[Category("Hypowered")]
		public int Index
		{
			get { return m_Index; }
		}
		public void SetIndex(int idx) { m_Index = idx; }

		[Category("Hypowered_Text")]
		public new string Text
		{
			get { return base.Text; }
			set { base.Text = value; this.Invalidate(); }
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
		protected object? m_parent = null;
		[Browsable(false)]
		public object? ParentForm
		{
			get { return m_parent; }
			set
			{ m_parent = value; }
		}
		[Browsable(false)]
		public HyperMainForm? MainForm
		{
			get
			{
				if (m_parent is HyperMainForm)
				{
					return (HyperMainForm)m_parent;
				}
				else
				{
					return null;
				}
			}
		}
		/// <summary>
		/// ターゲットのコントロールからの相対位置
		/// </summary>
		public Point LocationBack = new Point(0, 0);
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
		protected string[] m_DragDropItems = new string[0];
		[Browsable(false)]
		public string[] DragDropItems
		{
			get { return m_DragDropItems; }
		}
		protected DragDropFileType m_DragDropFileType = DragDropFileType.None;
		[Category("Hypowered")]
		public DragDropFileType DragDropFileType
		{
			get { return m_DragDropFileType; }
			set
			{
				m_DragDropFileType = value;
				AllowDrop = (m_DragDropFileType != DragDropFileType.None);
			}
		}
		// **************************************************************************
		/*
		 * スクリプト関係
		 */
		// **************************************************************************
		public HyperScriptCode ScriptCode = new HyperScriptCode();

		public void SetInScript(InScriptBit s)
		{
			ScriptCode.SetInScript(s);
		}
		public string GetScriptCode(ScriptKind ist)
		{
			return ScriptCode.GetScriptCode(ist);
		}
		[Category("Hypowered_Script")]
		public InScriptBit InScript
		{
			get { return ScriptCode.InScript; }
		}
		[Browsable(false)]
		public int ScriptCount
		{
			get { return ScriptCode.Count; }
		}
		[Browsable(false)]
		public string[] ValidSprictNames
		{
			get { return ScriptCode.ValidSprictNames; }
		}
		[Category("Hypowered_Script")]
		public ScriptKind[] ScriptKinds
		{
			get { return ScriptCode.ScriptKinds; }
		}
		[Browsable(false)]
		public string Script_MouseClick
		{
			get { return ScriptCode.Script_MouseClick; }
			set { ScriptCode.Script_MouseClick = value; }
		}
		[Browsable(false)]
		public string Script_MouseDoubleClick
		{
			get { return ScriptCode.Script_MouseDoubleClick; }
			set { ScriptCode.Script_MouseDoubleClick = value; }
		}
		[Browsable(false)]
		public string Script_SelectedIndexChanged
		{
			get { return ScriptCode.Script_SelectedIndexChanged; }
			set { ScriptCode.Script_SelectedIndexChanged = value; }
		}
		[Browsable(false)]
		public string Script_CurrentDirChanged
		{
			get { return ScriptCode.Script_CurrentDirChanged; }
			set { ScriptCode.Script_CurrentDirChanged = value; }
		}
		[Browsable(false)]
		public string Script_ValueChanged
		{
			get { return ScriptCode.Script_ValueChanged; }
			set { ScriptCode.Script_ValueChanged = value; }
		}
		[Browsable(false)]
		public string Script_DragDrop
		{
			get { return ScriptCode.Script_DragDrop; }
			set { ScriptCode.Script_DragDrop = value; }
		}
		// **************************************************************************
		// **************************************************************************
		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			if ((drgevent.Data != null)&&(m_DragDropFileType != DragDropFileType.None))
			{
				if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
				{
					drgevent.Effect = DragDropEffects.Copy;
					
				}
			}
			base.OnDragEnter(drgevent);
		}
		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			if ((drgevent.Data != null) && (m_DragDropFileType != DragDropFileType.None))
			{
				m_DragDropItems = new string[0];
				string[] files = (string[])drgevent.Data.GetData(DataFormats.FileDrop);
				List<string> list = new List<string>();
				foreach (string file in files)
				{
					if((m_DragDropFileType== DragDropFileType.FileOnly)
						&&(m_DragDropFileType == DragDropFileType.FileAndDirectory))
					{
						if(File.Exists(file))
						{
							list.Add(file);
						}
					}
					else if ((m_DragDropFileType == DragDropFileType.DirectoryOnly)
						&& (m_DragDropFileType == DragDropFileType.FileAndDirectory))
					{
						if (Directory.Exists(file))
						{
							list.Add(file);
						}
					}

				}
				m_DragDropItems= list.ToArray();
				if((MainForm!=null)&&(Script_DragDrop!=""))
				{
					MainForm.Script.ExecuteCode(Script_DragDrop);
				}
			}
			base.OnDragDrop(drgevent);
		}
		// **************************************************************************
		// **************************************************************************
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
		public HyperControl()
		{
			SetInScript(InScriptBit.None);
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			m_ForcusColor = ColU.ToColor(HyperColor.Forcus);
			m_UnCheckedColor = ColU.ToColor(HyperColor.Dark);
			m_format.Alignment = StringAlignment.Near;
			m_format.LineAlignment = StringAlignment.Center;
			SetName("HyperControl");
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
				if (m_IsDrawFrame)
				{
					Rectangle rr = ReRect(this.ClientRectangle, 2);
					p.Color = ForeColor;
					DrawFrame(g, p, rr);
				}
				//g.DrawRectangle(p, rr);

				if ((this.Focused) && (m_IsDrawFocuse))
				{
					Rectangle r2 = ReRect(this.ClientRectangle, 1);
					p.Color = m_ForcusColor;
					g.DrawRectangle(p, r2);
				}
				DrawEditMode(g, p, sb);
			}
		}
		private Rectangle? m_DrawTarget_Rect = null;
		private int m_DrawTarget_count = 0;
		protected virtual void DrawTarget(Rectangle rct)
		{
			Debug.WriteLine($"DrawTarget Base{m_DrawTarget_count}");
			/*
			if(m_DrawTarget_Rect != null)
			{
				ControlPaint.DrawReversibleFrame(
										(Rectangle)m_DrawTarget_Rect,
										BackColor,
										FrameStyle.Thick);
			}
			*/
			Point tl = this.PointToScreen(rct.Location);
			m_DrawTarget_Rect = new Rectangle(tl, rct.Size);
			ControlPaint.DrawReversibleFrame(
									(Rectangle)m_DrawTarget_Rect,
									BackColor,
									FrameStyle.Thick);
			m_DrawTarget_count++;
		}
		protected virtual void DrawEditMode(Graphics g, Pen p, SolidBrush sb)
		{
			if (m_IsEditMode)
			{
				//ControlPaint.DrawReversibleFrame(new Rectangle(10, 210, 40, 40),Color.White, FrameStyle.Thick);
				sb.Color = m_ForcusColor;
				string s = "Control";
				if (m_ControlType != null)
				{
					s = Enum.GetName(typeof(ControlType), m_ControlType);
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
		public void DrawFrame(Graphics g, Pen p, Rectangle rct)
		{
			float pw2;
			if (m_FrameWeight.Top > 0)
			{
				p.Width = (float)m_FrameWeight.Top;
				pw2 = rct.Top + (float)m_FrameWeight.Top / 2;
				g.DrawLine(p, rct.Left, pw2, rct.Right, pw2);
			}
			if (m_FrameWeight.Bottom > 0)
			{
				p.Width = (float)m_FrameWeight.Bottom;
				pw2 = rct.Bottom - (float)m_FrameWeight.Bottom / 2;
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
		// ****************************************************************************
		protected void ChkTargetSelected()
		{
			if ((this.Parent != null) && (this.Parent is HyperMainForm))
			{
				((HyperMainForm)this.Parent).ChkTargetSelected(this);
			}
		}
		protected void ChkTarget()
		{
			if ((this.Parent != null) && (this.Parent is HyperMainForm))
			{
				((HyperMainForm)this.Parent).ChkTarget(this);
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
		private ToolStripMenuItem? MakeMenuItem(FuncType ft)
		{
			ToolStripMenuItem? ret = null;
			if (MainForm == null) return ret;
			FuncItem? item = MainForm.Funcs.FindFunc(ft.Method.Name);
			if (item == null) return ret;
			ret = new ToolStripMenuItem();
			ret.Text = item.Caption;
			ret.Tag = (object?)item.Func;
			ret.Click += (sender, e) =>
			{
				if (sender == null) return;
				ToolStripMenuItem? m = (ToolStripMenuItem)sender;
				if (m == null) return;
				if ((m.Tag != null) && (m.Tag is FuncType))
				{
					((FuncType)m.Tag)();
				}
			};
			return ret;
		}
		public virtual void ShowCMenu()
		{
			ContextMenuStrip ret = new ContextMenuStrip();
			if (MainForm == null) return;
			List<ToolStripMenuItem> list = new List<ToolStripMenuItem>();
			void AddMI(ToolStripMenuItem? mi)
			{
				if (mi != null) list.Add(mi);
			}

			AddMI(MakeMenuItem(MainForm.ShowEditControl));
			AddMI(MakeMenuItem(MainForm.ControlToFront));
			AddMI(MakeMenuItem(MainForm.ControlToUp));
			AddMI(MakeMenuItem(MainForm.ControlToDown));
			AddMI(MakeMenuItem(MainForm.ControlToFloor));

			ret.Items.AddRange(list.ToArray());
			Point p = Cursor.Position;
			ret.Show(p);
		}
		// ****************************************************************************
		protected MDPos m_MDPos = MDPos.None;
		protected Point m_MDP = new Point(0, 0);
		protected Point m_MDLoc = new Point(0, 0);
		protected Size m_MDSize = new Size(0, 0);
		protected bool m_isMDmove = false;

		public void CallMouseDown(MouseEventArgs e)
		{
			this.OnMouseDown(e);
		}
		public void CallMouseMove(MouseEventArgs e)
		{
			this.OnMouseMove(e);
		}
		public void CallMouseUp(MouseEventArgs e)
		{
			this.OnMouseUp(e);
		}
		public void CallMouseClick(MouseEventArgs e)
		{
			this.OnMouseClick(e);
		}
		public void CallMouseDoubleClick(MouseEventArgs e)
		{
			this.OnMouseDoubleClick(e);
		}
		protected Point MousePos(MouseEventArgs e)
		{
			return new Point(e.X+this.Left, e.Y+this.Top);
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (m_IsEditMode)
			{
				if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
				{
					ChkTargetSelected();

					MDPos p = CU.GetMDPos(e.X, e.Y, this.Size);
					if ((p != MDPos.None) && (Locked == false))
					{
						if (MainForm != null) MainForm.LocationBackup();
						m_MDPos = p;
						m_MDP =  MousePos(e);
						m_MDLoc = this.Location;
						m_MDSize = this.Size;
						return;
					}
				}
				else if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
				{
					ChkTarget();
					ShowCMenu();
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (m_isMDmove == true) return;
			if (m_IsEditMode)
			{
				if (m_MDPos != MDPos.None)
				{
					Point now = MousePos(e);
					int ax = now.X - m_MDP.X;
					int ay = now.Y - m_MDP.Y;
					switch (m_MDPos)
					{
						case MDPos.BottomRight:
							this.Size = new Size(
								m_MDSize.Width + ax,
								m_MDSize.Height + ay);
							break;
						case MDPos.BottomLeft:
							this.Size = new Size(
								m_MDSize.Width - ax,
								m_MDSize.Height + ay);
							this.Location = new Point(
								m_MDLoc.X + ax,
								m_MDLoc.Y
								);
							break;
						case MDPos.TopLeft:
							this.Size = new Size(
								m_MDSize.Width - ax,
								m_MDSize.Height - ay);
							this.Location = new Point(
								m_MDLoc.X + ax,
								m_MDLoc.Y + ay
								);
							break;
						case MDPos.TopRight:
							this.Size = new Size(
								m_MDSize.Width + ax,
								m_MDSize.Height - ay);
							this.Location = new Point(
								m_MDLoc.X,
								m_MDLoc.Y + ay
								);
							break;
						case MDPos.Left:
							this.Size = new Size(
								m_MDSize.Width - ax,
								m_MDSize.Height);
							this.Location = new Point(
								m_MDLoc.X + ax,
								m_MDLoc.Y
								);
							break;
						case MDPos.Right:
							this.Size = new Size(
								m_MDSize.Width + ax,
								m_MDSize.Height);
							break;
						case MDPos.Bottom:
							this.Size = new Size(
								m_MDSize.Width,
								m_MDSize.Height+ay);
							break;
						case MDPos.Top:
							this.Size = new Size(
								m_MDSize.Width,
								m_MDSize.Height - ay);
							this.Location = new Point(
								m_MDLoc.X ,
								m_MDLoc.Y+ay
								);
							break;
						case MDPos.Center:
						default:
							if (this.MainForm != null)
							{
								MainForm.MoveSelected(new Point(ax,ay));
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
				if (MainForm != null)
				{
					MainForm.ShowEditControl();
				}
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
				if ((this.Parent != null) && (this.Parent is HyperMainForm))
				{
					((HyperMainForm)this.Parent).Invalidate();
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
		// ****************************************************************************
		public string LoadFromTextFile(string fn)
		{
			string ret = "";
			if(File.Exists(fn))
			{
				try
				{
					ret = File.ReadAllText(fn);
				}
				catch
				{
					ret = "";
				}
			}
			return ret;

		}
		// ****************************************************************************
		public bool SaveToTextFile(string fn,string s)
		{
			bool ret = false;
			try
			{
				File.WriteAllText(fn, s);
				ret = true;
			}
			catch
			{
				ret = false;
			}
			return ret;
		}
		public void LoadFileToText(string path)
		{
			this.Text = LoadFromTextFile(path);
			if (FileName != path) FileName = path; ;
		}
		public void LoadFileToText()
		{
			if (FileName != null)
			{
				this.Text = LoadFromTextFile(FileName);
			}
		}
		public void SaveFileFromText(string path)
		{
			SaveToTextFile(path, this.Text);
			if (FileName != path) FileName = path; ;
		}
		public void SaveFileFromText()
		{
			if (FileName != "")
			{
				SaveToTextFile(FileName, this.Text);
			}
		}
		// ****************************************************************************

		public virtual JsonObject ToJson()
		{
			JsonObject jo = new JsonObject();
			JsonFile jf = new JsonFile(jo);
			jf.SetValue(nameof(ControlType), ControlType);//Nullable`1
			jf.SetValue(nameof(Name), Name);//String
			jf.SetValue(nameof(Locked), Locked);//Size
			jf.SetValue(nameof(IsDrawFocuse), IsDrawFocuse);//Size
			jf.SetValue(nameof(IsSaveFileName), IsSaveFileName);//Size
			if (IsSaveFileName)
			{
				jf.SetValue(nameof(FileName), FileName);
			}

			jf.SetValue(nameof(Location), Location);//Point
			jf.SetValue(nameof(Size), Size);//Size
			jf.SetValue(nameof(Font), Font);//Font
			jf.SetValue(nameof(Text), Text);//String
			jf.SetValue(nameof(CanColorCustum), CanColorCustum);//Boolean
			jf.SetValue(nameof(ForcusColor), ForcusColor);//Color
			jf.SetValue(nameof(ForeColor), ForeColor);//Color
			jf.SetValue(nameof(BackColor), BackColor);//Color
			jf.SetValue(nameof(UnCheckedColor), UnCheckedColor);//Color
			jf.SetValue(nameof(Script_MouseClick), Script_MouseClick);//String
			jf.SetValue(nameof(Script_MouseDoubleClick), Script_MouseDoubleClick);//String
			jf.SetValue(nameof(Script_SelectedIndexChanged), Script_SelectedIndexChanged);//String
			jf.SetValue(nameof(Script_CurrentDirChanged), Script_CurrentDirChanged);//String
			jf.SetValue(nameof(Script_ValueChanged), Script_ValueChanged);//String
			jf.SetValue(nameof(Script_DragDrop), Script_DragDrop);//String
			jf.SetValue(nameof(FrameWeight), FrameWeight);

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
			object? v = null;
			v = jf.ValueAuto("MyType", typeof(Int32).Name);
			if (v != null) SetControlType((ControlType)v);
			v = jf.ValueAuto("ControlType", typeof(Int32).Name);
			if (v != null) SetControlType((ControlType)v);
			v = jf.ValueAuto("Locked", typeof(Boolean).Name);
			if (v != null) Locked = (bool)v;
			v = jf.ValueAuto("IsDrawFocuse", typeof(Boolean).Name);
			if (v != null) IsDrawFocuse = (bool)v;
			v = jf.ValueAuto("IsSaveFileName", typeof(Boolean).Name);
			if (v != null) IsSaveFileName = (bool)v;
			if (IsSaveFileName)
			{
				v = jf.ValueAuto("FileName", typeof(string).Name);
				if (v != null) FileName = (string)v;
			}
			v = jf.ValueAuto("Name", typeof(String).Name);
			if (v != null) SetName( (String)v);
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
			v = jf.ValueAuto("FrameWeight", typeof(Padding).Name);
			if (v != null) FrameWeight = (Padding)v;

			v = jf.ValueAuto("Script_MouseClick", typeof(String).Name);
			if (v != null) Script_MouseClick = (String)v;
			v = jf.ValueAuto("Script_MouseDoubleClick", typeof(String).Name);
			if (v != null) Script_MouseDoubleClick = (String)v;
			v = jf.ValueAuto("Script_SelectedIndexChanged", typeof(String).Name);
			if (v != null) Script_SelectedIndexChanged = (String)v;
			v = jf.ValueAuto("Script_CurrentDirChanged", typeof(String).Name);
			if (v != null) Script_CurrentDirChanged = (String)v;
			v = jf.ValueAuto("Script_ValueChanged", typeof(String).Name);
			if (v != null) Script_ValueChanged = (String)v;
			v = jf.ValueAuto("Script_DragDrop", typeof(String).Name);
			if (v != null) Script_DragDrop = (String)v;


			v = jf.ValueAuto("TextAligiment", typeof(int).Name);
			if (v != null) TextAligiment = (StringAlignment)v;
			v = jf.ValueAuto("TextLineAligiment", typeof(int).Name);
			if (v != null) TextLineAligiment = (StringAlignment)v;

			v = jf.ValueAuto("AccessibleRole", typeof(int).Name);
			if (v != null) AccessibleRole = (AccessibleRole)v;
			v = jf.ValueAuto("AllowDrop", typeof(Boolean).Name);
			if (v != null) AllowDrop = (Boolean)v;
			v = jf.ValueAuto("Anchor", typeof(int).Name);
			if (v != null) Anchor = (AnchorStyles)v;
			v = jf.ValueAuto("AutoSize", typeof(Boolean).Name);
			if (v != null) AutoSize = (Boolean)v;
			v = jf.ValueAuto("AutoScrollOffset", typeof(Point).Name);
			if (v != null) AutoScrollOffset = (Point)v;
			v = jf.ValueAuto("Capture", typeof(Boolean).Name);
			if (v != null) Capture = (Boolean)v;
			v = jf.ValueAuto("CausesValidation", typeof(Boolean).Name);
			if (v != null) CausesValidation = (Boolean)v;
			v = jf.ValueAuto("Dock", typeof(int).Name);
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
		[Browsable(false)]
		public HyperIcon? asIcon
		{
			get
			{
				HyperIcon? ret = null;
				if (this is HyperIcon) ret = (HyperIcon)this;
				return ret;
			}
		}
		[Browsable(false)]
		public HyperButton? asButton
		{
			get
			{
				HyperButton? ret = null;
				if (this is HyperButton) ret = (HyperButton)this;
				return ret;
			}
		}
		[Browsable(false)]
		public HyperTextBox? asTextBox
		{
			get
			{
				HyperTextBox? ret = null;
				if (this is HyperTextBox) ret = (HyperTextBox)this;
				return ret;
			}
		}
		[Browsable(false)]
		public HyperCheckBox? asCheckBox
		{
			get
			{
				HyperCheckBox? ret = null;
				if (this is HyperCheckBox) ret = (HyperCheckBox)this;
				return ret;
			}
		}
		[Browsable(false)]
		public HyperRadioButton? asRadioButton
		{
			get
			{
				HyperRadioButton? ret = null;
				if (this is HyperRadioButton) ret = (HyperRadioButton)this;
				return ret;
			}
		}
		[Browsable(false)]
		public HyperListBox? asListBox
		{
			get
			{
				HyperListBox? ret = null;
				if (this is HyperListBox) ret = (HyperListBox)this;
				return ret;
			}
		}
		[Browsable(false)]
		public HyperDropdownList? asDropdownList
		{
			get
			{
				HyperDropdownList? ret = null;
				if (this is HyperDropdownList) ret = (HyperDropdownList)this;
				return ret;
			}
		}
		[Browsable(false)]
		public HyperDriveIcons? asDriveIcons
		{
			get
			{
				HyperDriveIcons? ret = null;
				if (this is HyperDriveIcons) ret = (HyperDriveIcons)this;
				return ret;
			}
		}
		[Browsable(false)]
		public HyperDirList? asDirList
		{
			get
			{
				HyperDirList? ret = null;
				if (this is HyperDirList) ret = (HyperDirList)this;
				return ret;
			}
		}
		[Browsable(false)]
		public HyperFileList? asFileList
		{
			get
			{
				HyperFileList? ret = null;
				if (this is HyperFileList) ret = (HyperFileList)this;
				return ret;
			}
		}
		[Browsable(false)]
		public HyperPictureBox? asPictureBox
		{
			get
			{
				HyperPictureBox? ret = null;
				if (this is HyperPictureBox) ret = (HyperPictureBox)this;
				return ret;
			}
		}
		[Browsable(false)]
		public HyperDesign? asDesign
		{
			get
			{
				HyperDesign? ret = null;
				if (this is HyperDesign) ret = (HyperDesign)this;
				return ret;
			}
		}
		[Browsable(false)]
		public HyperHtml? asHtml
		{
			get
			{
				HyperHtml? ret = null;
				if (this is HyperHtml) ret = (HyperHtml)this;
				return ret;
			}
		}
		[Browsable(false)]
		public HyperFootageList? asFootageList
		{
			get
			{
				HyperFootageList? ret = null;
				if (this is HyperFootageList) ret = (HyperFootageList)this;
				return ret;
			}
		}
	}
}
