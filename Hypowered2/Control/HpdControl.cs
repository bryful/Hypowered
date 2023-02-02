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
		Label,
		ComboBox,
		ListBox,
		CheckBox,
		RadioButton,
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
		/// <summary>
		/// その名の通りスクリプトコード
		/// </summary>
		[Category("Hypowered"),Browsable(false)]
		public HpdScriptCode ScriptCode { get; set; }= new HpdScriptCode();

		protected Control? m_Item = null;
		// **************************************************************
		// **************************************************************
		[Category("Hypowered"), Browsable(false)]
		public HpdButton? AsHpdButton
		{
			get
			{
				HpdButton? ret = null;
				if (m_Item is not null and Button) ret = (HpdButton?)this;
				return ret;
			}
		}
		[Category("Hypowered"), Browsable(false)]
		public HpdTextBox? AsHpdTextBox
		{
			get
			{
				HpdTextBox? ret = null;
				if (m_Item is not null and TextBox) ret = (HpdTextBox?)this;
				return ret;
			}
		}
		[Category("Hypowered"), Browsable(false)]
		public HpdComboBox? AsHpdComboBox
		{
			get
			{
				HpdComboBox? ret = null;
				if (m_Item is not null and ComboBoxHpd) ret = (HpdComboBox?)this;
				return ret;
			}
		}
		[Category("Hypowered"), Browsable(false)]
		public HpdListBox? AsHpdListBox
		{
			get
			{
				HpdListBox? ret = null;
				if (m_Item is not null and ListBoxHpd) ret = (HpdListBox?)this;
				return ret;
			}
		}
		[Category("Hypowered"), Browsable(false)]
		public HpdCheckBox? AsHpdCheckBox
		{
			get
			{
				HpdCheckBox? ret = null;
				if (m_Item is not null and CheckBox) ret = (HpdCheckBox?)this;
				return ret;
			}
		}
		[Category("Hypowered"), Browsable(false)]
		public HpdRadioButton? AsHpdRadioButton
		{
			get
			{
				HpdRadioButton? ret = null;
				if (m_Item is not null and RadioButton) ret = (HpdRadioButton?)this;
				return ret;
			}
		}
		protected HpdMainForm? m_MainForm = null;
		[Category("Hypowered"), Browsable(false)]
		public HpdMainForm? MainForm
		{
			get
			{
				Control? ret = m_MainForm;
				if (m_MainForm == null)
				{
					ret  = (Control?)this.Parent;
					while ((ret != null) && (ret.Parent != null))
					{
						if (ret is HpdMainForm) break;
						ret = ret.Parent;
					}
					m_MainForm = (HpdMainForm?)ret;
				}

				return m_MainForm;
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
		protected Size m_PreferredSize = new Size(0, 0);
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
					if ((m_PreferredSize.Width > 0)&&(m_PreferredSize.Height > 0))
					{

						return m_PreferredSize;
					}
					else
					{
						return base.PreferredSize;
					}
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
				if (b) { if (MainForm != null) MainForm.AutoLayout(); }
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
				if (b) { if (MainForm != null) MainForm.AutoLayout(); }
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
				if (b) { if (MainForm != null) MainForm.AutoLayout(); }
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
				if (b) { if (MainForm != null) MainForm.AutoLayout(); }
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
				if ((b) && (MainForm != null)) MainForm.AutoLayout();
			}
		}
		protected SizePolicy m_SizePolicyVertual = SizePolicy.Fixed;
		[Category("Hypowered_layout")]
		public SizePolicy SizePolicyVertual
		{
			get
			{
				if(
					(m_Item is ComboBoxHpd)||
					(m_Item is CheckBox) ||
					(m_Item is RadioButton) ||
					((m_Item is TextBox)&&(((TextBox)m_Item).Multiline == false)))
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
				if (
					(m_Item is ComboBoxHpd) ||
					(m_Item is CheckBox) ||
					(m_Item is RadioButton) ||
					((m_Item is TextBox) && (((TextBox)m_Item).Multiline == false)))
				{
					value = SizePolicy.Fixed;
				}
				bool b = (m_SizePolicyVertual != value);
				m_SizePolicyVertual = value;
				if ((b) && (MainForm != null)) MainForm.AutoLayout();
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
				if ((b) && (MainForm != null)) MainForm.AutoLayout();
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
				if ((b) && (MainForm != null)) MainForm.AutoLayout();
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
					if(MainForm!=null) MainForm.AutoLayout();
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
					if ((m_Item is ComboBoxHpd)
						||((m_Item is TextBox)&&( ((TextBox)m_Item).Multiline==false)))
					{
						if (m_BaseSize.Height == m_Item.Height)
						{
							m_BaseSize.Height = m_Item.Height;
							if (MainForm != null) MainForm.AutoLayout();
						}
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
					if (MainForm != null) MainForm.AutoLayout();
				}
				this.Invalidate();
			}
		}

		protected StringFormat m_StringFormat = new StringFormat();
		[Category("Hypowered_Text")]
		public StringAlignment TextAligiment
		{
			get { return m_StringFormat.Alignment; }
			set { m_StringFormat.Alignment = value; this.Invalidate(); }
		}
		[Category("Hypowered_Text")]
		public StringAlignment TextLineAligiment
		{
			get { return m_StringFormat.LineAlignment; }
			set { m_StringFormat.LineAlignment = value; this.Invalidate(); }
		}


		#endregion
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
				case HpdType.Label:
					this.Size = new Size(80, 23);
					m_SizePolicyVertual = SizePolicy.Fixed;
					m_Item = null;
					break;
				case HpdType.ListBox:
					ListBoxHpd lb  = new ListBoxHpd();
					lb.IntegralHeight = false;
					lb.SelectedIndexChanged += (sender, e) =>
					{
						OnValueChanged(new ValueChangedEventArgs(lb.SelectedIndex));
					};
					m_Item = lb;
					break;
				case HpdType.ComboBox:
					ComboBoxHpd cb = new ComboBoxHpd();
					cb.SelectedIndexChanged += (sender, e) =>
					{
						OnValueChanged(new ValueChangedEventArgs(cb.SelectedIndex));
					};
					m_SizePolicyVertual = SizePolicy.Fixed;
					m_Item = cb;
					break;
				case HpdType.CheckBox:
					CheckBox cbx = new CheckBox();
					m_SizePolicyVertual = SizePolicy.Fixed;
					cbx.CheckedChanged += (sender,e)=> 
					{
						OnValueChanged(new ValueChangedEventArgs((object)cbx.Checked)); 
					};
					m_Item = cbx;
					break;
				case HpdType.RadioButton:
					RadioButton rb = new RadioButton();
					m_SizePolicyVertual = SizePolicy.Fixed;
					rb.CheckedChanged += (sender, e) =>
					{
						OnValueChanged(new ValueChangedEventArgs((object)rb.Checked));
					}; 
					m_Item = rb;
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
				if(m_HpdType== HpdType.Label)
				{
					if (ret.Width < 50) ret.Width = 50;
					if (ret.Height < 23) ret.Height = 23;
				}
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
			m_StringFormat.LineAlignment= StringAlignment.Center;
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
		protected HpdRadioButton[] GetHpdRadioButtons()
		{
			List<HpdRadioButton> list = new List<HpdRadioButton>();
			Control.ControlCollection? cl = ParentControls();
			if ((cl!=null)&&(cl.Count>0))
			{
				foreach(Control c in cl)
				{
					if(c is HpdRadioButton)
					{
						list.Add((HpdRadioButton)c);
					}
				}
			}
			return list.ToArray();
		}
		public bool ControlMoveUp()
		{

			if (this.Parent is HpdForm)
			{
				return ((HpdForm)this.Parent).ControlMoveUp(this);
			}else if (this.Parent is HpdPanel)
			{
				return ((HpdPanel)this.Parent).ControlMoveUp(this);
			}
			else
			{
				return false;
			}
		}
		public bool ControlMoveDown()
		{
			if (this.Parent is HpdForm)
			{
				return ((HpdForm)this.Parent).ControlMoveDown(this);
			}
			else if (this.Parent is HpdPanel)
			{
				return ((HpdPanel)this.Parent).ControlMoveDown(this);
			}
			else
			{
				return false;
			}
		}
		public bool RemoveMe()
		{
			if (this.Parent is HpdForm)
			{
				return ((HpdForm)this.Parent).ControlRemove(this);
			}
			else if (this.Parent is HpdPanel)
			{
				return ((HpdPanel)this.Parent).ControlRemove(this);
			}
			else
			{
				return false;
			}
		}

	}
}
