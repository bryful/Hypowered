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
//using System.Windows.Forms.Integration;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript.JavaScript;
//using static System.Windows.Forms.DataFormats;
//using Microsoft.CodeAnalysis.Elfie.Serialization;


namespace Hypowered
{
    public class HScript
    {
        public MainForm? Mainform = null;
		public HForm? HForm = null;

		public V8ScriptEngine? engine = null;
        public HRoot Root = new HRoot();
        
		public HScript()
        {
			Init();
        }
        public void SetMainForm(MainForm? mf,HForm? hf)
        {
            Mainform = mf;
			HForm = hf;
			Root.SetMainForm(mf, hf);
			Init();
		}
		public void Init()
		{
			if (HForm == null) return;
			if (engine != null) engine.Dispose();
			engine = new V8ScriptEngine();


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
				typeof(HRoot),
				typeof(HForm),
				typeof(HControl),
			});
			engine.AddHostObject("App", HostItemFlags.GlobalMembers, Root);
			InitControls();
		}
		public void InitControls()
		{
			if ((engine == null) ||(HForm == null)) return;
			if (HForm.Controls.Count > 0)
			{
				foreach (Control c in HForm.Controls)
				{
					if (c==null) continue;
					if (c is HButton)
					{
						engine.AddHostObject(c.Name, (HButton)c);
					}
					else if (c is HIconButton)
					{
						engine.AddHostObject(c.Name, (HIconButton)c);
					}
					else if (c is HLabel)
					{
						engine.AddHostObject(c.Name, (HLabel)c);
					}
					else if (c is HListBox)
					{
						engine.AddHostObject(c.Name, (HListBox)c);
					}
					else if (c is HPictureBox)
					{
						engine.AddHostObject(c.Name, (HPictureBox)c);
					}
					else if (c is HTextBox)
					{
						engine.AddHostObject(c.Name, (HTextBox)c);
					}
					else if (c is HMainMenu)
					{
						engine.AddHostObject(c.Name, (HMainMenu)c);
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
				//HpdMainForm.AlertMes(e.ToString(), "Exception");
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
