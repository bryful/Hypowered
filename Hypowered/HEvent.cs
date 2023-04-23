using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class TargetFormChangedArgs : EventArgs
	{
		public HForm? HForm = null;
		public TargetFormChangedArgs(HForm? idx)
		{
			HForm = idx;
		}
	}
	public class TargetControlChangedArgs : EventArgs
	{
		public HControl? HControl = null;
		public TargetControlChangedArgs(HControl? idx)
		{
			HControl = idx;
		}
	}

	public class FormChangedEventArgs : EventArgs
	{
		public String Name;
		public int Index;
		public FormChangedEventArgs(string n, int index)
		{
			Name = n;
			Index = index;
		}
	}
	public class ControlChangedEventArgs : EventArgs
	{
		public string Name;
		public int FormIndex;
		public int CtrlIndex;
		public ControlChangedEventArgs(string n, int findex, int cindex)
		{
			Name = n;
			CtrlIndex = cindex;
			FormIndex = findex;
		}
	}

	public class SelectedChangedEventArgs : EventArgs
	{
		public bool[] Selecteds = new bool[0];
		public SelectedChangedEventArgs(bool[] n)
		{
			Selecteds = n;
		}
	}
	// *********************************************************
	public class ScriptModeChangedEventArgs : EventArgs
	{
		public bool IsScript;
		public ScriptModeChangedEventArgs(bool v)
		{
			IsScript = v;
		}
	}
	// *********************************************************
}
