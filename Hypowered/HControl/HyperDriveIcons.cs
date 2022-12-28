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
using System.Text.Json.Nodes;

namespace Hypowered
{

    public partial class HyperDriveIcons : HyperControl
	{
		public delegate void CurrentDirChangedHandler(object sender, CurrentDirChangedEventArgs e);
		public event CurrentDirChangedHandler? CurrentDirChanged;
		protected virtual void OnCurrentDirChanged(CurrentDirChangedEventArgs e)
		{
			if (CurrentDirChanged != null)
			{
				CurrentDirChanged(this, e);
			}
			if ((HyperForm != null))
			{
				HyperForm.ExecuteCode(Script_CurrentDirChanged);
			}
		}
		private int m_SelectedDRIndex = -1;
		[Category("Hypowerd_DriveIcons")]
		public int SelectedDRIndex
		{
			get { return m_SelectedDRIndex; }
		}
		private DriveInfo[] m_drives = new DriveInfo[0];
		private string[] m_drivesPath = new string[0];
		[Category("Hypowerd_DriveIcons")]
		public string CurrentDir
		{
			get
			{
				string ret = "";
				if((m_SelectedDRIndex>=0)&&(m_SelectedDRIndex< m_drivesPath.Length))
				{
					ret = m_drivesPath[m_SelectedDRIndex];
				}
				return ret;
			}
			set
			{
				if (value == "") return;
				string c = value.Substring(0, 1);
				if (c == "") return;
				int ii = -1;
				for(int i=0;i<m_drivesPath.Length;i++)
				{
					if (m_drivesPath[i].Substring(0, 1) == c)
					{
						ii=i; break;
					}
				}
				if(ii>=0)
				{
					if(m_SelectedDRIndex != ii)
					{
						m_SelectedDRIndex = ii;
						m_drivesPath[m_SelectedDRIndex] = value;
						OnCurrentDirChanged(new CurrentDirChangedEventArgs(value));
					}
				}
			}
		}
//		private HyperDirList? m_HyperDirList = null;
//		public HyperDirList? HyperDirList


