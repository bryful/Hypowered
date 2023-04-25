using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public enum MenuExec
	{
		None=0,
		ShowMainForm,
		Close
	}
	public class HMenuItem : ToolStripMenuItem
	{
		public delegate void MenuChangedHandler(object sender, MenuChangedEventArgs e);
		public event MenuChangedHandler? MenuChanged;
		protected virtual void OnMenuChanged(MenuChangedEventArgs e)
		{
			if (MenuChanged != null)
			{
				MenuChanged(this, e);
			}
		}
		public delegate void MenuNameChangedHandler(object sender, MenuNameChangedEventArgs e);
		public event MenuNameChangedHandler? MenuNameChanged;
		protected virtual void OnMenuNameChanged(MenuNameChangedEventArgs e)
		{
			if (MenuNameChanged != null)
			{
				MenuNameChanged(this, e);
			}
		}

		[Category("Hypowered"),Browsable(false)]
		public bool IsRoot { get; set; } = false;
		public int Index = 0;
		public HForm? HForm = null;
		public void SetHForm(HForm? hf)
		{
			this.HForm = hf;
			if (this.DropDownItems.Count > 0)
			{
				for (int i = 0; i < this.DropDownItems.Count; i++)
				{
					if (this.DropDownItems[i] is HMenuItem)
					{
						((HMenuItem)this.DropDownItems[i]).SetHForm(hf);
					}
				}
			}
		}
		// ********************************************************************
		public void ChkMenu()
		{
			if (this.DropDownItems.Count > 0)
			{
				for (int i = 0; i < this.DropDownItems.Count; i++)
				{
					if (this.DropDownItems[i] is not HMenuItem) continue;
					HMenuItem mi = (HMenuItem)this.DropDownItems[i];
					mi.Index = i;
					mi.IsRoot = false;
					mi.ChkMenu();
				}
			}

		}
		// *************************************************
		private HMenuItem? ParentMenu()
		{
			HMenuItem? ret = null;
			if (this.Parent == null) return ret;
			ret = this.OwnerItem as HMenuItem;
			return ret; 
		}
		// *************************************************
		private int[] getIndexArray()
		{
			List<int> idxs = new List<int>();
			idxs.Add(this.Index);
			HMenuItem? p = ParentMenu();
		
			if ( p != null)
			{
				while ((p != null) && (p.IsRoot == false))
				{
					idxs.Add(p.Index);
					p = ParentMenu();
				}
				idxs.Reverse();
			}
			return idxs.ToArray();
		}
		public HMenuItem? GetHMenuItem(int[] idxs)
		{
			HMenuItem? ret = null;
			if(idxs.Length<=0) return ret;
			HMenuItem? p = this;
			if (p != null)
			{
				for (int i = 0; i < idxs.Length; i++)
				{
					int idx = idxs[i];
					if ((idx >= 0) && (idx < p.DropDownItems.Count))
					{
						if (p.DropDownItems[idx] is HMenuItem) break;
						p = (HMenuItem)p.DropDownItems[idx];
					}
					else
					{
						break;
					}
				}
			}
			return ret;
		}
		// *************************************************
		public int IndexOfMenuName(string nm)
		{
			return this.DropDownItems.IndexOfKey(nm);
		}
		// ********************************************************************
		#region Porp
		[Category("_Hypowered"), Browsable(true)]
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
		[Category("Hypowered_key"), Browsable(true)]
		public new System.Windows.Forms.Keys ShortcutKeys
		{
			get { return base.ShortcutKeys; }
			set { base.ShortcutKeys = value; }
		}
		[Category("Hypowered_key"), Browsable(true)]
		public new System.String ShortcutKeyDisplayString
		{
			get { return base.ShortcutKeyDisplayString; }
			set { base.ShortcutKeyDisplayString = value; }
		}
		[Category("Hypowered_key"), Browsable(true)]
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
		[Category("Hypowered"), Browsable(true)]
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
			set 
			{
				bool b = (base.Name != value);
				base.Name = value;
				if (b) OnMenuNameChanged(new MenuNameChangedEventArgs(Name,this));
			}
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
		[Category("Hypowered"), Browsable(true)]
		public new System.Boolean Visible
		{
			get 
			{ return base.Visible; }
			set 
			{
				base.Available = value;
				base.Visible = value;
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
		public ScriptItem ScriptItem = new ScriptItem();
		public FuncType? FuncType = null;
		public MenuExec MenuExec { get; set; } = MenuExec.None;
		public HMenuItem()
		{
			this.Click += (sender, e) =>
			{
				if (IsRoot) return;
				if (HForm == null) return;
				if(MenuExec == MenuExec.Close)
				{
					if (HForm != null)
					{
						HForm.ThisClose();
						return;
					}
				}else if (MenuExec == MenuExec.ShowMainForm)
				{
					if (HForm != null)
					{
						HForm.ShowMainMenu();
						return;
					}
				}
				if (ScriptItem.Code != "")
				{
					if (HForm != null)  HForm.Script.ExecuteCode(ref ScriptItem);
				}
				else if (FuncType != null)
				{
					FuncType();
				}
			};
		}
		// ********************************************************************
		public void MenuUp(HMenuItem mi)
		{
			int idx = this.DropDownItems.IndexOf(mi);
			if (idx >= 1)
			{
				ToolStripItem m = this.DropDownItems[idx];
				this.DropDownItems.RemoveAt(idx);
				this.DropDownItems.Insert(idx - 1, m);
				ChkMenu();
				OnMenuChanged(new MenuChangedEventArgs((HMenuItem)m));
			}
		}
		public void MenuDown(HMenuItem mi)
		{
			int idx = this.DropDownItems.IndexOf(mi);
			if ((idx >= 0) && (idx < this.DropDownItems.Count - 1))
			{
				ToolStripItem m = this.DropDownItems[idx];
				this.DropDownItems.RemoveAt(idx);
				this.DropDownItems.Insert(idx + 1, m);
				ChkMenu();
				OnMenuChanged(new MenuChangedEventArgs((HMenuItem)m));
			}
		}
		// ********************************************************************
		public virtual JsonObject? ToJson()
		{
			JsonFile? jf = new JsonFile();
			jf.SetValue(nameof(Name), (String)Name);//System.String
			jf.SetValue(nameof(Text), (String)Text);//System.String
			jf.SetValue(nameof(Checked), (Boolean)Checked);//System.Boolean
			jf.SetValue(nameof(CheckState), (int)CheckState);//System.Boolean
			jf.SetValue(nameof(ShortcutKeys), (int)ShortcutKeys);//System.Windows.Forms.Keys
			if(ShortcutKeyDisplayString!=null)
				jf.SetValue(nameof(ShortcutKeyDisplayString), (String)ShortcutKeyDisplayString);//System.String
			jf.SetValue(nameof(ShowShortcutKeys), (Boolean)ShowShortcutKeys);//System.Boolean
			jf.SetValue(nameof(TextAlign), (int)TextAlign);//System.Drawing.ContentAlignment
			jf.SetValue(nameof(TextDirection), (int)TextDirection);//System.Windows.Forms.ToolStripTextDirection
			if (ToolTipText != null)
				jf.SetValue(nameof(ToolTipText), (String)ToolTipText);//System.String
			jf.SetValue(nameof(ScriptItem), (string)(ScriptItem.Code));//System.Boolean
			jf.SetValue(nameof(MenuExec), (int)(MenuExec));//System.Boolean

			if (this.DropDownItems.Count > 0)
			{
				JsonArray arr = new JsonArray();
				if (DropDownItems.Count > 0)
				{
					foreach (ToolStripMenuItem item in DropDownItems)
					{
						if(item is HMenuItem)
						{
							arr.Add(((HMenuItem)item).ToJson());

						}
					}
				}
				jf.SetValue("DropDownItems", arr);
			}
			return jf.Obj;
		}
		public virtual void FromJson(JsonObject jo)
		{
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("Name", typeof(String).Name);
			if (v != null) Name = (String)v;
			v = jf.ValueAuto("Text", typeof(String).Name);
			if (v != null) Text = (String)v;
			v = jf.ValueAuto("Checked", typeof(Boolean).Name);
			if (v != null) Checked = (Boolean)v;
			v = jf.ValueAuto("CheckState", typeof(Int32).Name);
			if (v != null) CheckState = (CheckState)v;
			v = jf.ValueAuto("ShortcutKeys", typeof(Int32).Name);
			if (v != null) ShortcutKeys = (Keys)v;
			v = jf.ValueAuto("ShortcutKeyDisplayString", typeof(String).Name);
			if (v != null) ShortcutKeyDisplayString = (String)v;
			v = jf.ValueAuto("ShowShortcutKeys", typeof(Boolean).Name);
			if (v != null) ShowShortcutKeys = (Boolean)v;
			v = jf.ValueAuto("DisplayStyle", typeof(Int32).Name);
			if (v != null) DisplayStyle = (ToolStripItemDisplayStyle)v;
			v = jf.ValueAuto("TextAlign", typeof(Int32).Name);
			if (v != null) TextAlign = (ContentAlignment)v;
			v = jf.ValueAuto("TextDirection", typeof(Int32).Name);
			if (v != null) TextDirection = (ToolStripTextDirection)v;
			v = jf.ValueAuto("ToolTipText", typeof(String).Name);
			if (v != null) ToolTipText = (String)v;
			v = jf.ValueAuto("ScriptItem", typeof(String).Name);
			if (v != null)
			{
				ScriptItem.Script = null;
				ScriptItem.Code = (String)v;
			}
			v = jf.ValueAuto("MenuExec", typeof(Int32).Name);
			if (v != null) MenuExec = (MenuExec)v;
			JsonArray? arr = jf.ValueArray("DropDownItems");
			if (arr != null)
			{
				this.DropDownItems.Clear();
				List<HMenuItem> list = new List<HMenuItem>();
				if (arr.Count > 0)
				{
					foreach (var s in arr)
					{
						JsonObject? jj = (JsonObject?)s;
						if (jj != null)
						{
							HMenuItem mi = new HMenuItem();
							mi.IsRoot = false;
							mi.Available = true;
							mi.Visible = true;
							mi.MenuChanged += (sender, e) => { OnMenuChanged(e); };
							mi.FromJson(jj);
							list.Add(mi);
						}
					}
				}
				this.DropDownItems.AddRange(list.ToArray());
			}
		}
	}
	public class MenuNameChangedEventArgs : EventArgs
	{
		public string Name;
		public HMenuItem Menu;
		public MenuNameChangedEventArgs(string n, HMenuItem m)
		{
			Name = n;
			Menu = m;
		}
	}
}