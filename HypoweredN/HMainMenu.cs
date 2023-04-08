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
	public class HMainMenu : MenuStrip
	{
		public HMainMenu()
		{
			//HUtils.PropListToClipboard(typeof(MenuStrip),"base")
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean CanOverflow
		{
			get { return base.CanOverflow; }
			set { base.CanOverflow = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.ToolStripGripStyle GripStyle
		{
			get { return base.GripStyle; }
			set { base.GripStyle = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean ShowItemToolTips
		{
			get { return base.ShowItemToolTips; }
			set { base.ShowItemToolTips = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean Stretch
		{
			get { return base.Stretch; }
			set { base.Stretch = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ToolStripMenuItem MdiWindowListItem
		{
			get { return base.MdiWindowListItem; }
			set { base.MdiWindowListItem = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean AutoSize
		{
			get { return base.AutoSize; }
			set { base.AutoSize = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean AutoScroll
		{
			get { return base.AutoScroll; }
			set { base.AutoScroll = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.Size AutoScrollMargin
		{
			get { return base.AutoScrollMargin; }
			set { base.AutoScrollMargin = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.Size AutoScrollMinSize
		{
			get { return base.AutoScrollMinSize; }
			set { base.AutoScrollMinSize = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.Point AutoScrollPosition
		{
			get { return base.AutoScrollPosition; }
			set { base.AutoScrollPosition = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean AllowDrop
		{
			get { return base.AllowDrop; }
			set { base.AllowDrop = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean AllowItemReorder
		{
			get { return base.AllowItemReorder; }
			set { base.AllowItemReorder = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean AllowMerge
		{
			get { return base.AllowMerge; }
			set { base.AllowMerge = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.AnchorStyles Anchor
		{
			get { return base.Anchor; }
			set { base.Anchor = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Drawing.Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.BindingContext BindingContext
		{
			get { return base.BindingContext; }
			set { base.BindingContext = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean CausesValidation
		{
			get { return base.CausesValidation; }
			set { base.CausesValidation = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.Control.ControlCollection Controls
		{
			get { return base.Controls; }
		}
		[Category("Hypowered	"), Browsable(false)]
		public new System.Windows.Forms.Cursor Cursor
		{
			get { return base.Cursor; }
			set { base.Cursor = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Drawing.Font Font
		{
			get { return base.Font; }
			set { base.Font = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.ToolStripDropDownDirection DefaultDropDownDirection
		{
			get { return base.DefaultDropDownDirection; }
			set { base.DefaultDropDownDirection = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.DockStyle Dock
		{
			get { return base.Dock; }
			set { base.Dock = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.Rectangle DisplayRectangle
		{
			get { return base.DisplayRectangle; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Drawing.Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ToolStripGripDisplayStyle GripDisplayStyle
		{
			get { return base.GripDisplayStyle; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.Padding GripMargin
		{
			get { return base.GripMargin; }
			set { base.GripMargin = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.Rectangle GripRectangle
		{
			get { return base.GripRectangle; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean HasChildren
		{
			get { return base.HasChildren; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.HScrollProperties HorizontalScroll
		{
			get { return base.HorizontalScroll; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.Size ImageScalingSize
		{
			get { return base.ImageScalingSize; }
			set { base.ImageScalingSize = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ImageList ImageList
		{
			get { return base.ImageList; }
			set { base.ImageList = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean IsCurrentlyDragging
		{
			get { return base.IsCurrentlyDragging; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ToolStripItemCollection Items
		{
			get { return base.Items; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean IsDropDown
		{
			get { return base.IsDropDown; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.LayoutSettings LayoutSettings
		{
			get { return base.LayoutSettings; }
			set { base.LayoutSettings = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ToolStripLayoutStyle LayoutStyle
		{
			get { return base.LayoutStyle; }
			set { base.LayoutStyle = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.Layout.LayoutEngine LayoutEngine
		{
			get { return base.LayoutEngine; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ToolStripOverflowButton OverflowButton
		{
			get { return base.OverflowButton; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.Orientation Orientation
		{
			get { return base.Orientation; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ToolStripRenderer Renderer
		{
			get { return base.Renderer; }
			set { base.Renderer = value; }
		}
		[Category("Hypowered"), Browsable(false)]
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
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.VScrollProperties VerticalScroll
		{
			get { return base.VerticalScroll; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ScrollableControl.DockPaddingEdges DockPadding
		{
			get { return base.DockPadding; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.AccessibleObject AccessibilityObject
		{
			get { return base.AccessibilityObject; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.String AccessibleDefaultActionDescription
		{
			get { return base.AccessibleDefaultActionDescription; }
			set { base.AccessibleDefaultActionDescription = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.String AccessibleDescription
		{
			get { return base.AccessibleDescription; }
			set { base.AccessibleDescription = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.String AccessibleName
		{
			get { return base.AccessibleName; }
			set { base.AccessibleName = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.AccessibleRole AccessibleRole
		{
			get { return base.AccessibleRole; }
			set { base.AccessibleRole = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.Point AutoScrollOffset
		{
			get { return base.AutoScrollOffset; }
			set { base.AutoScrollOffset = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.Image BackgroundImage
		{
			get { return base.BackgroundImage; }
			set { base.BackgroundImage = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ImageLayout BackgroundImageLayout
		{
			get { return base.BackgroundImageLayout; }
			set { base.BackgroundImageLayout = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Int32 Bottom
		{
			get { return base.Bottom; }
		}
		[Category("Hypowered"), Browsable(false)]
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
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean Capture
		{
			get { return base.Capture; }
			set { base.Capture = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.Rectangle ClientRectangle
		{
			get { return base.ClientRectangle; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.Size ClientSize
		{
			get { return base.ClientSize; }
			set { base.ClientSize = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.String CompanyName
		{
			get { return base.CompanyName; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean ContainsFocus
		{
			get { return base.ContainsFocus; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ContextMenuStrip ContextMenuStrip
		{
			get { return base.ContextMenuStrip; }
			set { base.ContextMenuStrip = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean Created
		{
			get { return base.Created; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ControlBindingsCollection DataBindings
		{
			get { return base.DataBindings; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Int32 DeviceDpi
		{
			get { return base.DeviceDpi; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean IsDisposed
		{
			get { return base.IsDisposed; }
		}
		[Category("Hypowered"), Browsable(false)]
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
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean Focused
		{
			get { return base.Focused; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.IntPtr Handle
		{
			get { return base.Handle; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Int32 Height
		{
			get { return base.Height; }
			set { base.Height = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean IsHandleCreated
		{
			get { return base.IsHandleCreated; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean InvokeRequired
		{
			get { return base.InvokeRequired; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean IsAccessible
		{
			get { return base.IsAccessible; }
			set { base.IsAccessible = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean IsAncestorSiteInDesignMode
		{
			get { return base.IsAncestorSiteInDesignMode; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean IsMirrored
		{
			get { return base.IsMirrored; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Int32 Left
		{
			get { return base.Left; }
			set { base.Left = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Drawing.Point Location
		{
			get { return base.Location; }
			set { base.Location = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.Padding Margin
		{
			get { return base.Margin; }
			set { base.Margin = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Drawing.Size MaximumSize
		{
			get { return base.MaximumSize; }
			set { base.MaximumSize = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Drawing.Size MinimumSize
		{
			get { return base.MinimumSize; }
			set { base.MinimumSize = value; }
		}
		/*
		[Category("Hypowered"), Browsable(true)]
		public new System.String Name
		{
			get { return base.Name; }
			set { base.Name = value; }
		}
		*/
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.Control Parent
		{
			get { return base.Parent; }
			set { base.Parent = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.String ProductName
		{
			get { return base.ProductName; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.String ProductVersion
		{
			get { return base.ProductVersion; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean RecreatingHandle
		{
			get { return base.RecreatingHandle; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.Region Region
		{
			get { return base.Region; }
			set { base.Region = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Int32 Right
		{
			get { return base.Right; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.RightToLeft RightToLeft
		{
			get { return base.RightToLeft; }
			set { base.RightToLeft = value; }
		}
		[Category("Hypowered"), Browsable(false)]
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
		[Category("Hypowered"), Browsable(false)]
		public new System.Int32 Top
		{
			get { return base.Top; }
			set { base.Top = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.Control TopLevelControl
		{
			get { return base.TopLevelControl; }
		}
		[Category("Hypowered"), Browsable(false)]
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
		[Category("Hypowered"), Browsable(false)]
		public new System.Int32 Width
		{
			get { return base.Width; }
			set { base.Width = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.IWindowTarget WindowTarget
		{
			get { return base.WindowTarget; }
			set { base.WindowTarget = value; }
		}
		[Category("Hypowered"), Browsable(false)]
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
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ImeMode ImeMode
		{
			get { return base.ImeMode; }
			set { base.ImeMode = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.ComponentModel.IContainer? Container
		{
			get { return base.Container; }
		}
	}
}
