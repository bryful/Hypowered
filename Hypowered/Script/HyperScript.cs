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
				"System.Windows",
				"System.Windows.Forms");

			engine.AddHostObject("dotnet", typeCollection);
			engine.AddHostObject("alert", (object)alert);
			engine.AddHostTypes(new Type[]
			{
				typeof(Console),
				typeof(Point),
				typeof(Size),
				typeof(Padding),
			});
			engine.AddHostObject("alert", (object)alert);


		}
		public void InitControls(Control.ControlCollection cs)
		{
			if((engine!=null)&&(cs != null)&&(cs.Count>0))
			{
				foreach(Control c in cs)
				{
					engine.AddHostObject(c.Name, c);
				}
			}
		}
		public void alert(string s)
		{
			MessageBox.Show(s);
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
