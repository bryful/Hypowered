using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup.Localizer;
using Hypowered.Properties;

namespace Hypowered
{
	public partial class EditControl : Control
	{
		protected MainForm? MainForm { get; set; } = null;
		private HForm? m_TargetForm = null;
		private HControl? m_TargetControl = null;

		public void SetMainForm(MainForm mf)
		{
			MainForm = mf;
			if(MainForm != null)
			{
				FormListBox.SetMainForm(MainForm);
				SetTargetForm( MainForm.TargetForm);
				MainMenuTreeView.SetMainForm(MainForm);
			}
		}
		public void SetTargetForm(HForm? hf)
		{
			if((hf!=null)&&(hf.CanPropertyGrid==false))
			{
				m_TargetForm = null;
			}
			m_TargetForm = hf;
			CtrlListBox.SetHForm(m_TargetForm);
			MainMenuTreeView.SetHForrm(m_TargetForm);

		}
		public PropertyGrid? PropertyGrid { get; set; } = null;
		[Category("Hypowered_Ctrl")]
		public FormListBox FormListBox { get; set; } = new FormListBox();
		[Category("Hypowered_Ctrl")]
		public ControlListBox CtrlListBox { get; set; } = new ControlListBox();
		[Category("Hypowered_Ctrl")]
		public SizeMoveModePanel SizeMoveModePanel { get; set; } = new SizeMoveModePanel();
		[Category("Hypowered_Ctrl")]
		public ArrowPanel ArrowPanel { get; set; } = new ArrowPanel();
		[Category("Hypowered_Ctrl")]
		public ControlActionPanel ControlActionPanel { get; set; } = new ControlActionPanel();
		[Category("Hypowered_Ctrl")]
		public AlignPanel AlignPanel { get; set; } = new AlignPanel();
		[Category("Hypowered_Ctrl")]
		public ArrangPanel ArrangPanel { get; set; } = new ArrangPanel();
		[Category("Hypowered_Ctrl")]
		public NumericUpDown MoveScale { get; set; } = new NumericUpDown();
		[Category("Hypowered_Ctrl")]
		public FormActionPanel FormActionPanel { get; set; } = new FormActionPanel();
		[Category("Hypowered_Ctrl")]
		public MainMenuTreeView MainMenuTreeView { get; set; } = new MainMenuTreeView();

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
				MainMenuTreeView.BackColor = value;
				SizeMoveModePanel.BackColor = value;
				FormActionPanel.BackColor = value;

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
				MainMenuTreeView.ForeColor = value;
				SizeMoveModePanel.ForeColor = value;
				FormActionPanel.ForeColor = value;
			}
		}
		public EditControl()
		{
			InitializeComponent();
			ForeColor = Color.FromArgb(220, 220, 220);
			BackColor = Color.FromArgb(64, 64, 64);
			initControl();
			LayoutControl();
			this.Controls.Add(this.FormActionPanel);
			this.Controls.Add(this.FormListBox);
			this.Controls.Add(this.ControlActionPanel);
			this.Controls.Add(this.AlignPanel);
			this.Controls.Add(this.ArrangPanel);
			this.Controls.Add(this.SizeMoveModePanel);
			this.Controls.Add(this.ArrowPanel);
			this.Controls.Add(this.MoveScale);
			this.Controls.Add(this.CtrlListBox);
			this.Controls.Add(this.MainMenuTreeView);
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
			ForeColor = Color.FromArgb(220, 220, 220);
			BackColor = Color.FromArgb(64, 64, 64);

			CtrlListBox.SelectedIndexChanged += (sender, e) =>
			{
				if (MainForm == null) return;
				if(m_TargetForm == null) return;
				if(PropertyGrid == null) return;
				int[] sels = CtrlListBox.SelectedIndexArray;

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
					m_TargetForm.SetSelectedAll();
				}
				else
				{
					if(sels.Length==1)
					{
						m_TargetForm.TargetIndex = sels[0];
					}
					m_TargetForm.SetSelecteds(sels);
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
					if (MainForm.HForms[idx].CanPropertyGrid)
					{
						SetTargetForm(MainForm.HForms[idx]);
						MainForm.SetTargetForm(MainForm.HForms[idx]);
					}
					else
					{
						m_TargetForm = null;
					}
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

			MainMenuTreeView.PropertyGrid = this.PropertyGrid;
			MainMenuTreeView.SetMainForm(MainForm);
			MainMenuTreeView.BackColor = this.BackColor;
			MainMenuTreeView.ForeColor = this.ForeColor;
			MainMenuTreeView.AfterSelect += (sender, e) =>
			{
				if (PropertyGrid == null) return;
				if (e.Node == null) return;
				PropertyGrid.SelectedObject = e.Node.Tag;
			};

			MoveScale.BackColor = this.BackColor;
			MoveScale.ForeColor = this.ForeColor;
			MoveScale.Value = 1;
			MoveScale.Minimum = 1;
			MoveScale.Maximum = 100;
			MoveScale.BorderStyle = BorderStyle.FixedSingle;
			MoveScale.Size = new Size(48, 25);

			ControlActionPanel.ControlActionClick += (sender, e) =>
			{
				if ((MainForm != null) && (MainForm.TargetForm != null))
				{
					switch (e.Mode)
					{
						case ControlAction.Add:
							{
								MainForm.TargetForm.AddControl();
								CtrlListBox.SelectedIndex = 1;
							}
							break;
						case ControlAction.Up:
							CtrlListBox.PushSelection();
							if (CtrlListBox.SelectBak.Length > 0)
							{
								MainForm.TargetForm.ControlUp(CtrlListBox.SelectBak);
								CtrlListBox.SelectBakUp();
								CtrlListBox.PopSelection();
							}
							break;
						case ControlAction.Down:
							CtrlListBox.PushSelection();
							if (CtrlListBox.SelectBak.Length > 0)
							{
								MainForm.TargetForm.ControlDown(CtrlListBox.SelectBak);
								CtrlListBox.SelectBakDown();
								CtrlListBox.PopSelection();
							}
							break;
						case ControlAction.Top:
							CtrlListBox.PushSelection();
							if (CtrlListBox.SelectBak.Length > 0)
							{
								MainForm.TargetForm.ControlTop(CtrlListBox.SelectBak);
								CtrlListBox.SelectBakTop();
								CtrlListBox.PopSelection();
							}
							break;
						case ControlAction.Bottom:
							CtrlListBox.PushSelection();
							if (CtrlListBox.SelectBak.Length > 0)
							{
								MainForm.TargetForm.ControlBottom(CtrlListBox.SelectBak);
								CtrlListBox.SelectBakBottom();
								CtrlListBox.PopSelection();
							}
							break;
						case ControlAction.Delete:
							MainForm.TargetForm.RemoveControl();
							break;
					}
				}
			};
			FormActionPanel.FormActionClick += (sender, e) =>
			{
				if (MainForm != null)
				{
					switch (e.Mode)
					{
						case FormAction.New:
							MainForm.NewForm();
							break;
						case FormAction.Open:
							MainForm.OpenForm();
							break;
						case FormAction.Rename:
							MainForm.RenameForm();
							break;
						case FormAction.Dup:
							break;
						case FormAction.Close:
							MainForm.CloseForm();
							break;

					}
				}
			};
			ArrowPanel.ArrowChanged += (sender, e) =>
			{
				if ((MainForm != null) && (MainForm.TargetForm != null))
				{
					switch(SizeMoveModePanel.SizeMoveMode)
					{
						case SizeMoveMode.Move:
							MainForm.TargetForm.ControlMove(
								CtrlListBox.SelectedIndexArray,
								e.Arrow,
								(int)this.MoveScale.Value
								);
							break;
						case SizeMoveMode.ResizeLeftTop:
							MainForm.TargetForm.ControlResizeLeftTop(
								CtrlListBox.SelectedIndexArray,
								e.Arrow,
								(int)this.MoveScale.Value
								);
							break;
						case SizeMoveMode.ResizeRightBottom:
							MainForm.TargetForm.ControlResizeRightBottom(
								CtrlListBox.SelectedIndexArray,
								e.Arrow,
								(int)this.MoveScale.Value
								);
							break;
					}
				}
			};

		}
		public void LayoutControl()
		{
			int x = 0; int y = 0;

			FormActionPanel.Location = new Point(x, y);
			y += ControlActionPanel.Height + 2;

			FormListBox.Location = new Point(x, y);
			FormListBox.Size = new Size(this.Width, FormListBox.Height);
			y += FormListBox.Height + 4;

			ControlActionPanel.Location = new Point(x, y);
			y += ControlActionPanel.Height + 2;
			AlignPanel.Location = new Point(x, y);
			y += AlignPanel.Height + 2;
			ArrangPanel.Location = new Point(x, y);
			y += ArrangPanel.Height + 2;

			SizeMoveModePanel.Location = new Point(x, y);
			x += SizeMoveModePanel.Width + 2;
			ArrowPanel.Location = new Point(x, y);
			x += ArrowPanel.Width + 5;
			MoveScale.Location = new Point(x, y);
			x = 0;
			y += SizeMoveModePanel.Height + 2;

			CtrlListBox.Location = new Point(0, y);
			CtrlListBox.Size = new Size(this.Width, (this.Height - y)/2);
			y += CtrlListBox.Height + 2;
			MainMenuTreeView.Location = new Point(0, y);
			MainMenuTreeView.Size = new Size(this.Width, (this.Height - y));
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
