using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{

	public partial class HForm : BaseForm
	{
		 // TODO: フォーム位置が変な感じに表示される。
		#region Event
		// ****************
		public delegate void FormNameChangedHandler(object sender, FormChangedEventArgs e);
		public event FormNameChangedHandler? FormNameChanged;
		protected virtual void OnFormNameChange(FormChangedEventArgs e)
		{
			if (FormNameChanged != null)
			{
				FormNameChanged(this, e);
			}
		}
		// ****************************************
		public delegate void ControlNameChangedHandler(object sender, ControlChangedEventArgs e);
		public event ControlNameChangedHandler? ControlNameChanged;
		protected virtual void OnControlNameChanged(ControlChangedEventArgs e)
		{
			if (ControlNameChanged != null)
			{
				ControlNameChanged(this, e);
			}
		}
		// ****************
		public delegate void SelectedChangeHandler(object sender, SelectedChangedEventArgs e);
		public event SelectedChangeHandler? SelectedChanged;
		protected virtual void OnSelectedArrayChanged(SelectedChangedEventArgs e)
		{
			if (SelectedChanged != null)
			{
				SelectedChanged(this, e);
			}
		}
		// ****************
		public delegate void SelectedArrayChangeHandler(object sender, SelectedArrayChangedEventArgs e);
		public event SelectedArrayChangeHandler? SelectedArrayChanged;
		protected virtual void OnSelectedArrayChanged(SelectedArrayChangedEventArgs e)
		{
			if (SelectedArrayChanged != null)
			{
				SelectedArrayChanged(this, e);
			}
		}
		// ****************
		public delegate void ControlChangedHandler(object sender, EventArgs e);
		public event ControlChangedHandler? ControlChanged;
		protected virtual void OnControlChanged(EventArgs e)
		{
			if (ControlChanged != null)
			{
				ControlChanged(this, e);
			}
		}
		// ****************
		public delegate void TargetControlChangedHandler(object sender, TargetControlChangedArgs e);
		public event TargetControlChangedHandler? TargetControlChanged;
		protected virtual void OnTargetControlChanged(TargetControlChangedArgs e)
		{
			if (TargetControlChanged != null)
			{
				TargetControlChanged(this, e);
			}
		}
		public delegate void IsEditChangedHandler(object sender, EventArgs e);
		public event IsEditChangedHandler? IsEditChanged;
		protected virtual void OnIsEditChanged(EventArgs e)
		{
			if (IsEditChanged != null)
			{
				IsEditChanged(this, e);
			}
		}
		#endregion
		#region Props
		public HScript Script { get; set; } = new HScript();
		public void SetResult(object? o) { Script.Root.SetResult(o); }
		public HScriptCode ScriptCode { get; set; } = new HScriptCode();
		public void ExecScript(string s)
		{
			Script.ExecuteCode(s);
		}
		public void ExecScript(ScriptItem si)
		{
			Script.ExecuteCode(si);
		}
		// ******************
		private bool m_CanPropertyGrid = true;
		[Category("Hypowered"), Browsable(false)]
		public bool CanPropertyGrid
		{
			get { return m_CanPropertyGrid; }
			set
			{
				m_CanPropertyGrid = value;
				if (m_CanPropertyGrid == false)
				{
					IsEdit = false;
				}
			}
		}

		private int m_GridSize = 2;
		// ******************
		[Category("Hypowered_Size"), Browsable(true)]
		public int GridSize
		{
			get { return m_GridSize; }
			set
			{
				if (value < 1) value = 1;

				if(m_GridSize !=value)
				{
					m_GridSize = value;
					if (this.Controls.Count > 1)
					{
						for(int i=1; i< this.Controls.Count;i++)
						{
							if (this.Controls[i] is HControl)
							{
								((HControl)this.Controls[i]).GridSize = m_GridSize;
							}
						}
					}
				}
				this.Invalidate();
			}
		}
		// ******************
		public ItemsLib ItemsLib { get; set; } = new ItemsLib();
		// ******************

		public Bitmap? GetBitmapFromLib(string nm)
		{
			Bitmap? ret = null;
			if ((ItemsLib.Enabled) && (ItemsLib.ItemNamesCount > 0))
			{
				ret = ItemsLib.GetBitmap(nm);
			}
			if ((ret == null) && (MainForm != null))
			{
				ret = MainForm.ItemsLib.GetBitmap(nm);
			}
			return ret;
		}
		// ******************
		private MainForm? m_MainForm = null;
		[Category("Hypowered"), Browsable(false)]
		public MainForm? MainForm
		{
			get { return m_MainForm; }
		}
		public void SetMainForm(MainForm mf)
		{
			m_MainForm = mf;
			Script.SetMainForm(mf, this);

		}
		// ******************
		protected bool m_IsEdit = false;
		[Category("_Hypowered"), Browsable(true)]
		public bool IsEdit
		{
			get { return m_IsEdit; }
			set
			{
				if (m_IsEdit != value)
				{
					m_IsEdit = value;
					if (this.Controls.Count > 1)
					{
						for (int i = 1; i < this.Controls.Count; i++)
						{
							if (this.Controls[i] is HControl)
							{
								((HControl)this.Controls[i]).SetIsEdit(value);
							}
						}
					}
					this.Invalidate();
					OnIsEditChanged(new EventArgs());
				}
			}
		}
		// ******************
		private HControl? m_TargetControl = null;
		public HControl? TargetControl { get { return m_TargetControl; } }
		private int m_TargetIndex = -1;
		[Category("_Hypowered"), Browsable(true)]
		public int TargetIndex
		{
			get { return (int)m_TargetIndex; }
			set
			{
				if ((m_IsEdit == false) || (value <= 0) || (value >= this.Controls.Count))
				{
					value = -1;
				}
				if (value > 0)
				{
					if (this.Controls[value] is not HControl)
						value = -1;
				}
				bool b = (m_TargetIndex != value);

				m_TargetIndex = value;
				if (m_TargetIndex < 0)
				{
					m_TargetControl = null;
				}
				else
				{
					m_TargetControl = (HControl)this.Controls[m_TargetIndex];
				}
				this.Invalidate();
				if (b) OnTargetControlChanged(new TargetControlChangedArgs(m_TargetControl));
			}
		}

		[Category("Hypowered"), Browsable(false)]
		public bool[] SelectedArray
		{
			get
			{
				bool[] ret = new bool[this.Controls.Count];
				ret[0] = false;
				for (int i = 1; i < this.Controls.Count; i++)
				{
					if (this.Controls[i] is HControl)
					{
						HControl hc = (HControl)this.Controls[i];
						ret[i] = hc.Selected;
					}
					else
					{
						ret[i] = false;
					}
				}
				return ret;
			}
			set
			{
				if ((value.Length == this.Controls.Count) && (value.Length > 1))
				{
					for (int i = 1; i < this.Controls.Count; i++)
					{
						if (this.Controls[i] is HControl)
						{
							HControl hc = (HControl)this.Controls[i];
							hc.SetSelected(value[i]);
						}
					}
				}
			}
		}
		// ******************
		[Category("Hypowered")]
		public int Index { get; set; } = -1;

		private bool m_IsShowSelected = true;
		[Category("_Hypowered")]
		public bool IsShowSelected
		{
			get { return m_IsShowSelected; }
			set
			{
				m_IsShowSelected = value;
				this.Invalidate();
			}
		}

		[Category("Hypowered_Menu")]
		public HMainMenu MainMenu { get; set; } = new HMainMenu();



		[Category("Hypowered_Menu")]
		public bool MainMenuVisible
		{
			get { return MainMenu.Visible; }
			set { MainMenu.Visible = value; }
		}
		[Category("Hypowered_Draw")]
		public new double Opacity
		{
			get { return base.Opacity; }
			set { base.Opacity = value; }
		}
		[Category("Hypowered_Draw")]
		public new bool DoubleBuffered
		{
			get { return base.DoubleBuffered; }
			set { base.DoubleBuffered = value; }
		}
		[Category("Hypowered_Draw"), Browsable(true)]
		public new string Name
		{
			get { return base.Name; }
			set
			{
				if (base.Name != value)
				{
					if (ItemsLib.Rename(value))
					{
						base.Name = value;
						Script.InitControls();
						OnFormNameChange(new FormChangedEventArgs(base.Name, this.Index));
					}
				}
			}
		}
		protected Color m_TargetColor = Color.Yellow;
		[Category("Hypowered_Color"), Browsable(true)]
		public Color TargetColor
		{
			get { return m_TargetColor; }
			set { m_TargetColor = value; this.Invalidate(); }
		}
		protected Color m_SelectedColor = Color.Red;
		[Category("Hypowered_Color"), Browsable(true)]
		public Color SelectedColor
		{
			get { return m_SelectedColor; }
			set
			{
				m_SelectedColor = value;
				if (this.Controls.Count > 1)
				{
					for (int i = 1; i < this.Controls.Count; i++)
					{
						if (this.Controls[i] is HControl)
						{
							((HControl)this.Controls[i]).SelectedColor = m_SelectedColor;
						}
					}
				}
				this.Invalidate();
			}
		}
		#endregion
		/// <summary>
		/// 選択状態の一括設定
		/// </summary>
		/// <param name="b"></param>
		public void SetSelectedAll(bool b = false)
		{
			if (this.Controls.Count <= 0) return;
			for (int i = 0; i < this.Controls.Count; i++)
			{
				if (this.Controls[i] is HControl)
				{
					((HControl)this.Controls[i]).Selected = b;
				}
			}
		}
		public bool IsSelectedTrue
		{
			get
			{
				bool ret = false;
				if (this.Controls.Count <= 0) return ret;
				for (int i = 0; i < this.Controls.Count; i++)
				{
					if (this.Controls[i] is HControl)
					{
						if (((HControl)this.Controls[i]).Selected == true)
						{
							ret = true;
							break;
						}
					}
				}
				return ret;
			}

		}
		// 
		/// <summary>
		/// ListboxのSelectedIndeiesに対応
		/// </summary>
		/// <param name="b"></param>
		public void SetSelecteds(int[] b)
		{
			if (this.Controls.Count <= 0) return;
			bool[] bb = new bool[this.Controls.Count];
			for (int i = 0; i < this.Controls.Count; i++) bb[i] = false;
			if (b.Length > 0)
			{
				for (int i = 0; i < b.Length; i++)
				{
					int ii = b[i];
					if ((ii >= 0) && (ii < this.Controls.Count))
					{
						bb[ii] = true;
					}
				}
			}
			for (int i = 0; i < this.Controls.Count; i++)
			{
				if (this.Controls[i] is HControl)
				{
					((HControl)this.Controls[i]).Selected = bb[i];
				}
			}

		}
		// ************************************************************
		public HForm() : base()
		{
			this.StartPosition = FormStartPosition.CenterScreen;
			this.SuspendLayout();
			this.Opacity = 0;
			Script.SetMainForm(MainForm, this);
			ScriptCode.Setup(HScriptType.FormLoad, HScriptType.FormClosed);
			InitializeComponent();
			this.StartPosition = FormStartPosition.Manual;
			base.BackColor = Color.FromArgb(64, 64, 64);
			base.ForeColor = Color.FromArgb(230, 230, 230);
			this.DoubleBuffered = true;
			base.AutoScaleMode = AutoScaleMode.None;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw,
				true);
			this.UpdateStyles();

			MainMenu.Location = new Point(0, m_BarHeight);
			MainMenu.Size = new Size(this.Width, MainMenu.Height);

			MainMenu.SetHForm(this);
			MainMenu.CloseMenu.FuncType = ThisClose;
			MainMenu.MainFormMenu.FuncType = ShowMainMenu;
			this.Controls.Add(MainMenu);
			ChkControl();
			//InitMenuStrip();
			this.FormClosed += (sender, e) => { LastSettings(); };
			Debug.WriteLine("HFOrm");
			StartSettings();
			this.Opacity = 100;

		}
		// **********************************************************
		public void ThisClose()
		{
			this.Close();
		}
		public void ShowMainMenu()
		{
			if (MainForm != null) {
				MainForm.SetVisible(true); 
			}
		}
		// **********************************************************
		public void StartSettings()
		{
			if (ItemsLib.FileName != "")
			{
				PrefFile pf = new PrefFile(this, ItemsLib.FileName);
				pf.Load();
				pf.GetLocation();
			}
		}
		// **********************************************************
		private void LastSettings()
		{
			if (ItemsLib.FileName != "")
			{
				PrefFile pf = new PrefFile(this, ItemsLib.FileName);
				pf.SetLocation();
				pf.Save();
				SaveToHypf();
			}
		}

		// ************************************************************
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			MainMenu.Location = new Point(0, m_BarHeight);
			MainMenu.Size = new Size(this.Width, MainMenu.Height);
			this.Refresh();
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if((Control.ModifierKeys & (Keys.Control | Keys.Shift)) == (Keys.Control|Keys.Shift))
			{
				if(MainForm!=null) MainForm.SetVisible(true);
			}


			if (m_IsEdit)
			{
				if (this.Controls.Count > 1)
				{
					if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
					{
						if (IsSelectedTrue)
						{
							SetSelectedAll(false);
						}
						else
						{
							SetSelectedAll(true);
						}
						OnSelectedArrayChanged(new SelectedArrayChangedEventArgs(SelectedArray));
						this.Invalidate();
					}
				}
			}

			this.Focus();
			base.OnMouseDown(e);
		}

		// ********************************************************************
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if ((m_IsShowSelected) && (m_IsEdit))
			{
				if ((this.Controls.Count > 1) && (m_TargetControl != null))
				{
					{
						using (Pen p = new Pen(m_TargetColor))
						{
							p.Width = 1;
							Rectangle r = new Rectangle(
								m_TargetControl.Left - 1,
								m_TargetControl.Top - 1,
								m_TargetControl.Width + 2,
								m_TargetControl.Height + 2
								);
							e.Graphics.DrawRectangle(p, r);
						}
					}
				}
			}
		}
		// ********************************************************************
		protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
		{
			if(e.KeyData == (Keys.Home | Keys.Control))
			{
				ShowMainMenu();
			}

			base.OnPreviewKeyDown(e);
		}

		// ********************************************************************
	}
	
}
