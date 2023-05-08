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
		#region Event
		// ******************************************************
		public delegate void SelectObjectsChangedHandler(object sender, SelectObjectsChangedArgs e);
		public event SelectObjectsChangedHandler? SelectObjectsChanged;
		protected virtual void OnSelectObjectChanged(SelectObjectsChangedArgs e)
		{
			if (SelectObjectsChanged != null)
			{
				SelectObjectsChanged(this, e);
			}
		}
		public delegate void ArrowChangedHandler(object sender, ArrowChangedEventArgs e);
		public event ArrowChangedHandler? ArrowChanged;
		protected virtual void OnArrowChanged(ArrowChangedEventArgs e)
		{
			if (ArrowChanged != null)
			{
				ArrowChanged(this, e);
			}
		}
		public delegate void AlignClickHandler(object sender, AlignClickEventArgs e);
		public event AlignClickHandler? AlignClick;
		protected virtual void OnAlignClick(AlignClickEventArgs e)
		{
			if (AlignClick != null)
			{
				AlignClick(this, e);
			}
		}
		public delegate void ArrangClickHandler(object sender, ArrangClickEventArgs e);
		public event ArrangClickHandler? ArrangClick;
		protected virtual void OnArrangClick(ArrangClickEventArgs e)
		{
			if (ArrangClick != null)
			{
				ArrangClick(this, e);
			}
		}
		#endregion
		// *********************************************************************************************
		public SizeMoveMode SizeMoveMode
		{
			get { return SizeMoveModePanel.SizeMoveMode; }
		}
		public int MoveScaleValue
		{
			get { return (int)MoveScale.Value; }
			set { MoveScale.Value = (decimal)value;}
		}
		public int[] SelectedIndices
		{
			get 
			{
				List<int> ret = new List<int>();
				if (CtrlListBox.Items.Count>0)
				{
					foreach(var idx in CtrlListBox.SelectedIndices)
					{
						ret.Add((int)idx);
					}
				}
				return ret.ToArray();	
			}
		}
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
		private ControlActionPanel ControlActionPanel { get; set; } = new ControlActionPanel();
		private SizeMoveModePanel SizeMoveModePanel { get; set; } = new SizeMoveModePanel();
		private ArrowPanel ArrowPanel { get; set; } = new ArrowPanel();
		private AlignPanel AlignPanel { get; set; } = new AlignPanel();
		private ArrangPanel ArrangPanel { get; set; } = new ArrangPanel();
		private NumericUpDown MoveScale { get; set; } = new NumericUpDown();

		public MainForm? MainForm
		{
			get { return CtrlListBox.MainForm; }
			set
			{
				CtrlListBox.MainForm = value;
			}
		}	
		private ControlListBox CtrlListBox { get; set; } = new ControlListBox();

		// *********************************************************************************************
		public ControlPanel()
		{
			InitializeComponent();
			CtrlListBox.BackColor = BackColor;
			CtrlListBox.ForeColor = ForeColor;
			CtrlListBox.BorderStyle = BorderStyle.FixedSingle;
			CtrlListBox.SelectObjectsChanged += (sender, e) => { OnSelectObjectChanged(e); };

			ArrowPanel.ArrowChanged += (sender, e) => { OnArrowChanged(e); };
			AlignPanel.AlignClick += (sender, e) => { OnAlignClick(e); };
			ArrangPanel.ArrangClick += (sender, e) => { OnArrangClick(e); };

			MoveScale.BackColor = BackColor;
			MoveScale.ForeColor = ForeColor;
			MoveScale.Minimum = 1;
			MoveScale.Maximum= 500;
			MoveScale.Value = 2;

			ChkSize();
			this.Controls.Add(ControlActionPanel);
			this.Controls.Add(ArrangPanel);
			this.Controls.Add(AlignPanel);
			this.Controls.Add(SizeMoveModePanel);
			this.Controls.Add(ArrowPanel);
			this.Controls.Add(MoveScale);
			this.Controls.Add(CtrlListBox);

			ControlActionPanel.ControlActionClick += (sender, e)=>
			{
				if((MainForm != null)&&(MainForm.TargetForm!=null))
				{
					if (MainForm.TargetForm.IsEdit)
					{
						ControlActionExec(e.Mode);
					}
				}
			};
		}
		// *********************************************************************************************
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
		private void ControlActionExec(ControlAction ca)
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
								MainForm.TargetForm.ControlListUp();
								CtrlListBox.SelectBakUp();
								CtrlListBox.PopSelection();
							}
							break;
						case ControlAction.Down:
							CtrlListBox.PushSelection();
							if (CtrlListBox.SelectBak.Length > 0)
							{
								MainForm.TargetForm.ControlListDown();
								CtrlListBox.SelectBakDown();
								CtrlListBox.PopSelection();
							}
							break;
						case ControlAction.Top:
							CtrlListBox.PushSelection();
							if (CtrlListBox.SelectBak.Length > 0)
							{
								MainForm.TargetForm.ControlListTop();
								CtrlListBox.SelectBakTop();
								CtrlListBox.PopSelection();
							}
							break;
						case ControlAction.Bottom:
							CtrlListBox.PushSelection();
							if (CtrlListBox.SelectBak.Length > 0)
							{
								MainForm.TargetForm.ControlListBottom();
								CtrlListBox.SelectBakBottom();
								CtrlListBox.PopSelection();
							}
							break;
						case ControlAction.Delete:
						if (MainForm.TargetForm.TargetControl != null)
						{
							string s = MainForm.TargetForm.TargetControl.Name;
							if (MainForm.YesNoDialog($"del: {s}") ==true)
							{
								MainForm.TargetForm.RemoveControl();
							}
						}
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
