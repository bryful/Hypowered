using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class MainMenuTreeView : TreeView
	{
		public MainForm? MainForm = null;
		public HForm? HForm = null;
		public HMainMenu? MainMenu = null;
		public PropertyGrid? PropertyGrid { get; set; } = null;
		public void SetMainForm(MainForm? mf)
		{
			MainForm = mf;
			if(MainForm !=null)
			{

				MainForm.TargetFormChanged -= (sender, e) => { SetHForrm(MainForm.TargetForm); };
				MainForm.TargetFormChanged += (sender, e) => { SetHForrm(MainForm.TargetForm); };
				SetHForrm(MainForm.TargetForm);
			}
		}
		public void SetHForrm(HForm? hf)
		{
			HForm = hf;
			if(HForm!=null)
			{
				MainMenu = HForm.MainMenu;
				if(MainMenu !=null)
				{
					ScanMenu();
				}
			}
			else
			{
				MainMenu = null;

			}
		}
		public void ScanMenu()
		{
			if (MainMenu == null) return;
			this.Nodes.Clear();
			if (MainMenu.RootMenus.Length > 0)
			{
				foreach (HRootMenu rmenu in MainMenu.RootMenus)
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
	}
}
