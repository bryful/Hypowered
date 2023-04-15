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
	public partial class PictItemDialog : BaseForm
	{
		public string PictName
		{
			get { return pictItemListTwin1.TargetPictName; }
			set { pictItemListTwin1.TargetPictName = value; }
		}
		public void SetMainForm(MainForm? mf)
		{
			pictItemListTwin1.SetMainForm(mf);
		}
		public void SetMainItemsLib(ItemsLib? mf)
		{
			pictItemListTwin1.SetMainItemsLib(mf);
		}
		public void SetFormItemsLib(ItemsLib? mf)
		{
			pictItemListTwin1.SetFormItemsLib(mf);
		}
		public PictItemDialog()
		{
			InitializeComponent();
			pictItemListTwin1.PictItemChanged += (sender, e) =>
			{
				PictItem? pi = e.Item;
				if (pi != null)
				{
					textBox1.Text = $"Name: \"{pi.Name}\" Size:{pi.OrgSize.ToString()}";
				}
				else
				{
					textBox1.Text = "";
				}
			};
			btnCancel.Click += (sender, e) => { this.DialogResult = DialogResult.Cancel; };
			btnOK.Click += (sender, e) => { this.DialogResult = DialogResult.OK; };
		}
	}
}
