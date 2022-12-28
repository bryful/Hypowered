using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Hypowered
{
    public class PictureView : PictureBox
    {
		private string m_Path = "";
        private Bitmap m_OffScr = new Bitmap(720, 1280);
        private float m_ratio = 1F;
        private Point mdPos = new Point(360, 640);
        private bool mdFLag = false;
        private Rectangle imgRect;
		private Rectangle imgRectBak;

		//**************************************************************************
		public string FileName
		{
			get { return m_Path; }
		}
        public float Ratio
        {
            get { return m_ratio; }
            set { SetRatio(value); }
        }
		//**************************************************************************
		public PictureView()
        {
            chkSize();
            //ダブルバッファー表示
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

        }
        //**************************************************************************
        public Point ToOff(Point p)
        {
            int x = imgRect.Left + (int)((float)p.X / m_ratio);
            int y = imgRect.Top + (int)((float)p.Y / m_ratio);
            return new Point(x, y);
        }
        //**************************************************************************
        public Rectangle ClipRect(Point p, float r)
        {
            int w = (int)Math.Round(m_OffScr.Width * r);
            int h = (int)Math.Round(m_OffScr.Height * r);

            int l = p.X - w / 2;
            int t = p.Y - h / 2;
            return new Rectangle(l, t, w, h);

        }
        //**************************************************************************
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush b = new SolidBrush(Color.Gray);
            try
            {
				if (m_ratio < 1)
				{
					g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				}
				else
				{
					g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				}
                g.FillRectangle(b, new Rectangle(0, 0, this.Width, this.Height));
                if (m_OffScr != null)
                {
                    //画像を指定された範囲に描画する
                    g.DrawImage(m_OffScr, imgRect);
                }
            }
            finally
            {
                b.Dispose();
            }

        }
		//**************************************************************************
		public void calcImgRect(int x, int y,float r)
		{
			//クリックされた位置を画像上の位置に変換
			Point imgPoint = new Point(
				(int)Math.Round((x - imgRect.X) / m_ratio),
				(int)Math.Round((y - imgRect.Y) / m_ratio));

			//倍率を変更する
			m_ratio = r;

			//画像の表示範囲を計算する
			imgRect.Width = (int)Math.Round(m_OffScr.Width * m_ratio);
			imgRect.Height = (int)Math.Round(m_OffScr.Height * m_ratio);
			imgRect.X = (int)Math.Round(this.Width / 2 - imgPoint.X * m_ratio);
			imgRect.Y = (int)Math.Round(this.Height / 2 - imgPoint.Y * m_ratio);

			this.Invalidate();

		}
		//**************************************************************************
		public void SetRatio(float r)
		{
			calcImgRect(this.Width / 2, this.Height / 2, r);

		}
		//**************************************************************************
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
			if (mdFLag == true) return;
            mdFLag = true;
            mdPos = e.Location;
			imgRectBak = imgRect;
        }
        //**************************************************************************
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
			
			if (mdFLag == false) return;
			imgRect.X = imgRectBak.X + e.X - mdPos.X;
			imgRect.Y = imgRectBak.Y + e.Y - mdPos.Y;
			if ((imgRect.X + imgRect.Width) <= 8) { imgRect.X = -imgRect.Width + 8; }
			else if (imgRect.X >= (this.Width - 8)) { imgRect.X = (this.Width - 8); }
			if ((imgRect.Y + imgRect.Height) <= 8) { imgRect.Y = -imgRect.Height + 8; }
			else if (imgRect.Y >= (this.Height - 8)) { imgRect.Y = (this.Height - 8); }
			this.Invalidate();
        }
        //**************************************************************************
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (mdFLag == false) return;
			imgRect.X = imgRectBak.X + e.X - mdPos.X;
			imgRect.Y = imgRectBak.Y + e.Y - mdPos.Y;
			if ((imgRect.X + imgRect.Width) <= 8) { imgRect.X = -imgRect.Width + 8; }
			else if (imgRect.X >= (this.Width - 8)) { imgRect.X = (this.Width - 8); }
			if ((imgRect.Y + imgRect.Height) <= 8) { imgRect.Y = -imgRect.Height + 8; }
			else if (imgRect.Y >= (this.Height - 8)) { imgRect.Y = (this.Height - 8); }
			mdFLag = false;
            this.Invalidate();
        }
            
        //**************************************************************************
        private void chkSize()
        {
			imgRect.Width = (int)Math.Round(m_OffScr.Width * m_ratio);
			imgRect.Height = (int)Math.Round(m_OffScr.Height *m_ratio);
        }
        //**************************************************************************
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
			this.Invalidate();
        }
        //**************************************************************************
        public void OpenFile(string path)
        {
			try
			{
				Targa tga = new Targa();
				if (tga.LoadHeader(path) == true)
				{
					m_OffScr = tga.loadTGA(path);
				}
				else
				{
					m_OffScr = new Bitmap(path);
				}
				chkSize();
				m_Path = path;
				this.Invalidate();
			}
			catch
			{
				m_Path = "";
				MessageBox.Show("Open error!");
			}
        }
        public void SetOffScr(Bitmap? bmp)
        {
            if (bmp == null) return;

        }
        //**************************************************************************
        public void ClearPicture()
        {
            if (m_OffScr == null) return;
            m_OffScr.Dispose();
            m_OffScr = null;
            m_Path = "";
        }
 
        //**************************************************************************
  
    }
}
