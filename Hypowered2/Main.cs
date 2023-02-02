using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Reflection;
using RoslynPad.Editor;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using RoslynPad.Roslyn;
using System.Collections.Immutable;
using System.Windows.Forms.Integration;

namespace Hpd
{
	public partial class Main : HpdMainForm
	{
		public Main()
		{
			InitializeComponent();
		}



		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			this.Text = $"w:{this.Width},h:{this.Height},c:{ClientRectangle.ToString()} ,w:{HpdLayout.GetControlSize(this).ToString()} ";
		}
	}

}