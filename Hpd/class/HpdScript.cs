using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Immutable;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RoslynPad.Roslyn;
using RoslynPad.Editor;
using static System.Windows.Forms.DataFormats;

using BRY;

namespace Hpd
{
    public class HpdScript
    {
        public CustomRoslynHost Host { get; set; } = CustomRoslynHost.Def();
        public Assembly[] RoslynPadAssemblies
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
        public Assembly[] Assemblies
        {
            get
            {
                var ret = new[]
                {
                    typeof(object).Assembly,
                    typeof(MessageBox).Assembly,
					//Assembly.GetExecutingAssembly(),
				};
                return ret;
            }
        }
        public ScriptOptions ScriptOptions
        {
            get { return ScriptOptions.Default.WithReferences(Host.DefaultReferences); }
        }
        public ScriptState? ScriptState = null;
        public HpdScript()
        {
            SetType(null);
        }
        public void SetType(Type? t = null)
        {
            Host = new CustomRoslynHost(
                t,
                RoslynPadAssemblies,
                RoslynHostReferences.NamespaceDefault.With(assemblyReferences: Assemblies));
        }
        public void Init()
        {
        }
        public void Execute(Script script)
        {
            ScriptState = script.RunAsync(Host._targetType).Result;

        }

    }

}
