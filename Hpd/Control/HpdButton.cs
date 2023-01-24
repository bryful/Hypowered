using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hpd
{
	public partial class HpdButton : HpdControl
	{
		protected Button m_Button = new Button();
		[Category("Hypowered")]
		public Button Button
		{
			get { return m_Button; }
			set { m_Button = value; }
		}
		public override void SetIsEdit(bool b) 
		{
			m_IsEdit = b;
			m_Button.Visible = !b;
			this.Invalidate();
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
					m_Button.Name= value;
					OnNameChanged(new EventArgs());
				}
			}
		}
		[Category("Hypowered"), Browsable(true)]
		public new string Text
		{
			get { return m_Button.Text; }
			set 
			{
				base.Text = value;
				m_Button.Text = value;
			}
		}
		[Category("Hypowered"), Browsable(true)]
		public new bool TabStop
		{
			get { return m_Button.TabStop; }
			set
			{
				base.TabStop = value;
				m_Button.TabStop = value;
			}
		}
		public DialogResult DialogResult
		{
			get { return m_Button.DialogResult; }
			set { m_Button.DialogResult = value; }
		}
		public HpdButton()
		{
			SetHpdType(HpdType.Button);
			this.Size = new Size(100, 27);
			m_Button.Location = new Point(0, 0);
			m_Button.Size = this.Size;
			this.Size = m_Button.Size;
			m_SizeDef = this.Size;
			m_SizeDef = m_Button.Size;
			m_Button.Name = "button";
			m_Button.Text= base.Text;
			this.Controls.Add(m_Button);
			InitializeComponent();
			m_Button.Click += (sender, e) => { OnClick(e); };

			//HpdA.PropListToClipboard(typeof(Button),nameof(m_Button));
			this.GotFocus += (sendet, e) => { m_Button.Focus(); };
		}


		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			m_Button.Size = this.Size;
		}
	}
}
