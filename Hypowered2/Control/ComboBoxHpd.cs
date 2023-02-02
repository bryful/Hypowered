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

namespace Hpd
{
	public partial class ComboBoxHpd : ComboBox
	{
		public ComboBoxHpd()
		{
			InitializeComponent();
			base.DropDownStyle = ComboBoxStyle.DropDownList;
			this.DrawMode = DrawMode.OwnerDrawFixed;
			//項目の高さを設定
			this.ItemHeight = 20;
			//DrawItemイベントハンドラの追加
			this.DrawItem += new DrawItemEventHandler(ComboDrawItem);
		}
		[Category("Hypowered_ComboBox"), Browsable(false)]
		public new DrawMode DrawMode
		{
			get { return base.DrawMode; }
			set { }
		}
		[Category("Hypowered_ComboBox"), Browsable(false)]
		public new ComboBoxStyle DropDownStyle
		{
			get { return base.DropDownStyle; }
			set { }
		}
		public ListItem[] GetItems()
		{
			List<ListItem> list = new List<ListItem>();
			if(this.Items.Count > 0)
			{
				foreach(object? c in this.Items)
				{
					if (c == null)
					{
						list.Add(new ListItem());
					}else if( c is string)
					{
						list.Add(new ListItem((string)c));
					}else if (c is ListItem) 
					{
						list.Add((ListItem)c);
					}
					else
					{
						list.Add(new ListItem());
					}

				}
			}
			return list.ToArray();
		}
		public void SetItems(ListItem[] list)
		{
			this.Items.Clear();
			this.Items.AddRange(list);
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public new string? SelectedText
		{
			get 
			{
				string? ret = null;
				if((SelectedItem!=null))
				{
					if(SelectedItem is string)
					{
						ret = (string)SelectedItem;
					}else if (SelectedItem is ListItem)
					{
						ret= ((ListItem)SelectedItem).Text;
					}
				}
				return ret;
			}
			set
			{
				this.SelectedIndex = IndexOfText(value);

			}
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public ListItem? SelectedListItem
		{
			get
			{
				ListItem? ret = null;
				if((SelectedItem!=null))
				{
					if(SelectedItem is ListItem) 
					{
						ret = (ListItem)SelectedItem;
					}
				}
				return ret;
			}
			set
			{
				SelectedIndex = IndexOfListItem(value);

			}
		}
		public int IndexOfText(string? tx)
		{
			int ret = -1;
			if ((this.Items.Count <= 0)||(tx==null)) return ret;
			for(int i=0; i< this.Items.Count;i++)
			{
				if (this.Items[i]==null) continue;
				if (this.Items[i] is string)
				{
					if(tx== this.Items[i].ToString())
					{
						ret = i;
						break;
					}
				}else if (this.Items[i] is ListItem)
				{
					if (tx == ((ListItem)this.Items[i]).Text)
					{
						ret = i;
						break;
					}
				}
			}
			return ret;
		}
		public int IndexOfListItem(ListItem? li)
		{
			int ret = -1;
			if ((this.Items.Count <= 0) || (li == null)) return ret;
			ret = IndexOfText(li.Text);
			return ret;
		}
		private void ComboDrawItem(object? sender, DrawItemEventArgs e)
		{
			e.DrawBackground();

			ComboBoxHpd? cmb = (ComboBoxHpd?)sender;
			if (cmb != null)
			{
				using (SolidBrush sb = new SolidBrush(cmb.ForeColor))
				{
					string txt = "";
					if ((e.Index >= 0) && (e.Index < cmb.Items.Count))
					{
						if (cmb.Items[e.Index] == null)
						{
							txt = "null";
						}
						else if (cmb.Items[e.Index] is ListItem)
						{
							txt = ((ListItem)cmb.Items[e.Index]).Text;
						}
						else if (cmb.Items[e.Index] is string)
						{
							txt = (string)cmb.Items[e.Index];
						}
						else
						{
							try
							{
								txt = cmb.Items[e.Index].ToString();
							}catch(Exception ex)
							{
								txt = ex.ToString();
							}
						}
					}
					float ym =
						(e.Bounds.Height - e.Graphics.MeasureString(txt, cmb.Font).Height) / 2;
					e.Graphics.DrawString(txt, cmb.Font, sb, e.Bounds.X, e.Bounds.Y + ym);
				}
				//フォーカスを示す四角形を描画
				e.DrawFocusRectangle();
			}
		}
		// **************************************************************
		// **************************************************************
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.AutoCompleteMode AutoCompleteMode
		{
			get { return base.AutoCompleteMode; }
			set { base.AutoCompleteMode = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.AutoCompleteSource AutoCompleteSource
		{
			get { return base.AutoCompleteSource; }
			set { base.AutoCompleteSource = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.AutoCompleteStringCollection AutoCompleteCustomSource
		{
			get { return base.AutoCompleteCustomSource; }
			set { base.AutoCompleteCustomSource = value; }
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
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Object DataSource
		{
			get { return base.DataSource; }
			set { base.DataSource = value; }
		}

		[Category("Hypowered_ComboBox"), Browsable(true)]
		public new System.Int32 DropDownWidth
		{
			get { return base.DropDownWidth; }
			set { base.DropDownWidth = value; }
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public new System.Int32 DropDownHeight
		{
			get { return base.DropDownHeight; }
			set { base.DropDownHeight = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean DroppedDown
		{
			get { return base.DroppedDown; }
			set { base.DroppedDown = value; }
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public new System.Windows.Forms.FlatStyle FlatStyle
		{
			get { return base.FlatStyle; }
			set { base.FlatStyle = value; }
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public new System.Boolean Focused
		{
			get { return base.Focused; }
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public new System.Boolean IntegralHeight
		{
			get { return base.IntegralHeight; }
			set { base.IntegralHeight = value; }
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public new System.Int32 ItemHeight
		{
			get { return base.ItemHeight; }
			set { base.ItemHeight = value; }
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public new System.Int32 MaxDropDownItems
		{
			get { return base.MaxDropDownItems; }
			set { base.MaxDropDownItems = value; }
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public new System.Int32 MaxLength
		{
			get { return base.MaxLength; }
			set { base.MaxLength = value; }
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public new System.Int32 PreferredHeight
		{
			get { return base.PreferredHeight; }
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public new System.Int32 SelectionLength
		{
			get { return base.SelectionLength; }
			set { base.SelectionLength = value; }
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public new System.Int32 SelectionStart
		{
			get { return base.SelectionStart; }
			set { base.SelectionStart = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean Sorted
		{
			get { return base.Sorted; }
			set { }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.String DisplayMember
		{
			get { return base.DisplayMember; }
			set { base.DisplayMember = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.IFormatProvider FormatInfo
		{
			get { return base.FormatInfo; }
			set { base.FormatInfo = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.String FormatString
		{
			get { return base.FormatString; }
			set { base.FormatString = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean FormattingEnabled
		{
			get { return base.FormattingEnabled; }
			set { base.FormattingEnabled = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.String ValueMember
		{
			get { return base.ValueMember; }
			set { base.ValueMember = value; }
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public new System.Object SelectedValue
		{
			get { return base.SelectedValue; }
			set { base.SelectedValue = value; }
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
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public new System.Boolean AllowDrop
		{
			get { return base.AllowDrop; }
			set { base.AllowDrop = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.AnchorStyles Anchor
		{
			get { return base.Anchor; }
			set {  }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean AutoSize
		{
			get { return base.AutoSize; }
			set {  }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Drawing.Point AutoScrollOffset
		{
			get { return base.AutoScrollOffset; }
			set {  }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.Layout.LayoutEngine LayoutEngine
		{
			get { return base.LayoutEngine; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.BindingContext BindingContext
		{
			get { return base.BindingContext; }
			set { base.BindingContext = value; }
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public new System.Boolean CanFocus
		{
			get { return base.CanFocus; }
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
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
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean CausesValidation
		{
			get { return base.CausesValidation; }
			set { base.CausesValidation = value; }
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
		public new System.Boolean Created
		{
			get { return base.Created; }
		}
		[Category("Hypowered_ComboBox"), Browsable(true)]
		public new System.Windows.Forms.Cursor Cursor
		{
			get { return base.Cursor; }
			set { base.Cursor = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.ControlBindingsCollection DataBindings
		{
			get { return base.DataBindings; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Int32 DeviceDpi
		{
			get { return base.DeviceDpi; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Drawing.Rectangle DisplayRectangle
		{
			get { return base.DisplayRectangle; }
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
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.DockStyle Dock
		{
			get { return base.Dock; }
			set {  }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.IntPtr Handle
		{
			get { return base.Handle; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean HasChildren
		{
			get { return base.HasChildren; }
			set {  }
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
		public new System.ComponentModel.ISite Site
		{
			get { return base.Site; }
			set { base.Site = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Int32 TabIndex
		{
			get { return base.TabIndex; }
			set { base.TabIndex = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Boolean TabStop
		{
			get { return base.TabStop; }
			set { base.TabStop = value; }
		}
		[Category("Hypowered"),  Browsable(true)]
		public new System.Object Tag
		{
			get { return base.Tag; }
			set { base.Tag = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.Control TopLevelControl
		{
			get { return base.TopLevelControl; }
		}
		[Category("Hypowered"),  Browsable(true)]
		public new System.Boolean UseWaitCursor
		{
			get { return base.UseWaitCursor; }
			set { base.UseWaitCursor = value; }
		}
		[Category("Hypowered"), ScriptUsage(ScriptAccess.None), Browsable(false)]
		public new System.Windows.Forms.IWindowTarget WindowTarget
		{
			get { return base.WindowTarget; }
			set { base.WindowTarget = value; }
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
