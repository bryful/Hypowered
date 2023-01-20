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
		}
		public override void SetIsEdit(bool b) 
		{
			m_IsEdit = b;
			m_Button.Visible = !b;
			this.Invalidate();
		}

		public HpdButton()
		{
			SetHpdType(HpdType.Button);
			this.Size = new Size(100, 27);
			m_Button.Location = new Point(0, 0);
			m_Button.Size = new Size(this.Width, this.Height);
			m_Button.Name = "button";
			m_Button.Text= "button";
			this.Controls.Add(m_Button);
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			m_Button.Size = new Size(this.Width, this.Height);
		}
	}
}
