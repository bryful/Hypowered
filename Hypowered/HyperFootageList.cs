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
	public partial class HyperFootageList : HyperListBox
	{
		public delegate void CurrentDirChangedHandler(object sender, CurrentDirChangedEventArgs e);
		public event CurrentDirChangedHandler? CurrentDirChanged;
		protected virtual void OnCurrentDirChanged(CurrentDirChangedEventArgs e)
		{
			if (m_HyperDriveIcons != null)
			{
				if (m_HyperDriveIcons.CurrentDir != e.Path)
				{
					m_HyperDriveIcons.CurrentDir = e.Path;
				}
			}

			if (m_HyperLabel != null)
			{
				m_HyperLabel.Text = e.Path;
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
				if (m_CurrentDir != value)
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
				if (m_HyperDriveIcons != null)
				{
					m_HyperDriveIcons.CurrentDir = m_CurrentDir;
					m_HyperDriveIcons.CurrentDirChanged += M_HyperDriveIcons_CurrentDirChanged;
				}
			}
		}
		private void M_HyperDriveIcons_CurrentDirChanged(object sender, CurrentDirChangedEventArgs e)
		{
			if (m_CurrentDir != e.Path)
			{
				CurrentDir = e.Path;
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
		public HyperFootageList()
		{
			SetControlType(Hypowered.ControlType.FootageList);

			SetInScript(
				InScriptBit.CurrentDirChanged |
				InScriptBit.SelectedIndexChanged
				); 
			InitializeComponent();
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
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
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
			jf.SetValue(nameof(IntegralHeight), IntegralHeight);//Boolean
																//jf.SetValue(nameof(ItemHeight), ItemHeight);//Int32
			jf.SetValue(nameof(Items), Items);//ObjectCollection
			jf.SetValue(nameof(ForeColor), ForeColor);//Color
			jf.SetValue(nameof(BackColor), BackColor);//Color
			jf.SetValue(nameof(Font), Font);//Font

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("IntegralHeight", typeof(Boolean).Name);
			if (v != null) IntegralHeight = (Boolean)v;
			v = jf.ValueAuto("ItemHeight", typeof(Int32).Name);
			if (v != null) ItemHeight = (Int32)v;
			//v = jf.ValueAuto("Items", typeof(ListBox.ObjectCollection).Name);
			//if (v != null) Items.AddRange((string[])v);
			v = jf.ValueAuto("ForeColor", typeof(Color).Name);
			if (v != null) ForeColor = (Color)v;
			v = jf.ValueAuto("BackColor", typeof(Color).Name);
			if (v != null) BackColor = (Color)v;
			v = jf.ValueAuto("Font", typeof(Font).Name);
			if (v != null) Font = (Font)v;

		}
	}
}
