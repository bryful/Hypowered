using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Hypowered
{
	internal class HUtils
	{
		/*
		static public void PropListToClipboard(Type t, string nm)
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
		*/
		static public void PropToClipboard(Type t)
		{
			string s = "";
			foreach (var pi in t.GetProperties())
			{

				s += $"[Category(\"Hypowered\"), Browsable(false)]\r\n";
				s += $"public new {pi.PropertyType.FullName} {pi.Name}\r\n";
				s += $"{{\r\n";
				s += $"\tget {{ return base.{pi.Name}; }}\r\n";
				s += $"\tset {{ base.{pi.Name} = value; }}\r\n";
				s += $"}}\r\n";
			}

			Clipboard.SetText(s);
		}
	}
}
