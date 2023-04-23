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
using System.Drawing.Text;
namespace Hypowered
{
	public class HTextBox : HControl
	{
		#region Prop
		private EditTextBox TextBox { get; set; }= new EditTextBox();
		public override void SetSelected(bool b, bool IsEven = true)
		{
			if(m_Selected)
			{
				EndTextBox();
			}
			base.SetSelected(b, IsEven);
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
				TextBox.Font = value;
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
				TextBox.BackColor = value;
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
				TextBox.ForeColor = value;
				this.Invalidate();
			}
		}
		#endregion
		public HTextBox()
		{
			m_HType = HType.TextBox;
			ScriptCode.Setup(HScriptType.ValueChanged);
			TextBox.Visible = false;
			TextBox.BackColor = base.BackColor;
			TextBox.ForeColor = base.ForeColor;
			TextBox.BorderStyle = BorderStyle.FixedSingle;
			TextBox.Name = "EditTextBox";
			ChkSize();
			TextAlign = StringAlignment.Near;
			TextBox.LostFocus += (sender, e) => { EndTextBox(); };
			TextBox.PreviewKeyDown += (sender, e) =>
			{
				if (e.KeyData == Keys.Enter) { EndTextBox(true); }
				else if (e.KeyData == Keys.Escape) { EndTextBox(false); }
			};
			this.Controls.Add(TextBox);
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

			TextBox.Location = new Point(2, 2);
			TextBox.Size = new Size(base.Width - 4, base.Height - 4);
			if (TextBox.Height != base.Height-4)
			{
				base.Height = TextBox.Height + 4;
			}
			_RefFlag = b;
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(base.BackColor))
			using (Pen p = new Pen(base.ForeColor))
			{
				Graphics g = pe.Graphics;
				if (m_IsAnti)
				{
					g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
					g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				}

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

				DrawCtrlRect(g, p);
			}
		}
		protected override void OnResize(EventArgs e)
		{
			if (_RefFlag) return;
			ChkSize();
			base.OnResize(e);
		}
		private void SetTextBox()
		{
			if (m_Selected) return;
			switch(TextAlign)
			{
				case StringAlignment.Center:
					TextBox.TextAlign = HorizontalAlignment.Center; 
					break;
				case StringAlignment.Near:
					TextBox.TextAlign = HorizontalAlignment.Left;
					break;
				case StringAlignment.Far:
					TextBox.TextAlign = HorizontalAlignment.Right;
					break;
			}
			TextBox.Text = base.Text;
			TextBox.Visible = true;
			TextBox.Focus();
		}
		private void EndTextBox(bool rev=true)
		{
			if (m_Selected) return;
			if (rev==true)
			{
				base.Text = TextBox.Text;
			}
			TextBox.Visible = false;
			this.Focus();
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (m_IsEdit)
			{
				base.OnMouseDown(e);
			}
			else
			{
				if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
				{
					SetTextBox();
				}

			}
		}
		protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
		{
			if (m_IsEdit==false)
			{
				if (e.KeyData == Keys.Enter)
				{
					SetTextBox();
					return;
				}
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
			//object? v = null;
		}
	}
}
