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
		public void SetMainForm(MainForm? mf)
		{
			pictItemList1.SetMainForm(mf);
		}
		public void SetItemsLib(ItemsLib? mf)
		{
			pictItemList1.SetItemsLib(mf);
		}
		public PictItemDialog()
		{
			InitializeComponent();
			pictItemList1.PictItemChanged += (sender, e) =>
			{
				PictItem? pi = e.Item;
				if (pi != null)
				{
					textBox1.Text = $"Name:{pi.Name} Size:{pi.OrgSize.ToString()}";
				}
			};
			btnCancel.Click += (sender, e) => { this.DialogResult = DialogResult.Cancel; };
			btnOK.Click += (sender, e) => { this.DialogResult = DialogResult.OK; };
		}
	}
}
