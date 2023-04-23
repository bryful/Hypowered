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
	public partial class AddControlDialog : BaseForm
	{
		private HForm? m_HForm = null;
		public HForm? HForm
		{
			get { return m_HForm; }
			set
			{
				m_HForm = value;
				if (m_HForm != null)
				{
					textBox1.Text = m_HForm.ControlNewName(hTypeCombo1.HType);
				}
			}
		}
		public HType HType
		{
			get { return hTypeCombo1.HType; }
			set { hTypeCombo1.HType = value; }
		}
		public string CName
		{
			get { return textBox1.Text; }
			set { textBox1.Text = value; }
		}
		public string CText
		{
			get { return textBox2.Text; }
			set { textBox2.Text = value; }
		}
		public string Caption
		{
			get { return this.Text; }
			set { this.Text = value; }
		}
		public AddControlDialog()
		{
			this.StartPosition = FormStartPosition.CenterParent;
			InitializeComponent();
			hTypeCombo1.SelectedIndexChanged += (sender, e) =>
			{
				if (m_HForm != null)
				{
					textBox1.Text = m_HForm.ControlNewName(hTypeCombo1.HType);
				}
			};

			textBox1.TextChanged += (sender, e) =>
			{
				btnOK.Enabled = (textBox1.Text != "");
			};
			btnOK.Click += (sender, e) =>
			{
				this.DialogResult = DialogResult.OK;
			};
		}
	}
}
