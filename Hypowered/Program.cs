using BRY;
using System.Security.Cryptography;

namespace Hypowered
{
	internal static class Program
	{
		public const string MyAppId = "Hypowered"; // GUIDなどユニークなもの
																	 // *******************************************************************************************
		private static System.Threading.Mutex _mutex = new System.Threading.Mutex(false, MyAppId);
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			//Hypoweredのいずれかが起動している
			bool IsRunning = (_mutex.WaitOne(0, false)) == false;

			if(IsRunning)
			{
				string filename = (new HArgs(args)).First;
				if (filename != "")
				{
					string nID = Path.GetFileNameWithoutExtension(filename);
					System.Threading.Mutex _mutexA
						= new System.Threading.Mutex(false, nID);
					if((_mutex.WaitOne(0, false)) == false)
					{
						PipeData pd = new PipeData(args, PIPECALL.DoubleExec);
						F_Pipe.Client(nID, pd.ToJson()).Wait();
						return;
					}
				}
			}
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			//ApplicationConfiguration.Initialize();
			//Application.Run(new HyperMainForm());
			//Form1 mf = new Form1();
			ApplicationConfiguration.Initialize();
			HyperMainForm mf = new HyperMainForm();
			Application.Run(mf);
		}
	}
}