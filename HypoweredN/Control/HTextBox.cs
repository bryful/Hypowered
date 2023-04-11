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
		public EditTextBox EditTextBox { get; set; }= new EditTextBox();
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
			get {return EditTextBox.Text;}
			set 
			{
				base.Text = value;
				EditTextBox.Text = value;
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
				EditTextBox.Font = value;
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
				EditTextBox.BackColor = value;
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
				EditTextBox.ForeColor = value;
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
							EditTextBox.TextAlign = HorizontalAlignment.Left;
							break;
						case StringAlignment.Center:
							EditTextBox.TextAlign = HorizontalAlignment.Center;
							break;
						case StringAlignment.Far:
							EditTextBox.TextAlign = HorizontalAlignment.Right;
							break;
					}
				}
				this.Invalidate();
			}
		}
		[Category("Hypowered_Text"), Browsable(true)]
		public bool Multiline
		{
			get { return EditTextBox.Multiline; }
			set
			{
				EditTextBox.Multiline = value;
				ChkGridSize();
				ChkSize();
			}
		}
		public HTextBox()
		{
			m_HType = HType.TextBox;
			Ctrl = EditTextBox;
			EditTextBox.BackColor = base.BackColor;
			EditTextBox.ForeColor = base.ForeColor;
			EditTextBox.BorderStyle = BorderStyle.FixedSingle;
			EditTextBox.Name = "EditTextBox";
			EditTextBox.Location = new Point(2, 2);
			EditTextBox.Size = new Size(base.Width-4, base.Height-4);
			TextAlign = StringAlignment.Near;
			EditTextBox.GotFocus += (sender, e) => { this.Invalidate(); };
			EditTextBox.LostFocus += (sender, e) => { this.Invalidate(); };
			this.Controls.Add(EditTextBox);
		}
		public void ChkSize()
		{
			EditTextBox.Location = new Point(2, 2);
			EditTextBox.Size = new Size(base.Width - 4, base.Height - 4);
			if (EditTextBox.Height != base.Height-4)
			{
				base.Height = EditTextBox.Height + 4;
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
				if(m_IsEdit)
				{
					Rectangle r = RectInc(this.ClientRectangle, 2);
					sb.Color = BackColor;
					g.FillRectangle(sb, r);
					sb.Color = ForeColor;
					g.DrawString(EditTextBox.Text, EditTextBox.Font, sb, r,StringFormat);
				}

				if ((Focused) || (EditTextBox.Focused))
				{
					p.Color = m_ForcusColor;
					DrawFrame(g, p, this.ClientRectangle, 2);
				}
				DrawIsEdit(g, p);
			}
		}
		protected override void OnResize(EventArgs e)
		{
			ChkSize();
			base.OnResize(e);
		}
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			EditTextBox.Focus();
		}
	}
}
