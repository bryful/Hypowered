using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hpd
{
	public partial class HpdComboBox : HpdControl
	{
		[Category("Hypowered_TextBox")]
		public string[] Lines
		{
			get
			{
				if (m_Item != null)
				{
					return m_Item.Text.Split("\r\n");
				}
				else
				{
					return base.Text.Split("\r\n");
				}
			}
			set
			{
				if (m_Item != null)
				{
					m_Item.Text = string.Join("\r\n", value);
				}
				base.Text = string.Join("\r\n", value);
				this.Invalidate();
			}
		}
		[Category("Hypowered"), Browsable(true)]
		public FlatStyle FlatStyle
		{
			get
			{
				if ((m_Item!=null)&&(m_Item is ComboBox))
				{
					return ((ComboBox)m_Item).FlatStyle;
				}else { return FlatStyle.Standard; }
			}
			set
			{
				if ((m_Item != null) && (m_Item is ComboBox))
				{
					((ComboBox)m_Item).FlatStyle = value;
				}
			}
		}
		[Category("Hypowered_ComboBox")]
		public bool IntegralHeight
		{
			get
			{
				bool ret = false;
				if (m_Item != null)
				{
					if (m_Item is ComboBoxHpd)
					{
						ret = ((ComboBoxHpd)m_Item).IntegralHeight;
					}
				}
				return ret;
			}
			set
			{
				if (m_Item != null)
				{
					if (m_Item is ComboBoxHpd)
					{
						((ComboBoxHpd)m_Item).IntegralHeight = value;
					}
				}
			}
		}
		[Category("Hypowered")]
		public int SelectedIndex
		{
			get
			{
				int ret = -1;
				if (m_Item != null)
				{
					if (m_Item is ComboBoxHpd) 
					{
						ret = ((ComboBoxHpd)m_Item).SelectedIndex; 
					}
				}
				return ret;
			}
			set
			{
				if (m_Item != null)
				{
					if (m_Item is ComboBoxHpd) 
					{
						((ComboBoxHpd)m_Item).SelectedIndex = value; 
					}
				}
			}
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public ComboBox.ObjectCollection Items
		{
			get
			{
				return ((ComboBoxHpd)m_Item).Items;
			}
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public System.Int32 DropDownWidth
		{
			get
			{
				int ret = 0;
				if (m_Item != null)
				{
					if (m_Item is ComboBoxHpd)
					{
						ret = ((ComboBoxHpd)m_Item).DropDownWidth;
					}
				}
				return ret;
			}
			set
			{
				if (m_Item != null)
				{
					if (m_Item is ComboBoxHpd)
					{
						((ComboBoxHpd)m_Item).DropDownWidth = value;
					}
				}
			}
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public System.Int32 DropDownHeight
		{
			get
			{
				int ret = 0;
				if (m_Item != null)
				{
					if (m_Item is ComboBoxHpd)
					{
						ret = ((ComboBoxHpd)m_Item).DropDownHeight;
					}
				}
				return ret;
			}
			set
			{
				if (m_Item != null)
				{
					if (m_Item is ComboBoxHpd)
					{
						((ComboBoxHpd)m_Item).DropDownHeight = value;
					}
				}
			}
		}
		public HpdComboBox()
		{
			SetHpdType(HpdType.ComboBox);
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
