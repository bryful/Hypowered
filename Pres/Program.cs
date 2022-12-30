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
			List<string> listICONS = new List<string>();
			foreach (string file in files)
			{
				string e = Path.GetExtension(file).ToLower();
				if(e==".png")
				{
					string name = Path.GetFileNameWithoutExtension(file);
					if (name.IndexOf("ICON_")==0)
					{
						listICONS.Add(name);
					}
					else
					{
						list.Add(name);
					}

				}
			}
			int cnt = list.Count + listICONS.Count;
			string ret = $"private Bitmap[] m_Bitmaps = new Bitmap[{cnt}];\r\n";
			ret += $"int idx = 0;\r\n";

			int idx = 0;
			foreach (string file in listICONS)
			{
				ret += $"m_Bitmaps[idx] = Properties.Resources.{file};idx++;//{idx}\r\n";
				idx++;
			}
			foreach (string file in list) 
			{
				ret += $"m_Bitmaps[idx] = Properties.Resources.{file};idx++;//{idx}\r\n";
				idx++;
			}

			ret += "// ***********************************\r\n";
			ret += "private strings[] m_BitmapsNames =new string[]{\r\n";
			idx = 0;
			foreach (string file in listICONS)
			{
				ret += $"\"{file}\",//{idx}\r\n";
				idx++;
			}
			foreach (string file in list)
			{
				ret += $"\"{file}\",//{idx}\r\n";
				idx++;
			}
			ret += "};\r\n";
			Console.WriteLine(ret);
		}
	}
}
