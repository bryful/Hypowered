using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Hypowered
{
	public partial class HyperBaseForm : Form
	{
		public int Index = -1;
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
		protected virtual void OnCreatedControl(HyperChangedEventArgs e)
		{
			if (CreatedControl != null)
			{
				CreatedControl(this, e);
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
		[Category("Hypowerd_Form")]
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
		[Category("Hypowerd_Form")]
		public new string Name
		{
			get { return base.Name; }
			set
			{
				base.Name = value;
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
		[Category("Hypowerd")]
		public Padding FrameWeight
		{
			get { return m_FrameWeight; }
			set { m_FrameWeight = value; this.Invalidate(); }
		}
		[Category("Hypowerd_Form")]
		public new Size Size
		{
			get { return base.Size; }
			set { base.Size = value; this.Invalidate(); }
		}
		private Color m_SelectedColor = Color.Red;
		[Category("Hypowerd_Color")]
		public Color SelectedColor
		{
			get { return m_SelectedColor; }
			set { m_SelectedColor = value; this.Invalidate(); }
		}
		private Color m_TargetColor = Color.Blue;
		[Category("Hypowerd_Color")]
		public Color TargetColor
		{
			get { return m_TargetColor; }
			set { m_TargetColor = value; this.Invalidate(); }
		}
		private bool m_CanSetTransparencyKey = false;
		[Category("Hypowerd_Color")]
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
		[Category("Hypowerd_Color")]
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
		#endregion

		public HyperBaseForm()
		{
			//SetInScript(InScript.Startup| InScript.MouseClick| InScript.KeyPress);

			base.KeyPreview = true;
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			this.Name = "HyperDilaog";
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


		// ***********************************************************************
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
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
				if (name != "") ctrl.Name = name;
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
		public virtual bool RemoveControl( string key)
		{
			bool ret = false;
			Control[] ctrls = this.Controls.Find(key, false);
			if (ctrls.Length >= 1)
			{
				if (ctrls[0] is HyperMenuBar) return ret;
				this.Controls.Remove(ctrls[0]);
				ChkControls();
				ret = true;
				OnDeletedControl(new HyperChangedEventArgs(this,null));
			}
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
							h.ParentIndex = -1;
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
							h.ParentIndex = -1;
						}
					}
				}
				else
				{
					bool IsShift = ((Control.ModifierKeys & Keys.Shift) == Keys.Shift);
					if (IsShift)
					{
						m_isMultSelect = true;
					}
					else
					{
						if (hc.Selected == false) { m_isMultSelect = false; }
					}

					foreach (Control c in this.Controls)
					{
						if (c is HyperControl)
						{
							HyperControl h = (HyperControl)c;
							if (m_isMultSelect == false)
							{
								h.Selected = false;
								h.ParentIndex = -1;
							}
							if (h == hc)
							{
								m_TargetIndex = hc.Index;
								h.Selected = true;
							}

						}
					}
					// 複数選択の処理
					if (m_isMultSelect)
					{
						foreach (Control c in this.Controls)
						{
							if (c is HyperControl)
							{
								HyperControl h = (HyperControl)c;
								if (h.Selected && (h.Index != hc.Index))
								{
									h.ParentLocation = new Point(h.Left - hc.Left, h.Top - hc.Top);
									h.ParentIndex = hc.Index;
								}
								else
								{
									h.ParentLocation = new Point(0, 0);
									h.ParentIndex = -1;
								}
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
		public void MoveSelected()
		{
			if (m_TargetIndex < 0) return;
			if (m_isMultSelect == false) return;
			if (this.Controls.Count == 0) return;
			HyperControl pp = (HyperControl)this.Controls[m_TargetIndex];
			foreach (Control c in this.Controls)
			{
				if (c is HyperControl)
				{
					if (c is HyperMenuBar) continue;
					HyperControl h = (HyperControl)c;
					if (h != null)
					{
						if (h.Index != m_TargetIndex)
						{
							try
							{
								Point pt = h.ParentLocation;
								int x = pt.X + pp.Left;
								int y = pt.Y + pp.Top;
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
			base.OnMouseDown(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
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
			this.Invalidate();

		}
	}
}
