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
	public partial class HpdCheckBox : HpdControl
	{
		[Category("Hypowered")]
		public bool Checked
		{
			get
			{
				bool ret = false;
				if (m_Item != null)
				{
					if (m_Item is CheckBox)
					{
						return ((CheckBox)m_Item).Checked;
					}
				}
				return ret;
			}
			set
			{
				if (m_Item != null)
				{
					if (m_Item is CheckBox)
					{
						if (((CheckBox)m_Item).Checked != value)
						{
							((CheckBox)m_Item).Checked = value;
							OnValueChanged(new ValueChangedEventArgs((object?)((CheckBox)m_Item).Checked));
						}
					}
				}
			}
		}

		public HpdCheckBox()
		{
			SetHpdType(HpdType.CheckBox);
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

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
	}
}
