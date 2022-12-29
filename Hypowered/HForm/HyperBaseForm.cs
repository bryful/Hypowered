using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	
	public class TargetChangedEventArgs : EventArgs
	{
		public int Index;
		public HyperControl? control;
		public TargetChangedEventArgs(int idx, HyperControl? c)
		{
			Index= idx;
			control = c;
		}
	}


	public partial class HyperBaseForm : Form
	{
		#region Event
		// ****************************************************************************
		public delegate void TargetChangedHandler(object sender, TargetChangedEventArgs e);
		public event TargetChangedHandler? TargetChanged;
		protected virtual void OnTargetChanged(TargetChangedEventArgs e)
		{
			if (TargetChanged != null)
			{
				TargetChanged(this, e);
			}
		}
		public event EventHandler? ControlChanged;
		protected virtual void OnControlChanged(EventArgs e)
		{
			if (ControlChanged != null)
			{
				ControlChanged(this, e);
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
		public void SetIsEditMode(bool value)
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
					OnTargetChanged(new TargetChangedEventArgs(m_TargetIndex, TargetControl));
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
			this.SetStyle(
//ControlStyles.Selectable |
//ControlStyles.UserMouse |
ControlStyles.DoubleBuffer |
ControlStyles.UserPaint |
ControlStyles.AllPaintingInWmPaint |
ControlStyles.SupportsTransparentBackColor,
true);
			this.UpdateStyles();
			this.ControlAdded += HyperForm_ControlAdded;
			this.ControlRemoved += HyperForm_ControlRemoved;
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



		// ***********************************************************************
		protected void ChkControls()
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
						//h.HyperForm= this;
						h.IsEditMode = m_IsEditMode;
					}
					idx++;
				}
			}
			OnControlChanged(new EventArgs());
		}
		// ***********************************************************************
		private void HyperForm_ControlAdded(object? sender, ControlEventArgs e)
		{
			SetIsEditMode(m_IsEditMode);
			if (this.Controls.Count > 0)
			{
				foreach (Control c in this.Controls)
				{
					if (c is HyperControl)
					{
						HyperControl hc = (HyperControl)c;
						hc.LocationChanged -=Hc_LocationChanged;
						hc.LocationChanged += Hc_LocationChanged;
					}
				}
			}
		}

		private void Hc_LocationChanged(object? sender, EventArgs e)
		{
			this.Invalidate();
		}

		private void HyperForm_ControlRemoved(object? sender, ControlEventArgs e)
		{
			this.Invalidate();
		}
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
				pw2 = rct.Bottom - (float)m_FrameWeight.Bottom/2;
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
				pw2 = rct.Right - (float)m_FrameWeight.Right/2;
				g.DrawLine(p, pw2, rct.Top, pw2, rct.Bottom);
			}


		}
		// ******************************************************************************
		public Control? FindControl(string name)
		{
			Control[] ret = this.Controls.Find(name,false);
			if(ret.Length>0)
			{
				return ret[0];
			}
			else
			{
				return null;
			}
		}
	
		public int FindControlIndex(string name)
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
		public Control? CreateControl(ControlType ct)
		{
			Control? ctrl = null;
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
				default:
					break;
			}
			return ctrl;
		}
		// ******************************************************************************
		public bool AddControl(ControlType ct,string name,string tx,Font fnt)
		{
			bool ret = false;
			Control? ctrl = CreateControl(ct);
			if (ctrl != null)
			{
				if(name!="") ctrl.Name = name;
				if(tx!="") ctrl.Text = tx;
				if(fnt!=null) ctrl.Font= fnt;
				((HyperControl)ctrl).IsEditMode= this.IsEditMode;
				this.Controls.Add(ctrl);
				//this.Controls.SetChildIndex(ctrl, 1);
				ChkControls();
				//m_Script.InitControls(this.Controls);
			}
			return ret;
		}
		// ******************************************************************************
		public bool RemoveControl(string key)
		{
			bool ret = false;
			Control[] ctrls = this.Controls.Find(key,false);
			if(ctrls.Length>=1)
			{
				this.Controls.Remove(ctrls[0]);
				ChkControls();
				//m_Script.InitControls(this.Controls);
				ret = true;
			}
			return ret;
		}
		// ******************************************************************************
		public HyperControl[] GetControls()
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
					OnTargetChanged(new TargetChangedEventArgs(
						m_TargetIndex,
						TargetControl
						));
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
				OnTargetChanged(new TargetChangedEventArgs(
					m_TargetIndex,
					TargetControl
					));
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
					if (h == null) continue;
					if (h.Index != m_TargetIndex)
					{
						try
						{
							int x = h.ParentLocation.X + pp.Left;
							int y = h.ParentLocation.Y + pp.Top;
							h.Location = new Point(x, y);
						}
						catch { }
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
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			/*
			if(Script_MouseClick!="")
			{
				//ExecuteCode(Script_MouseClick);
			}
			*/
		}
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			/*
			if (Script_KeyPress != "")
			{
				//ExecuteCode(Script_KeyPress);
			}
			*/
		}
		// ****************************************************************************
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.Invalidate();

		}
		// ******************************************************************************
		public void SetEventHandler(Control objControl)
		{
			objControl.MouseDown += (sender, e) => this.OnMouseDown(e);
			objControl.MouseMove += (sender, e) => this.OnMouseMove(e);
			objControl.MouseUp += (sender, e) => this.OnMouseUp(e);

		}
		protected override bool ProcessDialogKey(Keys keyData)
		{
#if DEBUG
			this.Text = String.Format("{0}", keyData.ToString());
#endif

			return base.ProcessDialogKey(keyData);
		}
	}
}
