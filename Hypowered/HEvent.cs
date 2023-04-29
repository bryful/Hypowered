using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class SelectObjectsChangedArgs : EventArgs
	{
		public object?[]? objs;
		public SelectObjectsChangedArgs(object?[]? idx)
		{
			objs = idx;
		}
	}
	public class ControlNameChangedEventArgs : EventArgs
	{
		public string Name;
		public int Index;
		public ControlNameChangedEventArgs(string n, int index)
		{
			Name = n;
			Index = index;
		}
	}
	public class SelectedChangedEventArgs : EventArgs
	{
		public bool Selected;
		public int Index;
		public SelectedChangedEventArgs(bool n, int index)
		{
			Selected = n;
			Index = index;
		}
	}
	public class SelectedArrayChangedEventArgs : EventArgs
	{
		public bool[] Selecteds = new bool[0];
		public SelectedArrayChangedEventArgs(bool[] n)
		{
			Selecteds = n;
		}
	}
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
	public class MenuChangedEventArgs : EventArgs
	{
		public HMenuItem? Menu;
		public MenuChangedEventArgs(HMenuItem? v)
		{
			Menu = v;
		}
	}
	// *********************************************************
}
