using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using Microsoft.CodeAnalysis;
using RoslynPad;
using RoslynPad.Editor;
using RoslynPad.Roslyn;

namespace Hypowered
{
    public partial class EditPad : System.Windows.Forms.Control
	{
		#region Prop
		[Category("_ED")]
		public new string Text
		{
			get { return Edit.Text; }
			set { Edit.Text = value; }
		}
		[Category("_ED")]
		public System.Windows.Media.Brush Background
		{
			get { return Edit.Background; }
			set { Edit.Background = value; }
		}
		[Category("_ED")]
		public System.Windows.Media.Brush Foreground
		{
			get { return Edit.Foreground; }
			set { Edit.Foreground = value; }
		}
		[Category("_ED")]
		public System.Windows.Media.Brush LineNumbersForeground
		{
			get { return Edit.LineNumbersForeground; }
			set { Edit.LineNumbersForeground = value; }
		}
		[Category("_ED")]
		public TextEditorOptions Options
		{
			get { return Edit.Options; }
			set { Edit.Options = value; }
		}
		[Category("_ED")]
		public ScrollBarVisibility HorizontalScrollBarVisibility
		{
			get { return Edit.HorizontalScrollBarVisibility; }
			set { Edit.HorizontalScrollBarVisibility = value; }
		}
		[Category("_ED")]
		public bool ShowLineNumbers
		{
			get { return Edit.ShowLineNumbers; }
			set { Edit.ShowLineNumbers = value; }
		}

		[Category("_ED")]
		public bool WordWrap
		{
			get { return Edit.WordWrap; }
			set { Edit.WordWrap = value; }
		}
		[Category("_ED")]
		public bool ConvertTabsToSpaces
		{
			get { return Edit.Options.ConvertTabsToSpaces; }
			set { Edit.Options.ConvertTabsToSpaces = value; }
		}
		[Category("_ED")]
		public bool ShowColumnRuler
		{
			get { return Edit.Options.ShowColumnRuler; }
			set { Edit.Options.ShowColumnRuler = value; }
		}
		[Category("_ED")]
		public bool ShowTabs
		{
			get { return Edit.Options.ShowTabs; }
			set { Edit.Options.ShowTabs = value; }
		}
		[Category("_ED")]
		public bool ShowEndOfLine
		{
			get { return Edit.Options.ShowEndOfLine; }
			set { Edit.Options.ShowEndOfLine = value; }
		}
		[Category("_ED")]
		public bool ShowSpaces
		{
			get { return Edit.Options.ShowSpaces; }
			set { Edit.Options.ShowSpaces = value; }
		}
		
		#endregion
		private ElementHost m_Element = new ElementHost();
		public CustomRoslynHost Host;
		public RoslynCodeEditor Edit = new RoslynCodeEditor();
		public EditPad()
		{
			InitializeComponent();
			this.Size = new System.Drawing.Size(200,200);
			m_Element.Location = new System.Drawing.Point(0,0);
			m_Element.Size = this.Size;
			Host = new CustomRoslynHost(
				typeof(App),
			additionalAssemblies: new[]
			{
				Assembly.Load("System"),
				//Assembly.Load("System.Runtime"),
				//Assembly.Load("System.Dynamic"),
				Assembly.Load("System.Linq"),
				//Assembly.Load("System.Text"),
				Assembly.Load("System.IO"),
				//Assembly.Load("System.Collections.Generic"),
				Assembly.Load("System.Data"),
				Assembly.Load("System.Windows.Forms"),
				Assembly.Load("RoslynPad.Roslyn.Windows"),
				Assembly.Load("RoslynPad.Editor.Windows"),
				//Assembly.Load("app"),
				//Assembly.Load("Hypowered"),
				//Assembly.Load("Hypowered.App"),
				//Assembly.Load("Hypowered.App.Items"),
				Assembly.GetExecutingAssembly(),

			},
			 references: RoslynHostReferences.NamespaceDefault.With(typeNamespaceImports: new[] 
			 {
			 //typeof(System.Array),
			 typeof(System.Object),
			 typeof(System.Enum),
			 typeof(System.Linq.Enumerable),
			 typeof(System.Dynamic.DynamicObject),
			 typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo),  // Microsoft.CSharp
			 typeof(System.Dynamic.ExpandoObject),
			 typeof(System.Data.DataTable),
			 typeof(System.IO.File),
			 typeof(System.Windows.Forms.MessageBox),
			 typeof(Hypowered.App),

			 })
			);
			Edit.Loaded += EditorLoaded;
			m_Element.Child = Edit;
			this.Controls.Add(m_Element);
			Edit.Options.IndentationSize = 4;
			
		}
		
		public void Clear()
		{
			Edit.Clear();
		}
		public void AppendText(string text)
		{
			Edit.AppendText(text);
		}
		protected virtual void EditorLoaded(object sender, System.Windows.RoutedEventArgs e)
		{
			Init(Host);
		}
		public void Init(RoslynHost h)
		{
			Edit.InitializeAsync(
				h,
				new ClassificationHighlightColors(),
				Directory.GetCurrentDirectory(),
				String.Empty,
				SourceCodeKind.Script
				);
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			m_Element.Size = this.Size;
		}
	}
}
