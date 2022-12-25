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
	public partial class HyperListBox : HyperControl
	{
		private ListBox m_ListBox = new ListBox();
		public delegate void SelectedIndexChangedHandler(object sender, SelectedIndexChangedEventArgs e);
		public event SelectedIndexChangedHandler? SelectedIndexChanged;
		protected virtual void OnSelectedIndexChanged(SelectedIndexChangedEventArgs e)
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, e);
			}
			if ((HyperForm != null) && (m_ScriptCode != ""))
			{
				HyperForm.ExecuteCode(m_ScriptCode);
			}
		}
		public override void SetIsEditMode(bool value)
		{
			m_IsEditMode = value;
			m_ListBox.Visible = !value;
		}
		[Category("Hypowerd")]
		public new Font Font
		{
			get { return m_ListBox.Font; }
			set
			{
				m_ListBox.Font = value;
				this.Font = value;
			}
		}
		[Category("Hypowerd_ListBox")]
		public  ListBox ListBox
		{
			get { return m_ListBox; }
			set
			{
				m_ListBox = value;
			}
		}
		[Category("Hypowerd_ListBox")]
		public bool IntegralHeight
		{
			get { return m_ListBox.IntegralHeight; }
			set
			{
				m_ListBox.IntegralHeight = value;
			}
		}
		[Category("Hypowerd_ListBox")]
		public int ItemHeight
		{
			get { return m_ListBox.ItemHeight; }
			set
			{
				m_ListBox.ItemHeight = value;
			}
		}
		[Category("Hypowerd_ListBox")]
		public int SelectedIndex
		{
			get { return m_ListBox.SelectedIndex; }
			set
			{
				m_ListBox.SelectedIndex = value;
			}
		}
		[Category("Hypowerd_ListBox")]
		public ListBox.ObjectCollection Items
		{
			get { return m_ListBox.Items; }
		}
		[Category("Hypowerd_Color")]
		public new Color ForeColor
		{
			get { return m_ListBox.ForeColor; }
			set
			{
				base.ForeColor = value;
				m_ListBox.ForeColor = value;
			}
		}
		[Category("Hypowerd_Color")]
		public new Color BackColor
		{
			get { return m_ListBox.BackColor; }
			set
			{
				base.BackColor = value;
				m_ListBox.BackColor = value;
			}
		}
		public HyperListBox()
		{
			SetMyType(ControlType.ListBox);
			m_ScriptCode = "//ListBox";
			this.Size = new Size(150, 150);
			m_ListBox.Location = new Point(0, 0);
			m_ListBox.Size = new Size(this.Width,this.Height);
			if(Height!= m_ListBox.Height)
			{
				this.Size = new Size(this.Width, m_ListBox.Height);
			}
			base.BackColor = ColU.ToColor(HyperColor.Back);
			base.ForeColor = ColU.ToColor(HyperColor.Fore);
			m_ForcusColor = ColU.ToColor(HyperColor.Forcus);

			m_ListBox.BackColor =base.BackColor;
			m_ListBox.ForeColor = base.ForeColor;
			m_ListBox.BorderStyle= BorderStyle.None;
			m_ListBox.IntegralHeight = false;
			InitializeComponent();
			this.Controls.Add(m_ListBox);
			m_ListBox.SelectedIndexChanged += M_ListBox_SelectedIndexChanged;
		}

		private void M_ListBox_SelectedIndexChanged(object? sender, EventArgs e)
		{
			OnSelectedIndexChanged(new SelectedIndexChangedEventArgs(m_ListBox.SelectedIndex));	
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			if (m_IsEditMode)
			{
				base.OnPaint(pe);
			}
			else
			{
				pe.Graphics.Clear(BackColor);
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			m_ListBox.Size = new Size(this.Width, this.Height);
			if (Height != m_ListBox.Height)
			{
				this.Size = new Size(this.Width, m_ListBox.Height);
			}
		}

	}
}
