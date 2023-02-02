using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ClearScript;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;


namespace Hpd
{
    public partial class HpdMainForm : HpdForm
	{

		public EditPropertyForm? PropertyForm = null;
		public ScriptEditor? Editor = null;
		public ConsoleForm? ConsoleForm = null;

		public HpdScript Script = new HpdScript();

		public HpdMainForm()
		{
			Script.SetMainForm(this);
			InitializeComponent();
			Editor = new ScriptEditor();
			Editor.SetMainForm(this);
			m_seShow = false;
			CreateMainMenu();
		}
		public void CreateMainMenu()
		{
			var mi = MainMenu.FileMenu.AddMenuItem("Quit", "Quit");
			mi.Func = Exit;
			var mi2 = MainMenu.EditMenu.AddMenuItem("ShowPropertyForm", "ShowPropertyForm");
			mi2.Func = ShowPropertyForm;
			var mi3 = MainMenu.EditMenu.AddMenuItem("ShowScriptEditor", "ShowScriptEditor");
			mi3.Func = ShowScriptEditor;
			var mi4 = MainMenu.EditMenu.AddMenuItem("ShowConsoleForm", "ShowConsoleForm");
			mi4.Func = ShowConsoleForm;

			var mi5 = MainMenu.FileMenu.AddMenuItem("DebugX", "DebugX");
			mi5.Func = GetHpdRoot;
			//var mi6 = MainMenu.FileMenu.AddMenuItem("getGlobalThis", "getGlobalThis");
			//mi5.Func = GetGlobalThis;

		}
		public bool GetHpdRoot()
		{
			PropertySetToClipboard(typeof(ComboBox));
			return true;
		}
		public bool GetGlobalThis()
		{
			Script.GetGlobalThis();
			return true;
		}
		public bool ShowPropertyForm()
		{
			if(PropertyForm==null)
			{
				PropertyForm = new EditPropertyForm();
				PropertyForm.SetMainForm(this);
				PropertyForm.Show(this);
			}
			if (PropertyForm.Visible == false)
			{
				PropertyForm.Visible = true;
				PropertyForm.Activate();
			}
			return true;
		}
		private bool m_seShow = false;
		public bool ShowScriptEditor()
		{
			if(m_seShow == false)
			{
				if(Editor==null)
				{
					Editor = new ScriptEditor();
					Editor.SetMainForm(this);
				}
				Editor.Show(this);
				m_seShow = true;
			}
			if (Editor != null)
			{
				if (Editor.Visible == false)
				{
					Editor.Visible = true;
					Editor.Activate();
				}
			}
			return true;
		}
		public bool ShowConsoleForm()
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
				ConsoleForm.Activate();
			}
			return true;
		}

		public void Alert(object? obj, string cap = "")
		{
			AlertMes(obj, cap);
		}
		[ScriptUsage(ScriptAccess.None)]
		static public void AlertMes(object? obj, string cap = "")
		{
			using (AlertForm dlg = new AlertForm())
			{
				dlg.Text = ToStr(obj);
				if (cap != "") dlg.Title = cap;
				if (dlg.ShowDialog() == DialogResult.OK)
				{

				}
			}
		}

		public bool Exit()
		{
			Application.Exit();
			return true;
		}
		public int YesNoDialog(string str, string tx = "")
		{
			using (YesNoForm dlg = new YesNoForm())
			{
				dlg.ShowCancel = false;
				dlg.Text = str;
				if (tx != "") dlg.Title = tx;
				switch (dlg.ShowDialog())
				{
					case DialogResult.Cancel:
						return -1;
					case DialogResult.Yes:
					case DialogResult.OK:
						return 1;
					case DialogResult.No:
					default:
						return 0;
				}
			}
		}
		public void ConsoleWriteLine(object? o)
		{
			ShowConsoleForm();
			if(ConsoleForm!=null) ConsoleForm.WriteLine(o);
		}
		public void ConsoleWrite(object? o)
		{
			ShowConsoleForm();
			if (ConsoleForm != null) ConsoleForm.Write(o);
		}
		public void ConsoleClear()
		{
			ShowConsoleForm();
			if (ConsoleForm != null) ConsoleForm.Clear();
		}


		
	}
}
