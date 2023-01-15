using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Hypowered.HScript;
using Microsoft.ClearScript;
using Microsoft.WindowsAPICodePack.Dialogs;
namespace Hypowered
{

    public class HyperApp : HyperAppBase
	{
		private AppFormList m_forms = new AppFormList();
		public HyperBaseForm []forms { get { return m_forms.items; } }
		public dynamic formsEO { get { return m_forms.itemsEO; }  }
		public IDictionary<string, dynamic> formsD
		{
			get { return (IDictionary<string, dynamic>)m_forms.itemsEO; }
		}
		public int numForms { get { return m_forms.length; } }
		public StringCollection strings = new StringCollection();
		public PropertyBag bag = new PropertyBag();
		[ScriptUsage(ScriptAccess.None)]
		public HyperApp(HyperBaseForm? main):base(main)
		{
			base.form = main;
			ListupControls();
		}
		[ScriptUsage(ScriptAccess.None)]
		public override void ListupControls()
		{
			base.ListupControls();
			if(main!= null)
			{
				m_forms.ListupForms(main);
			}
		}
		public void exit()
		{
			Application.Exit();
		}
		public void alert(object? s)
		{
			using (AlertForm dlg = new AlertForm())
			{
				dlg.SelectedObject = s;
				dlg.ShowDialog();
			}
		}
		public void write(object? s, bool a = false)
		{
			if (main != null)
			{
				main.OutputWrite(s,a);
			}
		}

		public void writeln(object? s, bool a = false)
		{
			if (main != null)
			{
				main.OutputWriteLine(s,a);
			}
		}
		public void cls()
		{
			if (main != null)
			{
				main.OutputClear();
			}
		}
		public bool loadForm(string fn)
		{
			if (main != null)
			{
				return main.LoadFromHYPF(fn);
			}
			else
			{
				return false;
			}
		}
		public bool openForm(string fn)
		{
			if (main != null)
			{
				return main.OpenFromHYPF(fn);
			}
			else
			{
				return false;
			}
		}

