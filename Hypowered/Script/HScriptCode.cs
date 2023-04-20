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
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RoslynPad.Roslyn;
using RoslynPad.Editor;
using System.Text.Json.Nodes;
using Microsoft.ClearScript.V8;

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
		private HScriptType[] m_HScriptTypes = new HScriptType[0];
		public HScriptType[] HScriptTypes { get { return m_HScriptTypes; } }


		private string[] m_HScriptTypeNames = new string[0];
		public string[] HScriptTypeNames { get { return m_HScriptTypeNames; } }

		private ScriptItem[] m_Codes = new ScriptItem[0];
		public ScriptItem[] Codes { get { return m_Codes; } }
		public int Length { get { return HScriptTypeNames.Length; } }
		private int IndexOf(HScriptType st)
		{
			int ret = -1;
			if(m_HScriptTypes.Length > 0)
			{
				for(int i=0;i<m_HScriptTypes.Length;i++)
				{
					if (m_HScriptTypes[i] == st)
					{
						ret = i; 
						break;
					}
				}
			}
			return ret;
		}
		public ScriptItem? Code(HScriptType st)
		{
			ScriptItem? ret = null;
			int idx = IndexOf(st);
			if(idx>=0)
			{
				ret = m_Codes[idx];
			}
			return ret;
		}

		public HScriptCode()
		{
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
			m_Codes = Array.Empty<ScriptItem>();
			m_HScriptTypes = Array.Empty<HScriptType>();
			m_HScriptTypeNames = Array.Empty<string>();
			if (lst.Count > 0)
			{
				m_Codes = new ScriptItem[lst.Count];
				m_HScriptTypes = new HScriptType[lst.Count];
				m_HScriptTypeNames = new string[lst.Count];
				string[] ns = Enum.GetNames(typeof(HScriptType));
				int idx = 0;
				foreach (HScriptType t in lst)
				{
					m_Codes[idx] = new ScriptItem();
					m_HScriptTypes[idx] = t;
					m_HScriptTypeNames[idx] = ns[(int)t];
					idx++;
				}
			}
		}

	}
	public class ScriptItem
	{
		public string Code { get; set; } = "";
		public V8Script? Script { get; set; } = null;

	}
}