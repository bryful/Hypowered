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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Hypowered
{
	public class HyperLib
	{
		private string m_FileName = "";
		private HyperMainForm? m_form = null;
		public void SetMainForm(HyperMainForm? mf)
		{
			m_form= mf;
			if(m_form!=null)
			{
				m_FileName= m_form.FileName;
				ListupUserPictFile();
				ListupTextFile();
			}
		}
		public class PictItem
		{
			public Bitmap? Bitmap = null;
			public string Name = "";
			public bool IsRes = true;
			public bool Err = false;
			public int Index = -1;
			public PictItem(Bitmap? bitmap, string name)
			{
				Bitmap = bitmap;
				this.Name = name;
				IsRes = false;
				Err = false;
			}
			public PictItem(Bitmap? bitmap, string name,int idx, bool isRes, bool err)
			{
				Bitmap = bitmap;
				this.Name = name;
				Index= idx;
				IsRes = isRes;
				Err = err;
			}	
		}
		public class TextItem
		{
			public string Name = "";
			public bool IsRes = true;
			public bool Err = false;
			public int Index = -1;
			public TextItem(string txt, string name)
			{
				this.Name = name;
				IsRes = false;
				Err = false;
			}
			public TextItem(string name, int idx, bool isRes, bool err)
			{
				this.Name = name;
				Index = idx;
				IsRes = isRes;
				Err = err;
			}
		}
		private List<PictItem> m_PictItems = new List<PictItem>();
		private List<TextItem> m_TextItems = new List<TextItem>();

		private readonly string PICTDIR = "pict/";
		private readonly string TEXTDIR = "text/";
		private readonly string WAVDIR = "wav/";
		//private int m_ResCount = 0;
		public HyperLib()
		{
			GetBitmapRes();
		}
		// ***********************************************************************
		public Bitmap? loadBitmapRes(string name)
		{
			Bitmap? bmp = null;
			var prop = typeof(Properties.Resources).GetProperty(name);
			if (prop != null)
			{
				try
				{
					if (prop.PropertyType == typeof(Bitmap))
					{
						bmp = (Bitmap?)prop.GetValue(typeof(Properties.Resources));
					}
				}
				catch
				{
					bmp = null;
				}
			}
			return bmp;
		}
		// ***********************************************************************
		public void GetBitmapRes()
		{
			m_PictItems.Clear();
			PropertyInfo[] pi = typeof(Properties.Resources).GetProperties();
			if (pi.Length == 0) return;
			List<string> rlist= new List<string>();
			foreach (PropertyInfo p in pi)
			{
				if(p.Name.IndexOf("ICON_")==0)
				{
					rlist.Add(p.Name);
				}
			}
			foreach (PropertyInfo p in pi)
			{
				if (p.Name.IndexOf("ICON_") != 0)
				{
					rlist.Add(p.Name);
				}
			}

			foreach (string nm in rlist)
			{
				Bitmap? bmp = loadBitmapRes(nm);
				if(bmp != null)
				{
					PictItem pitem = new PictItem(bmp, nm, m_PictItems.Count, true, false);
					m_PictItems.Add(pitem);
				}
			}
		}
		public void ListupUserPictFile()
		{
			if(m_FileName=="") return;

			string[] lst = GetPictList();
			if(lst.Length <= 0) return;

			foreach(string nm in lst)
			{
				int idx = IndexOfBitmap(nm);
				Bitmap? bmp = ZipUtil.GetEntryBitmap(m_FileName, PICTDIR + nm);
				if (bmp == null) continue;
				PictItem pi = new PictItem(
						bmp,
						nm,
						m_PictItems.Count,
						false,
						false
						);
				if (idx < 0)
				{
					m_PictItems.Add(pi);
				}
				else
				{
					m_PictItems[idx] = pi;
				}
			}
		}
		// ***********************************************************************
		public PictItem GetPictItem(int index)
		{
			if((index>=0)&&(index<m_PictItems.Count))
			{
				return m_PictItems[index];
			}
			else
			{
				return new PictItem(null,"",-1,false,true);
			}
		}
		public bool AddUserPict(string filename)
		{
			bool ret = false;
			if (m_FileName == "") return ret;
			Bitmap? bmp = new Bitmap(filename);
			if (bmp == null) return ret;

			string ent = PICTDIR + Path.GetFileName(filename);
			string n = Path.GetFileName(filename);

			int idx = IndexOfBitmap(n);
			if(idx >=0) 
			{
				ZipUtil.DeleteEntry(m_FileName, ent);
			}
			ret = ZipUtil.AddFromFile(m_FileName, ent, filename);
			if(ret)
			{
				m_PictItems.Add(new PictItem(
					bmp,
					Path.GetFileName(filename),
					m_PictItems.Count,
					false,
					false
					));
			}

			return ret;
		}
  
		// ***********************************************************************
		private string[] GetPictList()
		{
			string [] ret = new string[0];
			if (m_FileName == "") return ret;

			return  ZipUtil.EntryList(m_FileName, PICTDIR);

		}
		
		// ***********************************************************************
		private Bitmap? LoadBitmaps(int idx)
		{
			if((idx<0)||(idx>=m_PictItems.Count)) return null;
			if (m_PictItems[idx].Bitmap == null)
			{
				if(m_PictItems[idx].IsRes)
				{
					m_PictItems[idx].Bitmap = loadBitmapRes(m_PictItems[idx].Name);
				}
				else
				{
					if (m_FileName == "") return null;
					string ent = PICTDIR + m_PictItems[idx].Name;
					Bitmap? bmp = ZipUtil.GetEntryBitmap(m_FileName, ent);
					if (bmp != null)
					{
						m_PictItems[idx].Bitmap = bmp;
					}
				}
			}
			return m_PictItems[idx].Bitmap;
		}
		
		public string BitmapName(int idx)
		{
			if ((idx >= 0) && (idx < m_PictItems.Count))
			{
				return m_PictItems[idx].Name;
			}
			else
			{
				return "";
			}
		}
		public string PictName(int idx)
		{
			string ret = "";
			if ((idx >= 0) && (idx < m_PictItems.Count))
			{
				ret = m_PictItems[idx].Name;
			}
			return ret;
		}
		
		public string BitmapInfo(int idx)
		{
			string ret = "";
			if ((idx >= 0) && (idx < m_PictItems.Count))
			{
				Bitmap? bmp = LoadBitmaps(idx);
				if (bmp != null)
				{
					ret = $"Index:{idx},Built-in:{m_PictItems[idx].IsRes} Width:{bmp.Width}, Height:{bmp.Height}";
				}
			}
			return ret;
		}

		public int IndexOfBitmap(string name)
		{
			int ret = -1;
			int idx = 0;
			foreach(PictItem nm in m_PictItems)
			{
				if(nm.Name == name)
				{
					ret = idx;
					return ret;
				}
				idx++;
			}
			return ret;
		}
		
		public Bitmap? FindBitmap(string name)
		{
			int idx = IndexOfBitmap(name);
			if((idx>=0)&&(idx<m_PictItems.Count))
			{
				return LoadBitmaps(idx);
			}
			else
			{
				return null;
			}
		}
		public int BitmapCount
		{
			get { return m_PictItems.Count; }
		}
	

		public Bitmap Thum(int idx,int width=48, int height=48)
		{
			Bitmap ret = new Bitmap(width, height);
			if ((idx < 0) || (idx >=BitmapCount)) return ret;
			Graphics g = Graphics.FromImage(ret);
			Bitmap? bmp = LoadBitmaps(idx);
			if (bmp == null) return ret;
			bmp.SetResolution(96, 96);
			int w = bmp.Width;
			int h = bmp.Height;
			if((w<= ret.Width) &&(h<=ret.Height))
			{
				int cx = ret.Width/2 - w / 2;
				int cy = ret.Height/2 - h / 2;

				g.DrawImage(bmp, new Rectangle(cx, cy, w, h));
			}
			else
			{
				double scal = width / (double)w;
				if (h > w) scal = height / (double)h;
				int w2 = (int)((double)w * scal);
				int h2 = (int)((double)h * scal);
				Rectangle rr = new Rectangle(
					ret.Width / 2 - w2 / 2,
					ret.Height / 2 - h2 / 2,
					w2, h2);
				g.DrawImage(bmp, rr);
			}
			return ret;
		}
		// ******************************************************************
		public int TextFileCount
		{
			get { return m_TextItems.Count; }
		}
		public void ListupTextFile()
		{
			if (m_FileName == "") return;

			string[] lst = GetTextList();
			if (lst.Length <= 0) return;

			foreach (string nm in lst)
			{
				int idx = IndexOfTextFile(nm);
				string? txt = ZipUtil.GetEntryToStr(m_FileName, TEXTDIR + nm);
				if (txt == null) continue;
				TextItem pi = new TextItem(
						nm,
						m_TextItems.Count,
						false,
						false
						);
				if (idx < 0)
				{
					m_TextItems.Add(pi);
				}
				else
				{
					m_TextItems[idx] = pi;
				}
			}
		}
		public bool AddUserTextFile(string filename)
		{
			bool ret = false;
			if (m_FileName == "") return ret;
			string n = Path.GetFileName(filename);
			string ent = TEXTDIR + Path.GetFileName(filename);
			int idx = IndexOfBitmap(n);
			if (idx >= 0)
			{
				ZipUtil.DeleteEntry(m_FileName, ent);
			}


			ret = ZipUtil.AddFromFile(m_FileName, ent, filename);
			if (ret)
			{
				m_TextItems.Add(new TextItem(
					Path.GetFileName(filename),
					m_TextItems.Count,
					false,
					false
					));
			}

			return ret;
		}
		public string GetTextListFromArc()
		{
			string ret = "";
			if (m_FileName == "") return ret;

			string[] sa = ZipUtil.EntryList(m_FileName, TEXTDIR);
			if (sa.Length > 0)
			{
				foreach (string s in sa)
				{
					if (ret != "") ret += "\r\n";
					ret += s;
				}
			}

			return ret;
		}
		private string[] GetTextList()
		{
			string[] ret = new string[0];
			if (m_FileName == "") return ret;

			return ZipUtil.EntryList(m_FileName, TEXTDIR);

		}
		public TextItem GetTextItem(int idx)
		{
			TextItem? ret = new TextItem("", -1, false, true);
			if ((idx >= 0) && (idx < m_TextItems.Count))
			{
				ret = m_TextItems[idx];
			}
			return ret;
		}
		public string TextFileName(int idx)
		{
			string ret = "";
			if ((idx >= 0) && (idx < m_PictItems.Count))
			{
				ret = m_TextItems[idx].Name;
			}
			return ret;
		}
		public int IndexOfTextFile(string name)
		{
			int ret = -1;
			int idx = 0;
			foreach (TextItem nm in m_TextItems)
			{
				if (nm.Name == name)
				{
					ret = idx;
					return ret;
				}
				idx++;
			}
			return ret;
		}
		public string? LoadTextFile(int idx)
		{
			string? ret = null;
			if (m_FileName == "") return null;
			if ((idx < 0) || (idx >= m_TextItems.Count)) return ret;
			if (m_TextItems[idx].Name == "")
			{
				string ent = TEXTDIR + m_TextItems[idx].Name;
				ret = ZipUtil.GetEntryToStr(m_FileName, ent);
			}
			return ret;
		}
		public string? LoadTextFile(string name)
		{
			string? ret = null;
			if (m_FileName == "") return null;
			return LoadTextFile(IndexOfTextFile(name));
		}
	}
}
