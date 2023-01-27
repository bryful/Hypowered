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
				if ((b)&&(Root != null)) Root.AutoLayout();
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
			if(IsDrawFrame)
			{
				using (Pen p = new Pen(ForeColor))
				{
					Rectangle r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
					pe.Graphics.DrawRectangle(p, r);
				}
			}
		}

		public void AddControl(string Name, HpdType ht)
		{
			HpdControl? c = HpdForm.CreateControl(Name, ht);
			if (c != null)
			{
				Controls.Add(c);
				if(Root!=null) Root.AutoLayout();
			}
		}
		public void AddControl()
		{
			using (NewControlDialog dlg = new NewControlDialog())
			{
				if (Root != null) dlg.HpdType = Root.DefHpdType;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					AddControl(dlg.HpdName, dlg.HpdType);
					if (Root != null) Root.DefHpdType = dlg.HpdType;
				}
			}
		}
	}
}
