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
    public class HpdA
    {
        static public HpdControl CreateControl(
            string name,
            string txt
            , HpdType ht)
        {
            HpdControl ret;
            switch (ht)
            {
				case HpdType.Panel:
					ret = new HpdPanel();
					ret.Name = name;
					ret.Text = txt;
					ret.Size = new Size(120, 300);
					ret.Location = new Point(80, 80);
					break;
				case HpdType.Button:
				default:
					ret = new HpdButton();
					ret.Name = name;
					ret.Text = txt;
					ret.Size = new Size(120, 25);
					ret.Location = new Point(100, 100);
					break;
			}
			return ret;
        }
    }
}
