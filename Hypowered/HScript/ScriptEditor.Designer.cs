namespace Hypowered
{
	partial class ScriptEditor
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Media.SolidColorBrush solidColorBrush1 = new System.Windows.Media.SolidColorBrush();
			ICSharpCode.AvalonEdit.Document.TextDocument textDocument1 = new ICSharpCode.AvalonEdit.Document.TextDocument();
			System.ComponentModel.Design.ServiceContainer serviceContainer1 = new System.ComponentModel.Design.ServiceContainer();
			ICSharpCode.AvalonEdit.Document.UndoStack undoStack1 = new ICSharpCode.AvalonEdit.Document.UndoStack();
			System.Windows.Media.SolidColorBrush solidColorBrush2 = new System.Windows.Media.SolidColorBrush();
			System.Windows.Media.SolidColorBrush solidColorBrush3 = new System.Windows.Media.SolidColorBrush();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptEditor));
			this.editPad1 = new Hypowered.EditPad();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.btnScriptEdit = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnExec = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.btnEditEnd = new System.Windows.Forms.ToolStripButton();
			this.cmbWord = new System.Windows.Forms.ToolStripComboBox();
			this.lbControl = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.cmbScript = new System.Windows.Forms.ToolStripComboBox();
			this.btnHide = new System.Windows.Forms.ToolStripButton();
			this.controlBrowser1 = new Hypowered.ControlBrowser();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.toolStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// editPad1
			// 
			this.editPad1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			solidColorBrush1.Color = System.Windows.Media.Color.FromArgb(((byte)(255)), ((byte)(221)), ((byte)(221)), ((byte)(221)));
			this.editPad1.Background = solidColorBrush1;
			this.editPad1.ConvertTabsToSpaces = true;
			this.editPad1.Dock = System.Windows.Forms.DockStyle.Fill;
			textDocument1.FileName = null;
			textDocument1.ServiceProvider = serviceContainer1;
			textDocument1.Text = "";
			undoStack1.SizeLimit = 2147483647;
			textDocument1.UndoStack = undoStack1;
			this.editPad1.Document = textDocument1;
			this.editPad1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.editPad1.ForeColor = System.Drawing.Color.Black;
			solidColorBrush2.Color = System.Windows.Media.Color.FromArgb(((byte)(255)), ((byte)(0)), ((byte)(0)), ((byte)(0)));
			this.editPad1.Foreground = solidColorBrush2;
			this.editPad1.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
			solidColorBrush3.Color = System.Windows.Media.Color.FromArgb(((byte)(255)), ((byte)(0)), ((byte)(0)), ((byte)(0)));
			this.editPad1.LineNumbersForeground = solidColorBrush3;
			this.editPad1.Location = new System.Drawing.Point(0, 0);
			this.editPad1.Name = "editPad1";
			this.editPad1.Offset = 0;
			this.editPad1.Options = ((ICSharpCode.AvalonEdit.TextEditorOptions)(resources.GetObject("editPad1.Options")));
			this.editPad1.SelectedText = "";
			this.editPad1.SelectionLength = 0;
			this.editPad1.SelectionStart = 0;
			this.editPad1.ShowColumnRuler = false;
			this.editPad1.ShowEndOfLine = true;
			this.editPad1.ShowLineNumbers = true;
			this.editPad1.ShowSpaces = false;
			this.editPad1.ShowTabs = false;
			this.editPad1.Size = new System.Drawing.Size(599, 462);
			this.editPad1.TabIndex = 0;
			this.editPad1.WordWrap = false;
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnScriptEdit,
            this.toolStripSeparator2,
            this.btnExec,
            this.toolStripSeparator3,
            this.btnEditEnd,
            this.cmbWord,
            this.lbControl,
            this.toolStripSeparator1,
            this.cmbScript,
            this.btnHide});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(853, 25);
			this.toolStrip.TabIndex = 1;
			this.toolStrip.Text = "toolStrip1";
			// 
			// btnScriptEdit
			// 
			this.btnScriptEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnScriptEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnScriptEdit.Image")));
			this.btnScriptEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnScriptEdit.Name = "btnScriptEdit";
			this.btnScriptEdit.Size = new System.Drawing.Size(61, 22);
			this.btnScriptEdit.Text = "ScriptEdit";
			this.btnScriptEdit.Click += new System.EventHandler(this.BtnScriptEdit_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// btnExec
			// 
			this.btnExec.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnExec.Enabled = false;
			this.btnExec.Image = ((System.Drawing.Image)(resources.GetObject("btnExec.Image")));
			this.btnExec.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnExec.Name = "btnExec";
			this.btnExec.Size = new System.Drawing.Size(52, 22);
			this.btnExec.Text = "Execute";
			this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// btnEditEnd
			// 
			this.btnEditEnd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnEditEnd.Enabled = false;
			this.btnEditEnd.Image = ((System.Drawing.Image)(resources.GetObject("btnEditEnd.Image")));
			this.btnEditEnd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnEditEnd.Name = "btnEditEnd";
			this.btnEditEnd.Size = new System.Drawing.Size(51, 22);
			this.btnEditEnd.Text = "EditEnd";
			this.btnEditEnd.Click += new System.EventHandler(this.BtnEditEnd_Click);
			// 
			// cmbWord
			// 
			this.cmbWord.DropDownHeight = 300;
			this.cmbWord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbWord.DropDownWidth = 350;
			this.cmbWord.IntegralHeight = false;
			this.cmbWord.Items.AddRange(new object[] {
            "app.exit();",
            "app.alert(str)",
            "app.write(str)",
            "app.writeln(str)",
            "app.cls();",
            "app.loadForm(path)",
            "app.openForm(path)",
            "app.executablePath",
            "app.currentPath",
            "app.hypfFolder",
            "app.homeHypf",
            "app.homeFolder",
            "app.appPath",
            "app.appFolder",
            "app.loadHome();",
            "app.openHome();",
            "app.yesnoDialog(cap,title)",
            "app.getenv(str)",
            "app.setenv(str,str)",
            "var Directory = dotnet.System.IO.Directory;",
            "var File = dotnet.System.IO.File;",
            "value"});
			this.cmbWord.Name = "cmbWord";
			this.cmbWord.Size = new System.Drawing.Size(121, 25);
			// 
			// lbControl
			// 
			this.lbControl.Name = "lbControl";
			this.lbControl.Size = new System.Drawing.Size(46, 22);
			this.lbControl.Text = "Control";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// cmbScript
			// 
			this.cmbScript.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbScript.Enabled = false;
			this.cmbScript.Name = "cmbScript";
			this.cmbScript.Size = new System.Drawing.Size(180, 25);
			// 
			// btnHide
			// 
			this.btnHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnHide.Image = ((System.Drawing.Image)(resources.GetObject("btnHide.Image")));
			this.btnHide.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnHide.Name = "btnHide";
			this.btnHide.Size = new System.Drawing.Size(36, 22);
			this.btnHide.Text = "Hide";
			this.btnHide.Click += new System.EventHandler(this.MenuHide_Click);
			// 
			// controlBrowser1
			// 
			this.controlBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlBrowser1.EditPad = this.editPad1;
			this.controlBrowser1.Location = new System.Drawing.Point(0, 0);
			this.controlBrowser1.Name = "controlBrowser1";
			this.controlBrowser1.Size = new System.Drawing.Size(250, 462);
			this.controlBrowser1.SplitterDistance = 123;
			this.controlBrowser1.TabIndex = 2;
			this.controlBrowser1.Text = "controlBrowser1";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(0, 28);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.editPad1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.controlBrowser1);
			this.splitContainer1.Size = new System.Drawing.Size(853, 462);
			this.splitContainer1.SplitterDistance = 599;
			this.splitContainer1.TabIndex = 3;
			// 
			// ScriptEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.ClientSize = new System.Drawing.Size(853, 491);
			this.ControlBox = false;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.toolStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "ScriptEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "HyperScriptEditor";
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private EditPad editPad1;
		private ToolStrip toolStrip;
		private ToolStripButton btnHide;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripButton btnExec;
		private ToolStripComboBox cmbScript;
		private ControlBrowser controlBrowser1;
		private ToolStripSeparator toolStripSeparator3;
		private SplitContainer splitContainer1;
		private ToolStripButton btnEditEnd;
		private ToolStripLabel lbControl;
		private ToolStripButton btnScriptEdit;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripComboBox cmbWord;
	}
}