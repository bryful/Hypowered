using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class ControlListBox : ListBox
	{
		private PropertyGrid? m_pg = null;
		public PropertyGrid? PropertyGrid
		{
			get { return m_pg; }
			set
			{
				m_pg = value;
				if (m_pg != null)
				{
					m_pg.SelectedObject = m_obj;
				}
			}
		}
		private Object? m_obj = null;
		public HyperMainForm? MainForm = null;
		public HyperBaseForm? TargetForm = null;
		public HyperControl? TargetControl = null;

		[Category("Hypowered_Color")]
		public new Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; this.Invalidate(); }
		}
		[Category("Hypowered_Color")]
		public new Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; this.Invalidate(); }
		}
		private Color m_SelectedColor = Color.FromArgb(255, 120, 32, 32);
		[Category("Hypowered_Color")]
		public Color SelectedColor
		{
			get { return m_SelectedColor; }
			set { m_SelectedColor = value; this.Invalidate(); }
		}
		[Category("Hypowered")]
		public new ObjectCollection Items
		{
			get { return base.Items; }
		}
		private bool ShouldSerializeItems()
		{
			return false;
		}

		public ControlListBox()
		{
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			BorderStyle= BorderStyle.FixedSingle;
			DrawMode = DrawMode.OwnerDrawFixed;
			InitializeComponent();
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			e.DrawBackground();

			Graphics g = e.Graphics;

			if (e.Index > -1)
			{
				using(SolidBrush sb = new SolidBrush(ForeColor))
				{
					if (MainForm!=null)
					{
						if((e.Index>=0)&&(e.Index<MainForm.Controls.Count))
						{
							if (MainForm.Controls[e.Index] is HyperControl)
							{
								HyperControl hc = (HyperControl)MainForm.Controls[e.Index];
								if((hc.Selected)&&(e.Index!=MainForm.TargetIndex))
								{
									sb.Color = m_SelectedColor;
									g.FillRectangle(sb, e.Bounds);
								}
							}
						}
					}
					string? txt = this.Items[e.Index].ToString();
					if((txt!=null)&&(txt!=""))
					{
						sb.Color = ForeColor;
						g.DrawString(txt, e.Font, sb, e.Bounds);
					}

				}
			}

			//フォーカスを示す四角形を描画
			e.DrawFocusRectangle();
		}
		public void SetMainForm(HyperMainForm? mf)
		{
			this.MainForm = mf;
			if (MainForm != null)
			{
				MainForm.FormChanged -= MainForm_FormChanged;
				MainForm.FormChanged += MainForm_FormChanged;
				MainForm.ControlChanged -= MainForm_ControlChanged;
				MainForm.ControlChanged += MainForm_ControlChanged;
				SetTargetControl(MainForm, MainForm.TargetControl);

			}
		}

		private void MainForm_ControlChanged(object sender, HyperChangedEventArgs e)
		{
			SetTargetControl(e.Form, e.Control);
		}

		private void MainForm_FormChanged(object sender, HyperChangedEventArgs e)
		{

			SetTargetControl(e.Form, e.Control);
		}

		public void SetTargetControl(HyperBaseForm? bf, HyperControl? c)
		{
			TargetForm = bf;
			TargetControl = c;
			if ((TargetForm != null))
			{
				TargetForm.ControlChanged -= TargetForm_ControlChanged;
				TargetForm.ControlChanged += TargetForm_ControlChanged;
				TargetForm.ControlsChanged -= TargetForm_ControlsChanged;
				TargetForm.ControlsChanged += TargetForm_ControlsChanged;

				TargetForm.CreatedControl -= TargetForm_CreatedControl;
				TargetForm.CreatedControl += TargetForm_CreatedControl;
				TargetForm.DeletetedControl -= TargetForm_DeletetedControl;
				TargetForm.DeletetedControl += TargetForm_DeletetedControl;
			Listup();
			}
		}

		private void TargetForm_ControlsChanged(object sender, HyperChangedEventArgs e)
		{
			if (e.Control != null)
			{
				Listup();
				int idx = e.Control.Index;
				if (SelectedIndex != idx)
				{
					SelectedIndex = idx;
				}
			}
			else
			{

			}
		}

		private void TargetForm_ControlChanged(object sender, HyperChangedEventArgs e)
		{
			if(e.Control!= null)
			{
				int idx = e.Control.Index;
				if(SelectedIndex!=idx)
				{
					SelectedIndex = idx;
				}
			}
			else
			{
				if (SelectedIndex != -1)
				{
					SelectedIndex = -1;
				}
				if((m_pg!=null)&&(MainForm!=null))
				{
					m_pg.SelectedObject = MainForm.targetForm;
				}
			}
		}

		private void TargetForm_DeletetedControl(object? sender, EventArgs e)
		{
			Listup();
		}

		private void TargetForm_CreatedControl(object sender, HyperChangedEventArgs e)
		{
			Listup();
			if (e.Control != null)
			{
				SelectedIndex = e.Control.Index;
			}
		}

		public void Listup()
		{
			this.SuspendLayout();
			base.Items.Clear();

			if((MainForm!=null)&&(TargetForm!=null)&&(TargetForm.Controls.Count>0))
			{
				List<string> strings= new List<string>();
				foreach(Control control in TargetForm.Controls)
				{
					strings.Add(control.Name);
				}
				base.Items.AddRange(strings.ToArray());

				if (TargetForm.TargetIndex >= base.Items.Count)
				{
					TargetForm.TargetIndex = base.Items.Count - 1;
				}
				this.SelectedIndex = TargetForm.TargetIndex;
			}
			this.ResumeLayout();
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			if (TargetForm!= null)
			{

				if (TargetForm.TargetIndex != SelectedIndex)
				{
					HyperControl hc = (HyperControl)TargetForm.Controls[SelectedIndex];
					TargetForm.ChkTargetSelected(hc);
				}
			}
			if(m_pg!=null)
			{
				if (MainForm != null)
				{
					if (SelectedIndex >= 0)
					{
						m_pg.SelectedObject = MainForm.targetControl;
					}
					else
					{
						m_pg.SelectedObject = MainForm.targetForm;
					}
				}

			}
		}
	}
}
