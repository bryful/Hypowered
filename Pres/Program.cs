using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Permissions;

namespace Pres
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
			List<string> list = new List<string>();
			foreach (string file in files)
			{
				string e = Path.GetExtension(file).ToLower();
				if(e==".png")
				{
					string name = Path.GetFileNameWithoutExtension(file);
					list.Add(name);

				}
			}
			string ret = $"private Bitmap[] m_Bitmaps = new Bitmap[{list.Count}];\r\n";

			int idx = 0;
			foreach (string file in list) 
			{
				ret += $"m_Bitmaps[{idx:000}] = Properties.Resources.{file};\r\n";
				idx++;
			}
			ret += "// ***********************************\r\n";
			ret += "private strings[] m_BitmapsNames =new string[]{\r\n";
			foreach (string file in list)
			{
				ret += $"\"{file}\",\r\n";
			}
			ret += "};\r\n";
			Console.WriteLine(ret);
		}
	}
}
