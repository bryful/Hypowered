using BRY;
using System.Security.Cryptography;

namespace Hypowered
{
	internal static class Program
	{
		static private System.Threading.Mutex? _mutex = null;
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			HArgs hargs = new HArgs(args);
			string AId = hargs.FileName;
			if (AId == "") AId = Application.ExecutablePath;
			AId = Path.GetFileNameWithoutExtension(AId);
			_mutex = new System.Threading.Mutex(false, AId);
			
			if(_mutex.WaitOne(0, false) == false) 
			{
				MessageBox.Show("Running/"+ AId);
				PipeData pd = new PipeData(args, PIPECALL.DoubleExec);
				F_Pipe.Client(AId, pd.ToJson()).Wait();
				return;
			}
			else
			{
				//ñ≥Ç©Ç¡ÇΩÇÁÉäÉäÅ[ÉX
				try
				{
					_mutex.ReleaseMutex();
				}
				catch
				{
					MessageBox.Show("program:ReleaseMutex");
				}
				_mutex.Dispose();
			}
			MessageBox.Show("none/"+AId);
			///í èÌãNìÆ
			ApplicationConfiguration.Initialize();
			HyperMainForm mf = new HyperMainForm();
			Application.Run(mf);
			
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			//ApplicationConfiguration.Initialize();
			//Application.Run(new HyperMainForm());
			//Form1 mf = new Form1();
		}
	}
}