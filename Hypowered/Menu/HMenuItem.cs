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
	public class HMenuItem : ToolStripMenuItem
	{
		public HForm? HForm = null;
		public void SetHForm(HForm? hf)
		{
			HForm = hf;
		}
		// ********************************************************************
		#region Porp
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean Enabled
		{
			get { return base.Enabled; }
			set { base.Enabled = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean Checked
		{
			get { return base.Checked; }
			set { base.Checked = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean CheckOnClick
		{
			get { return base.CheckOnClick; }
			set { base.CheckOnClick = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.CheckState CheckState
		{
			get { return base.CheckState; }
			set { base.CheckState = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ToolStripItemOverflow Overflow
		{
			get { return base.Overflow; }
			set { base.Overflow = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.Keys ShortcutKeys
		{
			get { return base.ShortcutKeys; }
			set { base.ShortcutKeys = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.String ShortcutKeyDisplayString
		{
			get { return base.ShortcutKeyDisplayString; }
			set { base.ShortcutKeyDisplayString = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean ShowShortcutKeys
		{
			get { return base.ShowShortcutKeys; }
			set { base.ShowShortcutKeys = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean IsMdiWindowListEntry
		{
			get { return base.IsMdiWindowListEntry; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ToolStripDropDown DropDown
		{
			get { return base.DropDown; }
			set { base.DropDown = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ToolStripDropDownDirection DropDownDirection
		{
			get { return base.DropDownDirection; }
			set { base.DropDownDirection = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ToolStripItemCollection DropDownItems
		{
			get { return base.DropDownItems; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean HasDropDownItems
		{
			get { return base.HasDropDownItems; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean HasDropDown
		{
			get { return base.HasDropDown; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean Pressed
		{
			get { return base.Pressed; }
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
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.ToolStripItemAlignment Alignment
		{
			get { return base.Alignment; }
			set { base.Alignment = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean AllowDrop
		{
			get { return base.AllowDrop; }
			set { base.AllowDrop = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean AutoSize
		{
			get { return base.AutoSize; }
			set { base.AutoSize = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean AutoToolTip
		{
			get { return base.AutoToolTip; }
			set { base.AutoToolTip = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean Available
		{
			get { return base.Available; }
			set { base.Available = value; }
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
		public new System.Drawing.Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.Rectangle Bounds
		{
			get { return base.Bounds; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.Rectangle ContentRectangle
		{
			get { return base.ContentRectangle; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean CanSelect
		{
			get { return base.CanSelect; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.AnchorStyles Anchor
		{
			get { return base.Anchor; }
			set { base.Anchor = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.DockStyle Dock
		{
			get { return base.Dock; }
			set { base.Dock = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle
		{
			get { return base.DisplayStyle; }
			set { base.DisplayStyle = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean DoubleClickEnabled
		{
			get { return base.DoubleClickEnabled; }
			set { base.DoubleClickEnabled = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Drawing.Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Drawing.Font Font
		{
			get { return base.Font; }
			set { base.Font = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Int32 Height
		{
			get { return base.Height; }
			set { base.Height = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.ContentAlignment ImageAlign
		{
			get { return base.ImageAlign; }
			set { base.ImageAlign = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.Image Image
		{
			get { return base.Image; }
			set { base.Image = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.Color ImageTransparentColor
		{
			get { return base.ImageTransparentColor; }
			set { base.ImageTransparentColor = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Int32 ImageIndex
		{
			get { return base.ImageIndex; }
			set { base.ImageIndex = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.String ImageKey
		{
			get { return base.ImageKey; }
			set { base.ImageKey = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ToolStripItemImageScaling ImageScaling
		{
			get { return base.ImageScaling; }
			set { base.ImageScaling = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean IsDisposed
		{
			get { return base.IsDisposed; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean IsOnDropDown
		{
			get { return base.IsOnDropDown; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean IsOnOverflow
		{
			get { return base.IsOnOverflow; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.Padding Margin
		{
			get { return base.Margin; }
			set { base.Margin = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.MergeAction MergeAction
		{
			get { return base.MergeAction; }
			set { base.MergeAction = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Int32 MergeIndex
		{
			get { return base.MergeIndex; }
			set { base.MergeIndex = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.String Name
		{
			get { return base.Name; }
			set { base.Name = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ToolStrip Owner
		{
			get { return base.Owner; }
			set { base.Owner = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ToolStripItem OwnerItem
		{
			get { return base.OwnerItem; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.Padding Padding
		{
			get { return base.Padding; }
			set { base.Padding = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ToolStripItemPlacement Placement
		{
			get { return base.Placement; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.RightToLeft RightToLeft
		{
			get { return base.RightToLeft; }
			set { base.RightToLeft = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean RightToLeftAutoMirrorImage
		{
			get { return base.RightToLeftAutoMirrorImage; }
			set { base.RightToLeftAutoMirrorImage = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean Selected
		{
			get { return base.Selected; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Drawing.Size Size
		{
			get { return base.Size; }
			set { base.Size = value; }
		}
		[Category("Hypowered"), Browsable(false)]
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
		[Category("Hypowered"), Browsable(true)]
		public new System.Drawing.ContentAlignment TextAlign
		{
			get { return base.TextAlign; }
			set { base.TextAlign = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ToolStripTextDirection TextDirection
		{
			get { return base.TextDirection; }
			set { base.TextDirection = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.TextImageRelation TextImageRelation
		{
			get { return base.TextImageRelation; }
			set { base.TextImageRelation = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.String ToolTipText
		{
			get { return base.ToolTipText; }
			set { base.ToolTipText = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean Visible
		{
			get { return base.Visible; }
			set { base.Visible = value; this.Invalidate(); }
		}
		private bool m_IsVisible = false;
		[Category("Hypowered"), Browsable(true)]
		public System.Boolean IsVisible
		{
			get { return m_IsVisible; }
			set 
			{
				m_IsVisible = value;
				base.Visible = m_IsVisible; 
				this.Invalidate(); 
			}
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Int32 Width
		{
			get { return base.Width; }
			set { base.Width = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.ComponentModel.ISite? Site
		{
			get { return base.Site; }
			set { base.Site = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.ComponentModel.IContainer? Container
		{
			get { return base.Container; }
		}
		#endregion
		// ********************************************************************
		public HScriptCode ScriptCode = new HScriptCode();
		public FuncType? FuncType =null;
		public HMenuItem()
		{

			ScriptCode.Setup(HScriptType.Click);
			this.Click += (sender, e) =>
			{
				if (HForm == null) return;
				if (ScriptCode.Codes[0].Code!="")
				{
					HForm.Script.ExecuteCode(ref ScriptCode.Codes[0]);
				}else if (FuncType!=null)
				{
					FuncType();
				}
			};
			m_IsVisible = base.Visible;
		}
	}
}
