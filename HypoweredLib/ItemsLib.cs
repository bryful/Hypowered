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
		private string[] m_SVGNames = new string[0];
		private string[] m_BitmapNames = new string[0];
		private string[] m_StringNames = new string[0];
		private string[] m_IconNames = new string[0];
		private string[] m_WaveNames = new string[0];
		public string[] SVGNames { get { return m_SVGNames; } }
		public string[] BitmapNames { get { return m_BitmapNames; } }
		public string[] StringNames { get { return m_StringNames; } }
		public string[] IconNames { get { return m_IconNames; } }
		public string[] WaveNames { get { return m_WaveNames; } }

		public string StrSVGNames { get { return ToStringA(m_SVGNames); } }
		public string StrBitmapNames { get { return ToStringA(m_BitmapNames); } }
		public string StrStringNames { get { return ToStringA(m_StringNames); } }
		public string StrIconNames { get { return ToStringA(m_IconNames); } }
		public string StrWaveNames { get { return ToStringA(m_WaveNames); } }


		public ItemsLib() 
		{
			GetResNames();
			//Properties.Resources.alarm_black_48dp;
			//System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.se_saa08);
			//player.Play();
		}
		// **********************************************************
		public void GetResNames()
		{
			PropertyInfo[] pi = typeof(Properties.Resources).GetProperties();
			if (pi.Length == 0) return;
			List<string> svglist = new List<string>();
			List<string> bmplist = new List<string>();
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
						svglist.Add(p.Name);
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
			m_SVGNames = svglist.ToArray();
			m_BitmapNames = svglist.ToArray();
			m_StringNames = strlist.ToArray();
			m_IconNames = icnlist.ToArray();
			m_WaveNames = wavlist.ToArray();
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