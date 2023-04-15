using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

using static System.Windows.Forms.DataFormats;

namespace Hypowered
{
    public partial class HyperLabel : HyperControl
	{
		public HyperLabel()
		{
			SetControlType(Hypowered.ControlType.Label);
			SetInScript(InScriptBit.DragDrop);
			SetName("HyperLabel");
			this.TabStop= false;
			this.Size = ControlDef.DefSize;
			InitializeComponent();
			this.SetStyle(
	//ControlStyles.Selectable |
	//ControlStyles.UserMouse |
	ControlStyles.DoubleBuffer |
	ControlStyles.UserPaint |
	ControlStyles.AllPaintingInWmPaint |
	ControlStyles.SupportsTransparentBackColor,
	true);
			this.UpdateStyles();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = pe.Graphics;
				// 背景色
				g.FillRectangle(sb, this.ClientRectangle);

				// 外枠
				Rectangle rr = ReRect(this.ClientRectangle, 2);
				
				if (this.Text != "")
				{
					sb.Color = ForeColor;
					g.DrawString(this.Text, this.Font, sb, ReRect(this.ClientRectangle, 3), m_format);
				}
				DrawEditMode(g, p, sb);


			}
		}
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(ControlType), (int?)ControlType);//Nullable`1

			return jf.Obj;
		}
	}
}
