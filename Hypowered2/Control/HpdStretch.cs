using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hpd
{
	public partial class HpdStretch : HpdControl
	{
		//protected SizePolicy m_SizePolicyHorizon = SizePolicy.Expanding;
		[Category("Hypowered_Text"), Browsable(false)]
		public new Padding Margin
		{
			get { return base.Margin; }
			set
			{
			}
		}
		[Category("Hypowered_Text"), Browsable(false)]
		public new Padding Padding
		{
			get { return base.Padding; }
			set
			{
			}
		}
		[Category("Hypowered_Text"), Browsable(false)]
		public new SizePolicy SizePolicyHorizon
		{
			get { return SizePolicy.Expanding; }
			set
			{
				m_SizePolicyHorizon = SizePolicy.Expanding;
			}
		}
		[Category("Hypowered_Text"), Browsable(false)]
		public new SizePolicy SizePolicyVertual
		{
			get { return SizePolicy.Expanding; }
			set
			{
				m_SizePolicyVertual = SizePolicy.Expanding;
			}
		}

		[Category("Hypowered_Text"),Browsable(false)]
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
