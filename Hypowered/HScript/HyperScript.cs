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
using Hypowered.HScript;
using System.Runtime.Remoting;
using System.Diagnostics;
using System.Linq;

namespace Hypowered
{

    public class HyperScript
    {
		public HyperApp? app = null;
		public Object? result
		{
			get 
			{
				if (app != null)
				{
					return app.result;
				}
				else
				{
					return null;
				}
			}
			set
			{
				if (app != null)
				{
					app.SetResult(value);
				}
			}
		}
		protected HyperMainForm? m_MainForm  = null;
		public HyperMainForm? MainForm
		{
			get { return m_MainForm; }
		}
		public void SetMainForm(HyperMainForm? mf)
		{
			m_MainForm = mf;
			app = new HyperApp(mf);
			if (engine != null)
			{
				engine.AddHostObject("app", app);
			}

		}
		public V8ScriptEngine? engine = null;

        public HyperScript()
        {
			Init();
		}
		public void ExecuteScript(HyperScriptCode cd, ScriptKind sk)
		{
			if (engine == null) return;
			
			if((cd.GetScriptComplieZ(sk)==null))
			{
				string s = cd.GetScriptCode(sk);
				if (s != "") 
				{
					try
					{
						var scp = engine.Compile(s);
						cd.SetScriptComplieZ(sk,scp );
					}catch(Exception e)
					{
						alert(e);
						return;
					}
				}
			}
			try
			{
				if (cd.GetScriptComplieZ(sk) == null)
				{
					writeLine("err!");
					engine.Execute(cd.GetScriptCode(sk));
				}
				else
				{
					engine.Execute(cd.GetScriptComplieZ(sk));
				}
			}
			catch (Exception e)
			{
				alert(e);
				return;
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
				alert(e);
			}
		}
		public void DeleteScriptObject(string itemName)
		{
			if (engine != null)
			{
				engine.Execute($"delete {itemName};");
			}
		}
		public void AddScriptObjectNull(string itemName)
		{
			if (engine != null)
			{
				try
				{
					string s = $"{itemName} = null;";
					engine.Execute(s);
				}
				catch (Exception e)
				{
					alert(e);
				}
			}
		}
		public void AddScriptObject(string itemName, string item)
		{
			if (engine != null)
			{
				try
				{
					string ss = item.Replace("\"","\\\"");
					ss = ss.Replace("\r", "\\\r");
					ss = ss.Replace("\n", "\\\n");
					ss = ss.Replace("\t", "\\\t");
					string s = $"{itemName} = \"{ss}\";";
					engine.Execute(s);
				}catch(Exception e)
				{
					alert(e);
				}
			}
		}
		public void AddScriptObject(string itemName, int item)
		{
			if (engine != null)
			{
				try
				{
					engine.Execute($"{itemName} = {item};");
				}
				catch (Exception e)
				{
					alert(e);
				}
			}
		}
		public void AddScriptObject(string itemName, string [] items)
		{
			if (engine != null)
			{
				string s = "";
				if(items.Length>0)
				{
					for(int i=0; i< items.Length;i++)
					{
						if (s != "") s += ",";
						s += "\"" + items[i].Replace("\\","\\\\") + "\"";
					}
				}
				try
				{
					string cmd = $"{itemName} = [{s}];";
					engine.Execute(cmd);
				}
				catch (Exception e)
				{
					alert(e);
				}
			}
		}
		public void AddScriptObject(string itemName, bool flg)
		{
			if (engine != null)
			{
				string s = "";
				if (flg) s = "true"; else s = "false";
				try
				{
					engine.Execute($"{itemName} = {s};");
				}catch(Exception e)
				{
					alert(e);
				}
			}
		}
		public void AddHostObject(string itemName,Object target )
		{
			if (engine != null) engine.AddHostObject(itemName, target);
		}
		public void AddHostObject(string itemName,HostItemFlags flgs, Object target)
		{
			if (engine != null) engine.AddHostObject(itemName, flgs, target);
		}
		public void AddHostType(Type types)
		{
			if (engine != null) engine.AddHostType(types);
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
				"System.IO",
				"System.Collections",
				"System.Windows.Forms");

