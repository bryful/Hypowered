using Hpd;
using Microsoft.ClearScript;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered2
{
	public partial class HpdMenu : MenuStrip
	{
		private HpdMenuItem[] Menus = new HpdMenuItem[]
		{
			new HpdMenuItem(),
			new HpdMenuItem(),
			new HpdMenuItem(),
			new HpdMenuItem()
		};
		public HpdMenuItem FileMenu { get { return Menus[0]; } set { Menus[0] = value; } }
		public HpdMenuItem EditMenu { get { return Menus[1]; } set { Menus[1] = value; } }
		public HpdMenuItem UserMenu { get { return Menus[2]; } set { Menus[2] = value; } }
		public HpdMenuItem HelpMenu { get { return Menus[3]; } set { Menus[3] = value; } }

		private HpdMenuItem[][] SubMenus = new HpdMenuItem[0][];
		protected HpdForm? m_Root = null;
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public HpdForm? Root
		{
			get
			{
				Control? ret = m_Root;
				if (m_Root == null)
				{
					ret = (Control?)this.Parent;
					while ((ret != null) && (ret.Parent != null))
					{
						if (ret is HpdForm) break;
						ret = ret.Parent;
					}
					m_Root = (HpdForm?)ret;
				}

				return m_Root;
			}

		}
		public HpdMenu()
		{
			this.Name = "MainMenu";
			FileMenu.Name = "FileMenu";
			FileMenu.Text = "File";
			EditMenu.Name = "EditMenu";
			EditMenu.Text = "Edit";
			UserMenu.Name = "UserMenu";
			UserMenu.Text = "User";
			HelpMenu.Name = "HelpMenu";
			HelpMenu.Text = "Help";

			this.Items.AddRange(Menus);
			InitializeComponent();
		}
		// *****************************************************************************
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean CanOverflow
		{
			get { return base.CanOverflow; }
			set { base.CanOverflow = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.ToolStripGripStyle GripStyle
		{
			get { return base.GripStyle; }
			set { base.GripStyle = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean ShowItemToolTips
		{
			get { return base.ShowItemToolTips; }
			set { base.ShowItemToolTips = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean Stretch
		{
			get { return base.Stretch; }
			set { base.Stretch = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.ToolStripMenuItem MdiWindowListItem
		{
			get { return base.MdiWindowListItem; }
			set { base.MdiWindowListItem = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean AutoSize
		{
			get { return base.AutoSize; }
			set { base.AutoSize = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean AutoScroll
		{
			get { return base.AutoScroll; }
			set { base.AutoScroll = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Drawing.Size AutoScrollMargin
		{
			get { return base.AutoScrollMargin; }
			set { base.AutoScrollMargin = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Drawing.Size AutoScrollMinSize
		{
			get { return base.AutoScrollMinSize; }
			set { base.AutoScrollMinSize = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Drawing.Point AutoScrollPosition
		{
			get { return base.AutoScrollPosition; }
			set { base.AutoScrollPosition = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean AllowDrop
		{
			get { return base.AllowDrop; }
			set { base.AllowDrop = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean AllowItemReorder
		{
			get { return base.AllowItemReorder; }
			set { base.AllowItemReorder = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean AllowMerge
		{
			get { return base.AllowMerge; }
			set { base.AllowMerge = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.AnchorStyles Anchor
		{
			get { return base.Anchor; }
			set { base.Anchor = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.BindingContext BindingContext
		{
			get { return base.BindingContext; }
			set { base.BindingContext = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean CausesValidation
		{
			get { return base.CausesValidation; }
			set { base.CausesValidation = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.Cursor Cursor
		{
			get { return base.Cursor; }
			set { base.Cursor = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.ToolStripDropDownDirection DefaultDropDownDirection
		{
			get { return base.DefaultDropDownDirection; }
			set { base.DefaultDropDownDirection = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.DockStyle Dock
		{
			get { return base.Dock; }
			set { base.Dock = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.ToolStripGripDisplayStyle GripDisplayStyle
		{
			get { return base.GripDisplayStyle; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.Padding GripMargin
		{
			get { return base.GripMargin; }
			set { base.GripMargin = value; }
		}
		[Category("Hypowered"),  Browsable(true)]
		public new System.Drawing.Rectangle GripRectangle
		{
			get { return base.GripRectangle; }
		}
		[Category("Hypowered"),  Browsable(false)]
		public new System.Boolean HasChildren
		{
			get { return base.HasChildren; }
		}
		[Category("Hypowered"),  Browsable(false)]
		public new System.Windows.Forms.HScrollProperties HorizontalScroll
		{
			get { return base.HorizontalScroll; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Drawing.Size ImageScalingSize
		{
			get { return base.ImageScalingSize; }
			set { base.ImageScalingSize = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.ImageList ImageList
		{
			get { return base.ImageList; }
			set { base.ImageList = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean IsCurrentlyDragging
		{
			get { return base.IsCurrentlyDragging; }
		}
		[Category("Hypowered"),Browsable(false)]
		public new System.Windows.Forms.ToolStripItemCollection Items
		{
			get { return base.Items; }
		}
		[Category("Hypowered"),  Browsable(false)]
		public new System.Boolean IsDropDown
		{
			get { return base.IsDropDown; }
		}
		[Category("Hypowered"),  Browsable(false)]
		public new System.Windows.Forms.LayoutSettings LayoutSettings
		{
			get { return base.LayoutSettings; }
			set { base.LayoutSettings = value; }
		}
		[Category("Hypowered"),  Browsable(false)]
		public new System.Windows.Forms.ToolStripLayoutStyle LayoutStyle
		{
			get { return base.LayoutStyle; }
			set { base.LayoutStyle = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.Layout.LayoutEngine LayoutEngine
		{
			get { return base.LayoutEngine; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.ToolStripOverflowButton OverflowButton
		{
			get { return base.OverflowButton; }
		}
		[Category("Hypowered"),  Browsable(false)]
		public new System.Windows.Forms.Orientation Orientation
		{
			get { return base.Orientation; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.ToolStripRenderer Renderer
		{
			get { return base.Renderer; }
			set { base.Renderer = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.ToolStripRenderMode RenderMode
		{
			get { return base.RenderMode; }
			set { base.RenderMode = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean TabStop
		{
			get { return base.TabStop; }
			set { base.TabStop = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.ToolStripTextDirection TextDirection
		{
			get { return base.TextDirection; }
			set { base.TextDirection = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.VScrollProperties VerticalScroll
		{
			get { return base.VerticalScroll; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.AccessibleObject AccessibilityObject
		{
			get { return base.AccessibilityObject; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.String AccessibleDefaultActionDescription
		{
			get { return base.AccessibleDefaultActionDescription; }
			set { base.AccessibleDefaultActionDescription = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.String AccessibleDescription
		{
			get { return base.AccessibleDescription; }
			set { base.AccessibleDescription = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.String AccessibleName
		{
			get { return base.AccessibleName; }
			set { base.AccessibleName = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.AccessibleRole AccessibleRole
		{
			get { return base.AccessibleRole; }
			set { base.AccessibleRole = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Drawing.Point AutoScrollOffset
		{
			get { return base.AutoScrollOffset; }
			set { base.AutoScrollOffset = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Drawing.Image BackgroundImage
		{
			get { return base.BackgroundImage; }
			set { base.BackgroundImage = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.ImageLayout BackgroundImageLayout
		{
			get { return base.BackgroundImageLayout; }
			set { base.BackgroundImageLayout = value; }
		}
		[Category("Hypowered"),  Browsable(false)]
		public new System.Int32 Bottom
		{
			get { return base.Bottom; }
		}
		[Category("Hypowered"),  Browsable(false)]
		public new System.Drawing.Rectangle Bounds
		{
			get { return base.Bounds; }
			set { base.Bounds = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean CanFocus
		{
			get { return base.CanFocus; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean CanSelect
		{
			get { return base.CanSelect; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean Capture
		{
			get { return base.Capture; }
			set { base.Capture = value; }
		}
		[Category("Hypowered"),	 Browsable(true)]
		public new System.Drawing.Rectangle ClientRectangle
		{
			get { return base.ClientRectangle; }
		}
		[Category("Hypowered"),  Browsable(true)]
		public new System.Drawing.Size ClientSize
		{
			get { return base.ClientSize; }
			set { base.ClientSize = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.String CompanyName
		{
			get { return base.CompanyName; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean ContainsFocus
		{
			get { return base.ContainsFocus; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.ContextMenuStrip ContextMenuStrip
		{
			get { return base.ContextMenuStrip; }
			set { base.ContextMenuStrip = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.ControlBindingsCollection DataBindings
		{
			get { return base.DataBindings; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Int32 DeviceDpi
		{
			get { return base.DeviceDpi; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean IsDisposed
		{
			get { return base.IsDisposed; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean Disposing
		{
			get { return base.Disposing; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean Enabled
		{
			get { return base.Enabled; }
			set { base.Enabled = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean Focused
		{
			get { return base.Focused; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.IntPtr Handle
		{
			get { return base.Handle; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean IsHandleCreated
		{
			get { return base.IsHandleCreated; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean InvokeRequired
		{
			get { return base.InvokeRequired; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean IsAccessible
		{
			get { return base.IsAccessible; }
			set { base.IsAccessible = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean IsAncestorSiteInDesignMode
		{
			get { return base.IsAncestorSiteInDesignMode; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean IsMirrored
		{
			get { return base.IsMirrored; }
		}
		[Category("Hypowered"),	Browsable(true)]
		public new System.Windows.Forms.Padding Margin
		{
			get { return base.Margin; }
			set { base.Margin = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.String Name
		{
			get { return base.Name; }
			set { base.Name = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.Control Parent
		{
			get { return base.Parent; }
			set { base.Parent = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.String ProductName
		{
			get { return base.ProductName; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.String ProductVersion
		{
			get { return base.ProductVersion; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean RecreatingHandle
		{
			get { return base.RecreatingHandle; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Drawing.Region Region
		{
			get { return base.Region; }
			set { base.Region = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.RightToLeft RightToLeft
		{
			get { return base.RightToLeft; }
			set { base.RightToLeft = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.ComponentModel.ISite Site
		{
			get { return base.Site; }
			set { base.Site = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Drawing.Size Size
		{
			get { return base.Size; }
			set { base.Size = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Int32 TabIndex
		{
			get { return base.TabIndex; }
			set { base.TabIndex = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Object Tag
		{
			get { return base.Tag; }
			set { base.Tag = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.String Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.Control TopLevelControl
		{
			get { return base.TopLevelControl; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean UseWaitCursor
		{
			get { return base.UseWaitCursor; }
			set { base.UseWaitCursor = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean Visible
		{
			get { return base.Visible; }
			set { base.Visible = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.IWindowTarget WindowTarget
		{
			get { return base.WindowTarget; }
			set { base.WindowTarget = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Drawing.Size PreferredSize
		{
			get { return base.PreferredSize; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.Padding Padding
		{
			get { return base.Padding; }
			set { base.Padding = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.ImeMode ImeMode
		{
			get { return base.ImeMode; }
			set { base.ImeMode = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.ComponentModel.IContainer Container
		{
			get { return base.Container; }
		}

	}
}
