using System.Collections;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript.JavaScript;
using System.Xml.Linq;
using System.Dynamic;

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
			//engine.AddHostObject("items", cs);
			string name;
			if(bf is HyperBaseForm)
			{
				name = "";
			}
			else
			{
				name = bf.Name+".";
			}
			if((engine!=null)&&(cs != null)&&(cs.Count>0))
			{

				foreach (Control c in cs)
				{
					if(c is HyperControl)
					engine.AddHostObject(name + c.Name, (HyperControl)c);
				}
			}
		}
		public void alert(object? s)
		{
			AlertForm dlg = new AlertForm();
			dlg.SelectedObject= s;
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
	}
}
