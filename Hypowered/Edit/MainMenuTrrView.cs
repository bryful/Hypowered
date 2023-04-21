using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class MainMenuTreeView : TreeView
	{
		private MainForm? m_MainForm = null;

		public MainForm? MainForm
		{
			get { return m_MainForm; }
			set { SetMainForm(value); }
		}
		private HForm? m_HForm = null;
		public HForm? HForm
		{
			get { return m_HForm; }
			set { SetHForm(value); }
		}

		private HMainMenu? m_MainMenu = null;
		public HMainMenu? MainMenu { get { return m_MainMenu; } }
		public PropertyGrid? PropertyGrid { get; set; } = null;
		public void SetMainForm(MainForm? mf)
		{
			m_MainForm = mf;
			if(m_MainForm != null)
			{

				m_MainForm.TargetFormChanged -= (sender, e) => { SetHForm(m_MainForm.TargetForm); };
				m_MainForm.TargetFormChanged += (sender, e) => { SetHForm(m_MainForm.TargetForm); };
				SetHForm(m_MainForm.TargetForm);
			}
		}
		public void SetHForm(HForm? hf)
		{
			m_HForm = hf;
			if(m_HForm!=null)
			{
				m_MainMenu = m_HForm.MainMenu;
				if(m_MainMenu !=null)
				{
					ScanMenu();
				}
			}
			else
			{
				m_MainMenu = null;

			}
		}
		public void ScanMenu()
		{
			if (m_MainMenu == null) return;
			this.Nodes.Clear();
			if (m_MainMenu.RootMenus.Length > 0)
			{
				foreach (HRootMenu rmenu in m_MainMenu.RootMenus)
				{
					TreeNode tn = new TreeNode(rmenu.Name);
					tn.Tag = rmenu;
					if (rmenu.SubMenu.Length > 0)
					{
						foreach (HMenuItem hmenu in rmenu.SubMenu)
						{
							TreeNode htn = new TreeNode(hmenu.Name);
							htn.Tag = hmenu;
							tn.Nodes.Add(htn);
						}
					}
					this.Nodes.Add(tn);
				}
			}
		}
		public MainMenuTreeView()
		{
		}
		protected override void OnAfterSelect(TreeViewEventArgs e)
		{
			if (PropertyGrid == null) return;
			if (e.Node == null) return;
			PropertyGrid.SelectedObject = e.Node.Tag;
		}
	}
}
