using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	partial class HyperForm
	{
		protected int m_TargetIndex = -1;
		[Category("Hypowerd_Form")]
		public int TargetIndex
		{
			get { return m_TargetIndex; }
			set 
			{
				if(m_TargetIndex != value)
				{
					m_TargetIndex = value;
					OnTargetChanged(new TargetChangedEventArgs(m_TargetIndex, TargetControl));
				}
				this.Invalidate();
			}
		}
		private string m_FileName = "Home";
		[Category("Hypowerd_Form")]
		public string FileName
		{
			get { return m_FileName; }
			set
			{
				m_FileName = value;
				base.Name = Path.GetFileNameWithoutExtension(value);
			}
		}
		[Category("Hypowerd_Form")]
		public new string Name
		{
			get { return base.Name; }
			set
			{
				if(m_FileName=="")
				{
					m_FileName = value;
				}
				else
				{
					string? d = Path.GetDirectoryName(m_FileName);
					string e = Path.GetExtension(m_FileName);
					m_FileName = value + e;
					if (d != null)
					{
						m_FileName = Path.Combine(d, m_FileName);
					}

				}
				base.Name = value;

			}
		}

		[Browsable(false)]
		public HyperControl? TargetControl
		{
			get
			{
				if((m_TargetIndex>=0)&& (m_TargetIndex < this.Controls.Count))
				{
					return (HyperControl)this.Controls[m_TargetIndex];
				}
				else
				{
					return null;
				}
			}
		}
		[Category("Hypowerd_Form")]
		public new Size Size
		{
			get { return base.Size; }
			set { base.Size = value; this.Invalidate(); }
		}
		private Color m_SelectedColor = Color.Red;
		[Category("Hypowerd_Color")]
		public Color SelectedColor
		{
			get { return m_SelectedColor; }
			set { m_SelectedColor = value; this.Invalidate(); }
		}
		private Color m_TargetColor = Color.Blue;
		[Category("Hypowerd_Color")]
		public Color TargetColor
		{
			get { return m_TargetColor; }
			set { m_TargetColor = value; this.Invalidate(); }
		}
		[Category("Hypowerd_Color")]
		public bool IsShowMenu
		{
			get { return m_menuBar.Visible; }
			set { m_menuBar.Visible = value; this.Invalidate(); }
		}
		// ***********************************************************************************
		[Browsable(false)]
		public new bool KeyPreview
		{
			get { return base.KeyPreview; }
			//set { base.KeyPreview = value; this.Invalidate(); }
		}
		private bool ShouldSerializeKeyPreview()
		{
			return false;
		}
	}
}
