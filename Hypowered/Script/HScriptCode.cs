using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Immutable;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RoslynPad.Roslyn;
using RoslynPad.Editor;
using Microsoft.ClearScript.V8;
using ExCSS;

namespace Hypowered
{
	public enum HScriptType
	{
		Click,
		DoubleClick,
		ValueChanged,
		SelectedIndexChanged,
		DragDrop,
		FormLoad,
		FormClosed,
		KeyPress
	}
	public class HScriptCode
	{
		private string[] m_HScriptTypeNames =new string[0];
		private ScriptItem[] m_ScriptItems = new ScriptItem[0];
		public ScriptItem[] ScriptItems { get { return m_ScriptItems; } }
		public HScriptType[] HScriptTypes 
		{ 
			get 
			{ 
				List<HScriptType> result = new List<HScriptType>();
				foreach (ScriptItem si in m_ScriptItems)
				{
					result.Add(si.ScriptType);
				}
				return result.ToArray(); 
			} 
		}
		public string[] HScriptTypeNames 
		{ 
			get 
			{
				List<string> result = new List<string>();
				foreach (ScriptItem si in m_ScriptItems)
				{
					result.Add(si.ScriptTypeName);
				}
				return result.ToArray();
			}
		}

		public int Length { get { return ScriptItems.Length; } }

		public string [] Codes 
		{ 
			get { return GetCodes(); } 
			set { SetCodes(value); }
		}
		public string[] GetCodes()
		{
			int cnt = m_ScriptItems.Length;
			string[] ret = new string[cnt];
			if(cnt > 0)
			{
				for(int i=0; i<cnt;i++) ret[i] = m_ScriptItems[i].Code;
			}
			return ret;
		}
		public void SetCodes(string[] sa)
		{
			int cnt = m_ScriptItems.Length;
			if (sa.Length != cnt) return;

			for (int i = 0; i < cnt; i++)
			{
				m_ScriptItems[i].Code = sa[i];
			}
		}

		private int IndexOf(HScriptType st)
		{
			int ret = -1;
			if(m_ScriptItems.Length > 0)
			{
				for(int i=0;i< m_ScriptItems.Length;i++)
				{
					if (m_ScriptItems[i].ScriptType == st)
					{
						ret = i; 
						break;
					}
				}
			}
			return ret;
		}
		private int IndexOf(string key)
		{
			int ret = -1;
			if (m_ScriptItems.Length > 0)
			{
				for (int i = 0; i < m_ScriptItems.Length; i++)
				{
					if (m_ScriptItems[i].ScriptTypeName == key)
					{
						ret = i;
						break;
					}
				}
			}
			return ret;
		}
		public ScriptItem? Items(HScriptType st)
		{
			ScriptItem? ret = null;
			int idx = IndexOf(st);
			if(idx>=0)
			{
				ret = m_ScriptItems[idx];
			}
			return ret;
		}
		public ScriptItem? Items(string key)
		{
			ScriptItem? ret = null;
			int idx = IndexOf(key);
			if (idx >= 0)
			{
				ret = m_ScriptItems[idx];
			}
			return ret;
		}

		public HScriptCode()
		{
			m_HScriptTypeNames = Enum.GetNames(typeof(HScriptType));
		}

		public void Setup(params HScriptType[] a)
		{
			List<HScriptType> lst = new List<HScriptType>();
			if (a.Length > 0)
			{
				for (int i = 0; i < a.Length; i++)
				{
					if (lst.IndexOf(a[i]) < 0)
					{
						lst.Add(a[i]);
					}
				}
			}
			m_ScriptItems = Array.Empty<ScriptItem>();
			if (lst.Count > 0)
			{
				m_ScriptItems = new ScriptItem[lst.Count];
				string[] ns = Enum.GetNames(typeof(HScriptType));
				int idx = 0;
				foreach (HScriptType t in lst)
				{
					m_ScriptItems[idx] = new ScriptItem();
					m_ScriptItems[idx].ScriptType = t;
					m_ScriptItems[idx].ScriptTypeName = ns[(int)t];
					idx++;
				}
			}
		}
		public JsonArray? ToJson()
		{
			JsonArray? result = new JsonArray();
			if (m_ScriptItems.Length>0)
			{
				foreach(ScriptItem item in m_ScriptItems)
				{ 
					result.Add(item.ToJson());
				}
			}
			return result;
		}
		public void FromJson(JsonArray? arr)
		{
			if ((arr == null)||(arr.Count<=0)) return;
			//List<ScriptItem> lst = new List<ScriptItem>();
			foreach (var item in arr) 
			{
				if(item is JsonObject)
				{
					ScriptItem si = new ScriptItem();
					si.FromJson((JsonObject?)item);
					if (si.ScriptTypeName != "")
					{
						int idx = IndexOf(si.ScriptTypeName);
						if (idx >= 0)
						{
							m_ScriptItems[idx].SetCode(si.Code);
						}
					}
				}
			}
		}

	}
	public class ScriptItem
	{
		public HScriptType ScriptType { get; set; } = HScriptType.Click;
		public string ScriptTypeName { get; set; } = "";

		public string Code { get; set; } = "";
		public V8Script? Script { get; set; } = null;

		public void SetCode(string s)
		{
			Code = s;
			Script = null;
		}
		public ScriptItem() { }

		public JsonObject ToJson()
		{
			JsonObject jo = new JsonObject();
			jo.Add("Code", Code);
			jo.Add("ScriptTypeName", ScriptTypeName);
			return jo;
		}
		public void FromJson(JsonObject? jo)
		{
			if (jo == null) return;
			if(jo.ContainsKey("Code"))
			{
				string? s =jo["Code"].GetValue<string?>();
				if(s != null) 
				{
					Code = s;
				}
			}
			if (jo.ContainsKey("ScriptTypeName"))
			{
				string? s = jo["ScriptTypeName"].GetValue<string?>();
				if (s != null)
				{
					HScriptType? st = HScriptTypeUtil.ScriptType(s);
					if (st != null)
					{
						ScriptType = (HScriptType)st;
						ScriptTypeName = s;
					}
				}
			}
		}
	}

	public class HScriptTypeUtil
	{
		static private string[] m_Names = new string[0];
		public HScriptTypeUtil()
		{
			m_Names = Enum.GetNames(typeof(HScriptType));
		}
		static private int IndexOf(string key)
		{
			if(m_Names.Length==0)
			{
				m_Names = Enum.GetNames(typeof(HScriptType));
			}
			int ret = -1;
			for(int i =0; i< m_Names.Length;i++)
			{
				if(m_Names[i] == key)
				{
					ret = i; 
					break;
				}
			}
			return ret;
		}
		static public HScriptType? ScriptType(string key)
		{
			HScriptType? ret = null;
			int idx = IndexOf(key);
			if (idx >= 0) ret = (HScriptType)idx;

			return ret;
		}
		static public string? Name(HScriptType st)
		{
			if (m_Names.Length == 0)
			{
				m_Names = Enum.GetNames(typeof(HScriptType));
			}
			if (((int)st>=0)&&((int)st< m_Names.Length))
			{
				return m_Names[(int)st];
			}
			else
			{
				return null;
			}
		}
	}
}