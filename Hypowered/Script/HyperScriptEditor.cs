using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class HyperScriptEditor : Form
	{

		[Category("Hypowerd")]
		public string ScriptCode
		{
			get { return editPad1.Text; }
			set
			{
				editPad1.Text = value;
			}
		}
		public HyperScriptEditor()
		{
			this.SetStyle(
//ControlStyles.Selectable |
//ControlStyles.UserMouse |
ControlStyles.DoubleBuffer |
ControlStyles.UserPaint |
ControlStyles.AllPaintingInWmPaint |
ControlStyles.SupportsTransparentBackColor,
true);
			this.UpdateStyles();
			//MakeMenu();
			InitializeComponent();
		}
		private void BtnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult= DialogResult.OK;	
		}
	}
}
