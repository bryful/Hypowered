using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Hpd
{
	partial class HpdControl
	{
		[Category("Hypowered_Color")]
		public new Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
				if (m_Item != null) m_Item.ForeColor = value;
				this.Invalidate();
			}
		}
		[Category("Hypowered_Color")]
		public new Color BackColor
		{
			get { return base.BackColor; }
			set
			{
				base.BackColor = value;
				if (m_Item != null) m_Item.BackColor = value;
				this.Invalidate();
			}
		}


		[Category("Hypowered"), Browsable(true)]
		public new bool TabStop
		{
			get
			{
				if (m_Item != null)
				{
					m_Item.TabStop = false;
					return m_Item.TabStop;
				}
				else
				{
					return base.TabStop;
				}
			}
			set
			{
				if (m_Item != null)
				{
					m_Item.TabStop = value;
					base.TabStop = false;
				}
				else
				{
					base.TabStop = value;
				}
			}
		}
		[Browsable(false)]
		public new AnchorStyles Anchor
		{
			get { return base.Anchor; }
			set
			{
				//base.Anchor = AnchorStyles.None;
			}
		}
		[Browsable(false)]
		public new DockStyle Dock
		{
			get { return base.Dock; }
			set
			{
				//base.Dock = DockStyle.None;
			}
		}
		[Browsable(false)]
		public new System.Windows.Forms.ControlBindingsCollection DataBindings
		{
			get { return base.DataBindings; }
		}
		[Browsable(false)]
		public new System.Drawing.Image? BackgroundImage
		{
			get { return base.BackgroundImage; }
			set { base.BackgroundImage = value; }
		}
		[Browsable(false)]
		public new ImageLayout BackgroundImageLayout
		{
			get { return base.BackgroundImageLayout; }
			set { base.BackgroundImageLayout = value; }
		}
		[Browsable(false)]
		public new ContextMenuStrip ContextMenuStrip
		{
			get { return base.ContextMenuStrip; }
			set { base.ContextMenuStrip = value; }
		}
		[Category("Hypowered")]
		public new Object? Tag
		{
			get { return base.Tag; }
			set { base.Tag = value; }
		}
		[Browsable(false)]
		public new Cursor Cursor
		{
			get { return base.Cursor; }
			set { base.Cursor = value; }
		}
		[Browsable(false)]
		public new bool CausesValidation
		{
			get { return base.CausesValidation; }
			set { base.CausesValidation = value; }
		}
		[Browsable(false)]
		public new string AccessibleDescription
		{
			get { return base.AccessibleDescription; }
			set { base.AccessibleDescription = value; }
		}
		[Browsable(false)]
		public new string AccessibleName
		{
			get { return base.AccessibleName; }
			set { base.AccessibleName = value; }
		}
		[Browsable(false)]
		public new AccessibleRole AccessibleRole
		{
			get { return base.AccessibleRole; }
			set { base.AccessibleRole = value; }
		}
		[Category("Hypowered_layout"), Browsable(true)]
		public new Size MinimumSize
		{
			get { return base.MinimumSize; }
			set { }
		}
		public void SetMinimumSize(Size sz)
		{
			base.MinimumSize = sz;
		}


	}
}
