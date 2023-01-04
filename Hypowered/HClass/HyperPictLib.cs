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

namespace Hypowered
{
	public class HyperPictLib
	{
		private string m_FileName = "";
		private HyperMainForm? m_form = null;
		public void SetMainForm(HyperMainForm? mf)
		{
			m_form= mf;
			if(m_form!=null)
			{
				m_FileName= m_form.FileName;
				LoadUserPictFile();
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
		private List<PictItem> m_Items = new List<PictItem>();

		private readonly string PICTDIR = "pict/";
		private int m_ResCount = 0;
		public HyperPictLib()
		{
			GetRes();
		}
		// ***********************************************************************
		public Bitmap? loadRes(string name)
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
		public void GetRes()
		{
			m_Items.Clear();
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
				Bitmap? bmp = loadRes(nm);
				if(bmp != null)
				{
					PictItem pitem = new PictItem(bmp, nm, m_Items.Count, true, false);
					m_Items.Add(pitem);
				}
			}
			m_ResCount = m_Items.Count;
		}
		public void LoadUserPictFile()
		{
			if(m_FileName=="") return;

			string[] lst = GetPictList();
			if(lst.Length <= 0) return;

			foreach(string nm in lst)
			{
				int idx = IndexOf(nm);
				Bitmap? bmp = ZipUtil.GetEntryBitmap(m_FileName, PICTDIR + nm);
				if (bmp == null) continue;
				PictItem pi = new PictItem(
						bmp,
						nm,
						m_Items.Count,
						false,
						false
						);
				if (idx < 0)
				{
					m_Items.Add(pi);
				}
				else
				{
					m_Items[idx] = pi;
				}
			}
		}
		// ***********************************************************************
		public bool AddUserPict(string filename)
		{
			bool ret = false;
			if (m_FileName == "") return ret;
			string n = Path.GetFileName(filename);
			int idx = IndexOf(n);
			if(idx >=0) { return ret; }

			Bitmap? bmp = new Bitmap(filename);
			if (bmp == null) return ret;
			string ent = PICTDIR + Path.GetFileName(filename);
			ret = ZipUtil.AddFromFile(m_FileName, ent, filename);
			if(ret)
			{
				m_Items.Add(new PictItem(
					bmp,
					Path.GetFileName(filename),
					m_Items.Count,
					false,
					false
					));
			}

			return ret;
		}
		// ***********************************************************************
		public string GetPictListFromArc()
		{
			string ret = "";
			if(m_FileName=="") return ret;

			string[] sa = ZipUtil.EntryList(m_FileName,PICTDIR);
			if(sa.Length>0)
			{
				foreach(string s in sa)
				{
					if (ret != "") ret += "\r\n";
					ret += s;
				}
			}

			return ret;
		}
		private string[] GetPictList()
		{
			string [] ret = new string[0];
			if (m_FileName == "") return ret;

			return  ZipUtil.EntryList(m_FileName, PICTDIR);

		}

		public PictItem this[int idx]
		{
			get
			{
				PictItem? ret = new PictItem(null,"",-1,false,true);
				if((idx>=0)&&(idx<m_Items.Count))
				{
					ret = m_Items[idx];
				}
				return ret;
			}
		}
		
		// ***********************************************************************
		private Bitmap? LoadBitmaps(int idx)
		{
			if((idx<0)||(idx>=m_Items.Count)) return null;
			if (m_Items[idx].Bitmap == null)
			{
				if(m_Items[idx].IsRes)
				{
					m_Items[idx].Bitmap = loadRes(m_Items[idx].Name);
				}
				else
				{
					if (m_FileName == "") return null;
					string ent = PICTDIR + m_Items[idx].Name;
					Bitmap? bmp = ZipUtil.GetEntryBitmap(m_FileName, ent);
					if (bmp != null)
					{
						m_Items[idx].Bitmap = bmp;
					}
				}
			}
			return m_Items[idx].Bitmap;
		}
		public string BitmapName(int idx)
		{
			if ((idx >= 0) && (idx < m_Items.Count))
			{
				return m_Items[idx].Name;
			}
			else
			{
				return "";
			}
		}
		public string PictName(int idx)
		{
			string ret = "";
			if ((idx >= 0) && (idx < m_Items.Count))
			{
				ret = m_Items[idx].Name;
			}
			return ret;
		}
		public string BitmapInfo(int idx)
		{
			string ret = "";
			if ((idx >= 0) && (idx < m_Items.Count))
			{
				Bitmap? bmp = LoadBitmaps(idx);
				if (bmp != null)
				{
					ret = $"Index:{idx},Built-in:{m_Items[idx].IsRes} Width:{bmp.Width}, Height:{bmp.Height}";
				}
			}
			return ret;
		}

		public int IndexOf(string name)
		{
			int ret = -1;
			int idx = 0;
			foreach(PictItem nm in m_Items)
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
		public Bitmap? Find(string name)
		{
			int idx = IndexOf(name);
			if((idx>=0)&&(idx<m_Items.Count))
			{
				return LoadBitmaps(idx);
			}
			else
			{
				return null;
			}
		}
		public int Count
		{
			get { return m_Items.Count; }
		}

		public Bitmap Thum(int idx,int width=48, int height=48)
		{
			Bitmap ret = new Bitmap(width, height);
			if ((idx < 0) || (idx >=Count)) return ret;
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
	}
}
