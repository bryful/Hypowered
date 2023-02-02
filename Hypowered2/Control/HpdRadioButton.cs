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
	public partial class HpdRadioButton : HpdControl
	{
		[Category("Hypowered")]
		public bool Checked
		{
			get
			{
				bool ret = false;
				if (m_Item != null)
				{
					if (m_Item is RadioButton)
					{
						return ((RadioButton)m_Item).Checked;
					}
				}
				return ret;
			}
			set
			{
				if (m_Item != null)
				{
					if (m_Item is RadioButton)
					{
						if (((RadioButton)m_Item).Checked != value)
						{
							((RadioButton)m_Item).Checked = value;
							OnValueChanged(new ValueChangedEventArgs((object)((RadioButton)m_Item).Checked));
						}
					}
				}
			}
		}
		public HpdRadioButton()
		{
			SetHpdType(HpdType.RadioButton);
			ScriptCode.SetSTypes(
				//ScriptTypeBit.Load|
				//ScriptTypeBit.Closed|
				ScriptTypeBit.ValueChanged
				//ScriptTypeBit.DragDrop|
				//ScriptTypeBit.KeyPress|
				//ScriptTypeBit.MouseDoubleClick |
				//ScriptTypeBit.MouseClick
				);
			InitializeComponent();
		}

	}
}
