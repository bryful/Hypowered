﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;

namespace Hypowered
{
    public class ZipUtil
    {
        private string m_FileName = "";
        public ZipUtil()
        {

        }
        /// <summary>
        /// Zipにファイルを追加する
        /// </summary>
        /// <param name="zipFile"></param>
        /// <param name="srcFile"></param>
        /// <returns></returns>
        static public bool AddFromFile( string zipFile,string entryName, string srcFile)
        {
            bool ret = false;
            try
            {
				using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Update))
                {
					var fileEntry = archive.GetEntry(entryName);
					if (fileEntry != null)
					{
						fileEntry.Delete();
					}
					archive.CreateEntryFromFile(srcFile, entryName, CompressionLevel.NoCompression);
                    ret = true;
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
		static public bool ExtractToFile(string zipFile, string entryName,string dstDir)
		{
			bool ret = false;
            using (ZipArchive a = ZipFile.OpenRead(zipFile))
            {
                ZipArchiveEntry? e = a.GetEntry(entryName);
                if (e != null)
                { 
                    e.ExtractToFile(Path.Combine(dstDir,entryName), true);
                    ret = true;
                }
            }
			return ret;
		}
		/// <summary>
		/// 書庫内のファイル一覧
		/// </summary>
		/// <param name="zipFile"></param>
		/// <returns></returns>
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
        /// <summary>
        /// メモリストリームに
        /// </summary>
        /// <param name="zipName"></param>
        /// <param name="entryName"></param>
        /// <returns></returns>
        static public MemoryStream? GetEntryToStream(string zipName, string entryName)
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

                        }
                    }
                }
            }
            return ret;
        }
		static public string? GetEntryToStr(string zipName, string entryName)
        {
            //  string str = Encoding.GetEncoding("Shift_JIS").GetString(byte2str_arr);
            string? ret = null;
            byte[]? bs = GetEntryToByte(zipName, entryName);
            if (bs != null)
            {
				ret = System.Text.Encoding.UTF8.GetString(bs);
				if (ret != null) ret = Regex.Unescape(ret);
			}
			return ret;

		}
		static public bool SetEntryFromStr(string zipName, string entryName, string s)
        {
			byte[] data = System.Text.Encoding.UTF8.GetBytes(s);
            return SetEntryFromByte(zipName, entryName, data);

		}
		static public byte[]? GetEntryToByte(string zipName, string entryName)
		{
            byte[]? ret = null;
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
							ret = memoryStream.ToArray();
						}
					}
				}
			}
			return ret;
		}
        /// <summary>
				/// 書庫内のファイルを削除
				/// </summary>
				/// <param name="zipName"></param>
				/// <param name="entryName"></param>
				/// <returns></returns>
		static public bool DeleteEntry(string zipName, string entryName)
		{
            bool ret = false;
			using (var archive = ZipFile.Open(zipName, ZipArchiveMode.Update))
			{
				var fileEntry = archive.GetEntry(entryName);
                if(fileEntry != null)
                {
					fileEntry.Delete();
				}
			}
			return ret;
		}
		static public bool SetEntryFromByte(string zipName, string entryName, byte[] bs)
		{
            bool ret = false;
			using (var archive = ZipFile.Open(zipName, ZipArchiveMode.Update))
			{
				var fileEntry = archive.GetEntry(entryName);
				if (fileEntry != null)
				{
					fileEntry.Delete();
				}
				var entry = archive.CreateEntry(entryName, CompressionLevel.NoCompression);
                if (entry != null)
                {
                    using (var es = entry.Open())
                    {
                        try
                        {
                            es.Write(bs, 0, bs.Length);
                            ret= true;
                        }
                        catch
                        {
							ret = false;
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

                        }
                    }
                }
            }
            return ret;
        }
    }
}
