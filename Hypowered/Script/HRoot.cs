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
			if(mainForm!=null)
			{
				mainForm.TargetFormChanged -= (sender, e) => { SetHForm(e.HForm); };
				mainForm.TargetFormChanged += (sender, e) => { SetHForm(e.HForm); };
			}
		}
		[ScriptUsage(ScriptAccess.None)]
		public void SetHForm(HForm? hf)
		{
			thisForm = hf;
		}
		public HRoot()
		{
		}
		public HControl? controls(int idx )
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
		public int numControls 
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
		public Object? result
		{
			get { return m_Result; }
		}
		public void SetResult(object? o) { m_Result = o; }
		public void alert(object? o)
		{
			if (thisForm == null) return;

			using (AlertForm dlg = new AlertForm())
			{
				dlg.Text = HUtils.ToStr(o);
				dlg.TopMost = thisForm.TopMost;
				if (dlg.ShowDialog(thisForm) == DialogResult.OK)
				{
				}
			}
		}
		public void exit()
		{
			Application.Exit();
		}
		public void writeLine(object? o)
		{
			if(mainForm!= null) 
				mainForm.WriteLine(o);
		}
		public void write(object? o)
		{
			if (mainForm != null)
				mainForm.Write(o);
		}
		public void cls()
		{
			if (mainForm != null)
				mainForm.Cls();
		}
		public bool yesNoDialog(string comment, string title)
		{
			if (mainForm != null)
			{
				return mainForm.YesNoDialog(comment, title);
			}
			else
			{
				return false;
			}
		}
	}
}

