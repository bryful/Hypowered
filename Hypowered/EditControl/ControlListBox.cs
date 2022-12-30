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
	public partial class ControlListBox : ListBox
	{
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
		private HyperMainForm? m_HyperForm = null;
		[Category("Hypowerd")]
		public HyperMainForm? MainForm
		{
			get { return m_HyperForm; }
			set { SetHyperForm(value);}
		}

		private ControlCollection? m_HyperControls = null;
		[Category("Hypowerd")]
		public ControlCollection? HyperControls
		{
			get { return m_HyperControls; }
		}
		public ControlListBox()
		{
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			BorderStyle= BorderStyle.FixedSingle;
			InitializeComponent();
		}
		public void SetHyperForm(HyperMainForm hf)
		{
			base.Items.Clear();
			this.Items.Clear();
			m_HyperForm = hf;
			if(m_HyperForm!=null)
			{
				m_HyperControls = m_HyperForm.Controls;
				Listup();
				
				m_HyperForm.TargetChanged += M_HyperForm_TargetChanged;
				m_HyperForm.ControlChanged += M_HyperForm_ControlChanged;
			}
		}

		private void M_HyperForm_ControlChanged(object? sender, EventArgs e)
		{
			Listup();
		}

		private void M_HyperForm_TargetChanged(object sender, TargetChangedEventArgs e)
		{
			if(SelectedIndex != e.Index)
			{
				SelectedIndex = e.Index;
			}
		}
		public void Listup()
		{
			this.SuspendLayout();
			base.Items.Clear();

			if((m_HyperForm!=null)&&(m_HyperControls!=null)&&(m_HyperControls.Count>0))
			{
				List<string> strings= new List<string>();
				foreach(Control control in m_HyperControls)
				{
					strings.Add(control.Name);
				}
				base.Items.AddRange(strings.ToArray());

				this.SelectedIndex = m_HyperForm.TargetIndex;
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
			if(m_HyperForm!= null)
			{
				if (m_HyperForm.TargetIndex != SelectedIndex)
				{
					m_HyperForm.TargetIndex = SelectedIndex;
				}
			}
		}
	}
}
