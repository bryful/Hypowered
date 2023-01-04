﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public enum Option
	{
		None,
		Open,
		Create,
	}
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
			public string Option
			{
				get 
				{
					if(m_IsOption)
					{
						return m_Value.Substring(1);
					}
					else
					{
						return "";
					}
				}
			}
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
		private Option m_Option = Option.None;
		public Option Option { get { return m_Option; } }
		private string m_FileName = "";
		public string FileName { get { return m_FileName; } }

		public HArgs(string[] args)
		{
			Args = new Arg[args.Length];
			int idx = 0;
			m_Option = Option.None;
			if (args.Length > 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					Args[idx] = new Arg(args[i], i);
					if (Args[idx].IsOption) 
					{
						string op = "";
						op = Args[idx].Option.ToLower();
						if (m_Option == Option.None)
						{
							switch (op)
							{
								case "n":
								case "new":
								case "create":
									m_Option = Option.Create;
									break;
								case "o":
								case "open":
								case "load":
								case "ld":
									m_Option = Option.Open;
									break;

							}
						}
					}
					else
					{
						if (m_FileName == "")
						{
							m_FileName = args[i];
						}

					}

				}
			}
		}
	}
}