
using Microsoft.ClearScript;
using System.Reflection;

namespace Hpd
{
	partial class HpdForm
	{
		

		static public void PropListToClipboard(Type t, string nm)
		{
			string s = "";
			foreach (MemberInfo mi in t.GetMembers())
			{
				if (mi.MemberType == MemberTypes.Event)
				{
					s += $"//{nm}.{mi.Name}+=(sender,e)=>{{ On{mi.Name}(e);}};\r\n";
				}
			}
			s += "********************************\r\n";
			foreach (var pi in t.GetProperties())
			{

				s += $"[Category(\"Hypowered\")]\r\n";
				s += $"public {pi.PropertyType.FullName} {pi.Name}\r\n";
				s += $"{{\r\n";
				s += $"\tget {{ return {nm}.{pi.Name}; }}\r\n";
				s += $"\tset {{ {nm}.{pi.Name} = value; }}\r\n";
				s += $"}}\r\n";
			}


			Clipboard.SetText(s);
		}
		static public void PropertySetToClipboard(Type t)
		{
			string s = "";
			foreach (var pi in t.GetProperties())
			{

				s += $"[Category(\"Hypowered\"),ScriptUsage(ScriptAccess.None), Browsable(false)]\r\n";
				s += $"public new {pi.PropertyType.FullName}  {pi.Name}\r\n";
				s += $"{{\r\n";
				s += $"\tget {{ return base.{pi.Name}; }}\r\n";
				s += $"\tset {{ bese.{pi.Name} = value; }}\r\n";
				s += $"}}\r\n";
			}
			Clipboard.SetText(s);
		}
		static public void PropertyToClipboard(Type t)
		{
			string s = "";
			foreach (var pi in t.GetProperties())
			{
				s += $"{pi.Name} ({pi.PropertyType.FullName})\r\n";
			}
			Clipboard.SetText(s);
		}
		[ScriptUsage(ScriptAccess.None)]
		static public string ScriptObjectStr(Object? so)
		{
			string ret = "";
			if (so == null) return "null";
			if (so is string)
			{
				ret = "\"" + (string)so + "\"";
			}
			else if (so is Boolean)
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
				ret = ToStr(so);
			}
			return ret;
		}
		static public string ToStr(object? obj)
		{
			string ret = "";

			if (obj == null)
			{
				ret = "null";
			}
			else if (obj is ScriptObject)
			{
				ret = ScriptObjectStr((ScriptObject?)obj);
			}
			else if (obj is bool)
			{
				ret = obj.ToString().ToLower();
			}
			else if (obj is Array)
			{
				foreach (object o1 in (Array)obj)
				{
					if (ret != "") ret += ",";
					if (o1 == null)
					{
						ret += "null";
					}
					else
					{
						ret += ToStr(o1);
					}
				}
				ret = "[" + ret + "]";
			}
			else
			{
				try
				{
					ret = obj.ToString();

				}
				catch (Exception ex)
				{
					ret = ex.ToString();
				}
			}
			if(ret == null ) { ret= "null"; }
			return ret;
		}
		
		public bool ControlMoveUp(HpdControl hc)
		{
			bool b = HU.ControlMoveUp(hc);
			if (b) { ListupControls();AutoLayout(); }
			return b;
		}
		public bool ControlMoveDown(HpdControl hc)
		{
			bool b = HU.ControlMoveDown(hc);
			if (b) { ListupControls(); AutoLayout(); }
			return b;
		}
		public HpdControl? ControlRemove(HpdControl hc)
		{
			return HU.ControlRemove(hc);
		}
		public HpdControl? CutCtrl()
		{
			if(this is HpdMainForm)
			{
				return HU.CutCtrl((HpdMainForm)this);
			}else { return null; }
		}
		public HpdControl? PasteCtrl()
		{
			if (this is HpdMainForm)
			{
				return HU.PasteCtrl((HpdMainForm)this);
			}
			else { return null; }
		}
	}
}