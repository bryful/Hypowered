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
    public partial class HyperDirList : HyperControl
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
			if (CurrentDirChanged != null)
			{
				CurrentDirChanged(this, e);
			}
			if(m_HyperLabel!= null)
			{
				m_HyperLabel.Text = e.Path;
			}
			if (m_FileList != null)
			{
				m_FileList.CurrentDir = e.Path;
			}
			if ((MainForm != null))
			{
				MainForm.ExecuteCode(Script_CurrentDirChanged);
			}

		}
		public delegate void SelectedIndexChangedHandler(object sender, SelectedIndexChangedEventArgs e);
		public event SelectedIndexChangedHandler? SelectedIndexChanged;
		protected virtual void OnSelectedIndexChanged(SelectedIndexChangedEventArgs e)
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, e);
			}
			if ((MainForm != null))
			{
				MainForm.ExecuteCode(Script_SelectedIndexChanged);
			}

		}
		private ListBox m_ListBox = new ListBox();
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




		public override void SetIsEditMode(bool value)
		{
			m_IsEditMode = value;
			m_ListBox.Visible = !value;
		}
		[Category("Hypowered")]
		public new Font Font
		{
			get { return m_ListBox.Font; }
			set
			{
				m_ListBox.Font = value;
				base.Font = value;
			}
		}
		[Category("Hypowered_DirList")]
		public ListBox ListBox
		{
			get { return m_ListBox; }
			set
			{
				m_ListBox = value;
			}
		}
		[Category("Hypowered_DirList")]
		public bool IntegralHeight
		{
			get { return m_ListBox.IntegralHeight; }
			set
			{
				m_ListBox.IntegralHeight = value;
			}
		}
		[Category("Hypowered_DirList")]
		public int ItemHeight
		{
			get { return m_ListBox.ItemHeight; }
			set
			{
				m_ListBox.ItemHeight = value;
			}
		}
		[Category("Hypowered_DirList")]
		public int SelectedIndex
		{
			get { return m_ListBox.SelectedIndex; }
			set
			{
				m_ListBox.SelectedIndex = value;
			}
		}
		[Category("Hypowered_DirList")]
		public string SelectedItem
		{
			get
			{
				string ret = "";
				if( m_ListBox.SelectedItem != null )
				{
					string? s = m_ListBox.SelectedItem.ToString();
					if(s != null )
					{
						ret = s;
						if (m_CurrentDir != "")
						{
							ret = Path.Combine(m_CurrentDir, ret);
						}
					}
				}
				return ret;
			}
			
		}
		[Category("Hypowered_DirList")]
		public ListBox.ObjectCollection Items
		{
			get { return m_ListBox.Items; }
		}
		[Category("Hypowered_DirList")]
		public int Count
		{
			get { return m_ListBox.Items.Count; }
		}
		[Category("Hypowered_Color")]
		public new Color ForeColor
		{
			get { return m_ListBox.ForeColor; }
			set
			{
				base.ForeColor = value;
				m_ListBox.ForeColor = value;
			}
		}
		[Category("Hypowered_Color")]
		public new Color BackColor
		{
			get { return m_ListBox.BackColor; }
			set
			{
				base.BackColor = value;
				m_ListBox.BackColor = value;
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
					m_HyperDriveIcons.CurrentDirChanged += M_HyperDriveIcons_CurrentDirChanged;
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
		[Category("Hypowered_DirList")]
		public BorderStyle BorderStyle
		{
			get { return m_ListBox.BorderStyle; }
			set
			{
				m_ListBox.BorderStyle = value;
			}
		}
		private void M_HyperDriveIcons_CurrentDirChanged(object sender, CurrentDirChangedEventArgs e)
		{
			if(m_CurrentDir != e.Path)
			{
				CurrentDir= e.Path;
			}
		}

		public HyperDirList()
		{
			SetMyType(ControlType.DirList);
			SetConnectProps(
				new ControlType[]
				{
					ControlType.DriveIcons,
					ControlType.FileList,
				}
			);
			SetInScript(
				InScriptBit.CurrentDirChanged|
				InScriptBit.SelectedIndexChanged
				);
			this.Size = new Size(150, 150);
			m_ListBox.Location = new Point(2, 2);
			m_ListBox.Size = new Size(this.Width-4,this.Height-4);
			if(Height!= m_ListBox.Height+4)
			{
				this.Size = new Size(this.Width, m_ListBox.Height+4);
			}

			m_ListBox.BackColor =base.BackColor;
			m_ListBox.ForeColor = base.ForeColor;
			m_ListBox.BorderStyle= BorderStyle.None;
			m_ListBox.IntegralHeight = false;
			InitializeComponent();
			this.Controls.Add(m_ListBox);
			Listup();
			m_ListBox.DoubleClick += M_ListBox_DoubleClick;
			m_ListBox.SelectedIndexChanged += M_ListBox_SelectedIndexChanged;
		}

		private void M_ListBox_SelectedIndexChanged(object? sender, EventArgs e)
		{
			string s = "";
			if((SelectedIndex>=0)&&(SelectedIndex<Count))
			{
				string? ss = m_ListBox.Items[SelectedIndex].ToString();
				if(ss!=null) { s = ss; }
			}
			OnSelectedIndexChanged(new SelectedIndexChangedEventArgs(SelectedIndex, s));
		}

		private void M_ListBox_DoubleClick(object? sender, EventArgs e)
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
		protected override void OnPaint(PaintEventArgs pe)
		{
			if (m_IsEditMode)
			{
				base.OnPaint(pe);
			}
			else
			{
				pe.Graphics.Clear(BackColor);
				if(IsDrawFrame)
				{
					using(Pen p = new Pen(ForeColor))
					{
						DrawFrame(pe.Graphics, p, this.ClientRectangle);
					}
				}
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			m_ListBox.Size = new Size(this.Width-4, this.Height-4);
		}
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(MyType), (int?)MyType);//Nullable`1
			jf.SetValue(nameof(IntegralHeight), IntegralHeight);//Boolean
			//jf.SetValue(nameof(ItemHeight), ItemHeight);//Int32
			jf.SetValue(nameof(Items), Items);//ObjectCollection
			jf.SetValue(nameof(ForeColor), ForeColor);//Color
			jf.SetValue(nameof(BackColor), BackColor);//Color
			jf.SetValue(nameof(Font), Font);//Font
			jf.SetValue(nameof(BorderStyle), (int)BorderStyle);//Font

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
			v = jf.ValueAuto("BorderStyle", typeof(int).Name);
			if (v != null) BorderStyle = (BorderStyle)v;

		}
	}
}

