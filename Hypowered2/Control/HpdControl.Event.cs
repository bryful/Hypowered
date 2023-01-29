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
		public EventHandler? CheckedChanged;
		protected virtual void OnCheckedChangedd(EventArgs e)
		{
			if (CheckedChanged != null)
			{
				CheckedChanged(this, e);
			}
		}

		#endregion
	}
}
