using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
//using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using Microsoft.CodeAnalysis;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript.JavaScript;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Document;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.Json.Nodes;

namespace Hypowered
{
	public partial class HyperEditor : HyperControl
	{
		public override void ExecScript(ScriptKind sk)
		{
			if (MainForm != null)
			{
				if (ScriptCode.IsScriptCode(sk))
				{
					switch (sk)
					{
						case ScriptKind.ValueChanged:
							MainForm.Script.AddScriptObject("value", Edit.Text);
							MainForm.Script.result = Edit.Text;
							break;
						default:
							MainForm.Script.AddScriptObjectNull("value");
							MainForm.Script.result = null;
							break;
					}
					MainForm.Script.ExecuteScript(ScriptCode, sk);
					MainForm.Script.DeleteScriptObject("value");
				}
			}
		}
		#region Prop
		public override void SetIsEditMode(bool value)
		{
			base.SetIsEditMode(value);
			m_Element.Visible = !m_IsEditMode;
		}
		[Category("Hypowered_Editor")]
		public new string Text
		{
			get { return Edit.Text; }
			set { Edit.Text = value; }
		}
		[Category("Hypowered")]
		public new Font Font
		{
			get 
			{ 
				return m_Element.Font;
			}
			set
			{
				m_Element.Font = value;
				m_Element.Invalidate();
			}
		}
		[Category("Hypowered_Editor")]
		public System.Windows.Media.Brush Background
		{
			get { return Edit.Background; }
			set { Edit.Background = value; }
		}
		[Category("Hypowered_Editor")]
		public System.Windows.Media.Brush Foreground
		{
			get { return Edit.Foreground; }
			set { Edit.Foreground = value; }
		}
		[Category("Hypowered_Editor")]
		public System.Windows.Media.Brush LineNumbersForeground
		{
			get { return Edit.LineNumbersForeground; }
			set { Edit.LineNumbersForeground = value; }
		}
		[Category("Hypowered_Editor")]
		public TextEditorOptions Options
		{
			get { return Edit.Options; }
			set { Edit.Options = value; }
		}
		[Category("Hypowered_Editor")]
		public ScrollBarVisibility HorizontalScrollBarVisibility
		{
			get { return Edit.HorizontalScrollBarVisibility; }
			set { Edit.HorizontalScrollBarVisibility = value; }
		}
		[Category("Hypowered_Editor")]
		public bool ShowLineNumbers
		{
			get { return Edit.ShowLineNumbers; }
			set { Edit.ShowLineNumbers = value; }
		}

