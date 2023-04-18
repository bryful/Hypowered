using Microsoft.ClearScript;
using Microsoft.CodeAnalysis.CSharp.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class HRoot
	{
		public MainForm? mainForm { get; set; } = null;
		public HForm? thisForm { get; set; } = null;
		[ScriptUsage(ScriptAccess.None)]
		public void SetMainForm(MainForm? mf, HForm? hf)
		{
			mainForm = mf;
			thisForm = hf;
		}
		public HRoot()
		{
		}
		public HControl? Item(int idx )
		{
			
			HControl? ret = null;
			if (thisForm != null)
			{
				if ((idx >= 0) && (idx < thisForm.Controls.Count))
				{
					if (thisForm.Controls[idx] is HControl)
					{
						ret = (HControl)thisForm.Controls[idx];
					}
				}
			}
			return ret;
		}
		public int numItems 
		{ 
			get 
			{
				int ret = 0;
				if (thisForm != null)
				{
					ret = thisForm.Controls.Count;
				}
				return ret;
			} 
		}
		private Object? m_Result = null;
		[Category("Hypowered")]
		public Object? Result
		{
			get { return m_Result; }
		}
		public void Alert(object? o)
		{
			if ((mainForm != null)&&(thisForm!=null))
			{
				mainForm.Alert(thisForm, o);
			}
		}
		/*
		public void WriteLine(object? o)
		{
			Main.ConsoleWriteLine(o);
		}
		public void Write(object? o)
		{
			Main.ConsoleWrite(o);
		}
		public void cls()
		{
			Main.ConsoleClear();
		}
		public int YesNoDialog(string comment, string title)
		{
			return Main.YesNoDialog(comment,title);
		}
		*/
	}
}

