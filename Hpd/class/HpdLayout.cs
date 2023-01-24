using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hpd
{
    public enum HpdAlgnment
    {
        Near,
        Center,
        Far,
        Fill
    }
    public enum HpdOrientation
    {
        Row,
        Column
    }
    public class HpdLayout
    {
        /// <summary>
        /// フォームだったらメニューとかのサイズを引いた値を返す
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        static public Rectangle ChkControlSize(Control cc)
        {
			Rectangle r = cc.ClientRectangle;
			if ((cc.Controls.Count<=0)||(cc is not Form))
            {
                return r;
            }
            else
            {
                int t = 0;
                int b = 0;
                foreach(Control c in cc.Controls)
                {

					if (c is StatusStrip)
					{
						b += c.Height + c.Margin.Top + c.Margin.Bottom;
					}
					else if ((c is MenuStrip)|| (c is ToolStrip))
					{
                        t += c.Height + c.Margin.Top+c.Margin.Bottom;
					}
				}
                return new Rectangle(r.Left, r.Top + t, r.Width, r.Height - t - b);
            }
        }

        /// <summary>
        /// ターゲットになるコントロールを返す。
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        static public List<HpdControl> GetLayoutControls(Control cc)
        {
            List<HpdControl> list = new List<HpdControl>();
            if(cc.Controls.Count>0)
            {
                foreach(Control c in cc.Controls)
                {
                    if(c is HpdControl ) 
                    {
						list.Add((HpdControl)c);
					}
                }
            }
            return list;
        }

        /// <summary>
        /// 横方向の自動レイアウト
        /// </summary>
        /// <param name="cc"></param>
		static public void ChkRow(Control cc)
        {
			List<HpdControl> list = GetLayoutControls(cc);
			if (list.Count <= 0) return;
			Rectangle rct = ChkControlSize(cc);

			bool IsHorResize = ((cc is HpdPanel) && (((HpdPanel)cc).Algnment != HpdAlgnment.Fill));
            bool IsVurResize = ((cc is HpdPanel) && (((HpdPanel)cc).LineAlgnment != HpdAlgnment.Fill));

            int defw = 0;
			int defh = 0;
			//横幅とFillの確認
			List<HpdControl> flist = new List<HpdControl>();
            //コントロールの横幅の合計 Fillは除く
            int w = 0;
            int ffw = 0;
            foreach (HpdControl c in list)
            {
				c.PopSizeDef();
                if (c.Algnment == HpdAlgnment.Fill)
                {
                    flist.Add(c);
					ffw += c.Width + c.Margin.Left + c.Margin.Right;
				}
                else
                {
                    w += c.Width + c.Margin.Left + c.Margin.Right;
                }
            }
            //親の横幅
            int ww = rct.Width - cc.Padding.Left - cc.Padding.Right;
            if (IsHorResize)
            {
                if(ww != w + ffw)
                {
                    int w2 = cc.Width + (w + ffw) - ww;
					((HpdPanel)cc).SetWidth(w2);
					rct = ChkControlSize(cc);
					ww = rct.Width - cc.Padding.Left - cc.Padding.Right;
                }
            }
            defw = rct.Width;
            if (ww >= w + ffw)
            {
                //Fillの大きさを設定
                if (flist.Count > 0)
                {
                    int fw = (ww - w) / flist.Count;
                    if (fw < 30) fw = 30;
                    foreach (HpdControl c in flist)
                    {
                        c.Width = fw -c.Margin.Left - c.Margin.Right;
                    }
                }
            }
            else
            {
                int cw = ww / list.Count;
                if (cw < 30) cw = 30;
                foreach (Control c in list)
                {
                    c.Width = cw -c.Margin.Left - c.Margin.Right;
                }
            }
            //縦の位置を設定
            //親の横幅
            if (IsVurResize)
            {
                int hm = 0;
                foreach (Control c in list)
                {
                    int hm2 = c.Height+c.Margin.Top+ c.Margin.Bottom;
					if (hm < hm2) hm = hm2;
				}
                hm += cc.Padding.Top + cc.Padding.Bottom;
                cc.Height= hm;
				rct = ChkControlSize(cc);
                defh = rct.Height;
			}
            if(cc is HpdPanel) ((HpdPanel)cc).SizeDef = new Size(defw, defh);

            int hh = rct.Height - cc.Padding.Top - cc.Padding.Bottom;
            int x = rct.Left + cc.Padding.Left;
            int y = 0;
            foreach (HpdControl c in list)
            {
                HpdAlgnment alg = c.LineAlgnment;
                int ch = (c.Height + c.Margin.Top + c.Margin.Bottom);

                switch (alg)
                {
                    case HpdAlgnment.Center:
                        y = hh / 2 - ch / 2 + cc.Padding.Top + c.Margin.Top;
                        break;
                    case HpdAlgnment.Near:
                        y = cc.Padding.Top + c.Margin.Top;
                        break;
                    case HpdAlgnment.Far:
                        y = hh - cc.Padding.Bottom - ch;
                        break;
                    case HpdAlgnment.Fill:
                        y = cc.Padding.Top + c.Margin.Top;
                        c.Height = hh;
                        break;
                }
                x += c.Margin.Left;
                c.Location = new Point(x, y+rct.Top);
                x += c.Width + c.Margin.Right;
            }
        }
		static public void ChkColumn(Control cc)
		{
			List<HpdControl> list = GetLayoutControls(cc);
			if (list.Count <= 0) return;

			bool IsHorResize = ((cc is HpdPanel) && (((HpdPanel)cc).Algnment != HpdAlgnment.Fill));
			bool IsVurResize = ((cc is HpdPanel) && (((HpdPanel)cc).LineAlgnment != HpdAlgnment.Fill));

			int defw = 0;
			int defh = 0;

			Rectangle rct = ChkControlSize(cc);
			//縦幅とFillの確認
			List<HpdControl> flist = new List<HpdControl>();
			//コントロールの縦幅の合計 Fillは除く
			int h = 0;
			int ffh = 0;
			foreach (HpdControl c in list)
			{
                c.PopSizeDef();
				if (c.LineAlgnment == HpdAlgnment.Fill)
				{
					flist.Add(c);
					ffh += c.Height + c.Margin.Top + c.Margin.Bottom;
				}
				else
				{
					h += c.Height + c.Margin.Top + c.Margin.Bottom;
				}
			}
			//親の縦幅
			int hh = rct.Height - cc.Padding.Top - cc.Padding.Bottom;
			if (IsVurResize)
			{
				if (hh != h + ffh)
				{
					int h2 = cc.Height + (h + ffh) - hh;
					((HpdPanel)cc).SetHeight(h2);
					rct = ChkControlSize(cc);
					hh = rct.Height - cc.Padding.Top - cc.Padding.Bottom;
				}
			}
			defh = rct.Height;
			if (hh >= h + ffh)
			{
				//Fillの大きさを設定
				if (flist.Count > 0)
				{
					int fh = (hh - h) / flist.Count;
					if (fh < 30) fh = 30;
					foreach (HpdControl c in flist)
					{
						c.Height = fh - c.Margin.Top - c.Margin.Bottom;
					}
				}
			}
			else
			{
				int ch = hh / list.Count;
				if (ch < 30) ch = 30;
				foreach (HpdControl c in list)
				{
					c.Height = ch - c.Margin.Top - c.Margin.Bottom;
				}
			}
			//縦の位置を設定
			//親の横幅
			if (IsHorResize)
			{
				int wm = 0;
				foreach (HpdControl c in list)
				{
					int wm2 = c.Width + c.Margin.Right + c.Margin.Left;
					if (wm < wm2) wm = wm2;
				}
				wm += cc.Padding.Left + cc.Padding.Right;
				cc.Width = wm;
				rct = ChkControlSize(cc);
				defw = rct.Width;
			}
			if (cc is HpdPanel) ((HpdPanel)cc).SizeDef = new Size(defw, defh);

			int ww = rct.Width - cc.Padding.Left - cc.Padding.Right;
			int y = rct.Top + cc.Padding.Top;
			int x = 0;
			foreach (HpdControl c in list)
			{
				HpdAlgnment alg = ((HpdControl)c).Algnment;
				int cw = (c.Width + c.Margin.Left + c.Margin.Right);

				switch (alg)
				{
					case HpdAlgnment.Center:
						x = ww / 2 - cw / 2 + cc.Padding.Left + c.Margin.Left;
						break;
					case HpdAlgnment.Near:
						x = cc.Padding.Left + c.Margin.Left;
						break;
					case HpdAlgnment.Far:
						x = ww - cc.Padding.Right - cw;
						break;
					case HpdAlgnment.Fill:
						x = cc.Padding.Left + c.Margin.Left;
						c.Width = ww;
						break;
				}
				y += c.Margin.Top;
				c.Location = new Point(x + rct.Left, y);
				y += c.Height + c.Margin.Bottom;
			}
		}
	}
}
