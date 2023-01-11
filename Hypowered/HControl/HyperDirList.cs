using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
    public partial class HyperDirList : HyperListBox
	{
		public delegate void CurrentDirChangedHandler(object sender, CurrentDirChangedEventArgs e);
		public event CurrentDirChangedHandler? CurrentDirChanged;
		protected virtual void OnCurrentDirChanged(CurrentDirChangedEventArgs e)
		{
			if(m_HyperDriveIcons!= null)
			{
				if(m_HyperDriveIcons.CurrentDir!=e.Path)
				{
					m_HyperDriveIcons.CurrentDir = e.Path;
				}
			}

			if(m_HyperLabel!= null)
			{
				m_HyperLabel.Text = e.Path;
			}
			if (m_FileList != null)
			{
				m_FileList.CurrentDir = e.Path;
			}
			if (CurrentDirChanged != null)
			{
				CurrentDirChanged(this, e);
			}
			if ((MainForm != null))
			{
				MainForm.ExecuteCode(Script_CurrentDirChanged);
			}

		}
		private string m_CurrentDir = Directory.GetCurrentDirectory();
		[Category("Hypowered_DirList")]
		public string CurrentDir
		{
			get { return m_CurrentDir; }
			set 
			{
				if(m_CurrentDir != value)
				{
					m_CurrentDir = value;
					Listup();
					OnCurrentDirChanged(new CurrentDirChangedEventArgs(m_CurrentDir));
				}
			}
		}
		private HyperDriveIcons? m_HyperDriveIcons = null;
		[Category("Hypowered_DirList")]
		public HyperDriveIcons? DriveIcons
		{
			get { return m_HyperDriveIcons; }
			set
			{
				m_HyperDriveIcons = value;
				if(m_HyperDriveIcons != null)
				{
					m_HyperDriveIcons.CurrentDir = m_CurrentDir;
					m_HyperDriveIcons.CurrentDirChanged += (sender, e) =>
					{
						if (m_CurrentDir != e.Path)
						{
							CurrentDir = e.Path;
						}
					};
				}
			}
		}
		private HyperFileList? m_FileList = null;
		[Category("Hypowered_DirList")]
		public HyperFileList? FileList
		{
			get { return m_FileList; }
			set
			{
				m_FileList = value;
				if (m_FileList != null)
				{
					m_FileList.CurrentDir = m_CurrentDir;
				}
			}
		}
		private HyperLabel? m_HyperLabel = null;
		[Category("Hypowered_DirList")]
		public HyperLabel? Label
		{
			get { return m_HyperLabel; }
			set
			{
				m_HyperLabel = value;
				if (m_HyperLabel != null)
				{
					m_HyperLabel.Text = m_CurrentDir;
				}
			}
		}

		public HyperDirList()
		{
			SetControlType(Hypowered.ControlType.DirList);
			
			SetInScript(
				InScriptBit.CurrentDirChanged|
				InScriptBit.SelectedIndexChanged
				);

			InitializeComponent();
			this.Controls.Add(m_ListBox);
			Listup();
			InitializeComponent();
			this.SetStyle(
	ControlStyles.Selectable |
	ControlStyles.UserMouse |
	ControlStyles.DoubleBuffer |
	ControlStyles.UserPaint |
	ControlStyles.AllPaintingInWmPaint |
	ControlStyles.SupportsTransparentBackColor,
	true);
			this.UpdateStyles();
		}


		protected override void OnListBoxDoubleClick(object? sender, EventArgs e)
		{
			int si = m_ListBox.SelectedIndex;
			if((si>=0)&&(si<m_ListBox.Items.Count))
			{
				string? s = m_ListBox.Items[si].ToString();
				if (s == null) return;
				DirectoryInfo di = new DirectoryInfo(Path.Combine(m_CurrentDir,s ));
				m_CurrentDir = di.FullName;
				Listup();
				m_ListBox.SelectedIndex = -1;
				OnCurrentDirChanged(new CurrentDirChangedEventArgs(m_CurrentDir));
			}
		}
		public void Listup()
		{
			try
			{
				IEnumerable<string> dirs = Directory.EnumerateDirectories(m_CurrentDir);

				List<string> strings = new List<string>();
				DirectoryInfo di = new DirectoryInfo(m_CurrentDir);
				if (di.Parent != null)
				{
					strings.Add("..\\");
				}
				foreach (string str in dirs)
				{
					string n = Path.GetFileName(str);
					strings.Add(n);
				}
				m_ListBox.Items.Clear();
				m_ListBox.Items.AddRange(strings.ToArray());
			}
			catch
			{

			}
		}

		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(ControlType), (int?)ControlType);//Nullable`1

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;


		}
	}
}

