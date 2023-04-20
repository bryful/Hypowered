using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class HRootMenu : HMenuItem
	{

		public HMenuItem[] SubMenu { get; } = new HMenuItem[10];
		public HRootMenu() 
		{
			for (int i =0; i< 10; i++)
			{
				SubMenu[i] = new HMenuItem();
				SubMenu[i].Name = $"Menu{i}";
				SubMenu[i].Text = $"Menu{i}";
				SubMenu[i].Visible = false;
				this.DropDownItems.Add(SubMenu[i]);
				SubMenu[i].Visible = false;
			}
		}
	}
}
