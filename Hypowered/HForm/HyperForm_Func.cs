using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	partial class HyperForm
	{
		public T_Funcs Funcs =new T_Funcs();
		// *************************************************************************
		public void SetupFuncs()
		{
			List<FuncItem> lst = new List<FuncItem>();

			lst.Add(new FuncItem(OpenForm, Keys.Control | Keys.O, "開く"));
			lst.Add(new FuncItem(SaveAsForm, Keys.Control | Keys.S, "コピーを保存"));
			lst.Add(new FuncItem(Quit, Keys.Control| Keys.Q, "終了"));
			lst.Add(new FuncItem(NewControl, Keys.Control | Keys.N, "新規コントロール"));
			lst.Add(new FuncItem(ToggleEditMode, Keys.Control | Keys.B, "編集モード"));
			lst.Add(new FuncItem(ToggleShowMenu, Keys.Control | Keys.F12, "メニューを消す"));
			lst.Add(new FuncItem(ShowPropForm, Keys.Control | Keys.I, "プロパティ"));
			lst.Add(new FuncItem(ShowControlList, Keys.Control | Keys.U, "コントロールリスト"));
			lst.Add(new FuncItem(ShowEditor, Keys.Control | Keys.F11, "スクリプトエディタ"));

			Funcs.SetFuncItems(lst.ToArray());
		}
		// *************************************************************************
		private HyperMenuItem? CreateMenuItem(FuncType fnc)
		{
			HyperMenuItem? ret = null;
			if (fnc == null) return ret;
			FuncItem? fi = Funcs.FindFunc(fnc.Method.Name);
			if(fi==null) return ret;
			ret = new HyperMenuItem(m_menuBar, fi.Caption, fi);
			return ret;
		}
		// *************************************************************************
		private HyperMenuItem? m_menuOpen = null;
		private HyperMenuItem? m_menuSaveAs = null;
		private HyperMenuItem? m_menuQuit = null;

		private HyperMenuItem? m_EditModeMenu = null;
		private HyperMenuItem? m_EditorMenu = null;
		private HyperMenuItem? m_ShowMenu = null;
		private HyperMenuItem? m_PropFormMenu = null;
		private HyperMenuItem? m_ControlListmMenu = null;
		private HyperMenuItem? m_NewControlMenu = null;
		// *************************************************************************
		public void MakeMenu()
		{
			m_menuOpen = CreateMenuItem(OpenForm);
			m_menuSaveAs = CreateMenuItem(SaveAsForm);
			m_menuQuit = CreateMenuItem(Quit);
			if (m_FileMenu != null)
			{
				m_FileMenu.Add(m_menuOpen);
				m_FileMenu.Add(m_menuSaveAs);
				m_FileMenu.Add(null);
				m_FileMenu.Add(m_menuQuit);
			}
			m_PropFormMenu = CreateMenuItem(ShowPropForm);
			m_NewControlMenu = CreateMenuItem(NewControl);
			m_EditModeMenu　= CreateMenuItem(ToggleEditMode);
			m_ShowMenu = CreateMenuItem(ToggleShowMenu);
			m_ControlListmMenu = CreateMenuItem(ShowControlList);
			m_EditorMenu = CreateMenuItem(ShowEditor);

			if (m_ControlMenu != null)
			{
				m_ControlMenu.Add(m_EditModeMenu);
				m_ControlMenu.Add(m_ShowMenu);
				m_ControlMenu.Add(null);
				m_ControlMenu.Add(m_PropFormMenu);
				m_ControlMenu.Add(m_ControlListmMenu);
				m_ControlMenu.Add(m_EditorMenu);
				m_ControlMenu.Add(null);
				m_ControlMenu.Add(m_NewControlMenu);
			}
		}
		// *************************************************************************
		public bool OpenForm()
		{
			return true;
		}
		// *************************************************************************
		public bool SaveAsForm()
		{
			return true;
		}
		// *************************************************************************
		public bool Quit()
		{
			Application.Exit();
			return true;
		}
		// *************************************************************************
		public bool ToggleEditMode()
		{
			SetIsEditMode(!m_IsEditMode);
			return true;
		}
		// *************************************************************************
		public bool ToggleShowMenu()
		{
			IsShowMenu = !IsShowMenu;
			return true;
		}
		// *************************************************************************
		private ControlType m_ct = ControlType.Button;
		public bool NewControl()
		{
			if (m_IsEditMode==false) return false;
			EditControlForm dlg= new EditControlForm();
			dlg.ControlType= m_ct;
			dlg.SetTarget(this);
			if(dlg.ShowDialog(this)==DialogResult.OK )
			{
				m_ct= dlg.ControlType;
				AddControl(dlg.ControlType, dlg.ControlName, dlg.ControlText, dlg.Font);
				return true;
			}
			return false;
		}
		// *************************************************************************
		public bool EditControl()
		{
			if (m_IsEditMode == false) return false;
			if(m_TargetIndex<=0) return false;
			EditControlForm dlg = new EditControlForm();
			HyperControl hc = (HyperControl)this.Controls[m_TargetIndex];
			dlg.SetTarget(this, hc);
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				if (hc.Name != dlg.ControlName) hc.Name = dlg.ControlName;
				if (hc.Text != dlg.ControlText) hc.Text = dlg.ControlText;
				hc.Font = dlg.Font;
				return true;
			}
			return false;
		}
		// *************************************************************************
		public bool ShowPropForm()
		{
			if (PropForm == null)
			{
				PropForm = new PropertyForm();
				PropForm.HyperForm = this;
				PropForm.Location = new Point(100, 100);
			}
			PropForm.Visible = !PropForm.Visible;
			return true;
		}
		// *************************************************************************
		public bool ShowControlList()
		{
			if (ControlList == null)
			{
				ControlList = new EditControlListForm();
				ControlList.HyperForm = this;
				ControlList.Location = new Point(100, 100);
			}

			ControlList.Visible  = !ControlList.Visible;
				return true;
			
			return false;
		}
		public bool ShowEditor()
		{
			if(Editor.Visible==false)
			{
				Editor.Visible = true;
				Editor.Activate();
			}
			else
			{
				Editor.Visible = false;
			}
			return true;
		}
		// *************************************************************************
	}
}
