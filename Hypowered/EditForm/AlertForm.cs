using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class AlertForm : EditForm
	{
		public AlertForm()
		{
			this.StartPosition = FormStartPosition.CenterParent;
			InitializeComponent();
			this.Text = "alert";
		}
		public override void OnButtunClick(EventArgs e)
		{
			base.OnButtunClick(e);
			DialogResult= DialogResult.Cancel;
		}
		public void ChkSize()
		{
			string[] sa = textBox1.Text.Split("\r\n");
			if(sa.Length>0 )
			{
				Bitmap bmp = new Bitmap(1000,50);
				Graphics g = Graphics.FromImage(bmp);
				StringFormat sf = new StringFormat();
				int max = 0;
				int h = 100;
				if (sa.Length > 0)
				{
					foreach (var s in sa)
					{
						SizeF sz = g.MeasureString(s, textBox1.Font, 2000, sf);
						if ((int)sz.Width > max) { max = (int)sz.Width; }
						if (h > (int)sz.Height) h = (int)sz.Height;
					}
				}
				max += 200;
				if (this.Width < max) this.Width = max;
				int hh = this.Height - textBox1.Height;
				h *= sa.Length;
				if(h > textBox1.Height ) { this.Height = h+hh;}
			}
		}
		public Object? SelectedObject
		{
			get { return (object)textBox1.Text; }
			set
			{
				string ret = "";
				if(value == null) 
				{
					ret = "null";
				}else
				{
					ret = value.ToString();
				}
				textBox1.Text = ret;
			}
		}
	}
}
