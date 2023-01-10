using System;
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
		InstallExt,
		UnInstallExt,
		Call,
		EnvSet,
		EnvDelete
	}
	public class HArgs
	{

		public class Arg
		{
			private string m_Value = "";
			public string Value { get { return m_Value; } }
			public string CommandValue
			{
				get
				{
					if (m_IsOption)
					{
						return m_Value;
					}
					else
					{
						return "\"" + m_Value + "\"";
					}
				}
			}
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
		public string CommandLine(int start =0)
		{
			string ret = "";
			if(Args.Length-start>0)
			{
				for(int i=start;i< Args.Length;i++)
				{
					if (Args[i] == null) continue;
					ret += " ";
					ret += Args[i].CommandValue;
				}
			}
			return ret;
		}
		private Option m_Option = Option.None;
		public Option Option { get { return m_Option; } }
		private string m_FileName = "";
		public string FileName { get { return m_FileName; } }
		public void SetArgs(string[] args)
		{
			Args = new Arg[0];
			int idx = 0;
			m_Option = Option.None;
			if (args.Length > 0)
			{
				List<Arg> list = new List<Arg>();
				for (int i = 0; i < args.Length; i++)
				{
					if ((args[i] == null)||(args[i]=="")) continue;
					if (args[i] == Application.ExecutablePath) continue;
					Arg arg = new Arg(args[i], idx); idx++;
					list.Add(arg);
					if (arg.IsOption)
					{
						string op = "";
						op = arg.Option.ToLower();
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
								case "l":
								case "ld":
								case "load":
									m_Option = Option.Open;
									break;
								case "inst":
								case "installext":
									if (args.Length == 1)
									{
										m_Option = Option.InstallExt;
									}
									break;
								case "uninst":
								case "uninstallext":
									if (args.Length == 1)
									{
										m_Option = Option.UnInstallExt;
									}
									break;
								case "call":
								case "callsystem":
									if (i == 0)
									{
										m_Option = Option.Call;
									}
									break;
								case "envset":
								case "env":
									if (args.Length == 1)
									{
										m_Option = Option.EnvSet;
									}
									break;
								case "envdelete":
								case "envdel":
								case "envremove":
								case "envclear":
								case "envreset":
									if (args.Length == 1)
									{
										m_Option = Option.EnvDelete;
									}
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
				Args = list.ToArray();
			}
		}

		public HArgs()
		{
		}
		public HArgs(string[] args)
		{
			SetArgs(args);
		}
	}
}
