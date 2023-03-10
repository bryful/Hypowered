using BRY;
using System.Diagnostics;

namespace Hypowered
{
	internal static class Program
	{

		static public bool IsWindow(string idname)
		{
			bool ret = false;
			Process[] ps =Process.GetProcessesByName("Hypowered");
			if(ps.Length > 0)
			{
				foreach(var ps2 in ps)
				{
					if (string.Compare( ps2.MainWindowTitle,idname,true)==0)
					{
						ret = true;
						break;
					}
				}
			}
			return ret;
		}
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			HArgs hargs = new HArgs(args);
			if(hargs.Option== Option.InstallExt)
			{
				F_W.SettupConsole();
				F_W.RelatingFile(Def.DefaultExt, "Hypowered hyfp file");
				Console.WriteLine($"拡張子{Def.DefaultExt}を登録しました");
				F_W.EndConsole();
				return;
			}else if (hargs.Option == Option.UnInstallExt)
			{
				F_W.SettupConsole();
				F_W.UnRelatingFile(Def.DefaultExt);
				Console.WriteLine($"拡張子{Def.DefaultExt}を解除しました");
				F_W.EndConsole();
				return;
			}else if (hargs.Option == Option.Call)
			{
				string cl = hargs.CommandLine(1);
				F_W.ProcessStart(Application.ExecutablePath, cl);
				return;
			}else if(hargs.Option == Option.EnvSet)
			{
				string ENV = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + Def.ENV_HOME_PATH;
				Def.SetDirectoryToEnvDialog(ENV);
				return;
			}
			else if (hargs.Option == Option.EnvDelete)
			{
				F_W.SettupConsole();
				string ENV = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + Def.ENV_HOME_PATH;
				string? s = Def.GetENV(ENV);
				if ((s == null)||(s=="")) 
				{
					Console.WriteLine($"環境変数{ENV}は異常です。");
				}
				else
				{
					Def.SetENV(ENV, "");
					Console.WriteLine($"環境変数{ENV}を削除しました");
				}
				F_W.EndConsole();
				return;
			}


			string AId = hargs.FileName;
			if (AId == "") AId = Application.ExecutablePath;
			AId = Path.GetFileNameWithoutExtension(AId);
			if(IsWindow(AId))
			{
				PipeData pd = new PipeData(args, PIPECALL.DoubleExec);
				F_Pipe.Client(AId, pd.ToJson()).Wait();
			}
			else
			{
				///通常起動
				// To customize application configuration such as set high DPI settings or default font,
				// see https://aka.ms/applicationconfiguration.
				ApplicationConfiguration.Initialize();
				HyperMainForm mf = new HyperMainForm();
				Application.Run(mf);
			}
		}
	}
}