using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ClearScript;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Scripting;
namespace Hpd
{
	public partial class HpdForm : Form
	{
		public delegate void ItemChangedChangedHandler(object sender, EventArgs e);
		public event ItemChangedChangedHandler? ItemChanged;
		protected virtual void OnItemChanged(EventArgs e)
		{
			if (ItemChanged != null)
			{
				ItemChanged(this, e);
			}
		}       
		/// <summary>
		/// メインメニュー
		/// </summary>
		[Category("Hypowered")]
		public HpdMainMenu MainMenu { get; set; } = new HpdMainMenu();
		public HpdScriptCode ScriptCode = new HpdScriptCode();
		protected HpdControlCollection m_Items = new HpdControlCollection();
		[Browsable(false)]
		public HpdControlCollection Items { get { return m_Items; } }
		public HpdControl? Item(int idx)
		{
			HpdControl? ret = null;
			if (idx >= 0 && idx < m_Items.Count)
			{
				ret = m_Items[idx];
			}
			return ret;
		}
		public HpdControl? Item(string key)
		{
			return m_Items.Find(key);
		}

		/// <summary>
		/// ItemCollectionを構築する
		/// </summary>
		/// <param name="ctrl"></param>
		[ScriptUsage(ScriptAccess.None)]
		protected void ListupControls(Control ctrl)
		{
			if(ctrl.Controls.Count > 0)
			{
				foreach (Control c in ctrl.Controls)
				{
					if(c is HpdPanel)
					{
						m_Items.Add((HpdControl)c);
						ListupControls(c);
					}else if (c is HpdControl)
					{
						m_Items.Add((HpdControl)c);
					}
				}
			}
		}
		/// <summary>
		/// ItemCollectionを構築する
		/// </summary>
		/// <param name="ctrl"></param>
		[ScriptUsage(ScriptAccess.None)]
		public void ListupControls()
		{
			string n = "";
			if (Items.TargetControl!=null)
			{
				n = Items.TargetControl.Name;
			}
			m_Items.Clear();
			foreach (Control c in Controls)
			{
				if (c is MenuStrip)
				{
					Controls.SetChildIndex(c, 0);
				}
			}
			ListupControls(this);
			if(n!="")
			{
				int idx = Items.IndexOf(n);
				if(idx >= 0 ) { Items.SetTargetIndexNoEvent(idx); } 
			}
			OnItemChanged(new EventArgs());
		}
		// *************************************************************
		public delegate void NameChangedHandler(object sender, EventArgs e);
		public event NameChangedHandler? NameChanged;
		public virtual void OnNameChanged(EventArgs e)
		{
			if (NameChanged != null)
			{
				NameChanged(this, e);
			}
		}

		// *************************************************************
		protected HpdOrientation m_Orientation = HpdOrientation.Vertical;
		[Category("Hypowered_layout")]
		public HpdOrientation Orientation
		{
			get { return m_Orientation; }
			set 
			{
				bool b = (m_Orientation != value);
				m_Orientation = value; 
				if(b) AutoLayout(); 
			}
		}
		protected Size m_BaseSize = new Size(0,0);
		[Category("Hypowered_layout")]
		public Size BaseSize
		{
			get { return m_BaseSize; }
			set { m_BaseSize = value; }
		}
		[ScriptUsage(ScriptAccess.None)]
		public void SetBaseSize(int? w = null, int? h = null)
		{
			int ww = 0;
			int hh = 0;
			if (w != null) ww = (int)w; else ww = m_BaseSize.Width;
			if (h != null) hh = (int)h; else hh = m_BaseSize.Height;
			m_BaseSize = new Size(ww, hh);
		}
		[Category("Hypowered_layout")]
		public new Padding Padding
		{
			get { return base.Padding; }
			set
			{
				bool b = (base.Padding != value);
				base.Padding = value;
				if (b) AutoLayout();
			}
		}
		[Category("Hypowered"),Browsable(true)]
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
			set 
			{ 
				base.Size = value; 
				AutoLayout();
				this.Invalidate(); 
			}
		}
		[Category("Hypowered_layout"),Browsable(true), ScriptUsage(ScriptAccess.None)]
		public new Size MinimumSize
		{
			get { return base.MinimumSize; }
			set { }
		}
		public void SetMinimumSize(Size sz)
		{
			base.MinimumSize = sz;
		}
		[Category("Hypowered_Text")]
		public new Font Font
		{
			get { return base.Font; }
			set { base.Font = value; }
		}
		// *******************************************************************************
		public HpdForm()
		{
			base.AutoScaleMode = AutoScaleMode.None;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw,
				true);
			this.UpdateStyles();

			this.Controls.Add(MainMenu);
			
