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
	public partial class HpdRadioButton : HpdControl
	{
		public HpdRadioButton()
		{
			SetHpdType(HpdType.RadioButton);
			InitializeComponent();
		}

	}
}
