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
	public partial class EditLines : EditForm
	{
		private Size m_orgSize = new Size(0,0);
		[Category("_Hypowered")]
		public bool Multiline
		{ 
			get { return textBox1.Multiline;}
			set 
			{
				bool b = (textBox1.Multiline != value);
				textBox1.Multiline = value;
				CanResize = value;
				if (b)
				{
					if(value)
					{
						this.Size = m_orgSize;
						if (this.Size.Height<=100)
						{
							this.Size = new Size(this.Width,400);
							m_orgSize = this.Size;
						}
					}
					else
					{
						m_orgSize = this.Size;
						this.Size = new Size(this.Width, 100);
					}
				}
			}
		
		}
		public string ControlText
		{
			get { return textBox1.Text; }
			set { textBox1.Text = value; }
		}
		public string[] ControlLines
		{
			get { return textBox1.Lines; }
			set { textBox1.Lines = value; }
		}
		public EditLines()
		{
			InitializeComponent();
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			m_orgSize = this.Size;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult= DialogResult.Cancel;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			DialogResult= DialogResult.OK;
		}
		public override void OnCloseButtunClick(EventArgs e)
		{
			base.OnCloseButtunClick(e);
			DialogResult = DialogResult.Cancel;
		}
	}
}
