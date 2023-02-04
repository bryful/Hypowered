using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hpd
{
	public partial class EditControlTree : Control
	{
		public delegate void TreeViewHandler(object sender, TreeViewEventArgs e);
		public event TreeViewHandler? AfterSelect;
		protected virtual void OnAfterSelect(TreeViewEventArgs e)
		{
			if (AfterSelect != null)
			{
				AfterSelect(this, e);
			}
			ChkEnabled();
		}
		public TreeNode SelectedNode
		{
			get { return m_TreeView.SelectedNode; }
			set { m_TreeView.SelectedNode = value; }
		}
		public Control? SelectedControl
		{
			get
			{
				Control? ret = null;
				if(m_TreeView.SelectedNode!=null)
				{
					if (m_TreeView.SelectedNode.Tag is Control)
					{
						ret = (Control)m_TreeView.SelectedNode.Tag;
					}
				}
				return ret;
			}
			set
			{
				if (value != null)
				{
					TreeNode? nd = m_TreeView.Find(value);
					if (nd != null)
					{
						m_TreeView.SelectedNode = nd;
					}
				}
			}
		}
		protected ControlTreeView m_TreeView = new ControlTreeView();

		protected ToolStrip m_Menu = new ToolStrip();
		protected ToolStripButton m_BtnScript = new ToolStripButton();
		protected ToolStripButton m_BtnNew = new ToolStripButton();
		protected ToolStripButton m_BtnUp = new ToolStripButton();
		protected ToolStripButton m_BtnDown = new ToolStripButton();
		protected ToolStripButton m_BtnDel = new ToolStripButton();
		protected ToolStripButton m_BtnCut = new ToolStripButton();
		protected ToolStripButton m_BtnPaste = new ToolStripButton();
		[Category("Hypowered")]
		public ControlTreeView TreeView
		{
			get { return m_TreeView; }
		}
		[Category("Hypowered")]
		public Form? Form
		{
			get { return m_TreeView.Form; }
			set 
			{ 
				m_TreeView.Form = value;
				if(m_TreeView.Form!=null)
				{

				}
			}
		}
		public new string Text
		{
			get { return m_TreeView.Text; }
			set
			{
				base.Text = value;
				m_TreeView.Text = value;
			}
		}
		public EditControlTree()
		{
			m_Menu.Dock = DockStyle.Left;
			m_Menu.GripStyle = ToolStripGripStyle.Hidden;
			m_Menu.AutoSize = true;

			m_BtnScript.Name = nameof(m_BtnScript);
			m_BtnScript.Text = "Script";
			m_BtnScript.TextAlign = ContentAlignment.MiddleLeft;

			m_BtnNew.Name = nameof(m_BtnNew);
			m_BtnNew.Text = "New";
			m_BtnNew.Click += M_BtnNew_Click;
			m_BtnNew.TextAlign = ContentAlignment.MiddleLeft;

			m_BtnUp.Name = nameof(m_BtnUp);
			m_BtnUp.Text = "Up";
			m_BtnUp.Click += M_BtnUp_Click;
			m_BtnUp.TextAlign = ContentAlignment.MiddleLeft;

			m_BtnDown.Name = nameof(m_BtnDown);
			m_BtnDown.Text = "Down";
			m_BtnDown.Click += M_BtnDown_Click;
			m_BtnDown.TextAlign = ContentAlignment.MiddleLeft;

			m_BtnDel.Name = nameof(m_BtnDel);
			m_BtnDel.Text = "Del";
			m_BtnDel.Click += M_BtnDel_Click;
			m_BtnDel.TextAlign = ContentAlignment.MiddleLeft;

			m_BtnCut.Name = nameof(m_BtnCut);
			m_BtnCut.Text = "Cut";
			m_BtnCut.Click += M_BtnCut_Click;
			m_BtnCut.TextAlign = ContentAlignment.MiddleLeft;

			m_BtnPaste.Name = nameof(m_BtnPaste);
			m_BtnPaste.Text = "Paste";
			m_BtnPaste.Click += M_BtnPaste_Click;
			m_BtnPaste.TextAlign = ContentAlignment.MiddleLeft;

			m_Menu.Items.Add(m_BtnScript);
			m_Menu.Items.Add(m_BtnNew);
			m_Menu.Items.Add(m_BtnUp);
			m_Menu.Items.Add(m_BtnDown);
			m_Menu.Items.Add(m_BtnCut);
			m_Menu.Items.Add(m_BtnPaste);
			m_Menu.Items.Add(new ToolStripSeparator());
			m_Menu.Items.Add(m_BtnDel);

			this.Size = new Size(100,250);
			m_TreeView.Location = new Point(m_Menu.Width, 0);
			m_TreeView.Size = new Size(this.Width- m_Menu.Width, this.Height);
			m_TreeView.Name = "TreeView";
			m_TreeView.Text = "TreeView";


			this.Controls.Add(m_Menu);
			this.Controls.Add(m_TreeView);

			InitializeComponent();
			m_TreeView.AfterSelect += (sender, e) =>
			{
				OnAfterSelect(e);
			};
			ChkEnabled();
		}

		private void M_BtnPaste_Click(object? sender, EventArgs e)
		{
			if ((m_TreeView.Form != null) && (m_TreeView.Form is HpdMainForm))
			{
				HpdControl? n = ((HpdMainForm)m_TreeView.Form).PasteCtrl();
				if ( n != null)
				{
					m_TreeView.RefreshTreeView();
					SelectedControl = n;
				}
			}
		}

		private void M_BtnCut_Click(object? sender, EventArgs e)
		{
			if ((m_TreeView.Form !=null)&&(m_TreeView.Form is HpdMainForm))
			{
				if (((HpdMainForm)m_TreeView.Form).CutCtrl() !=null)
				{
					m_TreeView.RefreshTreeView();
				}
			}
		}
		private void M_BtnDel_Click(object? sender, EventArgs e)
		{
			Control? c = SelectedControl;
			if (c == null) return;
			if (c is HpdForm) return;
			if (c is HpdControl)
			{
				HpdControl hc = (HpdControl)c;
				using(YesNoForm dlg = new YesNoForm())
				{
					dlg.Text = $"{hc.Name}を削除しますか？";
					dlg.Title = "コントロールの削除";
					dlg.ShowCancel = false;
					if(dlg.ShowDialog()==DialogResult.Yes)
					{
						hc.RemoveMe();
					}
				}
			}
		}
			private void M_BtnDown_Click(object? sender, EventArgs e)
		{
			Control? c = SelectedControl;
			if (c == null) return;
			if (c is HpdForm) return;
			if (c is HpdControl)
			{
				if (((HpdControl)c).ControlMoveDown())
				{
					m_TreeView.RefreshTreeView();
					SelectedControl = c;
				}
			}
		}

		private void M_BtnUp_Click(object? sender, EventArgs e)
		{
			Control? c = SelectedControl;
			if (c == null) return;
			if (c is HpdForm) return;
			if (c is HpdControl) 
			{
				if( ((HpdControl)c).ControlMoveUp())
				{
					m_TreeView.RefreshTreeView();
					SelectedControl = c;
				}
			}
		}
		private void M_BtnNew_Click(object? sender, EventArgs e)
		{
			if (m_TreeView.SelectedNode == null) return;
			Control? c = (Control?)m_TreeView.SelectedNode.Tag;
			if (c == null) return;
			if (c is HpdMainForm)
			{
				((HpdMainForm)c).AddControl();
				m_TreeView.RefreshTreeView();
			} 
			else if (c is HpdForm)
			{
				((HpdForm)c).AddControl();
				m_TreeView.RefreshTreeView();
			}
			else if (c is  HpdPanel)
			{
				((HpdPanel)c).AddControl();
				m_TreeView.RefreshTreeView();
			}
			else if (c is HpdControl)
			{
				HpdForm? fm =  ((HpdControl)c).MainForm;
				if (fm != null)
				{
					fm.AddControl();
					m_TreeView.RefreshTreeView();
				}
			}

		}

		protected void ChkEnabled()
		{
			m_BtnNew.Enabled = false;
			m_BtnUp.Enabled = false;
			m_BtnDown.Enabled = false;
			m_BtnDel.Enabled = false;
			if (m_TreeView.SelectedNode == null) return;
			int idx = m_TreeView.SelectedNode.Index;
			int idxM = idx;
			if (m_TreeView.SelectedNode.Parent != null)
			{
				idxM = m_TreeView.SelectedNode.Parent.Nodes.Count;
			}
			m_BtnNew.Enabled = true;
			m_BtnUp.Enabled = ((idx>0)&&(idx < idxM));
			m_BtnDown.Enabled = (idx<idxM-1);
			m_BtnDel.Enabled = true;
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			m_TreeView.Size = new Size(this.Width - m_Menu.Width, this.Height);
			m_TreeView.Location = new Point(m_Menu.Width, 0);
		}
	}
}
