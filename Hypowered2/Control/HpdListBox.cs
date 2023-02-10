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

	public partial class HpdListBox : HpdControl
	{
		[Category("Hypowered_ListBox")]
		public bool IntegralHeight
		{
			get
			{
				bool ret = false;
				if (m_Item != null)
				{
					if (m_Item is ListBoxHpd)
					{
						ret = ((ListBoxHpd)m_Item).IntegralHeight;
					}
				}
				return ret;
			}
			set
			{
				if (m_Item != null)
				{
					if (m_Item is ListBoxHpd)
					{
						((ListBoxHpd)m_Item).IntegralHeight = value;
					}
				}
			}
		}
		[Category("Hypowered_ListBox")]
		public int SelectedIndex
		{
			get
			{
				int ret = -1;
				if (m_Item != null)
				{
					if (m_Item is ListBoxHpd) 
					{
						ret = ((ListBoxHpd)m_Item).SelectedIndex;
					}
				}
				return ret;
			}
			set
			{
				if (m_Item != null)
				{
					if (m_Item is ListBoxHpd) 
					{ 
						((ListBoxHpd)m_Item).SelectedIndex = value; 
					}
				}
			}
		}
		[Category("Hypowered_ListBox")]
		public ListBox.ObjectCollection? Items
		{
			get
			{
				ListBox.ObjectCollection? ret = null;
				if ((m_Item != null) && (m_Item is ListBox))
				{
					ret = ((ListBox)m_Item).Items;
				}
				return ret;
			}
		}
		public HpdListBox()
		{
			SetHpdType(HpdType.ListBox);
			ScriptCode.SetSTypes(
				//ScriptTypeBit.None
				//ScriptTypeBit.Load|
				//ScriptTypeBit.Closed|
				ScriptTypeBit.ValueChanged|
				//ScriptTypeBit.DragDrop|
				//ScriptTypeBit.KeyPress|
				ScriptTypeBit.MouseDoubleClick |
				ScriptTypeBit.MouseClick
				);
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
	}
}
