using BRY;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace Hypowered
{
	public partial class HyperBaseForm : Form
	{
		public int Index = -1;
		[Category("Hypowered")]
		public bool Locked { get; set; } = false;

		#region Event

		// ****************************************************************************
		public delegate void ControlChangedHandler(object sender, HyperChangedEventArgs e);
		public event ControlChangedHandler? ControlChanged;
		protected virtual void OnControlChanged(HyperChangedEventArgs e)
		{
			if (ControlChanged != null)
			{
				ControlChanged(this, e);
			}
		}

		// *********************************************************
		public delegate void CreatedControlHandler(object sender, HyperChangedEventArgs e);
		public event CreatedControlHandler? CreatedControl;
		public virtual void OnCreatedControl(HyperChangedEventArgs e)
		{
			if (CreatedControl != null)
			{
				CreatedControl(this, e);
			}
		}
		// *********************************************************
		public delegate void ControlsChangedHandler(object sender, HyperChangedEventArgs e);
		public event ControlsChangedHandler? ControlsChanged;
		protected virtual void OnControlsChangedl(HyperChangedEventArgs e)
		{
			if (ControlsChanged != null)
			{
				ControlsChanged(this, e);
			}
		}
		// *********************************************************
		public delegate void DeletedControlHandler(object sender, HyperChangedEventArgs e);
		public event DeletedControlHandler? DeletetedControl;
		public virtual void OnDeletedControl(HyperChangedEventArgs e)
		{
			if (DeletetedControl != null)
			{
				DeletetedControl(this, e);
			}
		}
		#endregion
		// *********************************************************
		#region Prop
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
		protected bool m_CanEditMode = true;
		[Category("Hypowered")]
		public bool CanEditMode
		{
			get { return m_CanEditMode; }
			set
			{
				m_CanEditMode = value;
				if (m_CanEditMode == false)
				{
					SetIsEditMode(false);
				}
				this.Invalidate();
			}
		}


		public virtual void SetIsEditMode(bool value)
		{
			m_IsEditMode = value;
			if (this.Controls.Count > 0)
			{
				foreach (Control c in this.Controls)
				{
					if (c is HyperControl)
					{
						((HyperControl)c).IsEditMode = m_IsEditMode;
					}
				}
			}
			//ChkControls();
			this.Invalidate();
		}
		protected int m_TargetIndex = -1;
		[Category("Hypowered_Form")]
		public int TargetIndex
		{
			get { return m_TargetIndex; }
			set
			{
				if (m_TargetIndex != value)
				{
					m_TargetIndex = value;
					OnControlChanged(new HyperChangedEventArgs(this, TargetControl));
				}
				this.Invalidate();
			}
		}
		[Category("Hypowered_Form")]
		public new string Name
		{
			get { return base.Name; }
			set { }
		}
		public void SetName(string n)
		{
			base.Name = n;
		}
		[Category("Hypowered_Form")]
		public new bool DoubleBuffered
		{
			get { return base.DoubleBuffered; }
			set
			{
				base.DoubleBuffered = value;
			}
		}
		[Browsable(false)]
		public HyperControl? TargetControl
		{
			get
			{
				HyperControl? ret = null;
				if ((m_TargetIndex >= 0) && (m_TargetIndex < this.Controls.Count))
				{
					if (this.Controls[m_TargetIndex] is HyperControl)
					{
						ret = (HyperControl)this.Controls[m_TargetIndex];
					}
				}
				return ret;
			}
			set
			{
				if(value != null)
				{
					if((value.Index>=0)&&(value.Index< this.Controls.Count))
					{
						if (m_TargetIndex != value.Index)
						{
							m_TargetIndex = value.Index;
							OnControlChanged(new HyperChangedEventArgs(this, TargetControl));
						}
					}
				}
			}
		}
		protected Padding m_FrameWeight = new Padding(1, 1, 1, 1);
		[Category("Hypowered")]
		public Padding FrameWeight
		{
			get { return m_FrameWeight; }
			set { m_FrameWeight = value; this.Invalidate(); }
		}
		[Category("Hypowered_Form")]
		public new Size Size
		{
			get { return base.Size; }
			set { base.Size = value; this.Invalidate(); }
		}
		private Color m_SelectedColor = Color.Red;
		[Category("Hypowered_Color")]
		public Color SelectedColor
		{
			get { return m_SelectedColor; }
			set { m_SelectedColor = value; this.Invalidate(); }
		}
		private Color m_TargetColor = Color.Blue;
		[Category("Hypowered_Color")]
		public Color TargetColor
		{
			get { return m_TargetColor; }
			set { m_TargetColor = value; this.Invalidate(); }
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
		private bool m_CanSetTransparencyKey = false;
		[Category("Hypowered_Color")]
		public bool CanSetTransparencyKey
		{
			get { return m_CanSetTransparencyKey; }
			set 
			{
				m_CanSetTransparencyKey = value; 

				if(m_CanSetTransparencyKey==false)
				{
					base.TransparencyKey = Color.Empty;
				}
				else
				{
					base.TransparencyKey = m_TransparencyKey_Backup;
				}
				this.Invalidate(); 
			}
		}
		private Color m_TransparencyKey_Backup = Color.White;
		[Category("Hypowered_Color")]
		public new Color TransparencyKey
		{
			get { return base.TransparencyKey; }
			set
			{
				m_TransparencyKey_Backup = value;
				if (m_CanSetTransparencyKey)
				{
					base.TransparencyKey = value;
				}
				else
				{
					base.TransparencyKey = Color.Empty;
				}
			}
		}
		// ***********************************************************************************
		[Browsable(false)]
		public new bool KeyPreview
		{
			get { return base.KeyPreview; }
			set { }
		}
		private bool ShouldSerializeKeyPreview()
		{
			return false;
		}
		[Browsable(false)]
		public new IButtonControl AcceptButton
		{
			get { return base.AcceptButton; }
			set { base.AcceptButton = value; }
		}
		[Browsable(false)]
		public new IButtonControl CancelButton
		{
			get { return base.CancelButton; }
			set { base.CancelButton = value; }
		}
		[Browsable(false)]
		public new bool ControlBox
		{
			get { return base.ControlBox; }
			set { base.ControlBox = value; }
		}
		[Browsable(false)]
		public new bool HelpButton
		{
			get { return base.HelpButton; }
			set { base.HelpButton = value; }
		}
		[Browsable(false)]
		public new Icon Icon
		{
			get { return base.Icon; }
			set { base.Icon = value; }
		}
		[Browsable(false)]
		public new bool IsMdiContainer
		{
			get { return base.IsMdiContainer; }
			set { base.IsMdiContainer = value; }
		}
		[Browsable(false)]
		public new MenuStrip MainMenuStrip
		{
			get { return base.MainMenuStrip; }
			set { base.MainMenuStrip = value; }
		}
		[Browsable(false)]
		public new bool MaximizeBox
		{
			get { return base.MaximizeBox; }
			set { base.MaximizeBox = value; }
		}
		[Browsable(false)]
		public new bool MdiChildrenMinimizedAnchorBottom
		{
			get { return base.MdiChildrenMinimizedAnchorBottom; }
			set { base.MdiChildrenMinimizedAnchorBottom = value; }
		}
		[Browsable(false)]
		public new bool MinimizeBox
		{
			get { return base.MinimizeBox; }
			set { base.MinimizeBox = value; }
		}
		[Browsable(false)]
		public new ControlBindingsCollection DataBindings
		{
			get { return base.DataBindings; }
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
		[Browsable(false)]
		public new FormBorderStyle FormBorderStyle
		{
			get { return base.FormBorderStyle; }
			set { base.FormBorderStyle = value; }
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
		[Browsable(false)]
		public new AutoValidate AutoValidate
		{
			get { return base.AutoValidate; }
			set { base.AutoValidate = value; }
		}
		[Browsable(false)]
		public new bool AutoScroll
		{
			get { return base.AutoScroll; }
			set { base.AutoScroll = value; }
		}
		[Browsable(false)]
		public new bool AutoSize
		{
			get { return base.AutoSize; }
			set { base.AutoSize = value; }
		}
		[Browsable(false)]
		public new AutoSizeMode AutoSizeMode
		{
			get { return base.AutoSizeMode; }
			set { base.AutoSizeMode = value; }
		}
		[Browsable(false)]
		public new FormWindowState WindowState
		{
			get { return base.WindowState; }
			set { base.WindowState = value; }
		}
		[Browsable(false)]
		public new Size AutoScrollMargin
		{
			get { return base.AutoScrollMargin; }
			set { base.AutoScrollMargin = value; }
		}
		[Browsable(false)]
		public new Size AutoScrollMinSize
		{
			get { return base.AutoScrollMinSize; }
			set { base.AutoScrollMinSize = value; }
		}
		[Category("Hypowered_Text")]
		public new string Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}
		[Category("Hypowered_Text")]
		public new Font Font
		{
			get { return base.Font; }
			set { base.Font = value; }
		}
		#endregion

		public HyperBaseForm()
		{
			//SetInScript(InScriptBit.None);
			base.KeyPreview = true;
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			SetName( "HyperDilaog");
			FormBorderStyle = FormBorderStyle.None;
			AutoScaleMode = AutoScaleMode.None;
			TransparencyKey = Color.Empty;
			this.SetStyle(
//ControlStyles.Selectable |
//ControlStyles.UserMouse |
ControlStyles.DoubleBuffer |
ControlStyles.UserPaint |
ControlStyles.AllPaintingInWmPaint ,
//ControlStyles.SupportsTransparentBackColor,
true);
			this.UpdateStyles();
			InitializeComponent();

			ChkControls();
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
		}
		public int IndexOfName(string nm)
		{
			int ret = -1;
			if (this.Controls.Count > 0)
			{
				int cnt = 0;
				foreach (Control c in this.Controls)
				{
					if (c.Name == nm)
					{
						ret = cnt;
						break;
					}
					cnt++;
				}
			}
			return ret;
		}
		public ToolStripMenuItem[] GetControlsForMenu(HyperControl? target, System.EventHandler func)
		{
			List<ToolStripMenuItem> list = new List<ToolStripMenuItem>();
			if (this.Controls.Count > 0)
			{
				ChkControls();
				foreach (Control c in this.Controls)
				{
					if (c is not HyperControl) continue;
					HyperControl hc = (HyperControl)c;
					ToolStripMenuItem mi = new ToolStripMenuItem();
					if (target != null)
					{
						mi.Checked = (hc.Index == target.Index);
					}
					mi.Text = hc.Name;
					mi.Tag = (object)hc;
					mi.Click += func;
					list.Add(mi);
				}
			}
			return list.ToArray();
		}
		
		// ***********************************************************************
		public virtual void ChkControls()
		{
			if (this.Controls.Count > 0)
			{
				//番号を割り振る
				int idx = 0;
				foreach (Control c in this.Controls)
				{
					if (c is HyperControl)
					{
						HyperControl h = (HyperControl)c;
						h.SetIndex(idx);
						h.ParentForm= this;
						h.IsEditMode = m_IsEditMode;
					}
					idx++;
				}
			}
		}
		// ***********************************************************************
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

		// ***********************************************************************
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			using (Pen p = new Pen(ForeColor))
			{
				if (m_IsEditMode)
				{
					if (this.Controls.Count > 0)
					{
						foreach (Control c in this.Controls)
						{
							if (c is HyperControl)
							{
								HyperControl h = (HyperControl)c;
								if (h.Index == m_TargetIndex)
								{
									
									p.Color = TargetColor;
									p.Width = 2;
									p.DashStyle = DashStyle.Dot;
									g.DrawRectangle(p, h.Bounds(2));
									p.DashStyle = DashStyle.Solid;
								}
								else if (h.Selected)
								{
									p.Width = 2;
									p.Color = m_SelectedColor;
									p.DashStyle = DashStyle.Dot;
									g.DrawRectangle(p, h.Bounds(2));
									p.DashStyle = DashStyle.Solid;
								}
							}
						}
					}
				}
				p.Width = 1;
				p.Color = ForeColor;
				DrawFrame(g, p, this.ClientRectangle);
				//g.DrawRectangle(p,new Rectangle(0,0,Width-1,Height-1));
			}

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
		// ******************************************************************************
		public Control? FindControl(string name)
		{
			Control[] ret = this.Controls.Find(name, false);
			if (ret.Length > 0)
			{
				return ret[0];
			}
			else
			{
				return null;
			}
		}

		public int FindControlIndex( string name)
		{
			int ret = -1;
			int cnt = 0;
			foreach (Control c in this.Controls)
			{
				if (c is HyperControl)
				{
					if (c.Name == name)
					{
						ret = cnt;
						break;
					}
				}
				cnt++;
			}
			return ret;
		}
		public HyperControl? CreateControl(ControlType ct)
		{
			HyperControl? ctrl = null;
			switch (ct)
			{
				case ControlType.Label:
					ctrl = new HyperLabel();
					break;
				case ControlType.CheckBox:
					ctrl = new HyperCheckBox();
					break;
				case ControlType.TextBox:
					ctrl = new HyperTextBox();
					break;
				case ControlType.Button:
					ctrl = new HyperButton();
					break;
				case ControlType.RadioButton:
					ctrl = new HyperRadioButton();
					break;
				case ControlType.ListBox:
					ctrl = new HyperListBox();
					break;
				case ControlType.DropdownList:
					ctrl = new HyperDropdownList();
					break;
				case ControlType.DriveIcons:
					ctrl = new HyperDriveIcons();
					break;
				case ControlType.DirList:
					ctrl = new HyperDirList();
					break;
				case ControlType.FileList:
					ctrl = new HyperFileList();
					break;
				case ControlType.PictureBox:
					ctrl = new HyperPictureBox();
					break;
				case ControlType.Icon:
					ctrl = new HyperIcon();
					break;
				case ControlType.Design:
					ctrl = new HyperDesign();
					break;
				case ControlType.Html:
					ctrl = new HyperHtml();
					break;
				default:
					break;
			}
			return ctrl;
		}
		// ******************************************************************************
		public bool AddControl(HyperBaseForm? bf, Control ctrl)
		{
			if (bf == null) return false;
			if (ctrl is HyperControl)
			{
				HyperControl hc = (HyperControl)ctrl;
				hc.IsEditMode = bf.IsEditMode;
				hc.ParentForm = bf;
				hc.LocationChanged += bf.Hc_LocationChanged;
				int IsMenu = 0;
				if (bf.Controls.Count > 0)
				{
					if (bf.Controls[0] is HyperMenuBar) IsMenu = 1;
				}
				bf.Controls.Add(hc);
				bf.Controls.SetChildIndex(hc, IsMenu);
				bf.ChkControls();
				return true;
			}
			return false;
		}
		public bool AddControl(HyperBaseForm? bf,ControlType ct, string name, string tx, Font fnt)
		{
			bool ret = false;
			if (bf == null) return ret;
			HyperControl? ctrl = CreateControl(ct);
			if (ctrl != null)
			{
				if (name != "") ctrl.SetName(name);
				if (tx != "") ctrl.Text = tx;
				if (fnt != null) ctrl.Font = fnt;
				if( AddControl(bf,ctrl))
				{
					bf.OnCreatedControl(new HyperChangedEventArgs(bf, ctrl));
				}
			}
			return ret;
		}
		private void Hc_LocationChanged(object? sender, EventArgs e)
		{
			this.Invalidate();
		}
		// ******************************************************************************
		public virtual bool DeleteControl(HyperControl c)
		{
			bool ret = false;
			if(c == null) return ret;
			if (c is HyperMenuBar) return ret;
			this.Controls.Remove(c);
			ChkControls();
			ret = true;
			OnDeletedControl(new HyperChangedEventArgs(this,null));
			return ret;
		}
		// ******************************************************************************
		public HyperControl[]? GetControls()
		{
			List<HyperControl> list = new List<HyperControl>();
			if (this.Controls.Count > 0)
			{
				foreach (Control c in this.Controls)
				{
					if (c is HyperControl)
					{
						HyperControl hc = (HyperControl)c;
						list.Add(hc);
					}
				}
			}
			return list.ToArray();
		}

		// ****************************************************************************
		public bool ControlToUp(HyperControl c)
		{
			if(c.Index<=0) return false;
			if ((c.Index == 1) && (this.Controls[0] is HyperMenuBar)) return false;
			this.Controls.SetChildIndex(c, c.Index-1);
			ChkControls();
			OnControlsChangedl(new HyperChangedEventArgs(this,c));
			return true;
		}
		// ****************************************************************************
		public bool ControlToDown(HyperControl c)
		{
			if (c.Index >= this.Controls.Count-1) return false;
			if (c is HyperMenuBar) return false;
			this.Controls.SetChildIndex(c, c.Index + 1);
			ChkControls();
			OnControlsChangedl(new HyperChangedEventArgs(this, c));
			return true;
		}
		// ****************************************************************************
		public bool ControlToFront(HyperControl c)
		{
			int idx = 0;
			if (this.Controls[0] is HyperMenuBar) idx = 1;
			this.Controls.SetChildIndex(c, idx);
			ChkControls();
			OnControlsChangedl(new HyperChangedEventArgs(this, c));
			return true;
		}
		// ****************************************************************************
		public bool ControlToFloor(HyperControl c)
		{
			if (c.Index >= this.Controls.Count - 1) return false;
			if (c is HyperMenuBar) return false;
			this.Controls.SetChildIndex(c, this.Controls.Count-1);
			ChkControls();
			OnControlsChangedl(new HyperChangedEventArgs(this, c));
			return true;
		}
		// ****************************************************************************
		public void ChkTarget(HyperControl? hc)
		{
			int TI = m_TargetIndex;
			m_isMultSelect = false;
			if (this.Controls.Count > 0)
			{
				if (hc == null)
				{
					m_TargetIndex = -1;
					foreach (Control c in this.Controls)
					{
						if (c is HyperControl)
						{
							HyperControl h = (HyperControl)c;
							h.Selected = false;
						}
					}
				}
				else
				{

					foreach (Control c in this.Controls)
					{
						if (c is HyperControl)
						{
							HyperControl h = (HyperControl)c;

							if (h == hc)
							{
								m_TargetIndex = hc.Index;
								h.Selected = true;
							}
							else
							{
								h.Selected = false;
							}
						}
					}
				}
				this.Invalidate();
			}
			// ********************************************
			// イベントの発生
			if (TI != m_TargetIndex)
			{
				if (m_TargetIndex >= 0)
				{
					OnControlChanged(new HyperChangedEventArgs(this, TargetControl));
				}
			}
		}
		/// <summary>
		/// コントロールが複数選択してるとtrue
		/// </summary>
		private bool m_isMultSelect = false;
		/// <summary>
		/// コントロールの選択状態のチェック
		/// 引数で渡されたコントロールがターゲットになる
		/// </summary>
		/// <param name="hc">選ばれたコントロール　nullなら無選択状態になる</param>
		public void ChkTargetSelected(HyperControl? hc)
		{
			if (hc is HyperMenuBar) return;
			int TI = m_TargetIndex;
			if (this.Controls.Count > 0)
			{
				if (hc == null)
				{
					m_isMultSelect = false;
					m_TargetIndex = -1;
					foreach (Control c in this.Controls)
					{
						if (c is HyperControl)
						{
							HyperControl h = (HyperControl)c;
							h.Selected = false;
						}
					}
					OnControlChanged(new HyperChangedEventArgs(this, null));
				}
				else
				{
					bool IsShift = ((Control.ModifierKeys & Keys.Shift) == Keys.Shift);
					if(IsShift)
					{
						m_isMultSelect = true;
					}

					foreach (Control c in this.Controls)
					{
						if (c is HyperControl)
						{
							HyperControl h = (HyperControl)c;
							if (m_isMultSelect == false)
							{
								h.Selected = false;
							}
							if (h == hc)
							{
								m_TargetIndex = hc.Index;
								h.Selected = true;
							}

						}
					}
				}
				this.Invalidate();
			}
			else
			{
				m_isMultSelect = false;
			}
			// ********************************************
			// イベントの発生
			if (TI != m_TargetIndex)
			{
				OnControlChanged(new HyperChangedEventArgs(this, TargetControl));
			}
		}

		/// <summary>
		/// 選択されたコントロールを同時に動かす
		/// </summary>
		/// 
		
		public void MoveSelected(Point moveP)
		{
			if (m_TargetIndex < 0) return;
			if (this.Controls.Count == 0) return;
			foreach (Control c in this.Controls)
			{
				if (c is HyperControl)
				{
					if (c is HyperMenuBar) continue;
					HyperControl h = (HyperControl)c;
					if (h != null)
					{
						if ((h.Selected)||(h.Index ==m_TargetIndex) )
						{
							try
							{
								int x = h.LocationBack.X + moveP.X;
								int y = h.LocationBack.Y + moveP.Y;
								h.Location = new Point(x, y);
							}
							catch { }
						}
					}
				}
			}
		}
		
		// ****************************************************************************
		private MDPos m_MDPos = MDPos.None;
		private Point m_MDP = new Point(0, 0);
		private Point m_MDLoc = new Point(0, 0);
		private Size m_MDSize = new Size(0, 0);
		public void LocationBackup()
		{
			if(this.Controls.Count> 0)
			{
				foreach(var c in this.Controls)
				{
					if (c is HyperMenuBar) continue;
					if (c is HyperControl)
					{
						HyperControl h = (HyperControl)c;
						h.LocationBack = h.Location;
					}
				}
			}
		}
		public void DoMouseDown(MouseEventArgs e)
		{
			this.OnMouseDown(e);
		}
		public void DoMouseMove(MouseEventArgs e)
		{
			this.OnMouseMove(e);
		}
		public void DoMouseUp(MouseEventArgs e)
		{
			this.OnMouseUp(e);
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (m_IsEditMode) ChkTargetSelected(null);
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				MDPos p = CU.GetMDPosForm(e.X, e.Y, this.Size);
				if ((p != MDPos.None)&&(p!= MDPos.Center))
				{
					m_MDPos = p;
					m_MDP = new Point(e.X+this.Left, e.Y+this.Top);
					m_MDLoc = this.Location;
					if (Locked == false)
					{
						m_MDSize = this.Size;
					}
					return;
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (m_MDPos != MDPos.None)
			{
				int ax = e.X+this.Left - m_MDP.X;
				int ay = e.Y+this.Top - m_MDP.Y;
				switch (m_MDPos)
				{
					case MDPos.BottomRight:
						if (Locked == false)
						{
							this.Size = new Size(
								m_MDSize.Width + ax,
								m_MDSize.Height + ay);
						}
						break;
					case MDPos.Bottom:
						if (Locked == false)
						{
							this.Size = new Size(
								m_MDSize.Width,
								m_MDSize.Height + ay);
						}
						break;
					case MDPos.Right:
						if (Locked == false)
						{
							this.Size = new Size(
								m_MDSize.Width + ax,
								m_MDSize.Height);
						}
						break;
					case MDPos.Left:
						if (Locked == false)
						{
							this.Size = new Size(
								m_MDSize.Width - ax,
								m_MDSize.Height);
							this.Location = new Point(
								m_MDLoc.X + ax,
								m_MDLoc.Y
								);
						}
						break;
					case MDPos.Top:
					default:
						this.Location = new Point(
							m_MDLoc.X + ax,
							m_MDLoc.Y + ay);
						break;
				}
				return;
			}
			base.OnMouseMove(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_MDPos != MDPos.None)
			{
				m_MDPos = MDPos.None;
			}
			base.OnMouseUp(e);
		}
		// ****************************************************************************
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.Refresh();

		}
		public void RelatingFile(string extension)
		{
			string commandline = "\"" + Application.ExecutablePath + "\" \"%1\"";
			string fileType = Application.ProductName + ".0";
			string description = "Hypowered form file";
			string verb = "open";
			string verbDescription = Application.ProductName + "で開く(&O)";
			string iconPath = Application.ExecutablePath;
			int iconIndex = 1;
			Microsoft.Win32.RegistryKey currentUserKey = Microsoft.Win32.Registry.CurrentUser;

			Microsoft.Win32.RegistryKey regkey = currentUserKey.CreateSubKey("Software\\Classes\\" + extension);
			regkey.SetValue("", fileType);
			regkey.Close();
			Microsoft.Win32.RegistryKey typekey = currentUserKey.CreateSubKey("Software\\Classes\\" + fileType);
			typekey.SetValue("", description);
			typekey.Close();

			Microsoft.Win32.RegistryKey verblkey = currentUserKey.CreateSubKey("Software\\Classes\\" + fileType + "\\shell\\" + verb);
			verblkey.SetValue("", verbDescription);
			verblkey.Close();

			Microsoft.Win32.RegistryKey cmdkey = currentUserKey.CreateSubKey("Software\\Classes\\" + fileType + "\\shell\\" + verb + "\\command");
			cmdkey.SetValue("", commandline);
			cmdkey.Close();

			Microsoft.Win32.RegistryKey iconkey = currentUserKey.CreateSubKey("Software\\Classes\\" + fileType + "\\DefaultIcon");
			iconkey.SetValue("", iconPath + "," + iconIndex.ToString());
			iconkey.Close();
		}
		public void UnRelatingFile(string extension)
		{
			string fileType = Application.ProductName + ".0";
			Microsoft.Win32.RegistryKey currentUserKey = Microsoft.Win32.Registry.CurrentUser;
			currentUserKey.DeleteSubKeyTree("Software\\Classes\\" + extension);
			currentUserKey.DeleteSubKeyTree("Software\\Classes\\" + fileType);
		}
		public bool InstallExt()
		{
			RelatingFile(Def.DefaultExt);
			F_W.SHChangeNotify();
			return true;
		}
		public bool UnInstallExt()
		{
			UnRelatingFile(Def.DefaultExt);
			F_W.SHChangeNotify();
			return true;
		}
	}
}
