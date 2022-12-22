using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class HyperTextBox : HyperControl
	{

		public override void SetIsEditMode(bool value)
		{
			base.SetIsEditMode(value);
			m_TextBox.Visible = !m_IsEditMode;
		}
		private TextBox m_TextBox = new TextBox();
		[Category("Hypowerd_TextBox")]
		public TextBox TextBox
		{
			get { return m_TextBox; }
			set { m_TextBox = value; }
		}
		[Category("Hypowerd_TextBox")]
		public new string Text
		{
			get { return m_TextBox.Text; }
			set { m_TextBox.Text = value; }
		}
		[Category("Hypowerd")]
		public new Font Font
		{
			get { return m_TextBox.Font; }
			set
			{
				base.Font =
				m_TextBox.Font = value;
				this.Size = new Size(m_TextBox.Width + 2, m_TextBox.Height + 2);
			}
		}
		[Category("Hypowerd_TextBox")]
		public bool ReadOnly
		{
			get { return m_TextBox.ReadOnly; }
			set
			{
				m_TextBox.ReadOnly = value;
			}
		}
		[Category("Hypowerd_Text")]
		public HorizontalAlignment TextAlign
		{
			get { return m_TextBox.TextAlign; }
			set
			{
				m_TextBox.TextAlign = value;
			}
		}
		[Category("Hypowerd_Color")]
		public new Color ForeColor
		{
			get { return m_TextBox.ForeColor; }
			set
			{
				base.ForeColor = value;
				m_TextBox.ForeColor = value;
			}
		}
		[Category("Hypowerd_Color")]
		public new Color BackColor
		{
			get { return m_TextBox.BackColor; }
			set
			{
				base.BackColor = value;
				m_TextBox.BackColor = value;
			}
		}
		public HyperTextBox()
		{
			SetMyType(ControlType.TextBox);
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			m_TextBox.BorderStyle = BorderStyle.FixedSingle;

			this.Size = new Size(m_TextBox.Width+2,m_TextBox.Height+2);
			m_TextBox.Location = new Point(1, 1);
			InitializeComponent();
			this.Controls.Add(m_TextBox);
			this.Controls.SetChildIndex(m_TextBox, 0);
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			if (m_IsEditMode)
			{
				base.OnPaint(pe);
			}
			else
			{
				pe.Graphics.Clear(BackColor);
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.Size = new Size(this.Size.Width, m_TextBox.Height + 2);
			m_TextBox.Size = new Size(this.Size.Width - 2, m_TextBox.Height);
		}
	}
}
