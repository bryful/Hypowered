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
				this.Text = m_MenuItem.Name;
				m_NodeType = NodeType.MenuItem;
			}
			else if (o is HControl)
			{
				m_Control = (HControl)o;
				this.Text = m_Control.Name;
				m_NodeType = NodeType.Control;
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
					default:
						return null;
				}
			}
			
		}


		public void AddMenuItem(HTreeNode tn, HMenuItem item)
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
						HTreeNode tn2 = new HTreeNode();
						tn2.SetObject((HControl)hf.Controls[i]);
						this.Nodes.Add(tn2);
					}
				}

			}
		}
	}
	public enum NodeType
	{
		MainManu,
		MenuItem,
		Control
	}
}
