using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Drawing.Imaging;

namespace Hypowered
{
	public class PropUtil
	{
		static public string GetPropList(object obj)
		{
			string ret = "";

			PropertyInfo[] lst = obj.GetType().GetProperties();
			foreach (PropertyInfo pi in lst)
			{
				ret += $"jf.SetValue(nameof({pi.Name}), {pi.Name});//{pi.PropertyType.Name}\r\n";
			}

			ret += "// *************************************\r\n";
			foreach (PropertyInfo pi in lst)
			{
				ret += $"v = jf.ValueAuto(\"{pi.Name}\", typeof({pi.PropertyType.Name}).Name);\r\n" +
				$"if (v != null) {pi.Name} = ({pi.PropertyType.Name})v;\r\n";
			}
			return ret;

		}
	}
}
