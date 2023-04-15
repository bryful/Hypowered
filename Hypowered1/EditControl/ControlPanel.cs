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
	public partial class ControlPanel : Control
	{
		public PropertyGrid? PropertyGrid
		{
			get { return m_listbox.PropertyGrid; }
			set
			{
				m_listbox.PropertyGrid = value;
			}
		}
		public HyperMainForm? MainForm  =null;

		private int m_bheight = 23;
		private int m_bwidth = 45;
		protected ComboBox m_formCmb= new ComboBox();
		protected Button m_NewBtn = new Button();
		protected Button m_DelBtn = new Button();
		protected Button m_UpBtn = new Button();
		protected Button m_DownBtn = new Button();
		protected ControlListBox m_listbox = new ControlListBox();
		public Point LLcc { get { return m_listbox.Location; } }
		public Size LSize { get { return m_listbox.Size; } }
		[Category("Hypowered_Color")]
		public new Color ForeColor
		{
			get { return base.ForeColor; }
			set { 
				base.ForeColor = value;
				m_formCmb.ForeColor = value;
				m_listbox.ForeColor = value;
				m_NewBtn.ForeColor = value;
				m_DelBtn.ForeColor = value;
				m_UpBtn.ForeColor = value;
				m_DownBtn.ForeColor = value;
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
				m_formCmb.BackColor = value;
				m_listbox.BackColor = value;
				m_NewBtn.BackColor = value;
				m_DelBtn.BackColor = value;
				m_UpBtn.BackColor = value;
				m_DownBtn.BackColor = value;
				this.Invalidate();
			}
		}
		[Category("Hypowered_Color")]
		public Color SelectedColor
		{
			get { return m_listbox.SelectedColor; }
			set
			{
				m_listbox.SelectedColor = value;
				this.Invalidate();
			}
		}
		[Category("Hypowered_Color")]
		public int ItemHeight
		{
			get { return m_listbox.ItemHeight; }
			set
			{
				m_listbox.ItemHeight = value;
				this.Invalidate();
			}
		}
		public ControlPanel()
		{
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);

			m_formCmb.DropDownStyle= ComboBoxStyle.DropDownList;
			m_formCmb.Size = new Size(100, 25);
			m_formCmb.Location = new Point(2, 2);
			m_formCmb.FlatStyle = FlatStyle.Flat;
			m_formCmb.Click += M_formCmb_Click;
			m_NewBtn.Text= "New";
			m_NewBtn.Size = new Size(m_bwidth, m_bheight);
			m_NewBtn.FlatStyle= FlatStyle.Flat;
			m_NewBtn.Click += M_NewBtn_Click;
			m_DelBtn.Text = "Del";
			m_DelBtn.Size = new Size(m_bwidth, m_bheight);
			m_DelBtn.FlatStyle = FlatStyle.Flat;
			m_DelBtn.Click += M_DelBtn_Click;
			m_UpBtn.Text = "Up";
			m_UpBtn.Size = new Size(m_bwidth, m_bheight);
			m_UpBtn.FlatStyle = FlatStyle.Flat;
			m_UpBtn.Click += M_UpBtn_Click;
			m_DownBtn.Text = "Down";
			m_DownBtn.Size = new Size(m_bwidth, m_bheight);
			m_DownBtn.FlatStyle = FlatStyle.Flat;
			m_DownBtn.Click += M_DownBtn_Click;
			m_listbox.Size = new Size(100, 25);
			m_listbox.Location = new Point(2, 35);
			m_listbox.IntegralHeight= false;
			m_listbox.DoubleClick += M_listbox_DoubleClick;
			this.Controls.Add(m_formCmb);
			this.Controls.Add(m_NewBtn);
			this.Controls.Add(m_DelBtn);
			this.Controls.Add(m_UpBtn);
			this.Controls.Add(m_DownBtn);
			this.Controls.Add(m_listbox);
			InitializeComponent();
			ChkSize();
			ChkEnabled();
			m_listbox.SelectedIndexChanged += M_listbox_SelectedIndexChanged;
		}

		private void M_listbox_DoubleClick(object? sender, EventArgs e)
		{
			if (MainForm != null) MainForm.ShowEditControl();
		}

		private void M_DownBtn_Click(object? sender, EventArgs e)
		{
			if (MainForm != null) MainForm.ControlToDown();
		}

		private void M_UpBtn_Click(object? sender, EventArgs e)
		{
			if (MainForm != null) MainForm.ControlToUp();
		}

		private void M_DelBtn_Click(object? sender, EventArgs e)
		{
			if (MainForm != null) MainForm.DeleteControl();
		}

		private void M_NewBtn_Click(object? sender, EventArgs e)
		{
			if(MainForm!=null) MainForm.NewControl();
		}

		private void M_formCmb_Click(object? sender, EventArgs e)
		{
			if(MainForm==null) return;
			string[] lst = MainForm.GetFormsForList();
			m_formCmb.Items.Clear();
			m_formCmb.Items.AddRange(lst);
			m_listbox.SelectedIndex = -1;
		}

		private void M_listbox_SelectedIndexChanged(object? sender, EventArgs e)
		{
			ChkEnabled();
		}
		public void SetMainForm(HyperMainForm mf)
		{
			MainForm = mf;
			m_listbox.SetMainForm(mf);

		}

		private void Mainform_FormChanged(object sender, HyperChangedEventArgs e)
		{
		}

		public void ChkSize()
		{
			int x = 2;
			int y = 2;
			m_formCmb.Location = new Point(x, y); 
			y += m_formCmb.Height + 2;
			m_formCmb.Size = new Size(this.Width - 4, m_formCmb.Height);
			m_NewBtn.Location = new Point(x,y); 
			x += m_NewBtn.Width + 2;
			m_DelBtn.Location = new Point(x,y);
			x += m_DelBtn.Width + 2;
			m_UpBtn.Location = new Point(x, y);
			x += m_UpBtn.Width + 2;
			m_DownBtn.Location = new Point(x, y);
			x = 2;
			y += m_bheight + 2;
			m_listbox.Location = new Point(x, y);
			m_listbox.Size = new Size(
				this.Width - 4,
				this.Height - 8 - m_bheight-m_formCmb.Height); 
		}
		public void ChkEnabled()
		{
			int si = m_listbox.SelectedIndex;
			int cnt = m_listbox.Items.Count;
			m_NewBtn.Enabled = true;
			m_DelBtn.Enabled = ((si>=0)&&(si<cnt));
			m_UpBtn.Enabled = ((si >= 1) && (si < cnt));
			m_DownBtn.Enabled = ((si >= 0) && (si < cnt-1));
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
		}
	}
}
