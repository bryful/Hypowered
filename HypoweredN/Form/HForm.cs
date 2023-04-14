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
		#region Event
		public delegate void ControlChangedHandler(object sender, EventArgs e);
		public event ControlChangedHandler? ControlChanged;
		protected virtual void OnControlChanged(EventArgs e)
		{
			if (ControlChanged != null)
			{
				ControlChanged(this, e);
			}
		}
		#endregion
		#region Props
		// ******************
		private string m_FileName = string.Empty;

		public ItemsLib ItemsLib { get; set; } = new ItemsLib();

		public Bitmap? GetBitmapFromLib(string nm)
		{
			Bitmap? ret = null;
			if((ItemsLib.Enabled)&&(ItemsLib.ItemNamesCount>0))
			{
				ret = ItemsLib.GetBitmap(nm);
			}
			if((ret== null)&&(MainForm!=null))
			{
				ret = MainForm.ItemsLib.GetBitmap(nm);
			}
			return ret;
		}
		// ******************
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
		public HMenuItem OpenMenu { get; set; } = new HMenuItem();
		[Category("Hypowered_Menu")]
		public HMenuItem CloseMenu { get; set; } = new HMenuItem();
		[Category("Hypowered_Menu")]
		public HMenuItem MainFormMenu { get; set; } = new HMenuItem();
		[Category("Hypowered_Menu")]
		public HMenuItem PictItemDialogMenu { get; set; } = new HMenuItem();



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
			if (this.Controls.Count <= 0) return;
			bool[] bb = new bool[this.Controls.Count];
			for (int i = 0; i < this.Controls.Count; i++) bb[i] = false;
			if (b.Length > 0)
			{
				for (int i = 0; i < b.Length; i++)
				{
					int ii = b[i];
					if ((ii >= 0) && (ii < this.Controls.Count))
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
			PUtil.ToJsonCodeToClipboard(typeof(HForm));
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
			PictItemDialogMenu.Name = "PictDialogMenu";
			PictItemDialogMenu.Text = "PictDialog";
			PictItemDialogMenu.Click += (sender, e) =>
			{
				if (m_MainForm != null)
				{
					string name = m_MainForm.ShowPictItemDialog(null);
				}
			};

			FileMenu.DropDownItems.Add(OpenMenu);
			FileMenu.DropDownItems.Add(CloseMenu);

			ToolMenu.DropDownItems.Add(MainFormMenu);
			ToolMenu.DropDownItems.Add(PictItemDialogMenu);

			MainMenu.Items.Add(FileMenu);
			MainMenu.Items.Add(EditMenu);
			MainMenu.Items.Add(ToolMenu);

			this.Controls.Add(MainMenu);
		}
		// ************************************************************
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			MainMenu.Location = new Point(0, m_BarHeight);
			MainMenu.Size = new Size(this.Width, MainMenu.Height);
		}
		// ************************************************************
		public int IndexOfControl(string key)
		{
			return this.Controls.IndexOfKey(key);
		}
		// ************************************************************
		public string ControlNewName(HType ht)
		{
			string? nm = Enum.GetName(typeof(HType), ht);
			if (nm == null) nm = "ctrl";
			int idx = 1;
			string s = $"{nm}{idx}";
			while (this.Controls.IndexOfKey(s) >= 0)
			{
				s = $"{nm}{idx}";
				idx++;
			};
			return s;
		}
		private Point pDef = new Point(100, 100);
		// ************************************************************
		public void AddControl(HType ht, string nm, string tx)
		{
			HControl hc;
			switch (ht)
			{
				case HType.Button:
					hc = new HButton();
					hc.Location = pDef;
					hc.Size = new Size(75, 25);
					hc.Name = nm;
					if (tx == "") tx = nm;
					hc.Text = tx;
					break;
				case HType.Label:
					hc = new HLabel();
					hc.Location = pDef;
					hc.Size = new Size(75, 25);
					hc.Name = nm;
					if (tx == "") tx = nm;
					hc.Text = tx;
					break;
				case HType.TextBox:
					hc = new HTextBox();
					hc.Location = pDef;
					hc.Size = new Size(75, 25);
					hc.Name = nm;
					if (tx == "") tx = nm;
					hc.Text = tx;
					break;
				case HType.PictureBox:
					hc = new HPictureBox();
					hc.Location = pDef;
					hc.Size = new Size(200, 200);
					hc.Name = nm;
					if (tx == "") tx = nm;
					hc.Text = tx;
					break;
				case HType.IconButton:
					hc = new HIconButton();
					hc.Location = pDef;
					hc.Name = nm;
					if (tx == "") tx = nm;
					hc.Text = tx;
					break;
				default:
					return;
			}
			this.Controls.Add(hc);
			this.Controls.SetChildIndex(hc, 1);
			ChkControl();
			OnControlChanged(new EventArgs());
			pDef.X += 10;
			if (pDef.X > 250) pDef.X = 100;
			pDef.Y += 10;
			if (pDef.Y > 250) pDef.Y = 100;
		}
		private HType m_HTypeDef = HType.Button;
		public void AddControl()
		{
			using (AddControlDialog dlg = new AddControlDialog())
			{
				dlg.HForm = this;
				dlg.TopMost = this.TopMost;
				dlg.Caption = "Add Control Dialog";
				dlg.HType = m_HTypeDef;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					AddControl(dlg.HType, dlg.CName, dlg.CText);
					m_HTypeDef = dlg.HType;
				}
			}

		}
		public void ChkControl()
		{
			if (this.Controls.Count > 0)
			{
				foreach (Control c in this.Controls)
				{
					if (c is HMainMenu)
					{
						if (this.Controls.GetChildIndex(c) != 0)
						{
							this.Controls.SetChildIndex(c, 0);
						}
					}
				}
				int idx = 0;
				foreach (Control c in this.Controls)
				{
					if (c is HMainMenu)
					{
						((HMainMenu)c).Index = idx;
					}
					else if (c is HControl)
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
			if (this.Controls.Count > 0)
			{
				foreach (Control c in this.Controls)
				{
					if ((c is HMainMenu) || (c is HControl))
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

		public void ControlUp(int[] sels)
		{
			if (sels.Length <= 0) return;
			if (sels[0] == 1) return;
			int idx = sels[0] - 1;
			for (int i = 0; i < sels.Length; i++)
			{
				this.Controls.SetChildIndex(
					this.Controls[sels[i]],
					idx
					);
				idx++;
			}
			OnControlChanged(EventArgs.Empty);
		}
		public void ControlTop(int[] sels)
		{
			if (sels.Length <= 0) return;
			if (sels[0] == 1) return;
			int idx = 1;
			for (int i = 0; i < sels.Length; i++)
			{
				this.Controls.SetChildIndex(
					this.Controls[sels[i]],
					idx
					);
				idx++;
			}
			OnControlChanged(EventArgs.Empty);
		}
		public void ControlDown(int[] sels)
		{
			if (sels.Length <= 0) return;
			if (sels[sels.Length - 1] == this.Controls.Count - 1) return;
			int idx = sels[sels.Length - 1] + 1;
			for (int i = sels.Length - 1; i >= 0; i--)
			{
				this.Controls.SetChildIndex(
					this.Controls[sels[i]],
					idx
					);
				idx--;
			}
			OnControlChanged(EventArgs.Empty);
		}
		public void ControlBottom(int[] sels)
		{
			if (sels.Length <= 0) return;
			if (sels[sels.Length - 1] == this.Controls.Count - 1) return;
			int idx = this.Controls.Count - 1;
			for (int i = sels.Length - 1; i >= 0; i--)
			{
				this.Controls.SetChildIndex(
					this.Controls[sels[i]],
					idx
					);
				idx--;
			}
			OnControlChanged(EventArgs.Empty);
		}
		public void ControlMove(int[] sels, ArrowDown ad, int MoveScale)
		{
			if (sels.Length <= 0) return;
			foreach (int sel in sels)
			{
				if ((sel > 0) && (sel < this.Controls.Count))
				{
					if (this.Controls[(int)sel] is HControl)
					{
						((HControl)this.Controls[(int)sel]).MovePos(ad, MoveScale);
					}
				}
			}
		}
		public void ControlResizeLeftTop(int[] sels, ArrowDown ad, int MoveScale)
		{
			if (sels.Length <= 0) return;
			foreach (int sel in sels)
			{
				if ((sel > 0) && (sel < this.Controls.Count))
				{
					if (this.Controls[(int)sel] is HControl)
					{
						((HControl)this.Controls[(int)sel]).ResizeLeftTop(ad, MoveScale);
					}
				}
			}
		}
		public void ControlResizeRightBottom(int[] sels, ArrowDown ad, int MoveScale)
		{
			if (sels.Length <= 0) return;
			foreach (int sel in sels)
			{
				if ((sel > 0) && (sel < this.Controls.Count))
				{
					if (this.Controls[(int)sel] is HControl)
					{
						((HControl)this.Controls[(int)sel]).ResizeRightBottom(ad, MoveScale);
					}
				}
			}
		}
	}
}
