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
		protected HpdOrientation m_Orientation = HpdOrientation.Vertical;
		[Category("Hypowered_layout")]
		public HpdOrientation Orientation
		{
			get { return m_Orientation; }
			set
			{
				bool b = (m_Orientation != value);
				m_Orientation = value;
				if ((b)&&(MainForm != null)) MainForm.AutoLayout();
			}
		}

		public HpdPanel()
		{
			SetHpdType(HpdType.Panel);
			m_SizePolicyHorizon = SizePolicy.Expanding;
			m_SizePolicyVertual = SizePolicy.Expanding;
			this.Size = new Size(23*2, 23);
			SetBaseSize(0, 0);

			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}

		public HpdControl? AddControl(string Name, HpdType ht)
		{
			HpdControl? c = HpdForm.CreateControl(Name, ht);
			if (c != null)
			{
				c.NameChanged += (sender, e) =>
				{
					if (MainForm != null)
					{
						OnNameChanged(e);
					}
				};
				Controls.Add(c);
				if(MainForm!=null) MainForm.AutoLayout();
			}
			return c;
		}
		public void AddControl()
		{
			using (NewControlDialog dlg = new NewControlDialog())
			{
				if (MainForm != null)
				{
					dlg.HpdType = MainForm.DefHpdType;
					dlg.SetMainForm(MainForm);
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						AddControl(dlg.HpdName, dlg.HpdType);
						MainForm.DefHpdType = dlg.HpdType;
					}
				}
			}
		}
		public bool ControlMoveUp(HpdControl hc)
		{
			bool ret = false;
			int idx = Controls.GetChildIndex(hc);
			if (idx > 0)
			{
				Controls.SetChildIndex(hc, idx - 1);
				ret = true;
			}
			return ret;

		}
		public bool ControlMoveDown(HpdControl hc)
		{
			bool ret = false;

			int idx = Controls.GetChildIndex(hc);
			if ((idx >= 0) && (idx < Controls.Count))
			{
				Controls.SetChildIndex(hc, idx + 1);
				ret = true;
			}
			return ret;

		}
		public bool ControlRemove(HpdControl hc)
		{

			Controls.Remove(hc);
			return true;

		}
	}
}
