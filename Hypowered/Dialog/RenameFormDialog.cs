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
	public partial class RenameFormDialog : BaseForm
	{
		private HForm? HForm = null;
		public void SetHForm(HForm? mf)
		{
			HForm = mf;
		}
		public string CaptionOrg
		{
			get { return label1.Text; }
			set { label1.Text = value; }
		}
		public string CaptionNew
		{
			get { return label2.Text; }
			set { label2.Text = value; }
		}
		public string FormName
		{
			get { return tbNew.Text; }
			set
			{
				tbNew.Text = value;
				tbOrg.Text = value;
			}
		}
		public RenameFormDialog()
		{
			InitializeComponent();
			btnOK.Click += (sender, e) =>
			{
				if (tbNew.Text == tbOrg.Text) return;
				if (tbNew.Text == "") return;
				if (HForm != null)
				{
					string fn = HForm.ItemsLib.FileName;
					string? d = Path.GetDirectoryName(fn);
					if (d == null) d = "";
					string n = Path.GetFileNameWithoutExtension(fn);
					string ext = Path.GetExtension(fn);
					string nn = Path.Combine(d, tbNew.Text + ext);

					if ((File.Exists(nn)) || (Directory.Exists(nn)))
					{
						MessageBox.Show("A file with the same name already exists.");
						return;
					}
					if (HForm.RenameForm(tbNew.Text) == true)
					{
						this.DialogResult = DialogResult.OK;
					}

				}
			};
			tbNew.TextChanged += (sender, e) =>
			{
				btnOK.Enabled = ((tbNew.Text != "") && (tbNew.Text != tbOrg.Text));
			};
		}
	}
}
