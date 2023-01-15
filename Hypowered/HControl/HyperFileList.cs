using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
    public partial class HyperFileList : HyperListBox
	{

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
		public override void ExecScript(ScriptKind sk)
		{
			if (MainForm != null)
			{
				if (ScriptCode.IsScriptCode(sk))
				{
					switch (sk)
					{
						case ScriptKind.DragDrop:
							MainForm.Script.AddScriptObject("value", m_DragDropItems);
							MainForm.Script.result = m_DragDropItems;
							break;
						case ScriptKind.SelectedIndexChanged:
						case ScriptKind.MouseDoubleClick:
							MainForm.Script.AddScriptObject("value", SelectedItem);
							MainForm.Script.result = SelectedItem;
							break;
						default:
							MainForm.Script.AddScriptObjectNull("value");
							MainForm.Script.result = null;
							break;
					}
					MainForm.Script.ExecuteScript(ScriptCode, sk);
					MainForm.Script.DeleteScriptObject("value");
				}
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
						m_HyperDirList.CurrentDirChanged += (sender, e) =>
						{
							if (m_CurrentDir != e.Path)
							{
								CurrentDir = e.Path;
								Listup();
							}
						};
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
		[Category("Hypowered_ListBox")]
		public new string SelectedItem
		{
			get
			{
				string ret = "";
				int si = m_ListBox.SelectedIndex;
				if ((si >= 0) && (si < m_ListBox.Items.Count))
				{
					string? s = m_ListBox.Items[si].ToString();
					if (s == null)
					{
						ret = "";
					}
					else if (s == "..\\")
					{
						string? ss  = Path.GetDirectoryName(m_CurrentDir);
						if (ss != null) ret = ss;
					}
					else
					{
						ret = Path.Combine(m_CurrentDir, s);
					}
				}
				return ret;
			}
		}

		public HyperFileList()
		{
			SetControlType(Hypowered.ControlType.FileList);
			SetInScript(InScriptBit.MouseDoubleClick | InScriptBit.SelectedIndexChanged | InScriptBit.DragDrop);

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
		protected override void ListBoxSelectedIndexChanged(object? sender, EventArgs e)
		{
			string s = "";
			if ((m_ListBox.SelectedIndex >= 0) && (m_ListBox.SelectedIndex < m_ListBox.Items.Count))
			{
				if (m_ListBox.Items[m_ListBox.SelectedIndex] != null)
				{
					string? ss = m_ListBox.Items[m_ListBox.SelectedIndex].ToString();
					if (ss != null)
					{
						
						s = Path.Combine(m_CurrentDir, ss);
					}
				}
			}
			OnSelectedIndexChanged(new SelectedIndexChangedEventArgs(m_ListBox.SelectedIndex, s));
		}
		protected override void OnDoubleClick(EventArgs e)
		{
			base.OnDoubleClick(e);
			string s = SelectedItem;
			if(s!="")
			{
				ExecScript(ScriptKind.MouseDoubleClick);
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

		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(ControlType), (int?)ControlType);//Nullable`1
			jf.SetValue(nameof(Filter), Filter);//Font

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("Filter", typeof(string).Name);
			if (v != null) Filter = (string)v;

		}
	}
}
