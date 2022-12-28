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

		}
		public delegate void SelectedIndexChangedHandler(object sender, SelectedIndexChangedEventArgs e);
		public event SelectedIndexChangedHandler? SelectedIndexChanged;
		protected virtual void OnSelectedIndexChanged(SelectedIndexChangedEventArgs e)
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, e);
			}

		}
		private ListBox m_ListBox = new ListBox();
		private string m_CurrentDir = Directory.GetCurrentDirectory();
		[Category("Hypowerd_DirList")]
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
		[Category("Hypowerd_DirList")]
		public string SelectedDir
		{
			get
			{
				string ret = "";
				int si =m_ListBox.SelectedIndex;
				if((si>=0)&&(si<m_ListBox.Items.Count))
				{
					ret = m_ListBox.Items[si].ToString();
				}
				return ret;
			}
		}


		public override void SetIsEditMode(bool value)
		{
			m_IsEditMode = value;
			m_ListBox.Visible = !value;
		}
		[Category("Hypowerd")]
		public new Font Font
		{
			get { return m_ListBox.Font; }
			set
			{
				m_ListBox.Font = value;
				base.Font = value;
			}
		}
		[Category("Hypowerd_DirList")]
		public ListBox ListBox
		{
			get { return m_ListBox; }
			set
			{
				m_ListBox = value;
			}
		}
		[Category("Hypowerd_DirList")]
		public bool IntegralHeight
		{
			get { return m_ListBox.IntegralHeight; }
			set
			{
				m_ListBox.IntegralHeight = value;
			}
		}
		[Category("Hypowerd_DirList")]
		public int ItemHeight
		{
			get { return m_ListBox.ItemHeight; }
			set
			{
				m_ListBox.ItemHeight = value;
			}
		}
		[Category("Hypowerd_DirList")]
		public int SelectedIndex
		{
			get { return m_ListBox.SelectedIndex; }
			set
			{
				m_ListBox.SelectedIndex = value;
			}
		}
		[Category("Hypowerd_DirList")]
		public ListBox.ObjectCollection Items
		{
			get { return m_ListBox.Items; }
		}
		[Category("Hypowerd_DirList")]
		public int Count
		{
			get { return m_ListBox.Items.Count; }
		}
		[Category("Hypowerd_Color")]
		public new Color ForeColor
		{
			get { return m_ListBox.ForeColor; }
			set
			{
				base.ForeColor = value;
				m_ListBox.ForeColor = value;
			}
		}
		[Category("Hypowerd_Color")]
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
		[Category("Hypowerd_DirList")]
		public HyperDriveIcons? HyperDriveIcons
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
		private HyperLabel? m_HyperLabel = null;
		[Category("Hypowerd_DirList")]
		public HyperLabel? HyperLabel
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
			SetInScript(InScript.DirList);
			this.Size = new Size(150, 150);
			m_ListBox.Location = new Point(0, 0);
			m_ListBox.Size = new Size(this.Width,this.Height);
			if(Height!= m_ListBox.Height)
			{
				this.Size = new Size(this.Width, m_ListBox.Height);
			}

			m_ListBox.BackColor =base.BackColor;
			m_ListBox.ForeColor = base.ForeColor;
			m_ListBox.BorderStyle= BorderStyle.FixedSingle;
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
				s = m_ListBox.Items[SelectedIndex].ToString();
			}
			OnSelectedIndexChanged(new SelectedIndexChangedEventArgs(SelectedIndex, s));
		}

		private void M_ListBox_DoubleClick(object? sender, EventArgs e)
		{
			int si = m_ListBox.SelectedIndex;
			if((si>=0)&&(si<m_ListBox.Items.Count))
			{
				DirectoryInfo di = new DirectoryInfo(Path.Combine(m_CurrentDir, m_ListBox.Items[si].ToString()));
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
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			m_ListBox.Size = new Size(this.Width, this.Height);
			if (Height != m_ListBox.Height)
			{
				this.Size = new Size(this.Width, m_ListBox.Height);
			}
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

