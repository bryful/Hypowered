using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup.Localizer;
using Hypowered.Properties;

namespace Hypowered
{
	public partial class EditControl : Control
	{
		// TODO: ボタンが効かない

		public object?[]? SelectObjects = null;
		// ****************************************
		public delegate void SelectObjectsChangedHandler(object sender, SelectObjectsChangedArgs e);
		public event SelectObjectsChangedHandler? SelectObjectsChanged;
		protected virtual void OnSelectObjectsChanged(SelectObjectsChangedArgs e)
		{
			SelectObjects = e.objs;
			if (SelectObjectsChanged != null)
			{
				SelectObjectsChanged(this, e);
			}
		}
		// ****************************************
		public FormPanel FormPanel { get; set; } = new FormPanel();
		public ControlPanel ControlPanel { get; set; } = new ControlPanel();
		public MenuPanel MenuPanel { get; set; } = new MenuPanel();
		public SplitContainer MenuSplit { get; set; } = new SplitContainer();
		public SplitContainer MainSplit { get; set; } = new SplitContainer();
		public int MainDistance
		{
			get { return MainSplit.SplitterDistance; }
			set 
			{ 
				if(MainSplit !=null)
				MainSplit.SplitterDistance = value;
			}
		}
		public int MenuDistance
		{
			get { return MenuSplit.SplitterDistance; }
			set
			{
				try
				{
					MenuSplit.SplitterDistance = value;

				}
				catch { }
			}
		}
		private int m_IsScript = 1;
		public bool IsScript
		{
			get
			{
				int ret = 0;
				if (FormPanel.MainForm!=null)
				{
					if (FormPanel.MainForm.IsScript)
					{
						ret = 1;
					}
				}
				m_IsScript = ret;
				if (ret==1)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			set
			{
				if (FormPanel.MainForm != null)
				{
					if (FormPanel.MainForm.IsScript)
					{
						m_IsScript = 1;
					}
					else
					{
						m_IsScript = 0;
					}
					this.Invalidate();
				}
			}
		}
		public MainForm? MainForm
		{
			get { return this.FormPanel.MainForm; }
			set
			{
				FormPanel.MainForm = value;
				if (FormPanel.MainForm != null)
				{
					IsScript = FormPanel.IsScript;
				}
				ControlPanel.MainForm = value;
				MenuPanel.MainForm = value;
			}
		}
		public int SelectedRootIndex
		{
			get { return MenuPanel.SelectedRootIndex; }
		}
		[Category("Hypowered_Color"),Browsable(false)]
		public SizeMoveMode SizeMoveMode
		{
			get { return ControlPanel.SizeMoveMode; }
		}
		[Category("Hypowered_Color")]
		public new Color BackColor
		{
			get { return base.BackColor; }
			set
			{
				base.BackColor = value;
				FormPanel.BackColor = value;
				ControlPanel.BackColor = value;
				MenuPanel.BackColor = value;
			}
		}
		[Category("Hypowered_Color")]
		public new Color ForeColor
		{
			get { return base.ForeColor; }
			set
			{
				base.ForeColor = value;
				FormPanel.ForeColor = value;
				ControlPanel.ForeColor = value;
				MenuPanel.ForeColor = value;
			}
		}
		public EditControl()
		{

			InitializeComponent();
			MainSplit.MinimumSize= new Size(150, 110);
			
			ForeColor = Color.FromArgb(220, 220, 220);
			BackColor = Color.FromArgb(64, 64, 64);
			FormPanel.ScriptModeChanged += (sender, e) =>
			{
				if (MainForm != null)
				{
					MainForm.IsScript = e.IsScript;
				}
			};

			FormPanel.Dock = DockStyle.Fill;
			ControlPanel.Dock = DockStyle.Fill;
			MenuPanel.Dock = DockStyle.Fill;

			MainSplit.Panel1.Controls.Add(FormPanel);
			MainSplit.Panel2.Controls.Add(this.ControlPanel);
			MainSplit.Orientation = Orientation.Horizontal;
			
			MainSplit.Dock = DockStyle.Fill;
			MenuSplit.Panel1.Controls.Add(MainSplit);
			MenuSplit.Panel2.Controls.Add(MenuPanel);
			MenuSplit.Dock = DockStyle.Fill;
			MenuSplit.Orientation = Orientation.Horizontal;
			this.Controls.Add(MenuSplit);

			this.Size = new Size(170, 400);
			MenuDistance = 350;
			MainDistance = 90;
			FormPanel.SelectObjectsChanged += (sender, e) => { OnSelectObjectsChanged(e); };
			ControlPanel.SelectObjectsChanged += (sender, e) => { OnSelectObjectsChanged(e); };
			MenuPanel.SelectObjectsChanged += (sender, e) => { OnSelectObjectsChanged(e); };

			ControlPanel.ArrowChanged += (sender, e) => { ControlArrowAction(e); };
			MenuPanel.MenuActionClick += (sender, e) => { PushMenuAction(e); };
		}
		protected override void InitLayout()
		{
			if (MainDistance < 90) MainDistance = 90;
			base.InitLayout();
		}
		public void PushMenuAction(MenuActionClickArgs e)
		{
			if(MainForm==null) return;
			switch(e.Mode)
			{
				case MenuAction.AddRoot:
					MainForm.ShowAddRootMenuDialog();
					break;
				case MenuAction.AddSub:
					MainForm.ShowAddSubMenuDialog();
					break;
			}
		}
		public void ControlArrowAction(ArrowChangedEventArgs e)
		{
			if ((MainForm == null) || (MainForm.TargetForm==null)) return;
			switch(SizeMoveMode)
			{
				case SizeMoveMode.Move:
					MainForm.TargetForm.ControlMove(e.Arrow, ControlPanel.MoveScaleValue);
						break;
				case SizeMoveMode.ResizeLeftTop:
					MainForm.TargetForm.ControlResizeLeftTop(e.Arrow, ControlPanel.MoveScaleValue);
					break;
				case SizeMoveMode.ResizeRightBottom:
					MainForm.TargetForm.ControlResizeRightBottom(e.Arrow, ControlPanel.MoveScaleValue);
					break;
			}
		}
		protected override void OnResize(EventArgs e)
		{
			if (MainDistance<90)
			{
				MainDistance = 90;
			}
			base.OnResize(e);
		}
	}
	
	public class SelectObjectsChangedArgs : EventArgs
	{
		public object?[]? objs;
		public SelectObjectsChangedArgs(object?[]? idx)
		{
			objs = idx;
		}
	}
}
