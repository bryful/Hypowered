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
using ImageMagick;

namespace Hypowered
{
	public class HTextBox : HControl
	{
		private EditTextBox Edit { get; set; }= new EditTextBox();
		public override void SetIsEdit(bool b, bool IsEven = true)
		{
			if(m_IsEdit)
			{
				EndEdit();
			}
			base.SetIsEdit(b, IsEven);
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
				Edit.Font = value;
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
				Edit.BackColor = value;
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
				Edit.ForeColor = value;
				this.Invalidate();
			}
		}
		public HTextBox()
		{
			m_HType = HType.TextBox;
			Edit.Visible = false;
			Edit.BackColor = base.BackColor;
			Edit.ForeColor = base.ForeColor;
			Edit.BorderStyle = BorderStyle.FixedSingle;
			Edit.Name = "EditTextBox";
			ChkSize();
			TextAlign = StringAlignment.Near;
			Edit.LostFocus += (sender, e) => { EndEdit(); };
			Edit.PreviewKeyDown += (sender, e) =>
			{
				if (e.KeyData == Keys.Enter) { EndEdit(true); }
				else if (e.KeyData == Keys.Escape) { EndEdit(false); }
			};
			this.Controls.Add(Edit);
		}
		public void ChkSize()
		{
			bool b = _RefFlag;
			_RefFlag = true;
			Point np = new Point(
				(base.Location.X / m_GridSize) * m_GridSize,
				(base.Location.Y / m_GridSize) * m_GridSize);
			if (base.Location != np) base.Location = np;
			Size ns = new Size(
				(base.Size.Width / m_GridSize) * m_GridSize,
				(base.Size.Height / m_GridSize) * m_GridSize);
			if (base.Size != ns) base.Size = ns;

			Edit.Location = new Point(2, 2);
			Edit.Size = new Size(base.Width - 4, base.Height - 4);
			if (Edit.Height != base.Height-4)
			{
				base.Height = Edit.Height + 4;
			}
			_RefFlag = b;
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
				Rectangle r = RectInc(this.ClientRectangle, 2);
				sb.Color = BackColor;
				g.FillRectangle(sb, r);
				sb.Color = ForeColor;
				g.DrawString(base.Text, base.Font, sb, r,StringFormat);

				p.Color = ForeColor;
				DrawFrame(g,p, r, 1);

				DrawIsEdit(g, p);
			}
		}
		protected override void OnResize(EventArgs e)
		{
			if (_RefFlag) return;
			ChkSize();
			base.OnResize(e);
		}
		private void SetEdit()
		{
			if (m_IsEdit) return;
			Edit.Text = base.Text;
			Edit.Visible = true;
			Edit.Focus();
		}
		private void EndEdit(bool rev=true)
		{
			if (m_IsEdit) return;
			if (rev==true)
			{
				base.Text = Edit.Text;
			}
			Edit.Visible = false;
			this.Focus();
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
			{
				SetIsEdit(true);
				this.Invalidate();
				if (HForm != null)
				{
					HForm.TargetIndex = this.Index;
					HForm.Invalidate();
				}
				return;
			}
			if ((e.Button & MouseButtons.Left)== MouseButtons.Left)
			{
				if(m_IsEdit==false)
				{
					SetEdit();
					return;
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
		{
			if((IsEdit==false)&&(e.KeyData== Keys.Enter))
			{
				SetEdit();
				return;
			}
			base.OnPreviewKeyDown(e);
		}
		public override JsonObject? ToJson()
		{
			JsonFile? jf = new JsonFile(base.ToJson());

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
		}
	}
}
