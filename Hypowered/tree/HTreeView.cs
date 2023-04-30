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
		private HForm? m_Form=null;
		public HForm? Form
		{
			get { return m_Form; }
			set
			{
				m_Form=value;
				ChkForm();
			}
		}

		public HTreeView()
		{
			base.CheckBoxes = true;
			base.BackColor = Color.FromArgb(64, 64, 64);
			base.ForeColor = Color.FromArgb(230, 230, 230);
		}
		private void ChkForm()
		{
			if(m_Form!=null)
			{
				AddForm(m_Form);
				m_Form.TargetControlChanged -= M_Form_TargetControlChanged;
				m_Form.TargetControlChanged += M_Form_TargetControlChanged;
				m_Form.ControlChanged -= M_Form_ControlChanged;
				m_Form.ControlChanged += M_Form_ControlChanged;
				m_Form.ControlNameChanged -= M_Form_ControlNameChanged;
				m_Form.ControlNameChanged += M_Form_ControlNameChanged;
			}
		}

		private void M_Form_ControlNameChanged(object sender, ControlChangedEventArgs e)
		{
			this.Nodes[e.CtrlIndex].Text = e.Name;
		}

		private void M_Form_ControlChanged(object sender, EventArgs e)
		{
			ChkForm();
		}

		private void M_Form_TargetControlChanged(object sender, TargetControlChangedArgs e)
		{
			if (e.HControl != null)
			{
				this.SelectedNode = this.Nodes[e.HControl.Index];
			}
		}

		private void AddForm(HForm hf)
		{
			this.Nodes.Clear();
			m_Form = hf;
			AddMainMenu(hf);
			AddControls(hf);
		}
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
		protected override void OnAfterCheck(TreeViewEventArgs e)
		{
			if (e.Node is HTreeNode)
			{
				HTreeNode tn = (HTreeNode)e.Node;
				if((tn.NodeType!= NodeType.Control)) 
				{
					if (tn.Checked == true)
					{
						tn.Checked = false;
						return;
					}
				}
			}
			base.OnAfterCheck(e);
		}
		protected override void OnAfterSelect(TreeViewEventArgs e)
		{
			if (e.Node is HTreeNode)
			{
				HTreeNode tn = (HTreeNode)e.Node;
				if ((tn.NodeType != NodeType.Control))
				{
					if (tn.Checked == true)
					{
						tn.Checked = false;
					}
				}
			}
			base.OnAfterSelect(e);
		}
	}
}
