using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	partial class MainForm
	{
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
					hf.LoadFromHypf();
					hf.StartSettings();
					HForms.Add(hf);
					RescanForms();
					hf.Show(this);
					OnFormChanged(new EventArgs());
					SetTargetForm(hf);
				}
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
					HForms.Add(hf);
					RescanForms();
					hf.Show(this);
					OnFormChanged(new EventArgs());
					SetTargetForm(hf);
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
					if (HForms.Count <= 0)
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
		}       // **********************************************************
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
		public void WriteLine(object? obj)
		{
			ShowConsole();
			ConsoleForm.WriteLine(obj);
		}
		// ************************************************************
		public void Write(object? obj)
		{
			ShowConsole();
			ConsoleForm.Write(obj);
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
		public void ShowAddRootMenuDialog()
		{
			if (TargetForm == null) return;
			using (AddMenuDialog dlg = new AddMenuDialog())
			{
				dlg.SetMainForm(this);
				dlg.AtSubMenu = false;
				dlg.TopMost = this.TopMost;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					TargetForm.MainMenu.AddRootMenu(dlg.MenuName, dlg.MenuText);
				}
			}
		}
	}
}
