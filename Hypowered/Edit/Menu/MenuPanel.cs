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
	public partial class MenuPanel : Control
	{
		#region Event
		// ***************************************************************************
		public delegate void SelectObjectsChangedHandler(object sender, SelectObjectsChangedArgs e);
		public event SelectObjectsChangedHandler? SelectObjectsChanged;
		protected virtual void OnSelectObjectsChanged(SelectObjectsChangedArgs e)
		{
			if (SelectObjectsChanged != null)
			{
				SelectObjectsChanged(this, e);
			}
		}
		// ***************************************************************************
		public delegate void MenuActionClickHandler(object sender, MenuActionClickArgs e);
		public event MenuActionClickHandler? MenuActionClick;
		protected virtual void OnMenuActionClick(MenuActionClickArgs e)
		{
			if (MenuActionClick != null)
			{
				MenuActionClick(this, e);
			}
		}
		#endregion
		public MainForm? MainForm
		{
			get { return MenuTreeView.MainForm; }
			set { MenuTreeView.MainForm =value; }
		}
		public void SetMainForm(MainForm? mf)
		{
			MenuTreeView.SetMainForm(mf);
		}
		public HForm? HForm
		{
			get { return MenuTreeView.HForm; }
			set { MenuTreeView.HForm = value; }
		}
		public new Color BackColor
		{
			get { return (Color)base.BackColor; }
			set
			{
				base.BackColor = value;
				MenuTreeView.BackColor = value;
				this.Invalidate();
			}
		}
		public new Color ForeColor
		{
			get { return (Color)base.ForeColor; }
			set
			{
				base.ForeColor = value;
				MenuTreeView.ForeColor = value;
				this.Invalidate();
			}
		}
		public HMainMenu? MainMenu { get { return MenuTreeView.MainMenu; } }
		private Bitmap[] MenuActionIcon = new Bitmap[6];
		private int m_MenuAction = 0;
		public int SelectedRootIndex
		{
			get { return MenuTreeView.SelectedRootIndex; }
		}
		// ***************************************************************************
		public MainMenuTreeView MenuTreeView { get; set; } = new MainMenuTreeView();
		public MenuPanel()
		{
			MenuActionIcon[0] = Properties.Resources.MenuAction0;
			MenuActionIcon[1] = Properties.Resources.MenuAction1;
			MenuActionIcon[2] = Properties.Resources.MenuAction2;
			MenuActionIcon[3] = Properties.Resources.MenuAction3;
			MenuActionIcon[4] = Properties.Resources.MenuAction4;
			MenuActionIcon[5] = Properties.Resources.MenuAction5;
			InitializeComponent();
			base.BackColor = Color.FromArgb(64, 64, 64);
			base.ForeColor = Color.FromArgb(230, 230, 230);
			this.DoubleBuffered = true;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.UpdateStyles();


			this.Controls.Add(this.MenuTreeView);
			MenuTreeView.BackColor = BackColor;
			MenuTreeView.ForeColor = ForeColor;
			MenuTreeView.BorderStyle = BorderStyle.FixedSingle;
			ControlLayout();
			MenuTreeView.SelectObjectsChanged +=(sender,e)=> { OnSelectObjectsChanged(e); };
		}
		protected override void InitLayout()
		{
			MenuTreeView.BackColor = BackColor;
			MenuTreeView.ForeColor = ForeColor;
			base.InitLayout();
		}
		public void ControlLayout()
		{
			MenuTreeView.Location = new Point(0, 22);
			MenuTreeView.Size = new Size(this.Width, this.Height-22);
		}
		protected override void OnResize(EventArgs e)
		{
			ControlLayout();
			base.OnResize(e);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			using(SolidBrush sb = new SolidBrush(BackColor))
			{
				Graphics g = e.Graphics;
				g.FillRectangle(sb, this.ClientRectangle);
				g.DrawImage(MenuActionIcon[m_MenuAction], 0, 0);
			}
		}
		private bool m_MD = false;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if(e.Y<20)
			{
				int x = e.X/30;
				if((x>=0)&&(x<=4))
				{
					m_MenuAction = x + 1;
					m_MD = true;
					this.Invalidate();
					return;
				}
				else
				{
					m_MenuAction = 0;
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if(m_MD)
			{
				MenuAction ma = (MenuAction)(m_MenuAction - 1);
				m_MenuAction = 0;
				m_MD=false;
				this.Invalidate();
				if ((MainForm != null) && (MainForm.TargetForm != null))
				{
					if (MainForm.TargetForm.IsEdit)
					{
						OnMenuActionClick(new MenuActionClickArgs(ma));
					}
				}
			}
			base.OnMouseUp(e);
		}

	}
	public enum MenuAction
	{
		AddRoot,
		AddSub,
		Up,
		Down,
		Delete
	}
	public class MenuActionClickArgs : EventArgs
	{
		public MenuAction Mode = MenuAction.AddRoot;
		public MenuActionClickArgs(MenuAction idx)
		{
			Mode = idx;
		}
	}
}
