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
			set { m_TargetIndex = value; this.Invalidate(); }
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
