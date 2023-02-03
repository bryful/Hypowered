using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hpd
{
	partial class HpdControl
	{
		public bool Edit()
		{
			bool ret = false;
			switch(m_HpdType)
			{
				case HpdType.TextBox:
					HpdTextBox? lb = AsHpdTextBox;
					if(lb != null)
					{
						using(TextEditForm dlg = new TextEditForm())
						{
							dlg.Title = lb.Name;
							dlg.Lines = lb.Lines;
							if(dlg.ShowDialog() == DialogResult.OK)
							{
								lb.Lines = dlg.Lines;
								ret = true;
							}
						}
					}
					break;
				case HpdType.None:
				default:
					break;
			}
			return ret;
		}
	}
}
