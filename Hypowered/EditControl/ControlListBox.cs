using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class ControlListBox : ListBox
	{
		public HyperMainForm? MainForm = null;
		public HyperBaseForm? TargetForm = null;
		public HyperControl? TargetControl = null;

		[Category("Hypowerd_Color")]
		public new Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; this.Invalidate(); }
		}
		[Category("Hypowerd_Color")]
		public new Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; this.Invalidate(); }
		}
		[Category("Hypowerd")]
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
			InitializeComponent();
		}
		public void SetMainForm(HyperMainForm? mf)
		{
			this.MainForm = mf;
			if (MainForm != null)
			{
				MainForm.FormChanged -= MainForm_FormChanged;
				MainForm.FormChanged += MainForm_FormChanged;
				SetTargetControl(MainForm, MainForm.TargetControl);
			}
		}

		private void MainForm_FormChanged(object sender, HyperChangedEventArgs e)
		{

			SetTargetControl(e.Form, e.Control);
		}

		public void SetTargetControl(HyperBaseForm? bf, HyperControl? c)
		{
			if ((bf!=null))
			{
				if (TargetForm != bf)
				{
					TargetForm = bf;
					TargetForm.ControlChanged -= TargetForm_ControlChanged;
					TargetForm.ControlChanged += TargetForm_ControlChanged;
					TargetForm.CreatedControl -= TargetForm_CreatedControl;
					TargetForm.CreatedControl += TargetForm_CreatedControl;
					TargetForm.DeletetedControl -= TargetForm_DeletetedControl;
					TargetForm.DeletetedControl += TargetForm_DeletetedControl;
				}
				if (c != null)
				{
					TargetControl = c;
				}
				Listup();
				if (c != null)
				{
					SelectedIndex = c.Index;
				}

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
			if(TargetForm!= null)
			{
				if (TargetForm.TargetIndex != SelectedIndex)
				{
					TargetForm.TargetIndex = SelectedIndex;
				}
			}
		}

	}
}
