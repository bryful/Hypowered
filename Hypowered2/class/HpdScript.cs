using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Immutable;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript.JavaScript;
//using static System.Windows.Forms.DataFormats;
using Microsoft.CodeAnalysis.Elfie.Serialization;


namespace Hpd
{
    public class HpdScript
    {
        private HpdMainForm? Mainform = null;
		public V8ScriptEngine? engine = null;
        public HpdRoot? Root = null;
        
		public HpdScript()
        {
			Init();
        }
        public void SetMainForm(HpdMainForm? mf=null)
        {
            Mainform = mf;
			Init();
			if(Mainform!= null)
			{
				Mainform.NameChanged += (sender, e) => { Init(); };
				Mainform.ControlAdded += (sender, e) => { Init(); };
				Mainform.ControlRemoved += (sender, e) => { Init(); };
			}
		}
		public void Init()
		{
			if (Mainform == null) return;
			if (engine != null) engine.Dispose();
			engine = new V8ScriptEngine();
			Root = new HpdRoot(Mainform);


			var typeCollection = new HostTypeCollection(
				"mscorlib",
				"System",
				"System",
				"System.Core",
				"System.Drawing",
				"System.IO",
				"System.Collections",
				"System.Windows.Forms");

			engine.AddHostObject("dotnet", typeCollection);
			if (Mainform != null)
			{
				//engine.AddHostObject("Alert", (object)Mainform.Alert);
				//engine.AddHostObject("Write", (object)Mainform.ConsoleWrite);
				//engine.AddHostObject("WriteLine", (object)Mainform.ConsoleWriteLine);
				//engine.AddHostObject("cls", (object)Mainform.ConsoleClear);
				//engine.AddHostObject("YesNoDialog", (object)Mainform.YesNoDialog);
			}
			engine.AddHostTypes(new Type[]
			{
				typeof(Enumerable),
				typeof(int),
				typeof(Int32),
				typeof(String),
				typeof(String[]),
				typeof(Array),
				typeof(Boolean),
				typeof(bool),
				typeof(Point),
				typeof(Size),
				typeof(Padding),
				typeof(Rectangle),
				typeof(Color),
				typeof(DateTime),
				typeof(HpdRoot),
				typeof(HpdForm),
				typeof(HpdMainForm),
				typeof(HpdControl),
				typeof(HpdControlCollection),


			});
			engine.AddHostObject("App", HostItemFlags.GlobalMembers, Root);
			InitControls();
		}
		public void InitControls()
		{
			if ((engine == null) || (Mainform == null)) return;
			if (Mainform.Items.Count > 0)
			{
				foreach (HpdControl c in Mainform.Items)
				{
					if (c is HpdButton)
					{
						engine.AddHostObject(c.Name, (HpdButton)c);
					}
					else if (c is HpdComboBox)
					{
						engine.AddHostObject(c.Name, (HpdComboBox)c);

					}
					else if (c is HpdCheckBox)
					{
						engine.AddHostObject(c.Name, (HpdCheckBox)c);
					}
					else if (c is HpdListBox)
					{
						engine.AddHostObject(c.Name, (HpdListBox)c);
					}
					else if (c is HpdPanel)
					{
						engine.AddHostObject(c.Name, (HpdPanel)c);
					}
					else if (c is HpdStretch)
					{
						engine.AddHostObject(c.Name, (HpdStretch)c);
					}
					else if (c is HpdTextBox)
					{
						engine.AddHostObject(c.Name, (HpdTextBox)c);
					}
				}
			}
		}
		public void ExecuteCode(string code)
		{
			if (engine == null) return;
			try
			{
				engine.Execute(code);
			}
			catch (Exception e)
			{
				HpdMainForm.AlertMes(e.ToString(), "Exception");
			}
		}
		public void GetGlobalThis()
		{
			if(engine == null) return;
			string s = 
@"function getGlobalThis()
{
	var s = "";
	for(var ss in globalThis)
	{
		s+= ss+'\r\n';
	}
	return s;
}";
			engine.Execute(s);
		}
	}

}
