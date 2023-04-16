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
		public CHType CHType = new CHType();
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
			_Refflag = true;
			this.DropDownStyle = ComboBoxStyle.DropDownList;
			this.Items.Clear();
			this.Items.AddRange(this.CHType.NamesNone);
			this.SelectedIndex = 0;
			_Refflag = false;
		}
		protected override void InitLayout()
		{
			base.InitLayout();
			InitList();
		}
		public void InitList()
		{
			if(this.Items.Count != this.CHType.Names.Length)
			{
				_Refflag = true;
				int idx = this.SelectedIndex;
				this.Items.Clear();
				this.Items.AddRange(this.CHType.NamesNone);
				if((idx>=-1)&&(idx<this.Items.Count))
				{


					if(this.SelectedIndex != idx)
						this.SelectedIndex=idx;
				}
				_Refflag = false;
			}
		}
		private bool _Refflag = false;
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			if (_Refflag) return;
			base.OnSelectedIndexChanged(e);
		}
	}
}
