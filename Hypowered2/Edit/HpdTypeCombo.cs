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
	public partial class HpdTypeCombo : ComboBox
	{
		public HpdType HpdType
		{
			get { return (HpdType)this.SelectedIndex; }
			set
			{
				int v = (int)value;
				if ((v >= 0) && (v < Items.Count))
				{
					this.SelectedIndex = v;
				}
				else
				{
					this.SelectedIndex = -1;
				}
			}
		}
		public HpdTypeCombo()
		{
			Init();
			InitializeComponent();
		}
		protected override void InitLayout()
		{
			base.InitLayout();
			Init();
		}
		public void Init()
		{
			this.Items.Clear();
			string[] names = Enum.GetNames(typeof(HpdType));
			List<string> list = new List<string>();
			foreach (string name in names)
			{
				if(name=="None") continue;
				list.Add(name);
			}
			names = list.ToArray();
			this.Items.AddRange(names);
			this.SelectedIndex = 0;
		}
		public string DefName
		{
			get
			{
				string ret = "hpdControl";
				if((SelectedIndex>=0)&&(Items[SelectedIndex]!=null))
				{
					string? s = Items[SelectedIndex].ToString();
					if((s!=null)&&(s!=""))
					{
						ret = s.Substring(0, 1).ToLower() + s.Substring(1);
					}
				}
				return ret;
			}
		}
	}
}
