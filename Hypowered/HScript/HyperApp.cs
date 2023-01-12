using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Hypowered.HScript;
using Microsoft.ClearScript;

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
		public void write(object? s)
		{
			if (main != null)
			{
				main.OutputWrite(s);
			}
		}

		public void writeln(object? s)
		{
			if (main != null)
			{
				main.OutputWriteLine(s);
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
	}
}