		private Size m_IconSize = new Size(18, 18);
		[Category("Hypowerd_DriveIcons")]
		public Size IconSize
		{
			get { return m_IconSize; }
			set { m_IconSize = value; }
		}
		public bool SetIconSize(Size sz)
		{
			if(m_IconSize==sz) return false;
			m_IconSize = sz;
			ChkSize();
			if (m_drives.Length>0)
			{
				this.MinimumSize = new Size(m_IconSize.Width*1, m_IconSize.Height);
				this.MaximumSize = new Size(m_IconSize.Width * 27, m_IconSize.Height);
				//this.Size = new Size(m_IconSize.Width * (m_drives.Length + 1), m_IconSize.Height);
			}
			return true;
		}
		public void ChkSize()
		{
			if (m_drives.Length > 0)
			{
				this.MinimumSize = new Size(0,0);
				this.MaximumSize = new Size(0,0);
				this.MinimumSize = new Size(m_IconSize.Width * 1, m_IconSize.Height);
				this.MaximumSize = new Size(m_IconSize.Width * 27, m_IconSize.Height*3);
				//this.Size = new Size(m_IconSize.Width * (m_drives.Length + 1), m_IconSize.Height);
			}
		}
		public HyperDriveIcons()
		{
			Listup();
			SetMyType(ControlType.DriveIcons);
			SetInScript(InScript.CurrentDirChanged);
			//m_ScriptCodes = "//DriveIcons";
			this.Size = ControlDef.DefSize;

			InitializeComponent();
			ChkSize();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = pe.Graphics;
				// 背景色
				g.FillRectangle(sb, this.ClientRectangle);
				if(m_drives.Length>0)
				{
					sb.Color = ForeColor;
					m_format.Alignment = StringAlignment.Center;
					int left = 0;
					Rectangle r = new Rectangle(left + 2, 2, m_IconSize.Width - 4, m_IconSize.Height - 4);
					g.DrawRectangle(p, r);
					Rectangle r2 = ReRect(r, 3);
					g.DrawEllipse(p, r2);
					for (int i=0; i<m_drives.Length; i++)
					{
						left = (i+1) * m_IconSize.Width;
						r = new Rectangle(left + 2, 2, m_IconSize.Width - 4, m_IconSize.Height - 4);
						r2 = ReRect(r, 1);
						string s = m_drives[i].Name.Substring(0,1);
						if(i==m_SelectedDRIndex)
						{
							sb.Color= ForeColor;
							g.FillRectangle(sb, r2);
							sb.Color = Color.Black;
						}
						else
						{
							sb.Color = ForeColor;
						}
						g.DrawString(s, this.Font, sb, r, m_format);
						g.DrawRectangle(p, r);
					}
				}


				// 外枠
				Rectangle rr = ReRect(this.ClientRectangle, 2);
				//p.Color = ForeColor;
				//g.DrawRectangle(p, rr);
				if (this.Focused)
				{
					rr = ReRect(this.ClientRectangle, 1);
					p.Color = m_ForcusColor;
					g.DrawRectangle(p, rr);
				}
				if (this.Focused)
				{
					rr = ReRect(this.ClientRectangle, 1);
					p.Color = m_ForcusColor;
					g.DrawRectangle(p, rr);
				}
				DrawType(g, sb);
			}
		}
		private void Listup()
		{
			string cur = "";
			if((m_SelectedDRIndex>=0)&&(m_SelectedDRIndex<m_drivesPath.Length))
			{
				cur = m_drivesPath[m_SelectedDRIndex];
			}
			m_drives = DriveInfo.GetDrives();
			if(m_drivesPath.Length != m_drives.Length)
			{
				m_drivesPath = new string[m_drives.Length];
				for(int i=0; i<m_drives.Length;i++)
				{
					m_drivesPath[i] = m_drives[i].Name;
				}
			}
			if((cur!="")&&(m_drivesPath.Length>0))
			{
				string curT = cur.Substring(0, 1);
				int ii = -1;
				for (int i = 0; i < m_drivesPath.Length; i++)
				{
					if (m_drivesPath[i].Substring(0,1)==curT)
					{
						ii = i;
						break;
					}
				}
				if(ii>=0)
				{
					m_SelectedDRIndex= ii;
				}
			}


			/*
			foreach (DriveInfo driveInfo in ds)
			{
				// ボリュームラベルを取得
				string volumeLabel = driveInfo.VolumeLabel;
				// ボリュームラベルが設定されているかどうか
				if (volumeLabel == "")
				{
					// ドライブ名を取得
					string name = driveInfo.Name;
					if (name == @"C:\")
					{
						// Cドライブの場合はボリュームラベルを「Windows」に設定
						driveInfo.VolumeLabel = "Windows";
					}
					else
					{
						// ドライブ文字を取得（C, Dなど）
						string drive = name.Substring(0, 1);
						// Cドライブ以外の場合はドライブ名＋「ボリューム」に設定
						driveInfo.VolumeLabel = $"{drive} ボリューム";
					}
				}
			}
			*/
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if(m_IsEditMode==false)
			{
				int idx = e.X / m_IconSize.Width;
				if(idx==0)
				{
					Listup();
					this.Invalidate();
					return;
				}
				else
				{
					idx -= 1; 
					if((idx>=0)&&(idx<m_drives.Length))
					{
						if(m_SelectedDRIndex != idx)
						{
							m_SelectedDRIndex = idx;
							OnCurrentDirChanged(new CurrentDirChangedEventArgs(m_drivesPath[m_SelectedDRIndex]));
						}
						this.Invalidate();
						return;
					}
				}
			}
			
		}
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(MyType), (int?)MyType);//Nullable`1
			jf.SetValue(nameof(IconSize), IconSize);//Size

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("IconSize", typeof(Size).Name);
			if (v != null) IconSize = (Size)v;

		}

	}
}
