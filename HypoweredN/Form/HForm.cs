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
	public partial class HForm : BaseForm
	{
		#region Props
		private MainForm? m_MainForm = null;
		[Category("Hypowered"), Browsable(false)]
		public MainForm? MainForm
		{
			get { return m_MainForm; }
		}
		public void SetMainForm(MainForm mf)
		{
			m_MainForm = mf;
		}
		[Category("Hypowered")]
		public int Index { get; set; } = -1;


		[Category("Hypowered_Menu")]
		public HMainMenu MainMenu { get; set; } = new HMainMenu();
		[Category("Hypowered_Menu")]
		public HMenuItem FileMenu { get; set; } = new HMenuItem();
		[Category("Hypowered_Menu")]
		public HMenuItem EditMenu { get; set; } = new HMenuItem();
		[Category("Hypowered_Menu")]
		public HMenuItem ToolMenu { get; set; } = new HMenuItem();

		[Category("Hypowered_Menu")]
		public HMenuItem NewMenu { get; set; } = new HMenuItem();
		[Category("Hypowered_Menu")]
		public HMenuItem OpenMenu { get; set; } = new HMenuItem();
		[Category("Hypowered_Menu")]
		public HMenuItem CloseMenu { get; set; } = new HMenuItem();
		[Category("Hypowered_Menu")]
		public HMenuItem MainFormMenu { get; set; } = new HMenuItem();



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
		public void ClearIsEdits()
		{
			if (this.Controls.Count <= 0) return;
			for (int i = 0; i < this.Controls.Count; i++)
			{
				if (this.Controls[i] is HControl)
				{
					((HControl)this.Controls[i]).IsEdit = false;
				}
			}
		}
		public void SetIsEdits(int[] b)
		{
			if(this.Controls.Count<=0) return;
			bool [] bb = new bool[this.Controls.Count];
			for (int i = 0; i < this.Controls.Count; i++) bb[i] = false;
			if(b.Length>0)
			{
				for (int i = 0; i < b.Length; i++)
				{
					int ii = b[i];
					if ((ii>=0) && (ii< this.Controls.Count))
					{
						bb[ii] = true;
					}
				}
			}
			for (int i = 0; i < this.Controls.Count; i++)
			{
				if (this.Controls[i] is HControl)
				{
					((HControl)this.Controls[i]).IsEdit = bb[i];
				}
			}

		}
		// ************************************************************
		public HForm() : base()
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
		}
		// ************************************************************
		private void InitMenuStrip()
		{
			MainMenu.Name = "MainManu";
			MainMenu.Text = "Main";
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

			NewMenu.Name = "NewMenu";
			NewMenu.Text = "NewForm";
			NewMenu.Click += (sender, e) => { if (m_MainForm != null) m_MainForm.AddFrom(); };

			OpenMenu.Name = "OpenMenu";
			OpenMenu.Text = "Open";

			CloseMenu.Name = "CloseMenu";
			CloseMenu.Text = "Close";
			CloseMenu.Click += (sender, e) => { this.Close(); };

			MainFormMenu.Name = "MainFormMenu";
			MainFormMenu.Text = "Main";
			MainFormMenu.Click += (sender, e) =>
			{
				if (m_MainForm != null)
				{
					m_MainForm.Visible = true;
					m_MainForm.Activate();
				}
			};

			FileMenu.DropDownItems.Add(NewMenu);
			FileMenu.DropDownItems.Add(OpenMenu);
			FileMenu.DropDownItems.Add(CloseMenu);

			ToolMenu.DropDownItems.Add(MainFormMenu);

			MainMenu.Items.Add(FileMenu);
			MainMenu.Items.Add(EditMenu);
			MainMenu.Items.Add(ToolMenu);

			this.Controls.Add(MainMenu);
		}
		// ************************************************************
		public void AddControl(HType ht, string nm)
		{
			HControl hc;
			switch (ht)
			{
				case HType.Button:
					hc = new HButton();
					hc.Location = new Point(50, 120);
					hc.Size = new Size(75, 25);
					hc.Name = nm;
					hc.Text = nm;
					break;
				default:
					return;
			}
			this.Controls.Add(hc);
			ChkControl();
		}
		public void AddControl()
		{
			using (AddControlDialog dlg = new AddControlDialog())
			{
				dlg.TopMost = this.TopMost;
				dlg.HForm = this;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					AddControl(dlg.HType, dlg.CName);
				}
			}

		}
		public void ChkControl()
		{
			if(this.Controls.Count > 0)
			{
				int idx = 0;
				foreach(Control c in this.Controls)
				{
					if(c is HMainMenu)
					{
						((HMainMenu)c).Index = idx;
					}else if(c is HControl)
					{
						((HControl)c).Index = idx;
					}
					idx++;
				}
			}
		}
		public string[] ControlList()
		{
			List<string> list = new List<string>();
			if(this.Controls.Count > 0)
			{
				foreach(Control c in this.Controls)
				{
					if ((c is HMainMenu)|| (c is HControl))
					{
						list.Add(c.Name);
					}
				}
			}
			return list.ToArray();
		}
		public Control[] ControlArray()
		{
			List<Control> list = new List<Control>();
			if (this.Controls.Count > 0)
			{
				foreach (Control c in this.Controls)
				{
					if ((c is HMainMenu) || (c is HControl))
					{
						list.Add(c);
					}
				}
			}
			return list.ToArray();
		}
	}
}
