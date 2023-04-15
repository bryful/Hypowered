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
	public class EditListBox : ListBox
	{
		public int[] SelectBak = new int[0];

		#region Prop
		[Category("Hypowered_Color"), Browsable(true)]
		public new System.Drawing.Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
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
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.BorderStyle BorderStyle
		{
			get { return base.BorderStyle; }
			set { base.BorderStyle = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Int32 ColumnWidth
		{
			get { return base.ColumnWidth; }
			set { base.ColumnWidth = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean UseCustomTabOffsets
		{
			get { return base.UseCustomTabOffsets; }
			set { base.UseCustomTabOffsets = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.DrawMode DrawMode
		{
			get { return base.DrawMode; }
			set { base.DrawMode = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Drawing.Font Font
		{
			get { return base.Font; }
			set { base.Font = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Drawing.Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Int32 HorizontalExtent
		{
			get { return base.HorizontalExtent; }
			set { base.HorizontalExtent = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean HorizontalScrollbar
		{
			get { return base.HorizontalScrollbar; }
			set { base.HorizontalScrollbar = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean IntegralHeight
		{
			get { return base.IntegralHeight; }
			set { base.IntegralHeight = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Int32 ItemHeight
		{
			get { return base.ItemHeight; }
			set { base.ItemHeight = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ListBox.ObjectCollection Items
		{
			get { return base.Items; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean MultiColumn
		{
			get { return base.MultiColumn; }
			set { base.MultiColumn = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Int32 PreferredHeight
		{
			get { return base.PreferredHeight; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean ScrollAlwaysVisible
		{
			get { return base.ScrollAlwaysVisible; }
			set { base.ScrollAlwaysVisible = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Int32 SelectedIndex
		{
			get { return base.SelectedIndex; }
			set 
			{
				if((value>=-1)&&(value<this.Items.Count))
				{
					base.SelectedIndex = value;
				}
			}
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ListBox.SelectedIndexCollection SelectedIndices
		{
			get { return base.SelectedIndices; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Object SelectedItem
		{
			get { return base.SelectedItem; }
			set { base.SelectedItem = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ListBox.SelectedObjectCollection SelectedItems
		{
			get { return base.SelectedItems; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.SelectionMode SelectionMode
		{
			get { return base.SelectionMode; }
			set { base.SelectionMode = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean Sorted
		{
			get { return base.Sorted; }
			set { base.Sorted = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.String Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Int32 TopIndex
		{
			get { return base.TopIndex; }
			set { base.TopIndex = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean UseTabStops
		{
			get { return base.UseTabStops; }
			set { base.UseTabStops = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ListBox.IntegerCollection CustomTabOffsets
		{
			get { return base.CustomTabOffsets; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.Padding Padding
		{
			get { return base.Padding; }
			set { base.Padding = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Object DataSource
		{
			get { return base.DataSource; }
			set { base.DataSource = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.String DisplayMember
		{
			get { return base.DisplayMember; }
			set { base.DisplayMember = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.IFormatProvider FormatInfo
		{
			get { return base.FormatInfo; }
			set { base.FormatInfo = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.String FormatString
		{
			get { return base.FormatString; }
			set { base.FormatString = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean FormattingEnabled
		{
			get { return base.FormattingEnabled; }
			set { base.FormattingEnabled = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.String ValueMember
		{
			get { return base.ValueMember; }
			set { base.ValueMember = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Object SelectedValue
		{
			get { return base.SelectedValue; }
			set { base.SelectedValue = value; }
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
		public new System.Boolean AllowDrop
		{
			get { return base.AllowDrop; }
			set { base.AllowDrop = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Windows.Forms.AnchorStyles Anchor
		{
			get { return base.Anchor; }
			set { base.Anchor = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean AutoSize
		{
			get { return base.AutoSize; }
			set { base.AutoSize = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Drawing.Point AutoScrollOffset
		{
			get { return base.AutoScrollOffset; }
			set { base.AutoScrollOffset = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.Layout.LayoutEngine LayoutEngine
		{
			get { return base.LayoutEngine; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.BindingContext BindingContext
		{
			get { return base.BindingContext; }
			set { base.BindingContext = value; }
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
		public new System.Boolean CausesValidation
		{
			get { return base.CausesValidation; }
			set { base.CausesValidation = value; }
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
		public new System.Windows.Forms.Control.ControlCollection Controls
		{
			get { return base.Controls; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Boolean Created
		{
			get { return base.Created; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.Cursor Cursor
		{
			get { return base.Cursor; }
			set { base.Cursor = value; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.ControlBindingsCollection DataBindings
		{
			get { return base.DataBindings; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Int32 DeviceDpi
		{
			get { return base.DeviceDpi; }
		}
		[Category("Hypowered"), Browsable(false)]
		public new System.Drawing.Rectangle DisplayRectangle
		{
			get { return base.DisplayRectangle; }
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
		[Category("Hypowered"), Browsable(false)]
		public new System.Windows.Forms.DockStyle Dock
		{
			get { return base.Dock; }
			set { base.Dock = value; }
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
		public new System.Boolean HasChildren
		{
			get { return base.HasChildren; }
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
		[Category("Hypowered"), Browsable(true)]
		public new System.String Name
		{
			get { return base.Name; }
			set { base.Name = value; }
		}
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
		public new System.Boolean TabStop
		{
			get { return base.TabStop; }
			set { base.TabStop = value; }
		}
		[Category("Hypowered"), Browsable(true)]
		public new System.Object Tag
		{
			get { return base.Tag; }
			set { base.Tag = value; }
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

		#endregion

		public EditListBox()
		{

		}
		public void PushSelection()
		{
			SelectBak = SelectedIndexArray;
		}
		public void PopSelection()
		{
			SelectedIndexArray = SelectBak;
		}
		public void SelectBakUp()
		{
			if (SelectBak.Length <= 0) return;
			if (SelectBak[0]<=1) return;
			int idx = SelectBak[0] - 1;
			for (int i=0; i< SelectBak.Length;i++)
			{
				SelectBak[i] = idx;
				idx++;
			}
		}

		public void SelectBakTop()
		{
			if (SelectBak.Length <= 0) return;
			if (SelectBak[0] <= 1) return;
			int idx = 1;
			for (int i = 0; i < SelectBak.Length; i++)
			{
				SelectBak[i] = idx;
				idx++;
			}
		}
		public void SelectBakDown()
		{
			if (SelectBak.Length <= 0) return;
			if (SelectBak[SelectBak.Length-1] >= this.Items.Count-1) return;
			int idx = SelectBak[SelectBak.Length - 1] +1;
			for (int i = SelectBak.Length-1; i >=0; i--)
			{
				SelectBak[i] = idx;
				idx--;
			}
		}
		public void SelectBakBottom()
		{
			if (SelectBak.Length <= 0) return;
			if (SelectBak[SelectBak.Length - 1] >= this.Items.Count - 1) return;
			int idx = this.Items.Count - 1;
			for (int i = SelectBak.Length - 1; i >= 0; i--)
			{
				SelectBak[i] = idx;
				idx--;
			}
		}
		public bool _EvenFlag = false;
		public int[] SelectedIndexArray
		{
			get
			{
				List<int> ints = new List<int>();
				if(this.SelectedIndices.Count > 0)
				{
					foreach(int c in this.SelectedIndices)
					{
						ints.Add(c);
					}
				}
				return ints.ToArray();
			}
			set
			{
				bool[] sels = new bool[this.Items.Count];
				if(sels.Length>0)
				{
					for (int i = 0; i < sels.Length; i++) sels[i] = false;
					if(value.Length>0)
					{
						foreach(int idx in value)
						{
							if((idx>=0)&&(idx< sels.Length))
							{
								sels[idx] = true;
							}
						}
					}
					_EvenFlag = true;
					for(int i = 0; i < sels.Length;i++)
					{
						this.SetSelected(i, sels[i]);
					}
					_EvenFlag = false;

				}
			}
		}
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			if (_EvenFlag) return;
			base.OnSelectedIndexChanged(e);
		}
	}
}
