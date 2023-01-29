using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ClearScript;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
namespace Hpd
{
	partial class HpdForm
	{

		[Browsable(false)]
		public new System.Windows.Forms.IButtonControl AcceptButton
		{
			get { return base.AcceptButton; }
			set { base.AcceptButton = value; }
		}
		[Browsable(false)]
		public new System.Windows.Forms.Form ActiveMdiChild
		{
			get { return base.ActiveMdiChild; }
		}
		[Browsable(false)]
		public new System.Boolean AllowTransparency
		{
			get { return base.AllowTransparency; }
			set { base.AllowTransparency = value; }
		}
		[Browsable(false)]
		public new System.Boolean AutoScale
		{
			get { return base.AutoScale; }
			set { base.AutoScale = value; }
		}
		[Browsable(false)]
		public new System.Drawing.Size AutoScaleBaseSize
		{
			get { return base.AutoScaleBaseSize; }
			set { base.AutoScaleBaseSize = value; }
		}
		/*
		[Browsable(false)]
		public new System.Boolean AutoScroll
		{
			get { return base.AutoScroll; }
			set { base.AutoScroll = value; }
		}
		*/
		[Category("Hypowered_layout")]
		public new System.Boolean AutoSize
		{
			get { return base.AutoSize; }
			set { base.AutoSize = value; }
		}
		[Category("Hypowered_layout")]
		public new System.Windows.Forms.AutoSizeMode AutoSizeMode
		{
			get { return base.AutoSizeMode; }
			set { base.AutoSizeMode = value; }
		}
		[Category("Hypowered_layout")]
		public new System.Windows.Forms.AutoValidate AutoValidate
		{
			get { return base.AutoValidate; }
			set { base.AutoValidate = value; }
		}
		/*
		[Category("Hypowered")]
		public new System.Windows.Forms.FormBorderStyle FormBorderStyle
		{
			get { return base.FormBorderStyle; }
			set { base.FormBorderStyle = value; }
		}
		*/
		[Browsable(false)]
		public new System.Windows.Forms.IButtonControl CancelButton
		{
			get { return base.CancelButton; }
			set { base.CancelButton = value; }
		}
		[Category("Hypowered_layout")]
		public new System.Drawing.Size ClientSize
		{
			get { return base.ClientSize; }
			set { base.ClientSize = value; }
		}
		[Browsable(false)]
		public new System.Boolean ControlBox
		{
			get { return base.ControlBox; }
			set { base.ControlBox = value; }
		}
		/*
[Category("Hypowered")]
public System.Drawing.Rectangle DesktopBounds
{
	get { return HpdForm.DesktopBounds; }
	set { HpdForm.DesktopBounds = value; }
}
[Category("Hypowered")]
public System.Drawing.Point DesktopLocation
{
	get { return HpdForm.DesktopLocation; }
	set { HpdForm.DesktopLocation = value; }
}
		*/
		[Category("Hypowered")]
		public new System.Windows.Forms.DialogResult DialogResult
		{
			get { return base.DialogResult; }
			set { base.DialogResult = value; }
		}
		[Browsable(false)]
		public new System.Boolean HelpButton
		{
			get { return base.HelpButton; }
			set { base.HelpButton = value; }
		}
		[Browsable(false)]
		public new System.Drawing.Icon Icon
		{
			get { return base.Icon; }
			set { base.Icon = value; }
		}
		[Browsable(false)]
		public new System.Boolean IsMdiChild
		{
			get { return base.IsMdiChild; }
		}
		[Browsable(false)]
		public new System.Boolean IsMdiContainer
		{
			get { return base.IsMdiContainer; }
			set { base.IsMdiContainer = value; }
		}
		[Browsable(false)]
		public new System.Boolean IsRestrictedWindow
		{
			get { return base.IsRestrictedWindow; }
		}
		/*
		[Category("Hypowered")]
		public new System.Boolean KeyPreview
		{
			get { return base.KeyPreview; }
			set { base.KeyPreview = value; }
		}
		*/
		[Category("Hypowered_layout")]
		public new System.Drawing.Size MaximumSize
		{
			get { return base.MaximumSize; }
			set { base.MaximumSize = value; }
		}
		[Browsable(false)]
		public new System.Windows.Forms.MenuStrip MainMenuStrip
		{
			get { return base.MainMenuStrip; }
			set { base.MainMenuStrip = value; }
		}
		[Category("Hypowered_layout")]
		public new System.Windows.Forms.Padding Margin
		{
			get { return base.Margin; }
			set { base.Margin = value; }
		}
		[Browsable(false)]
		public new System.Boolean MaximizeBox
		{
			get { return base.MaximizeBox; }
			set { base.MaximizeBox = value; }
		}
		[Browsable(false)]
		public new System.Windows.Forms.Form[] MdiChildren
		{
			get { return base.MdiChildren; }
		}
		[Browsable(false)]
		public new System.Boolean MdiChildrenMinimizedAnchorBottom
		{
			get { return base.MdiChildrenMinimizedAnchorBottom; }
			set { base.MdiChildrenMinimizedAnchorBottom = value; }
		}
		[Browsable(false)]
		public new System.Windows.Forms.Form MdiParent
		{
			get { return base.MdiParent; }
			set { base.MdiParent = value; }
		}
		[Browsable(false)]
		public new System.Boolean MinimizeBox
		{
			get { return base.MinimizeBox; }
			set { base.MinimizeBox = value; }
		}
		[Browsable(false)]
		public new System.Boolean Modal
		{
			get { return base.Modal; }
		}
		/*
		[Category("Hypowered")]
		public System.Double Opacity
		{
			get { return HpdForm.Opacity; }
			set { HpdForm.Opacity = value; }
		}
		*/
		[Browsable(false)]
		public new System.Windows.Forms.Form[] OwnedForms
		{
			get { return base.OwnedForms; }
		}
		[Browsable(false)]
		public new System.Windows.Forms.Form Owner
		{
			get { return base.Owner; }
			set { base.Owner = value; }
		}
		/*
		[Category("Hypowered")]
		public System.Drawing.Rectangle RestoreBounds
		{
			get { return HpdForm.RestoreBounds; }
			set { HpdForm.RestoreBounds = value; }
		}
		[Category("Hypowered")]
		public System.Boolean RightToLeftLayout
		{
			get { return HpdForm.RightToLeftLayout; }
			set { HpdForm.RightToLeftLayout = value; }
		}
		*/
		[Browsable(false)]
		public new System.Boolean ShowInTaskbar
		{
			get { return base.ShowInTaskbar; }
			set { base.ShowInTaskbar = value; }
		}
		[Browsable(false)]
		public new System.Boolean ShowIcon
		{
			get { return base.ShowIcon; }
			set { base.ShowIcon = value; }
		}
		[Browsable(false)]
		public new System.Windows.Forms.SizeGripStyle SizeGripStyle
		{
			get { return base.SizeGripStyle; }
			set { base.SizeGripStyle = value; }
		}
		/*
[Category("Hypowered")]
public System.Windows.Forms.FormStartPosition StartPosition
{
	get { return HpdForm.StartPosition; }
	set { HpdForm.StartPosition = value; }
}
[Category("Hypowered")]
public System.Int32 TabIndex
{
	get { return HpdForm.TabIndex; }
	set { HpdForm.TabIndex = value; }
}
[Category("Hypowered")]
public System.Boolean TabStop
{
	get { return HpdForm.TabStop; }
	set { HpdForm.TabStop = value; }
}
		*/
		[Browsable(false)]
		public new System.Boolean TopLevel
		{
			get { return base.TopLevel; }
			set { base.TopLevel = value; }
		}
		[Category("Hypowered_layout")]
		public new System.Boolean TopMost
		{
			get { return base.TopMost; }
			set { base.TopMost = value; }
		}
		[Category("Hypowered_layout")]
		public new System.Drawing.Color TransparencyKey
		{
			get { return base.TransparencyKey; }
			set { base.TransparencyKey = value; }
		}
		[Category("Hypowered_layout")]
		public new System.Windows.Forms.FormWindowState WindowState
		{
			get { return base.WindowState; }
			set { base.WindowState = value; }
		}
		[Browsable(false)]
		public new System.Drawing.SizeF AutoScaleDimensions
		{
			get { return base.AutoScaleDimensions; }
			set { base.AutoScaleDimensions = value; }
		}
		[Category("Hypowered_layout")]
		public new System.Windows.Forms.AutoScaleMode AutoScaleMode
		{
			get { return base.AutoScaleMode; }
			set { base.AutoScaleMode = value; }
		}
		[Browsable(false)]
		public new System.Windows.Forms.BindingContext BindingContext
		{
			get { return base.BindingContext; }
			set { base.BindingContext = value; }
		}
		/*
		[Category("Hypowered")]
		public System.Windows.Forms.Control ActiveControl
		{
			get { return HpdForm.ActiveControl; }
			set { HpdForm.ActiveControl = value; }
		}
		[Category("Hypowered")]
		public System.Drawing.SizeF CurrentAutoScaleDimensions
		{
			get { return HpdForm.CurrentAutoScaleDimensions; }
			set { HpdForm.CurrentAutoScaleDimensions = value; }
		}
		*/
		[Browsable(false)]
		public new System.Drawing.Size AutoScrollMargin
		{
			get { return base.AutoScrollMargin; }
			set { base.AutoScrollMargin = value; }
		}
		[Browsable(false)]
		public new System.Drawing.Point AutoScrollPosition
		{
			get { return base.AutoScrollPosition; }
			set { base.AutoScrollPosition = value; }
		}
		[Browsable(false)]
		public new System.Drawing.Size AutoScrollMinSize
		{
			get { return base.AutoScrollMinSize; }
			set { base.AutoScrollMinSize = value; }
		}
		/*
[Category("Hypowered")]
public System.Drawing.Rectangle DisplayRectangle
{
	get { return HpdForm.DisplayRectangle; }
	set { HpdForm.DisplayRectangle = value; }
}
[Category("Hypowered")]
public System.Windows.Forms.HScrollProperties HorizontalScroll
{
	get { return HpdForm.HorizontalScroll; }
	set { HpdForm.HorizontalScroll = value; }
}
[Category("Hypowered")]
public System.Windows.Forms.VScrollProperties VerticalScroll
{
	get { return HpdForm.VerticalScroll; }
	set { HpdForm.VerticalScroll = value; }
}
		*/
		[Browsable(false)]
		public new System.Windows.Forms.AccessibleObject AccessibilityObject
		{
			get { return base.AccessibilityObject; }
		}
		[Browsable(false)]
		public new System.String AccessibleDefaultActionDescription
		{
			get { return base.AccessibleDefaultActionDescription; }
			set { base.AccessibleDefaultActionDescription = value; }
		}
		[Browsable(false)]
		public new System.String AccessibleDescription
		{
			get { return base.AccessibleDescription; }
			set { base.AccessibleDescription = value; }
		}
		[Browsable(false)]
		public new System.String AccessibleName
		{
			get { return base.AccessibleName; }
			set { base.AccessibleName = value; }
		}
		[Browsable(false)]
		public new System.Windows.Forms.AccessibleRole AccessibleRole
		{
			get { return base.AccessibleRole; }
			set { base.AccessibleRole = value; }
		}
		/*
		[Category("Hypowered")]
		public System.Boolean AllowDrop
		{
			get { return HpdForm.AllowDrop; }
			set { HpdForm.AllowDrop = value; }
		}
		*/
		[Browsable(false)]
		public new System.Windows.Forms.AnchorStyles Anchor
		{
			get { return base.Anchor; }
			set { base.Anchor = value; }
		}
		[Browsable(false)]
		public new System.Drawing.Point AutoScrollOffset
		{
			get { return base.AutoScrollOffset; }
			set { base.AutoScrollOffset = value; }
		}
		[Browsable(false)]
		public new System.Windows.Forms.Layout.LayoutEngine LayoutEngine
		{
			get { return base.LayoutEngine; }
		}
		[Browsable(false)]
		public new System.Drawing.Image BackgroundImage
		{
			get { return base.BackgroundImage; }
			set { base.BackgroundImage = value; }
		}
		[Browsable(false)]
		public new System.Windows.Forms.ImageLayout BackgroundImageLayout
		{
			get { return base.BackgroundImageLayout; }
			set { base.BackgroundImageLayout = value; }
		}
		/*
[Category("Hypowered")]
public System.Int32 Bottom
{
	get { return HpdForm.Bottom; }
	set { HpdForm.Bottom = value; }
}
[Category("Hypowered")]
public System.Drawing.Rectangle Bounds
{
	get { return HpdForm.Bounds; }
	set { HpdForm.Bounds = value; }
}
[Category("Hypowered")]
public System.Boolean CanFocus
{
	get { return HpdForm.CanFocus; }
	set { HpdForm.CanFocus = value; }
}
[Category("Hypowered")]
public System.Boolean CanSelect
{
	get { return HpdForm.CanSelect; }
	set { HpdForm.CanSelect = value; }
}
		*/
		[Browsable(false)]
		public new System.Boolean Capture
		{
			get { return base.Capture; }
			set { base.Capture = value; }
		}
		[Browsable(false)]
		public new System.Boolean CausesValidation
		{
			get { return base.CausesValidation; }
			set { base.CausesValidation = value; }
		}
		[Category("Hypowered_layout")]
		public new System.Drawing.Rectangle ClientRectangle
		{
			get { return base.ClientRectangle; }
		}
		[Browsable(false)]
		public new System.String CompanyName
		{
			get { return base.CompanyName; }
		}
		[Browsable(false)]
		public new System.Boolean ContainsFocus
		{
			get { return base.ContainsFocus; }
		}

