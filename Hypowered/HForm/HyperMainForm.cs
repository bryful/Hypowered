using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using BRY;

namespace Hypowered
{

    public partial class HyperMainForm : HyperBaseForm
	{
		private readonly string m_MainENtryName = "hyperform.json";
		private readonly string m_BackupENtryName = "hyperform_backup.json";
		static private System.Threading.Mutex? _mutex = null;
		protected string m_FileName = "";
		[Category("Hypowered_Form")]
		public string FileName
		{
			get { return m_FileName; }
		}
		[Category("Hypowered_Form")]
		public string IDName
		{
			get { return Path.GetFileNameWithoutExtension(m_FileName); }
		}
		[Category("Hypowered")]
		public new string Text
		{
			get { return base.Text; }
			set {; }
		}
		// ****************************************************************************
		public delegate void FormChangedHandler(object sender, HyperChangedEventArgs e);
		public event FormChangedHandler? FormChanged;
		protected virtual void OnFormChanged(HyperChangedEventArgs e)
		{
			if (FormChanged != null)
			{
				FormChanged(this, e);
			}
		}
		public HyperFormList forms = new HyperFormList();
		[Browsable(false)]
		public List<HyperBaseForm> formItems
		{
			get { return forms.Items; }

		}
		[Browsable(false)]
		public HyperBaseForm? targetForm
		{
			get { return forms.TargetForm; }
		}
		[Browsable(false)]
		public HyperControl? targetControl
		{
			get
			{
				if((forms.TargetFormIndex>=0)&&(forms.TargetFormIndex <= forms.Count))
				{
					return forms[forms.TargetFormIndex].TargetControl;
				}
				else
				{
					return null;
				}

			}

		}
		[Browsable(false)]
		public string[] GetAllControlNames
		{
			get { return forms.GetAllContorolName(); }
		}
		public bool IsNameChk(string name)
		{
			return forms.IsNameChk(name);
		}
		private void FormList_FormChanged(object sender, HyperChangedEventArgs e)
		{
			OnFormChanged(e);
		}
		public ToolStripMenuItem[] GetFormsForMenu(HyperBaseForm? target, System.EventHandler func)
		{
			List<ToolStripMenuItem> list = new List<ToolStripMenuItem>();
			if ( forms.Count> 0)
			{
				foreach (HyperBaseForm c in forms.Items)
				{
					ToolStripMenuItem mi = new ToolStripMenuItem();
					if (target != null)
					{
						mi.Checked = (c.Index == target.Index);
					}
					mi.Text = c.Name;
					mi.Tag = (object)c;
					mi.Click += func;
					list.Add(mi);
				}
			}
			return list.ToArray();
		}
		public string[] GetFormsForList()
		{
			List<string> list = new List<string>();
			if (forms.Count > 0)
			{
				foreach (HyperBaseForm c in forms.Items)
				{
					list.Add(((HyperBaseForm)c).Name);
				}
			}
			return list.ToArray();
		}
		// ****************************************************************************
		protected HyperMenuBar m_menuBar = new HyperMenuBar();
		protected HyperMenuItem? m_FileMenu = null;
		protected HyperMenuItem? m_EditlMenu = null;
		protected HyperMenuItem? m_ControlMenu = null;
		protected HyperMenuItem? m_UserMenu = null;

		
		public override void SetIsEditMode(bool value)
		{
			for (int i = 1; i < forms.Count; i++)
			{
				if (forms[i] != this)
				{
					forms[i].SetIsEditMode(value);
				}
			}
			base.SetIsEditMode(value);
		}

