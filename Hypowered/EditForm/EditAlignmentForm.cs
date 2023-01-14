using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class EditAlignmentForm : EditForm
	{
		private HyperMainForm? m_MainForm = null;
		public HyperMainForm? MainForm
		{
			get { return m_MainForm; }
			set
			{
				m_MainForm = value;
			}
		}
		private AlignmentBtn[] m_btns = new AlignmentBtn[Enum.GetNames<AStyle>().Length];
		public EditAlignmentForm()
		{
			this.MinimumSize = new Size(25 * 6, 75);
			this.MaximumSize = new Size(25 * 6, 75);
			this.Size = new Size(25 * 6, 75);
			int y = 25;
			for (int i = 0;i< m_btns.Length; i++)
			{
				if (i < 6) y = 25; else y = 50;
				m_btns[i] = new AlignmentBtn();
				m_btns[i].AStyle = (AStyle)i;
				m_btns[i].Location = new Point((i%6) * 25,y);
				m_btns[i].Click += (sender, e) =>
				{
					AlignmentBtn? ab = (AlignmentBtn?)sender;
					if (ab == null) return;

					if(m_MainForm!= null)
					{
						if(m_MainForm.targetForm!= null)
						{
							m_MainForm.targetForm.AlignmentControl(ab.AStyle);
						}
					}
				};
				this.Controls.Add(m_btns[i]);
			}
			CanResize= false;
			InitializeComponent();
		}
		public override void OnButtunClick(EventArgs e)
		{
			this.Visible = false;
			base.OnButtunClick(e);
		}
		public void SetMainForm(HyperMainForm? fm)
		{
			m_MainForm = fm;
			if (m_MainForm != null)
			{
				m_MainForm.FormChanged += (sender, e) =>
				{
				};
				m_MainForm.ControlChanged += (sender, e) =>
				{
				};
				this.LocationChanged += (sender, e) =>
				{
					m_MainForm.AlignmentFormBounds = this.Bounds;
				};
			}
		}
	}
}
