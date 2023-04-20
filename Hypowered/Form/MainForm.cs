using System.ComponentModel;
using System.Diagnostics;

namespace Hypowered
{
	public partial class MainForm : BaseForm
	{
		static public readonly string HOME_ENV = "HypoweredHome";
		static public readonly string HOME_NAME = "Home";
		static public readonly string DefEXT = ".hypf";
		static public readonly string HYPF_JSON = "hypf.json";
		// ********************************************************************
		private string m_HomeFolder = string.Empty;
		public string HomeFolder { get { return m_HomeFolder; } }
		public void GetHomeFolder()
		{
			string? p = Environment.GetEnvironmentVariable(HOME_ENV,
							  EnvironmentVariableTarget.User);
			if ((p != null) && (Directory.Exists(p)))
			{
				m_HomeFolder = p;
			}
			else
			{
				string p2 = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Hypowered";
				lock (typeof(Application))
				{
					if (!Directory.Exists(p2))
					{
						Directory.CreateDirectory(p2);
					}
				}
				m_HomeFolder = p2;
				Environment.SetEnvironmentVariable(
					HOME_ENV,
					m_HomeFolder,
					EnvironmentVariableTarget.User);
			}
		}
		static public string GetFileSystemPath(Environment.SpecialFolder folder)
		{
			// パスを取得
			string path = $"{Environment.GetFolderPath(folder)}\\"
				+ $"{Application.CompanyName}\\"
				+ $"{Application.ProductName}";

			// パスのフォルダを作成
			lock (typeof(Application))
			{
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
			}
			return path;
		}
		// ********************************************************************
		public ItemsLib ItemsLib = new ItemsLib();
		public ConsoleForm? ConsoleForm = null;
		public ScriptEditor? ScriptForm = null;
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
		public int TargetFormIndex
		{
			get
			{
				int ret = -1;
				if (m_TargetForm != null) ret = m_TargetForm.Index;
				return ret;
			}
			set
			{
				SetTargetForm(value);
			}
		}
		public void SetTargetForm(HForm? fm)
		{
			if ((fm != null) && (fm.CanPropertyGrid == false))
			{
				fm = null;
			}

			bool b = (m_TargetForm != fm);

			m_TargetForm = fm;
			if (m_TargetForm != null)
			{
				m_TargetControl = m_TargetForm.TargetControl;
				this.Text = $"Hypowered [{m_TargetForm.Name}]";
				m_TargetForm.Activate();
			}
			else
			{
				m_TargetControl = null;
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

		private HControl? m_TargetControl = null;
		public HControl? TargetControl { get { return m_TargetControl; } }
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
		public void OpenForm()
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.Title = "Open Form";
				dlg.Filter = "*.hypf|*.hypf|*.*|*.*";
				dlg.InitialDirectory = m_HomeFolder;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					if (File.Exists(dlg.FileName))
					{
						OpenForm(dlg.FileName);
					}
				}
			}
		}
		// ********************************************************************
		public void RenameForm()
		{
			if (TargetForm == null) return;
			using (RenameFormDialog dlg = new RenameFormDialog())
			{
				dlg.TopMost = this.TopMost;
				dlg.FormName = TargetForm.ItemsLib.Name;
				dlg.SetHForm(TargetForm);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					TargetForm.ItemsLib.Rename(dlg.FormName);
					base.Name = TargetForm.ItemsLib.Name;
					OnFormChanged(new EventArgs());
				}
			}
		}
		// ********************************************************************
		public HForm? IndexOfHForm(string p)
		{
			HForm? ret = null;
			if (HForms.Count > 0)
			{
				foreach (HForm hf in HForms)
				{
					if (hf.ItemsLib.FileName == p)
					{
						ret = hf;
						break;
					}
				}
			}
			return ret;
		}
		// ********************************************************************
		public bool OpenForm(string p)
		{
			bool ret = false;
			if (File.Exists(p))
			{
				HForm? hf0 = IndexOfHForm(p);
				if (hf0 != null)
				{
					SetTargetForm(hf0);
				}
				else
				{
					HForm hf = CreateForm(p);
					hf.ImportFromHypf();
					SetTargetForm(hf);
					hf.StartSettings();

				}
				OnFormChanged(new EventArgs());
				ret = true;
			}

			return ret;
		}
		// ********************************************************************
		private void OutputDefHypf(string p)
		{
			File.WriteAllBytes(p, Properties.Resources.hypfdef);
		}
		private int m_AddFormCount = 0;
		// ********************************************************************
		public void NewForm()
		{
			using (CreateFormDialog dlg = new CreateFormDialog())
			{
				dlg.TopMost = this.TopMost;
				dlg.FullFormName = $"{m_HomeFolder}\\Form{m_AddFormCount}{DefEXT}";
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					m_AddFormCount++;
					OutputDefHypf(dlg.FullFormName);
					HForm hf = CreateForm(dlg.FullFormName, dlg.FormSize);
					SetTargetForm(hf);
					OnFormChanged(new EventArgs());
				}
			}

		}
		// ********************************************************************
		public HForm CreateForm(string path, Size? sz = null)
		{
			HForm form = new HForm();
			form.SetMainForm(this);
			form.Index = HForms.Count;
			if (sz != null) form.Size = (Size)sz;
			form.ItemsLib.Setup(path);
			form.Name = form.ItemsLib.Name;
			form.Text = form.Name;
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
			form.FormNameChanged += (sender, e) =>
			{
				HForm mf = ((HForm)sender);
			};
			form.Show(this);
			HForms.Add(form);
			RescanForms();
			return form;
		}
		// ********************************************************************
		public void CloseForm()
		{
			if (m_TargetForm != null)
			{
				m_TargetForm.Close();
				RescanForms();
				OnFormChanged(new EventArgs());
			}
		}
		// ********************************************************************
		public void AddControl()
		{
			if (m_TargetForm == null) return;
			m_TargetForm.AddControl();
		}
		// ********************************************************************
		public bool DeleteControl()
		{
			bool ret = false;
			if (m_TargetForm != null)
			{
				if (m_TargetForm.TargetControl != null)
				{
					ret = m_TargetForm.RemoveControl();
				}
			}
			return ret;
		}
		// ********************************************************************
		public MainForm()
		{
			GetHomeFolder();

			// ItemsLibの読み込み
			ItemsLib.Setup(Path.ChangeExtension(Application.ExecutablePath, DefEXT));
			//ItemsLib.Aarchive();

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
			openFormMenu.Click += (sender, e) => { OpenForm(); };
			newFormMenu.Click += (sender, e) => { NewForm(); };
			closeFormMenu.Click += (sender, e) => { CloseForm(); };
			deleteControlMenu.Click += (sender, e) => { DeleteControl(); };
			quitMenu.Click += (sender, e) => { Application.Exit(); };
			consoleMenu.Click += (sender, e) => { ShowConsole(); };
			scriptEditorMenu.Click += (sender, e) => { ShowScript(); };
			Command(Environment.GetCommandLineArgs().Skip(1).ToArray(), PIPECALL.StartupExec);
			//PUtil.ToJsonCodeToClipboard(typeof(HTextBox));
			//PUtil.PropListToClipboard(typeof(EditTextBox),"Edit");
			ItemsLib.Beep();
		}
		// **********************************************************
		private void StartSettings()
		{
			PrefFile pf = new PrefFile(this, Application.ExecutablePath);
			pf.Load();
			Rectangle? rect = pf.GetBounds();
			object? obj = null;
			obj = pf.JsonFile.ValueInt("SplitterDistance");
			if (obj != null) splitContainer1.SplitterDistance = (int)obj;
			obj = pf.JsonFile.ValueInt("AddFormCount");
			if (obj != null) m_AddFormCount = (int)obj;
			if (m_AddFormCount > 100) m_AddFormCount = 0;
		}
		// **********************************************************
		private void LastSettings()
		{
			PrefFile pf = new PrefFile(this, Application.ExecutablePath);
			pf.SetBounds();
			pf.JsonFile.SetValue("SplitterDistance", splitContainer1.SplitterDistance);
			pf.JsonFile.SetValue("AddFormCount", m_AddFormCount);
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

		// **********************************************************
		public string ShowPictItemDialog(HForm hf, string pn = "")
		{
			string ret = "";
			if (m_TargetForm == null) return ret;

			using (PictItemDialog dlg = new PictItemDialog())
			{

				dlg.SetMainForm(this);
				dlg.SetMainItemsLib(this.ItemsLib);
				dlg.SetFormItemsLib(m_TargetForm.ItemsLib);
				if (pn != "") dlg.PictName = pn;
				dlg.TopMost = m_TargetForm.TopMost;
				if (dlg.ShowDialog(hf) == DialogResult.OK)
				{
					ret = dlg.PictName;
				}
			}
			return ret;
		}
		// ************************************************************
		public void Alert(HForm hf, object? obj, string cap = "")
		{
			if (m_TargetForm == null) return;

			using (AlertForm dlg = new AlertForm())
			{
				if (cap != "") dlg.Title = cap;
				dlg.Text = HUtils.ToStr(obj);
				dlg.TopMost = hf.TopMost;
				if (dlg.ShowDialog(hf) == DialogResult.OK)
				{
				}
			}
		}
		// ************************************************************
		public void ShowConsole()
		{
			if (ConsoleForm == null)
			{
				ConsoleForm = new ConsoleForm();
				ConsoleForm.SetMainForm(this);
				ConsoleForm.Show(this);
			}

			if (ConsoleForm.Visible == false)
			{
				ConsoleForm.Visible = true;
			}
			ConsoleForm.Activate();
		}
		// ************************************************************
		public void ShowScript()
		{
			if (ScriptForm == null)
			{
				ScriptForm = new ScriptEditor();
				ScriptForm.SetMainForm(this);
				ScriptForm.Show(this);
			}

			if (ScriptForm.Visible == false)
			{
				ScriptForm.Visible = true;
			}
			ScriptForm.Activate();
		}
	}
}