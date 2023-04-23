using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class PrefFile
	{
		private Form? m_form = null;
		public JsonFile JsonFile { get; set; } = new JsonFile();
		// *********************************
		private string m_AppName = "";
		public string AppName { get { return m_AppName; } }
		// *********************************
		private string m_AppDataPath = "";
		public string AppDataPath { get { return m_AppDataPath; } }
		private string m_AppDataDirectory = "";
		public string AppDataDirectory { get { return m_AppDataDirectory; } }
		// ****************************************************
		public PrefFile(Form fm,string path)
		{
			m_form = fm;
			m_AppName = Path.GetFileNameWithoutExtension(path);
			m_AppDataDirectory = GetAppDataPath();
			m_AppDataPath = Path.Combine(m_AppDataDirectory, m_AppName + ".json");

		}
		// ****************************************************
		public bool SetBounds()
		{
			if (m_form == null) return false;
			JsonFile.SetValue("Bouns", m_form.Bounds);
			return true;
		}
		public bool SetLocation()
		{
			if (m_form == null) return false;
			JsonFile.SetValue("Location", m_form.Location);
			return true;
		}
		// ****************************************************
		public Rectangle? GetBounds()
		{
			Rectangle? ret = null;
			if (m_form == null) return ret;
			ret = JsonFile.ValueRectangle("Bouns");
			if (ret != null)
			{
				if (IsInRect(NowScreen(m_form), (Rectangle)ret) == false)
				{
					ret = null;
				}
				if (ret != null)
				{
					m_form.StartPosition = FormStartPosition.Manual;
					m_form.WindowState = FormWindowState.Normal;
					m_form.Bounds = (Rectangle)ret;
				}
			}
			return ret;
		}
		public System.Drawing.Point? GetLocation()
		{
			Point? ret = null;
			if (m_form == null) return ret;
			ret = JsonFile.ValuePoint("Location");
			if (ret != null)
			{
				Point p = (Point)ret;
				Rectangle scr = NowScreen(m_form);
				if((p.X >=scr.Left)&&(p.X < scr.Right)
					&& (p.Y >= scr.Top) && (p.Y < scr.Bottom)
					)
				{
					m_form.Location = p;
				}
				else
				{
					ret = null;
				}
			}
			return ret;
		}
		// ****************************************************
		public bool Save(string s = "")
		{
			if (s == "") s = m_AppDataPath;

			return JsonFile.Save(s);
		}
		// ****************************************************
		public bool Load(string s = "")
		{
			if (s == "") s = m_AppDataPath;

			return JsonFile.Load(s);
		}
		// ****************************************************
		static public string GetAppDataPath()
		{
			return GetFileSystemPath(Environment.SpecialFolder.ApplicationData);
		}
		// ****************************************************
		static public string GetFileSystemPath(Environment.SpecialFolder folder)
		{
			// パスを取得
			string path = $"{Environment.GetFolderPath(folder)}\\"
				+ $"{Application.CompanyName}\\"
				+ $"{Application.ProductName}";

			// パスのフォルダを作成
			lock (typeof(Application))
			{
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
			}
			return path;
		}
		// ****************************************************
		static public bool IsInRect(Rectangle scr, Rectangle b)
		{
			bool ret = true;

			if ((scr.Left > b.Left + b.Width) || (scr.Left + scr.Width < b.Left))
			{
				ret = false;
			}
			if ((scr.Top > b.Top + b.Height) || (scr.Top + scr.Height < b.Top))
			{
				ret = false;
			}
			return ret;
		}
		// ****************************************************
		static public bool ScreenIn(Rectangle? rct)
		{
			bool ret = false;
			if (rct == null) return ret;
			foreach (Screen s in Screen.AllScreens)
			{
				Rectangle r = s.WorkingArea;
				if (IsInRect(r, (Rectangle)rct))
				{
					ret = true;
					break;
				}
			}
			return ret;
		}
		// ****************************************************
		static public Rectangle NowScreen(Form fm)
		{
			return Screen.FromControl(fm).WorkingArea;
		}
		// ****************************************************
		static public bool ScreenIn(Point p, Size sz)
		{
			return ScreenIn(new Rectangle(p, sz));
		}

	}
}
