using BRY;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
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

			lst.Add(new FuncItem(OpenForm, Keys.Control | Keys.O, "別のフォームで開く"));
			lst.Add(new FuncItem(LoadForm, Keys.Alt | Keys.O, "開く"));
			lst.Add(new FuncItem(NewForm, Keys.Control | Keys.N, "新規"));
			lst.Add(new FuncItem(SaveAsForm, Keys.Control | Keys.S, "コピーを保存"));
			lst.Add(new FuncItem(Quit, Keys.Control| Keys.Q, "終了"));
			lst.Add(new FuncItem(NewControl, Keys.Control | Keys.I, "新規コントロール"));
			lst.Add(new FuncItem(ToggleEditMode, Keys.Control | Keys.B, "編集モード"));
			lst.Add(new FuncItem(ToggleShowMenu, Keys.Control | Keys.F12, "メニューを消す"));
			lst.Add(new FuncItem(ShowControlList, Keys.Control | Keys.U, "コントロールリスト"));
			lst.Add(new FuncItem(OpenScriptEdit, Keys.Control | Keys.L, "スクリプト編集表示"));
			lst.Add(new FuncItem(ExecScriptEdit, Keys.Alt | Keys.E, "スクリプト編集"));
			lst.Add(new FuncItem(ShowPictLibDialog, Keys.Control | Keys.F1, "アイコン選択"));
			lst.Add(new FuncItem(AddUserPict, Keys.Control | Keys.F2, "ユーザー画像追加"));
			lst.Add(new FuncItem(DeleteControl, Keys.Delete, "コントロール削除"));
			lst.Add(new FuncItem(ShowFontDialog, Keys.Control|Keys.F, "フォント"));
			lst.Add(new FuncItem(ShowEditContent, Keys.Control | Keys.E, "テキスト編集"));
			lst.Add(new FuncItem(ShowEditControl, Keys.Shift | Keys.E, "コントロール編集"));
			lst.Add(new FuncItem(ControlToDown, Keys.Alt | Keys.Down, "コントロールを下へ"));
			lst.Add(new FuncItem(ControlToUp, Keys.Alt | Keys.Up, "コントロールを上へ"));
			lst.Add(new FuncItem(ControlToFloor, Keys.Alt | Keys.Down, "コントロールを一番下へ"));
			lst.Add(new FuncItem(ControlToFront, Keys.Alt | Keys.Up, "コントロールを一番上へ"));
			lst.Add(new FuncItem(ShowInputForm, Keys.Control | Keys.F9, "JavaScript Input"));
			lst.Add(new FuncItem(ShowOutputForm, Keys.Control | Keys.F8, "JavaScript Output"));
			lst.Add(new FuncItem(CopyControl, Keys.Control | Keys.C, "Copy"));
			lst.Add(new FuncItem(CutControl, Keys.Control | Keys.X, "Cut"));
			lst.Add(new FuncItem(PasteControl, Keys.Control | Keys.V, "Paste"));
			lst.Add(new FuncItem(ShowAlignmentForm, Keys.Control | Keys.J, "配置パレット"));
			Funcs.SetFuncItems(lst.ToArray());
		}
		// *************************************************************************
		private HyperMenuItem? CreateMenuItem(FuncType fnc,bool IsEO=false)
		{
			HyperMenuItem? ret = null;
			if (fnc == null) return ret;
			FuncItem? fi = Funcs.FindFunc(fnc.Method.Name);
			if(fi==null) return ret;
			ret = new HyperMenuItem(m_menuBar, fi.Caption, fi);
			ret.IsEditModeOnly = IsEO;
			return ret;
		}
		// *************************************************************************
		private HyperMenuItem? m_menuOpen = null;
		private HyperMenuItem? m_menuLoad = null;
		private HyperMenuItem? m_menuNew = null;
		private HyperMenuItem? m_menuSaveAs = null;
		private HyperMenuItem? m_menuQuit = null;

		private HyperMenuItem? m_EditModeMenu = null;
		private HyperMenuItem? m_ShowMenu = null;
		private HyperMenuItem? m_ControlListmMenu = null;
		private HyperMenuItem? m_NewControlMenu = null;
		private HyperMenuItem? m_OpenScriptEditMenu = null;
		private HyperMenuItem? m_ScriptEditMenu = null;
		private HyperMenuItem? m_PictLibMenu = null;
		private HyperMenuItem? m_AddUserPictMenu = null;
		private HyperMenuItem? m_DeleteControlMenu = null;
		private HyperMenuItem? m_FontMenu = null;
		private HyperMenuItem? m_ContentMenu = null;
		private HyperMenuItem? m_InputMenu = null;
		private HyperMenuItem? m_OutputMenu = null;
		private HyperMenuItem? m_AlignMentMenu = null;

		private HyperMenuItem? m_CopyMenu = null;
		private HyperMenuItem? m_CutMenu = null;
		private HyperMenuItem? m_PasteMenu = null;

		// *************************************************************************
		public void MakeMenu()
		{
			m_menuOpen = CreateMenuItem(OpenForm);
			m_menuLoad = CreateMenuItem(LoadForm);
			m_menuNew = CreateMenuItem(NewForm);
			m_menuSaveAs = CreateMenuItem(SaveAsForm);
			m_menuQuit = CreateMenuItem(Quit);
			if (m_FileMenu != null)
			{
				m_FileMenu.Add(m_menuOpen);
				m_FileMenu.Add(m_menuLoad);
				m_FileMenu.Add(m_menuNew);
				m_FileMenu.Add(m_menuSaveAs);
				m_FileMenu.Add(null);
				m_FileMenu.Add(m_menuQuit);
			}
			m_CopyMenu = CreateMenuItem(CopyControl,true);
			m_CutMenu = CreateMenuItem(CutControl, true);
			m_PasteMenu = CreateMenuItem(PasteControl, true);
			if (m_EditlMenu!=null)
			{
				m_EditlMenu.Add(m_CopyMenu);
				m_EditlMenu.Add(m_CutMenu);
				m_EditlMenu.Add(m_PasteMenu);
			}

			m_EditModeMenu = CreateMenuItem(ToggleEditMode);
			m_ShowMenu = CreateMenuItem(ToggleShowMenu);

			m_NewControlMenu = CreateMenuItem(NewControl,true);
			m_ControlListmMenu = CreateMenuItem(ShowControlList, true);
			m_OpenScriptEditMenu = CreateMenuItem(OpenScriptEdit);
			m_ScriptEditMenu = CreateMenuItem(ExecScriptEdit, true);
			m_PictLibMenu = CreateMenuItem(ShowPictLibDialog, true);
			m_AddUserPictMenu = CreateMenuItem(AddUserPict, true);
			m_DeleteControlMenu = CreateMenuItem(DeleteControl, true);
			m_FontMenu = CreateMenuItem(ShowFontDialog, true);
			m_ContentMenu = CreateMenuItem(ShowEditContent, true);
			m_InputMenu = CreateMenuItem(ShowInputForm);
			m_OutputMenu = CreateMenuItem(ShowOutputForm);
			m_AlignMentMenu = CreateMenuItem(ShowAlignmentForm,true);
			if (m_ControlMenu != null)
			{
				m_ControlMenu.Add(m_EditModeMenu);
				m_ControlMenu.Add(m_ShowMenu);
				m_ControlMenu.Add(null);
				m_ControlMenu.Add(m_ControlListmMenu);
				m_ControlMenu.Add(m_AlignMentMenu);
				m_ControlMenu.Add(m_AddUserPictMenu);
				m_ControlMenu.Add(m_PictLibMenu);
				m_ControlMenu.Add(m_OpenScriptEditMenu);
				m_ControlMenu.Add(m_FontMenu);
				m_ControlMenu.Add(m_ContentMenu);
				m_ControlMenu.Add(m_OutputMenu);
				m_ControlMenu.Add(null);
				m_ControlMenu.Add(m_ScriptEditMenu);
				m_ControlMenu.Add(m_InputMenu);
				m_ControlMenu.Add(m_DeleteControlMenu);
				m_ControlMenu.Add(m_NewControlMenu);
			}
		}
		// *************************************************************************
		static public  Process? ProcessStart2(string app,string args)
		{
			ProcessStartInfo psi = new ProcessStartInfo();
			psi.FileName = app;
			psi.Arguments = args;
			psi.UseShellExecute = true;
			return Process.Start(psi);
		}
		// *************************************************************************
		public bool LoadForm()
		{
			bool ret = false;
			OpenFileDialog dlg = new OpenFileDialog();
			if(m_FileName=="")
			{
				dlg.InitialDirectory = HYPF_Folder;
			}
			else
			{
				dlg.InitialDirectory = Path.GetDirectoryName(m_FileName);
				dlg.FileName = Path.GetFileName(m_FileName);
			}
			dlg.Filter = $"*{Def.DefaultExt}|*{Def.DefaultExt}|*.*|*.*";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				ret = LoadFromHYPF(dlg.FileName);
			}
			return ret;
		}
		public bool OpenForm()
		{
			bool ret = false;
			OpenFileDialog dlg = new OpenFileDialog();
			if (m_FileName == "")
			{
				dlg.InitialDirectory = HYPF_Folder;
			}
			else
			{
				dlg.InitialDirectory = Path.GetDirectoryName(m_FileName);
				dlg.FileName = Path.GetFileName(m_FileName);
			}
			dlg.Filter = $"*{Def.DefaultExt}|*{Def.DefaultExt}|*.*|*.*";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (m_FileName != dlg.FileName)
				{
					F_W.ProcessStart(Application.ExecutablePath, " -open \"" + dlg.FileName + "\"");
					ret = true;
				}
			}
			return ret;
		}
		public bool NewForm()
		{
			bool ret = false;
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.InitialDirectory = HYPF_Folder;
			dlg.FileName = "newform.hypf";
			dlg.Filter = $"*{Def.DefaultExt}|*{Def.DefaultExt}|*.*|*.*";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (m_FileName != dlg.FileName)
				{
					string s = " /create";
					s += " ";
					s += "\"" + dlg.FileName + "\"";

					F_W.ProcessStart(Application.ExecutablePath, s);
					//F_W.ProcessStart("ArgsTest.exe", s);
					ret = true;
				}
			}


			return ret;
		}
		// *************************************************************************
		public bool SaveAsForm()
		{
			bool ret = false;
			if (m_FileName == "") return ret;
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.InitialDirectory = HYPF_Folder;
			dlg.FileName = Path.GetFileName(m_FileName);
			dlg.Filter = $"*{Def.DefaultExt}|*{Def.DefaultExt}|*.*|*.*";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if(dlg.FileName==m_HOME_HYPF_FILE)
				{
					MessageBox.Show($"{m_HOME_HYPF_FILE}\r\n can not override!");
					return ret;
				}
				if(m_FileName!=dlg.FileName)
				{
					//とりあえず保存
					ret = SaveToHYPF();
					//同じのあったら消す
					if(File.Exists(dlg.FileName)) File.Delete(dlg.FileName);
					//新しい名前でコピー
					File.Copy(m_FileName, dlg.FileName);
					ret = true;

					if (answerDialog.Show($"{Path.GetFileName(dlg.FileName)}を開きますか？"))
					{
						F_W.ProcessStart(Application.ExecutablePath, " -open \"" + dlg.FileName + "\"");
					}
				}
			}
			return ret;
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
			if(m_CanEditMode==false)
			{
				if(m_IsEditMode==true) SetIsEditMode(false);
			}
			else
			{
				SetIsEditMode(!m_IsEditMode);
			}
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
			if (forms.TargetForm == null) return false;

			EditControlForm dlg = new EditControlForm();
			dlg.ControlType= m_ct;
			dlg.SetMainForm(this,forms.TargetForm,null);
			if(dlg.ShowDialog(forms.TargetForm) ==DialogResult.OK )
			{
				m_ct= dlg.ControlType;
				AddControl(forms.TargetForm, dlg.ControlType, dlg.ControlName, dlg.ControlText,dlg.Font);
				return true;
			}
			return false;
		}
		
		public bool DeleteControl()
		{
			bool ret = false;
			if (m_IsEditMode == false) return ret;

			HyperBaseForm? bf = forms.TargetForm;
			if (bf != null)
			{
				Control? c = bf.TargetControl;
				if (c != null)
				{
					if (c is HyperMenuBar) return ret;
					bf.Controls.Remove(c);
					bf.ChkControls();
					bf.Invalidate();
					ret = true;
					bf.OnDeletedControl(new HyperChangedEventArgs(bf, null));
				}
			}

			return ret;
		}       // *************************************************************************
				// *************************************************************************
		public bool ShowEditControl()
		{
			if (m_IsEditMode == false) return false;
			if (targetForm == null) return false;
			if (targetControl == null) return false;
			EditControlForm dlg = new EditControlForm();
			dlg.SetMainForm(this, targetForm, targetControl);
			if (dlg.ShowDialog(forms.TargetForm) == DialogResult.OK)
			{
				switch (dlg.DROption)
				{
					case DROption.Script:
						ExecScriptEdit();
						break;
					case DROption.Font:
						ShowFontDialog();
						break;
					case DROption.FileOpen:
						ShowFileNameDialog();
						break;
					case DROption.Content:
						ShowEditContent();
						break;
					case DROption.Connect:
						break;
					case DROption.Icon:
						ShowPictLibDialog();
						break;
					case DROption.OK:
						if (targetControl.Name != dlg.ControlName)
						{
							targetControl.SetName(dlg.ControlName);
							Script.InitControls(this);
							OnControlChanged(new HyperChangedEventArgs(targetForm, targetControl));
						}
						if (targetControl.Text != dlg.ControlText) targetControl.Text = dlg.ControlText;
						break;
					case DROption.Cancel:
						break;
				}

				return true;
			}
			else
			{
				return false;
			}
		}

		// *************************************************************************
		public bool ShowControlList()
		{
			if (m_IsEditMode == false) return false;
			if (ControlList == null)
			{
				ControlList = new EditControlList();
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
				ControlList.Show(this);
			}
			else
			{
				if (ControlList.Visible == false)
				{
					ControlList.Visible = true;
					ControlList.Activate();
				}
				else
				{
					ControlList.Visible = false;
				}

			}


			return true;
		}
		// *************************************************************************
		public bool ShowAlignmentForm()
		{
			if (m_IsEditMode == false) return false;
			if (AlignmentForm == null)
			{
				EditAlignmentForm AlignmentForm  = new EditAlignmentForm();
				AlignmentForm.SetMainForm(this);
				if (AlignmentFormBounds.Left == -1)
				{
					AlignmentForm.Location = new Point(
						this.Right + 5,
						this.Top);
				}
				else
				{
					AlignmentForm.Location = AlignmentFormBounds.Location;
				}
				AlignmentForm.Show(this);
			}
			else
			{
				if (AlignmentForm.Visible == false)
				{
					AlignmentForm.Visible = true;
					AlignmentForm.Activate();
				}
				else
				{
					AlignmentForm.Visible = false;
				}

			}


			return true;
		}
		// *************************************************************************
		public bool ShowOutputForm()
		{
			if (OutputForm == null)
			{
				OutputForm = new JSOutputForm();
				OutputForm.MainForm =this;
				if (OutputForm != null) OutputForm.Font = OutputFormFont;
				if (OutputFormBounds.Left == -1)
				{
					OutputForm.Location = new Point(
						this.Left,
						this.Bottom);
				}
				else
				{
					OutputForm.Bounds = OutputFormBounds;
				}
				OutputForm.LocationChanged += (sender,e)=>
				{
					OutputFormBounds = OutputForm.Bounds;
				};
				OutputForm.SizeChanged += (sender, e) =>
				{
					OutputFormBounds = OutputForm.Bounds;
				};
				OutputForm.Show(this);
			}
			else
			{
				if (OutputForm.Visible == false)
				{
					OutputForm.Visible = true;
					OutputForm.Activate();
				}
				else
				{
					OutputForm.Visible = true;
				}

			}


			return true;
		}
		// *************************************************************************
		public bool ShowInputForm()
		{
			if (InputForm == null)
			{
				InputForm = new JSInputForm();
				InputForm.MainForm = this;
				if(InputFormFont!=null) InputForm.Font= InputFormFont;
				if (InputFormBounds.Left == -1)
				{
					InputForm.Location = new Point(
						this.Left,
						this.Bottom);
				}
				else
				{
					InputForm.Bounds = InputFormBounds;
				}
				InputForm.LocationChanged += (sender, e) =>
				{
					InputFormBounds = InputForm.Bounds;
				};
				InputForm.SizeChanged += (sender, e) =>
				{
					InputFormBounds = InputForm.Bounds;
				};
				InputForm.Show(this);
			}
			else
			{
				if (InputForm.Visible == false)
				{
					InputForm.Visible = true;
					InputForm.Activate();
				}
				else
				{
					InputForm.Visible = true;
				}
			}


			return true;
		}
		// *************************************************************************
		public void OutputWrite(object? o)
		{
			if (OutputForm == null) ShowOutputForm();
			if(OutputForm!=null)
			{
				if (OutputForm.Visible == false) OutputForm.Visible = true;
				OutputForm.write(o);
			}
		}
		// *************************************************************************
		public void OutputWriteLine(object? o)
		{
			if (OutputForm == null) ShowOutputForm();
			if (OutputForm != null)
			{
				if (OutputForm.Visible == false) OutputForm.Visible = true;
				OutputForm.writeLine(o);
			}
		}
		public void OutputClear()
		{
			if (OutputForm == null) return;
			if (OutputForm.Visible == false) OutputForm.Visible = true;
			OutputForm.clear();
		}
		// *************************************************************************
		public bool OpenScriptEdit()
		{
			if (ScriptEdit == null)
			{
				ScriptEdit = new ScriptEditor();
				ScriptEdit.SetMainForm(this);
				if (ScriptEditBounds.Left == -1)
				{
					ScriptEdit.Location = new Point(
						this.Left,
						this.Top);
				}
				else
				{
					ScriptEdit.Bounds = ScriptEditBounds;
				}
				ScriptEdit.Show(this);
			}
			else
			{
				ScriptEdit.Visible = true;
			}
			return true;
		}
		// *************************************************************************
		public bool ExecScriptEdit()
		{
			bool ret = false;
			OpenScriptEdit();
			if (ScriptEdit != null)
			{
				ScriptEdit.SetTargetControl(targetForm, targetControl);
			}
			return true;
		}
		public bool ShowPictLibDialog()
		{
			bool ret = false;
			if (m_IsEditMode == false) return ret;
			if (targetControl == null) return ret;
			if ((targetControl.ControlType == ControlType.Icon)
				|| (targetControl.ControlType == ControlType.DriveIcons)
				|| (targetControl.ControlType == ControlType.Button))
			{
				EditPictLibDialog dlg = new EditPictLibDialog();
				dlg.SetMainForm(this);
				if(targetControl is HyperIcon)
				{
					dlg.PictName = ((HyperIcon)targetControl).PictName;
				}else if (targetControl is HyperDriveIcons)
				{
					dlg.PictName = ((HyperDriveIcons)targetControl).PictName;
				}
				else if (targetControl is HyperButton)
				{
					dlg.PictDownMode= true;
					dlg.PictName = ((HyperButton)targetControl).PictName;
					dlg.PictName_Down = ((HyperButton)targetControl).PictName_Down;

				}
				dlg.StartPosition = FormStartPosition.CenterParent;
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					if(targetControl is HyperIcon)
					{
						((HyperIcon)targetControl).PictName = dlg.PictName;
					}else if (targetControl is HyperDriveIcons)
					{
						((HyperDriveIcons)targetControl).PictName = dlg.PictName;
					}
					else if (targetControl is HyperButton)
					{
						((HyperButton)targetControl).PictName = dlg.PictName;
						((HyperButton)targetControl).PictName_Down = dlg.PictName_Down;
					}
					ret = true;
				}
			}
			return ret;
		}
		public bool ShowFontDialog()
		{
			bool ret = false;
			if (m_IsEditMode == false) return ret;
			if (targetControl == null) return ret;
			FontDialog dlg = new FontDialog();
			dlg.Font = targetControl.Font;
			if(dlg.ShowDialog(targetForm)==DialogResult.OK)
			{
				targetControl.Font = dlg.Font;
				ret = true;
			}
			return ret;
		}
		public bool ShowFileNameDialog()
		{
			bool ret = false;
			if (m_IsEditMode == false) return ret;
			if (targetControl == null) return ret;
			if (targetControl.ControlType != ControlType.PictureBox) return ret;
			HyperPictureBox pb = (HyperPictureBox)targetControl;
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "*.png|*.png|*.jpg|*.jpg|*.*|*.*";
			if(pb.FileName!="")
			{
				dlg.InitialDirectory= Path.GetDirectoryName(pb.FileName);
				dlg.FileName = Path.GetFileName(pb.FileName);
			}
			if (dlg.ShowDialog(targetForm) == DialogResult.OK)
			{
				pb.FileName = dlg.FileName;
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
				int idx = Lib.IndexOfBitmap(nm);
				if(idx >=0)
				{
					MessageBox.Show("同じ名前のファイルがあります。");
					return false;
				}

				if(Lib.AddUserPict(ofd.FileName)==true)
				{
					MessageBox.Show("[ "+nm + " ]を追加しました。");
					ret = true;
				}
			}
			return ret;
		}
		public bool ShowEditContent()
		{
			bool ret = false;
			if (m_IsEditMode == false) return ret;
			if(targetControl==null) return ret;
			EditLines dlg = new EditLines();
			dlg.Text = "Edit Content";
			switch (targetControl.ControlType)
			{
				case ControlType.ListBox:
					dlg.Multiline = true;
					dlg.ControlLines = ((HyperListBox)targetControl).Lines;
					break;
				case ControlType.DropdownList:
					dlg.Multiline = true;
					dlg.ControlLines =((HyperDropdownList)targetControl).Lines;
					break;
				case ControlType.RadioButton:
					dlg.Multiline = true;
					dlg.ControlLines =((HyperRadioButton)targetControl).Lines;
					break;

				default:
					dlg.Multiline = false;
					dlg.ControlText = targetControl.Text;

					break;
			}
			if(dlg.ShowDialog(targetForm)==DialogResult.OK)
			{
				switch (targetControl.ControlType)
				{
					case ControlType.ListBox:
						((HyperListBox)targetControl).Lines = dlg.ControlLines;
						break;
					case ControlType.DropdownList:
						((HyperDropdownList)targetControl).Lines = dlg.ControlLines;
						break;
					case ControlType.RadioButton:
						((HyperRadioButton)targetControl).Lines = dlg.ControlLines;
						break;

					default:
						targetControl.Text = dlg.ControlText;

						break;
				}
			}
			return ret;
		}

		public bool ControlToUp()
		{
			bool ret = false;
			if (m_IsEditMode == false) return ret;
			if ((targetForm!=null)&&(targetControl!=null))
			{
				ret =targetForm.ControlToUp(targetControl);
			}
			return ret;
		}
		public bool ControlToDown()
		{
			bool ret = false;
			if (m_IsEditMode == false) return ret;
			if ((targetForm != null) && (targetControl != null))
			{
				ret = targetForm.ControlToDown(targetControl);
			}
			return ret;
		}
		public bool ControlToFront()
		{
			bool ret = false;
			if (m_IsEditMode == false) return ret;
			if ((targetForm != null) && (targetControl != null))
			{
				ret = targetForm.ControlToFront(targetControl);
			}
			return ret;
		}
		public bool ControlToFloor()
		{
			bool ret = false;
			if (m_IsEditMode == false) return ret;
			if ((targetForm != null) && (targetControl != null))
			{
				ret = targetForm.ControlToFloor(targetControl);
			}
			return ret;
		}
		public bool CopyControl()
		{
			if (m_IsEditMode == false) return false;
			bool ret = false;
			if ((targetForm != null) && (targetControl != null))
			{
				string js = targetControl.ToJsonCode();
				if((js!=null)&&(js!=""))
				{
					Clipboard.SetText(js);
					ret = true;
				}
			}
			return ret;
		}
		public bool CutControl()
		{
			if (m_IsEditMode == false) return false;
			bool ret = CopyControl();
			if(ret)
			{
				ret =DeleteControl();
			}
			return ret;
		}
		public bool PasteControl()
		{
			bool ret = false;
			if (m_IsEditMode == false) return ret;
			if (targetForm == null) return ret;
			string js = Clipboard.GetText();
			if(js==null) return false;
			HyperControl? hc = null;
			try
			{
				var doc = JsonNode.Parse(js);
				if (doc != null)
				{
					var obj = (JsonObject?)doc;
					if(obj != null)
					{
						if (obj.ContainsKey("ControlType"))
						{
							int? mt = obj["ControlType"].GetValue<int?>();
							if (mt != null)
							{
								ControlType ct = (ControlType)mt;
								hc = (HyperControl?)CreateControl(ct);
								if (hc != null)
								{
									hc.FromJson(obj);
									hc.LocationChanged += (sender,e)=>
									{
										targetForm.Invalidate();
									};
									hc.SetName(IsNameChkMake(hc.Name));
									hc.Location = new Point(hc.Location.X + 50, hc.Location.Y + 50);
									targetForm.Controls.Add(hc);
									targetForm.Controls.SetChildIndex(hc,1);
									targetForm.ChkControls();
									targetForm.OnCreatedControl(new HyperChangedEventArgs(targetForm, hc));
								}
							}
						}
					}
				}
			}
			catch
			{
				return false;
			}
			return ret;
		}
	}
}
