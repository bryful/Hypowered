using System.ComponentModel;
namespace Hypowered
{
	public partial class MainForm : BaseForm
	{
		public ItemsLib ItemsLib { get; set; } = new ItemsLib();
		// ********************************************************************
		#region Event
		// ********************************************************************
		public delegate void TargetFormChangedHandler(object sender, EventArgs e);
		public event TargetFormChangedHandler? TargetFormChanged;
		protected virtual void OnTargetFormChanged(EventArgs e)
		{
			if (TargetFormChanged != null)
			{
				TargetFormChanged(this, e);
			}
		}
		public delegate void FormChangedHandler(object sender, EventArgs e);
		public event FormChangedHandler? FormChanged;
		protected virtual void OnFormChanged(EventArgs e)
		{
			if (FormChanged != null)
			{
				FormChanged(this, e);
			}
		}
		#endregion
		// ********************************************************************
		#region Prop
		// ********************************************************************
		private EditControl? m_EditControl = null;
		[Category("Hypowered_Ctrl")]
		public EditControl? EditControl
		{
			get { return m_EditControl; }
			set
			{
				m_EditControl = value;
				if (m_EditControl != null)
				{
					m_EditControl.SetMainForm(this);
				}
			}
		}

		// ********************************************************************
		private HForm? m_TargetForm = null;
		public HForm? TargetForm { get { return m_TargetForm; } }
		public void SetTargetForm(HForm? fm)
		{
			bool b = (m_TargetForm != fm);

			m_TargetForm = fm;
			if (m_TargetForm != null)
			{
				this.Text = $"Hypowered [{m_TargetForm.Name}]";
				m_TargetForm.Activate();
			}
			else
			{
				this.Text = $"Hypowered MainForm";
			}
			if (b) OnTargetFormChanged(new EventArgs());
		}
		public void SetTargetForm(int idx)
		{
			if ((idx >= 0) && (idx < HForms.Count))
			{
				if (m_TargetForm != null)
				{
					if (m_TargetForm.Index == idx) return;
				}
				SetTargetForm(HForms[idx]);
			}
			else
			{
				SetTargetForm(null);
			}
		}
		#endregion
		// ********************************************************************
		#region Server
		// ********************************************************************
		private F_Pipe m_Server = new F_Pipe();
		public void StartServer(string pipename)
		{
			m_Server.Server(pipename);
			m_Server.Reception += (sender, e) =>
			{
				this.Invoke((Action)(() =>
				{
					PipeData pd = new PipeData(e.Text);
					Command(pd.Args, PIPECALL.PipeExec);
					this.Activate();
				}));
			};
		}
		// ********************************************************************
		public void StopServer()
		{
			m_Server.StopServer();
		}
		#endregion
		// ********************************************************************
		public List<HForm> HForms = new List<HForm>();
		public void RescanForms()
		{
			if (HForms.Count > 0)
			{
				for (int i = 0; i < HForms.Count; i++)
				{
					HForms[i].Index = i;
				}
			}
		}
		// ********************************************************************
		private int m_AddFromCount = 0;
		public void AddFrom()
		{
			HForm form = new HForm();
			form.SetMainForm(this);
			form.Index = HForms.Count;
			form.Name = $"Form{m_AddFromCount}";
			form.Text = $"Form{m_AddFromCount}";
			m_AddFromCount++;
			form.FormClosed += (sender, e) =>
			{
				if (sender is HForm)
				{
					HForm hf = ((HForm)sender);
					int idx = hf.Index;
					bool b = (m_TargetForm == hf);
					if ((idx >= 0) && (idx < HForms.Count))
					{
						HForms.RemoveAt(idx);
					}
					RescanForms();
					if (b) SetTargetForm(null);
					if ((HForms.Count <= 0) && (this.Visible == false))
					{
						Application.Exit();
					}
					OnFormChanged(new EventArgs());
				}
			};
			form.Activated += (sender, e) =>
			{
				if (sender is HForm)
				{
					HForm mf = ((HForm)sender);
					SetTargetForm(mf);
				}
			};
			form.Show(this);
			HForms.Add(form);
			SetTargetForm(form);
			OnFormChanged(new EventArgs());
		}
		// ********************************************************************
		public void AddControl()
		{
			if (m_TargetForm == null) return;
			m_TargetForm.AddControl();
		}
		// ********************************************************************
		public MainForm()
		{
			string? d = Path.GetDirectoryName(Application.ExecutablePath);
			string? n = Path.GetFileNameWithoutExtension(Application.ExecutablePath);

			ItemsLib.Setup(Path.Combine(d, n));
			this.AllowDrop = true;
			InitializeComponent();
			BackColor = Color.FromArgb(64, 64, 64);
			ForeColor = Color.FromArgb(230, 230, 230);
			this.DoubleBuffered = true;
			base.AutoScaleMode = AutoScaleMode.None;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw,
				true);
			this.UpdateStyles();
			EditControl = editControl1;


