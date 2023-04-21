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

		public FormPanel FormPanel { get; set; } = new FormPanel();
		public ControlPanel ControlPanel { get; set; } = new ControlPanel();
		public MainMenuTreeView MainMenuTreeView { get; set; } = new MainMenuTreeView();
		public SplitContainer MenuPanel { get; set; } = new SplitContainer();
		public SplitContainer MainPanel { get; set; } = new SplitContainer();
		public int MainDistance
		{
			get { return MainPanel.SplitterDistance; }
			set { MainPanel.SplitterDistance = value; }
		}
		public int MenuDistance
		{
			get { return MenuPanel.SplitterDistance; }
			set
			{
				try
				{
					MenuPanel.SplitterDistance = value;

				}
				catch { }
			}
		}

		public MainForm? MainForm
		{
			get { return this.FormPanel.MainForm; }
			set
			{
				FormPanel.MainForm = value;
				MainMenuTreeView.MainForm = value;
			}
		}
		public HForm? HForm
		{
			get { return ControlPanel.HForm; }
			set
			{
				ControlPanel.HForm = value;
				MainMenuTreeView.HForm = value;
			}
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
				MainMenuTreeView.BackColor = value;
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
				MainMenuTreeView.ForeColor = value;
			}
		}
		public PropertyGrid? PropertyGrid
		{
			get { return FormPanel.PropertyGrid; }
			set
			{
				FormPanel.PropertyGrid = value;
				ControlPanel.PropertyGrid = value;
				MainMenuTreeView.PropertyGrid = value;
			}
		}
		public EditControl()
		{

			InitializeComponent();
			FormPanel.ControlPanel = this.ControlPanel;

			ForeColor = Color.FromArgb(220, 220, 220);
			BackColor = Color.FromArgb(64, 64, 64);

			FormPanel.Dock = DockStyle.Fill;
			ControlPanel.Dock = DockStyle.Fill;
			MainMenuTreeView.Dock = DockStyle.Fill;

			MainPanel.Panel1.Controls.Add(FormPanel);
			MainPanel.Panel2.Controls.Add(this.ControlPanel);
			MainPanel.Orientation = Orientation.Horizontal;
			MainPanel.SplitterDistance = 160;

			MainPanel.Dock = DockStyle.Fill;
			MenuPanel.Panel1.Controls.Add(MainPanel);
			MenuPanel.Panel2.Controls.Add(MainMenuTreeView);
			MenuPanel.Dock = DockStyle.Fill;
			MenuPanel.Orientation = Orientation.Horizontal;
			MenuPanel.SplitterDistance = 660;
			this.Controls.Add(MenuPanel);

			MainPanel.SplitterMoved += (sender, e) =>
			{ 
				Debug.WriteLine($"MainPanel:{MainPanel.SplitterDistance} MenuPanel:{MenuPanel.SplitterDistance}");
			};
			MenuPanel.SplitterMoved += (sender, e) =>
			{
				Debug.WriteLine($"MainPanel:{MainPanel.SplitterDistance} MenuPanel:{MenuPanel.SplitterDistance}");
			};
		}
	}
}
