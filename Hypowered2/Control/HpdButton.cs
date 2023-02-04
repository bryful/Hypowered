using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hpd
{
	public partial class HpdButton : HpdControl
	{
		[Category("Hypowered"), Browsable(true)]
		public DialogResult DialogResult
		{
			get
			{
				if ((m_Item != null) && (m_Item is Button))
				{
					return ((Button)m_Item).DialogResult;
				}
				else
				{
					return DialogResult.Cancel;
				}
			}
			set
			{
				if ((m_Item != null) && (m_Item is Button))
				{
					((Button)m_Item).DialogResult = value;
				}
			}
		}
		[Category("Hypowered"), Browsable(true)]
		public FlatStyle FlatStyle
		{
			get
			{
				if ((m_Item != null) && (m_Item is Button))
				{
					return ((Button)m_Item).FlatStyle;
				}
				else { return FlatStyle.Standard; }
			}
			set
			{
				if ((m_Item != null) && (m_Item is Button))
				{
					((Button)m_Item).FlatStyle = value;
				}
			}
		}
		public HpdButton()
		{
			SetHpdType(HpdType.Button);
			ScriptCode.SetSTypes(
				//ScriptTypeBit.Load|
				//ScriptTypeBit.Closed|
				//ScriptTypeBit.ValueChanged|
				//ScriptTypeBit.DragDrop|
				//ScriptTypeBit.KeyPress|
				//ScriptTypeBit.MouseDoubleClick |
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
