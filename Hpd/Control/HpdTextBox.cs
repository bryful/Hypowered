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
		protected TextBox m_TextBox = new TextBox();
		[Category("Hypowered")]
		public TextBox TextBox
		{
			get { return m_TextBox; }
			set { m_TextBox = value; }
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
					m_TextBox.Name = value;
					OnNameChanged(new EventArgs());
				}
			}
		}
		[Category("Hypowered")]
		public new Font Font
		{
			get { return m_TextBox.Font; }
			set
			{
				base.Font = value;
				m_TextBox.Font = value;
				this.Size = m_TextBox.Size;
			}
		}
		public override void SetIsEdit(bool b)
		{
			m_IsEdit = b;
			m_TextBox.Visible = !b;
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
		private string m_Caption = "caption";
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
		[Category("Hypowered")]
		public new string Text
		{
			get { return m_TextBox.Text; }
			set
			{
				m_TextBox.Text = value;
				Invalidate();
			}
		}
		[Category("Hypowered")]
		public new string[] Lines
		{
			get { return m_TextBox.Text.Split("\r\n"); }
			set
			{
				m_TextBox.Text = string.Join("\r\n", value);
				Invalidate();
			}
		}
		[Category("Hypowered")]
		public  bool Multiline
		{
			get { return m_TextBox.Multiline; }
			set
			{
				m_TextBox.Multiline= value;
				ChkSize();
				Invalidate();
			}
		}
		[Category("Hypowered"), Browsable(true)]
		public new bool TabStop
		{
			get { return m_TextBox.TabStop; }
			set
			{
				base.TabStop = value;
				m_TextBox.TabStop = value;
			}
		}
		public HpdTextBox()
		{
			SetHpdType(HpdType.TextBox);
			this.Size = new Size(120, 23);
			m_TextBox.Size = this.Size;
			m_TextBox.Location = new Point(0, 0);
			m_TextBox.Name = "TextBox";
			m_TextBox.Text = "TextBox";
			this.Controls.Add(m_TextBox);
			InitializeComponent();
			ChkSize();
			m_SizeDef = this.Size;
			this.GotFocus += (sendet, e) => { m_TextBox.Focus(); };
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
			m_TextBox.Location = new Point(m_CaptionWidth, 0);
			m_TextBox.Size = new Size(this.Width - m_CaptionWidth, this.Height);
			if (this.Height != m_TextBox.Height) this.Height = m_TextBox.Height;
		}
	}
}
