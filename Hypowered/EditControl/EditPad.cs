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
//using Microsoft.CodeAnalysis;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript.JavaScript;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Document;

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

		[Category("_ED")]
		public TextDocument Document
		{
			get { return Edit.Document; }
			set { Edit.Document= value; }
		}
		[Category("_ED")]
		public string SelectedText
		{
			get { return Edit.SelectedText; }
			set { Edit.SelectedText = value; }
		}
		[Category("_ED")]
		public int SelectionStart
		{
			get { return Edit.SelectionStart; }
			set { Edit.SelectionStart = value; }
		}
		[Category("_ED")]
		public int SelectionLength
		{
			get { return Edit.SelectionLength; }
			set { Edit.SelectionLength = value; }
		}
		[Category("_ED")]
		public int Offset
		{
			get { return Edit.TextArea.Caret.Offset; }
			set { Edit.TextArea.Caret.Offset = value; }
		}
		public void Select(int start,int length)
		{
			Edit.Select(start, length);
		}
		public void SetText(string s)
		{
			if(Edit.SelectionLength> 0)
			{
				int ss = Edit.SelectionStart + Edit.SelectionLength;
				Edit.SelectedText = s;
				try
				{
					Edit.TextArea.Caret.Offset = ss;
					Edit.SelectionLength = 0;
				}
				catch { }
				Edit.Focus();
			}
			else
			{
				int cc = Edit.TextArea.Caret.Offset+s.Length;
				Edit.Document.Insert(Edit.TextArea.Caret.Offset, s);
				try
				{
					Edit.TextArea.Caret.Offset = cc;
				} catch { }
				Edit.Focus();
			}
		}
		#endregion
		private ElementHost m_Element = new ElementHost();
		public TextEditor Edit { get;} = new TextEditor();
		public EditPad()
		{
			InitializeComponent();
			this.Size = new System.Drawing.Size(200,200);
			m_Element.Location = new System.Drawing.Point(0,0);
			m_Element.Size = this.Size;
	
			m_Element.Child = Edit;
			this.Controls.Add(m_Element);
			//Edit.Options.IndentationSize = 4;
			
			
		}


		public void Clear()
		{
			Edit.Clear();
		}
		public void AppendText(string text)
		{
			Edit.AppendText(text);
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
