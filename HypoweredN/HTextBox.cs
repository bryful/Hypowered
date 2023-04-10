using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Hypowered
{
	public class HTextBox : HControl
	{
		public override void SetIsEdit(bool b)
		{
			m_IsEdit = b;
			if(Ctrl!=null)
			{
				Ctrl.Visible = !b;
			}
			this.Invalidate();
		}
		[Category("Hypowered_Text"), Browsable(true)]
		public new System.String Text
		{
			get {
				if (Ctrl != null)
				{
					return Ctrl.Text;
				}
				else
				{
					return base.Text;
				}
			}
			set 
			{
				base.Text = value;
				if (Ctrl!=null)
				{
					Ctrl.Text = value;
				}
			}
		}
		[Category("Hypowered_Size"), Browsable(true)]
		public new System.Drawing.Size Size
		{
			get { return base.Size; }
			set 
			{
				base.Size = value; 
				ChkGridSize();
				ChkSize();
			}
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Int32 Height
		{
			get { return base.Height; }
			set {
				base.Height = value;
				ChkGridSize();
				ChkSize();
			}
		}
		[Category("Hypowered_Text"), Browsable(true)]
		public new System.Drawing.Font Font
		{
			get { return base.Font; }
			set 
			{
				base.Font = value;
				ChkGridSize();
				ChkSize();
			}
		}
		[Category("Hypowered_Color"), Browsable(true)]
		public new System.Drawing.Color BackColor
		{
			get { return base.BackColor; }
			set 
			{
				base.BackColor = value; 
				if(Ctrl!=null) { Ctrl.BackColor = value; }
				this.Invalidate(); 
			}
		}
		[Category("Hypowered_Color"), Browsable(true)]
		public new System.Drawing.Color ForeColor
		{
			get { return base.ForeColor; }
			set
			{
				base.ForeColor = value;
				if (Ctrl != null) { Ctrl.ForeColor = value; }
				this.Invalidate();
			}
		}
		[Category("Hypowered_Text"), Browsable(true)]
		public new StringAlignment TextAlign
		{
			get { return StringFormat.Alignment; }
			set
			{
				StringFormat.Alignment = value;
				if(Ctrl!=null)
				{
					switch(value)
					{
						case StringAlignment.Near:
							((EditTextBox)Ctrl).TextAlign = HorizontalAlignment.Left;
							break;
						case StringAlignment.Center:
							((EditTextBox)Ctrl).TextAlign = HorizontalAlignment.Center;
							break;
						case StringAlignment.Far:
							((EditTextBox)Ctrl).TextAlign = HorizontalAlignment.Right;
							break;
					}
				}
				this.Invalidate();
			}
		}
		public HTextBox()
		{
			m_HType = HType.TextBox;
			Ctrl = new EditTextBox();
			Ctrl.BackColor = base.BackColor;
			Ctrl.ForeColor = base.ForeColor;
			((EditTextBox)Ctrl).BorderStyle = BorderStyle.FixedSingle;
			Ctrl.Name = "EditTextBox";
			Ctrl.Location = new Point(2, 2);
			Ctrl.Size = new Size(base.Width-4, base.Height-4);
			TextAlign = StringAlignment.Near;
			this.Controls.Add(Ctrl);
		}
		public void ChkSize()
		{
			if (Ctrl != null)
			{
				Ctrl.Location = new Point(2, 2);
				Ctrl.Size = new Size(base.Width - 4, base.Height - 4);
				if (Ctrl.Height != base.Height-4)
				{
					base.Height = Ctrl.Height + 4;
				}
			}
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(base.BackColor))
			using (Pen p = new Pen(base.ForeColor))
			{
				Graphics g = pe.Graphics;
				// 塗り
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);
				// IsEdit
				if((m_IsEdit)&&(Ctrl != null))
				{
					Rectangle r = RectInc(this.ClientRectangle, 2);
					sb.Color = BackColor;
					g.FillRectangle(sb, r);
					sb.Color = ForeColor;
					g.DrawString(Ctrl.Text, Ctrl.Font, sb, r,StringFormat);
				}
				DrawIsEdit(g, p);
			}
		}
		protected override void OnResize(EventArgs e)
		{
			ChkSize();
			base.OnResize(e);
		}
	}
}
