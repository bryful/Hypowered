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
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using BRY;
namespace Hpd
{
	public partial class HpdForm : Form
	{
		public delegate void NameChangedHandler(object sender, EventArgs e);
		public event NameChangedHandler? NameChanged;
		protected virtual void OnNameChanged(EventArgs e)
		{
			if (NameChanged != null)
			{
				NameChanged(this, e);
			}
		}
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
			set
			{
				m_BaseSize = value;
			}
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
		[Category("Hypowered")]
		[Browsable(true)]
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
			set { base.Size = value; this.Invalidate(); }
		}
		[Category("Hypowered"),Browsable(false)]
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
			set
			{
				base.Font = value;
			}
		}
		public HpdForm()
		{
			//base.BackColor = Color.FromArgb(32, 32, 32);
			//base.ForeColor = Color.FromArgb(220, 220, 220);
			base.AutoScaleMode = AutoScaleMode.None;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw,
				true);
			this.UpdateStyles();
			InitializeComponent();
			ControlAdded += (sender, e) => { AutoLayout(); };
			ControlRemoved += (sender, e) => { AutoLayout(); };

			AutoLayout();
		}
		public HpdControl[] FindControl(string name)
		{
			Control[] controls = this.Controls.Find(name, true);

			List<HpdControl> result = new List<HpdControl>();
			foreach(Control c in controls)
			{
				if( c is HpdControl)
				{
					result.Add((HpdControl)c);
				}
			}

			return result.ToArray();
		}
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

			while(FindControl(node+numStr).Length>0)
			{
				num++;
				numStr = $"{num}";
			}
			return node + numStr;
		}
		static public HpdControl CreateControl(string name, HpdType ht)
		{
			HpdControl ret;
			switch (ht)
			{


				case HpdType.TextBox:
					HpdTextBox htb = new HpdTextBox();
					htb.Name = name;
					htb.Text = name;
					ret = (HpdControl)htb;
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
		public void AddControl(string Name,HpdType ht)
		{
			HpdControl? c = CreateControl(Name, ht);
			if(c != null)
			{
				Controls.Add(c);
				AutoLayout();
			}
		}
		public HpdType DefHpdType = HpdType.Button;
		public void AddControl()
		{
			using (NewControlDialog dlg = new NewControlDialog())
			{
				dlg.HpdType= DefHpdType;
				if( dlg.ShowDialog()== DialogResult.OK )
				{
					AddControl(dlg.HpdName,dlg.HpdType);
					DefHpdType = dlg.HpdType;
				}
			}
		}
		private bool AutoLayoutFlag = false;
		public void AutoLayout()
		{
			if ((this.Width <= this.MinimumSize.Width) && (this.Height <= this.MinimumSize.Height)) return;
			if (AutoLayoutFlag) return;
			AutoLayoutFlag = true;
			this.SuspendLayout();
			HpdLayout.ChkPanelLayout(this);
			HpdLayout.ChkLayout(this);
			this.ResumeLayout(false);
			AutoLayoutFlag = false;
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			AutoLayout();
		}
	}
}
