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
		public class ValueChangedEventArgs : EventArgs
		{
			public object? Value;
			public ValueChangedEventArgs(object? v)
			{
				Value = v;
			}
		}
		public delegate void ValueChangedHandler(object sender, ValueChangedEventArgs e);
		/// <summary>
		/// trueにするとイベントが発生しない。
		/// </summary>
		protected bool ValueChangedFlag=false;
		public ValueChangedHandler? ValueChanged;
		protected virtual void OnValueChanged(ValueChangedEventArgs e)
		{
			if (ValueChangedFlag == true) return;
			if (ValueChanged != null)
			{
				ValueChanged(this, e);
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
							r.ValueChangedFlag = true;
							r.Checked = false;
							r.ValueChangedFlag = false;
						}
					}
				}
			}
		}

		#endregion
	}
}
