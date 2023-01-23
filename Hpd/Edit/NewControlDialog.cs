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
	public partial class NewControlDialog : HpdForm
	{
		public HpdType HpdType
		{
			get
			{
				HpdType ret = HpdType.None;
				object? obj = cmbControl.SelectedItem;
				if(obj != null)
				{
					if(obj is CombItem)
					{
						CombItem ci = (CombItem)obj;
						if (ci.obj != null)
							ret = (HpdType)ci.obj;
					}
				}
				return ret;
			}
			set
			{
				int idx = FindHpdType(value);
				if(idx >= 0) { cmbControl.SelectedIndex = idx; }
			}
		}
		private int FindHpdType(HpdType ht)
		{
			int ret = -1;
			int idx = 0;
			foreach(object? c in cmbControl.Items)
			{
				if((c!=null)&&(c is CombItem))
				{
					object? o2 = ((CombItem)c).obj;
					if(o2!=null)
					{
						if ((HpdType)o2 == ht)
						{
							ret = idx;
							break;
						}
					}
				}
				idx++;
			}
			return ret;
		}
		public string HpdName
		{
			get { return tbName.Text; }
			set { tbName.Text = value; }
		}
		public string HpdText
		{
			get { return tbName.Text; }
			set { tbName.Text = value; }
		}
		public NewControlDialog()
		{
			InitializeComponent();
			SetupComb();
		}
		protected override void InitLayout()
		{
			base.InitLayout();
			SetupComb();
		}
		private void SetupComb()
		{

			cmbControl.Items.Clear();
			string[] itms = Enum.GetNames(typeof(HpdType));
			int idx = 0;
			List<CombItem> items = new List<CombItem>();
			foreach (string itm in itms)
			{
				CombItem ii = new CombItem(itm, idx);
				if(ii.Name!="None")
				{
					items.Add(ii);
				}
				idx++;
			}
			cmbControl.Items.AddRange(items.ToArray());
			
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult= DialogResult.Cancel;
		}

		private void hpdButton1_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
	}
}
