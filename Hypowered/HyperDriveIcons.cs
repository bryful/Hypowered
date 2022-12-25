using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace Hypowered
{
	public partial class HyperDriveIcons : HyperControl
	{
		private DirectoryInfo[] m_drives = new DirectoryInfo[0];
		private Size m_IconSize = new Size(18, 18);
		public Size IconSize
		{
			get { return m_IconSize; }
			set { m_IconSize = value; }
		}
		public bool SetIconSize(Size sz)
		{
			if(m_IconSize==sz) return false;
			m_IconSize = sz;
			if(m_drives.Length>0)
			{
				this.MinimumSize = new Size(m_IconSize.Width*1, m_IconSize.Height);
				this.MaximumSize = new Size(m_IconSize.Width * 27, m_IconSize.Height);
				this.Size = new Size(m_IconSize.Width * (m_drives.Length + 1), m_IconSize.Height);

			}
			return true;
		}
		public HyperDriveIcons()
		{
			//Listup();
			SetMyType(ControlType.DriveIcons);
			m_ScriptCode = "//CheckBox";
			//m_CheckSize = 16;
			this.Size = ControlDef.DefSize;

			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
		private void Listup()
		{
			ManagementObject mo = new ManagementObject();

			string[] drives = Environment.GetLogicalDrives();
			List<DirectoryInfo> dirs = new List<DirectoryInfo>();
			foreach (string drive in drives)
			{
				DirectoryInfo di = new DirectoryInfo(drive);
				dirs.Add(di);
			}
			m_drives= dirs.ToArray();
		}
	}
}
