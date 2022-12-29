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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{

    public partial class HyperMainForm : HyperBaseForm
	{

		protected HyperMenuBar m_menuBar = new HyperMenuBar();
		protected HyperMenuItem? m_FileMenu = null;
		protected HyperMenuItem? m_EditlMenu = null;
		protected HyperMenuItem? m_ControlMenu = null;
		protected HyperMenuItem? m_UserMenu = null;

		public HyperMainForm()
		{
			SetInScript(InScript.Startup| InScript.MouseClick| InScript.KeyPress);

			base.KeyPreview = true;
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			this.Name = "HyperForm";
			FormBorderStyle = FormBorderStyle.None;
			AutoScaleMode = AutoScaleMode.None;
			this.SetStyle(
//ControlStyles.Selectable |
//ControlStyles.UserMouse |
ControlStyles.DoubleBuffer |
ControlStyles.UserPaint |
ControlStyles.AllPaintingInWmPaint |
ControlStyles.SupportsTransparentBackColor,
true);
			this.UpdateStyles();
			//this.ControlAdded += HyperForm_ControlAdded;
			//this.ControlRemoved += HyperForm_ControlRemoved;
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
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			LoadFromFile("aaa.json");
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

			SaveToFile("aaa.json");
		}



		// ***********************************************************************
		public void InitScript()
		{
			m_Script.Init();
			m_Script.InitControls(this.Controls);
		}
		// ***********************************************************************
		// ****************************************************************************
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
	}
}
