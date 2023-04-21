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
	public enum FormAction
	{
		None,
		New,
		Open,
		Rename,
		Dup,
		Close

	}
	public partial class FormPanel : Control
	{
		public class FormActionClickEventArgs : EventArgs
		{
			public FormAction Mode;
			public FormActionClickEventArgs(FormAction v)
			{
				Mode = v;
			}
		}
		public delegate void ControlActionClickHandler(object sender, FormActionClickEventArgs e);
		public event ControlActionClickHandler? FormActionClick;
		protected virtual void OnFormActionClick(FormActionClickEventArgs e)
		{
			if (FormActionClick != null)
			{
				FormActionClick(this, e);
			}
		}
		protected Bitmap[] FormActionIcon = new Bitmap[7];
		protected Bitmap[] EditModeIcon = new Bitmap[2];
		protected FormAction m_ActionMode = FormAction.None;
		protected int m_IsEdit = 0;
		public bool IsEdit
		{
			get
			{
				if ((MainForm!=null)&&(MainForm.TargetForm!=null))
				{
					if(MainForm.TargetForm.IsEdit)
					{
						m_IsEdit = 1;
					}
					else
					{
						m_IsEdit = 0;
					}
					return MainForm.TargetForm.IsEdit;
				}
				else
				{
					m_IsEdit = 0;
					return false;
				}
			}
			set
			{
				if ((MainForm != null) && (MainForm.TargetForm != null))
				{
					if (value)
					{
						m_IsEdit = 1;
					}
					else
					{
						m_IsEdit = 0;
					}
					MainForm.TargetForm.IsEdit = value;
				}
				else
				{
					m_IsEdit = 0;
				}
			}
		}
		public FormListBox FormListBox = new FormListBox();
		public MainForm? MainForm
		{
			get { return FormListBox.MainForm; }
			set
			{
				FormListBox.SetMainForm(value);
			}
		}
		public ControlListBox? ControlListBox
		{
			get { return FormListBox.ControlListBox; }
			set
			{
				FormListBox.ControlListBox =value;
			}
		}
		public PropertyGrid? PropertyGrid
		{
			get { return FormListBox.PropertyGrid; }
			set
			{
				FormListBox.PropertyGrid = value;
			}
		}
		private ControlPanel? m_ControlPanel = null;
		public ControlPanel? ControlPanel
		{
			get { return m_ControlPanel; }
			set
			{
				m_ControlPanel = value;
				if(m_ControlPanel != null)
				{
					FormListBox.ControlListBox = m_ControlPanel.CtrlListBox;
				}
				else
				{
					FormListBox.ControlListBox = null;
				}
			}
		}
		public FormPanel()
		{
			base.BackColor = Color.FromArgb(64, 64, 64);
			base.ForeColor = Color.FromArgb(230, 230, 230);
			this.Location = new Point(0, 0);
			this.Size = new Size(150, 300);
			this.MinimumSize = new Size(150, 20);
			this.MaximumSize = new Size(1000, 1000); 
			InitializeComponent();
			FormActionIcon[0] = Properties.Resources.FAction0;
			FormActionIcon[1] = Properties.Resources.FAction1;
			FormActionIcon[2] = Properties.Resources.FAction2;
			FormActionIcon[3] = Properties.Resources.FAction3;
			FormActionIcon[4] = Properties.Resources.FAction4;
			FormActionIcon[5] = Properties.Resources.FAction5;

			EditModeIcon[0] = Properties.Resources.EditMode0;
			EditModeIcon[1] = Properties.Resources.EditMode1;

			BackColor = Color.FromArgb(64, 64, 64);
			ForeColor = Color.FromArgb(230, 230, 230);
			this.DoubleBuffered = true;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.UpdateStyles();

			FormListBox.Location = new Point(0, 42);
			FormListBox.BackColor = BackColor;
			FormListBox.ForeColor = ForeColor;
			FormListBox.BorderStyle = BorderStyle.FixedSingle;
			this.Size = new Size(this.Width, this.Height - 22);
			this.Controls.Add( FormListBox );

		}
		private int GetPos(MouseEventArgs e)
		{
			int ret = -1;
			int x = e.X / 30;
			if ((x >= 0) && (x <= 4)&&(e.Y<20))
			{
				ret = x + 1;
			}
			return ret;
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(BackColor))
			{
				Graphics g = pe.Graphics;
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);
				g.DrawImage(EditModeIcon[(int)m_IsEdit], 0, 20);
				g.DrawImage(FormActionIcon[(int)m_ActionMode], 0, 0);
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				int pos = GetPos(e);
				if (pos >= 1)
				{
					m_ActionMode = (FormAction)pos;
					this.Invalidate();
				}else if((e.Y>=20) &&(e.Y < 40))
				{
					IsEdit = ! IsEdit;
					this.Invalidate();
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_ActionMode != FormAction.None)
			{
				FormAction mode = m_ActionMode;
				m_ActionMode = FormAction.None;
				this.Invalidate();
				Exec(mode);
				OnFormActionClick(new FormActionClickEventArgs(mode));
			}
			else
			{
				base.OnMouseUp(e);
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			FormListBox.Size = new Size(this.Width, this.Height - 42);
		}
		public void Exec(FormAction ac)
		{
			if (MainForm != null)
			{
				switch (ac)
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
		}
	}
}
