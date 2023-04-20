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

namespace Hypowered
{
	
	public partial class HForm : BaseForm
	{
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
		protected virtual void OnSelectedChanged(SelectedChangedEventArgs e)
		{
			if (SelectedChanged != null)
			{
				SelectedChanged(this, e);
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
		public delegate void TargetControlChangedHandler(object sender, TargetControlChangedEventArgs e);
		public event TargetControlChangedHandler? TargetControlChanged;
		protected virtual void OnTargetControlChanged(TargetControlChangedEventArgs e)
		{
			if (TargetControlChanged != null)
			{
				TargetControlChanged(this, e);
			}
		}
		#endregion
		#region Props
		public HScript Script { get; set; } = new HScript();
		public HScriptCode ScriptCode { get; set; } = new HScriptCode();
		// ******************
		private bool m_CanPropertyGrid = true;
		[Category("Hypowered"), Browsable(false)]
		public bool CanPropertyGrid
		{
			get { return m_CanPropertyGrid; }
			set 
			{ 
				m_CanPropertyGrid = value; 
				if(m_CanPropertyGrid==false)
				{
					IsEdit=false;
				}
			}
		}

		private string m_FileName = string.Empty;

		public ItemsLib ItemsLib { get; set; } = new ItemsLib();

		public Bitmap? GetBitmapFromLib(string nm)
		{
			Bitmap? ret = null;
			if((ItemsLib.Enabled)&&(ItemsLib.ItemNamesCount>0))
			{
				ret = ItemsLib.GetBitmap(nm);
			}
			if((ret== null)&&(MainForm!=null))
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
				m_IsEdit = value; 
				if(this.Controls.Count>1)
				{
					for(int i=1; i< this.Controls.Count;i++)
					{
						if(this.Controls[i] is HControl)
						{
							((HControl)this.Controls[i]).SetIsEdit(value);
						}
					}
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
				if((m_IsEdit==false)||(value<=0)||(value>=this.Controls.Count))
				{
					value = -1;
				}
				if(value>0)
				{
					if (this.Controls[value] is not HControl)
						value = -1;
				}
				bool b=(m_TargetIndex !=value);

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
				if (b) OnTargetControlChanged(new TargetControlChangedEventArgs(m_TargetIndex));
			}
		}

		[Category("Hypowered"),Browsable(false)]
		public bool[] SelectedArray
		{
			get
			{
				bool[] ret = new bool[this.Controls.Count];
				ret[0] = false;
				for(int i=1; i<this.Controls.Count; i++)
				{
					if(this.Controls[i] is HControl)
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

		private bool m_IsShowSelected =true;
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
		[Category("Hypowered_Draw"),Browsable(true)]
		public new string Name
		{
			get { return base.Name; }
			set
			{
				if(base.Name != value)
				{
					if (ItemsLib.Rename(value))
					{
						base.Name = value;
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
				if(this.Controls.Count > 1)
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
						if (((HControl)this.Controls[i]).Selected ==true)
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
			Script.SetMainForm(null, this);
			ScriptCode.Setup(HScriptType.FormLoad,HScriptType.FormClosed);
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
			this.Controls.Add(MainMenu);
			ChkControl();
			//InitMenuStrip();
			this.FormClosed += (sender, e) => { LastSettings(); };
			StartSettings();
		}
		// **********************************************************
		public void StartSettings()
		{
			if(ItemsLib.FileName!="")
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
				ExportToHypf();
			}
		}
		// ************************************************************
		private void InitMenuStrip()
		{
			MainMenu.Name = "MainManu";
			MainMenu.Text = "Main";
			MainMenu.AutoSize = false;
			MainMenu.Dock = DockStyle.None;
			MainMenu.Anchor = AnchorStyles.None;
			MainMenu.Location = new Point(0, m_BarHeight);
			MainMenu.Size = new Size(this.Width, MainMenu.Height);

			/*
			FileMenu.Name = "FileMenu";
			FileMenu.Text = "File";
			EditMenu.Name = "EditMenu";
			EditMenu.Text = "Edit";
			ToolMenu.Name = "ToolMenu";
			ToolMenu.Text = "Tool";

			OpenMenu.Name = "OpenMenu";
			OpenMenu.Text = "Open";
			*/
			//MainMenu.CloseMenu.Click += (sender, e) => { this.Close(); };
			//MainMenu.FileMenu.Click += (sender, e) => { this.Close(); };

			/*
			MainMenu.Name = "MainFormMenu";
			MainMenu.Text = "Main";
			MainMenu.Click += (sender, e) =>
			{
				if (m_MainForm != null)
				{
					m_MainForm.Visible = true;
					m_MainForm.Activate();
				}
			};

			FileMenu.DropDownItems.Add(OpenMenu);
			FileMenu.DropDownItems.Add(CloseMenu);

			ToolMenu.DropDownItems.Add(MainMenu);

			MainMenu.Items.Add(FileMenu);
			MainMenu.Items.Add(EditMenu);
			MainMenu.Items.Add(ToolMenu);

			this.Controls.Add(MainMenu);
			*/
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
			if(m_IsEdit)
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
						OnSelectedChanged(new SelectedChangedEventArgs(SelectedArray));
						this.Invalidate();
					}
				}
			}

			this.Focus();
			base.OnMouseDown(e);
		}
		// ************************************************************
		public int IndexOfControl(string key)
		{
			return this.Controls.IndexOfKey(key);
		}
		// ************************************************************
		public string ControlNewName(HType ht)
		{
			string? nm = Enum.GetName(typeof(HType), ht);
			if (nm == null) nm = "ctrl";
			int idx = 1;
			string s = $"{nm}{idx}";
			while (this.Controls.IndexOfKey(s) >= 0)
			{
				s = $"{nm}{idx}";
				idx++;
			};
			return s;
		}
		private Point pDef = new Point(100, 100);
		// ************************************************************
		public HControl CreateControl(HType ht)
		{
			HControl hc;
			switch (ht)
			{

				case HType.Label:
					hc = new HLabel();
					hc.Location = pDef;
					hc.Size = new Size(75, 25);
					break;
				case HType.TextBox:
					hc = new HTextBox();
					hc.Location = pDef;
					hc.Size = new Size(75, 25);
					break;
				case HType.PictureBox:
					hc = new HPictureBox();
					hc.Location = pDef;
					hc.Size = new Size(200, 200);
					break;
				case HType.IconButton:
					hc = new HIconButton();
					hc.Location = pDef;
					break;
				case HType.ListBox:
					hc = new HListBox();
					hc.Location = pDef;
					hc.Size = new Size(250, 200);
					break;
				case HType.Button:
				default:
					hc = new HButton();
					hc.Location = pDef;
					hc.Size = new Size(75, 25);
					break;
			}
			hc.SetHForm(this);
			hc.SelectedChanged += (sender, e) => { OnSelectedChanged(new SelectedChangedEventArgs(SelectedArray)); };

			hc.ControlNameChanged += (semder, e) =>
			{
				OnControlNameChanged(new ControlChangedEventArgs(
					e.Name,
					Index,
					e.Index
					));
			};
			
			return hc;
		}
		public void AddControl(HType ht, string nm, string tx)
		{
			HControl hControl = CreateControl(ht);
			hControl.Name = nm;
			if (tx != "") tx = nm;
			hControl.Text = tx;
			this.Controls.Add(hControl);
			this.Controls.SetChildIndex(hControl, 1);
			ChkControl();
			pDef.X += 10;
			if (pDef.X > 250) pDef.X = 100;
			pDef.Y += 10;
			if (pDef.Y > 250) pDef.Y = 100;

			OnControlChanged(new EventArgs());
			ExportToHypf();
		}
		private HType m_HTypeDef = HType.Button;
		public void AddControl()
		{
			using (AddControlDialog dlg = new AddControlDialog())
			{
				dlg.HForm = this;
				dlg.TopMost = this.TopMost;
				dlg.Caption = "Add Control Dialog";
				dlg.HType = m_HTypeDef;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					AddControl(dlg.HType, dlg.CName, dlg.CText);
					m_HTypeDef = dlg.HType;
				}
			}

		}
		public void ChkControl()
		{
			if (this.Controls.Count > 0)
			{
				foreach (Control c in this.Controls)
				{
					if (c is HMainMenu)
					{
						if (this.Controls.GetChildIndex(c) != 0)
						{
							this.Controls.SetChildIndex(c, 0);
						}
					}
				}
				int idx = 0;
				foreach (Control c in this.Controls)
				{
					if (c is HMainMenu)
					{
						((HMainMenu)c).Index = idx;
					}
					else if (c is HControl)
					{
						((HControl)c).Index = idx;
					}
					idx++;
				}
			}
		}
		public string[] ControlList()
		{
			List<string> list = new List<string>();
			if (this.Controls.Count > 0)
			{
				foreach (Control c in this.Controls)
				{
					if ((c is HMainMenu) || (c is HControl))
					{
						list.Add(c.Name);
					}
				}
			}
			return list.ToArray();
		}
		public Control[] ControlArray()
		{
			List<Control> list = new List<Control>();
			if (this.Controls.Count > 0)
			{
				foreach (Control c in this.Controls)
				{
					if ((c is HMainMenu) || (c is HControl))
					{
						list.Add(c);
					}
				}
			}
			return list.ToArray();
		}

		public void ControlUp(int[] sels)
		{
			if (sels.Length <= 0) return;
			if (sels[0] == 1) return;
			int idx = sels[0] - 1;
			for (int i = 0; i < sels.Length; i++)
			{
				this.Controls.SetChildIndex(
					this.Controls[sels[i]],
					idx
					);
				idx++;
			}
			OnControlChanged(EventArgs.Empty);
		}
		public void ControlTop(int[] sels)
		{
			if (sels.Length <= 0) return;
			if (sels[0] == 1) return;
			int idx = 1;
			for (int i = 0; i < sels.Length; i++)
			{
				this.Controls.SetChildIndex(
					this.Controls[sels[i]],
					idx
					);
				idx++;
			}
			OnControlChanged(EventArgs.Empty);
		}
		public void ControlDown(int[] sels)
		{
			if (sels.Length <= 0) return;
			if (sels[sels.Length - 1] == this.Controls.Count - 1) return;
			int idx = sels[sels.Length - 1] + 1;
			for (int i = sels.Length - 1; i >= 0; i--)
			{
				this.Controls.SetChildIndex(
					this.Controls[sels[i]],
					idx
					);
				idx--;
			}
			OnControlChanged(EventArgs.Empty);
		}
		public void ControlBottom(int[] sels)
		{
			if (sels.Length <= 0) return;
			if (sels[sels.Length - 1] == this.Controls.Count - 1) return;
			int idx = this.Controls.Count - 1;
			for (int i = sels.Length - 1; i >= 0; i--)
			{
				this.Controls.SetChildIndex(
					this.Controls[sels[i]],
					idx
					);
				idx--;
			}
			OnControlChanged(EventArgs.Empty);
		}
		public void ControlMove(int[] sels, ArrowDown ad, int MoveScale)
		{
			if (sels.Length <= 0) return;
			foreach (int sel in sels)
			{
				if ((sel > 0) && (sel < this.Controls.Count))
				{
					if (this.Controls[(int)sel] is HControl)
					{
						((HControl)this.Controls[(int)sel]).MovePos(ad, MoveScale);
					}
				}
			}
		}
		public void ControlResizeLeftTop(int[] sels, ArrowDown ad, int MoveScale)
		{
			if (sels.Length <= 0) return;
			foreach (int sel in sels)
			{
				if ((sel > 0) && (sel < this.Controls.Count))
				{
					if (this.Controls[(int)sel] is HControl)
					{
						((HControl)this.Controls[(int)sel]).ResizeLeftTop(ad, MoveScale);
					}
				}
			}
		}
		public void ControlResizeRightBottom(int[] sels, ArrowDown ad, int MoveScale)
		{
			if (sels.Length <= 0) return;
			foreach (int sel in sels)
			{
				if ((sel > 0) && (sel < this.Controls.Count))
				{
					if (this.Controls[(int)sel] is HControl)
					{
						((HControl)this.Controls[(int)sel]).ResizeRightBottom(ad, MoveScale);
					}
				}
			}
		}
		// ********************************************************************
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if ((m_IsShowSelected)&&(m_IsEdit))
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
		public bool ExportToHypf()
		{
			if(ItemsLib.FileName=="")
			{
				if(MainForm!=null) MainForm.NewForm();
				if (ItemsLib.FileName == "") return false;
			}

			base.Name = ItemsLib.Name;
			base.Text = base.Name;
			string js = ToJsonCode();
			return ItemsLib.SetText(MainForm.HYPF_JSON, js);
		}
		// ********************************************************************
		public bool ImportFromHypf()
		{
			bool ret = false;
			if (ItemsLib.FileName == "")
			{
				return ret;
			}
			string? js = ItemsLib.GetText(MainForm.HYPF_JSON);
			if((js==null)||(js=="")) return ret;
			var doc = JsonNode.Parse(js);
			if (doc != null)
			{
				JsonObject? jo = (JsonObject?)doc;
				if (jo != null)
				{
					FromJson(jo);
					ret = true;
				}
			}
			base.Name = ItemsLib.Name;
			base.Text = base.Name;
			return ret;
		}
		// ********************************************************************
		public bool RemoveControl(Control? c)
		{
			try
			{
				this.Controls.Remove(c);
				ChkControl();
				OnControlChanged(new EventArgs());
				return true;
			}
			catch
			{
				return false;
			}
		}
		public bool RemoveControl()
		{
			if(m_TargetControl!=null)
			{
				return RemoveControl(m_TargetControl);
			}
			else
			{
				return false;
			}
		}
		public bool RenameForm(string nm)
		{
			bool ret = ItemsLib.Rename(nm);
			if (ret)
			{
				base.Name = ItemsLib.Name;
				OnFormNameChange(new FormChangedEventArgs(base.Name, Index));
			}
			return ret;
		}
	}
	public class FormChangedEventArgs : EventArgs
	{
		public String Name;
		public int Index;
		public FormChangedEventArgs(string n, int index)
		{
			Name = n;
			Index = index;
		}
	}
	public class ControlChangedEventArgs : EventArgs
	{
		public string Name;
		public int FormIndex;
		public int CtrlIndex;
		public ControlChangedEventArgs(string n, int findex, int cindex)
		{
			Name = n;
			CtrlIndex = cindex;
			FormIndex = findex;
		}
	}

	public class SelectedChangedEventArgs : EventArgs
	{
		public bool[] Selecteds = new bool[0];
		public SelectedChangedEventArgs(bool[] n)
		{
			Selecteds = n;
		}
	}
	public class TargetControlChangedEventArgs : EventArgs
	{
		public int Index;
		public TargetControlChangedEventArgs(int idx)
		{
			Index = idx;
		}
	}
}
