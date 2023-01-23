using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hpd
{
	public partial class HpdPanel : HpdControl
	{
		public HpdPanel()
		{
			SetHpdType(HpdType.Panel);
			this.Size = new Size(100, 27);
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
		public void AddControl(string Name, string tx, HpdType ht)
		{
			HpdControl? c = HpdA.CreateControl(Name, tx, ht);
			if (c != null)
			{
				Controls.Add(c);
			}
		}
		public void AddControl()
		{
			using (NewControlDialog dlg = new NewControlDialog())
			{
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					AddControl(dlg.HpdName, dlg.HpdText, dlg.HpdType);
				}
			}
		}
	}
}