			engine.AddHostObject("dotnet", typeCollection);
			engine.AddHostObject("alert", (object)alert);
			engine.AddHostObject("write", (object)write);
			engine.AddHostObject("writeln", (object)writeLine);
			engine.AddHostObject("clr", (object)writeClear);
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
				typeof(HyperBaseForm),
				typeof(HyperMainForm),
				typeof(HyperFormList),
				typeof(HyperAppBase),
				typeof(HyperApp),
				typeof(HyperControl),
				typeof(ControlType),
				typeof(AppControlList),
				typeof(AppFormList),


			});
			if(app!=null) engine.AddHostObject("app",app);


		}
		public void InitForms(HyperMainForm mf)
		{
			if (engine != null)
			{
			}
		}
		private void InitControlsSub(HyperBaseForm? bf)
		{
			if ((engine == null) || (bf == null)) return;

			engine.Script[bf.Name] = new ExpandoObject();
			if (bf.Controls.Count > 0)
			{
				foreach (Control c in bf.Controls)
				{
					if (c is HyperControl)
					{
						engine.AddHostObject(bf.Name +"."+ c.Name, (HyperControl)c);
					}
				}
			}
		}
		public void InitControls(HyperMainForm? mf)
		{
			if ((engine == null) || (mf == null)) return;
			if(app!=null) engine.Script["result"] = app.result;
			if (mf.Controls.Count > 0)
			{
				foreach (Control c in mf.Controls)
				{
					if (c is HyperControl)
					{
						engine.AddHostObject(c.Name, (HyperControl)c);
					}
				}
			}
			if(mf.forms.Count>0)
			{
				foreach (var c in mf.forms)
				{
					if (c is HyperBaseForm)
					{
						InitControlsSub((HyperBaseForm)c);
					}
				}
			}

			if (app != null)
			{
				app.ListupControls();
			}
			else
			{
				app = new HyperApp(mf);
				app.ListupControls();

			}
		}
		static public string toString(object? o,bool isJson = false)
		{
			if (isJson) return objectToJson(o);


			string ret = "";

			if (o == null) {
				ret = "null";
			}else if (o is ScriptObject)
			{
				ret = ScriptObjectStr((ScriptObject?)o);
			}
			else if(o is bool)
			{
				ret = o.ToString().ToLower();
			}
			else if (o is Array)
			{
				foreach (object o1 in (Array)o)
				{
					if (ret != "") ret += ",";
					if (o1 == null)
					{
						ret += "null";
					}
					else
					{
						ret += toString(o1, isJson); ;
					}
				}
				ret = "[" + ret + "]";
			}
			else
			{
				try 
				{
					ret = o.ToString();

				}
				catch (Exception ex)
				{
					ret = ex.ToString();
				}
			}
			return ret;
		}
		static public string objectToJson(Object? Obj)
		{
			if (Obj != null)
			{
				try
				{
					var js = JsonSerializer.Serialize(Obj,
						new JsonSerializerOptions
						{
							WriteIndented = true,
							Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
						});
					return js;
				}
				catch(Exception e)
				{
					return e.ToString();
				}
			}
			else
			{
				return "null";
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
			if(m_MainForm!=null)
			{
				m_MainForm.OutputWrite(s);
			}
		}

		public void writeLine(object? s)
		{
			if (m_MainForm != null)
			{
				m_MainForm.OutputWriteLine(s);
			}
		}
		public void writeClear()
		{
			if (m_MainForm != null)
			{
				m_MainForm.OutputClear();
			}
		}
		public bool yesnoDialog(string cap,string title)
		{
			if (m_MainForm != null)
			{
				return answerDialog.Show(cap, title);
			}
			else
			{
				return false;
			}
		}
		public string[] getGlobalThis()
		{
			string[] ret = new string[0];
			if (engine != null)
			{
				string ss = "";
				foreach(var str in engine.Global.GetDynamicMemberNames())
				{
					if (ss != "") ss += ",";
					ss += str;
				}
				ret = ss.Split(',');
				/*
				alert(ss);

				var s = engine.Evaluate(
					@"value = '';for (var s in globalThis){if(value!='') value +=','; value += s;}value;"
					);
				if (s is string) {
					ret = ((string)s).Split(',');
				}
				*/
			}
			return ret;
		}
		static public string ScriptObjectStr(Object? so)
		{
			string ret = "";
			if (so == null) return "null";
			if( so is string)
			{
				ret = "\""+(string)so+ "\"";
			}
			else if(so is Boolean)
			{
				ret = ((Boolean)so).ToString().ToLower();
			}
			else if (so is ScriptObject) 
			{
				ScriptObject soo = (ScriptObject)so;
				if (soo.GetType().ToString().IndexOf("Array") >= 0)
				{
					foreach (int idx in soo.PropertyIndices)
					{
						if (ret != "") ret += ",";
						ret += ScriptObjectStr(soo.GetProperty(idx));
					}
					ret = "[" + ret + "]";
				}
				else
				{
					foreach (string n in soo.PropertyNames)
					{
						if (ret != "") ret += ",";
						object aa = soo.GetProperty(n);
						string nm = ScriptObjectStr(aa);
						ret += $"{n}:{nm}";
					}
					ret = "{" + ret + "}";

				}
			}
			else
			{
				ret = toString(so);
			}
			return ret;
		}
		static public string[] SplitSppce(string str)
		{
			string[] ret = new string[0];
			if (str.Length <= 0) return ret;
			int idx = 0;
			List<string> list = new List<string>();
			//list.Add(str);
			string blc = "";
			while (idx < str.Length)
			{
				string c = str.Substring(idx, 1);
				if ((c == " ") || (c == "\t"))
				{
					list.Add(blc);
					blc = "";
					idx++;
				}
				else if (c == "\"")
				{
					int idx2 = -1;
					for (int j = idx + 1; j < str.Length; j++)
					{
						string c1 = str.Substring(j - 1, 1);
						string c2 = str.Substring(j, 1);
						if ((c2 == "\"") && (c1 != "\\"))
						{
							idx2 = j;
							break;
						}
					}
					if (idx2 == -1)
					{
						blc += str.Substring(idx);
						list.Add(blc);
						blc = "";
						break;
					}
					else
					{
						blc += str.Substring(idx, idx2 - idx+1);
						idx = idx2 + 1;
					}
				}
				else
				{
					blc += c;
					idx++;
				}

			}
			if (blc != "") list.Add(blc);
			return list.ToArray();
		}
	}
}
