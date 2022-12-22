﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
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
	public enum ControlType
	{
		Button,
		Label,
		TextBox,
		ListBox,
		CheckBox,
		ComboBox,
		RadioButton,

		FileListBox,
		DriveComboBox,
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
		Line = 0xFF313131,
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

}