		[Browsable(false)]
		public new System.Windows.Forms.ContextMenuStrip ContextMenuStrip
		{
			get { return base.ContextMenuStrip; }
			set { base.ContextMenuStrip = value; }
		}
		/*
		[Category("Hypowered")]
		public System.Windows.Forms.Cursor Cursor
		{
			get { return HpdForm.Cursor; }
			set { HpdForm.Cursor = value; }
		}*/
		[Browsable(false)]
		public new System.Windows.Forms.ControlBindingsCollection DataBindings
		{
			get { return base.DataBindings; }
		}
		/*
		[Category("Hypowered")]
		public System.Int32 DeviceDpi
		{
			get { return HpdForm.DeviceDpi; }
			set { HpdForm.DeviceDpi = value; }
		}
		*/
		[Browsable(false)]
		public new System.Windows.Forms.DockStyle Dock
		{
			get { return base.Dock; }
			set { base.Dock = value; }
		}
		/*
		[Category("Hypowered")]
		public System.Boolean Enabled
		{
			get { return HpdForm.Enabled; }
			set { HpdForm.Enabled = value; }
		}
		[Category("Hypowered")]
		public System.Boolean Focused
		{
			get { return HpdForm.Focused; }
			set { HpdForm.Focused = value; }
		}
		[Category("Hypowered")]
		public System.Boolean IsAncestorSiteInDesignMode
		{
			get { return HpdForm.IsAncestorSiteInDesignMode; }
			set { HpdForm.IsAncestorSiteInDesignMode = value; }
		}
		[Category("Hypowered")]
		public System.Boolean IsMirrored
		{
			get { return HpdForm.IsMirrored; }
			set { HpdForm.IsMirrored = value; }
		}
		[Category("Hypowered")]
		public System.Int32 Left
		{
			get { return HpdForm.Left; }
			set { HpdForm.Left = value; }
		}
		[Category("Hypowered")]
		public System.Windows.Forms.Control Parent
		{
			get { return HpdForm.Parent; }
			set { HpdForm.Parent = value; }
		}
		[Category("Hypowered")]
		public System.String ProductName
		{
			get { return HpdForm.ProductName; }
			set { HpdForm.ProductName = value; }
		}
		[Category("Hypowered")]
		public System.String ProductVersion
		{
			get { return HpdForm.ProductVersion; }
			set { HpdForm.ProductVersion = value; }
		}
[Category("Hypowered")]
public System.Boolean RecreatingHandle
{
	get { return HpdForm.RecreatingHandle; }
	set { HpdForm.RecreatingHandle = value; }
}
[Category("Hypowered")]
public System.Drawing.Region Region
{
	get { return HpdForm.Region; }
	set { HpdForm.Region = value; }
}
[Category("Hypowered")]
public System.Int32 Right
{
	get { return HpdForm.Right; }
	set { HpdForm.Right = value; }
}
[Category("Hypowered")]
public System.Windows.Forms.RightToLeft RightToLeft
{
	get { return HpdForm.RightToLeft; }
	set { HpdForm.RightToLeft = value; }
}
		*/
		[Browsable(false)]
		public new System.ComponentModel.ISite Site
		{
			get { return base.Site; }
			set { base.Site = value; }
		}
		/*
[Category("Hypowered")]
public System.Object Tag
{
	get { return HpdForm.Tag; }
	set { HpdForm.Tag = value; }
}
[Category("Hypowered")]
public System.Int32 Top
{
	get { return HpdForm.Top; }
	set { HpdForm.Top = value; }
}
[Category("Hypowered")]
public System.Windows.Forms.Control TopLevelControl
{
	get { return HpdForm.TopLevelControl; }
	set { HpdForm.TopLevelControl = value; }
}
[Category("Hypowered")]
public System.Boolean UseWaitCursor
{
	get { return HpdForm.UseWaitCursor; }
	set { HpdForm.UseWaitCursor = value; }
}
[Category("Hypowered")]
public System.Boolean Visible
{
	get { return HpdForm.Visible; }
	set { HpdForm.Visible = value; }
}
[Category("Hypowered")]
public System.Int32 Width
{
	get { return HpdForm.Width; }
	set { HpdForm.Width = value; }
}
[Category("Hypowered")]
public System.Windows.Forms.IWindowTarget WindowTarget
{
	get { return HpdForm.WindowTarget; }
	set { HpdForm.WindowTarget = value; }
}
		[Category("Hypowered")]
public System.Drawing.Size PreferredSize
{
	get { return HpdForm.PreferredSize; }
	set { HpdForm.PreferredSize = value; }
}
*/
		[Browsable(false)]
		public new System.Windows.Forms.ImeMode ImeMode
		{
			get { return base.ImeMode; }
			set { base.ImeMode = value; }
		}
		[Browsable(false)]
		public new System.ComponentModel.IContainer? Container
		{
			get { return base.Container; }
		}	
	}
}
