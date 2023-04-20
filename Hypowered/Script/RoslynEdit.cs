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
using System.IO;

namespace Hypowered
{
	public partial class RoslynEdit : Control
	{
		public new string Text
		{
			get
			{
				if(Editor!=null)
					return Editor.Text;
				else
					return "";
			}
			set 
			{
				if (Editor != null)
					Editor.Text=value;
			}
		}


		public ElementHost Element = new ElementHost();
		public RoslynCodeEditor? Editor =null;
		public CustomRoslynHost? Host = null;
		public RoslynEdit()
		{
			this.Name = nameof(RoslynEdit);
			this.Size = new Size(400, 410);
			Element.Name = "ElementHost";
			Element.Size = this.Size;
			Element.Location= new Point(0,0);
			var roslynPadAssemblies = new[]
			{
				Assembly.Load("RoslynPad.Roslyn.Windows"),
				Assembly.Load("RoslynPad.Editor.Windows"),
				Assembly.Load("System"),
				Assembly.Load("System.IO"),
				Assembly.Load("System.Windows.Forms"),
				typeof(System.Dynamic.DynamicObject).Assembly,  // System.Code
				//typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo).Assembly,  // Microsoft.CSharp
				typeof(System.Dynamic.ExpandoObject).Assembly,
				typeof(System.Data.DataTable).Assembly,
				typeof(System.Object).Assembly,
				typeof(HRoot).Assembly,
				Assembly.GetExecutingAssembly(),
			};
			var assemblies = new[]
			{
				Assembly.Load("System.Private.CoreLib"),
				typeof(System.Dynamic.DynamicObject).Assembly,
				typeof(System.Dynamic.ExpandoObject).Assembly,
				typeof(System.Object).Assembly,
				typeof(System.Runtime.DependentHandle).Assembly,
				typeof(System.Windows.Forms.MessageBox).Assembly,
				typeof(HRoot).Assembly,
				Assembly.GetExecutingAssembly(),
			};
			SetAssemblies(typeof(HRoot), roslynPadAssemblies,assemblies);
			Element.Child = Editor;
			this.Controls.Add(Element);
			InitializeComponent();
		}
		private  void SetAssemblies(Type t,Assembly[] ros, Assembly[] abs)
		{

			Host = new CustomRoslynHost(
				t,
				ros,
				RoslynHostReferences.NamespaceDefault.With(assemblyReferences: abs));

			Editor = new RoslynCodeEditor();
			Editor.Name= nameof(RoslynEdit);
			_ = Editor.InitializeAsync(Host, new ClassificationHighlightColors(), Directory.GetCurrentDirectory(), string.Empty, SourceCodeKind.Script);
			Element.Child = Editor;
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Element.Size = this.Size;
		}

	}
	public class CustomRoslynHost : RoslynHost
	{
		public readonly Type _targetType;
		static public Assembly[] RoslynPadAssemblies
		{
			get
			{
				var ret = new[]
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
				return ret;
			}
		}
		static public Assembly[] Assemblies
		{
			get
			{
				var ret = new[]
				{
					typeof(System.Object).Assembly,
					typeof(System.Windows.Forms.MessageBox).Assembly,
					//Assembly.GetExecutingAssembly(),
				};
				return ret;
			}
		}

		static public CustomRoslynHost Def()
		{
			return new CustomRoslynHost(
				null,
				RoslynPadAssemblies,
				RoslynHostReferences.NamespaceDefault.With(assemblyReferences: Assemblies));
		}

		public CustomRoslynHost(
			Type targetType,
			IEnumerable<Assembly> additionalAssemblies = null,
			RoslynHostReferences references = null,
			ImmutableArray<string>? disabledDiagnostics = null) : base(additionalAssemblies, references, disabledDiagnostics)
		{
			_targetType = targetType;
		}

		protected override Project CreateProject(Solution solution, DocumentCreationArgs args, CompilationOptions compilationOptions, Project? previousProject = null)
		{
			var projectId = ProjectId.CreateNewId();
			var projectInfo = ProjectInfo.Create(
				projectId,
				VersionStamp.Create(),
				"MyProject",
				"MyAssembly",
				LanguageNames.CSharp,
				compilationOptions: compilationOptions,
				parseOptions: new CSharpParseOptions(kind: SourceCodeKind.Script),
				metadataReferences: DefaultReferences,
				isSubmission: true,
				hostObjectType: _targetType);
			return solution.AddProject(projectInfo).GetProject(projectId);
		}
	}
}
