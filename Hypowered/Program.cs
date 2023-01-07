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
				F_W.RelatingFile(Def.DefaultExt, "Hypowered hyfp file");
				return;
			}else if (hargs.Option == Option.UnInstallExt)
			{
				F_W.UnRelatingFile(Def.DefaultExt);
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
				///í èÌãNìÆ
				ApplicationConfiguration.Initialize();
				HyperMainForm mf = new HyperMainForm();
				Application.Run(mf);
			}

			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			//ApplicationConfiguration.Initialize();
			//Application.Run(new HyperMainForm());
			//Form1 mf = new Form1();
		}
	}
}