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
    public partial class HyperFileList : HyperControl
	{
		public delegate void SelectedIndexChangedHandler(object sender, SelectedIndexChangedEventArgs e);
		public event SelectedIndexChangedHandler? SelectedIndexChanged;
		protected virtual void OnSelectedIndexChanged(SelectedIndexChangedEventArgs e)
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, e);
			}
			if(MainForm != null)
			{
				MainForm.ExecuteCode(Script_SelectedIndexChanged);
			}

		}
		protected override void OnDoubleClick(EventArgs e)
		{
			base.OnDoubleClick(e);
			if (IsEditMode == false)
			{
				if (MainForm != null)
				{
					MainForm.ExecuteCode(Script_MouseDoubleClick);
				}
			}
		}
		private ListBox m_ListBox = new ListBox();
		private string m_CurrentDir = Directory.GetCurrentDirectory();
		[Category("Hypowered_FileList")]
		public string CurrentDir
		{
			get { return m_CurrentDir; }
			set 
			{
				if(m_CurrentDir != value)
				{
					m_CurrentDir = value;
					Listup();
				}
			}
		}
		[Category("Hypowered_FileList")]
		public string SelectedItem
		{
			get
			{
				string ret = "";
				int si =m_ListBox.SelectedIndex;
				if((si>=0)&&(si<m_ListBox.Items.Count))
				{
					string? s = m_ListBox.Items[si].ToString();
					if (s == null)
					{
						ret = "";
					}
					else
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
		[Category("Hypowered_FileList")]
		public ListBox ListBox
		{
			get { return m_ListBox; }
			set
			{
				m_ListBox = value;
			}
		}
		
		[Category("Hypowered_FileList")]
		public bool IntegralHeight
		{
			get { return m_ListBox.IntegralHeight; }
			set
			{
				m_ListBox.IntegralHeight = value;
			}
		}
		[Category("Hypowered_FileList")]
		public int ItemHeight
		{
			get { return m_ListBox.ItemHeight; }
			set
			{
				m_ListBox.ItemHeight = value;
			}
		}
		[Category("Hypowered_FileList")]
		public int SelectedIndex
		{
			get { return m_ListBox.SelectedIndex; }
			set
			{
				m_ListBox.SelectedIndex = value;
			}
		}
		[Category("Hypowered_FileList")]
		public ListBox.ObjectCollection Items
		{
			get { return m_ListBox.Items; }
		}
		[Category("Hypowered_FileList")]
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
		private HyperDirList? m_HyperDirList = null;
		[Category("Hypowered_FileList")]
		public HyperDirList? DirList
		{
			get { return m_HyperDirList; }
			set
			{
				m_HyperDirList = value;
				if(m_HyperDirList != null)
				{
					if(m_CurrentDir != m_HyperDirList.CurrentDir)
					{
						m_CurrentDir = m_HyperDirList.CurrentDir;
						Listup();
						m_HyperDirList.CurrentDirChanged += M_HyperDirList_CurrentDirChanged;
					}
				}
			}
		}
		private string [] m_Filter = new string[0];
		[Category("Hypowered_FileList")]
		public string Filter
		{
			get
			{
				string ret = "";
				if (m_Filter.Length > 0)
				{
					foreach (var s in m_Filter)
					{
						if (ret != "") ret += "|";
						ret += s;
					}

				}
				return ret;
			}
			set
			{
				if(value.Trim() =="")
				{
					m_Filter = new string[0];
				}
				else
				{
					string[] sa = value.Split('|');
					if(sa.Length>0)
					{
						List<string> list = new List<string>();
						foreach(var s in sa)
						{
							string ss = s.Trim();
							if (ss == "") continue;
							list.Add(ss);
						}
						m_Filter = list.ToArray();
					}
					Listup();
				}
			}
		}
		private HyperLabel? m_HyperLabel = null;
		[Category("Hypowered_FileList")]
		public HyperLabel? Label
		{
			get { return m_HyperLabel; }
			set
			{
				m_HyperLabel = value;
				if (m_HyperLabel != null)
				{
					m_HyperLabel.Text = SelectedItem;
				}
			}
		}
		private void M_HyperDirList_CurrentDirChanged(object sender, CurrentDirChangedEventArgs e)
		{
			if(m_CurrentDir != e.Path)
			{
				CurrentDir= e.Path;
				Listup();
			}
		}

		public HyperFileList()
		{
			SetMyType(ControlType.FileList);
			SetConnectProps(
				new ControlType[] 
				{ ControlType.DirList,
					ControlType.Label 
				});
			SetInScript(InScriptBit.MouseDoubleClick | InScriptBit.SelectedIndexChanged);
			this.Size = new Size(150, 150);
			m_ListBox.Location = new Point(2, 2);
			m_ListBox.Size = new Size(this.Width - 4,this.Height - 4);
			
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
			string? s = "";
			if((SelectedIndex>=0)&&(SelectedIndex<Count))
			{
				s = m_ListBox.Items[SelectedIndex].ToString();
			}
			if (s != null)
			{
				OnSelectedIndexChanged(new SelectedIndexChangedEventArgs(SelectedIndex, s));
			}
		}

		private void M_ListBox_DoubleClick(object? sender, EventArgs e)
		{
			int si = m_ListBox.SelectedIndex;
			if((si>=0)&&(si<m_ListBox.Items.Count))
			{
				OnDoubleClick(e);
			}
		}

		public void Listup()
		{
			try
			{
				IEnumerable<string> files = Directory.EnumerateFiles(m_CurrentDir);

				List<string> strings = new List<string>();
				
				foreach (string str in files)
				{
					string n = Path.GetFileName(str);

					if(m_Filter.Length>0)
					{
						string e = Path.GetExtension(n);
						foreach(var s in m_Filter)
						{
							if(string.Compare(e,s,true)==0)
							{
								strings.Add(n);
								break;
							}
						}
					}
					else
					{
						strings.Add(n);
					}

					
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
				if (IsDrawFrame)
				{
					using (Pen p = new Pen(ForeColor))
					{
						DrawFrame(pe.Graphics, p, this.ClientRectangle);
					}
				}
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			m_ListBox.Size = new Size(this.Width - 4, this.Height - 4);
		
		}
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(MyType), (int?)MyType);//Nullable`1
			jf.SetValue(nameof(IntegralHeight), IntegralHeight);//Boolean
			jf.SetValue(nameof(ItemHeight), ItemHeight);//Int32
			//jf.SetValue(nameof(Items), Items);//ObjectCollection
			jf.SetValue(nameof(ForeColor), ForeColor);//Color
			jf.SetValue(nameof(BackColor), BackColor);//Color
			jf.SetValue(nameof(Font), Font);//Font
			jf.SetValue(nameof(Filter), Filter);//Font

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
			v = jf.ValueAuto("Filter", typeof(string).Name);
			if (v != null) Filter = (string)v;

		}
	}
}
