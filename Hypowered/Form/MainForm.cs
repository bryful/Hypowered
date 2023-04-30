using System.ComponentModel;
using System.Diagnostics;

namespace Hypowered
{
	public partial class MainForm : BaseForm
	{
		// ********************************************************************
		#region Event
		// ********************************************************************
		public delegate void TargetFormChangedHandler(object sender, TargetFormChangedArgs e);
		public event TargetFormChangedHandler? TargetFormChanged;
		protected virtual void OnTargetFormChanged(TargetFormChangedArgs e)
		{
			if (TargetFormChanged != null)
			{
				TargetFormChanged(this, e);
			}
		}
		public delegate void TargetControlChangedHandler(object sender, TargetControlChangedArgs e);
		public event TargetControlChangedHandler? TargetControlChanged;
		protected virtual void OnTargetControlChanged(TargetControlChangedArgs e)
		{
			if (TargetControlChanged != null)
			{
				TargetControlChanged(this, e);
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
		public string HomeFilePath
		{
			get
			{
				return Path.Combine(m_HomeFolder, HOME_NAME + DefEXT);
			}
		}
		// ********************************************************************
		public ItemsLib ItemsLib = new ItemsLib();
		public ConsoleForm? ConsoleForm = null;

		// ********************************************************************
		#region Prop


		// ********************************************************************
		private HForm? m_TargetForm = null;
		public HForm? TargetForm
		{
			get { return m_TargetForm; }
			set
			{
				SetTargetForm(value);
			}
		}

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
				m_TargetForm.TargetControlChanged -= (sender, e) => { OnTargetControlChanged(e); };
				m_TargetForm.TargetControlChanged += (sender, e) => { OnTargetControlChanged(e); };
				m_TargetForm.IsEditChanged += (sender, e) => { IsEditoPropertyGrid(); };


				this.Text = $"Hypowered [{m_TargetForm.Name}]";
				m_TargetForm.Activate();
			}
			else
			{
				m_TargetControl = null;
				this.Text = $"Hypowered MainForm";
			}
			if (b) OnTargetFormChanged(new TargetFormChangedArgs(m_TargetForm));
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
		// *************************************************************
		public ScriptEditor? editor = null;
		protected bool m_IsScript = false;
		protected int m_ScriptWidth = 400;
		public int ScriptWidth
		{
			get { return m_ScriptWidth; }
			set
			{
				m_ScriptWidth = value;
				if (m_ScriptWidth < 200) m_ScriptWidth = 200;
			}
		}
		// *************************************************************
		[Category("_Hypowered"), Browsable(true)]
		public int ScriptEditorWidth
		{
			get { return m_ScriptWidth; }
			set
			{
				m_ScriptWidth = value;
				if (m_ScriptWidth < 200) m_ScriptWidth = 200;
			}
		}
		// *************************************************************
		[Category("_Hypowered"), Browsable(true)]
		public bool IsScript
		{
			get { return m_IsScript; }
			set
			{
				m_IsScript = value;
				if (m_IsScript)
				{
					if (editor == null)
					{
						using (WaitDialog dlg = new WaitDialog())
						{
							dlg.Location = System.Windows.Forms.Cursor.Position;
							dlg.Show(this);
							dlg.Refresh();
							editor = new ScriptEditor();
							editor.MainForm = this;
							editor.Location = new Point(splitLeft.Right + 2, splitLeft.Top);
							this.Controls.Add(editor);
							dlg.Close();
						}
					}
					this.Width = splitLeft.Width + 2 + m_ScriptWidth;
				}
				else
				{
					this.Width = splitLeft.Width;
				}
				ControlLayout();
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
		public string[] HFormsNames()
		{
			List<string> names = new List<string>();
			if (HForms.Count > 0)
			{
				foreach (HForm hf in HForms)
				{
					names.Add(hf.Name);
				}
			}
			return names.ToArray();
		}
		public void RescanForms()
		{
			if (HForms.Count > 0)
			{
				for (int i = 0; i < HForms.Count; i++)
				{
					HForms[i].Index = i;
				}
			}
			//hTreeView1.MainForm = this;
		}

		// ********************************************************************
		public new bool Visible
		{
			get { return base.Visible; }
			set
			{
				base.Visible = false;
				base.Opacity = 0;
			}
		}
		public void SetVisible(bool b)
		{
			base.Visible = b;
			if (b == false)
			{
				base.Opacity = 0;
			}
			else
			{
				base.Opacity = 100;
			}
		}
		// ********************************************************************
		public MainForm()
		{
			SetVisible(false);
			GetHomeFolder();
			this.SuspendLayout();
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

			// ToDO ;mm
			/*
			editControl1.SelectObjectsChanged += (sender, e) =>
			{
				ToPropertyGrid(e.objs);
			};
			*/

			this.FormClosed += (sender, e) => { LastSettings(); };
			StartSettings();
			ControlLayout();
			openFormMenu.Click += (sender, e) => { OpenForm(); };
			newFormMenu.Click += (sender, e) => { NewForm(); };
			closeFormMenu.Click += (sender, e) => { CloseForm(); };
			deleteControlMenu.Click += (sender, e) => { DeleteControl(); };
			quitMenu.Click += (sender, e) => { Application.Exit(); };
			Command(Environment.GetCommandLineArgs().Skip(1).ToArray(), PIPECALL.StartupExec);
			this.ResumeLayout();
			SetVisible(false);
			if (this.HForms.Count == 0)
			{
				if (OpenForm(HomeFilePath) == false)
				{
					this.SetVisible(true);
				}
			}
			ItemsLib.Beep();
			//editControl1.MainForm = this;
			editHypowerd1.MainForm = this;
			ControlLayout();
			//PUtil.ToJsonCodeToClipboard(typeof(HMainMenu));
			//PUtil.PropListToClipboard(typeof(EditTextBox),"Edit");
		}
		// **********************************************************
		private void StartSettings()
		{
			PrefFile pf = new PrefFile(this, Application.ExecutablePath);
			pf.Load();
			Rectangle? rect = pf.GetBounds();
			object? obj = null;

			obj = pf.JsonFile.ValueInt("Left_SplitterDistance");
			if (obj != null) splitLeft.SplitterDistance = (int)obj;
			/*
			obj = pf.JsonFile.ValueInt("MainDistance");
			if (obj != null) editControl1.MainDistance = (int)obj;
			obj = pf.JsonFile.ValueInt("MenuDistance");
			if (obj != null) editControl1.MenuDistance = (int)obj;
			*/
			obj = pf.JsonFile.ValueInt("ScriptWidth");
			if (obj != null) m_ScriptWidth = (int)obj;

			obj = pf.JsonFile.ValueInt("AddFormCount");
			if (obj != null) m_AddFormCount = (int)obj;
			if (m_AddFormCount > 100) m_AddFormCount = 0;
		}
		// **********************************************************
		private void LastSettings()
		{
			PrefFile pf = new PrefFile(this, Application.ExecutablePath);
			if (IsScript) IsScript = false;
			pf.SetBounds();
			pf.JsonFile.SetValue("Left_SplitterDistance", splitLeft.SplitterDistance);
			//pf.JsonFile.SetValue("MainDistance", editControl1.MainDistance);
			//pf.JsonFile.SetValue("MenuDistance", editControl1.MenuDistance);
			pf.JsonFile.SetValue("AddFormCount", m_AddFormCount);
			pf.JsonFile.SetValue("ScriptWidth", m_ScriptWidth);
			pf.Save();
		}

		public void IsEditoPropertyGrid()
		{
			if (TargetForm != null)
				propertyGrid1.Enabled = TargetForm.IsEdit;

		}
		public void ToPropertyGrid(object?[]? objs)
		{
			IsEditoPropertyGrid();
			if ((objs == null) || (objs.Length <= 0))
			{
				propertyGrid1.SelectedObject = null;
			}
			else if (objs.Length == 1)
			{
				propertyGrid1.SelectedObject = objs[0];
			}
			else
			{
				propertyGrid1.SelectedObjects = objs;
			}
			if (editor != null)
			{
				if ((objs == null) || (objs.Length <= 0))
				{
					editor.Target = null;
				}
				else if (objs.Length > 0)
				{
					editor.Target = objs[0];
				}
			}

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
		// ************************************************************
		public void ControlLayout()
		{
			int y = 0;
			if (menuStrip1 != null)
			{
				menuStrip1.Location = new Point(0, m_BarHeight);
				menuStrip1.Size = new Size(this.Width, menuStrip1.Height);
				y += menuStrip1.Bottom + 2;
			}
			if (splitLeft != null)
			{
				splitLeft.Location = new Point(0, y);
				int h = this.Height - y - 20;
				if (m_IsScript)
				{
					splitLeft.Size = new Size(splitLeft.Width, h);
					int w = this.Width - splitLeft.Width - 2;
					editor.Location = new Point(splitLeft.Right + 2, y);
					if ((editor.Width != w) || (editor.Height != h))
					{
						editor.Size = new Size(w, h);
						m_ScriptWidth = editor.Width;
					}
				}
				else
				{
					splitLeft.Size = new Size(this.Width, h);
				}
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
	}

}