using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public class HTreeView : TreeView
	{
		bool _EventFlag = false;
		public object?[]? SelectObjects = null;
		// ****************************************
		public delegate void SelectObjectsChangedHandler(object sender, SelectObjectsChangedArgs e);
		public event SelectObjectsChangedHandler? SelectObjectsChanged;
		protected virtual void OnSelectObjectsChanged(SelectObjectsChangedArgs e)
		{
			if (_EventFlag) return;
			if (SelectObjectsChanged != null)
			{
				SelectObjectsChanged(this, e);
			}
		}
		// ****************************************
		public delegate void TargetChangedHandler(object sender, TargetControlChangedArgs e);
		public event TargetChangedHandler? TargetControlChanged;
		protected virtual void OnTargetControlChanged(TargetControlChangedArgs e)
		{
			if (_EventFlag) return;
			if (TargetControlChanged != null)
			{
				TargetControlChanged(this, e);
			}
		}
		// ****************************************************************
		private HForm? m_Form = null;
		public HForm? Form
		{
			get { return m_Form; }
			set
			{
				m_Form = value;
				ChkForm();
			}
		}
		public HControl? TargetControl
		{
			get
			{
				HControl? ret = null;
				if((this.SelectedNode != null)&&(this.SelectedNode is HTreeNode))
				{
					HTreeNode tn = (HTreeNode)this.SelectedNode;
					ret = tn.Control;
				}
				return ret;	
			}
			set
			{
				if (this.SelectedNode != null)
				{
					HTreeNode tn = (HTreeNode)this.SelectedNode;
					if (tn.Control == value)
					{
						return;
					}
				}
				if(value != null)
				{

					if(m_FormNode!=null)
						this.SelectedNode = m_FormNode.Nodes[value.Index];
				}
				else
				{
					this.SelectedNode = null;
				}
			}
		}
		private HTreeNode? m_FormNode = null;
		public HTreeView()
		{
			base.CheckBoxes = true;
			base.BackColor = Color.FromArgb(64, 64, 64);
			base.ForeColor = Color.FromArgb(230, 230, 230);
		}


		private void RescanControl()
		{
			if (m_FormNode != null)
			{
				m_FormNode.RescanControls();
				this.CollapseAll();
			}

		}
		private void RescanMainMenu()
		{
			if (m_FormNode != null)
			{
				m_FormNode.RescanMainMenu();
				this.CollapseAll();
			}

		}
		private void ChkForm()
		{
			if (m_Form != null)
			{
				AddForm(m_Form);
			}
		}

		private void AddForm(HForm hf)
		{
			this.Nodes.Clear();
			m_FormNode = null;
			m_Form = hf;
			if (m_Form!=null)
			{
				HTreeNode tn = new HTreeNode();
				tn.SetObject(hf);
				tn.AddMainMenu(hf.MainMenu);
				tn.AddControls(hf);
				this.Nodes.Add(tn);

				m_Form.TargetControlChanged -= M_Form_TargetControlChanged;
				m_Form.TargetControlChanged += M_Form_TargetControlChanged;
				m_Form.ControlChanged -= M_Form_ControlChanged;
				m_Form.ControlChanged += M_Form_ControlChanged;
				m_Form.ControlNameChanged -= M_Form_ControlNameChanged;
				m_Form.ControlNameChanged += M_Form_ControlNameChanged;
				m_FormNode = tn;
				this.CollapseAll();
			}
		}
		private void M_Form_ControlNameChanged(object sender, ControlChangedEventArgs e)
		{
			this.Nodes[e.CtrlIndex].Text = e.Name;
		}

		private void M_Form_ControlChanged(object sender, EventArgs e)
		{
			RescanControl();
		}

		private void M_Form_TargetControlChanged(object sender, TargetControlChangedArgs e)
		{
			TargetControl = e.HControl;
		}
		/*
		public void AddMainMenu(HForm hf)
		{
			HTreeNode tn = new HTreeNode();
			tn.SetObject(hf.MainMenu);
			if (hf.MainMenu.Items.Count > 0)
			{
				foreach (var mm in hf.MainMenu.Items)
				{
					if (mm is HMenuItem)
					{
						HMenuItem mm2 = (HMenuItem)mm;
						HTreeNode tn2 = new HTreeNode();
						tn2.SetObject(mm2);
						tn2.AddMenuItem(tn, mm2);
					}
				}
			}
			this.Nodes.Add(tn);
		}
		*/
	/*
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
		}*/
		// ***********************************************************************
		private object?[]? GetChecked()
		{
			List<object?> list = new List<object?>();
			if(this.Nodes.Count > 0)
			{
				foreach (var node in this.Nodes)
				{
					if (node is HTreeNode)
					{
						HTreeNode tn = (HTreeNode)node;
						if (tn.Checked==true)
						{
							list.Add(tn.TObject);
						}
						list.AddRange(GetChecked(tn));
					}
				}
			}
			if((this.SelectedNode!=null))
			{
				bool b =false;
				HTreeNode nt = (HTreeNode)this.SelectedNode;
				if (list.Count > 0)
				{
					foreach (var node in list)
					{
						if (node == nt.TObject) b = true;
					}
				}
				if(b==false) list.Add(nt.TObject);
			}
			return list.ToArray();
		}
		private List<object?> GetChecked(HTreeNode tn)
		{
			List<object?> list = new List<object?>();
			if (tn.Nodes.Count > 0)
			{
				foreach (var node in tn.Nodes)
				{
					if (node is HTreeNode)
					{
						HTreeNode tn2 = (HTreeNode)node;
						if (tn2.Checked == true)
						{
							list.Add(tn2.TObject);
						}
						list.AddRange(GetChecked(tn2));
					}
				}
			}
			return list;
		}
		// ***********************************************************************
		private void SetChecked(bool b)
		{
			if (this.Nodes.Count > 0)
			{
				foreach (TreeNode node in this.Nodes)
				{
					node.Checked = b;
					SetChecked(node, b);
				}
			}
		}
		private void SetChecked(TreeNode tn,bool b)
		{
			if (tn.Nodes.Count > 0)
			{
				foreach (TreeNode node in tn.Nodes)
				{
					node.Checked = b;
					SetChecked(node, b);
				}
			}
		}
		// ***********************************************************************
		protected override void OnAfterCheck(TreeViewEventArgs e)
		{
			if (_EventFlag) return;
			if (e.Node is HTreeNode)
			{
				_EventFlag = true;
				HTreeNode tn = (HTreeNode)e.Node;
				if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
				{
					this.SelectedNode = e.Node;
					SelectObjects = GetChecked();
					if ((tn.NodeType == NodeType.Control) && (tn.Control != null))
					{
						tn.Control.Selected = e.Node.Checked;
					}
				}
				else
				{
					SetChecked(false);
					this.SelectedNode = e.Node;
					SelectObjects = new object?[] { tn.TObject };
					m_Form.SetSelectedAll(false);
					if ((tn.NodeType == NodeType.Control) && (tn.Control != null))
					{
						tn.Control.Selected = tn.Checked;
					}
				}
				_EventFlag = false;
				if (tn.Control != null)
				{
					OnTargetControlChanged(new TargetControlChangedArgs(tn.Control));
				}
				OnSelectObjectsChanged(new SelectObjectsChangedArgs(SelectObjects));
			}
		}
		// ***********************************************************************
		protected override void OnAfterSelect(TreeViewEventArgs e)
		{
			if (_EventFlag) return;
			if(e.Node is HTreeNode)
			{
				_EventFlag = true;
				HTreeNode tn = (HTreeNode)e.Node;
				if ( (Control.ModifierKeys& Keys.Shift)==Keys.Shift)
				{
					tn.Checked = ! tn.Checked;
					SelectObjects = GetChecked();
					if ((tn.NodeType == NodeType.Control)&&(tn.Control!=null))
					{
						tn.Control.Selected = tn.Checked;
					}

				}
				else
				{
					SetChecked(false);
					tn.Checked = true;
					SelectObjects = new object?[] { tn.TObject };
					m_Form.SetSelectedAll(false);
					if ((tn.NodeType == NodeType.Control) && (tn.Control != null))
					{
						tn.Control.Selected = tn.Checked;
					}
				}

				_EventFlag = false;
				if (tn.Control!=null)
				{
					OnTargetControlChanged(new TargetControlChangedArgs(tn.Control));
				}
				OnSelectObjectsChanged(new SelectObjectsChangedArgs(SelectObjects));
			}
		}
	}
}
