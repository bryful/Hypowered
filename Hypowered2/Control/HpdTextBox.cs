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
					pe.Graphics.DrawString(m_Caption, this.Font, sb, r, m_format);
				}
			}
		}
		
	}
}