			InitializeComponent();
			ControlAdded += (sender, e) => 
			{ 
				AutoLayout();
				ListupControls();
			};
			ControlRemoved += (sender, e) => 
			{
				AutoLayout();
				ListupControls();
			};
			AutoLayout();
			ListupControls();
			m_Items.TargetControlChanged += (sender, e) =>
			{
				if (e.Ctrl != null)
				{
					this.Text = e.Ctrl.Text;
				}
				else
				{
					this.Text = "None Target";
				}
			};

		}
		// *******************************************************************************
		
		// *******************************************************************************
		private int LastNumber(string name)
		{
			int ret = -2;
			if (name.Length > 0)
			{
				for (int i = name.Length - 1; i >= 0; i--)
				{
					if ((name[i] < '0') || (name[i] > '9'))
					{
						ret = i;
						break;
					}
				}
				if (ret == name.Length - 1)
				{
					ret = -1;
				}
				else if (ret == -2)
				{
					ret = 0;
				}
				else
				{
					ret += 1;
				}
			}
			return ret;
		}
		// *******************************************************************************
		[ScriptUsage(ScriptAccess.None)]
		public string NewName(string name)
		{
			if (name == "") return "";
			int idx = LastNumber(name);
			string node="";
			int num = 0;
			string numStr = "";
			if (idx <0)
			{
				node = name;
			}
			else
			{
				node = name.Substring(0, idx);
				string ns = name.Substring(idx);
				if(int.TryParse(ns, out num)==false)
				{
					num = 1;
				}
				numStr = $"{num}";

			}

			
			while(m_Items.IndexOf(node+numStr)>=0)
			{
				num++;
				numStr = $"{num}";
			}
			return node + numStr;
		}
		// *******************************************************************************
		[ScriptUsage(ScriptAccess.None)]
		public HpdControl CreateControl(string name, HpdType ht)
		{
			HpdControl ret;

			name = NewName(name);

			switch (ht)
			{
				case HpdType.TextBox:
					HpdTextBox htb = new HpdTextBox();
					htb.Name = name;
					htb.Text = name;
					ret = (HpdControl)htb;
					break;
				case HpdType.Label:
					HpdLabel lbl = new HpdLabel();
					lbl.Name = name;
					lbl.Text = name;
					ret = (HpdControl)lbl;
					break;
				case HpdType.ComboBox:
					HpdComboBox comb = new HpdComboBox();
					comb.Name = name;
					comb.Text = name;
					ret = (HpdControl)comb;
					break;
				case HpdType.ListBox:
					HpdListBox lb = new HpdListBox();
					lb.Name = name;
					lb.Text = name;
					ret = (HpdControl)lb;
					break;
				case HpdType.CheckBox:
					HpdCheckBox cb = new HpdCheckBox();
					cb.Name = name;
					cb.Text = name;
					ret = (HpdCheckBox)cb;
					break;
				case HpdType.RadioButton:
					HpdRadioButton rb = new HpdRadioButton();
					rb.Name = name;
					rb.Text = name;
					ret = (HpdRadioButton)rb;
					break;
				case HpdType.Panel:
					HpdPanel hp = new HpdPanel();
					hp.SetBaseSize(0, 0);
					hp.Name = name;
					hp.Text = name;
					ret = (HpdControl)hp;
					break;
				case HpdType.Stretch:
					HpdStretch stretch = new HpdStretch();
					stretch.SetBaseSize(0, 0);
					stretch.Name = name;
					stretch.Text = name;
					ret = (HpdControl)stretch;
					break;
				case HpdType.Button:
				default:
					HpdButton hb = new HpdButton();
					hb.Name = name;
					hb.Text = name;
					ret = (HpdControl)hb;
					break;
			}
			return ret;
		}
		// *******************************************************************************
		[ScriptUsage(ScriptAccess.None)]
		public HpdControl? AddControl(string Name,HpdType ht)
		{

			return HU.AddControl(this,this,Name,ht);
		}
		// *******************************************************************************
		public HpdType DefHpdType = HpdType.Button;
		// *******************************************************************************
		public HpdControl? AddControl()
		{
			return HU.AddControl(this, this);
		}
		// *******************************************************************************
		private bool AutoLayoutFlag = false;
		[ScriptUsage(ScriptAccess.None)]
		public void AutoLayout()
		{
			if (AutoLayoutFlag) return;
			AutoLayoutFlag = true;
			this.SuspendLayout();
			HL.ChkPanelLayout(this);
			HL.ChkLayout(this);
			this.ResumeLayout(false);
			AutoLayoutFlag = false;
		}
		// *******************************************************************************
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			AutoLayout();
		}
		// *******************************************************************************
		[Category("Hypowered_layout")]
		public Padding WakuPadding
		{
			get
			{
				Rectangle ret = this.ClientRectangle;

				int l = 0;
				int t = 0;
				int r = 0;
				int b = 0;
				if ( Controls.Count > 0 )
				{
					foreach ( Control c in Controls)
					{
						if((c is MenuStrip)
							|| (c is HpdMainMenu)
							||(c is StatusStrip)
							|| (c is ToolStrip)
							)
						{
							if (c.Visible == false) continue;
							switch (c.Dock)
							{
								case DockStyle.Top:
									t += c.Height + c.Margin.Top + c.Margin.Bottom;
									break;
								case DockStyle.Bottom:
									b += c.Height + c.Margin.Top + c.Margin.Bottom;
									break;
								case DockStyle.Left:
									l += c.Width + c.Margin.Left + c.Margin.Right;
									break;
								case DockStyle.Right:
									r += c.Width + c.Margin.Left + c.Margin.Right;
									break;
							}

						}
					}
				}
				return new Padding(l, t, r, b);
			}
		}
		[Category("Hypowered_layout")]
		public Size WakuSize
		{
			get
			{
				return new Size(this.Width - this.ClientSize.Width,
					this.Height - this.ClientSize.Height);
			}
		}
		// *******************************************************************************
		public Rectangle ClientRectangleEx
		{
			get
			{
				Rectangle r = ClientRectangle;
				Padding d = WakuPadding;
				return new Rectangle(
					r.Left + d.Left,
					r.Top + d.Top,
					r.Width - d.Left - d.Right,
					r.Height - d.Top - d.Bottom);
			}
			set
			{

			}
		}
		public void SetClientMinimumSize(Size sz)
		{
			Size h = WakuSize;
			this.Size = new Size(sz.Width+h.Width, sz.Height+h.Height);
		}
	}
}
