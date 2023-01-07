using System.Collections;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript.JavaScript;
using System.Xml.Linq;
using System.Dynamic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Hypowered
{

    public class HyperScript
    {
		public HyperMainForm? MainForm { get; set; } = null;
		private string startCode =
	  @"var System = dotnet.System;\r\n"
	+ @"var System.Core = dotnet.System.Core;"
	+ @"var System.Drawing = dotnet.System.Drawing;"
;
		private V8ScriptEngine? engine = null;
        public HyperScript()
        {
			Init();
		}
		public void Init()
		{
			if(engine != null)
			{
				engine.Dispose();
			}
			engine = new V8ScriptEngine();
			var typeCollection = new HostTypeCollection(
				"mscorlib",
				"System",
				"System.Core",
				"System.Drawing",
				"System.Windows.Forms");

			engine.AddHostObject("dotnet", typeCollection);
			engine.AddHostObject("alert", (object)alert);
			engine.AddHostObject("write", (object)write);
			engine.AddHostObject("writeLine", (object)writeLine);
			engine.AddHostObject("writeClear", (object)writeClear);
			engine.AddHostObject("openForm", (object)openForm);
			engine.AddHostObject("loadForm", (object)loadForm);
			engine.AddHostObject("appPath", (object)appPath);
			engine.AddHostObject("hypfPath", (object)hypfPath);
			engine.AddHostObject("homeHypf", (object)homeHypf);
			engine.AddHostObject("loadHome", (object)loadHome);
			engine.AddHostTypes(new Type[]
			{
				typeof(Console),
				typeof(Enumerable),
				typeof(Int32),
				typeof(String),
				typeof(Array),
				typeof(String[]),
				typeof(Boolean),
				typeof(Point),
				typeof(Size),
				typeof(Padding),
				typeof(Rectangle),
				typeof(Color),
				typeof(DateTime),
				typeof(Bitmap),
				typeof(HyperBaseForm),
				typeof(HyperMainForm),
				typeof(HyperFormList),
				typeof(List<HyperBaseForm>),

			});
			//engine.Execute("var app={};");

		}
		public void InitForms(HyperMainForm mf)
		{
			if (engine != null)
			{
				engine.AddHostObject("app", mf);
				engine.AddHostObject("_app_controls", mf.Controls);
				engine.AddHostObject("_app_forms", mf.formItems);
			}
		}
		public void InitControls(HyperBaseForm? bf)
		{
			if (bf == null) return;
			Control.ControlCollection cs= bf.Controls;
			engine.AddHostObject("controls", cs);
			if((engine!=null)&&(cs != null)&&(cs.Count>0))
			{
				foreach (Control c in cs)
				{
					if(c is HyperControl)
					engine.AddHostObject(c.Name, (HyperControl)c);
				}
			}
		}
		public void alert(object? s)
		{
			AlertForm dlg = new AlertForm();
			dlg.SelectedObject = s;
			dlg.ShowDialog();
			dlg.Dispose();
		}
		public void write(object? s)
		{
			if(MainForm!=null)
			{
				MainForm.OutputWrite(s);
			}
		}

		public void writeLine(object? s)
		{
			if (MainForm != null)
			{
				MainForm.OutputWriteLine(s);
			}
		}
		public void writeClear()
		{
			if (MainForm != null)
			{
				MainForm.OutputClear();
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
				AlertForm dlg= new AlertForm();
				dlg.SelectedObject = e;
				dlg.ChkSize();
				dlg.ShowDialog();
				dlg.Dispose();
			}
		}
		public bool loadForm(string fn)
		{
			if (MainForm!=null)
			{
				return MainForm.LoadFromHYPF(fn);
			}
			else
			{
				return false;
			}
		}
		public bool openForm(string fn)
		{
			if (MainForm != null)
			{
				return MainForm.OpenFromHYPF(fn);
			}
			else
			{
				return false;
			}
		}
		public string appPath()
		{
			string? p = Path.GetDirectoryName(Application.ExecutablePath);
			if(p == null)
			{
				p = Directory.GetCurrentDirectory();
			}
			return p;
		}
		public string hypfPath()
		{
			return Path.Combine(appPath(), Def.hypfFolder);
		}
		public string homeHypf()
		{
			string? p = Path.ChangeExtension(Application.ExecutablePath,Def.DefaultExt);
			if (p == null) p = "";
			return p;
		}
		public void loadHome()
		{
			loadForm(homeHypf());
		}
	}
}
