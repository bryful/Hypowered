using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hypowered.Properties;

namespace Hypowered
{
	public partial class EditControl : Control
	{
		protected MainForm? MainForm { get; set; } = null;
		private HForm? m_TargetForm = null;
		private Control[]? m_Controls = null;
		public void SetMainForm(MainForm mf)
		{
			MainForm = mf;
			if(MainForm != null)
			{
				DispFormListBox();
				MainForm.FormChanged += (sender, e) => { DispFormListBox(); };
				SetTargetForm( MainForm.TargetForm);
				MainForm.TargetFormChanged += (sender, e) =>{SetTargetForm(MainForm.TargetForm);};
			}
		}
		public void SetTargetForm(HForm? hf)
		{
			m_TargetForm = hf;
			if (m_TargetForm != null)
			{
				m_TargetForm.ControlAdded -= (sender, e) => { MakeCtrlListBox(); };
				m_TargetForm.ControlAdded += (sender, e) => { MakeCtrlListBox(); };
				m_TargetForm.ControlRemoved -= (sender, e) => { MakeCtrlListBox(); };
				m_TargetForm.ControlRemoved += (sender, e) => { MakeCtrlListBox(); };
				MakeCtrlListBox();
			}

		}
		public void MakeCtrlListBox()
		{
			if (m_TargetForm != null)
			{
				CtrlListBox.Items.Clear();
				CtrlListBox.Items.AddRange(m_TargetForm.ControlList());
				m_Controls = m_TargetForm.ControlArray();
			}
			else
			{
				m_Controls = null;
			}
		}
		public PropertyGrid? PropertyGrid { get; set; } = null;
		[Category("Hypowered_Ctrl")]
		public ListBox FormListBox { get; set; } = new ListBox();
		[Category("Hypowered_Ctrl")]
		public ListBox CtrlListBox { get; set; } = new ListBox();
		[Category("Hypowered_Ctrl")]
		public Button BtnAdd { get; set; } = new Button();
		[Category("Hypowered_Ctrl")]
		public Button BtnUp { get; set; } = new Button();
		[Category("Hypowered_Ctrl")]
		public Button BtnDown { get; set; } = new Button();
		[Category("Hypowered_Ctrl")]
		public Button BtnDel { get; set; } = new Button();
		[Category("Hypowered_Ctrl")]
		public int FormListHeight
		{
			get { return FormListBox.Height; }
			set
			{
				FormListBox.Height = value;
				LayoutControl();
			}
		}

