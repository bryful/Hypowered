﻿using System;
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
	public partial class EditPictLibDialog : EditForm
	{
		private HyperMainForm? m_form = null;
		public int TargetIndex
		{
			get { return pictLibBox1.TargetIndex; }
			set { pictLibBox1.TargetIndex = value; }
		}
		public string PictName
		{
			get { return pictLibBox1.PictName; }
			set { pictLibBox1.PictName = value; }
		}
		public EditPictLibDialog()
		{
			this.StartPosition = FormStartPosition.Manual;
			InitializeComponent();
		}
		public void SetMainForm(HyperMainForm? mf)
		{
			m_form = mf;
			pictLibBox1.SetMainForm(mf);
		}
	}
}