﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Hypowered
{
	public partial class HyperLabel : HyperControl
	{
		private StringFormat m_format = new StringFormat();
		[Category("Hypowerd_Text")]
		public StringAlignment TextAligiment
		{
			get { return m_format.Alignment; }
			set { m_format.Alignment = value;this.Invalidate(); }
		}
		public HyperLabel()
		{
			SetMyType(ControlType.Label);
			m_format.Alignment = StringAlignment.Near;
			m_format.LineAlignment= StringAlignment.Center;
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			this.Name = "HyperLabel";
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
				//p.Color = ForeColor;
				//g.DrawRectangle(p, rr);
				/*
				if (this.Focused)
				{
					rr = ReRect(this.ClientRectangle, 1);
					p.Color = m_ForcusColor;
					g.DrawRectangle(p, rr);
				}
				*/
				if (this.Text != "")
				{
					sb.Color = ForeColor;
					g.DrawString(this.Text, this.Font, sb, ReRect(this.ClientRectangle, 3), m_format);
				}
				if (m_IsEditMode)
				{
					sb.Color = m_ForcusColor;
					g.DrawString("IsEdit", this.Font, sb, this.ClientRectangle);
				}
			}
		}
	}
}
