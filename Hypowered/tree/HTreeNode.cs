using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class HTreeNode : TreeNode
	{
		private NodeType m_NodeType = NodeType.Control;
		public NodeType NodeType { get { return m_NodeType; } }
		private HForm? m_Form = null;
		public HForm? Form { get { return m_Form; } }
		private HMainMenu? m_MainMenu = null;
		public HMainMenu? HMainMenu { get { return m_MainMenu; } }
		private HMenuItem? m_MenuItem = null;
		public HMenuItem? MenuItem { get { return m_MenuItem; } }
		private HControl? m_Control = null;
		public HControl? Control { get { return m_Control; } }
		
		public HTreeNode() 
		{
		}
		public void SetObject(Object o)
		{
			m_NodeType = NodeType.Control;
			m_Form = null;
			m_MainMenu = null;
			m_MenuItem = null;
			m_Control = null;
			if (o is HMainMenu)
			{
				m_MainMenu = (HMainMenu)o;
				this.Text = m_MainMenu.Name;
				m_NodeType = NodeType.MainManu;
			}
			else if (o is HMenuItem)
			{
				m_MenuItem = (HMenuItem)o;
				m_MenuItem.MenuNameChanged -= (sender, e) => { this.Text = e.Name; };
				m_MenuItem.MenuNameChanged += (sender, e) => { this.Text = e.Name; };
				this.Text = m_MenuItem.Name;
				m_NodeType = NodeType.MenuItem;
			}
			else if (o is HControl)
			{
				m_Control = (HControl)o;
				m_Control.ControlNameChanged -= (sender, e) => { this.Text = e.Name; };
				m_Control.ControlNameChanged += (sender, e) => { this.Text = e.Name; };
				this.Text = m_Control.Name;
				this.Checked = m_Control.Selected;
				m_NodeType = NodeType.Control;
			}
			else if (o is HForm)
			{
				m_Form = (HForm)o;
				m_Form.FormNameChanged -= (sender, e) => { this.Text = e.Name; };
				m_Form.FormNameChanged += (sender, e) => { this.Text = e.Name; };
				this.Text = m_Form.Name;
				m_NodeType = NodeType.Form;
			}
		}
		public object? TObject
		{
			get
			{
				switch (m_NodeType)
				{
					case NodeType.MainManu:
						return (object?)m_MainMenu;
					case NodeType.MenuItem:
						return (object?)m_MenuItem;
					case NodeType.Control:
						return (object?)m_Control;
					case NodeType.Form:
						return (object?)m_Form;
					default:
						return null;
				}
			}
			
		}

		public void AddMainMenu(HMainMenu mm)
		{
			HTreeNode tn = new HTreeNode();
			tn.SetObject(mm);
			if (mm.Items.Count > 0)
			{
				foreach (var mm2 in mm.Items)
				{
					if (mm2 is HMenuItem)
					{
						HMenuItem mm3 = (HMenuItem)mm2;
						HTreeNode tn2 = new HTreeNode();
						tn2.SetObject(mm3);
						AddMenuItem(tn,mm3);
					}
				}
			}
			this.Nodes.Add(tn);
		}
		private void AddMenuItem(HTreeNode tn, HMenuItem item)
		{
			HTreeNode tn2 = new HTreeNode();
			tn2.SetObject(item);
			if (item.DropDownItems.Count > 0)
			{
				foreach (var i in item.DropDownItems)
				{
					if (i is HMenuItem)
					{
						AddMenuItem(tn2, (HMenuItem)i);
					}
				}
			}
			tn.Nodes.Add(tn2);
		}
		public void AddControls(HForm hf)
		{
			if (hf.Controls.Count > 0)
			{
				for (int i = 0; i < hf.Controls.Count; i++)
				{
					if (hf.Controls[i] is HControl)
					{
						HTreeNode tn = new HTreeNode();
						tn.SetObject((HControl)hf.Controls[i]);
						this.Nodes.Add(tn);
					}
				}

			}
		}
		public void RescanControls()
		{
			if(m_Form != null)
			{
				RemoveControls();
				AddControls(m_Form);
			}
		}
		private void RemoveControls()
		{
			if(this.Nodes.Count > 0)
			{
				for(int i= this.Nodes.Count-1;i>=0;i--)
				{
					if (this.Nodes[i] is HTreeNode)
					{

						HTreeNode tn = (HTreeNode)this.Nodes[i];
						if(tn.NodeType== NodeType.Control)
						{
							this.Nodes.RemoveAt(i);
						}
					}
				}
			}
		}
		private void RemoveMainMenu()
		{
			if (this.Nodes.Count > 0)
			{
				for (int i = this.Nodes.Count - 1; i >= 0; i--)
				{
					if (this.Nodes[i] is HTreeNode)
					{

						HTreeNode tn = (HTreeNode)this.Nodes[i];
						if (tn.NodeType == NodeType.MainManu)
						{
							if(tn.Nodes.Count > 0)
							{
								foreach(TreeNode node in tn.Nodes)
								{
									tn.Nodes.Remove(node);
								}
							}
						}
					}
				}
			}
		}
		public void RescanMenu()
		{
			if ((this.NodeType != NodeType.MenuItem)||(m_MenuItem==null)) return;
			this.Nodes.Clear();
			if(m_MenuItem.DropDownItems.Count>0)
			{
				foreach (var i in m_MenuItem.DropDownItems)
				{
					if (i is HMenuItem)
					{
						AddMenuItem(this, (HMenuItem)i);
					}
				}
			}
		}
		public void RescanMainMenu()
		{
			if ((this.NodeType != NodeType.MainManu) || (m_MainMenu == null)) return;
			this.Nodes.Clear();
			if (m_MainMenu.Items.Count > 0)
			{
				foreach (var i in m_MainMenu.Items)
				{
					if (i is HMenuItem)
					{
						AddMenuItem(this, (HMenuItem)i);
					}
				}
			}
		}
	}
	public enum NodeType
	{
		Form,
		MainManu,
		MenuItem,
		Control
	}
}