		public HyperMainForm()
		{
			SetInScript(
				InScriptBit.Startup| 
				InScriptBit.MouseDoubleClick|
				InScriptBit.KeyPress|
				InScriptBit.Shutdown);
			forms.SetMain(this);
			Script.MainForm= this;

			forms.FormChanged += FormList_FormChanged;

			base.KeyPreview = true;
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			base.Name = "HyperForm";
			FormBorderStyle = FormBorderStyle.None;
			AutoScaleMode = AutoScaleMode.None;
			TransparencyKey = Color.Empty;
			this.SetStyle(
//ControlStyles.Selectable |
//ControlStyles.UserMouse |
ControlStyles.DoubleBuffer |
ControlStyles.UserPaint |
ControlStyles.AllPaintingInWmPaint ,
//ControlStyles.SupportsTransparentBackColor,
true);
			this.UpdateStyles();
			SetupFuncs();
			InitializeComponent();
			
			//念のためデザイナーで作られた物の確認
			if(this.Controls.Count > 0)
			{
				foreach(Control c in this.Controls)
				{
					AddControl(this,c);
				}
			}


			if(m_menuBar==null)m_menuBar = new HyperMenuBar();
			m_menuBar.ParentForm = this;
			m_FileMenu = new HyperMenuItem(m_menuBar, "File", null);
			m_EditlMenu = new HyperMenuItem(m_menuBar, "Edit", null);
			m_ControlMenu = new HyperMenuItem(m_menuBar, "Control", null);
			m_UserMenu = new HyperMenuItem(m_menuBar, "User", null);



			m_menuBar.Items.Add(m_FileMenu);
			m_menuBar.Items.Add(m_EditlMenu);
			m_menuBar.Items.Add(m_ControlMenu);
			m_menuBar.Items.Add(m_UserMenu);
			MakeMenu();
			this.Controls.Add(m_menuBar);
			this.Controls.SetChildIndex(m_menuBar,0);
			ChkControls();

			InitScript();
		}



		// *********************************************************************
		public void Command(string[] args, PIPECALL IsPipe = PIPECALL.StartupExec)
		{
			if(IsPipe!= PIPECALL.StartupExec)
			{
				this.Activate();
				return;
			}
			HArgs args1 = new HArgs(args);
			if (args1.FileName != "")
			{
				if (args1.Option == Option.Create)
				{
					if (args1.FileName != "")
					{
						m_FileName = args1.FileName;
						base.Name = IDName;
						base.Text= base.Name;
						if (SaveToHYPF())
						{
							StartServer();
							MutexStart();
						}
					}
				} else if(args1.Option == Option.Open)
				{
					if (LoadToHYPF(args1.FileName)==false)
					{
						Application.Exit();
					}
					MutexStart();

				}
				else
				{
					if(LoadToHYPF(args1.FileName))
					{
						MutexStart();
					}
				}
			}
		}
		// *********************************************************************
		public void MutexStart()
		{
			MutexStop();
			_mutex = new System.Threading.Mutex(false, base.Name);
			MessageBox.Show("MutexStart/" + base.Name);
		}
		public void MutexStop()
		{
			if (_mutex != null)
			{
				/*
				try
				{
					_mutex.ReleaseMutex();
				}
				catch 
				{
					MessageBox.Show("Error:ReleaseMutex");
				}*/
				try
				{
					_mutex.Dispose();
				}
				catch
				{
					MessageBox.Show("Error:DisposeMutex");
				}
				_mutex = null;
			}

		}

