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
	public partial class HpdControlTree : HpdControl
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

		protected Button m_BtnEdit = new Button();
		protected Button m_BtnNew = new Button();
		protected Button m_BtnUp = new Button();
		protected Button m_BtnDown = new Button();
		protected Button m_BtnDel = new Button();
		[Category("Hypowered")]
		public ControlTreeView TreeView
		{
			get { return m_TreeView; }
		}
		[Category("Hypowered")]
		public Form? Form
		{
			get { return m_TreeView.Form; }
			set { m_TreeView.Form = value; }
		}
		public override void SetIsEdit(bool b)
		{
			m_IsEdit = b;
			m_TreeView.Visible = !b;
			this.Invalidate();
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
		public HpdControlTree()
		{
			SetHpdType(HpdType.ControlTree);

			Size sz = new Size(50, 23);
			int x = 0;
			int y = 0;
			m_BtnEdit.Name = nameof(m_BtnEdit);
			m_BtnEdit.Text = "Edit";
			m_BtnEdit.Size = sz;
			m_BtnEdit.Location = new Point(x, y);
			m_BtnEdit.GotFocus += (sender, e) => { m_TreeView.Focus(); };
			//m_BtnEdit.Click += M_BtnNew_Click;
			y += sz.Height;

			m_BtnNew.Name = nameof(m_BtnNew);
			m_BtnNew.Text = "New";
			m_BtnNew.Size = sz;
			m_BtnNew.Location = new Point(x, y);
			m_BtnNew.GotFocus += (sender, e) => { m_TreeView.Focus(); };
			m_BtnNew.Click += M_BtnNew_Click;
			y += sz.Height;

			m_BtnUp.Name = nameof(m_BtnUp);
			m_BtnUp.Text = "up";
			m_BtnUp.Size = sz;
			m_BtnUp.Location = new Point(x, y);
			m_BtnUp.GotFocus += (sender, e) => { m_TreeView.Focus(); };
			y += sz.Height;
			m_BtnDown.Name = nameof(m_BtnDown);
			m_BtnDown.Text = "dn";
			m_BtnDown.Size = sz;
			m_BtnDown.Location = new Point(x, y);
			m_BtnDown.GotFocus += (sender, e) => { m_TreeView.Focus(); };
			y += sz.Height;

			m_BtnDel.Name = nameof(m_BtnDel);
			m_BtnDel.Text = "del";
			m_BtnDel.Size = sz;
			m_BtnDel.Location = new Point(x, y);
			m_BtnDel.GotFocus += (sender, e) => { m_TreeView.Focus(); };
			y += sz.Height;


			this.Size = new Size(100,250);
			m_TreeView.Location = new Point(sz.Width, 0);
			m_TreeView.Size = new Size(this.Width, this.Height-sz.Height);
			m_TreeView.Name = "TreeView";
			m_TreeView.Text = "TreeView";

			this.Controls.Add(m_BtnEdit);
			this.Controls.Add(m_BtnNew);
			this.Controls.Add(m_BtnUp);
			this.Controls.Add(m_BtnDown);
			this.Controls.Add(m_BtnDel);

			this.Controls.Add(m_TreeView);

			InitializeComponent();
			m_TreeView.AfterSelect += (sender, e) =>
			{
				OnAfterSelect(e);
			};
			ChkEnabled();
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
				HpdForm? fm =  ((HpdControl)c).Root;
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
			m_TreeView.Size = new Size(this.Width, this.Height-25);
		}
	}
}
