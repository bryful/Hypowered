using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
    public partial class HyperTextBox : HyperControl
	{

		public override void SetIsEditMode(bool value)
		{
			base.SetIsEditMode(value);
			m_TextBox.Visible = !m_IsEditMode;
			if (m_TextBox.Visible ==false)
			{
				base.Text = m_TextBox.Text;
			}
			else
			{
				m_TextBox.Text =base.Text;
			}
		}
		private TextBox m_TextBox = new TextBox();
		[Category("Hypowered_TextBox")]
		public TextBox TextBox
		{
			get { return m_TextBox; }
			set { m_TextBox = value; }
		}
		[Category("Hypowered_TextBox")]
		public bool Multiline
		{
			get { return m_TextBox.Multiline; }
			set { m_TextBox.Multiline = value; ChkSize(); }
		}
		[Category("Hypowered_TextBox")]
		public ScrollBars ScrollBars
		{
			get { return m_TextBox.ScrollBars; }
			set { m_TextBox.ScrollBars = value; ChkSize(); }
		}
		[Category("Hypowered_TextBox")]
		public BorderStyle BorderStyle
		{
			get { return m_TextBox.BorderStyle; }
			set { m_TextBox.BorderStyle = value; }
		}
		[Category("Hypowered_TextBox")]
		public new string Text
		{
			get { return m_TextBox.Text; }
			set 
			{
				base.Text = value;
				m_TextBox.Text = value; 
			}
		}
		[Category("Hypowered_TextBox")]
		public  HorizontalAlignment TextAlign
		{
			get { return m_TextBox.TextAlign; }
			set { m_TextBox.TextAlign = value; }
		}
		[Category("Hypowered")]
		public new Font Font
		{
			get { return m_TextBox.Font; }
			set
			{
				base.Font =
				m_TextBox.Font = value;
				this.Size = new Size(m_TextBox.Width + 2, m_TextBox.Height + 2);
			}
		}
		[Category("Hypowered_TextBox")]
		public bool ReadOnly
		{
			get { return m_TextBox.ReadOnly; }
			set
			{
				m_TextBox.ReadOnly = value;
			}
		}
		[Category("Hypowered_Color")]
		public new Color ForeColor
		{
			get { return m_TextBox.ForeColor; }
			set
			{
				base.ForeColor = value;
				m_TextBox.ForeColor = value;
			}
		}
		[Category("Hypowered_Color")]
		public new Color BackColor
		{
			get { return m_TextBox.BackColor; }
			set
			{
				base.BackColor = value;
				m_TextBox.BackColor = value;
			}
		}
		[Category("Hypowered")]
		public new DragDropFileType DragDropFileType
		{
			get { return m_DragDropFileType; }
			set
			{
				m_DragDropFileType = value;
				m_TextBox.AllowDrop = (m_DragDropFileType != DragDropFileType.None);
			}
		}
		[Category("Hypowered")]
		public new bool AllowDrop
		{
			get { return m_TextBox.AllowDrop; }
			set
			{
				//base.AllowDrop = value;
				m_TextBox.AllowDrop = value;
			}
		}
		public HyperTextBox()
		{
			SetControlType(Hypowered.ControlType.TextBox);
			SetInScript( InScriptBit.DragDrop);
			m_TextBox.BorderStyle = BorderStyle.FixedSingle;
			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			this.Size = new Size(m_TextBox.Width,m_TextBox.Height);
			m_TextBox.Location = new Point(0, 0);
			InitializeComponent();
			this.Controls.Add(m_TextBox);
			this.Controls.SetChildIndex(m_TextBox, 0);
			m_TextBox.DragEnter += M_TextBox_DragEnter;
			m_TextBox.DragDrop += M_TextBox_DragDrop;
		}

		private void M_TextBox_DragEnter(object? sender, DragEventArgs e)
		{
			if ((e.Data != null) && (m_DragDropFileType != DragDropFileType.None))
			{
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
				{
					e.Effect = DragDropEffects.Copy;

				}
			}
		}

		private void M_TextBox_DragDrop(object? sender, DragEventArgs e)
		{
			if ((e.Data != null) && (m_DragDropFileType != DragDropFileType.None))
			{
				m_DragDropItems = new string[0];
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
				List<string> list = new List<string>();
				if (files.Length > 0)
				{
					foreach (string file in files)
					{
						if ((m_DragDropFileType == DragDropFileType.FileOnly)
							|| (m_DragDropFileType == DragDropFileType.FileAndDirectory))
						{
							if (File.Exists(file))
							{
								list.Add(file);
							}
						}
						else if ((m_DragDropFileType == DragDropFileType.DirectoryOnly)
							|| (m_DragDropFileType == DragDropFileType.FileAndDirectory))
						{
							if (Directory.Exists(file))
							{
								list.Add(file);
							}
						}

					}
				}
				m_DragDropItems = list.ToArray();
				ExecScript(ScriptKind.DragDrop);
			}
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
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
		}
		private void ChkSize()
		{
			m_TextBox.Size = new Size(this.Size.Width, this.Size.Height);
			if (Multiline == false)
			{
				if (m_TextBox.Height != this.Height)
				{
					this.Size = new Size(this.Size.Width, m_TextBox.Height);
				}
			}
		}
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(ControlType), (int?)ControlType);
			jf.SetValue(nameof(Multiline), Multiline);
			jf.SetValue(nameof(Text), Text);
			jf.SetValue(nameof(TextAlign), (int)TextAlign);
			jf.SetValue(nameof(Font), Font);
			jf.SetValue(nameof(ReadOnly), ReadOnly);
			jf.SetValue(nameof(ForeColor), ForeColor);
			jf.SetValue(nameof(BackColor), BackColor);
			jf.SetValue(nameof(BorderStyle), (int)BorderStyle);
			jf.SetValue(nameof(ScrollBars), (int)ScrollBars);
			jf.SetValue(nameof(AllowDrop), AllowDrop);
			jf.SetValue(nameof(DragDropFileType), (int)DragDropFileType);
			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("Multiline", typeof(Boolean).Name);
			if (v != null) Multiline = (Boolean)v;
			v = jf.ValueAuto("Text", typeof(String).Name);
			if (v != null) Text = (String)v;
			v = jf.ValueAuto("TextAlign", typeof(HorizontalAlignment).Name);
			if (v != null) TextAlign = (HorizontalAlignment)v;
			v = jf.ValueAuto("Font", typeof(Font).Name);
			if (v != null) Font = (Font)v;
			v = jf.ValueAuto("ReadOnly", typeof(Boolean).Name);
			if (v != null) ReadOnly = (Boolean)v;
			v = jf.ValueAuto("ForeColor", typeof(Color).Name);
			if (v != null) ForeColor = (Color)v;
			v = jf.ValueAuto("BackColor", typeof(Color).Name);
			if (v != null) BackColor = (Color)v;
			v = jf.ValueAuto("BorderStyle", typeof(Int32).Name);
			if (v != null) BorderStyle = (BorderStyle)v;
			v = jf.ValueAuto("ScrollBars", typeof(Int32).Name);
			if (v != null) ScrollBars = (ScrollBars)v;
			v = jf.ValueAuto("Size", typeof(Size).Name);
			if (v != null) Size = (Size)v;
			v = jf.ValueAuto("AllowDrop", typeof(Boolean).Name);
			if (v != null) AllowDrop = (bool)v;
			v = jf.ValueAuto("DragDropFileType", typeof(Int32).Name);
			if (v != null) DragDropFileType = (DragDropFileType)v;
		}
	}
}
