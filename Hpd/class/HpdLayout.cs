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
        Full
    }
    public enum HpdOrientation
    {
        Row,
        Column
    }
    public class HpdLayout
    {
        static public void ChkRow(Control cc)
        {
            if (cc.Controls.Count <= 0) return;
            cc.SuspendLayout();
            //まずコントロールの横幅合計を求める
            int wall = 0;
            int w = 0;
            List<Control> list = new List<Control>();
            int cnt = 0;

            foreach (Control c in cc.Controls)
            {
                bool isFull = false;
                if (c is HpdControl)
                {
                    isFull = ((HpdControl)c).Algnment == HpdAlgnment.Full;
                    list.Add(c);
                }
                if (isFull)
                {
                    w += c.Margin.Left + c.Margin.Right;
                }
                else
                {
                    wall += c.Width + c.Margin.Left + c.Margin.Right;
                }

                cnt++;
            }
            if (list.Count > 0)
            {
                int aw = (cc.Width - w) / list.Count;
                if (aw > 20) aw = 20;
                foreach (Control c in list)
                {
                    c.Width = aw;
                }
            }
            int x = 0;
            int y = 0;
            foreach (Control c in cc.Controls)
            {
                x += c.Margin.Left;
                HpdAlgnment a = HpdAlgnment.Center;
                if (c is HpdControl)
                {
                    a = ((HpdControl)c).LineAlgnment;
                }
                switch (a)
                {
                    case HpdAlgnment.Full:
                        y = c.Padding.Top;
                        c.Size = new Size(c.Width, cc.Height - c.Padding.Top - c.Padding.Bottom); ;
                        break;
                    case HpdAlgnment.Near:
                        y = c.Padding.Top;
                        break;
                    case HpdAlgnment.Far:
                        y = cc.Height - c.Height - c.Padding.Bottom;
                        break;
                    case HpdAlgnment.Center:
                        y = (cc.Height - c.Height) / 2;
                        break;
                }
                c.Location = new Point(x, y);
            }
            cc.ResumeLayout();
        }
    }
}
