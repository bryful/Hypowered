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
		protected HpdOrientation m_Orientation = HpdOrientation.Row;
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
			this.Size = new Size(150, 60);
			m_SizeDef = this.Size;
			Algnment = HpdAlgnment.Fill;
			LineAlgnment = HpdAlgnment.Fill;
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
			if((IsDrawFrame)&&(IsEdit==false))
			{
				using (Pen p = new Pen(ForeColor))
				{
					Rectangle r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
					pe.Graphics.DrawRectangle(p, r);
				}
			}
		}
		public void AddControl(string Name, string tx, HpdType ht)
		{
			HpdControl? c = HpdControl.CreateControl(Name, tx, ht);
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
					AddControl(dlg.HpdName, dlg.HpdText, dlg.HpdType);
					if (Root != null) Root.DefHpdType = dlg.HpdType;
				}
			}
		}
	}
}
