using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	partial class HyperMainForm
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
			lst.Add(new FuncItem(ShowScriptEdit, Keys.Control | Keys.E, "スクリプト編集"));
			lst.Add(new FuncItem(PictLibDialog, Keys.Control | Keys.F1, "Pict選択"));
			lst.Add(new FuncItem(AddUserPict, Keys.Control | Keys.U, "ユーザー画像追加"));
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
		private HyperMenuItem? m_ShowMenu = null;
		private HyperMenuItem? m_PropFormMenu = null;
		private HyperMenuItem? m_ControlListmMenu = null;
		private HyperMenuItem? m_NewControlMenu = null;
		private HyperMenuItem? m_ScriptEditMenu = null;
		private HyperMenuItem? m_PictLibMenu = null;
		private HyperMenuItem? m_AddUserPictMenu = null;
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
			m_ScriptEditMenu = CreateMenuItem(ShowScriptEdit);
			m_PictLibMenu = CreateMenuItem(PictLibDialog);
			m_AddUserPictMenu = CreateMenuItem(AddUserPict);
			if (m_ControlMenu != null)
			{
				m_ControlMenu.Add(m_EditModeMenu);
				m_ControlMenu.Add(m_ShowMenu);
				m_ControlMenu.Add(null);
				m_ControlMenu.Add(m_PropFormMenu);
				m_ControlMenu.Add(m_ControlListmMenu);
				m_ControlMenu.Add(m_PictLibMenu);
				m_ControlMenu.Add(m_AddUserPictMenu);
				m_ControlMenu.Add(null);
				m_ControlMenu.Add(m_ScriptEditMenu);
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
			if (FormList.TargetForm == null) return false;

			EditControlForm dlg = new EditControlForm();
			dlg.ControlType= m_ct;
			dlg.SetMainForm(this,FormList.TargetForm,null);
			if(dlg.ShowDialog(FormList.TargetForm) ==DialogResult.OK )
			{
				m_ct= dlg.ControlType;
				AddControl(FormList.TargetForm, dlg.ControlType, dlg.ControlName, dlg.ControlText, dlg.Font);
				return true;
			}
			return false;
		}
		public bool DeleteControl()
		{
			if (m_IsEditMode == false) return false;

			return false;
		}
		// *************************************************************************
		// *************************************************************************
		public bool EditControl()
		{
			if (m_IsEditMode == false) return false;
			if(m_TargetIndex<=0) return false;
			if (FormList.TargetForm == null) return false;
			if (FormList.TargetForm.TargetControl == null) return false;
			HyperControl hc = FormList.TargetForm.TargetControl;
			EditControlForm dlg = new EditControlForm();
			dlg.SetMainForm(this, FormList.TargetForm, hc);
			if (dlg.ShowDialog(FormList.TargetForm) == DialogResult.OK)
			{
				if (hc.Name != dlg.ControlName) hc.Name = dlg.ControlName;
				if (hc.Text != dlg.ControlText) hc.Text = dlg.ControlText;
				return true;
			}
			else
			{
				return false;
			}
		}
		// *************************************************************************
		public bool ShowPropForm()
		{
			if (PropForm == null)
			{
				PropForm = new HyperPropForm();
				PropForm.SetMainForm(this);
				if(PropFormBounds.Left==-1)
				{
					PropForm.Location = new Point(
						this.Right+5,
						this.Top );
				}
				else
				{
					PropForm.Bounds = PropFormBounds;
				}

			}
			if (PropForm.Visible == false)
			{
				PropForm.Visible = true;
				PropForm.IsShow= true;
				PropForm.Activate();
			}
			else
			{
				PropForm.Visible = false;
				PropForm.IsShow = false;
			}
			return true;
		}
		// *************************************************************************
		public bool ShowControlList()
		{
			if (ControlList == null)
			{
				ControlList = new HyperControlList();
				ControlList.SetMainForm(this);
				if (ControlListBounds.Left == -1)
				{
					ControlList.Location = new Point(
						this.Right + 5,
						this.Top);
				}
				else
				{
					ControlList.Bounds = ControlListBounds;
				}
			}

			if (ControlList.Visible == false)
			{
				ControlList.Visible = true;
				ControlList.Activate();
			}
			else
			{
				ControlList.Visible = false;
			}

			return false;
		}
		// *************************************************************************
		// *************************************************************************
		public bool ShowScriptEdit()
		{
			bool ret = false;
			if(m_IsEditMode==false) return ret;
			using (HyperScriptEditor dlg = new HyperScriptEditor())
			{
				dlg.SetMainForm(this);
				if (ScriptEditBounds.Left == -1)
				{
					dlg.Bounds = this.Bounds;
				}
				else
				{
					dlg.Bounds = ScriptEditBounds;
				}

				if (dlg.ShowDialog() == DialogResult.OK)
				{
					ret = true;
				}
				ScriptEditBounds = dlg.Bounds;
			}
			return ret;
		}
		public bool PictLibDialog()
		{
			bool ret = false;
			if (m_IsEditMode == false) return ret;
			HyperPictLibDialog dlg = new HyperPictLibDialog();
			dlg.SetMainForm(this);
			dlg.StartPosition = FormStartPosition.CenterParent;
			if(dlg.ShowDialog(this)==DialogResult.OK)
			{
				ret = true;
			}
			return ret;
		}
		public bool AddUserPict()
		{
			bool ret = false;
			if (m_IsEditMode == false) return ret;
			if (m_FileName == "") return ret;
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "*.png|*.png|*.jpg|*.jpg|*.*|*.*";
			if(ofd.ShowDialog(this)==DialogResult.OK)
			{
				string nm = Path.GetFileName(ofd.FileName);
				int idx = PictLib.IndexOf(nm);
				if(idx >=0)
				{
					MessageBox.Show("同じ名前のファイルがあります。");
					return false;
				}

				if(PictLib.AddUserPict(ofd.FileName)==true)
				{
					MessageBox.Show("[ "+nm + " ]を追加しました。");
					ret = true;
				}
			}
			return ret;
		}
		public bool RemoveControl()
		{
			bool ret = false;

			HyperBaseForm? bf = FormList.TargetForm;
			if (bf!=null)
			{
				Control? c = bf.TargetControl;
				if (c!=null)
				{
					if (c is HyperMenuBar) return ret;
					bf.Controls.Remove(c);
					bf.ChkControls();
					ret = true;
					bf.OnHControlRemoved(new EventArgs());

				}
			}

			return ret;
		}
	}
}
