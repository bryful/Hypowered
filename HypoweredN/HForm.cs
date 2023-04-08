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
	public partial class HForm : Form
	{
		#region Props
		[Category("Hypowered")]
		public new System.String Name
		{
			get { return base.Name; }
			set { base.Name = value; }
		}
		[Category("Hypowered_Draw")]
		public new System.Int32 DeviceDpi
		{
			get { return base.DeviceDpi; }
		}
		[Category("Hypowered")]
		public new bool Enabled
		{
			get { return base.Enabled; }
			set { base.Enabled = value; }
		}
		[Category("Hypowered_Draw")]
		public new Font Font
		{
			get { return base.Font; }
			set { base.Font = value; }
		}

		[Category("Hypowered")]
		public new System.Object Tag
		{
			get { return base.Tag; }
			set { base.Tag = value; }
		}
		[Category("Hypowered")]
		public new bool Visible
		{
			get { return base.Visible; }
			set { base.Visible = value; }
		}
		[Category("Hypowered_Size")]
		public new Point Location
		{
			get { return base.Location; }
			set { base.Location = value; }
		}
		[Category("Hypowered_Size")]
		public new Size MaximumSize
		{
			get { return base.MaximumSize; }
			set { base.MaximumSize = value; }
		}
		[Category("Hypowered_Size")]
		public new Size MinimumSize
		{
			get { return base.MinimumSize; }
			set { base.MinimumSize = value; }
		}
		[Category("Hypowered_Size")]
		public new System.Drawing.Size PreferredSize
		{
			get { return base.PreferredSize; }
		}
		[Category("Hypowered_Size")]
		public new System.Windows.Forms.Padding Padding
		{
			get { return base.Padding; }
			set { base.Padding = value; }
		}
		[Category("Hypowered")]
		public new bool TopMost
		{
			get { return base.TopMost; }
			set { base.TopMost = value; this.Invalidate(); }
		}


		[Category("Hypowered_Size")]
		public int BarHeight
		{
			get { return m_BarHeight; }
			set
			{
				m_BarHeight = value;
				int w = m_BarHeight - 8;
				m_TopMostRect = new Rectangle(10, 4, w, w);
				CalcCloseRect();
			}
		}
		private Color m_BarBackColor = Color.FromArgb(80, 80, 80);
		[Category("Hypowered_Color"),Browsable(true)]
		public Color BarBackColor
		{
			get { return m_BarBackColor; }
			set { m_BarBackColor = value; this.Invalidate(); }
		}
		[Category("Hypowered_Color")]
		public new System.Drawing.Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; }
		}
		[Category("Hypowered_Color")]
		public new System.Drawing.Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}

		[Category("Hypowered_Menu")]
		public HMainMenu MainMenu { get;  set; } = new HMainMenu();
		[Category("Hypowered_Menu")]
		public ToolStripMenuItem FileMenu { get; set; } = new ToolStripMenuItem();
		[Category("Hypowered_Menu")]
		public ToolStripMenuItem EditMenu { get; set; } = new ToolStripMenuItem();
		[Category("Hypowered_Menu")]
		public ToolStripMenuItem ToolMenu { get; set; } = new ToolStripMenuItem();
		[Category("Hypowered_Menu")]
		public bool MainMenuVisible
		{
			get { return MainMenu.Visible; }
			set { MainMenu.Visible = value; }
		}
		[Category("Hypowered_Draw")]
		public new double Opacity
		{
			get { return base.Opacity; }
			set { base.Opacity = value; }
		}
		[Category("Hypowered_Draw")]
		public new bool DoubleBuffered
		{
			get { return base.DoubleBuffered; }
			set { base.DoubleBuffered = value; }
		}

		#endregion

		private int m_BarHeight = 20;
		private Rectangle m_TopMostRect = new Rectangle(10, 4, 12, 12);
		private Rectangle m_CloseRect = new Rectangle(10, 4, 12, 12);
		private void CalcCloseRect()
		{
			int w = m_BarHeight - 8;
			m_CloseRect = new Rectangle(this.Width - w -10, 4, w, w);

		}
		// ************************************************************
		public HForm()
		{
			InitializeComponent();
			base.BackColor = Color.FromArgb(64, 64, 64);
			base.ForeColor = Color.FromArgb(230, 230, 230);
			this.DoubleBuffered = true;
			base.AutoScaleMode = AutoScaleMode.None;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw,
				true);
			this.UpdateStyles();

			InitMenuStrip();


			//HUtils.PropListToClipboard(typeof(HForm),"HForm");
		}
		// ************************************************************
		private void InitMenuStrip()
		{
			MainMenu.AutoSize = false;
			MainMenu.Dock = DockStyle.None;
			MainMenu.Anchor = AnchorStyles.None;
			MainMenu.Location = new Point(0, m_BarHeight);
			MainMenu.Size = new Size(this.Width, MainMenu.Height);

			FileMenu.Name = "FileMenu";
			FileMenu.Text = "File";
			EditMenu.Name = "EditMenu";
			EditMenu.Text = "Edit";
			ToolMenu.Name = "ToolMenu";
			ToolMenu.Text = "Tool";

			MainMenu.Items.Add(FileMenu);
			MainMenu.Items.Add(EditMenu);
			MainMenu.Items.Add(ToolMenu);

			this.Controls.Add(MainMenu);
		}
		// ************************************************************
		protected override void OnResize(EventArgs e)
		{
			MainMenu.Location = new Point(0, m_BarHeight);
			MainMenu.Size = new Size(this.Width, MainMenu.Height);
			CalcCloseRect();
			base.OnResize(e);
		}
		// ************************************************************
		protected override void OnPaint(PaintEventArgs e)
		{
			using (SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = e.Graphics;
				g.FillRectangle(sb, this.ClientRectangle);
				// TopBar
				Rectangle rct = new Rectangle(0,0,this.Width,m_BarHeight);
				sb.Color = m_BarBackColor;
				g.FillRectangle(sb, rct);
				// TopBar TopMost
				if (this.TopMost)
				{
					sb.Color = ForeColor;
					g.FillRectangle(sb, m_TopMostRect);
				}
				else
				{
					p.Color = ForeColor;
					g.DrawRectangle(p, m_TopMostRect);
				}
				// TopBar Close
				p.Color = ForeColor;
				g.DrawRectangle(p, m_CloseRect);
				g.DrawLine(p, m_CloseRect.Left, m_CloseRect.Top, m_CloseRect.Right, m_CloseRect.Bottom);
				g.DrawLine(p, m_CloseRect.Left, m_CloseRect.Bottom, m_CloseRect.Right, m_CloseRect.Top);



			}

			base.OnPaint(e);
		}
	}
}
