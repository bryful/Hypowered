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
	public partial class TextEditForm : Form
	{
		[Category("Hypowered")]
		public bool Muitiline
		{
			get { return textBox1.Multiline; }
			set 
			{
				if(value==false)
				{
					this.Size = new Size(410, 130);
					this.MinimumSize = new Size(410, 130);
					this.MaximumSize = new Size(410, 130);
					textBox1.ScrollBars= ScrollBars.None;
					textBox1.Location = new Point(25, 25);
					textBox1.Size = new Size(350, 23);
					btnCancel.Location = new Point(208, 56);
					btnOK.Location = new Point(289, 56);

				}
				else
				{
					this.MinimumSize = new Size(410, 130);
					this.MaximumSize = new Size(0, 0);
					textBox1.ScrollBars = ScrollBars.Both;

				}
				textBox1.Multiline = value;
			}

		}
		[Category("Hypowered")]
		public Color BackColorAll
		{
			get { return BackColor; }
			set { SetBackColor(this, value); }
		}
		[Category("Hypowered")]
		public Color ForeColorAll
		{
			get { return ForeColor; }
			set { SetForeColor(this, value); }
		}
		[Category("Hypowered")]
		public new string Text
		{
			get { return textBox1.Text; }
			set { textBox1.Text = value; }
		}
		[Category("Hypowered")]
		public string[] Lines
		{
			get { return textBox1.Text.Split("\r\n"); }
			set { textBox1.Text = string.Join("\r\n", value); }
		}
		[Category("Hypowered")]
		public string Title
		{
			get { return base.Text; }
			set { base.Text = value; }
		}
		public FlatStyle FlatStyle
		{
			get { return btnCancel.FlatStyle; }
			set
			{
				btnCancel.FlatStyle = value;
				btnOK.FlatStyle = value;
			}
		}
		public TextEditForm()
		{
			InitializeComponent();
		}
		public void SetForeColor(Control c,Color col)
		{
			c.ForeColor = col;
			if(c.Controls.Count>0)
			{
				foreach( Control cc in c.Controls )
				{
					SetForeColor(cc, col);
				}
			}
		}
		public void SetBackColor(Control c, Color col)
		{
			c.BackColor = col;
			if (c.Controls.Count > 0)
			{
				foreach (Control cc in c.Controls)
				{
					SetForeColor(cc, col);
				}
			}
		}
	}
}
