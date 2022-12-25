using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public class App
	{
		public void alert(string s)
		{
			MessageBox.Show(s);
		}
		public HyperForm? Project { get; set; }

		public Control.ControlCollection? Controls  { get; set; }

	}


}
