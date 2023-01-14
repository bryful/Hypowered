using BRY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
			if(CurrentDirChanged !=null)
			{
				CurrentDirChanged(this, e);
			}
			if ((MainForm != null)&&(Script_CurrentDirChanged!=""))
			{
				MainForm.Script.AddScriptObject("value", e.Path);
				MainForm.ExecuteScript(ScriptCode, ScriptKind.CurrentDirChanged);
				MainForm.Script.DeleteScriptObject("value");
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
					DirectoryInfo di  = new DirectoryInfo(value);
					if (di.Exists)
					{
						m_CurrentDir = di.FullName;
						Listup();
						OnCurrentDirChanged(new CurrentDirChangedEventArgs(m_CurrentDir));
					}
				}
			}
		}
		public new string[] Lines
		{
			get { return base.Lines; }
			set { }
		}
		public FootageFiles[] m_files = new FootageFiles[0];
		public HyperFootageList()
		{
			SetControlType(Hypowered.ControlType.FootageList);

			SetInScript(
				InScriptBit.CurrentDirChanged |
				InScriptBit.SelectedIndexChanged |
				InScriptBit.DragDrop
				); 
			InitializeComponent();
			m_ListBox.DrawMode = DrawMode.OwnerDrawFixed;
			m_ListBox.DrawItem += new DrawItemEventHandler(DrawItems);
			m_ListBox.ItemHeight = 20;
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

		public void Listup()
		{
			try
			{
				IEnumerable<string> dirs = Directory.EnumerateDirectories(m_CurrentDir);
				IEnumerable<string> files = Directory.EnumerateFiles(m_CurrentDir);

				List<FootageFiles> foorages = new List<FootageFiles>();

				FootageFiles p = new FootageFiles();
				p.SetParetn(Path.GetDirectoryName(m_CurrentDir));
				if (p.Exists)
				{
					foorages.Add(p);
					Debug.WriteLine(p.FullName);
				}

				foreach (string str in dirs)
				{
					foorages.Add(new FootageFiles(str));
				}

				foreach (string str in files)
				{
					if(foorages.Count==0)
					{
						foorages.Add(new FootageFiles(str));
					}
					else
					{
						if (foorages[foorages.Count-1].Add(str)==false)
						{
							foorages.Add(new FootageFiles(str));
						}
					}
				}
				m_ListBox.Items.Clear();
				m_ListBox.Items.AddRange(foorages.ToArray());
			}
			catch
			{

			}
		}
		private void DrawItems(object? sender, System.Windows.Forms.DrawItemEventArgs e)
		{
			if ((e == null)||(e.Font==null)) return;
			using (SolidBrush sb = new SolidBrush(this.BackColor))
			{
				e.DrawBackground();
				if (e.Index > -1)
				{
					if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
					{
						sb.Color = Color.FromArgb(128,32,32);
						e.Graphics.FillRectangle(sb, e.Bounds);
					}
					FootageFiles ff = (FootageFiles)m_ListBox.Items[e.Index];
					
					//文字列の取得
					string txt = ff.CaptionName;
					if (ff.IsParent) txt = "..\\";
					//文字列の描画
					sb.Color = ForeColor;
					Rectangle r2 = new Rectangle(e.Bounds.Left + 40, e.Bounds.Top, e.Bounds.Width - 40, e.Bounds.Height);
					if(ff.IsFolder)
					{
						Rectangle r1 = new Rectangle(e.Bounds.Left, e.Bounds.Top, 40, e.Bounds.Height);
						e.Graphics.DrawString("<dir>", e.Font, sb, r1);
					}
					e.Graphics.DrawString(txt, e.Font, sb, r2);
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
					FootageFiles? ff = (FootageFiles)m_ListBox.Items[si]; 
					if (ff == null)
					{
						ret = "";
					}
					else
					{
						ret = ff.FullName;
					}
				}
				return ret;
			}
		}
		protected override void ListBoxMouseDoubleClick(object? sender, MouseEventArgs e)
		{
			bool b = false;
			if ((m_ListBox.SelectedIndex >= 0) && (m_ListBox.SelectedIndex < m_ListBox.Items.Count))
			{
				if (m_ListBox.Items[m_ListBox.SelectedIndex] != null)
				{
					FootageFiles f = (FootageFiles)(m_ListBox.Items[m_ListBox.SelectedIndex]);
					if (f != null)
					{
						if(f.IsFolder)
						{
							CurrentDir = f.FullName;
							b=true;
						}
					}

				}
			}
			if(b==false)
				base.ListBoxMouseDoubleClick(sender, e);

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
