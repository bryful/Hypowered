using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class EditHypowerd : Control
	{
		private MainForm? m_MainForm = null;
		public MainForm? MainForm
		{
			get { return m_MainForm; }
			set
			{
				SetMainForm(value);
			}
		}
		public void SetMainForm(MainForm? mf)
		{
			m_MainForm = mf;
			m_FormComb.MainForm = mf;
			if(m_MainForm != null)
			{
				m_HTreeView.Form = m_MainForm.TargetForm;
				m_MainForm.TargetFormChanged += (sender, e) =>
				{
					m_HTreeView.Form = e.HForm;
				};
				m_FormComb.MainForm = m_MainForm;
			}

			

		}

		private FormActionPanel m_FormActionPanel = new FormActionPanel();
		private ModePanel m_EditMode = new ModePanel();
		private ModePanel m_ScriptMode = new ModePanel();
		private FormComb m_FormComb = new FormComb();
		private Label m_lbCtrl = new Label();
		private ControlActionPanel m_ControlActionPanel = new ControlActionPanel();
		private ArrangPanel m_ArrangPanel = new ArrangPanel();
		private AlignPanel m_AlignPanel = new AlignPanel();
		private SizeMoveModePanel m_SizeMoveModePanel = new SizeMoveModePanel();
		private ArrowPanel m_ArrowPanel = new ArrowPanel();
		private NumericUpDown m_MoveScale = new NumericUpDown();
		private Label m_lbMenu = new Label();
		private MenuActionPanel m_MenuActionPanel = new MenuActionPanel();
		private HTreeView m_HTreeView = new HTreeView();

		// ***********************************************************
		public EditHypowerd()
		{
			base.BackColor = Color.FromArgb(64, 64, 64);
			base.ForeColor = Color.FromArgb(230, 230, 230);

			int x = 0; int y = 0;
			m_FormActionPanel.Location = new Point(x,y);
			y += m_FormActionPanel.Bottom+2;
			x = 0;

			m_EditMode.Location = new Point(x, y);
			m_EditMode.Size = new Size((this.Width - m_EditMode.Left-4)/2, 20);
			m_EditMode.Text = "Edit";
			x += m_EditMode.Width + 4;
			
			m_ScriptMode.Location = new Point(x, y);
			m_ScriptMode.Size = m_EditMode.Size;
			m_ScriptMode.Text = "Script";
			x = 0;
			y += m_ScriptMode.Height +2;
			m_FormComb.Location = new Point(x, y);
			m_FormComb.Size = new Size(this.Width, m_FormComb.Height);
			m_FormComb.BackColor = BackColor;
			m_FormComb.ForeColor = ForeColor;
			y += m_FormComb.Height + 2;

			m_lbCtrl.Location = new Point(x, y);
			m_lbCtrl.AutoSize = false;
			m_lbCtrl.Size = new Size(40, 20);
			m_lbCtrl.Text = "Ctrl";
			m_lbCtrl.TextAlign = ContentAlignment.MiddleRight;
			x = m_lbCtrl.Right + 2;

			m_ControlActionPanel.Location = new Point(x, y);
			y += m_ControlActionPanel.Height + 2;
			x = 0;

			m_SizeMoveModePanel.Location = new Point(x, y);
			x += m_SizeMoveModePanel.Width + 2;
			m_ArrowPanel.Location = new Point(x, y);
			x += m_ArrowPanel.Width + 2;

			m_MoveScale.Minimum = 1;
			m_MoveScale.Maximum = 300;
			m_MoveScale.Value = 2;
			m_MoveScale.BackColor = BackColor;
			m_MoveScale.ForeColor = ForeColor;
			m_MoveScale.Location = new Point(x, y);
			m_MoveScale.Size = new Size(this.Width - m_MoveScale.Left, m_MoveScale.Height);
			y += m_ArrowPanel.Height + 2;
			x = 0;
			m_AlignPanel.Location = new Point(x, y);
			y += m_AlignPanel.Height;
			m_ArrangPanel.Location = new Point(x, y);
			x = 0;
			y += m_ArrangPanel.Height + 2;
			m_lbMenu.Location = new Point(x, y);
			m_lbMenu.AutoSize = false;
			m_lbMenu.Size = new Size(40, 20);
			m_lbMenu.Text = "Menu";
			m_lbMenu.TextAlign = ContentAlignment.MiddleRight;
			x += m_lbMenu.Width;
			m_MenuActionPanel.Location = new Point(x, y);
			x += m_MenuActionPanel.Width+10;
			x = 0;
			y += m_MenuActionPanel.Height + 2;
			m_HTreeView.Location = new Point(x, y);
			m_HTreeView.Size = new Size(this.Width, this.Height - y);

			this.DoubleBuffered = true;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.UpdateStyles();

			this.Controls.Add(m_FormActionPanel);
			this.Controls.Add(m_EditMode);
			this.Controls.Add(m_ScriptMode);
			this.Controls.Add(m_FormComb);
			this.Controls.Add(m_lbCtrl);
			this.Controls.Add(m_ControlActionPanel);
			this.Controls.Add(m_ArrangPanel);
			this.Controls.Add(m_AlignPanel);
			this.Controls.Add(m_SizeMoveModePanel);
			this.Controls.Add(m_ArrowPanel);
			this.Controls.Add(m_MoveScale);
			this.Controls.Add(m_lbMenu);
			this.Controls.Add(m_MenuActionPanel);
			this.Controls.Add(m_HTreeView);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			using (Pen p = new Pen(ForeColor))
			using (SolidBrush sb = new SolidBrush(BackColor))
			{
				Graphics g = e.Graphics;
				sb.Color = BackColor;
				g.FillRectangle(sb, this.ClientRectangle);
				Rectangle r = new Rectangle(
					m_AlignPanel.Right + 2,
					m_AlignPanel.Top + 2,
					this.Width - m_AlignPanel.Right - 4,
					60
					);
				p.Color = ForeColor;
				g.DrawRectangle(p, r );
				g.DrawLine(p,r.Left,r.Top,r.Right,r.Bottom);
				g.DrawLine(p, r.Left, r.Bottom, r.Right, r.Top);

			}
		}
		protected override void OnResize(EventArgs e)
		{
			m_EditMode.Size = new Size((this.Width - m_EditMode.Left - 4) / 2, 20);
			m_ScriptMode.Location = new Point(m_EditMode.Right + 4, m_EditMode.Top);
			m_ScriptMode.Size = m_EditMode.Size;
			m_MoveScale.Size = new Size(this.Width - m_MoveScale.Left, m_MoveScale.Height);
			m_FormComb.Size = new Size(this.Width, m_FormComb.Height);

			m_HTreeView.Size = new Size(this.Width,
				this.Height - m_HTreeView.Top-2);

			base.OnResize(e);
		}
	}
}
