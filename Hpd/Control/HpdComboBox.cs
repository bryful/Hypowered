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

namespace Hpd
{
	public partial class HpdComboBox : HpdControl
	{
		protected ComboBox m_comb = new ComboBox();
		protected List<object?> m_items = new List<object?>();
		[Category("Hypowered")]
		public ComboBox ComboBox
		{
			get { return m_comb; }
			set { m_comb = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new string Name
		{
			get { return base.Name; }
			set
			{
				if (base.Name != value)
				{
					base.Name = value;
					m_comb.Name = value;
					OnNameChanged(new EventArgs());
				}
			}
		}
		[Category("Hypowered")]
		public ComboBox.ObjectCollection Items
		{
			get { return m_comb.Items; }
		}
		[Category("Hypowered")]
		public ComboBoxStyle DropDownStyle
		{
			get { return m_comb.DropDownStyle; }
			set { m_comb.DropDownStyle = value;}
		}
		[Category("Hypowered")]
		public int SelectedIndex
		{
			get { return m_comb.SelectedIndex; }
			set { m_comb.SelectedIndex = value; }
		}
		[Category("Hypowered")]
		public object? SelectedItem
		{
			get
			{
				object? ret = null;
				if (m_comb.SelectedIndex>=0)
				{
					ret =  (object?)m_comb.Items[m_comb.SelectedIndex];
				}
				return ret;
			}
			set {
				if (m_comb.SelectedIndex >= 0)
				{
					m_comb.Items[m_comb.SelectedIndex] = value;
				}
			}
		}
		[Category("Hypowered")]
		public new Font Font
		{
			get { return m_comb.Font; }
			set { 
				base.Font = value;
				m_comb.Font = value;
				this.Size= m_comb.Size;
			}
		}
		private string m_Caption = "caption";
		[Category("Hypowered")]
		public  string Caption
		{
			get { return m_Caption; }
			set
			{
				m_Caption = value;
				Invalidate();
			}
		}
		public override void SetIsEdit(bool b)
		{
			m_IsEdit = b;
			m_comb.Visible = !b;
			this.Invalidate();
		}
		private int m_CaptionWidth = 0;
		[Category("Hypowered")]
		public int CaptionWidth
		{
			get { return m_CaptionWidth; }
			set
			{
				m_CaptionWidth = value;
				ChkSize();
				Invalidate();
			}
		}
		[Category("Hypowered"), Browsable(true)]
		public new bool TabStop
		{
			get { return m_comb.TabStop; }
			set
			{
				base.TabStop = value;
				m_comb.TabStop = value;
			}
		}
		public HpdComboBox()
		{
		
			SetHpdType(HpdType.ComboBox);
			this.Size = new Size(120, 23);
			m_comb.Size = new Size(this.Width- m_CaptionWidth,this.Height);
			m_comb.Location=new Point(m_CaptionWidth,0);
			m_comb.Name = "ComboBox";
			m_comb.Text = "ComboBox";
			m_comb.DrawMode = DrawMode.OwnerDrawFixed;
			m_comb.ItemHeight = 20;
			m_comb.DrawItem += M_comb_DrawItem;
			this.Controls.Add(m_comb);
			InitializeComponent();
			ChkSize();
			m_SizeDef = this.Size;
			this.GotFocus += (sendet, e) => { m_comb.Focus(); };
		}

		private void M_comb_DrawItem(object? sender, DrawItemEventArgs e)
		{
			e.DrawBackground();

			ComboBox? cmb = (ComboBox?)sender;
			if (cmb != null)
			{
				using (SolidBrush sb = new SolidBrush(cmb.ForeColor))
				{
					string txt = cmb.Text;
					if((e.Index >=0)&&(e.Index<cmb.Items.Count))
					{
						if (cmb.Items[e.Index] is CombItem)
						{
							txt = ((CombItem)cmb.Items[e.Index]).Name;
						}
						else
						{
							txt = cmb.Items[e.Index].ToString();
						}
					}
					float ym =
						(e.Bounds.Height - e.Graphics.MeasureString(txt, cmb.Font).Height) / 2;
					e.Graphics.DrawString(txt, cmb.Font, sb, e.Bounds.X, e.Bounds.Y + ym);
				}
				//フォーカスを示す四角形を描画
				e.DrawFocusRectangle();
			}
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
			if (m_CaptionWidth > 0)
			{
				using (SolidBrush sb = new SolidBrush(ForeColor))
				{
					Rectangle r = new Rectangle(0, 0, m_CaptionWidth, this.Height);
					pe.Graphics.DrawString(m_Caption, this.Font, sb, r, m_format);
				}
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();

		}
		public void ChkSize()
		{
			m_comb.Location = new Point(m_CaptionWidth, 0);
			m_comb.Size = new Size(this.Width - m_CaptionWidth, this.Height);
			if (this.Height != m_comb.Height) this.Height = m_comb.Height;
		}
	}

	public class CombItem
	{
		public string Name { get; set; } = "";
		public Object? obj { get; set; } = null;
		public CombItem()
		{

		}
		public CombItem(string n,object? o)
		{
			Name = n;
			obj = o;
		}
		public new string ToString()
		{
			return Name;
		}
	}
}
