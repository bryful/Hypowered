using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hpd
{
	partial class HpdControl
	{
		#region Event
		public delegate void NameChangedHandler(object sender, EventArgs e);
		public event NameChangedHandler? NameChanged;
		protected virtual void OnNameChanged(EventArgs e)
		{
			if (NameChanged != null)
			{
				NameChanged(this, e);
			}
		}

		public EventHandler? SelectedIndexChanged;
		protected virtual void OnSelectIndexChanged(EventArgs e)
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, e);
			}
		}
		/// <summary>
		/// trueにするとイベントが発生しない。
		/// </summary>
		protected bool CheckedChangedFlag=false;
		public EventHandler? CheckedChanged;
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			if (CheckedChangedFlag == true) return;
			if (CheckedChanged != null)
			{
				CheckedChanged(this, e);
			}
			//他のラジオボタンをオフにする処理
			if((m_Item!=null)&&(m_Item is RadioButton))
			{
				RadioButton rb = (RadioButton)m_Item;
				if(rb.Checked)
				{
					HpdRadioButton[] rbs = GetHpdRadioButtons();
					foreach(HpdRadioButton r in rbs)
					{
						if (this.Equals(r)) continue;
						if (r.Checked)
						{
							r.CheckedChangedFlag = true;
							r.Checked = false;
							r.CheckedChangedFlag = false;
						}
					}
				}
			}
		}

		#endregion
	}
}
