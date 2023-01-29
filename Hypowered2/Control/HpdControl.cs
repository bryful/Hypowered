using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hpd
{
    public enum HpdType
	{
		None = -1,
		Button,
		TextBox,
		ComboBox,
		ListBox,
		CheckBox,
		Panel,
		Stretch
	}
	public partial class HpdControl : Control
	{
		static public void PropListToClipboard(Type t, string nm)
		{
			string s = "";
			foreach (MemberInfo mi in t.GetMembers())
			{
				if (mi.MemberType == MemberTypes.Event)
				{
					s += $"//{nm}.{mi.Name}+=(sender,e)=>{{ On{mi.Name}(e);}};\r\n";
				}
			}
			s += "********************************\r\n";
			foreach (var pi in t.GetProperties())
			{

				s += $"[Category(\"Hypowered\")]\r\n";
				s += $"public {pi.PropertyType.FullName} {pi.Name}\r\n";
				s += $"{{\r\n";
				s += $"\tget {{ return {nm}.{pi.Name}; }}\r\n";
				s += $"\tset {{ {nm}.{pi.Name} = value; }}\r\n";
				s += $"}}\r\n";
			}


			Clipboard.SetText(s);
		}


		#region Hypowered
		[Category("Hypowered"),Browsable(false)]
		protected HpdScriptCode ScriptCode { get; set; }= new HpdScriptCode();

		protected Control? m_Item = null;
		[Category("Hypowered"), Browsable(false)]
		public Button? AsButton
		{
			get
			{
				Button? ret = null;
				if (m_Item is Button) ret = (Button?)m_Item;
				return ret;
			}
			set{if ((value!=null)&&(m_Item is Button)) m_Item = value;}
		}
		[Category("Hypowered"), Browsable(false)]
		public TextBox? AsTextBox
		{
			get
			{
				TextBox? ret = null;
				if (m_Item is TextBox) ret = (TextBox?)m_Item;
				return ret;
			}
			set { if ((value != null) && (m_Item is TextBox)) m_Item = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public ComboBox? AsComboBox
		{
			get
			{
				ComboBox? ret = null;
				if (m_Item is ComboBox) ret = (ComboBox?)m_Item;
				return ret;
			}
			set { if ((value != null) && (m_Item is ComboBox)) m_Item = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public ListBox? AsListBox
		{
			get
			{
				ListBox? ret = null;
				if (m_Item is ListBox) ret = (ListBox?)m_Item;
				return ret;
			}
			set { if ((value != null) && (m_Item is ListBox)) m_Item = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public CheckBox? AsCheckBox
		{
			get
			{
				CheckBox? ret = null;
				if (m_Item is CheckBox) ret = (CheckBox?)m_Item;
				return ret;
			}
			set { if ((value != null) && (m_Item is CheckBox)) m_Item = value; }
		}
		protected HpdForm? m_Root = null;
		[Category("Hypowered"), Browsable(false)]
		public HpdForm? Root
		{
			get
			{
				Control? ret = m_Root;
				if (m_Root == null)
				{
					ret  = (Control?)this.Parent;
					while ((ret != null) && (ret.Parent != null))
					{
						if (ret is HpdForm) break;
						ret = ret.Parent;
					}
					m_Root = (HpdForm?)ret;
				}

				return m_Root;
			}

		}
		protected HpdType m_HpdType = HpdType.None;
		[Category("Hypowered")]
		public HpdType HpdType { get { return m_HpdType; } }

		[Category("Hypowered"), Browsable(true)]
		public new string Name
		{
			get { return base.Name; }
			set 
			{
				if (base.Name != value)
				{
					if(m_Item!=null) m_Item.Name = value;
					base.Name = value;
					OnNameChanged(new EventArgs());
				}
			}
		}
		[Category("Hypowered"), Browsable(true)]
		public DialogResult DialogResult
		{
			get {
				if((m_Item!= null)&&(m_Item is Button))
				{
					return ((Button)m_Item).DialogResult;
				}
				else
				{
					return DialogResult.Cancel;
				}
			}
			set 
			{
				if ((m_Item != null) && (m_Item is Button))
				{
					((Button)m_Item).DialogResult = value;
				}
			}
		}
		[Category("Hypowered_Text"), Browsable(true)]
		public new string Text
		{
			get
			{
				if (m_Item != null)
				{
					return m_Item.Text;
				}
				else
				{
					return base.Text;
				}
			}
			set
			{
				if (m_Item != null)
				{
					m_Item.Text = value;
				}
				base.Text = value;
				this.Invalidate(); 
			}
		}
		/// <summary>
		/// Textを配列として
		/// </summary>
		[Category("Hypowered_Text")]
		public string[] Lines
		{
			get 
			{
				if(m_Item!=null)
				{
					return m_Item.Text.Split("\r\n");
				}
				else
				{
					return base.Text.Split("\r\n");
				}
			}
			set
			{
				if (m_Item != null)
				{
					m_Item.Text = string.Join("\r\n", value);
				}
				base.Text = string.Join("\r\n", value);
				this.Invalidate();
			}
		}
		protected bool m_IsDrawFrame = false;
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
		[Category("Hypowered_layout"), Browsable(true)]
		public new Size PreferredSize
		{
			get
			{
				if (m_Item != null)
				{
					return m_Item.PreferredSize;
				}
				else
				{
					return base.PreferredSize;
				}
			}
		}
		[Category("Hypowered_layout")]
		public new Point Location
		{
			get { return base.Location; }
			set { base.Location = value; this.Invalidate(); }
		}
		[Category("Hypowered_layout")]
		public new Size Size
		{
			get { return base.Size; }
			set 
			{ 
				bool b= (base.Size != value);
				base.Size = value;
				if (b) { if (Root != null) Root.AutoLayout(); }
				this.Invalidate();
			}
		}
		public void SetSize(Size sz) { base.Size= sz; }
		public void SetSize(int w, int h) { base.Size = new Size(w,h); }

		public void SetWidth(int w) { base.Width = w; }
		public void SetHeight(int h) { base.Height = h; }
		[Category("Hypowered_layout"), Browsable(false)]
		public new int Width
		{
			get { return base.Width; }
			set
			{
				bool b = (base.Width != value);
				base.Width = value;
				if (b) { if (Root != null) Root.AutoLayout(); }
			}
		}
		[Category("Hypowered_layout"), Browsable(false)]
		public new int Height
		{
			get { return base.Height; }
			set
			{
				bool b = (base.Height != value);
				base.Height = value;
				if (b) { if (Root != null) Root.AutoLayout(); }
			}
		}
		protected Size m_BaseSize = new Size(80, 23);
		[Category("Hypowered_layout"), Browsable(true)]
		public Size BaseSize
		{
			get { return m_BaseSize; }
			set 
			{
				bool b = (m_BaseSize != value);
				m_BaseSize = value;
				if (b) { if (Root != null) Root.AutoLayout(); }
			}
		}
		public void SetBaseSize(int? w =null,int? h = null)
		{
			int ww=0;
			int hh = 0;
			if (w != null) ww = (int)w;  else ww = m_BaseSize.Width;
			if (h != null) hh = (int)h; else hh = m_BaseSize.Height;
			m_BaseSize = new Size(ww, hh);
		}
		protected SizePolicy m_SizePolicyHorizon = SizePolicy.Expanding;
		[Category("Hypowered_layout")]
		public SizePolicy SizePolicyHorizon
		{
			get { return m_SizePolicyHorizon; }
			set
			{
				bool b = (m_SizePolicyHorizon != value);
				m_SizePolicyHorizon = value;
				if ((b) && (Root != null)) Root.AutoLayout();
			}
		}
		protected SizePolicy m_SizePolicyVertual = SizePolicy.Fixed;
		[Category("Hypowered_layout")]
		public SizePolicy SizePolicyVertual
		{
			get
			{
				if((m_Item is ComboBox)|| (Multiline == false))
				{
					m_SizePolicyVertual = SizePolicy.Fixed;
					return SizePolicy.Fixed;
				}
				else
				{
					return m_SizePolicyVertual;
				}
			}
			set
			{
				if ((m_Item is ComboBox) || (Multiline == false))
				{
					value = SizePolicy.Fixed;
				}
				bool b = (m_SizePolicyVertual != value);
				m_SizePolicyVertual = value;
				if ((b) && (Root != null)) Root.AutoLayout();
			}
		}
		[Category("Hypowered_layout")]
		public new Padding Margin
		{
			get { return base.Margin; }
			set
			{
				bool b = (base.Margin != value);
				base.Margin = value;
				if ((b) && (Root != null)) Root.AutoLayout();
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
				if ((b) && (Root != null)) Root.AutoLayout();
			}
		}
		protected string m_Caption = "caption";
		[Category("Hypowered")]
		public string Caption
		{
			get { return m_Caption; }
			set
			{
				m_Caption = value;
				Invalidate();
			}
		}
		protected int m_CaptionWidth = 0;
		[Category("Hypowered")]
		public int CaptionWidth
		{
			get { return m_CaptionWidth; }
			set
			{
				int w = m_BaseSize.Width - m_CaptionWidth;

				m_CaptionWidth = value;
				m_BaseSize.Width = w + m_CaptionWidth;
				ChkSize();
				Invalidate();
			}
		}
		public virtual void ChkSize()
		{
			if(m_Item!=null)
			{
				m_Item.Location = new Point(m_CaptionWidth, 0);
				m_Item.Size = new Size(this.Width - m_CaptionWidth, this.Height);
				if (this.Height != m_Item.Height)
				{
					this.Height = m_Item.Height;
					m_BaseSize.Height = m_Item.Height;
				}
			}
		}
		[Category("Hypowered")]
		public new Font Font
		{
			get { return base.Font; }
			set
			{
				base.Font = value;
				if(m_Item!=null)
				{
					m_Item.Font = value;
					base.Size = new Size(m_Item.Width + m_CaptionWidth, m_Item.Height);
					if ((m_Item is HpdComboBox)||(Multiline==false))
					{
						m_BaseSize.Height = m_Item.Height;
					}
				}
			}
		}
		[Category("Hypowered_layout")]
		public new bool Visible
		{
			get { return base.Visible; }
			set
			{
				base.Visible = value;
				if (m_Item != null)
				{
					m_Item.Visible = value;
				}
				if (Root != null) Root.AutoLayout();
				this.Invalidate();
			}
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


		#endregion
		[Category("Hypowered")]
		public ListBox.ObjectCollection? ListBoxItems
		{
			get
			{
				ListBox.ObjectCollection? ret = null;
				if ((m_Item!=null)&&(m_Item is ListBox))
				{
					ret = ((ListBox)m_Item).Items;
				}
				return ret;
			}
		}
		[Category("Hypowered")]
		public ComboBox.ObjectCollection? ComboBoxItems
		{
			get
			{
				ComboBox.ObjectCollection? ret = null;
				if ((m_Item != null) && (m_Item is ComboBox))
				{
					ret = ((ComboBox)m_Item).Items;
				}
				return ret;
			}
		}
		[Category("Hypowered")]
		public int SelectedIndex
		{
			get
			{
				int ret = -1;
				if(m_Item!=null)
				{
					if(m_Item is ListBox) { ret = ((ListBox)m_Item).SelectedIndex; }
					else if (m_Item is ComboBox) { ret = ((ComboBox)m_Item).SelectedIndex; }
				}
				return ret;
			}
			set 
			{
				if (m_Item != null)
				{
					if (m_Item is ListBox) { ((ListBox)m_Item).SelectedIndex = value; }
					else if (m_Item is ComboBox) { ((ComboBox)m_Item).SelectedIndex=value; }
				}
			}
		}
		[Category("Hypowered_Text")]
		public bool Multiline
		{
			get 
			{
				TextBox? tb = AsTextBox; 
				if(tb!=null)
				{
					return tb.Multiline;
				}
				return true; 
			}
			set
			{
				TextBox? tb = AsTextBox;
				if (tb !=null)
				{
					tb.Multiline = value;
					if (tb.Multiline)
					{
						m_SizePolicyVertual = SizePolicy.Expanding;
					}
					else
					{
						m_SizePolicyVertual = SizePolicy.Fixed;
					}
					ChkSize();
					if (Root != null) Root.AutoLayout();
				}
				Invalidate();
			}
		}
		public virtual void SetHpdType(HpdType ht) 
		{
			m_HpdType = ht;

			switch(m_HpdType)
			{
				case HpdType.None:
					m_Item = null;
					this.Size = new Size(0, 0);
					m_BaseSize = new Size(0, 0);
					m_SizePolicyVertual = SizePolicy.Expanding;
					m_SizePolicyHorizon = SizePolicy.Expanding;
					break;
				case HpdType.Button:
					Button btn = new Button();
					m_Item = btn;
					break;
				case HpdType.TextBox:
					TextBox tb = new TextBox();
					m_SizePolicyVertual = SizePolicy.Fixed;
					m_Item = tb;
					break;
				case HpdType.ListBox:
					ListBox lb  = new ListBox();
					lb.IntegralHeight = false;
					m_Item = lb;
					break;
				case HpdType.ComboBox:
					ComboBox cb = new ComboBox();
					cb.DropDownStyle = ComboBoxStyle.DropDownList;
					cb.SelectedIndexChanged += (sender, e) => { OnSelectIndexChanged(e); };
					m_SizePolicyVertual = SizePolicy.Fixed;
					m_Item = cb;
					break;
				case HpdType.CheckBox:
					CheckBox cbx = new CheckBox();
					m_SizePolicyVertual = SizePolicy.Fixed;
					cbx.CheckedChanged += (sender,e)=> { OnCheckedChangedd(e); };
					m_Item = cbx;
					break;
				case HpdType.Panel:
					m_SizePolicyHorizon = SizePolicy.Expanding;
					m_SizePolicyVertual = SizePolicy.Expanding;
					m_Item = null;
					break;
				case HpdType.Stretch:
					m_SizePolicyHorizon = SizePolicy.Expanding;
					m_SizePolicyVertual = SizePolicy.Expanding;
					base.Padding = new Padding(0,0,0,0);
					base.Margin = new Padding(0, 0, 0, 0);
					m_Item = null;
					break;
			}

			if (m_Item != null)
			{
				m_Item.Font = base.Font;
				m_Item.Name = base.Name;
				m_Item.Text = base.Text;
				Size ps = ChkPreferredSize();
				this.Size = ps;
				m_BaseSize = ps;
				m_Item.Location = new Point(m_CaptionWidth, 0);
				this.Controls.Add(m_Item);
				m_Item.Click += (sender, e) => { OnClick(e); };
				m_Item.DoubleClick += (sender, e) => { OnDoubleClick(e); };
				ChkSize();
			}
		}


		public Size ChkPreferredSize()
		{
			Size ret;
			Size bak;
			if (m_Item != null)
			{
				string tx = m_Item.Text;
				m_Item.Text = "HpdControl";
				bak = m_Item.Size;
				m_Item.Size = new Size(0, 0);
				ret = m_Item.PreferredSize;
				if (ret.Width < 25 * 3) ret.Width = 25 * 3;
				if (ret.Height < 25) ret.Height = 25;
				ret.Width = ret.Width + m_CaptionWidth;
				m_Item.Size = bak;
				m_Item.Text = tx;
			}
			else
			{
				string tx = base.Text;
				base.Text = "HpdControl";
				bak = base.Size;
				base.Size = new Size(0, 0);
				ret = base.PreferredSize;
				if (ret.Width <25*3) ret.Width = 25 * 3;
				if (ret.Height < 25) ret.Height = 25;
				base.Size = bak;
				base.Text = tx;
			}
			return ret;
		}
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if(m_Item!=null) m_Item.Focus();
		}
		public HpdControl()
		{
			ScriptCode.SetSTypes(ScriptTypeBit.None);
			this.SetStyle(
				ControlStyles.Selectable |
				ControlStyles.UserMouse |
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.UpdateStyles();
			InitializeComponent();
			ChkSize();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(BackColor))
			{
				Graphics g = pe.Graphics;
				g.FillRectangle(sb, this.ClientRectangle);
				if (m_IsDrawFrame)
				{
					using (Pen p = new Pen(ForeColor))
					{
						Rectangle r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
						g.DrawRectangle(p, r);
					}
				}
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
		}
		private Control.ControlCollection? ParentControls()
		{
			Control.ControlCollection? cl = null;
			if (this.Parent is HpdPanel) cl = ((HpdPanel)this.Parent).Controls;
			else if (this.Parent is HpdForm) cl = ((HpdForm)this.Parent).Controls;
			else if (this.Parent is Form) cl = ((HpdForm)this.Parent).Controls;
			else if (this.Parent is Control) cl = ((Control)this.Parent).Controls;
			return cl;
		}
		public bool ListMoveUp()
		{
			bool ret = false;
			Control.ControlCollection? cl = ParentControls();
			if (cl == null) return ret;

			int idx = cl.GetChildIndex(this);
			if (idx > 0)
			{
				cl.SetChildIndex(this, idx - 1);
				if (Root != null) Root.AutoLayout();
				ret = true;
			}
			return ret;
		}
		public bool ListMoveDown()
		{
			bool ret = false;
			Control.ControlCollection? cl = ParentControls();
			if (cl == null) return ret;

			int idx = cl.GetChildIndex(this);
			if (idx < cl.Count-1)
			{
				cl.SetChildIndex(this, idx + 1);
				if (Root != null) Root.AutoLayout();
				ret = true;
			}
			return ret;
		}
		public bool RemoveMe()
		{
			bool ret = false;
			Control.ControlCollection? cl = ParentControls();
			if (cl == null) return ret;
			cl.Remove(this);
			return ret;
		}
	}
}
