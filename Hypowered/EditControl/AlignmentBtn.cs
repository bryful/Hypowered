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
		HorEqu,
		HeightMax,
		HeightMin,
		WidthMax,
		WidthMin
	}
	public partial class AlignmentBtn : Button
	{
		private Bitmap[] m_bitmaps = new Bitmap[12];
		private Bitmap[] m_bitmapsD = new Bitmap[12];
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
			m_bitmaps[0] = Properties.Resources.zCtrlAli01;
			m_bitmaps[1] = Properties.Resources.zCtrlAli02;
			m_bitmaps[2] = Properties.Resources.zCtrlAli03;
			m_bitmaps[3] = Properties.Resources.zCtrlAli04;
			m_bitmaps[4] = Properties.Resources.zCtrlAli05;
			m_bitmaps[5] = Properties.Resources.zCtrlAli06;
			m_bitmaps[6] = Properties.Resources.zCtrlAli07;
			m_bitmaps[7] = Properties.Resources.zCtrlAli08;
			m_bitmaps[8] = Properties.Resources.zCtrlAli09;
			m_bitmaps[9] = Properties.Resources.zCtrlAli10;
			m_bitmaps[10] = Properties.Resources.zCtrlAli11;
			m_bitmaps[11] = Properties.Resources.zCtrlAli12;

			m_bitmapsD[0] = Properties.Resources.zCtrlAli_push01;
			m_bitmapsD[1] = Properties.Resources.zCtrlAli_push02;
			m_bitmapsD[2] = Properties.Resources.zCtrlAli_push03;
			m_bitmapsD[3] = Properties.Resources.zCtrlAli_push04;
			m_bitmapsD[4] = Properties.Resources.zCtrlAli_push05;
			m_bitmapsD[5] = Properties.Resources.zCtrlAli_push06;
			m_bitmapsD[6] = Properties.Resources.zCtrlAli_push07;
			m_bitmapsD[7] = Properties.Resources.zCtrlAli_push08;
			m_bitmapsD[8] = Properties.Resources.zCtrlAli_push09;
			m_bitmapsD[9] = Properties.Resources.zCtrlAli_push10;
			m_bitmapsD[10] = Properties.Resources.zCtrlAli_push11;
			m_bitmapsD[11] = Properties.Resources.zCtrlAli_push12;
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