			this.FormClosed += (sender, e) => { LastSettings(); };
			StartSettings();
			ControlLayout();
			Command(Environment.GetCommandLineArgs().Skip(1).ToArray(), PIPECALL.StartupExec);


			ItemsLib.Beep();
			string s = ItemsLib.GetItemNamesS();
			Clipboard.SetText(Clipboard.GetText() + "***********\r\n" + s);
		}
		// **********************************************************
		private void StartSettings()
		{
			PrefFile pf = new PrefFile(this);
			pf.Load();
			Rectangle? rect = pf.GetBounds();
		}
		// **********************************************************
		private void LastSettings()
		{
			PrefFile pf = new PrefFile(this);
			pf.SetBounds();
			pf.Save();
		}
		// **********************************************************
		/// <summary>
		/// コマンド解析
		/// </summary>
		/// <param name="args">コマンド配列</param>
		/// <param name="IsPipe">起動時かダブルクリック時か判別</param>
		public void Command(string[] args, PIPECALL IsPipe = PIPECALL.StartupExec)
		{
			string ret = "";
			if (args.Length > 0)
			{
				foreach (string arg in args)
				{
					if (ret != "") ret += ",\r\n";
					ret += arg;
				}
			}
			//textBox1.Text = ret;
		}
		public void ControlLayout()
		{
			int y = 0;
			if (menuStrip1 != null)
			{
				menuStrip1.Location = new Point(0, m_BarHeight);
				menuStrip1.Size = new Size(this.Width, menuStrip1.Height);
				y += menuStrip1.Bottom + 2;
			}
			if (splitContainer1 != null)
			{
				splitContainer1.Location = new Point(0, y);
				splitContainer1.Size = new Size(this.Width, this.Height - y - 20);
			}
		}
		// ************************************************************
		protected override void OnResize(EventArgs e)
		{
			//base.OnResize(e);
			CalcCloseRect();
			ControlLayout();
			this.Refresh();
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
				Rectangle rct = new Rectangle(0, 0, this.Width, m_BarHeight);
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
				// TopBar Title
				rct = new Rectangle(m_TopMostRect.Right + 2, 0,
					this.Width - m_TopMostRect.Right, m_BarHeight);
				sb.Color = ForeColor;
				g.DrawString(this.Text, this.Font, sb, rct, SFormat);
				// TopBar Close
				p.Color = ForeColor;
				g.DrawRectangle(p, m_CloseRect);
				g.DrawLine(p, m_CloseRect.Left, m_CloseRect.Top, m_CloseRect.Right, m_CloseRect.Bottom);
				g.DrawLine(p, m_CloseRect.Left, m_CloseRect.Bottom, m_CloseRect.Right, m_CloseRect.Top);

				// 外枠
				rct = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
				p.Color = m_BarBackColor;
				g.DrawRectangle(p, rct);

			}

			//base.OnPaint(e);
		}
		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			if ((drgevent != null) && (drgevent.Data != null))
			{
				if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
				{
					drgevent.Effect = DragDropEffects.Copy;
				}
			}
			else
			{
				base.OnDragEnter(drgevent);
			}
		}
		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			if ((drgevent != null) && (drgevent.Data != null))
			{
				if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
				{

					// ドラッグ中のファイルやディレクトリの取得
					string[] drags = (string[])drgevent.Data.GetData(DataFormats.FileDrop);
					Command(drags);
					/*
					foreach (string d in drags)
					{
						if (!System.IO.File.Exists(d))
						{
						}
						else if (!System.IO.Directory.Exists(d))
						{
						}
					}
					*/
					drgevent.Effect = DragDropEffects.Copy;
				}
			}
			else
			{
				base.OnDragDrop(drgevent);
			}
		}
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (Visible == false)
			{
				if (HForms.Count <= 0) { Application.Exit(); }
			}
		}
		private void newFormToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddFrom();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			AddControl();
		}

		private void quitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void editControl1_Click(object sender, EventArgs e)
		{

		}
		// **********************************************************
		public ItemName? ShowPictItemDialog(ItemsLib? il)
		{
			ItemName? ret = null;
			if (m_TargetForm == null) return ret;
			using (PictItemDialog dlg = new PictItemDialog())
			{
				dlg.SetMainForm(this);
				if (il == null) il = ItemsLib;
				dlg.SetItemsLib(il);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					ret = null;
				}
			}
			return ret;
		}
		// ************************************************************

	}
}