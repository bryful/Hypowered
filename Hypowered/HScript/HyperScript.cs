using System.Collections;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript.JavaScript;
using System.Xml.Linq;

namespace Hypowered
{

    public class HyperScript
    {
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
			engine.AddHostTypes(new Type[]
			{
				typeof(Console),
				typeof(Int32),
				typeof(String[]),
				typeof(Boolean),
				typeof(Point),
				typeof(Size),
				typeof(Padding),
				typeof(Rectangle),
				typeof(Color),
				typeof(DateTime),
				typeof(Bitmap),
			});
			engine.AddHostObject("alert", (object)alert);


		}
		public void InitControls(Control.ControlCollection cs)
		{
			if((engine!=null)&&(cs != null)&&(cs.Count>0))
			{
				foreach(Control c in cs)
				{
					if(c is HyperControl)
					engine.AddHostObject(c.Name, (HyperControl)c);
				}
			}
		}
		public void alert(object? s)
		{
			string ret = "";
			try
			{
				if (s != null)
				{
					ret = s.ToString();
				}
				else
				{
					ret = "null";
				}
			}
			catch
			{
				ret = "null";
			}
			MessageBox.Show($"{ret}");
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
				MessageBox.Show(e.ToString());
			}
		}
	}
}
