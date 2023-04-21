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

		public delegate void ControlActionClickHandler(object sender, ControlActionClickEventArgs e);
		public event ControlActionClickHandler? ControlActionClick;
		protected virtual void OnControlActionClick(ControlActionClickEventArgs e)
		{
			if (ControlActionClick != null)
			{
				ControlActionClick(this, e);
			}
		}
		// *********************************************************************************************

		// *********************************************************************************************
		public new Color BackColor
		{
			get { return base.BackColor; }
			set
			{
				base.BackColor = value;
				CtrlListBox.BackColor = value;
				MoveScale.BackColor = value;
			}
		}
		public new Color ForeColor
		{
			get { return base.ForeColor; }
			set
			{
				base.ForeColor = value;
				CtrlListBox.ForeColor = value;
				MoveScale.ForeColor = value;
			}
		}
		public ControlActionPanel ControlActionPanel { get; set; } = new ControlActionPanel();

		public SizeMoveModePanel SizeMoveModePanel { get; set; } = new SizeMoveModePanel();
		public ArrowPanel ArrowPanel { get; set; } = new ArrowPanel();
		public AlignPanel AlignPanel { get; set; } = new AlignPanel();
		public ArrangPanel ArrangPanel { get; set; } = new ArrangPanel();
		public NumericUpDown MoveScale { get; set; } = new NumericUpDown();

		public PropertyGrid? PropertyGrid
		{
			get { return CtrlListBox.PropertyGrid; }
			set { CtrlListBox.PropertyGrid = value; }
		}
		public MainForm? MainForm { set; get; } = null;
		public HForm? HForm 
		{
			get { return CtrlListBox.HForm; }
			set { CtrlListBox.SetHForm(value); }
		}
		public ControlListBox CtrlListBox { get; set; } = new ControlListBox();

		public ControlPanel()
		{
			InitializeComponent();
			CtrlListBox.BackColor = BackColor;
			CtrlListBox.ForeColor = ForeColor;
			CtrlListBox.BorderStyle = BorderStyle.FixedSingle;
			MoveScale.BackColor = BackColor;
			MoveScale.ForeColor = ForeColor;
			MoveScale.Minimum = 1;
			MoveScale.Maximum= 200;
			MoveScale.Value = 2;

			ChkSize();
			this.Controls.Add(ControlActionPanel);
			this.Controls.Add(ArrangPanel);
			this.Controls.Add(AlignPanel);
			this.Controls.Add(SizeMoveModePanel);
			this.Controls.Add(ArrowPanel);
			this.Controls.Add(MoveScale);
			this.Controls.Add(CtrlListBox);

			ControlActionPanel.ControlActionClick += (sender, e)=>{ OnControlActionClick(e); };
		}
		protected override void InitLayout()
		{
			CtrlListBox.BackColor = BackColor;
			CtrlListBox.ForeColor = ForeColor;
			MoveScale.BackColor = BackColor;
			MoveScale.ForeColor = ForeColor;
			base.InitLayout();
		}
		public void ChkSize()
		{
			int y = 0;
			ControlActionPanel.Location = new Point(0, y);
			ControlActionPanel.Size = new Size(this.Width, 20);
			y += ControlActionPanel.Height + 2;
			ArrangPanel.Location = new Point(0, y);
			y += ArrangPanel.Height;
			AlignPanel.Location = new Point(0, y);
			y += AlignPanel.Height + 2;
			SizeMoveModePanel.Location = new Point(0, y);
			int x = SizeMoveModePanel.Width + 2;
			ArrowPanel.Location = new Point(x, y);
			x += ArrowPanel.Width+2;
			MoveScale.Location = new Point(x, y);
			MoveScale.Size = new Size(this.Width - x, MoveScale.Height);

			y += ArrowPanel.Height + 2;
			CtrlListBox.Location = new Point(0, y);
			CtrlListBox.Size = new Size(this.Width, this.Height - y);
		}
		protected override void OnResize(EventArgs e)
		{
			ChkSize();
			base.OnResize(e);
		}
		private void Exec(ControlAction ca)
		{
				if ((MainForm != null) && (MainForm.TargetForm != null))
				{
					switch (ca)
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
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			using (Pen p = new Pen(ForeColor))
			using (SolidBrush sb = new SolidBrush(Color.Transparent))
			{
				Graphics g = e.Graphics;
				g.FillRectangle(sb, this.ClientRectangle);

				int w = this.Width - 120 - 4;
				Rectangle r = new Rectangle(122, 2, w, 54+4);
				p.Color = Color.FromArgb(128,128,128);
				g.DrawLine(p, r.Left, r.Top, r.Right, r.Bottom);
				g.DrawLine(p, r.Left, r.Bottom, r.Right, r.Top);
				g.DrawRectangle(p,r);
			}
		}
	}
}
