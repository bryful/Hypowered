using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	
	partial class HyperMainForm
	{
		// ****************************************************************************
		public HyperPropForm? PropForm = null;
		public HyperControlList? ControlList = null;
		// ****************************************************************************
		protected HyperScript m_Script = new HyperScript();
		public void ExecuteCode(string code)
		{
			if (code != "")
			{

				m_Script.ExecuteCode(code);
			}
		}

		// ****************************************************************************
		protected override bool ProcessDialogKey(Keys keyData)
		{
#if DEBUG
			this.Text = String.Format("{0}", keyData.ToString());
#endif
			FuncItem? fi = Funcs.FindKeys(keyData);
			if ((fi != null) && (fi.Func != null))
			{
				if (fi.Func()) this.Invalidate();
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}
	}
}
