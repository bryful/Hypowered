using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Hpd
{
	public class HU
	{
		static public HpdControl? AddControl(HpdForm mf,Control ctrl,string Name, HpdType ht)
		{
			HpdControl? c = mf.CreateControl(Name, ht);
			if (c != null)
			{
				c.NameChanged += (sender, e) =>
				{
					if (mf != null)
					{
						mf.OnNameChanged(e);
					}
				};
				ctrl.Controls.Add(c);
				mf.ListupControls();
				mf.AutoLayout();
			}
			return c;
		}
		static public HpdControl? AddControl(HpdForm mf, Control ctrl)
		{
			HpdControl? ret = null;
			using (NewControlDialog dlg = new NewControlDialog())
			{
				if (mf != null)
				{
					dlg.HpdType = mf.DefHpdType;
					dlg.SetMainForm(mf);
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						mf.DefHpdType = dlg.HpdType;
						ret =AddControl(mf,ctrl,dlg.HpdName, dlg.HpdType);
					}
				}
			}
			return ret;
		}
		static public bool ControlMoveUp(HpdControl hc)
		{
			bool ret = false;
			if ((hc.Parent != null) && (hc.Parent is Control))
			{
				Control ctrl = (Control)hc.Parent;
				int idx = ctrl.Controls.GetChildIndex(hc);
				if (idx > 0)
				{
					ctrl.Controls.SetChildIndex(hc, idx - 1);
					ret = true;
				}
			}
			return ret;

		}
		static public bool ControlMoveDown(HpdControl hc)
		{
			bool ret = false;

			if ((hc.Parent != null) && (hc.Parent is Control))
			{
				Control ctrl = (Control)hc.Parent;
				int idx = ctrl.Controls.GetChildIndex(hc);
				if ((idx >= 0) && (idx < ctrl.Controls.Count))
				{
					ctrl.Controls.SetChildIndex(hc, idx + 1);
					ret = true;
				}
			}
			return ret;

		}
		static public HpdControl? ControlRemove(HpdControl hc)
		{
			if ((hc.Parent != null)&&(hc.Parent is Control))
			{
				((Control)hc.Parent).Controls.Remove(hc);
				return hc;
			}
			else
			{
				return null;
			}

		}
		static public HpdControl? BufCtrl = null;
		static public HpdControl? CutCtrl(HpdForm mf)
		{
			HpdControl? ret = null;
			if (mf != null)
			{
				if (mf.Items.TargetControl != null)
				{
					BufCtrl = (HpdControl)mf.Items.TargetControl;
					ret = ControlRemove(BufCtrl);
					mf.AutoLayout();
				}
			}
			return ret;
		}
		static public HpdControl? PasteCtrl(HpdMainForm mf)
		{
			HpdControl? ret = null;
			if ((BufCtrl != null))
			{
				Control? c = mf;
				bool b = false;
				if (mf.Items.TargetControl !=null)
				{
					c = mf.Items.TargetControl.Parent;
					b= true;
				}

				if (c != null)
				{
					int idx = -1;
					if (b) idx = c.Controls.GetChildIndex(mf.Items.TargetControl);
					c.Controls.Add(BufCtrl);
					if (idx != -1)
					{
						c.Controls.SetChildIndex(BufCtrl, idx);
					}
					ret = BufCtrl;
					BufCtrl = null;
					mf.AutoLayout();
				}
			}
			return ret;
		}
	}
}