		public string? executablePath
		{
			get { return Path.GetDirectoryName(Application.ExecutablePath); }
		}
		public string? currentPath
		{
			get { return Directory.GetCurrentDirectory(); }
			set 
			{ 
				if((value!=null)&&(Directory.Exists(value))) 
					Directory.SetCurrentDirectory(value); 
			}
		}
		public string hypfFolder
		{
			get
			{
				if (main != null)
				{
					return main.HYPF_Folder;
				}
				else
				{
					return "";
				}
			}
		}
		public string homeHypf
		{
			get 
			{
				if (main != null)
				{
					return main.HOME_HYPF_FILE;
				}
				else
				{
					return "";
				}
			} 
		}
		public string homeFolder
		{
			get
			{
				if (main != null)
				{
					string? s = Path.GetDirectoryName(main.HOME_HYPF_FILE);
					if( s != null)
						return  s;
					else
					{
						return "";
					}
				}
				else
				{
					return "";
				}
			}
		}
		public string appPath
		{
			get
			{
				return Application.ExecutablePath;
			}
		}
		public string appFolder
		{
			get
			{
				return Path.GetDirectoryName(Application.ExecutablePath);
			}
		}
		public void loadHome()
		{
			if(homeHypf != "") loadForm(homeHypf);
		}
		public void openHome()
		{
			if (homeHypf != "") openForm(homeHypf);
		}
		public bool yesnoDialog(string cap, string title)
		{
			if (main != null)
			{
				return answerDialog.Show(cap, title);
			}
			else
			{
				return false;
			}
		}
		public string? getenv(string EnvNane)
		{
			return Def.GetENV(EnvNane);
		}
		public void getenv(string EnvNane,string value)
		{
			Def.SetENV(EnvNane,value);
		}
		public string[] members()
		{
			var ps = this.GetType().GetMembers();
			List<string> props = new List<string>();
			foreach (var p in ps)
			{
				string s = p.Name;
				if ((s.IndexOf("add_") == 0)
					|| (s.IndexOf("get_") == 0)
					|| (s.IndexOf("remove_") == 0)
					|| (s.IndexOf("set_") == 0)
					|| (s.IndexOf(".") == 0)
					|| (s == "ListupControls")
					)
				{
					continue;
				}
				string ss = "app." + p.Name;
				string mt = p.MemberType.ToString();
				if (mt == "Method") ss = ss + "()";
				props.Add(ss);
			}
			var ps1 = typeof(AppControlList).GetMembers();
			foreach (var p in ps1)
			{
				string s = p.Name;
				if ((s.IndexOf("add_") == 0)
					|| (s.IndexOf("get_") == 0)
					|| (s.IndexOf("remove_") == 0)
					|| (s.IndexOf("set_") == 0)
					|| (s.IndexOf(".") == 0)
					|| (s == "ListupControls")
					)
				{
					continue;
				}
				string ss = "app.items[idx]." + p.Name;
				string mt = p.MemberType.ToString();
				if (mt == "Method") ss = ss + "()";
				props.Add(ss);
			}
			props.Sort();
			props.Add("File = dotnet.System.IO.File;");
			props.Add("Direcrory = dotnet.System.IO.Directory;");
			props.Add("JSON.stringify");
			props.Add("JSON.parse");
			props.Add("value");
			return props.ToArray();
		}
		public string toString(object? o)
		{
			return HyperScript.toString(o);
		}
		public Color? colorDialog(Color? col=null)
		{
			using(ColorDialog dlg = new ColorDialog())
			{
				if(col!= null) dlg.Color = (Color)col;
				dlg.FullOpen = true;
				dlg.AllowFullOpen = true;
				if(dlg.ShowDialog() == DialogResult.OK)
				{
					return dlg.Color;
				}
				else
				{
					return null;
				}
			}
		}
		public string? openFileDialog() { return openFileDialog("", "", "", ""); }
		public string? openFileDialog(string p="", string t="",string filert="",string defE="")
		{
			using(OpenFileDialog dlg = new OpenFileDialog())
			{
				if(p!="")
				{
					dlg.InitialDirectory =Path.GetDirectoryName(p);
					dlg.FileName = Path.GetFileName(p);
				}
				if(t!="") dlg.Title = t;
				if (filert != "") dlg.Filter = filert;
				if(defE!="") dlg.DefaultExt= defE;
				dlg.CheckFileExists= true;
				if(dlg.ShowDialog()==DialogResult.OK)
				{
					return dlg.FileName;
				}
				else
				{
					return null;
				}
			}
		}
		public string? saveFileDialog() { return saveFileDialog("", "", "", ""); }
		public string? saveFileDialog(string p = "", string t = "", string filert = "", string defE = "")
		{
			using (SaveFileDialog dlg = new SaveFileDialog())
			{
				if (p != "")
				{
					dlg.InitialDirectory = Path.GetDirectoryName(p);
					dlg.FileName = Path.GetFileName(p);
				}
				if (t != "") dlg.Title = t;
				if (filert != "") dlg.Filter = filert;
				if (defE != "") dlg.DefaultExt = defE;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					return dlg.FileName;
				}
				else
				{
					return null;
				}
			}
		}
		public string? folderSelectDialog(string p = "")
		{
			using (CommonOpenFileDialog dlg = new CommonOpenFileDialog())
			{
				if (p != "") dlg.InitialDirectory = p;
				dlg.IsFolderPicker= true;
				if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
				{
					return dlg.FileName;
				}
				else
				{
					return null;
				}
			}
		}
		public Font? fontDialog(string nm,float sz)
		{
			return fontDialog(new Font(nm, sz));
		}
		public Font? fontDialog(Font? fnt=null)
		{
			using (FontDialog dlg = new FontDialog())
			{
				if (fnt != null) dlg.Font = fnt;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					return dlg.Font;
				}
				else
				{
					return null;
				}
			}
		}
	}
}
