using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class HArgs
	{
		public class Arg
		{
			private string m_Value="";
			public string Value { get { return m_Value; } }
			private int m_Index =-1;
			public int Index { get { return m_Index; } }
			private bool m_IsOption=false;
			public bool IsOption { get { return m_IsOption; } }
			public Arg(string value, int index)
			{
				m_Value = value;
				this.m_Index = index;
				m_IsOption = ((value != "") && ((value[0] == '-') || ((value[0] == '/'))));

			}
		}

		public Arg[] Args = new Arg[0];
		public int Count
		{ get { return Args.Length; } }
		public Arg this[int idx]
		{
			get
			{
				Arg ret = new Arg("",-1);
				if((idx>=0)&&(idx<Count))
				{
					ret = Args[idx];
				}
				return ret;
			}

		}
		public HArgs(string[] args)
		{
			Args = new Arg[args.Length];
			int idx = 0;
			if (args.Length > 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					Args[idx] = new Arg(args[i], i);
				}
			}

		}
		public string First
		{
			get
			{
				string ret = "";
				for(int i=0;i< Args.Length;i++)
				{
					if (Args[i].IsOption==false)
					{
						ret = Args[i].Value;
						break;
					}
				}
				return ret;
			}
		}

	}
}
