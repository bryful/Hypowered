namespace Hypowered
{
	partial class JSInputForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JSInputForm));
			this.controlBrowser1 = new Hypowered.ControlBrowser();
			this.editPad1 = new Hypowered.EditPad();
			this.btnExec = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.cmbGlobal = new System.Windows.Forms.ComboBox();
			this.btnFont = new System.Windows.Forms.Button();
			this.btnCLS = new System.Windows.Forms.Button();
			this.cmbWord = new System.Windows.Forms.ComboBox();
			this.btnAlert = new System.Windows.Forms.Button();
			this.btnWriteln = new System.Windows.Forms.Button();
			this.btnUndo = new System.Windows.Forms.Button();
			this.btnRedo = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// controlBrowser1
			// 
			this.controlBrowser1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.controlBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlBrowser1.EditPad = this.editPad1;
			this.controlBrowser1.ForeColor = System.Drawing.Color.Silver;
			this.controlBrowser1.Location = new System.Drawing.Point(0, 0);
			this.controlBrowser1.Name = "controlBrowser1";
			this.controlBrowser1.Size = new System.Drawing.Size(262, 316);
			this.controlBrowser1.SplitterDistance = 129;
			this.controlBrowser1.TabIndex = 1;
			this.controlBrowser1.Text = "controlBrowser1";
			// 
			// editPad1
			// 
			this.editPad1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			solidColorBrush1.Color = System.Windows.Media.Color.FromArgb(((byte)(255)), ((byte)(221)), ((byte)(221)), ((byte)(221)));
			this.editPad1.Background = solidColorBrush1;
			this.editPad1.ConvertTabsToSpaces = false;
			textDocument1.FileName = null;
			textDocument1.ServiceProvider = serviceContainer1;
			textDocument1.Text = "";
			undoStack1.SizeLimit = 2147483647;
			textDocument1.UndoStack = undoStack1;
			this.editPad1.Document = textDocument1;
			this.editPad1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			solidColorBrush2.Color = System.Windows.Media.Color.FromArgb(((byte)(255)), ((byte)(0)), ((byte)(0)), ((byte)(0)));
			this.editPad1.Foreground = solidColorBrush2;
			this.editPad1.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
			solidColorBrush3.Color = System.Windows.Media.Color.FromArgb(((byte)(255)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
			this.editPad1.LineNumbersForeground = solidColorBrush3;
			this.editPad1.Location = new System.Drawing.Point(3, 34);
			this.editPad1.Name = "editPad1";
			this.editPad1.Offset = 0;
			this.editPad1.Options = ((ICSharpCode.AvalonEdit.TextEditorOptions)(resources.GetObject("editPad1.Options")));
			this.editPad1.SelectedText = "";
			this.editPad1.SelectionLength = 0;
			this.editPad1.SelectionStart = 0;
			this.editPad1.ShowColumnRuler = true;
			this.editPad1.ShowEndOfLine = true;
			this.editPad1.ShowLineNumbers = true;
			this.editPad1.ShowSpaces = false;
			this.editPad1.ShowTabs = true;
			this.editPad1.Size = new System.Drawing.Size(792, 229);
			this.editPad1.TabIndex = 5;
			this.editPad1.WordWrap = true;
			// 
			// btnExec
			// 
			this.btnExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnExec.Location = new System.Drawing.Point(618, 269);
			this.btnExec.Name = "btnExec";
			this.btnExec.Size = new System.Drawing.Size(165, 34);
			this.btnExec.TabIndex = 3;
			this.btnExec.Text = "execute";
			this.btnExec.UseVisualStyleBackColor = true;
			this.btnExec.Click += new System.EventHandler(this.Button1_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(12, 31);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.cmbGlobal);
			this.splitContainer1.Panel1.Controls.Add(this.btnFont);
			this.splitContainer1.Panel1.Controls.Add(this.btnCLS);
			this.splitContainer1.Panel1.Controls.Add(this.cmbWord);
			this.splitContainer1.Panel1.Controls.Add(this.btnAlert);
			this.splitContainer1.Panel1.Controls.Add(this.btnWriteln);
			this.splitContainer1.Panel1.Controls.Add(this.btnUndo);
			this.splitContainer1.Panel1.Controls.Add(this.btnRedo);
			this.splitContainer1.Panel1.Controls.Add(this.editPad1);
			this.splitContainer1.Panel1.Controls.Add(this.btnExec);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.controlBrowser1);
			this.splitContainer1.Size = new System.Drawing.Size(1064, 316);
			this.splitContainer1.SplitterDistance = 798;
			this.splitContainer1.TabIndex = 6;
			// 
			// cmbGlobal
			// 
			this.cmbGlobal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.cmbGlobal.DropDownHeight = 300;
			this.cmbGlobal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbGlobal.DropDownWidth = 350;
			this.cmbGlobal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmbGlobal.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmbGlobal.FormattingEnabled = true;
			this.cmbGlobal.IntegralHeight = false;
			this.cmbGlobal.Location = new System.Drawing.Point(357, 7);
			this.cmbGlobal.Name = "cmbGlobal";
			this.cmbGlobal.Size = new System.Drawing.Size(121, 23);
			this.cmbGlobal.TabIndex = 13;
			this.cmbGlobal.SelectedIndexChanged += new System.EventHandler(this.cmbGlobal_SelectedIndexChanged);
			this.cmbGlobal.Click += new System.EventHandler(this.cmbGlobal_Click);
			// 
			// btnFont
			// 
			this.btnFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFont.Location = new System.Drawing.Point(11, 5);
			this.btnFont.Name = "btnFont";
			this.btnFont.Size = new System.Drawing.Size(62, 25);
			this.btnFont.TabIndex = 12;
			this.btnFont.Text = "Font";
			this.btnFont.UseVisualStyleBackColor = true;
			this.btnFont.Click += new System.EventHandler(this.BtnFont_Click);
			// 
			// btnCLS
			// 
			this.btnCLS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCLS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCLS.Location = new System.Drawing.Point(3, 269);
			this.btnCLS.Name = "btnCLS";
			this.btnCLS.Size = new System.Drawing.Size(53, 34);
			this.btnCLS.TabIndex = 11;
			this.btnCLS.Text = "cls";
			this.btnCLS.UseVisualStyleBackColor = true;
			// 
			// cmbWord
			// 
			this.cmbWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.cmbWord.DropDownHeight = 300;
			this.cmbWord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbWord.DropDownWidth = 350;
			this.cmbWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmbWord.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmbWord.FormattingEnabled = true;
			this.cmbWord.IntegralHeight = false;
			this.cmbWord.Location = new System.Drawing.Point(230, 7);
			this.cmbWord.Name = "cmbWord";
			this.cmbWord.Size = new System.Drawing.Size(121, 23);
			this.cmbWord.TabIndex = 10;
			// 
			// btnAlert
			// 
			this.btnAlert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAlert.Location = new System.Drawing.Point(154, 5);
			this.btnAlert.Name = "btnAlert";
			this.btnAlert.Size = new System.Drawing.Size(70, 25);
			this.btnAlert.TabIndex = 9;
			this.btnAlert.Text = "alert";
			this.btnAlert.UseVisualStyleBackColor = true;
			// 
			// btnWriteln
			// 
			this.btnWriteln.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnWriteln.Location = new System.Drawing.Point(78, 5);
			this.btnWriteln.Name = "btnWriteln";
			this.btnWriteln.Size = new System.Drawing.Size(70, 25);
			this.btnWriteln.TabIndex = 8;
			this.btnWriteln.Text = "writeln";
			this.btnWriteln.UseVisualStyleBackColor = true;
			// 
			// btnUndo
			// 
			this.btnUndo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnUndo.Location = new System.Drawing.Point(121, 269);
			this.btnUndo.Name = "btnUndo";
			this.btnUndo.Size = new System.Drawing.Size(61, 34);
			this.btnUndo.TabIndex = 7;
			this.btnUndo.Text = "Undo";
			this.btnUndo.UseVisualStyleBackColor = true;
			this.btnUndo.Click += new System.EventHandler(this.BtnUndo_Click);
			// 
			// btnRedo
			// 
			this.btnRedo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRedo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRedo.Location = new System.Drawing.Point(62, 269);
			this.btnRedo.Name = "btnRedo";
			this.btnRedo.Size = new System.Drawing.Size(53, 34);
			this.btnRedo.TabIndex = 6;
			this.btnRedo.Text = "Redo";
			this.btnRedo.UseVisualStyleBackColor = true;
			this.btnRedo.Click += new System.EventHandler(this.BtnRedo_Click);
			// 
			// JSInputForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.CanResize = true;
			this.ClientSize = new System.Drawing.Size(1088, 359);
			this.Controls.Add(this.splitContainer1);
			this.ForeColor = System.Drawing.Color.LightGray;
			this.KeyPreview = true;
			this.MenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.MenuForeColor = System.Drawing.Color.DarkGray;
			this.Name = "JSInputForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "JSInputFormcs";
			this.Controls.SetChildIndex(this.splitContainer1, 0);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private ControlBrowser controlBrowser1;
		private Button btnExec;
		private EditPad editPad1;
		private SplitContainer splitContainer1;
		private Button btnUndo;
		private Button btnRedo;
		private Button btnWriteln;
		private ComboBox cmbWord;
		private Button btnAlert;
		private Button btnCLS;
		private Button btnFont;
		private ComboBox cmbGlobal;
	}
}