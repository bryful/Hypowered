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
		protected Button m_BtnUp = new Button();
		protected Button m_BtnDown = new Button();
		protected Button m_BtnIn = new Button();
		protected Button m_BtnOut = new Button();
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

			Size sz = new Size(35, 25);
			int x = 0;
			m_BtnUp.Name = nameof(m_BtnUp);
			m_BtnUp.Text = "up";
			m_BtnUp.Size = sz;
			m_BtnUp.Location = new Point(x, 0);
			m_BtnUp.GotFocus += (sender, e) => { m_TreeView.Focus(); };
			x += sz.Width;
			m_BtnDown.Name = nameof(m_BtnDown);
			m_BtnDown.Text = "dn";
			m_BtnDown.Size = sz;
			m_BtnDown.Location = new Point(x, 0);
			m_BtnDown.GotFocus += (sender, e) => { m_TreeView.Focus(); };
			x += sz.Width;

			m_BtnIn.Name = nameof(m_BtnIn);
			m_BtnIn.Text = "in";
			m_BtnIn.Size = sz;
			m_BtnIn.Location = new Point(x, 0);
			m_BtnIn.GotFocus += (sender, e) => { m_TreeView.Focus(); };
			x += sz.Width;
			m_BtnOut.Name = nameof(m_BtnOut);
			m_BtnOut.Text = "out";
			m_BtnOut.Size = sz;
			m_BtnOut.Location = new Point(x, 0);
			m_BtnOut.GotFocus += (sender, e) => { m_TreeView.Focus(); };
			x += sz.Width;
			m_BtnDel.Name = nameof(m_BtnDel);
			m_BtnDel.Text = "del";
			m_BtnDel.Size = sz;
			m_BtnDel.Location = new Point(x, 0);
			m_BtnDel.GotFocus += (sender, e) => { m_TreeView.Focus(); };


			this.Size = new Size(100,250);
			m_TreeView.Location = new Point(0, sz.Height);
			m_TreeView.Size = new Size(this.Width, this.Height-sz.Height);
			m_TreeView.Name = "TreeView";
			m_TreeView.Text = "TreeView";

			this.Controls.Add(m_BtnUp);
			this.Controls.Add(m_BtnDown);
			this.Controls.Add(m_BtnIn);
			this.Controls.Add(m_BtnOut);
			this.Controls.Add(m_BtnDel);

			this.Controls.Add(m_TreeView);

			InitializeComponent();
			m_TreeView.AfterSelect += (sender, e) =>
			{
				OnAfterSelect(e);
			};
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
