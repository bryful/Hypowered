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
	public partial class HyperFileList : HyperControl
	{
		public delegate void SelectedIndexChangedHandler(object sender, SelectedIndexChangedEventArgs e);
		public event SelectedIndexChangedHandler? SelectedIndexChanged;
		protected virtual void OnSelectedIndexChanged(SelectedIndexChangedEventArgs e)
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, e);
			}

		}
		private ListBox m_ListBox = new ListBox();
		private string m_CurrentDir = Directory.GetCurrentDirectory();
		[Category("Hypowerd_DirList")]
		public string CurrentDir
		{
			get { return m_CurrentDir; }
			set 
			{
				if(m_CurrentDir != value)
				{
					m_CurrentDir = value;
					Listup();
				}
			}
		}
		[Category("Hypowerd_DirList")]
		public string SelectedFile
		{
			get
			{
				string ret = "";
				int si =m_ListBox.SelectedIndex;
				if((si>=0)&&(si<m_ListBox.Items.Count))
				{
					ret = m_ListBox.Items[si].ToString();
				}
				return ret;
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
		[Category("Hypowerd_FileList")]
		public ListBox ListBox
		{
			get { return m_ListBox; }
			set
			{
				m_ListBox = value;
			}
		}
		[Category("Hypowerd_FileList")]
		public bool IntegralHeight
		{
			get { return m_ListBox.IntegralHeight; }
			set
			{
				m_ListBox.IntegralHeight = value;
			}
		}
		[Category("Hypowerd_FileList")]
		public int ItemHeight
		{
			get { return m_ListBox.ItemHeight; }
			set
			{
				m_ListBox.ItemHeight = value;
			}
		}
		[Category("Hypowerd_FileList")]
		public int SelectedIndex
		{
			get { return m_ListBox.SelectedIndex; }
			set
			{
				m_ListBox.SelectedIndex = value;
			}
		}
		[Category("Hypowerd_FileList")]
		public ListBox.ObjectCollection Items
		{
			get { return m_ListBox.Items; }
		}
		[Category("Hypowerd_FileList")]
		public int Count
		{
			get { return m_ListBox.Items.Count; }
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
		private HyperDirList? m_HyperDirList = null;
		[Category("Hypowerd_FileList")]
		public HyperDirList? HyperDirList
		{
			get { return m_HyperDirList; }
			set
			{
				m_HyperDirList = value;
				if(m_HyperDirList != null)
				{
					m_CurrentDir= m_HyperDirList.CurrentDir = m_CurrentDir;
					Listup();
					m_HyperDirList.CurrentDirChanged += M_HyperDirList_CurrentDirChanged;
				}
			}
		}
		private HyperLabel? m_HyperLabel = null;
		[Category("Hypowerd_DirList")]
		public HyperLabel? HyperLabel
		{
			get { return m_HyperLabel; }
			set
			{
				m_HyperLabel = value;
				if (m_HyperLabel != null)
				{
					m_HyperLabel.Text = SelectedFile;
				}
			}
		}
		private void M_HyperDirList_CurrentDirChanged(object sender, CurrentDirChangedEventArgs e)
		{
			if(m_CurrentDir != e.Path)
			{
				CurrentDir= e.Path;
				Listup();
			}
		}

		public HyperFileList()
		{
			SetMyType(ControlType.FileList);
			m_ScriptCode = "//FileList";
			this.Size = new Size(150, 150);
			m_ListBox.Location = new Point(0, 0);
			m_ListBox.Size = new Size(this.Width,this.Height);
			if(Height!= m_ListBox.Height)
			{
				this.Size = new Size(this.Width, m_ListBox.Height);
			}

			m_ListBox.BackColor =base.BackColor;
			m_ListBox.ForeColor = base.ForeColor;
			m_ListBox.BorderStyle= BorderStyle.FixedSingle;
			m_ListBox.IntegralHeight = false;
			InitializeComponent();
			this.Controls.Add(m_ListBox);
			Listup();
			m_ListBox.DoubleClick += M_ListBox_DoubleClick;
			m_ListBox.SelectedIndexChanged += M_ListBox_SelectedIndexChanged;
		}

		private void M_ListBox_SelectedIndexChanged(object? sender, EventArgs e)
		{
			string s = "";
			if((SelectedIndex>=0)&&(SelectedIndex<Count))
			{
				s = m_ListBox.Items[SelectedIndex].ToString();
			}
			OnSelectedIndexChanged(new SelectedIndexChangedEventArgs(SelectedIndex, s));
		}

		private void M_ListBox_DoubleClick(object? sender, EventArgs e)
		{
			int si = m_ListBox.SelectedIndex;
			if((si>=0)&&(si<m_ListBox.Items.Count))
			{
			}
		}

		public void Listup()
		{
			try
			{
				IEnumerable<string> files = Directory.EnumerateFiles(m_CurrentDir);

				List<string> strings = new List<string>();
				
				foreach (string str in files)
				{
					string n = Path.GetFileName(str);
					strings.Add(n);
				}
				m_ListBox.Items.Clear();
				m_ListBox.Items.AddRange(strings.ToArray());
			}
			catch
			{

			}
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
