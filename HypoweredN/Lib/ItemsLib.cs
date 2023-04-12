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
using Svg;
using ImageMagick;

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
		public Bitmap? GetBitmap(string name, string entry="")
		{
			Bitmap? ret = null;

			if(m_IsZip==false)
			{
				string p = m_TargetPath;
				if(entry!="") p = Path.Combine(p, entry);
				p = Path.Combine(p, name);
				if (File.Exists(p) == false) return ret;
				ret = LoadBitmap(p);
			}
			else
			{
				string p2 = name;
				if (entry != "") p2 = entry + "/" + p2;
				string e = Path.GetExtension(name).ToLower();
				using (MemoryStream? ms = HZip.GetEntryToStream(m_TargetPath, p2))
				{
					if (ms != null)
					{
						ret = LoadBitmapFromStream(ms,e);
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
				case "bmp":
				case "gif":
				case "exif":
				case "jpg":
				case "jpeg":
				case "png":
				case "tif":
					try { ret = new Bitmap(name); }catch { ret =null; }
					break;
				case "svg":
					try
					{
						SvgDocument svgDoc = SvgDocument.Open(name);
						ret = svgDoc.Draw();
					}catch { ret = null; }
					break;
				case "psd":
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
			return ret;
		}
		static public Bitmap? LoadBitmapFromStream(MemoryStream ms,string e)
		{
			Bitmap? ret = null;
			switch (e)
			{
				//BMP、GIF、EXIF、JPG、PNG、TIFF 
				case "bmp":
				case "gif":
				case "exif":
				case "jpg":
				case "jpeg":
				case "png":
				case "tif":
					try { ret = new Bitmap(ms); } catch { ret = null; }
					break;
				case "svg":
					try
					{
						SvgDocument svgDoc = SvgDocument.Open<SvgDocument>(ms);
						ret = svgDoc.Draw();
					}
					catch { ret = null; }
					break;
				case "psd":
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
			return ret;
		}
		public bool PlayWave(string name, string entry = "")
		{
			bool ret = false;
			if (soundPlayer != null) StopWave();

			if (m_IsZip == false)
			{
				string p = m_TargetPath;
				if (entry != "") p = Path.Combine(p, entry);
				p = Path.Combine(p, name);
				if (File.Exists(p) == false) return ret;
				soundPlayer = new System.Media.SoundPlayer(p);
			}
			else
			{
				string p2 = name;
				if (entry != "") p2 = entry + "/" + p2;
				string e = Path.GetExtension(name).ToLower();
				using (MemoryStream? ms = HZip.GetEntryToStream(m_TargetPath, p2))
				{
					if (ms != null)
					{
						soundPlayer = new System.Media.SoundPlayer(ms);
					}
				}
			}
			if (soundPlayer != null)
			{
				soundPlayer.Play();
				ret = true;
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
			if (e == null) e = LibDefExt;
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
		// **********************************************************
		// **********************************************************
		// **********************************************************

		// **********************************************************
		public void Beep()
		{
			PlayWave("maou_se_system41.wav", "wav");
		}
		// **********************************************************

	}
}