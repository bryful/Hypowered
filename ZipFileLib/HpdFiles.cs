namespace Hpd
{
	

	public class HpdFiles
	{
		private string m_FileName = "";
		public string FileName
		{
			get { return m_FileName; } 
			set { m_FileName = value; }
		}
		private readonly string FILEDIR = "file/";
		private readonly string PICTDIR = "pict/";
		private readonly string TEXTDIR = "text/";
		private readonly string WAVEDIR = "wave/";
		public HpdFiles() 
		{
		}

	}
	public enum FileItemType
	{
		/// <summary>
		/// ただのファイル
		/// </summary>
		File,
		/// <summary>
		/// 画像ファイル
		/// </summary>
		Pict,
		/// <summary>
		/// サウンドファイル
		/// </summary>
		Wave,
		/// <summary>
		/// テキストファイル
		/// </summary>
		Text
	}
	public class FileItem
	{
		private FileItemType m_FileItemType = FileItemType.File;
		/// <summary>
		/// ファイルの種類
		/// </summary>
		public FileItemType FileItemType { get { return m_FileItemType; } }

		public Bitmap? Bitmap = null;
		public string Name = "";
		public bool IsRes = true;
		public bool Err = false;
		public int Index = -1;
		public FileItem(Bitmap? bitmap, string name)
		{
			Bitmap = bitmap;
			this.Name = name;
			IsRes = false;
			Err = false;
		}
		public FileItem(Bitmap? bitmap, string name, int idx, bool isRes, bool err)
		{
			Bitmap = bitmap;
			this.Name = name;
			Index = idx;
			IsRes = isRes;
			Err = err;
		}
	}
}