using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public enum ControlType
	{
		Button = 0,
		Label,
		TextBox,
		CheckBox,
		RadioButton,
		ListBox,
		DropdownList,

		DriveIcons,
		DirList,
		FileList,
	}

	[Flags]
	public enum InScript
	{
		None = 0,
		Startup
			= 0b_0000_0000_0001,
		MouseClick
			= 0b_0000_0000_0010,
		MouseDoubleClick
			= 0b_0000_0000_0100,
		MouseUp
			= 0b_0000_0000_1000,
		SelectedIndexChanged
			= 0b_0000_0001_0000,
		ValueChanged
			= 0b_0000_0010_0000,
		Reserve0
			= 0b_0000_0100_0000,
		Reserve1
			= 0b_0000_1000_0000,
		Reserve2
			= 0b_0001_0000_0000,
		Reserve3
			= 0b_0010_0000_0000,
		Reserve4
			= 0b_0100_0000_0000,
		Shutdown
			= 0b_1000_0000_0000,
		Button
			= MouseClick,
		DirList
			= SelectedIndexChanged | MouseDoubleClick,
		FileList
			= SelectedIndexChanged | MouseDoubleClick,
		ListBox
			= SelectedIndexChanged | MouseDoubleClick,
	}
	/// <summary>
	/// フォームやコントロールのクリックされた場所
	/// </summary>
	public enum MDPos
	{
		None = -1,
		TopLeft = 0,
		Top,
		TopRight,
		Left,
		Center,
		Right,
		BottomLeft,
		Bottom,
		BottomRight,
	}

	
	/// <summary>
	/// コントロールを扱うユーティリティ関数
	/// </summary>
	public class CU
	{
		/// <summary>
		/// フォームのクリックされたエリアを返す。
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="sz"></param>
		/// <returns></returns>
		static public MDPos GetMDPos(int x, int y, Size sz)
		{
			MDPos ret = MDPos.None;
			if ((x < 0) || (y < 0) || (x >= sz.Width) || (y >= sz.Height)) return ret;
			int ww = 20;
			if (ww > sz.Width / 6) ww = sz.Width / 6;
			if (ww < 2) ww = 2;
			int hh = 20;
			if (hh > sz.Height / 6) hh = sz.Height / 6;
			if (hh < 2) hh = 2;

			int Xadr = 0;
			if (x < ww) Xadr = 0;
			else if (x > sz.Width - ww) Xadr = 2;
			else Xadr = 1;

			int Yadr = 0;
			if (y < ww) Yadr = 0;
			else if (y > sz.Height - ww) Yadr = 2;
			else Yadr = 1;

			return (MDPos)(Yadr * 3 + Xadr);

		}
	}
	/// <summary>
	/// 色のデフォルト値 uint
	/// </summary>
	public enum HyperColor : uint
	{
		Back = 0xFF232323,
		Dark = 0xFF1d1d1d,
		Line = 0xFF515151,
		LineRed = 0xFFFF3131,
		Fore = 0xFFb9b9b9,
		Forcus = 0xFF2c85de,
		MenuFourcus = 0xFF404040,
	}
	public class ColU
	{
		static public Color ToColor(HyperColor hv)
		{
			uint v = (uint)hv;
			int r, g, b, a;
			b = (int)(v & 0xFF);
			g = (int)((v >> 8) & 0xFF);
			r = (int)((v >> 16) & 0xFF);
			a = (int)((v >> 24) & 0xFF);
			return Color.FromArgb(a, r, g, b);
		}
	}
	public class FntU
	{
		/// <summary>
		/// フォントダイアログを簡単に開く
		/// </summary>
		/// <param name="me">親コントロール</param>
		/// <param name="fnt">デフォルトフォント</param>
		/// <returns></returns>
		static public Font? Dialog(Control me,Font? fnt = null)
		{
			using (FontDialog dlg = new FontDialog())
			{
				if(fnt==null)
				{
					dlg.Font = me.Font;
				}
				else
				{
					dlg.Font = fnt;
				}
				if (dlg.ShowDialog(me)==DialogResult.OK)
				{
					return dlg.Font;
				}
				else
				{
					return null;
				}
			}
		}
	}
	public class ControlDef
	{
		static public Size DefSize = new Size(120, 25);
	}
	// *************************************************************************
	public class CheckedChangedEventArgs : EventArgs
	{
		public bool Checked;
		public CheckedChangedEventArgs(bool v)
		{
			Checked = v;
		}
	}
	public class SelectedIndexChangedEventArgs : EventArgs
	{
		public int SelectedIndex;
		public string Value;
		public SelectedIndexChangedEventArgs(int v, string s)
		{
			SelectedIndex = v;
			Value = s;
		}
	}
	public class RButtonChangedEventArgs : EventArgs
	{
		public int Index;
		public RButtonChangedEventArgs(int v)
		{
			Index = v;
		}
	}
	public class CurrentDirChangedEventArgs : EventArgs
	{
		public string Path;
		public CurrentDirChangedEventArgs(string v)
		{
			Path = v;
		}
	}
}
