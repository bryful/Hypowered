using Elfie.Serialization;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RoslynPad.Editor;
using RoslynPad.Roslyn;
using System.Collections.Immutable;
using System.Reflection;

namespace Hypowered
{

    public class HyperScript
    {
        static private ScriptOptions? m_ScriptOptions = null;
        static public ScriptOptions Options(bool init = false)
        {
            if (m_ScriptOptions == null || init == true)
            {
#pragma warning disable CS8604 // Null 参照引数の可能性があります。
                List<Assembly> assembly = new List<Assembly>()
                {
                    Assembly.GetAssembly(typeof(System.Dynamic.DynamicObject)),  // System.Code
					Assembly.GetAssembly(typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo)),  // Microsoft.CSharp
					Assembly.GetAssembly(typeof(System.Dynamic.ExpandoObject)),
                    Assembly.GetAssembly(typeof(Enumerable)),
                    Assembly.GetAssembly(typeof(System.Data.DataTable)),
                    Assembly.GetAssembly(typeof(object)),
                    Assembly.GetAssembly(typeof(System.Runtime.ProfileOptimization)),

                    Assembly.GetAssembly(typeof(MessageBox)),
                    Assembly.GetExecutingAssembly(),
                };
#pragma warning restore CS8604 // Null 参照引数の可能性があります。
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
                    "Hypowered.App",
					//"Hypowered.App.Project",
					//"Hypowered.HyperScript",
					//"Hypowered.App.Project.Items",
					//"Hypowered.HyperForm",
					//"Hypowered.HyperControl",
				};
                m_ScriptOptions = ScriptOptions.Default.AddReferences(assembly).AddImports(import);
            }
            return m_ScriptOptions;

        }
        public string ExecScript(string source, Type argstype, object args)
        {
            string ret = "";
            try
            {
                var script = CSharpScript.Create(source, globalsType: argstype, options: m_ScriptOptions);
                var result = script.RunAsync(globals: args).Result;
                var value = result.ReturnValue;

                if (value == null)
                {
                    ret = "";
                }
                else
                {
                    ret = value.ToString();
                }
            }
            catch (CompilationErrorException ex)
            {
                MessageBox.Show(ex.Message, "コンパイルエラー");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー");
            }
            return ret;
        }
        public HyperScript()
        {
            m_ScriptOptions = Options();
        }

    }

    public class CustomRoslynHost : RoslynHost
    {
        private readonly Type _targetType;

        public CustomRoslynHost(
            Type targetType,
            IEnumerable<Assembly> additionalAssemblies = null,
            RoslynHostReferences references = null,
            ImmutableArray<string>? disabledDiagnostics = null) : base(additionalAssemblies, references, disabledDiagnostics)
        {
            _targetType = targetType;
        }

        protected override Project CreateProject(
            Solution solution,
            DocumentCreationArgs args,
            CompilationOptions compilationOptions,
            Project? previousProject = null)
        {
            var projectId = ProjectId.CreateNewId();
            var projectInfo = ProjectInfo.Create(
                projectId,
                VersionStamp.Create(),
                "Hypowered",
                "Hypowered",
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
