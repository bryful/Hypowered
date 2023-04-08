using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.Windows.Input;
using System.Text.Json.Nodes;

namespace Hpd
{
	public class HpdFiles
	{
		private string m_FileName = "";
		public string FileName
		{
			get { return m_FileName; } 
			set { m_FileName = value; }
		}
		private  string[] m_ResBitmapNames = new string[0];
		public string[] ResBitmapNames { get { return m_ResBitmapNames; } }
		private readonly string FILEDIR = "file/";
		private readonly string PICTDIR = "pict/";
		// ****************************************************************************
		public HpdFiles() 
		{
			m_ResBitmapNames = GetBitmapNamesFromRes();
		}
		// ****************************************************************************
		public string[] GetBitmapNames()
		{
			string[] resB = GetBitmapNamesFromRes();
			string[] resZ = GetBitmapNamesFromZip();
			string[] ret = new string[0];
			Array.Copy(resB, ret, resB.Length);
			if (resZ.Length > 0)
			{
				Array.Copy(resZ, ret, resZ.Length);
			}
			return ret;
		}
		// ****************************************************************************
		static public string[] GetBitmapNamesFromRes()
		{
			PropertyInfo[] pi = typeof(Properties.Resources).GetProperties();
			if (pi.Length == 0) return new string[0];
			List<string> list1 = new List<string>();
			List<string> list2 = new List<string>();
			foreach (PropertyInfo p in pi)
			{
				if (p.PropertyType.Name == "Bitmap")
				{
					if(p.Name.IndexOf("ICON_")==0)
					{
						list1.Add(p.Name);
					}
					else
					{
						list2.Add(p.Name);
					}
				}
			}
			list1.AddRange(list2);
			return list1.ToArray();
		}
		// ****************************************************************************
		public string[] GetBitmapNamesFromZip()
		{
			string[] list = HpdZip.EntryList(m_FileName, PICTDIR);
			return list;
		}
		// ****************************************************************************
		public string[] GetFileNamesFromZip()
		{
			string[] list = HpdZip.EntryList(m_FileName, FILEDIR);
			return list;
		}
		// ****************************************************************************
		public int IndexOfBitmap(string name)
		{
			int ret = -1;
			int idx = 0;
			foreach (string nm in m_ResBitmapNames)
			{
				if (nm == name)
				{
					ret = idx;
					return ret;
				}
				idx++;
			}
			return ret;
		}
		// ****************************************************************************
		private int IndexOf(string[] list,string name)
		{
			int ret = -1;
			int idx = 0;
			foreach (string nm in list)
			{
				if (nm == name)
				{
					ret = idx;
					return ret;
				}
				idx++;
			}
			return ret;
		}
		// ****************************************************************************
		public bool AddPictFile(string filename)
		{
			bool ret = false;
			if (m_FileName == "") return ret;
			Bitmap? bmp = new Bitmap(filename);
			if (bmp == null) return ret;

			return AddFile(filename, PICTDIR);
		}
		// ****************************************************************************
		public bool AddFile(string filename)
		{
			return AddFile(FileName, FILEDIR);
		}
		// ****************************************************************************
		public bool AddFile(string filename,string folder)
		{
			bool ret = false;
			if (m_FileName == "") return ret;

			string ent = folder + Path.GetFileName(filename);
			string n = Path.GetFileName(filename);

			string[] list = HpdZip.EntryList(m_FileName, folder);

			int idx = IndexOf(list, n);
			if (idx >= 0)
			{
				HpdZip.DeleteEntry(m_FileName, ent);
			}
			ret = HpdZip.AddFromFile(m_FileName, ent, filename);
			return ret;
		}
	}

}