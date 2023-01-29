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
			var roslynPadAssemblies = new[]
				   {
				Assembly.Load("RoslynPad.Roslyn.Windows"),
				Assembly.Load("RoslynPad.Editor.Windows"),
				Assembly.Load("System"),
				Assembly.Load("System.IO"),
				Assembly.Load("System.Windows.Forms"),
				typeof(System.Dynamic.DynamicObject).Assembly,  // System.Code
				typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo).Assembly,  // Microsoft.CSharp
				typeof(System.Dynamic.ExpandoObject).Assembly,
				typeof(System.Data.DataTable).Assembly,
				//Assembly.GetExecutingAssembly(),
				};
			var assemblies = new[] {
				typeof(System.Object).Assembly,
				typeof(System.Windows.Forms.MessageBox).Assembly,
				typeof(Main).Assembly,
				//Assembly.GetExecutingAssembly(),
			};
			//roslynPadControl1.SetAssemblies(typeof(Form1), roslynPadAssemblies, assemblies);
			string s = "";
			foreach(Control c in Controls)
			{
				s += c.Name + "\r\n";
			}
		}

		public string ExecScript(string source)
		{
			return ExecScript(source , typeof(Main),this);	
		}
		public string ExecScript(string source, Type argstype, object args)
		{
			try
			{
				List<Assembly> assembly = new List<Assembly>()
				{
					typeof(System.Dynamic.DynamicObject).Assembly,  // System.Code
					typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo).Assembly,  // Microsoft.CSharp
					typeof(System.Dynamic.ExpandoObject).Assembly,
					typeof(System.Data.DataTable).Assembly,
					typeof(System.Windows.Forms.MessageBox).Assembly,
					typeof(Main).Assembly,
					typeof(RoslynEdit).Assembly,

				};

				List<string> import = new List<string>()
				{
					"System",
					"System.Dynamic", 
					"System.Linq", 
					"System.Text",
					"System.IO",
					"System.Collections.Generic", 
					"System.Data", 
					"System.Windows.Forms",
					"HpdTest",
					"HpdTest.From1",
				};

				var opt = ScriptOptions.Default.AddReferences(assembly).AddImports(import);

				var script = CSharpScript.Create(source, globalsType: argstype, options: opt);
				
				var result = script.RunAsync(globals: args).Result;
				//script.RunAsync
				var value = result.ReturnValue;
				if(value != null)
				{
					value= value.ToString();
				}
				else
				{
					value= "";
				}
			}
			catch (ArgumentNullException e)
			{
				return e.ToString();
			}
			catch (NotSupportedException e)
			{
				return e.ToString();
			}
			catch (CompilationErrorException e)
			{
				return e.ToString();
			}
			catch (Exception ex)
			{
				return ex.ToString();
			}
			return "";
		}


		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			this.Text = $"w:{this.Width},h:{this.Height},c:{ClientRectangle.ToString()} ,w:{HpdLayout.GetControlSize(this).ToString()} ";
		}

		private void hpdButton2_Click(object sender, EventArgs e)
		{
			NewControlDialog dlg = new NewControlDialog();
			bool b = (dlg.ShowDialog() == DialogResult.OK);
			MessageBox.Show(b.ToString());
			dlg.Dispose();
		}

		private void button1_Click_2(object sender, EventArgs e)
		{
			
		}

		private void cCToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowPropertyForm();
		}

		private void eDToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowScriptEditor();
		}

		private void tTToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PropListToClipboard(typeof(HpdForm), "HpdForm");
		}
	}

}