using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hpd
{
	public partial class HpdStretch : HpdControl
	{
		//protected SizePolicy m_SizePolicyHorizon = SizePolicy.Expanding;
		[Category("Hypowered_layout")]
		public new SizePolicy SizePolicyHorizon
		{
			get { return SizePolicy.Expanding; }
			set
			{
				m_SizePolicyHorizon = SizePolicy.Expanding;
			}
		}
		[Category("Hypowered_layout")]
		public new SizePolicy SizePolicyVertual
		{
			get { return SizePolicy.Expanding; }
			set
			{
				m_SizePolicyVertual = SizePolicy.Expanding;
			}
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
		public HpdStretch()
		{
			SetHpdType(HpdType.Stretch);
			m_BaseSize = new Size(0, 0);
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
	}
}