		[Category("Hypowered_Editor")]
		public bool WordWrap
		{
			get { return Edit.WordWrap; }
			set { Edit.WordWrap = value; }
		}
		[Category("Hypowered_Editor")]
		public bool ConvertTabsToSpaces
		{
			get { return Edit.Options.ConvertTabsToSpaces; }
			set { Edit.Options.ConvertTabsToSpaces = value; }
		}
		[Category("Hypowered_Editor")]
		public bool ShowColumnRuler
		{
			get { return Edit.Options.ShowColumnRuler; }
			set { Edit.Options.ShowColumnRuler = value; }
		}
		[Category("Hypowered_Editor")]
		public bool ShowTabs
		{
			get { return Edit.Options.ShowTabs; }
			set { Edit.Options.ShowTabs = value; }
		}
		[Category("Hypowered_Editor")]
		public bool ShowEndOfLine
		{
			get { return Edit.Options.ShowEndOfLine; }
			set { Edit.Options.ShowEndOfLine = value; }
		}
		[Category("Hypowered_Editor")]
		public bool ShowSpaces
		{
			get { return Edit.Options.ShowSpaces; }
			set { Edit.Options.ShowSpaces = value; }
		}
		[Category("Hypowered_Editor")]
		public TextDocument Document
		{
			get { return Edit.Document; }
			set { Edit.Document = value; }
		}
		[Category("Hypowered_Editor")]
		public string SelectedText
		{
			get { return Edit.SelectedText; }
			set { Edit.SelectedText = value; }
		}
		[Category("Hypowered_Editor")]
		public int SelectionStart
		{
			get { return Edit.SelectionStart; }
			set { Edit.SelectionStart = value; }
		}
		[Category("Hypowered_Editor")]
		public int SelectionLength
		{
			get { return Edit.SelectionLength; }
			set { Edit.SelectionLength = value; }
		}
		[Category("Hypowered_Editor")]
		public int Offset
		{
			get { return Edit.TextArea.Caret.Offset; }
			set { Edit.TextArea.Caret.Offset = value; }
		}
		[Category("Hypowered_Editor")]
		public bool BackColorTransparent 
		{
			get { return m_Element.BackColorTransparent; }
			set
			{
				m_Element.BackColorTransparent = value;
			} 
		}
		[Category("Hypowered_Editor")]
		public new Color BackColor
		{
			get { return m_Element.BackColor; }
			set
			{
				m_Element.BackColor = value;
				base.BackColor = value;
			}
		}
		[Category("Hypowered_Editor")]
		public new Color ForeColor
		{
			get { return m_Element.ForeColor; }
			set
			{
				m_Element.ForeColor = value;
				base.ForeColor = value;
			}
		}
		public void Select(int start, int length)
		{
			Edit.Select(start, length);
		}
		public void SetText(string s)
		{
			if (Edit.SelectionLength > 0)
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
				int cc = Edit.TextArea.Caret.Offset + s.Length;
				Edit.Document.Insert(Edit.TextArea.Caret.Offset, s);
				try
				{
					Edit.TextArea.Caret.Offset = cc;
				}
				catch { }
				Edit.Focus();
			}
		}
		#endregion
		private ElementHost m_Element = new ElementHost();
		public TextEditor Edit { get; } = new TextEditor();
		public HyperEditor()
		{
			SetControlType(Hypowered.ControlType.Editor);
			SetInScript(InScriptBit.ValueChanged);
			this.Size = new System.Drawing.Size(200, 200);
			m_Element.Location = new System.Drawing.Point(2, 2);
			m_Element.Size = new System.Drawing.Size(196, 196);
			m_Element.Child = Edit;
			Edit.TextChanged += Edit_TextChanged;
			this.Controls.Add(m_Element);
			InitializeComponent();
			ChkSize();
			this.SetStyle(
	//ControlStyles.Selectable |
	//ControlStyles.UserMouse |
	ControlStyles.DoubleBuffer |
	ControlStyles.UserPaint |
	ControlStyles.AllPaintingInWmPaint |
	ControlStyles.SupportsTransparentBackColor,
	true);
			this.UpdateStyles();
		}

		private void Edit_TextChanged(object? sender, EventArgs e)
		{
			ExecScript(ScriptKind.ValueChanged);
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			if (m_IsEditMode)
			{
				base.OnPaint(pe);
			}
			else
			{
				pe.Graphics.Clear(BackColor);
			}
		}
		public void Clear()
		{
			Edit.Clear();
		}
		public void AppendText(string text)
		{
			Edit.AppendText(text);
		}
		private void ChkSize()
		{
			m_Element.Location = new System.Drawing.Point(2, 2);
			m_Element.Size = new System.Drawing.Size(this.Width-4,this.Height-4);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
			m_Element.Invalidate();
			this.Invalidate();
		}
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(ControlType), (int?)ControlType);
			jf.SetValue(nameof(Text), Text);
			jf.SetValue(nameof(Font), Font);
			jf.SetValue(nameof(ForeColor), ForeColor);
			jf.SetValue(nameof(BackColor), BackColor);
			jf.SetValue(nameof(AllowDrop), AllowDrop);
			jf.SetValue(nameof(DragDropFileType), (int)DragDropFileType);
			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("Text", typeof(String).Name);
			if (v != null) Text = (String)v;
			v = jf.ValueAuto("Font", typeof(Font).Name);
			if (v != null) Font = (Font)v;
			v = jf.ValueAuto("Size", typeof(Size).Name);
			if (v != null) Size = (Size)v;
			v = jf.ValueAuto("AllowDrop", typeof(Boolean).Name);
			if (v != null) AllowDrop = (bool)v;
			v = jf.ValueAuto("DragDropFileType", typeof(Int32).Name);
			if (v != null) DragDropFileType = (DragDropFileType)v;
		}
	}
}
