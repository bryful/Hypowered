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
	public enum AStyle
	{
		VurLeft,
		VurCenetr,
		VurRight,
		HorTop,
		HorCenter,
		HorBottom,
		VurEqu,
		HorEqu
	}
	public partial class AlignmentBtn : Button
	{
		private Bitmap[] m_bitmaps = new Bitmap[8];
		private Bitmap[] m_bitmapsD = new Bitmap[8];
		private AStyle m_AStyle = AStyle.HorTop;
		public AStyle AStyle
		{
			get { return m_AStyle; }
			set { m_AStyle = value; this.Invalidate(); }
		}
		private Color m_PushColor = Color.White;
		public Color PushColor
		{
			get { return m_PushColor; } 
			set { m_PushColor = value;}
		}
		public AlignmentBtn()
		{
			m_bitmaps[0] = Properties.Resources.CTRLALI1;
			m_bitmaps[1] = Properties.Resources.CTRLALI2;
			m_bitmaps[2] = Properties.Resources.CTRLALI3;
			m_bitmaps[3] = Properties.Resources.CTRLALI4;
			m_bitmaps[4] = Properties.Resources.CTRLALI5;
			m_bitmaps[5] = Properties.Resources.CTRLALI6;
			m_bitmaps[6] = Properties.Resources.CTRLALI7;
			m_bitmaps[7] = Properties.Resources.CTRLALI8;
			m_bitmapsD[0] = Properties.Resources.CTRLALI_push1;
			m_bitmapsD[1] = Properties.Resources.CTRLALI_push2;
			m_bitmapsD[2] = Properties.Resources.CTRLALI_push3;
			m_bitmapsD[3] = Properties.Resources.CTRLALI_push4;
			m_bitmapsD[4] = Properties.Resources.CTRLALI_push5;
			m_bitmapsD[5] = Properties.Resources.CTRLALI_push6;
			m_bitmapsD[6] = Properties.Resources.CTRLALI_push7;
			m_bitmapsD[7] = Properties.Resources.CTRLALI_push8;
			this.Size= new Size(24,24);
			this.FlatStyle= FlatStyle.Flat;
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
			Graphics g = pe.Graphics;
			if (m_isMD)
			{
				g.DrawImage(m_bitmapsD[(int)m_AStyle], 0, 0);
			}
			else
			{
				g.DrawImage(m_bitmaps[(int)m_AStyle], 0, 0);
			}
		}
		private bool m_isMD=false;
		protected override void OnMouseDown(MouseEventArgs mevent)
		{
			if(m_isMD==false)
			{
				m_isMD=true;
				this.Invalidate();
			}
			base.OnMouseDown(mevent);
		}
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			if (m_isMD == true)
			{
				m_isMD = false;
				this.Invalidate();
			}
			base.OnMouseUp(mevent);
		}
	}
}
