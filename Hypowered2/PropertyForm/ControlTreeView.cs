using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Hpd
{
	public partial class ControlTreeView : TreeView
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
					if (m_form is HpdForm)
					{
						((HpdForm)m_form).Items.TargetControlChanged += (sender, e) =>
						{
							HpdControl? hc = e.Ctrl;
							if (hc != null)
							{
								this.SelectedNode = Find(hc);
							}
						};
					}
					if (m_form is HpdMainForm)
					{
						((HpdMainForm)m_form).NameChanged += (sender, e) =>
						{
							RefreshTreeView();
						};

					}
				}
				else
				{
					this.Nodes.Clear();
				}
			}

		}
		protected override void OnAfterSelect(TreeViewEventArgs e)
		{
			base.OnAfterSelect(e);
			if((m_form!=null)&&(m_form is HpdForm))
			{
				HpdForm hf = (HpdForm)m_form;
				if ((e.Node != null)&&(e.Node.Tag is HpdControl)) {

					hf.Items.TargetControl = (HpdControl?)e.Node.Tag;
				}
				else
				{
					hf.Items.TargetControl = null;
				}
			}
		}

		public ControlTreeView()
		{
			this.HideSelection= false;
			this.Nodes.Clear();
			InitializeComponent();
			RefreshTreeView();
		}
		protected override void InitLayout()
		{
			base.InitLayout();
			RefreshTreeView();
		}
		protected void ChkEnabled()
		{
		}
		private TreeNode Listup(Control ctrl)
		{
			TreeNode ret = new TreeNode(ctrl.Name);
			ret.Tag= ctrl;
			if ((ctrl is not HpdControl)||(ctrl is HpdPanel))
			{
				if (ctrl.Controls.Count > 0)
				{
					List<TreeNode> list = new List<TreeNode>();
					foreach (Control c in ctrl.Controls)
					{
						list.Add(Listup(c));
					}
					//list.Reverse();
					ret.Nodes.AddRange(list.ToArray());
				}
			}
			return ret;
		}
		public void RefreshTreeView()
		{
			this.Nodes.Clear();
			if (m_form != null)
			{
				TreeNode tn = Listup(m_form);
				this.Nodes.Add(tn);
				if (this.TopNode != null)
				{
					this.TopNode.Expand();
					this.ExpandAll();
				}
			}
		}
		public TreeNode? Find(Control c)
		{
			return Find(this.Nodes, c);
		}
		public TreeNode? Find(TreeNodeCollection tc, Control c)
		{
			TreeNode? ret = null;
			if( tc.Count > 0)
			{
				foreach(TreeNode node in tc)
				{
					if (node != null)
					{
						if( node.Tag is Control)
						{
							if( c.Equals((Control)node.Tag))
							{
								ret = node;
								return ret;
							}
						}
						if(node.Nodes.Count > 0)
						{
							ret = Find(node.Nodes, c);
							if(ret!=null) return ret;
						}
					}
				}
			}
			return ret;
		}

	}
}