		[Category("Hypowered_Color")]
		public new Color BackColor
		{
			get { return base.BackColor; }
			set
			{
				base.BackColor = value;
				CtrlListBox.BackColor = value;
				FormListBox.BackColor = value;
			}
		}
		[Category("Hypowered_Color")]
		public new Color ForeColor
		{
			get { return base.ForeColor; }
			set
			{
				base.ForeColor = value;
				CtrlListBox.ForeColor = value;
				FormListBox.ForeColor = value;
			}
		}
		public EditControl()
		{
			ForeColor = Color.FromArgb(220, 220, 220);
			BackColor = Color.FromArgb(64, 64, 64);
			InitializeComponent();
			initControl();
			LayoutControl();
			this.Controls.Add(this.FormListBox);
			this.Controls.Add(this.BtnAdd);
			this.Controls.Add(this.BtnUp);
			this.Controls.Add(this.BtnDown);
			this.Controls.Add(this.BtnDel);
			this.Controls.Add(this.CtrlListBox);
		}
		private void DispFormListBox()
		{
			FormListBox.Items.Clear();
			if (MainForm == null) return;
			if (MainForm.HForms.Count > 0)
			{
				List<string> list = new List<string>();
				foreach (var item in MainForm.HForms)
				{
					list.Add(item.Name);
				}
				FormListBox.Items.AddRange(list.ToArray());
				if (m_TargetForm != null)
				{
					if (FormListBox.SelectedIndex != m_TargetForm.Index)
					{
						FormListBox.SelectedIndex = m_TargetForm.Index;
					}
				}
			}
		}
		public void initControl()
		{
			BtnAdd.FlatStyle = FlatStyle.Flat;
			BtnAdd.Image = Resources.CAdd;
			BtnUp.FlatStyle = FlatStyle.Flat;
			BtnUp.Image = Resources.CUp;
			BtnDown.FlatStyle = FlatStyle.Flat;
			BtnDown.Image = Resources.CDown;
			BtnDel.FlatStyle = FlatStyle.Flat;
			BtnDel.Image = Resources.CDel;

			BtnAdd.Click += (sender, e) =>
			{
				if(m_TargetForm !=null)
				{
					m_TargetForm.AddControl();
				}
			};

			CtrlListBox.SelectedIndexChanged += (sender, e) =>
			{
				if (MainForm == null) return;
				if(m_TargetForm == null) return;
				if(PropertyGrid == null) return;
				int[] sels = new int[0];
				if(CtrlListBox.SelectedIndices.Count>0)
				{
					sels = new int[CtrlListBox.SelectedIndices.Count];
					int i = 0;
					foreach(int c in CtrlListBox.SelectedIndices)
					{
						sels[i] = c;
						i++;
					}
				}

				if(sels.Length>=1) 
				{
					List<object> list = new List<object>();
					foreach (int c in sels)
					{
						if ((c >= 0) && (c < m_TargetForm.Controls.Count))
						{
							list.Add( m_TargetForm.Controls[c]);
						}
					}
					if(list.Count>0)
					{
						PropertyGrid.SelectedObjects = list.ToArray();
					}
				}
				if(sels.Length==0)
				{
					m_TargetForm.ClearIsEdits();
				}
				else
				{
					m_TargetForm.SetIsEdits(sels);
				}
			};
			CtrlListBox.BorderStyle = BorderStyle.FixedSingle;
			CtrlListBox.BackColor = this.BackColor;
			CtrlListBox.ForeColor = this.ForeColor;
			CtrlListBox.ScrollAlwaysVisible = true;
			CtrlListBox.SelectionMode = SelectionMode.MultiExtended;

			FormListBox.SelectedIndexChanged += (sender, e) =>
			{
				if (MainForm == null) return;


				int idx = FormListBox.SelectedIndex;
				if((idx>=0) && (idx< MainForm.HForms.Count))
				{
					SetTargetForm(MainForm.HForms[idx]);
					MainForm.SetTargetForm(MainForm.HForms[idx]);
					if (PropertyGrid != null)
					{
						PropertyGrid.SelectedObject = m_TargetForm;
					}
				}
			};
			FormListBox.BorderStyle = BorderStyle.FixedSingle;
			FormListBox.BackColor = this.BackColor;
			FormListBox.ForeColor = this.ForeColor;
			FormListBox.ScrollAlwaysVisible = true;
			FormListBox.Location = new Point(0, 0);
			FormListBox.Size = new Size(this.Width, 100);
			
		}
		public void LayoutControl()
		{
			int w = 20;
			int h = 20;
			int x = 0; int y = 0;
			FormListBox.Location = new Point(x, y);
			FormListBox.Size = new Size(this.Width, FormListBox.Height);
			y += FormListBox.Height + 4;

			BtnAdd.Size = new Size(w, h);
			BtnAdd.Location = new Point(x, y);
			x += BtnAdd.Width + 2;
			BtnUp.Size = new Size(w, h);
			BtnUp.Location = new Point(x, y);
			x += BtnUp.Width + 2;
			BtnDown.Size = new Size(w, h);
			BtnDown.Location = new Point(x, y);
			x += BtnDown.Width + 2;
			BtnDel.Size = new Size(w, h);
			BtnDel.Location = new Point(x, y);
			x += BtnDel.Width + 2;
			y += h + 2;


			CtrlListBox.Location = new Point(0, y);
			CtrlListBox.Size = new Size(this.Width, this.Height - y);
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			LayoutControl();
		}
	}
}
