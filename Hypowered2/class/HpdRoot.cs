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

namespace Hpd
{
	public class HpdRoot
	{
		public HpdMainForm Main { get; set; }
		[ScriptUsage(ScriptAccess.None)]
		public HpdRoot(HpdMainForm main)
		{
			Main = main;
		}
		public HpdControlCollection Items
		{
			get { return Main.Items; }
		}
		public int NumItems { get { return Main.Items.Count; } }
		public HpdControl? Item(int idx)
		{
			return Main.Items[idx];
		}
		public HpdControl? Item(string key)
		{
			return Main.Items[key];
		}
		private Object? m_Result = null;
		[Category("Hypowered")]
		public Object? Result
		{
			get { return m_Result; }
		}
		public void Alert(object? o)
		{
			Main.Alert(o);
		}
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
	}
}

