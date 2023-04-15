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
	public class HTypeCombo : ComboBox
	{
		public HType HType
		{
			get 
			{
				InitList();
				if (this.SelectedIndex < 0) this.SelectedIndex = 0;
				return (HType)(this.SelectedIndex + 1);
			} 
			set
			{
				int idx = ((int)value - 1);
				if(idx < 0) { idx = 0; }
				InitList();
				this.SelectedIndex = idx;
			}
		}
		public HTypeCombo()
		{
			this.DropDownStyle = ComboBoxStyle.DropDownList;
			InitList();
		}
		protected override void InitLayout()
		{
			base.InitLayout();
			InitList();
		}
		public void InitList()
		{
			string[] sa = Enum.GetNames(typeof(HType));
			int si = this.SelectedIndex;
			if(this.Items.Count != sa.Length-1) 
			{
				this.Items.Clear();
				string[] sa2 = new string[sa.Length-1];
				for(int i=1; i<sa.Length; i++)
				{
					sa2[i-1] = sa[i];
				}
				this.Items.AddRange(sa2);
				this.SelectedIndex = si;
			}
		}
	}
}
