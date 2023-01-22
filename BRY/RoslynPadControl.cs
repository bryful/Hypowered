using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Reflection;
using BRY;
using RoslynPad.Editor;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using RoslynPad.Roslyn;
using System.Collections.Immutable;
using System.Windows.Forms.Integration;
using System.IO;

namespace BRY
{
	public partial class RoslynPadControl : Control
	{
		public ElementHost Element = new ElementHost();
		public RoslynCodeEditor Editor = new RoslynCodeEditor();
		public CustomRoslynHost? Host = null;
		public RoslynPadControl()
		{
			this.Name = nameof(RoslynPadControl);
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
				typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo).Assembly,  // Microsoft.CSharp
				typeof(System.Dynamic.ExpandoObject).Assembly,
				typeof(System.Data.DataTable).Assembly,
			};
			var assemblies = new[]
			{
				Assembly.Load("System.Private.CoreLib")
			};
			SetAssemblies(typeof(RoslynPadControl), roslynPadAssemblies,assemblies);
			Element.Child = Editor;
			this.Controls.Add(Element);
			InitializeComponent();
		}
		public  void SetAssemblies(Type t,Assembly[] ros, Assembly[] abs)
		{

			Host = new CustomRoslynHost(
				t,
				ros,
				RoslynHostReferences.NamespaceDefault.With(assemblyReferences: abs));

			Editor = new RoslynCodeEditor();
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
