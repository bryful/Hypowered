﻿using System;
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
		public HpdComboBox()
		{
			SetHpdType(HpdType.ComboBox);
			InitializeComponent();
		}
		
	}
}
