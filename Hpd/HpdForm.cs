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
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using BRY;
namespace Hpd
{
	public partial class HpdForm : Form
	{
		public delegate void NameChangedHandler(object sender, EventArgs e);
		public event NameChangedHandler? NameChanged;
		protected virtual void OnNameChanged(EventArgs e)
		{
			if (NameChanged != null)
			{
				NameChanged(this, e);
			}
		}

		public void ItemsRefresh() { m_Items.Listup(this); }
		private HpdControlCollection m_Items = new HpdControlCollection();
		[Category("Hypowered")]
		public HpdControlCollection Items { get { return m_Items; } }

		protected bool m_IsEdit = false;
		[Category("Hypowered")]
		public bool IsEdit { get { return m_IsEdit; } }
		public void SetIsEdit(bool b){m_IsEdit = b;}
		public void SetIsEdit(Control m,bool b)
		{
			if(m is HpdControl)
			{
				((HpdControl)m).SetIsEdit(b);
			}else if (m is HpdForm)
			{
				((HpdForm)m).SetIsEdit(b);
			}

			if (m.Controls.Count>0)
			{
				foreach(var c in m.Controls)
				{
					if(c is HpdControl)
					{
						((HpdControl)c).SetIsEdit(b);
					}
				}
			}
		}
		[Category("Hypowered")]
		public bool CanResize { get; set; } = false;
		[Category("Hypowered")]
		[Bindable(true)]
		public new string Name
		{
			get { return base.Name; }
			set { SetName(value); }
		}
		[Category("Hypowered")]
		[Bindable(true)]
		public string ControlName
		{
			get { return base.Name; }
			set { SetName(value); }
		}
		public void SetName(string n)
		{
			string on = base.Name;
			if (base.Name != n)
			{
				base.Name = n;
				OnNameChanged(new EventArgs());
			}
		}
		protected bool m_IsSaveFileName = false;
		/// <summary>
		/// ファイル名を保存するかどうか.
		/// </summary>
		[Category("Hypowered")]
		public bool IsSaveFileName
		{
			get { return m_IsSaveFileName; }
			set { m_IsSaveFileName = value; }
		}
		private string m_FileName = "";
		[Category("Hypowered")]
		public String FileName
		{
			get { return m_FileName; }
			set
			{
				m_FileName = value;

			}
		}
		[Category("Hypowered")]
		public new Point Location
		{
			get { return base.Location; }
			set { base.Location = value; this.Invalidate(); }
		}
		[Category("Hypowered")]
		public new Size Size
		{
			get { return base.Size; }
			set { base.Size = value; this.Invalidate(); }
		}
		[Category("Hypowered_Text")]
		public new Font Font
		{
			get { return base.Font; }
			set
			{
				base.Font = value;
			}
		}
		public HpdForm()
		{
			//base.BackColor = Color.FromArgb(32, 32, 32);
			//base.ForeColor = Color.FromArgb(220, 220, 220);

			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw,
				true);
			this.UpdateStyles();
			InitializeComponent();
			ChkControls();
		}
		protected void ChkControls()
		{
			/*
			if(Controls.Count > 0 )
			{
				int idx = 0;
				foreach( Control c in Controls )
				{
					if(c is HpdControl)
					{
						HpdControl hc = (HpdControl)c;
						hc.Index = idx;
					}
					idx++;
				}
			}*/
			m_Items.Listup(this);
		}

	}
}
