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
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
		static public string JsonType(Type t)
		{
			string ret = "";
			switch (t.Name)
			{
				case "Int32":
					ret = "(Int32)";
					break;
				case "Float":
					ret = "(Float)";
					break;
				case "Double":
					ret = "(Double)";
					break;
				case "String":
					ret = "(String)";
					break;
				case "Boolean":
					ret = "(Boolean)";
					break;
				case "Point":
					ret = "(Point)";
					break;
				case "Size":
					ret = "(Size)";
					break;
				case "SizeF":
					ret = "(SizeF)";
					break;
				case "Padding":
					ret = "(Padding)";
					break;
				case "Rectangle":
					ret = "(Rectangle)";
					break;
				case "Font":
					ret = "(Font)";
					break;
				case "Color":
					ret = "(Color)";
					break;
				case "DialogResult":
				case "FlatStyle":
				case "SizePolicy":
				case "SizePolicyVertual":
				case "StringAlignment":
				case "AnchorStyles":
				case "DockStyle":
				case "AccessibleRole":
				case "RightToLeft":
				case "ImeMode":
				case "Orientation":
				case "AutoSizeMode":
				case "AutoValidate":
				case "SizeGripStyle":
				case "FormWindowState":
				case "AutoScaleMode":
				case "FormBorderStyle":
				case "FormStartPosition":
					ret = "(int)";
					break;
			}
			
			return ret;
		}
		static public void ToJsonCodeToClipboard(Type t)
		{
			string s = "";
			s += "// **********************************************************\r\n";
			foreach (var pi in t.GetProperties())
			{
				if ((pi.CanRead == false) || (pi.CanWrite == false)) continue;
				if (pi.PropertyType.Name == "HpdMainMenu") continue;
				if (pi.PropertyType.Name == "Image") continue;
				if (pi.PropertyType.Name == "ContextMenuStrip") continue;
				if (pi.PropertyType.Name == "Object") continue;
				if (pi.PropertyType.Name == "Cursor") continue;
				if (pi.PropertyType.Name == "BindingContext") continue;
				if (pi.PropertyType.Name == "Control") continue;
				if (pi.PropertyType.Name == "Region") continue;
				if (pi.PropertyType.Name == "ISite") continue;
				if (pi.PropertyType.Name == "IWindowTarget") continue;
				if (pi.PropertyType.Name == "MainMenu") continue;
				if (pi.PropertyType.Name == "Icon") continue;
				if (pi.PropertyType.Name == "Form") continue;
				if (pi.PropertyType.Name == "MenuStrip") continue;
				if (pi.PropertyType.Name == "ImageLayout") continue;
				if (pi.PropertyType.Name == "IWindowTarget") continue;
				if (pi.PropertyType.Name == "IButtonControl") continue;

				s += $"jf.SetValue(nameof({pi.Name}), {JsonType(pi.PropertyType)}{pi.Name});//{pi.PropertyType.FullName}\r\n";
			}
			s += "// **********************************************************\r\n";
			foreach (var pi in t.GetProperties())
			{
				if ((pi.CanRead == false) || (pi.CanWrite == false)) continue;
				if (pi.PropertyType.Name == "HpdMainMenu") continue;
				if (pi.PropertyType.Name == "Image") continue;
				if (pi.PropertyType.Name == "ContextMenuStrip") continue;
				if (pi.PropertyType.Name == "Object") continue;
				if (pi.PropertyType.Name == "Cursor") continue;
				if (pi.PropertyType.Name == "BindingContext") continue;
				if (pi.PropertyType.Name == "Control") continue;
				if (pi.PropertyType.Name == "Region") continue;
				if (pi.PropertyType.Name == "ISite") continue;
				if (pi.PropertyType.Name == "IWindowTarget") continue;
				if (pi.PropertyType.Name == "MainMenu") continue;
				if (pi.PropertyType.Name == "Icon") continue;
				if (pi.PropertyType.Name == "Form") continue;
				if (pi.PropertyType.Name == "MenuStrip") continue;
				if (pi.PropertyType.Name == "ImageLayout") continue;
				if (pi.PropertyType.Name == "IWindowTarget") continue;
				if (pi.PropertyType.Name == "IButtonControl") continue;

				s += $"v = jf.ValueAuto(\"{pi.Name}\", typeof{JsonType(pi.PropertyType)}.Name);\r\n";
				s += $"if (v != null) {pi.Name} = ({pi.PropertyType.Name})v;\r\n";
			}

			Clipboard.SetText(s);
		}
	}
}
