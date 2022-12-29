using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class HyperPictLibDialog : Form
	{
		private HyperMainForm? m_form = null;
		public HyperPictLibDialog()
		{
			this.StartPosition = FormStartPosition.Manual;
			InitializeComponent();
		}
		public void SetMainForm(HyperMainForm? mf)
		{
			m_form = mf;
			pictLibBox1.SetMainForm(mf);
		}
	}
}