		public void FormInit(string fn,bool IsOpen =true)
		{
			//ホームファイルを読む無かったら作る
			m_FileName = "";
			base.Name = "";
			ClearFroms();
		}
		public void FormFisnish()
		{
			if (m_FileName != "")
			{
				if(Script_Shutdown!="")
				{
					Script.ExecuteCode(Script_Shutdown);
				}
				SaveToHYPF();
				MutexStop();
				StopServer();
				ClearFroms();
			}
		}
		// *********************************************************************
		protected override void OnLoad(EventArgs e)
		{
			RelatingFile(".hypf");
			base.OnLoad(e);
			//ホームファイルを読む無かったら作る
			m_FileName = "";
			base.Name = "";
			Command(Environment.GetCommandLineArgs().Skip(1).ToArray(), PIPECALL.StartupExec);
			if (m_FileName == "")
			{
				string home = DefaultFileName();
				if (File.Exists(home) == false)
				{
					m_FileName = home;
					base.Name = Path.GetFileNameWithoutExtension(home);
					if(SaveToHYPF())
					{
						StartServer();
						MutexStart();
					}
					else
					{
						MessageBox.Show("Err1");
					}
				}
				else
				{
					if (LoadToHYPF(home)==false)
					{
						MessageBox.Show("Err2");
					}
					else
					{
						MutexStart();
					}
				}
			}
		}
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			if(Script_MouseDoubleClick!="")
			{
				Script.ExecuteCode(Script_MouseDoubleClick);
			}
		}
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if (Script_KeyPress != "")
			{
				Script.ExecuteCode(Script_KeyPress);
			}
		}
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);

			if(ControlList!=null) ControlList.Dispose();

			FormFisnish();
		}
		protected override void OnCreatedControl(HyperChangedEventArgs e)
		{
			base.OnCreatedControl(e);
			Script.InitControls(this);
		}
		public override void OnDeletedControl(HyperChangedEventArgs e)
		{
			base.OnDeletedControl(e);
			Script.InitControls(this);
		}
		// ****************************************************************************
		protected override bool ProcessDialogKey(Keys keyData)
		{
			FuncItem? fi = Funcs.FindKeys(keyData);
			if ((fi != null) && (fi.Func != null))
			{
				if (fi.Func()) this.Invalidate();
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}
		// ****************************************************************************
		// ********************************************************************
		private F_Pipe m_Server = new F_Pipe();
		public void StartServer()
		{
			string id = IDName;
			if(id!="")
			{
				m_Server.Server(id);
				m_Server.Reception += M_Server_Reception;
			}

		}
		// ********************************************************************
		public void StopServer()
		{
			m_Server.StopServer();
		}
		// ********************************************************************
		private void M_Server_Reception(object sender, ReceptionArg e)
		{
			this.Invoke((Action)(() => {
				PipeData pd = new PipeData(e.Text);
				Command(pd.Args, PIPECALL.PipeExec);
				ForegroundWindow();
			}));
		}
		// ****************************************************************************
			public void ForegroundWindow()
		{
			F_W.SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
		}
		// ****************************************************************************
		public string StatusFileName()
		{
			string p = JsonFile.GetAppDataPath();
			string n = IDName;
			return Path.Combine(p, n + ".json");
		}
		public string DefaultFileName()
		{
			return Path.ChangeExtension(Application.ExecutablePath, ".hypf");
		}
		// ****************************************************************************
		public bool SaveStatus(string s)
		{
			JsonFile jf = new JsonFile();
			jf.SetValue(nameof(Location), Location);
			jf.SetValue(nameof(ControlListBounds), ControlListBounds);
			jf.SetValue(nameof(ScriptEditBounds), ScriptEditBounds);
			jf.SetValue(nameof(OutputFormBounds), OutputFormBounds);
			jf.SetValue(nameof(InputFormBounds), InputFormBounds);
			return jf.Save(s);
		}
		public bool Connect(HyperControl s, ControlType ct,HyperControl d)
		{
			return ConnectList.AddConnect(this.Controls, s.Name, ct, d.Name);
		}
		// ****************************************************************************
		public bool LoadStatus(string s)
		{
			JsonFile jf = new JsonFile();
			bool ret = jf.Load(s);
			if(ret==false) { return ret; }

			object? v = null;
			v = jf.ValueAuto("Location", typeof(Point).Name);
			if (v != null) Location = (Point)v;
			v = jf.ValueAuto("ControlListBounds", typeof(Rectangle).Name);
			if (v != null) ControlListBounds = (Rectangle)v;
			v = jf.ValueAuto("ScriptEditBounds", typeof(Rectangle).Name);
			if (v != null) ScriptEditBounds = (Rectangle)v;
			v = jf.ValueAuto("InputFormBounds", typeof(Rectangle).Name);
			if (v != null) InputFormBounds = (Rectangle)v;
			v = jf.ValueAuto("OutputFormBounds", typeof(Rectangle).Name);
			if (v != null) OutputFormBounds = (Rectangle)v;

			if (JsonFile.ScreenIn(this.Bounds) == false)
			{
				Rectangle rct = Screen.PrimaryScreen.Bounds;
				Point p = new Point((rct.Width - Width) / 2, (rct.Height - Height) / 2);
				Location = p;
			}
			return ret=true;
		}

		public bool SaveToHYPF()
		{
			bool ret = false;
			if (m_FileName == "") return ret;
			ret = SaveForm(m_FileName);
			if (ret)
			{
				SaveStatus(StatusFileName());
				base.Name = Path.GetFileNameWithoutExtension(m_FileName);
				Lib.SetMainForm(this);//FileNameを設定してる
				InitScript();
			}
			return ret;
		}
		public void ClearFroms()
		{
			forms.Clear();
			if(this.Controls.Count > 1)
			{
				for(int i= this.Controls.Count-1; i>=1;i--)
				{
					//this.Controls[i].Dispose();
					this.Controls.RemoveAt(i);
				}
			}
			Script.Init();
		}
		public bool LoadToHYPF(string p)
		{
			bool ret = false;
			if (p == "") return ret;
			FormFisnish();

			ret = LoadForm(p);
			if (ret)
			{
				m_FileName = p;
				base.Name = IDName;
				base.Text = IDName;
				Lib.SetMainForm(this);//FileNameを設定してる
				ConnectList.ConnectAll(this.Controls);
				LoadStatus(StatusFileName());
				InitScript();
				if (Script_Startup != "")
				{
					ExecuteCode(Script_Startup);
				}
			}
			return ret;
		}
		public bool SaveForm(string p)
		{
			bool ret = false;

			try
			{
				m_FileName = p;
				base.Name = IDName;
				base.Text = IDName;
				string js = ToJsonCode();
				bool b = File.Exists(m_FileName);
				if(b) ZipUtil.BackupEntry(p, m_MainENtryName, m_BackupENtryName);
				ZipUtil.SetEntryFromStr(p, m_MainENtryName, js);
				if(!b) ZipUtil.BackupEntry(p, m_MainENtryName, m_BackupENtryName);
				ret = true;
			}
			catch
			{
				m_FileName = "";
				ret = false;
			}
			return ret;
		}
		public bool LoadForm(string p)
		{
			bool ret = false;

			JsonObject? GetEntry(string entryName)
			{
				JsonObject? ret = null;
				string? js = ZipUtil.GetEntryToStr(p, m_MainENtryName);
				if (js != null)
				{
					var doc = JsonNode.Parse(js);
					if (doc != null)
					{
						ret = (JsonObject?)doc;
					}
				}
				return ret;
			}

			try
			{
				JsonObject? Obj = GetEntry(m_MainENtryName);
				if(Obj!=null)
				{
					try
					{
						ClearFroms();
						FromJson(Obj);
					}
					catch
					{
						Obj = GetEntry(m_BackupENtryName);
						if(Obj!=null)
						{
							ClearFroms(); 
							FromJson(Obj);
						}
					}
				}
				Script.Init();
				Script.InitControls(this);
				m_FileName = p;
				base.Name = IDName;
				ret = true;
			}
			catch
			{
				ret = false;
				m_FileName = "";
			}
			return ret;
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process[] ps =
				System.Diagnostics.Process.GetProcesses();
			List<string> list = new List<string>();
			foreach (System.Diagnostics.Process p in ps)
			{
				string ss = "";
				try
				{
					//プロセス名を出力する
					ss+=$"プロセス名: {p.ProcessName}";
					//ID
					ss += $",id: {p.Id}";
					ss += $",fn: {p.MainModule.FileName}";
				}
				catch (Exception ex)
				{
					ss += $",fn: {ex.Message}";
				}
				list.Add(ss);
			}
			MessageBox.Show(string.Join("\r\n", list));
		}
	}
}
