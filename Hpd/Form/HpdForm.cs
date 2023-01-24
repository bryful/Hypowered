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
		protected HpdOrientation m_Orientation = HpdOrientation.Row;
		[Category("Hypowered_layout")]
		public HpdOrientation Orientation
		{
			get { return m_Orientation; }
			set 
			{
				bool b = (m_Orientation != value);
				m_Orientation = value; 
				if(b) AutoLayout(); 
			}
		}
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
		[Browsable(true)]
		public new string Name
		{
			get { return base.Name; }
			set 
			{
				if (base.Name != value)
				{
					base.Name = value;
					OnNameChanged(new EventArgs());
				}
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
			AutoLayout();
		}
		protected void ChkControls()
		{
			if(Controls.Count > 0 )
			{
				int idx = 0;
				foreach( Control c in Controls )
				{
					if(c is HpdControl)
					{
						HpdControl hc = (HpdControl)c;
						hc.SetIndex(idx);
					}
					idx++;
				}
			}
		}
		
		public void AddControl(string Name,string tx,HpdType ht)
		{
			HpdControl? c = HpdControl.CreateControl(Name, tx, ht);
			if(c != null)
			{
				Controls.Add(c);
				AutoLayout();
			}
		}
		public HpdType DefHpdType = HpdType.Button;
		public void AddControl()
		{
			using (NewControlDialog dlg = new NewControlDialog())
			{
				dlg.HpdType= DefHpdType;
				if( dlg.ShowDialog()== DialogResult.OK )
				{
					AddControl(dlg.HpdName,dlg.HpdText,dlg.HpdType);
					DefHpdType = dlg.HpdType;
				}
			}
		}
		private bool AutoLayoutFlag = false;
		public void AutoLayout()
		{
			if (AutoLayoutFlag) return;
			AutoLayoutFlag=true;
			AutoLayout(this);
			AutoLayoutFlag = false;
		}
		public void AutoLayout(Control ctrl,bool isReSize=false)
		{
			if (ctrl.Controls.Count > 0)
			{
				foreach (Control c in ctrl.Controls)
				{
					if (c is HpdPanel)
					{
						AutoLayout(c, true);
					}
				}
			}
			HpdOrientation ori = HpdOrientation.Row;
			if(ctrl is HpdForm) { ori = ((HpdForm)ctrl).Orientation; }
			else if (ctrl is HpdPanel) { ori = ((HpdPanel)ctrl).Orientation; }
			if (ori == HpdOrientation.Row)
			{
				HpdLayout.ChkRow(ctrl);
			}
			else
			{
				HpdLayout.ChkColumn(ctrl);
			}

		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			AutoLayout();
		}
	}
}
