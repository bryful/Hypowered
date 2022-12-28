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

    public partial class HyperForm : Form
	{
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
			if(m_EditModeMenu!=null) m_EditModeMenu.Checked = value;
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


		protected HyperMenuBar m_menuBar = new HyperMenuBar();
		protected HyperMenuItem? m_FileMenu = null;
		protected HyperMenuItem? m_EditlMenu = null;
		protected HyperMenuItem? m_ControlMenu = null;
		protected HyperMenuItem? m_UserMenu = null;

		public HyperForm()
		{

			base.KeyPreview = true;
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			this.Name = "HyperForm";
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
			SetupFuncs();
			InitializeComponent();
			if(m_menuBar==null)m_menuBar = new HyperMenuBar();
			m_FileMenu = new HyperMenuItem(m_menuBar, "File", null);
			m_EditlMenu = new HyperMenuItem(m_menuBar, "Edit", null);
			m_ControlMenu = new HyperMenuItem(m_menuBar, "Control", null);
			m_UserMenu = new HyperMenuItem(m_menuBar, "User", null);



			m_menuBar.Items.Add(m_FileMenu);
			m_menuBar.Items.Add(m_EditlMenu);
			m_menuBar.Items.Add(m_ControlMenu);
			m_menuBar.Items.Add(m_UserMenu);
			MakeMenu();
			this.Controls.Add(m_menuBar);
			this.Controls.SetChildIndex(m_menuBar,0);
			ChkControls();

			InitScript();
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			LoadFromFile("aaa.json");
		}
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);

			if(PropForm!=null) PropForm.Dispose();
			if(ControlList!=null) ControlList.Dispose();
			if(Editor!=null) Editor.Dispose();

			SaveToFile("aaa.json");
		}



		// ***********************************************************************
		public void InitScript()
		{
			m_Script.Init();
			m_Script.InitControls(this.Controls);
		}
		// ***********************************************************************
		protected void ChkControls()
		{
			if (this.Controls.Count > 0)
			{
				//メニューは必ず先頭に
				foreach (Control c in this.Controls)
				{
					if (c is HyperMenuBar)
					{
						this.Controls.SetChildIndex(c, 0);
					}
				}
				//番号を割り振る
				int idx = 0;
				foreach (Control c in this.Controls)
				{
					if (c is HyperControl)
					{
						HyperControl h = (HyperControl)c;
						h.SetIndex(idx);
						h.HyperForm= this;
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
						hc.LocationChanged -= Hc_LocationChanged;
						hc.LocationChanged += Hc_LocationChanged;
					}
				}
			}
			InitScript();
		}
		private void HyperForm_ControlRemoved(object? sender, ControlEventArgs e)
		{
			InitScript();
		}
		private void Hc_LocationChanged(object? sender, EventArgs e)
		{
			this.Invalidate();
		}
		// ***********************************************************************
		public void ChkRadioButton(HyperRadioButton ct)
		{
			if(this.Controls.Count > 0)
			{
				foreach (Control c in this.Controls)
				{
					if (c is HyperRadioButton)
					{
						HyperRadioButton hc = (HyperRadioButton)c;
						if (hc.Group == ct.Group)
						{
							if (hc.Index == ct.Index)
							{
								hc.SetCheckedNoEvent(true);
							}
							else
							{
								hc.SetCheckedNoEvent(false);
							}
						}
					}
				}

			}
		}
		public bool IsRadioButtonIndex(HyperRadioButton ct,int idx)
		{
			bool ret = false;
			if (this.Controls.Count > 0)
			{
				foreach (Control c in this.Controls)
				{
					if (c is HyperRadioButton)
					{
						HyperRadioButton hc = (HyperRadioButton)c;
						if (hc.Group == ct.Group)
						{
							hc.GroupIndex = idx;
							ret = true;
						}
					}
				}

			}
			return ret;
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
				g.DrawRectangle(p,new Rectangle(0,0,Width-1,Height-1));
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
		private Control? CreateControl(ControlType ct)
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
				this.Controls.Add(ctrl);
				//this.Controls.SetChildIndex(ctrl, 1);
				ChkControls();
				
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
		public ToolStripMenuItem[] GetMenuControls(HyperControl? target, System.EventHandler func)
		{
			List<ToolStripMenuItem> list = new List<ToolStripMenuItem>();
			if (this.Controls.Count > 0)
			{
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
	}
}
