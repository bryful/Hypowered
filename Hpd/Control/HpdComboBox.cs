﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hpd
{
	public partial class HpdComboBox : HpdControl
	{
		public HpdComboBox()
		{
			SetHpdType(HpdType.ComboBox);

			InitializeComponent();
			ChkSize();
		}

		private void M_comb_DrawItem(object? sender, DrawItemEventArgs e)
		{
			e.DrawBackground();

			ComboBox? cmb = (ComboBox?)sender;
			if (cmb != null)
			{
				using (SolidBrush sb = new SolidBrush(cmb.ForeColor))
				{
					string txt = cmb.Text;
					if((e.Index >=0)&&(e.Index<cmb.Items.Count))
					{
						if (cmb.Items[e.Index] is CombItem)
						{
							txt = ((CombItem)cmb.Items[e.Index]).Name;
						}
						else
						{
							txt = cmb.Items[e.Index].ToString();
						}
					}
					float ym =
						(e.Bounds.Height - e.Graphics.MeasureString(txt, cmb.Font).Height) / 2;
					e.Graphics.DrawString(txt, cmb.Font, sb, e.Bounds.X, e.Bounds.Y + ym);
				}
				//フォーカスを示す四角形を描画
				e.DrawFocusRectangle();
			}
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
			if (m_CaptionWidth > 0)
			{
				using (SolidBrush sb = new SolidBrush(ForeColor))
				{
					Rectangle r = new Rectangle(0, 0, m_CaptionWidth, this.Height);
					pe.Graphics.DrawString(m_Caption, this.Font, sb, r, m_format);
				}
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
		}
		
	}

	public class CombItem
	{
		public string Name { get; set; } = "";
		public Object? obj { get; set; } = null;
		public CombItem()
		{

		}
		public CombItem(string n,object? o)
		{
			Name = n;
			obj = o;
		}
		public new string ToString()
		{
			return Name;
		}
	}
}
