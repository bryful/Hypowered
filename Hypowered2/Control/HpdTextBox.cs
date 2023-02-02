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
	public partial class HpdTextBox : HpdControl
	{
		[Category("Hypowered_TextBox")]
		public string[] Lines
		{
			get
			{
				if (m_Item != null)
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
		[Category("Hypowered_TextBox")]
		public bool Multiline
		{
			get
			{
				TextBox? tb = m_Item as TextBox;
				if (tb != null)
				{
					return tb.Multiline;
				}
				return true;
			}
			set
			{
				TextBox? tb = m_Item as TextBox;
				if (tb != null)
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
					if (MainForm != null) MainForm.AutoLayout();
				}
				Invalidate();
			}
		}
		[Category("Hypowered_TextBox")]
		public ScrollBars ScrollBars
		{
			get
			{
				TextBox? tb = m_Item as TextBox;
				if (tb != null)
				{
					return tb.ScrollBars;
				}
				return ScrollBars.None;
			}
			set
			{
				TextBox? tb = m_Item as TextBox;
				if (tb != null)
				{
					tb.ScrollBars = value;
				}
				Invalidate();
			}
		}

		public HpdTextBox()
		{
			SetHpdType(HpdType.TextBox);
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
			if (m_CaptionWidth > 0)
			{
				using (SolidBrush sb = new SolidBrush(ForeColor))
				{
					Rectangle r = new Rectangle(0, 0, m_CaptionWidth, this.Height);
					pe.Graphics.DrawString(m_Caption, this.Font, sb, r, m_StringFormat);
				}
			}
		}
		
	}
}
