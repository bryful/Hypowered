using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
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
        Horizontal,
        Vertical
    }
	public enum SizePolicy
	{
		Fixed,
		Expanding
	}

	public class HpdLayout
    {
		static private Size PreferredSize(HpdControl c)
		{
			if(c is HpdButton)
			{
				HpdButton hb = (HpdButton)c;
				return hb.PreferredSize;
			}else if (c is HpdPanel)
			{
				return ((HpdPanel)c).PreferredSize;
			}
			else if (c is HpdComboBox)
			{
				return ((HpdComboBox)c).PreferredSize;

			}
			else if (c is HpdTextBox)
			{
				return ((HpdTextBox)c).PreferredSize;

			}
			else
			{
				return c.PreferredSize;
			}

		}
		/// <summary>
		/// フォームだったらメニューとかのサイズを引いた値を返す
		/// </summary>
		/// <param name="cc"></param>
		/// <returns></returns>
		static public Rectangle GetControlSize(Control cc)
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
        static private List<HpdControl> GetHpdControls(Control cc)
        {
            List<HpdControl> list = new List<HpdControl>();
            if(cc.Controls.Count>0)
            {
                foreach(Control c in cc.Controls)
                {
                    if((c is HpdControl ) &&(c.Visible==true))
                    {
						list.Add((HpdControl)c);
					}
                }
            }
            return list;
        }
		static private List<HpdPanel> GetHpdPanel(List<HpdControl> cc)
		{
			List<HpdPanel> list = new List<HpdPanel>();
			if (cc.Count > 0)
			{
				foreach (HpdControl c in cc)
				{
					if ((c is HpdPanel) && (c.Visible == true))
					{
						list.Add((HpdPanel)c);
					}
				}
			}
			return list;
		}
		static private List<HpdStretch> GetHpdStretch(List<HpdControl> cc)
		{
			List<HpdStretch> list = new List<HpdStretch>();
			if (cc.Count > 0)
			{
				foreach (HpdControl c in cc)
				{
					if ((c is HpdStretch) && (c.Visible == true))
					{
						list.Add((HpdStretch)c);
					}
				}
			}
			return list;
		}
		static private List<HpdControl> GetHorExpondControls(List<HpdControl> cc)
		{
			List<HpdControl> list = new List<HpdControl>();
			if (cc.Count > 0)
			{
				foreach (HpdControl c in cc)
				{
					if ((c is HpdStretch) || (c.Visible == false)) continue;
					if (c.SizePolicyHorizon == SizePolicy.Expanding)
					{
						list.Add(c);
					}
				}
			}
			return list;
		}
		static private List<HpdControl> GetVurExpondControls(List<HpdControl> cc)
		{
			List<HpdControl> list = new List<HpdControl>();
			if (cc.Count > 0)
			{
				foreach (HpdControl c in cc)
				{
					if ((c is HpdStretch) || (c.Visible == false)) continue;
					if (c.SizePolicyVertual == SizePolicy.Expanding)
					{
						list.Add(c);
					}
				}
			}
			return list;
		}
		static private List<HpdControl> GetHorFixedControls(List<HpdControl> cc)
		{
			List<HpdControl> list = new List<HpdControl>();
			if (cc.Count > 0)
			{
				foreach (HpdControl c in cc)
				{
					if ((c is HpdStretch) || (c.Visible == false)) continue;
					if (c.SizePolicyHorizon == SizePolicy.Fixed)
					{
						list.Add(c);
					}
				}
			}
			return list;
		}
		static private List<HpdControl> GetVurFixedControls(List<HpdControl> cc)
		{
			List<HpdControl> list = new List<HpdControl>();
			if (cc.Count > 0)
			{
				foreach (HpdControl c in cc)
				{
					if ((c is HpdStretch) || (c.Visible == false)) continue;
					if (c.SizePolicyVertual == SizePolicy.Fixed)
					{
						list.Add(c);
					}
				}
			}
			return list;
		}
		static private int GetAllHeight(List<HpdControl> cc)
		{
			int ret = 0;
			if (cc.Count > 0)
			{
				foreach (HpdControl c in cc)
				{
					if (c.Visible == false) continue;
					if (c.Height > 0)
					{
						ret += c.Height + c.Margin.Top + c.Margin.Bottom;
					}
				}
			}
			return ret;
		}
		static private int GetAllWidth(List<HpdControl> cc)
		{
			int ret = 0;
			if (cc.Count > 0)
			{
				foreach (HpdControl c in cc)
				{
					if (c.Width > 0)
					{
						ret += c.Width + c.Margin.Left + c.Margin.Right;
					}
				}
			}
			return ret;
		}
		static public void ChkLayout(Control cc)
		{
			cc.SuspendLayout();
			HpdOrientation ori = HpdOrientation.Horizontal;
			if (cc is HpdForm) ori = ((HpdForm)cc).Orientation;
			else if (cc is HpdPanel) ori = ((HpdPanel)cc).Orientation;
			if(ori == HpdOrientation.Horizontal)
			{
				ChkHorizon(cc);
			}else if (ori == HpdOrientation.Vertical)
			{
				ChkVirtual(cc);
			}
			cc.ResumeLayout(false);
		}
		static public void ChkPanelLayout(Control cc)
		{
			cc.SuspendLayout();
			HpdOrientation ori = HpdOrientation.Horizontal;
			if (cc is HpdForm) ori = ((HpdForm)cc).Orientation;
			else if (cc is HpdPanel) ori = ((HpdPanel)cc).Orientation;
			if (ori == HpdOrientation.Horizontal)
			{
				ChkPanelHorizon(cc);
			}
			else if (ori == HpdOrientation.Vertical)
			{
				ChkPanelVirtual(cc);
			}
			cc.ResumeLayout(false);
		}
		static public void ChkPanelVirtual(Control cc)
		{
			if ((cc is not HpdForm) && (cc is not HpdPanel)) return;
			List<HpdControl> list = GetHpdControls(cc);
			if(list.Count==0)
			{
				if (cc is HpdPanel)
				{
					((HpdPanel)cc).SetBaseSize(0,0);
				}
				return;
			}
			int w = 0;
			int h = 0;
			foreach(HpdControl c in list)
			{
				if (c is HpdPanel) ChkPanelLayout(c);
				int w2 = c.BaseSize.Width + c.Margin.Right +c.Margin.Left;
				if (w < w2) w = w2;
				h += c.BaseSize.Height +c.Margin.Top + c.Margin.Bottom;
			}
			w += cc.Padding.Left + cc.Padding.Right;
			h += cc.Padding.Top + cc.Padding.Bottom;

			if (cc is HpdPanel)
			{
				((HpdPanel)cc).BaseSize= new Size(w, h);
				if(cc.Height<h)
				{
					cc.Height = h;
				}
				if (cc.Width != w)
				{
					cc.Width = w;
				}
				((HpdPanel)cc).SetMinimumSize(new Size(w, h));
			}
			else if (cc is HpdForm)
			{
				Rectangle r = GetControlSize(cc);
				Rectangle cr = cc.ClientRectangle;
				int dw = cr.Width - r.Width;
				int dh = cr.Height - r.Height;
				if (r.Height<h)
				{
					int ah = h - r.Height;
					cc.Height = cc.Height + ah;
				}
				if(r.Width<w)
				{
					int aw = w - r.Width;
					cc.Width = cc.Width + aw;
				}
				((HpdForm)cc).SetMinimumSize( new Size(w + dw, h + dh));
			}
		}
		static public void ChkPanelHorizon(Control cc)
		{
			if ((cc is not HpdForm) && (cc is not HpdPanel)) return;
			List<HpdControl> list = GetHpdControls(cc);
			if (list.Count == 0)
			{
				if (cc is HpdPanel)
				{
					((HpdPanel)cc).SetBaseSize(0, 0);
				}
				return;
			}
			int w = 0;
			int h = 0;
			foreach (HpdControl c in list)
			{
				if (c is HpdPanel) ChkPanelLayout(c);
				int h2 = c.BaseSize.Height + c.Margin.Top + c.Margin.Bottom;
				if (h < h2) h = h2;
				w += c.BaseSize.Width + c.Margin.Left + c.Margin.Right;
			}
			w += cc.Padding.Left + cc.Padding.Right;
			h += cc.Padding.Top + cc.Padding.Bottom;
			if (cc is HpdPanel)
			{
				((HpdPanel)cc).BaseSize = new Size(w, h);
				if (cc.Height != h)
				{
					cc.Height = h;
				}
				if (cc.Width != w)
				{
					cc.Width = w;
				}
				((HpdPanel)cc).SetMinimumSize(new Size(w,h));
			}
			else if (cc is HpdForm)
			{
				Rectangle r = GetControlSize(cc);
				Rectangle cr = cc.ClientRectangle;
				int dw = cr.Width - r.Width;
				int dh = cr.Height - r.Height;
				if (r.Height < h)
				{
					int ah = h - r.Height;
					cc.Height = cc.Height + ah;
				}
				if (r.Width < w)
				{
					int aw = w - r.Width;
					cc.Width = cc.Width + aw;
				}
				((HpdForm)cc).SetMinimumSize(new Size(w + dw, h + dh));
			}
		}
		/// <summary>
		/// 横方向の自動レイアウト
		/// </summary>
		/// <param name="cc"></param>
		static public void ChkVirtual(Control cc)
        {
			List<HpdControl> list = GetHpdControls(cc);
			if (list.Count <= 0)
			{
				if (cc is HpdPanel)
				{
					if (((HpdPanel)cc).SizePolicyVertual == SizePolicy.Expanding)
					{
						cc.Height = 0;
					}
					else
					{
						cc.Height = ((HpdPanel)cc).BaseSize.Height;
					}
				}
				return;
			}
			List<HpdPanel> plist = GetHpdPanel(list);
			if(plist.Count >0) foreach(HpdPanel p in plist) { ChkLayout(p); }
			//横幅をそろえる
			Rectangle rct = GetControlSize(cc);
			int w = rct.Width - cc.Padding.Left - cc.Padding.Right;
			foreach(HpdControl c in list)
			{
				int h = 23;
				if(c.SizePolicyVertual == SizePolicy.Fixed)
				{
					h = c.BaseSize.Height;
				}
				c.SetSize(w - c.Margin.Left - c.Margin.Right,h);
			}
			List<HpdStretch> slist = GetHpdStretch(list);
			List<HpdControl> elist = GetVurExpondControls(list);
			List<HpdControl> flist = GetVurFixedControls(list);
			int inter = 0;
			int intermod = 0;
			int stretchHeight = 0;
			if (slist.Count>0)
			{
				int h = GetAllHeight(elist) + GetAllHeight(flist);
				stretchHeight = (rct.Height - cc.Padding.Top - cc.Padding.Bottom - h);
				int sh = stretchHeight / slist.Count;
				int shmod = stretchHeight % slist.Count;
				foreach (HpdStretch c in slist) 
				{
					int h1 = sh - c.Margin.Top - c.Margin.Bottom;
					if (shmod > 0)
					{
						h1 += 1;
						shmod--;
					}
					if (h1 <= 0) h1 = 0;
					c.Height = h1;
				}
			}
			if (elist.Count>0)
			{
				int h = GetAllHeight(flist);
				int hmax = (rct.Height - cc.Padding.Top - cc.Padding.Bottom - h - stretchHeight);
				int sh  =  hmax / elist.Count;
				int shmod = hmax % elist.Count;
				foreach (HpdControl c in elist)
				{
					int h1 = sh - c.Margin.Top - c.Margin.Bottom;
					if(shmod>0)
					{
						h1 += 1;
						shmod--;
					}
					if (h1 < 0) h1 = 0;
					c.Height = h1;
				}
			}
			else if(flist.Count>0)
			{
				int h = GetAllHeight(flist);
				int hmax = (rct.Height - cc.Padding.Top - cc.Padding.Bottom - h - stretchHeight);
				inter =  hmax / (flist.Count+1);
				intermod = hmax % (flist.Count + 1);
				if (inter < 0)
				{
					inter = 0;
				}
			}
			//並び替え
			int x= rct.Left+ cc.Padding.Left;
			int y= rct.Top+ cc.Padding.Top;
			Debug.WriteLine($"inter:{inter}");
			foreach(HpdControl c in list)
			{
				y += inter;
				if(intermod>0)
				{
					y += 1;
					intermod--;
				}
				y += c.Margin.Top;
				c.Location = new Point(x + c.Margin.Left, y);
				y += c.Margin.Bottom + c.Height;
			}
			if(cc is HpdPanel)
			{
				cc.Height = cc.PreferredSize.Height;
				((HpdPanel)cc).SetBaseSize(null,cc.PreferredSize.Height);
			}

		}
		static public void ChkHorizon(Control cc)
		{
			List<HpdControl> list = GetHpdControls(cc);
			if (list.Count <= 0)
			{
				if (cc is HpdPanel)
				{
					if (((HpdPanel)cc).SizePolicyHorizon == SizePolicy.Expanding)
					{
						cc.Width = 0;
					}
					else
					{
						cc.Width = ((HpdPanel)cc).BaseSize.Width;
					}
				}
				return;
			}
			List<HpdPanel> plist = GetHpdPanel(list);
			if (plist.Count > 0) foreach (HpdPanel p in plist) { ChkLayout(p); }
			//縦幅をそろえる
			Rectangle rct = GetControlSize(cc);
			int hh = rct.Height - cc.Padding.Top - cc.Padding.Bottom;
			foreach (HpdControl c in list)
			{
				int cw = c.BaseSize.Width;
				int ch = c.BaseSize.Height;
				if (c.SizePolicyVertual == SizePolicy.Expanding)
				{
					ch = hh -c.Margin.Top -c.Margin.Bottom;
				}
				c.SetSize(cw, ch);
			}
			List<HpdStretch> slist = GetHpdStretch(list);
			List<HpdControl> elist = GetVurExpondControls(list);
			List<HpdControl> flist = GetVurFixedControls(list);
			int inter = 0;
			int intermod = 0;
			int stretchWidth = 0;

			if (slist.Count > 0)
			{
				int w = GetAllWidth(elist) + GetAllWidth(flist);
				stretchWidth = (rct.Width - cc.Padding.Left - cc.Padding.Right - w);
				int sw = stretchWidth / slist.Count;
				int swmod = stretchWidth % slist.Count;
				foreach (HpdStretch c in slist)
				{
					int w1 = sw - c.Margin.Left - c.Margin.Right;
					if(swmod>0)
					{
						w1++;
						swmod--;
					}
					if (w1 <= 0) w1 = 0;
					c.Width = w1;
				}
			}
			if (elist.Count > 0)
			{
				int w = GetAllWidth(flist);
				int swMax = (rct.Width - cc.Padding.Left - cc.Padding.Right - w - stretchWidth);
				int sw = swMax / elist.Count;
				int swmod = swMax % elist.Count;
				foreach (HpdControl c in elist)
				{
					int w1 = sw - c.Margin.Left - c.Margin.Right;
					if(swmod>0)
					{
						w1++;
						swmod--;
					}
					if (w1 < 0) w1 = 0;
					c.Width = w1;
				}
			}
			else if (flist.Count > 0)
			{
				int w = GetAllWidth(flist);
				int wMax = (rct.Width - cc.Padding.Left - cc.Padding.Right - w - stretchWidth);
				inter = wMax / (flist.Count + 1);
				intermod = wMax % (flist.Count + 1);
				if (inter < 0) inter = 0;
			}
			//並び替え
			int x = rct.Left + cc.Padding.Left;
			int y = rct.Top + cc.Padding.Top;
			Debug.WriteLine($"inter:{inter}");
			foreach (HpdControl c in list)
			{
				x += inter;
				if(intermod>0)
				{
					x++;
					intermod--;
				}
				y = rct.Height / 2 - (c.Height + c.Margin.Top + c.Margin.Bottom) / 2;
				y = y + rct.Top + cc.Padding.Top + c.Margin.Top;
				x += c.Margin.Left;
				c.Location = new Point(x, y);
				x += c.Margin.Right + c.Width;
			}
			if (cc is HpdPanel)
			{
				cc.Width = cc.PreferredSize.Width;
				((HpdPanel)cc).SetBaseSize( cc.PreferredSize.Width,null);
			}
		}
	}
}
