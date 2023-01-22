using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BRY
{
	public partial class ControlTree : TreeView
	{
		private Form? m_form = null;
		public Form? Form
		{
			get { return m_form;}
			set
			{
				m_form = value; 
				if(m_form!= null)
				{
					RefreshTreeView();
					m_form.ControlAdded += (sender, e) =>
					{
						RefreshTreeView();
					};
					m_form.ControlRemoved += (sender, e) =>
					{
						RefreshTreeView();
					};
				}
				else
				{
					this.Nodes.Clear();
				}
			}

		}

		public ControlTree()
		{
			this.Nodes.Clear();
			InitializeComponent();
			RefreshTreeView();
		}
		protected override void InitLayout()
		{
			base.InitLayout();
			RefreshTreeView();
		}
		private TreeNode Listup(Control ctrl)
		{
			TreeNode ret = new TreeNode(ctrl.Name);
			ret.Tag= ctrl;
			if (ctrl.Controls.Count > 0)
			{
				List<TreeNode> list = new List<TreeNode>();
				foreach(Control c in ctrl.Controls)
				{
					list.Add(Listup(c));
				}
				list.Reverse();
				ret.Nodes.AddRange(list.ToArray());
			}
			return ret;
		}
		private void RefreshTreeView()
		{
			this.Nodes.Clear();
			if (m_form != null)
			{
				TreeNode tn = Listup(m_form);
				this.Nodes.Add(tn);
				if (this.TopNode != null)
				{
					this.TopNode.Expand();
				}
			}
		}

	}
}
