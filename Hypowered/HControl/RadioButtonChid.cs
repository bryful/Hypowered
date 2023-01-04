using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Hypowered
{
	public partial class RadioButtonChild : HyperControl
	{
		public new bool IsEditMode
		{
			get { return base.m_IsEditMode; }
			set
			{
				base.SetIsEditMode(value);
				if (m_isEdit)
				{
					ChkEdit();
					EndEdit();
				}
				this.Invalidate();
			}
		}
		private bool m_Checked;
		[Category("Hypowered_RadioButton")]
		public bool Checked
		{
			get { return m_Checked; }
			set { m_Checked = value; this.Invalidate(); }
		}
		private int m_CheckSize;
		[Category("Hypowered_RadioButton")]
		public int CheckSize
		{
			get { return m_CheckSize; }
			set { m_CheckSize = value; this.Invalidate(); }
		}
		public RadioButtonChild()
		{
			m_CheckSize = 16;
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = pe.Graphics;
				// 背景色
				g.FillRectangle(sb, this.ClientRectangle);

				Rectangle r = new Rectangle(3, (this.Height - m_CheckSize) / 2, m_CheckSize, m_CheckSize);
				p.Width = 1;
				g.DrawEllipse(p, r);
				if (m_Checked)
				{
					sb.Color = ForeColor;
				}
				else
				{
					sb.Color = m_UnCheckedColor;
				}
				RectangleF rs = ReRectF(r, 5);
				g.FillEllipse(sb, rs);

				// 外枠
				Rectangle rr = ReRect(this.ClientRectangle, 2);
				//p.Color = ForeColor;
				//g.DrawRectangle(p, rr);
				if (this.Focused)
				{
					rr = ReRect(this.ClientRectangle, 1);
					p.Color = m_ForcusColor;
					g.DrawRectangle(p, rr);
				}
				if (this.Text != "")
				{
					sb.Color = ForeColor;
					rr = new Rectangle(m_CheckSize + 5, 3, this.Width - m_CheckSize - 5, this.Height - 6);
					g.DrawString(this.Text, this.Font, sb, rr, m_format);
				}
				DrawType(g, sb);

			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			HyperRadioButton? p=null;
			if (this.Parent is HyperRadioButton)
			{
				p = (HyperRadioButton)this.Parent;
				IsEditMode = p.IsEditMode;
			}
			if (m_IsEditMode)
			{
					SetEdit();
			}
			else
			{
				
				if(p!= null)
				{
					p.CheckAllOff();
					m_Checked= true;
					p.SetValue(this.Index,false);
					this.Invalidate();
				}
				
				
			}
		}
		private bool m_isEdit = false;
		private void SetEdit()
		{
			if (m_isEdit) return;
			m_isEdit = true;
			TextBox tb = new TextBox();
			tb.Text = this.Text;
			tb.BorderStyle = BorderStyle.FixedSingle;
			tb.Size = new Size(this.Width-m_CheckSize-10, this.Height);
			tb.Location = new Point(m_CheckSize+4, 0);
			tb.KeyDown += Tb_KeyDown;
			tb.LostFocus += Tb_LostFocus;
			this.Controls.Add(tb);
			tb.Focus();
		}
		private void Tb_LostFocus(object? sender, EventArgs e)
		{
			ChkEdit();
			EndEdit();
		}
		private void ChkEdit()
		{
			TextBox tb = (TextBox)this.Controls[this.Controls.Count - 1];
			this.Text = tb.Text;
		}
		private void EndEdit()
		{
			TextBox tb = (TextBox)this.Controls[this.Controls.Count - 1];
			this.Controls.Remove(tb);
			tb.Dispose();
			m_isEdit = false;
			this.Focus();
			this.Invalidate();
		}
		private void Tb_KeyDown(object? sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				ChkEdit();
				EndEdit();
			}
			else if (e.KeyData == Keys.Escape)
			{
				EndEdit();
			}
		}
		public void StopEdit()
		{
			if(m_isEdit)
			{
				ChkEdit();
				EndEdit();
			}
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if ((e.KeyData == Keys.Enter)&&(this.Focused)&&(m_IsEditMode))
			{
				SetEdit();
			}else if ((e.KeyData == Keys.Space)|| (e.KeyData == Keys.Enter))
			{
				HyperRadioButton? p = null;
				if (this.Parent is HyperRadioButton)
				{
					p = (HyperRadioButton)this.Parent;
					IsEditMode = p.IsEditMode;
				}
				if (p != null)
				{
					p.CheckAllOff();
					m_Checked = true;
					p.SetValue(this.Index, false);
					this.Invalidate();
				}
			}
			else
			{
				base.OnKeyDown(e);
			}
		}
	}
}
