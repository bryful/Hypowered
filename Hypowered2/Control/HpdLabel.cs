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
	public partial class HpdLabel : HpdControl
	{
		[Browsable(false)]
		public new int  CaptionWidth
		{
			get { m_CaptionWidth = 0; return 0; }
			set { m_CaptionWidth = 0; }
		}
		[Browsable(false)]
		public new string Caption
		{
			get { m_Caption = ""; return ""; }
			set
			{
				m_Caption = "";
			}
		}
		public HpdLabel()
		{
			SetHpdType(HpdType.Label);
			ScriptCode.SetSTypes(
				ScriptTypeBit.None
				//ScriptTypeBit.Load|
				//ScriptTypeBit.Closed|
				//ScriptTypeBit.ValueChanged
				//ScriptTypeBit.DragDrop|
				//ScriptTypeBit.KeyPress|
				//ScriptTypeBit.MouseDoubleClick |
				//ScriptTypeBit.MouseClick
				);
			InitializeComponent();
		}
	}
}
