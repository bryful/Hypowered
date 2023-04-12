using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.Windows.Input;
using System.Text.Json.Nodes;
using System.Windows;
namespace HypoweredLib
{
	public enum LibTarget
	{
		Def,
		Dir,
		Zip,
	}
	public class ItemsLib
	{
		// **********************************************************
		private System.Media.SoundPlayer soundPlayer = null;
		// **********************************************************
		private string m_TargetPath = "";
		private string m_LibDefExt = ".lib";
		private LibTarget m_LibTarget =  LibTarget.Def;
		private bool m_Enabled = false;
		public bool Enabled { get { return m_Enabled; } }
		// **************************************************************
		
		// **************************************************************
		public ItemsLib(string libName, LibTarget lt = LibTarget.Def)
		{
			m_TargetPath = "";
			m_Enabled = false;
			if (lt == LibTarget.Zip)
			{
				if (SetupZip(libName) == false)
				{
					return;
				}
				m_LibTarget = LibTarget.Zip;
			}
			else if (lt == LibTarget.Dir)
			{
				if (SetupDir(libName) == false)
				{
					return;
				}
				m_LibTarget = LibTarget.Dir;
			}
			else
			{
				if (SetupZip(libName) == true)
				{
					m_LibTarget = LibTarget.Zip;
				}
				else
				{
					if (SetupDir(libName) == true)
					{
						m_LibTarget = LibTarget.Dir;
					}
					else
					{
						return;
					}
				}
			}
			//GetResNames();
			//Beep();
			//MessageBox.Show(Application.ExecutablePath);
		}
		// **************************************************************
		private bool SetupDir(string path)
		{
			m_TargetPath = "";
			m_Enabled = false;
			string? p = Path.GetDirectoryName(path);
			string? n = Path.GetFileNameWithoutExtension(path);
			if(n==null) return	false;
			string? e = Path.GetExtension(path);
			string target = n;
			if (p == null)
			{
				p = Directory.GetCurrentDirectory();
			}
			target = Path.Combine(p, target);
			if(Directory.Exists(target) ==false)
			{
				try
				{
					Directory.CreateDirectory(target);
				}
				catch
				{
					return false;
				}
			}
			m_TargetPath = target;
			return true;
		}
		private bool SetupZip(string path)
		{
			m_TargetPath = "";
			m_Enabled = false;
			string? p = Path.GetDirectoryName(path);
			if (p == null) p = Directory.GetCurrentDirectory();
			string? n = Path.GetFileNameWithoutExtension(path);
			if(n==null) return false;
			string? e = Path.GetExtension(path);
			if (e == null) e = m_LibDefExt;
			string target = n;
			target = Path.Combine(p, target+e);
			if (File.Exists(target) == false)
			{
				return false;
			}
			m_TargetPath = target;
			return true;
		}
		// **************************************************************
		// **********************************************************
		// **********************************************************
		// **********************************************************
		public void PlaySound(string waveFile)
		{
			//再生されているときは止める
			if (soundPlayer != null)StopSound();

			//読み込む
			soundPlayer = new System.Media.SoundPlayer(waveFile);
			//非同期再生する
			soundPlayer.Play();
		}
		// **********************************************************
		public void Beep()
		{
			//PlaySound(Wave("se_saa08"));
		}
		// **********************************************************
		public void PlaySound(UnmanagedMemoryStream? wave)
		{
			//再生されているときは止める
			if (soundPlayer != null) StopSound();

			//読み込む
			soundPlayer = new System.Media.SoundPlayer(wave);
			//非同期再生する
			soundPlayer.Play();
		}
		// **********************************************************
		public void StopSound()
		{
			if (soundPlayer != null)
			{
				soundPlayer.Stop();
				soundPlayer.Dispose();
				soundPlayer = null;
			}
		}

		//Button1のClickイベントハンドラ
		private void Button1_Click(object sender, EventArgs e)
		{
			PlaySound("C:\\music.wav");
		}

		//Button2のClickイベントハンドラ
		private void Button2_Click(object sender, EventArgs e)
		{
			StopSound();
		}
	}
}