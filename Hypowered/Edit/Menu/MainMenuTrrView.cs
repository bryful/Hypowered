using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class MainMenuTreeView : TreeView
	{
		protected HMenuItem? TargetMenu = null;
		// ****************************************
		public delegate void SelectObjectsChangedHandler(object sender, SelectObjectsChangedArgs e);
		public event SelectObjectsChangedHandler? SelectObjectsChanged;
		protected virtual void OnSelectObjectsChanged(SelectObjectsChangedArgs e)
		{
			if (SelectObjectsChanged != null)
			{
				SelectObjectsChanged(this, e);
			}
		}
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
					ScanMainMenu();
					m_MainMenu.MenuChanged -= (sender, e) => { ScanMainMenu(); };
					m_MainMenu.MenuChanged += (sender, e) => { ScanMainMenu(); };
				}
			}
			else
			{
				m_MainMenu = null;

			}
		}
		public TreeNode ScanMainMenuSub(HMenuItem mi, TreeNodeCollection tnc)
		{
			TreeNode tn = new TreeNode(mi.Name);
			tn.Tag = mi;
			tnc.Add(tn);
			if (mi.DropDownItems.Count>0)
			{
				foreach (var rmenu in mi.DropDownItems)
				{
					if(rmenu is HMenuItem)
					{
						ScanMainMenuSub((HMenuItem)rmenu, tn.Nodes);
					}
				}
			}
			return tn;
		}
		public void ScanMainMenu()
		{
			if (m_MainMenu == null) return;
			this.Nodes.Clear();
			if (m_MainMenu.Items.Count > 0)
			{
				foreach (var rmenu in m_MainMenu.Items)
				{
					if(rmenu is HMenuItem)
					{
						ScanMainMenuSub((HMenuItem)rmenu, this.Nodes);
					}
				}
			}
		}
		public MainMenuTreeView()
		{
		}
		public HMenuItem? GetMenu(HMenuItem mi, List<int> list)
		{
			if ((MainMenu == null) || (list.Count <= 0) )return null;
			if ((list[0] >= 0) && (list[0] < mi.DropDownItems.Count))
			{
				ToolStripItem mi2 = mi.DropDownItems[list[0]];
				if (mi2 is HMenuItem)
				{
					if (list.Count == 1)
					{
						return (HMenuItem)mi2;
					}

				}
			}
			return null;
		}
		public HMenuItem? GetMenu(List<int> list)
		{
			if((MainMenu==null)||(list.Count<=0) )return null;
			if ((list[0]>=0)&&(list[0]<MainMenu.Items.Count))
			{
				ToolStripItem mi2 = MainMenu.Items[list[0]];
				if(mi2 is HMenuItem)
				{
					if (list.Count == 1)
					{
						return (HMenuItem)mi2;
					}
					return GetMenu((HMenuItem)mi2, list.Skip(1).ToList<int>());
				}
			}
			return null;

		}
		private TreeNode? m_SelectedNode = null;
		public int SelectedRootIndex
		{
			get
			{
				return GetSelectedRootRootIndex();
			}
		}

		public TreeNode? GetNode(TreeNode tn,List<int> list)
		{
			TreeNode? ret = null;
			if (list.Count <= 0) return ret;
			int idx = list[0];
			if ((idx > 0) && (idx < tn.Nodes.Count))
			{
				ret = tn.Nodes[idx];
				if ((ret != null) && (list.Count > 1))
				{
					ret = GetNode(ret, list.Skip(1).ToList<int>());
				}
			}
			return ret;
		}
		public TreeNode? GetNode(List<int> list)
		{
			TreeNode? ret = null;
			if(list.Count<=0)return ret;
			int idx = list[0];
			if((idx>0)&&(idx<this.Nodes.Count))
			{
				ret = this.Nodes[idx];
				if((ret!=null)&&(list.Count>1))
				{
					ret = GetNode(ret,list.Skip(1).ToList<int>());
				}
			}
			return ret;
		}
		public List<int>  NodeList( TreeNode tn)
		{
			List<int> list = new List<int>();
			list.Add(tn.Index);
			tn = tn.Parent;
			while (tn != null)
			{
				list.Add(tn.Index);
				tn = tn.Parent;
			}
			list.Reverse();
			return list;
		}
		public int GetSelectedRootRootIndex()
		{
			int ret = -1;
			if (this.SelectedNode!=null)
			{
				List<int> list = NodeList(this.SelectedNode);
				if(list.Count>0) ret = list[0];
			}
			return ret;
		}
		protected override void OnAfterSelect(TreeViewEventArgs e)
		{
			if ((e==null)||(e.Node == null)) return;
			List<int> list = NodeList(e.Node);
			HMenuItem? m = GetMenu(list);
			TargetMenu = m;
			if(TargetMenu!=null)
			{
				TargetMenu.MenuNameChanged -= (sender, e) => { ExecMenuNameChange(e); };
				TargetMenu.MenuNameChanged += (sender, e) => { ExecMenuNameChange(e); };
			}
			OnSelectObjectsChanged(new SelectObjectsChangedArgs(new object?[] { m }));
		}
		private void ExecMenuNameChange(MenuNameChangedEventArgs args)
		{
			if((TargetMenu!=null)&&(SelectedNode!=null))
			{
				if(SelectedNode.Name != args.Name)
				{
					SelectedNode.Name = args.Name;
					SelectedNode.Text = args.Name;
				}
			}
		}
	}
}
