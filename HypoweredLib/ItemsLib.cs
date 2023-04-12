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
using System.Windows;
namespace HypoweredLib
{
	public class ItemsLib
	{
		private string[] m_Names = new string[0];
		private int m_BitmapStart = -1;
		private int m_BitmapLength = 0;
		private int m_SvgStart = -1;
		private int m_SvgpLength = 0;
		private int m_IconStart = -1;
		private int m_IconLength = -0;
		private int m_StrStart =   -1;
		private int m_StrLength = 0;
		private int m_WaveStart = -1;
		private int m_WaveLength = 0;
		public int Count { get {  return m_Names.Length; } }

		public string[] Names { get { return m_Names; } }

		public string StrNames { get { return ToStringA(m_Names); } }

		public int IndexOf(string n,int start=0,int count=-1)
		{
			int ret = -1;
			if((n=="")||(start>= m_Names.Length)||(start<0)||(count==0)) return ret;
			int cnt = m_Names.Length;
			if (count > 0)
			{
				cnt = count + start;
				if(cnt> m_Names.Length) cnt = m_Names.Length;
			}
			for(int i=start; i<cnt;i++)
			{
				if(n.Equals(m_Names[i], StringComparison.OrdinalIgnoreCase))
				{
					ret =i;
					break;
				}
			}
			return ret;
		}
		public int IndexOf(string n,int start=0) { return IndexOf(n, start, -1); }
		public int IndexOfBitmap(string n){return IndexOf(n, m_BitmapStart, m_BitmapLength);}
		public int IndexOfSVG(string n) { return IndexOf(n, m_SvgStart, m_SvgpLength); }
		public int IndexOfIcon(string n) { return IndexOf(n, m_IconStart, m_IconLength); }
		public int IndexOfString(string n) { return IndexOf(n, m_StrStart, m_StrLength); }
		public int IndexOfWave(string n) { return IndexOf(n, m_WaveStart, m_WaveLength); }
		// **********************************************************
		public ItemsLib() 
		{
			GetResNames();
		}
		public byte[]? SVG(string name)
		{
			byte[]? ret = null;
			var prop = typeof(Properties.Resources).GetProperty(name);
				if (prop != null)
				{
					try
					{
						if (prop.PropertyType == typeof(Byte[]))
						{
							ret = (Byte[]?)prop.GetValue(typeof(Properties.Resources));
						}
					}
					catch
					{
						ret = null;
					}
				}

			return ret;
		}
		public Bitmap? Bitmap(string name)
		{
			Bitmap? ret = null;
			var prop = typeof(Properties.Resources).GetProperty(name);
			if (prop != null)
			{
				try
				{
					if (prop.PropertyType == typeof(Bitmap))
					{
						ret = (Bitmap?)prop.GetValue(typeof(Properties.Resources));
					}
				}
				catch
				{
					ret = null;
				}
			}

			return ret;
		}
		public Icon? Icon(string name)
		{
			Icon? ret = null;
			var prop = typeof(Properties.Resources).GetProperty(name);
			if (prop != null)
			{
				try
				{
					if (prop.PropertyType == typeof(Icon))
					{
						ret = (Icon?)prop.GetValue(typeof(Properties.Resources));
					}
				}
				catch
				{
					ret = null;
				}
			}

			return ret;
		}
		public String? String(string name)
		{
			String? ret = null;
			var prop = typeof(Properties.Resources).GetProperty(name);
			if (prop != null)
			{
				try
				{
					if (prop.PropertyType == typeof(String))
					{
						ret = (String?)prop.GetValue(typeof(Properties.Resources));
					}
				}
				catch
				{
					ret = null;
				}
			}

			return ret;
		}
		public UnmanagedMemoryStream? Wave(string name)
		{
			UnmanagedMemoryStream? ret = null;
			var prop = typeof(Properties.Resources).GetProperty(name);
			if (prop != null)
			{
				try
				{
					if (prop.PropertyType == typeof(UnmanagedMemoryStream))
					{
						ret = (UnmanagedMemoryStream?)prop.GetValue(typeof(Properties.Resources));
					}
				}
				catch
				{
					ret = null;
				}
			}

			return ret;
		}
		// **********************************************************
		public void GetResNames()
		{
			PropertyInfo[] pi = typeof(Properties.Resources).GetProperties();
			if (pi.Length == 0) return;
			List<string> svglist = new List<string>();
			List<string> bmplist1 = new List<string>();
			List<string> bmplist2 = new List<string>();
			List<string> strlist = new List<string>();
			List<string> icnlist = new List<string>();
			List<string> wavlist = new List<string>();
			foreach (PropertyInfo p in pi)
			{
				switch(p.PropertyType.Name)
				{
					case "Byte[]":
						svglist.Add(p.Name);
						break;
					case "Bitmap":
						if (p.Name.IndexOf("ICON_") == 0)
						{
							bmplist1.Add(p.Name);
						}
						else
						{
							bmplist2.Add(p.Name);
						}
						break;
					case "String":
						strlist.Add(p.Name);
						break;
					case "Icon":
						icnlist.Add(p.Name);
						break;
					case "UnmanagedMemoryStream":
						wavlist.Add(p.Name);
						break;
				}
			}
			List<string> list = new List<string>();

			m_BitmapStart = -1;
			m_SvgStart = -1;
			m_IconStart = -1;
			m_StrStart = -1;
			m_WaveStart = -1;
			if ((bmplist1.Count > 0)||(bmplist2.Count > 0)) m_BitmapStart = 0;
			m_BitmapLength = bmplist1.Count + bmplist2.Count;
			list.AddRange(bmplist1);
			list.AddRange(bmplist2);

			if(svglist.Count>0) m_SvgStart = list.Count;
			m_StrLength = svglist.Count;
			list.AddRange(svglist);

			if (icnlist.Count > 0) m_IconStart = list.Count;
			m_IconLength = icnlist.Count;
			list.AddRange(icnlist);
			if (strlist.Count > 0) m_StrStart = list.Count;
			m_StrLength = strlist.Count;
			list.AddRange(strlist);
			if (wavlist.Count > 0) m_WaveStart = list.Count;
			m_WaveLength = wavlist.Count;
			list.AddRange(wavlist);

			m_Names = list.ToArray();
		}

		// **********************************************************
		private string ToStringA(string[] sa)
		{
			string ret = "";
			foreach (string s in sa)
			{
				ret += s+"\r\n";
			}
			return ret;
		}
		// **********************************************************

	}
}