using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Windows.Media.Imaging;

namespace Hypowered
{
    public class ZipUtil
    {
        private string m_FileName = "";
        public ZipUtil()
        {

        }
        static public bool AddFromFile(string srcFile, string zipFile)
        {
            bool ret = false;
            try
            {
                using (var z = ZipFile.Open(zipFile, ZipArchiveMode.Update))
                {
                    z.CreateEntryFromFile(srcFile, Path.GetFileName(srcFile), CompressionLevel.NoCompression);
                    ret = true;
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
        static public string[] EntryList(string zipFile)
        {
            List<string> list = new List<string>();
            using (ZipArchive a = ZipFile.OpenRead(zipFile))
            {
                foreach (ZipArchiveEntry e in a.Entries)
                {
                    list.Add(e.Name);
                    //Console.WriteLine("名前       : {0}", e.Name);
                    //ディレクトリ付きのファイル名
                    //Console.WriteLine("フルパス   : {0}", e.FullName);
                    //Console.WriteLine("サイズ     : {0}", e.Length);
                    //Console.WriteLine("圧縮サイズ : {0}", e.CompressedLength);
                    //Console.WriteLine("更新日時   : {0}", e.LastWriteTime);
                }
            }
            return list.ToArray();
        }
        static public MemoryStream? GetEntry(string zipName, string entryName)
        {
            MemoryStream? ret = null;
            using (ZipArchive a = ZipFile.OpenRead(zipName))
            {
                ZipArchiveEntry? e = a.GetEntry(entryName);
                if (e != null)
                {
                    using (Stream stream = e.Open())
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            stream.CopyTo(memoryStream);
                            memoryStream.Position = 0;
                            ret = memoryStream;
                            //ret = memoryStream.ToArray();
                            /*
							BitmapImage bitmap = new BitmapImage();
							bitmap.BeginInit();
							bitmap.CacheOption = BitmapCacheOption.OnLoad;
							bitmap.StreamSource = memoryStream;
							bitmap.DecodePixelHeight = imageHeight;
							bitmap.EndInit();
							return new WriteableBitmap(bitmap);
							*/
                        }
                    }
                }
            }
            return ret;
        }
        static public Bitmap? GetEntryBitmap(string zipName, string entryName)
        {
            Bitmap? ret = null;
            using (ZipArchive a = ZipFile.OpenRead(zipName))
            {
                ZipArchiveEntry? e = a.GetEntry(entryName);
                if (e != null)
                {
                    using (Stream stream = e.Open())
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            stream.CopyTo(memoryStream);
                            memoryStream.Position = 0;
                            ret = new Bitmap(memoryStream);
                            /*BitmapImage bitmap = new BitmapImage();
							bitmap.BeginInit();
							bitmap.CacheOption = BitmapCacheOption.OnLoad;
							bitmap.StreamSource = memoryStream;
							bitmap.DecodePixelHeight = imageHeight;
							bitmap.EndInit();
							return new WriteableBitmap(bitmap);*/
                        }
                    }
                }
            }
            return ret;
        }
    }
}
