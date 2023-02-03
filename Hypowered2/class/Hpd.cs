using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Hpd
{
    public class HpdA
    {
		[DllImport("user32.dll")]
		static extern bool SetSysColors(int cElements, int[] lpaElements, uint[] lpaRgbValues);
	
		static public void PropListToClipboard(Type t,string nm)
		{
			string s = "";
			foreach (MemberInfo mi in t.GetMembers())
			{
				if (mi.MemberType == MemberTypes.Event)
				{
					s += $"//{nm}.{mi.Name}+=(sender,e)=>{{ On{mi.Name}(e);}};\r\n";
				}
			}
			s += "********************************\r\n";
			foreach (var pi in t.GetProperties())
			{

				s += $"[Category(\"Hypowered\")]\r\n";
				s += $"public {pi.PropertyType.FullName} {pi.Name}\r\n";
				s += $"{{\r\n";
				s += $"\tget {{ return {nm}.{pi.Name}; }}\r\n";
				s += $"\tset {{ {nm}.{pi.Name} = value; }}\r\n";
				s += $"}}\r\n";
			}


			Clipboard.SetText(s);
		}
    }
}
