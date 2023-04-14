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
using Svg;
using ImageMagick;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace Hypowered
{
	public enum LibTarget
	{
		Def,
		Dir,
		Zip,
	}
	public class ItemsLib
	{
		// **********************************************************
		private System.Media.SoundPlayer soundPlayer = null;
		// **********************************************************
		private string m_TargetPath = "";
		public readonly string LibDefExt = ".lib";
		private LibTarget m_LibTarget =  LibTarget.Def;
		private bool m_IsZip = false;
		private bool m_Enabled = false;
		public bool Enabled { get { return m_Enabled; } }
		public bool IsZip { get { return m_IsZip; } }
		public string TargetPath { get { return m_TargetPath; } }
		private string[] m_ItemNames = new string[0];
		public int IndexOf(string? name)
		{
			int ret = -1;
			if ((name == null)||(name=="")||(m_ItemNames.Length<=0)) return -1;
			int idx = 0;
			foreach(string item in m_ItemNames)
			{
				if (name.Equals(item, StringComparison.OrdinalIgnoreCase) ==true)
				{
					ret = idx;
					break;
				}
				idx++;
			}
			return ret;
		}
		// **************************************************************
		public ItemsLib()
		{

		}
		// **************************************************************
		public ItemsLib(string libName, LibTarget lt = LibTarget.Def)
		{
			Setup(libName, lt);
		}
		public bool Setup(string libName, LibTarget lt = LibTarget.Def)
		{
			m_TargetPath = "";
			m_ItemNames = new string[0];
			m_Enabled = false;
			if (lt == LibTarget.Zip)
			{
				if (SetupZip(libName) == false)
				{
					return false;
				}
				m_LibTarget = LibTarget.Zip;
			}
			else if (lt == LibTarget.Dir)
			{
				if (SetupDir(libName) == false)
				{
					return false;
				}
				m_LibTarget = LibTarget.Dir;
			}
			else
			{
				if (SetupZip(libName) == true)
				{
					m_LibTarget = LibTarget.Zip;
				}
				else
				{
					if (SetupDir(libName) == true)
					{
						m_LibTarget = LibTarget.Dir;
					}
					else
					{
						return false;
					}
				}
			}
			m_ItemNames = GetItemNames();
			m_Enabled = true;
			return true;
		}
		// **************************************************************
		public bool Aarchive(string defExt = ".lib")
		{
			bool ret = false; 
			if((IsZip==true)||(m_TargetPath=="")||(m_Enabled==false)) return ret;

			string zipFile = m_TargetPath + defExt;

			ret = HZip.CreateFromDirectory(m_TargetPath, zipFile);
			if(ret == true )
			{
				string mm = m_TargetPath;
				ret = Setup(zipFile, LibTarget.Zip);
				if (ret==false)
				{
					ret  = Setup(mm, LibTarget.Dir);
				}
			}
			return ret;
		}
		public Bitmap? GetBitmap(string name)
		{
			Bitmap? ret = null;

			if(m_IsZip==false)
			{
				string p = Path.Combine(m_TargetPath,name.Replace("/","\\"));
				if (File.Exists(p) == false) return ret;
				ret = LoadBitmap(p);
			}
			else
			{
				using (MemoryStream? ms = HZip.GetEntryToStream(m_TargetPath, name))
				{
					if (ms != null)
					{
						ret = LoadBitmapFromStream(ms, Path.GetExtension(name).ToLower());
					}
				}
			}


			return ret;
		}
		static public Bitmap? LoadBitmap(string name)
		{
			Bitmap? ret = null;
			string e = Path.GetExtension(name).ToLower();
			switch(e)
			{
				//BMP、GIF、EXIF、JPG、PNG、TIFF 
				case ".bmp":
				case ".gif":
				case ".exif":
				case ".jpg":
				case ".jpeg":
				case ".png":
				case ".tif":
					try { ret = new Bitmap(name); }catch { ret =null; }
					break;
				case ".svg":
					try
					{
						SvgDocument svgDoc = SvgDocument.Open(name);
						ret = svgDoc.Draw();
						//ColorAt(ret, Color.FromArgb(200, 200, 200));

					}
					catch { ret = null; }
					break;
				case ".psd":
				default:
					try
					{
						using (var myMagick = new ImageMagick.MagickImage(name))
						{
							ret = myMagick.ToBitmap(); //Bitmapへ変換
						}
					}
					catch { ret = null; }
					break;
			}
			if (ret != null)
			{
				ret.SetResolution(96, 96);
			}

			return ret;
		}
		static public Bitmap? LoadBitmapFromStream(MemoryStream ms,string e)
		{
			Bitmap? ret = null;
			switch (e)
			{
				//BMP、GIF、EXIF、JPG、PNG、TIFF 
				case ".bmp":
				case ".gif":
				case ".exif":
				case ".jpg":
				case ".jpeg":
				case ".png":
				case ".tif":
					try { ret = new Bitmap(ms); } catch { ret = null; }
					break;
				case ".svg":
					try
					{
						SvgDocument svgDoc = SvgDocument.Open<SvgDocument>(ms);
						ret = svgDoc.Draw();
						//ColorAt(ret, Color.FromArgb(200, 200, 200));
					}
					catch { ret = null; }
					break;
				case ".psd":
				default:
					try
					{
						using (var myMagick = new ImageMagick.MagickImage(ms))
						{
							ret = myMagick.ToBitmap(); //Bitmapへ変換
						}
					}
					catch { ret = null; }
					break;
			}
			if(ret != null)
			{
				ret.SetResolution(96, 96);
			}
			return ret;
		}
		public bool PlayWave(string name)
		{
			bool ret = false;
			if (soundPlayer != null) StopWave();

			if (m_IsZip == false)
			{
				string p = Path.Combine( m_TargetPath,name.Replace("/","\\"));
				if (File.Exists(p) == false) return ret;
				try
				{
					soundPlayer = new System.Media.SoundPlayer(p);
					soundPlayer.Play();
					ret = true;
				}
				catch
				{
					ret = false;
				}
			}
			else
			{
				try
				{
					using (MemoryStream? ms = HZip.GetEntryToStream(m_TargetPath, name))
					{
						if (ms != null)
						{
							soundPlayer = new System.Media.SoundPlayer(ms);
							soundPlayer.Play();
							ret = true;
						}
					}
				}
				catch { ret = false; }
			}
			return ret;
		}
		public void StopWave()
		{
			if (soundPlayer != null)
			{
				soundPlayer.Stop();
				soundPlayer.Dispose();
				soundPlayer = null;
			}
		}
		// **************************************************************
		private bool SetupDir(string path)
		{
			m_TargetPath = "";
			m_Enabled = false;
			string? p = Path.GetDirectoryName(path);
			string? n = Path.GetFileNameWithoutExtension(path);
			if(n==null) return	false;
			string? e = Path.GetExtension(path);
			string target = n;
			if (p == null)
			{
				p = Directory.GetCurrentDirectory();
			}
			target = Path.Combine(p, target);
			if(Directory.Exists(target) ==false)
			{
				try
				{
					Directory.CreateDirectory(target);
				}
				catch
				{
					return false;
				}
			}
			m_TargetPath = target;
			m_IsZip = false;
			return true;
		}
		private bool SetupZip(string path)
		{
			m_TargetPath = "";
			m_Enabled = false;
			string? p = Path.GetDirectoryName(path);
			if (p == null) p = Directory.GetCurrentDirectory();
			string? n = Path.GetFileNameWithoutExtension(path);
			if(n==null) return false;
			string? e = Path.GetExtension(path);
			if (e == "") e = LibDefExt;
			string target = n;
			target = Path.Combine(p, target+e);
			if (File.Exists(target) == false)
			{
				return false;
			}
			m_TargetPath = target;
			m_IsZip = true;
			return true;
		}
		// **************************************************************
		public string[] GetItemNames(string entry="")
		{
			List<string> ret = new List<string>();
			if((m_TargetPath == "")||(m_Enabled==false)) return ret.ToArray();
			string p = m_TargetPath;
			if(m_IsZip == false)
			{
				if (entry!="") p = Path.Combine(p, entry);
				IEnumerable<string> files =Directory.EnumerateFiles(
					p, "*", SearchOption.AllDirectories);
				int idx = m_TargetPath.Length+1;
				foreach (string f in files)
				{
					string f2 = f.Substring(idx).Replace("\\","/");
					ret.Add(f2);
				}
				
			}
			else
			{
				string[] sa = HZip.EntryList(m_TargetPath, entry);
				foreach (string s in sa)
				{
					if (s[s.Length-1] != '/') 
					{
						string sss = s;
						if (entry != "") sss = entry + "/" + sss;
						ret.Add(sss);
					}
				}
			}

			return ret.ToArray();
		}
		public string GetItemNamesS(string entry = "")
		{
			string ret = "";
			string[] a = GetItemNames(entry);
			if(a.Length <= 0) return ret;
			foreach(string s in a)
			{
				ret += s + "\r\n";
			}
			return ret;
		}
		// **********************************************************
		public string[] GetItemNamesAtPict(string entry = "")
		{
			string[] ret = GetItemNames(entry);
			if(ret.Length > 0)
			{
				List<string> list = new List<string>();
				foreach (string s in ret) 
				{
					string e = Path.GetExtension(s).ToLower();
					if((e==".jpg")
						|| (e == ".jpeg")
						|| (e == ".png")
						|| (e == ".gif")
						|| (e == ".svg")
						|| (e == ".tif")
						|| (e == ".png")
						|| (e == ".exif")
						|| (e == ".bmp"))
					{
						list.Add(s);
					}
				}
				ret = list.ToArray();
			}
			return ret;
		}
		// **********************************************************
		// **********************************************************

		// **********************************************************
		public void Beep()
		{
			PlayWave("wav/maou_se_system41.wav");
		}
		// **********************************************************
		static public void ColorAt(Bitmap bitmap, Color col)
		{
			BitmapData data = bitmap.LockBits(
				new Rectangle(0, 0, bitmap.Width, bitmap.Height),
				ImageLockMode.ReadWrite,
				PixelFormat.Format32bppArgb);
			int bytes = bitmap.Width * bitmap.Height * 4;
			Int32 a = ((Int32)col.B) << 16 | ((Int32)col.G) << 8 | ((Int32)col.R);
			for (int i = 0; i < bytes; i += 4)
			{
				Int32 value = Marshal.ReadInt32(data.Scan0, i);

				Color c = Color.FromArgb(value);
				c = Color.FromArgb(c.A, col.R, col.G, col.B);
				Marshal.WriteInt32(data.Scan0, i, c.ToArgb());
			}
			bitmap.UnlockBits(data);
		}

	}
	public class ItemName
	{
		public string Name { get; set; }="";
		public string Entry { get; set; } = "";
		public string Ext { get; set; } = "";
		public ItemName()
		{
		}
		public ItemName(string name, string entry)
		{
			SetEntry(entry);
			SetName(name);
		}
		public ItemName(string[] names)
		{
			if(names.Length>=2)
			{
				SetEntry(names[0]);
				SetName(names[1]);
			}
		}
		public void SetEntry(string s)
		{
			if(s!="")
			{
				if (s[s.Length - 1] == '/') s = s.Substring(0,s.Length - 1);
			}
			Entry = s;
		}
		public void SetName(string s)
		{
			if (s != "")
			{
				Ext = Path.GetExtension(s).ToLower();
			}
			Name = s;
		}

		public void SetZipEntry(string s)
		{
			string[] sa = SplitEntry(s);
			Entry = sa[0];
			Name = sa[1];
		}
		public string Info
		{
			get { return $"Entry:{Entry}  Name:{Name}"; }
		}
		static public string SplitBase(string baseP, string p)
		{
			string ret = "";
			if(p=="") return ret;
			p = p.Substring(baseP.Length+1).Replace("\\", "/");
			return p;
		}
		static public string[] SplitEntry(string p)
		{
			string [] ret = new string[] { "", "" };
			if (p == "") return ret;
			if (p == "") return ret;
			int idx = p.LastIndexOf('/');
			if(idx==-1)
			{
				ret[1] = p;
			}else if(idx==0)
			{
				ret[1] = p.Substring(1);
			}
			else
			{
				ret[0] = p.Substring(0,idx);
				ret[1] = p.Substring(idx + 1);

			}
			return ret;
		}
	}
}