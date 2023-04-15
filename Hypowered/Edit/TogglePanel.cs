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
	public partial class TogglePanel : Control
	{
		public class IndexChangedEventArgs : EventArgs
		{
			public int Index;
			public IndexChangedEventArgs(int v)
			{
				Index = v;
			}
		}
		public delegate void IndexChangedHandler(object sender, IndexChangedEventArgs e);
		public event IndexChangedHandler? IndexChanged;
		protected virtual void OnIndexChanged(IndexChangedEventArgs e)
		{
			if (IndexChanged != null)
			{
				IndexChanged(this, e);
			}
		}
		private string[] m_Texts = new string[]
		{
			"page1",
			"page2",
			"page3",
			"page4",
			"page5",
			"page6",
			"page7",
			"page8",
			"page9",
			"page10",
		};
		public string[] Texts
		{
			get { return m_Texts; }
			set {
				m_Texts = value;
				this.Invalidate();
			}
		}
		private int m_Index = 0;
		public int Index
		{
			get { return m_Index; }
			set 
			{
				if (m_Index !=value)
				{
					m_Index = value;
					OnIndexChanged(new IndexChangedEventArgs(m_Index));
				}
				this.Invalidate();
			}
		}

		private int m_Count = 2;
		public int Count
		{
			get { return m_Count; }
			set
			{
				m_Count = value;
				if (m_Count < 2) m_Count = 2;
				else if (m_Count > 10) m_Count = 10;
				ChkSize(new Size(m_BtnWidth * m_Count, this.Height));
				this.Invalidate();
			}
		}
		private int m_BtnWidth = 80;
		public int BtnWidth
		{
			get { return m_BtnWidth; }
			set
			{
				m_BtnWidth = value;
				ChkSize(new Size(m_BtnWidth * m_Count, this.Height));
				this.Invalidate();
			}
		}
		public new Size Size
		{ 
			get { return base.Size; }
			set 
			{ 
				ChkSize(new Size(m_BtnWidth * m_Count, value.Height));
				this.Invalidate();
			}
		}
		private void ChkSize(Size sz)
		{
			this.MinimumSize = new Size(0, 0);
			this.MaximumSize = new Size(0, 0);
			base.Size = sz;
			this.MinimumSize = base.Size;
			this.MaximumSize = base.Size;
		}
		public new int Width
		{
			get { return base.Width; }
			set {}
		}
		private StringFormat m_Format= new StringFormat();
		public TogglePanel()
		{
			m_Format.Alignment = StringAlignment.Center;
			m_Format.LineAlignment = StringAlignment.Center;

			this.Location = new Point(0, 0);
			this.Size = new Size(m_BtnWidth* m_Count, 20);
			InitializeComponent();
			base.BackColor = Color.FromArgb(64, 64, 64);
			base.ForeColor = Color.FromArgb(230, 230, 230);
			this.DoubleBuffered = true;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.UpdateStyles();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			using(SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = pe.Graphics;
				g.FillRectangle(sb, this.ClientRectangle);

				for(int i=0; i<m_Count; i++)
				{
					Rectangle r = new Rectangle(i*m_BtnWidth,0,m_BtnWidth,this.Height);
					string tx = "";
					if((i>=0)&&(i<m_Texts.Length))
					{
						tx = m_Texts[i];
					}
					else
					{
						tx = $"Page{i}";
					}
					if(i==m_Index)
					{
						sb.Color =ForeColor;
						g.FillRectangle(sb, r);
						sb.Color =BackColor;
						g.DrawString(tx, this.Font,sb,r,m_Format);
					}
					else
					{
						sb.Color = ForeColor;
						g.DrawString(tx, this.Font, sb, r, m_Format);
						p.Color = ForeColor;
						Rectangle r2 = new Rectangle(r.Left,r.Top,r.Width-1,r.Height-1);
						g.DrawRectangle(p, r2);
					}
				}
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if((e.Button & MouseButtons.Left)== MouseButtons.Left)
			{
				int idx = e.X/m_BtnWidth;
				if( m_Index!=idx )
				{
					m_Index = idx;
					this.Invalidate();
					OnIndexChanged(new IndexChangedEventArgs(m_Index));
				}
			}
			base.OnMouseDown(e);
		}
	}
}
