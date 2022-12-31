using System;
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
		private System.Threading.Mutex? _mutex = null;
		public HyperFormList FormList = new HyperFormList();
		protected HyperMenuBar m_menuBar = new HyperMenuBar();
		protected HyperMenuItem? m_FileMenu = null;
		protected HyperMenuItem? m_EditlMenu = null;
		protected HyperMenuItem? m_ControlMenu = null;
		protected HyperMenuItem? m_UserMenu = null;

		public override void SetIsEditMode(bool value)
		{
			m_IsEditMode = value;
			if (this.Controls.Count > 0)
			{
				foreach (Control c in this.Controls)
				{
					if (c is HyperControl)
					{
						((HyperControl)c).IsEditMode = m_IsEditMode;
					}
				}
			}
			if(FormList.Count>1)
			{
				for(int i=1;i<FormList.Count;i++)
				{
					if (FormList[i] != null)
					{
						FormList[i].SetIsEditMode(value);
					}
				}
			}
			//ChkControls();
			this.Invalidate();
		}
		public HyperMainForm()
		{
			SetInScript(InScript.Startup| InScript.MouseClick| InScript.KeyPress);
			FormList.SetMain(this);
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

			if(m_menuBar==null)m_menuBar = new HyperMenuBar();
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
				return;
			}
			HArgs args1 = new HArgs(args);
			LoadToHYPF(args1.First);
		}
		// *********************************************************************
		protected override void OnLoad(EventArgs e)
		{
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
					SaveToHYPF();
				}
				else
				{
					LoadToHYPF(home);
				}
			}

			LoadStatus(StatusFileName());


			StartServer(base.Name);
			if(Script_Startup!="")
			{
				ExecuteCode(Script_Startup);
			}
		}
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);

			if(PropForm!=null) PropForm.Dispose();
			if(ControlList!=null) ControlList.Dispose();

			SaveStatus(StatusFileName());
			SaveToHYPF();
			if(_mutex!=null)
			{
				_mutex.ReleaseMutex(); 
				_mutex.Dispose();
				_mutex= null;
			}
			StopServer();
		}
		protected override void OnHControlAdd(HControlAddEventArgs e)
		{
			base.OnHControlAdd(e);
			Script.InitControls(this.Controls);
		}
		public override void OnHControlRemoved(EventArgs e)
		{
			base.OnHControlRemoved(e);
			Script.InitControls(this.Controls);
		}
		protected override void OnActivated(EventArgs e)
		{
			
			base.OnActivated(e);
		}
		protected override void OnDeactivate(EventArgs e)
		{
		
			base.OnDeactivate(e);
		}
		// ****************************************************************************
		/*
		public ToolStripMenuItem[] GetMenuControls(HyperControl? target, System.EventHandler func)
		{
			List<ToolStripMenuItem> list = new List<ToolStripMenuItem>();
			if (this.Controls.Count > 0)
			{
				foreach (Control c in this.Controls)
				{
					if (c is not HyperControl) continue;
					HyperControl hc = (HyperControl)c;
					ToolStripMenuItem mi = new ToolStripMenuItem();
					if (target != null)
					{
						mi.Checked = (hc.Index == target.Index);
					}
					mi.Text = hc.Name;
					mi.Tag = (object)hc;
					mi.Click += func;
					list.Add(mi);
				}
			}
			return list.ToArray();
		}
		*/
		// ****************************************************************************
		protected override bool ProcessDialogKey(Keys keyData)
		{
#if DEBUG
			this.Text = String.Format("{0}", keyData.ToString());
#endif
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
		public void StartServer(string pipename)
		{
			m_Server.Server(pipename);
			m_Server.Reception += M_Server_Reception;
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
			string n = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
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
			jf.SetValue(nameof(PropFormBounds), PropFormBounds);
			jf.SetValue(nameof(ControlListBounds), ControlListBounds);
			jf.SetValue(nameof(ScriptEditBounds), ScriptEditBounds);
			return jf.Save(s);
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
			v = jf.ValueAuto("PropFormBounds", typeof(Rectangle).Name);
			if (v != null) PropFormBounds = (Rectangle)v;
			v = jf.ValueAuto("ControlListBounds", typeof(Rectangle).Name);
			if (v != null) ControlListBounds = (Rectangle)v;
			v = jf.ValueAuto("ScriptEditBounds", typeof(Rectangle).Name);
			if (v != null) ScriptEditBounds = (Rectangle)v;

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
				base.Name = Path.GetFileNameWithoutExtension(m_FileName);
				PictLib.SetMainForm(this);//FileNameを設定してる
				if (_mutex == null)
				{
					_mutex = new System.Threading.Mutex(false, base.Name);
				}
			}
			return ret;
		}
		public void InitForm()
		{
			FormList.Init();
			if(this.Controls.Count > 1)
			{
				for(int i= this.Controls.Count-1; i>=1;i--)
				{
					this.Controls[i].Dispose();
					this.Controls.RemoveAt(i);
				}
			}
			Script.Init();
		}
		public bool LoadToHYPF(string p)
		{
			bool ret = false;
			if (p == "") return ret;
			InitForm();
			ret = LoadForm(p);
			if (ret)
			{
				m_FileName = p;
				base.Name = Path.GetFileNameWithoutExtension(p);
				PictLib.SetMainForm(this);//FileNameを設定してる

				if (_mutex == null)
				{
					_mutex = new System.Threading.Mutex(false, base.Name);
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
				base.Name = Path.GetFileNameWithoutExtension(p);
				string js = ToJsonCode();
				ZipUtil.SetEntryFromStr(p, "hyperform.json", js);
			}
			catch
			{
				ret = false;
			}
			return ret;
		}
		public bool LoadForm(string p)
		{
			bool ret = false;

			try
			{
				string? js = ZipUtil.GetEntryToStr(p, "hyperform.json");
				if (js!= null)
				{
					var doc = JsonNode.Parse(js);
					if (doc != null)
					{
						var Obj = (JsonObject?)doc;
						if (Obj != null)
						{
							FromJson(Obj);
							Script.Init();
							Script.InitControls(this.Controls);
							m_FileName = p;
							base.Name = Path.GetFileNameWithoutExtension(p);
							ret = true;
						}
					}

					m_FileName = p;
				}
			}
			catch
			{
				ret = false;
			}
			return ret;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			PropertyInfo[] pp =typeof( Properties.Resources).GetProperties();

			string ret = "";
			foreach (PropertyInfo prop in pp)
			{
				ret += prop.Name+"\r\n";
			}
			MessageBox.Show(ret);
		}
	}
}
