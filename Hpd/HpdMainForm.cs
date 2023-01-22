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
	public partial class HpdMainForm : HpdForm
	{
		public HpdScript Script = new HpdScript();
		public HpdScriptCode ScriptCode = new HpdScriptCode();
		public HpdApp App = new HpdApp();

		public HpdMainForm()
		{
			App.SetMainForm(this);
			InitializeComponent();
		}
	}
}
