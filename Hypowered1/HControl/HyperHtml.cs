using Markdig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class HyperHtml : HyperControl
	{
		public override void SetIsEditMode(bool value)
		{
			base.SetIsEditMode(value);
			m_webBrowser.Visible = !m_IsEditMode;
			
		}
		private WebBrowser m_webBrowser = new WebBrowser();
		//private WebView2 
		[Browsable(false)]
		public WebBrowser WebBrowser { get { return m_webBrowser; } }
		//[System.ComponentModel.Browsable(false)]
		[Category("Hypowered_Html")]
		public string DocumentText 
		{
			get { return m_webBrowser.DocumentText; }
			set { m_webBrowser.DocumentText = value; } 
		}
		[Category("Hypowered_Html")]
		public string MarkDown
		{
			get { return m_webBrowser.DocumentText; }
			set { m_webBrowser.DocumentText = MdToHtml(value); }
		}
		public void SetMarkDown(string md)
		{
			m_webBrowser.DocumentText = MdToHtml(md);
		}
		[System.ComponentModel.Browsable(false)]
		public string DocumentTitle { get { return m_webBrowser.DocumentTitle; } }
		[System.ComponentModel.Browsable(false)]
		public string DocumentType { get { return m_webBrowser.DocumentType; } }
		[System.ComponentModel.Bindable(true)]
		[Category("Hypowered_Html")]
		public Uri Url
		{ 
			get{ return m_webBrowser.Url; }
			set { m_webBrowser.Url = value; } 
		}
		[Category("Hypowered_Html")]
		public new string[] Lines
		{
			get { return m_webBrowser.DocumentText.Split("\r\n"); }
			set
			{
				m_webBrowser.DocumentText = string.Join("\r\n", value);
			}
		}
		public bool GoBack() { return m_webBrowser.GoBack(); }
		public bool GoForward() { return m_webBrowser.GoForward(); }
		public void GoHome() { m_webBrowser.GoHome(); }
		public void Navigate(System.Uri url) { m_webBrowser.Navigate(url); }
		public void Navigate(string url) { m_webBrowser.Navigate(url); }
		[Category("Hypowered")]
		public new DragDropFileType DragDropFileType
		{
			get { return m_DragDropFileType; }
			set
			{
				m_DragDropFileType = value;
				m_webBrowser.AllowDrop = (m_DragDropFileType != DragDropFileType.None);
			}
		}
		[Category("Hypowered")]
		public new bool AllowDrop
		{
			get { return m_webBrowser.AllowDrop; }
			set
			{
				m_webBrowser.AllowDrop = value;
			}
		}
		public HyperHtml()
		{
			SetControlType(Hypowered.ControlType.Html);
			ScriptCode.SetInScript(InScriptBit.DragDrop);
			BackColor = Color.Transparent;
			IsDrawFrame= false;
			FrameWeight = new Padding(0, 0, 0, 0);
			m_webBrowser.Location = new Point(2, 2);
			m_webBrowser.Size = new Size(150, 150);
			this.Controls.Add(m_webBrowser);
			InitializeComponent();
			this.SetStyle(
	//ControlStyles.Selectable |
	//ControlStyles.UserMouse |
	ControlStyles.DoubleBuffer |
	ControlStyles.UserPaint |
	ControlStyles.AllPaintingInWmPaint |
	ControlStyles.ResizeRedraw|
	ControlStyles.SupportsTransparentBackColor,
	true);
			this.UpdateStyles();
			ChkSize();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			if(m_IsEditMode)
			{
				base.OnPaint(pe);
				using (Pen p = new Pen(ForeColor))
				{
					p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
					pe.Graphics.DrawRectangle(p,new Rectangle(0, 0, Width-1, Height-1));

				}
			}
			else
			{
				using(SolidBrush sb = new SolidBrush(BackColor))
				{
					pe.Graphics.FillRectangle(sb, this.ClientRectangle);
				}
			}
		}
		protected void ChkSize()
		{
			m_webBrowser.Location = new Point(2, 2);
			m_webBrowser.Size = new Size(this.Width - 4, this.Height - 4);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
			m_webBrowser.Invalidate();
			this.Invalidate();
		}
		static string MdToHtml(string input)
		{
			var pipeline = new Markdig.MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
			return Markdig.Markdown.ToHtml(input, pipeline);
		}
	}
}
