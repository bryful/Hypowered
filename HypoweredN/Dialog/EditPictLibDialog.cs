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
	public partial class EditPictLibDialog : BaseForm
	{
		private HyperMainForm? m_form = null;
		public int TargetIndex
		{
			get { return pictLibBox1.TargetIndex; }
			set { pictLibBox1.TargetIndex = value; }
		}
		private bool m_PictDownMode = false;
		public bool PictDownMode
		{
			get { return m_PictDownMode; }
			set
			{
				bool b = (m_PictDownMode != value);
				m_PictDownMode = value;
				rbPictName.Visible = m_PictDownMode;
				rbPictName_Down.Visible = m_PictDownMode;
				rbPictName.Checked = value;
				rbPictName_Down.Checked = !value;
				if(m_PictDownMode)
				{
					if(b) m_PictName = pictLibBox1.PictName;
					pictLibBox1.PictName = m_PictName_Down;
				}
				else
				{
					if (b) m_PictName_Down = pictLibBox1.PictName;
					pictLibBox1.PictName = m_PictName;
				}
			}
		}
		private bool m_TargetDown = false;
		public bool TargetDown
		{
			get { return m_TargetDown; }
			set 
			{
				if(m_TargetDown != value)
				{
					m_TargetDown = value;
					if(rbPictName_Down.Checked!=m_TargetDown)
					{
						rbPictName_Down.Checked = m_TargetDown;
					}
					if(m_TargetDown)
					{
						m_PictName = pictLibBox1.PictName;
						pictLibBox1.PictName = m_PictName_Down;
					}
					else
					{
						m_PictName_Down = pictLibBox1.PictName;
						pictLibBox1.PictName = m_PictName;
					}
				}

			}
		}
		private string m_PictName = "";
		public string PictName
		{
			get { return m_PictName; }
			set
			{
				m_PictName = value; 
				if(m_PictDownMode==false)
				{
					pictLibBox1.PictName = m_PictName;
				}
			}
		}
		private string m_PictName_Down = "";
		public string PictName_Down
		{
			get { return m_PictName_Down; }
			set
			{ 
				m_PictName_Down = value;
				if (m_PictDownMode == true)
				{
					pictLibBox1.PictName = m_PictName;
				}
			}
		}

		public EditPictLibDialog()
		{
			this.StartPosition = FormStartPosition.Manual;
			InitializeComponent();
			rbPictName.Tag = false;
			rbPictName_Down.Tag = true;
			rbPictName.CheckedChanged += RbPictName_CheckedChanged;
			rbPictName_Down.CheckedChanged += RbPictName_CheckedChanged;
			pictLibBox1.TargetIndexChanged += PictLibBox1_TargetIndexChanged;
		}

		private void PictLibBox1_TargetIndexChanged(object sender, PictLibBox.TargetIndexChangedEventArgs e)
		{
			if(m_TargetDown)
			{
				m_PictName_Down = pictLibBox1.PictName;
			}
			else
			{
				m_PictName = pictLibBox1.PictName;
			}
		}

		private void RbPictName_CheckedChanged(object? sender, EventArgs e)
		{
			RadioButton? rb = (RadioButton?)sender;
			if (rb == null) return;
			TargetDown = (bool)rb.Tag;
		}

		public void SetMainForm(HyperMainForm? mf)
		{
			m_form = mf;
			pictLibBox1.SetMainForm(mf);
		}
	}
}
