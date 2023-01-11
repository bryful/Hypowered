﻿namespace Hypowered
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
			this.controlBrowser1.Size = new System.Drawing.Size(208, 253);
			this.controlBrowser1.SplitterDistance = 102;
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
			this.editPad1.Location = new System.Drawing.Point(3, 3);
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
			this.editPad1.Size = new System.Drawing.Size(635, 197);
			this.editPad1.TabIndex = 5;
			this.editPad1.WordWrap = true;
			// 
			// btnExec
			// 
			this.btnExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnExec.Location = new System.Drawing.Point(488, 206);
			this.btnExec.Name = "btnExec";
			this.btnExec.Size = new System.Drawing.Size(138, 34);
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
			this.splitContainer1.Panel1.Controls.Add(this.btnUndo);
			this.splitContainer1.Panel1.Controls.Add(this.btnRedo);
			this.splitContainer1.Panel1.Controls.Add(this.editPad1);
			this.splitContainer1.Panel1.Controls.Add(this.btnExec);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.controlBrowser1);
			this.splitContainer1.Size = new System.Drawing.Size(853, 253);
			this.splitContainer1.SplitterDistance = 641;
			this.splitContainer1.TabIndex = 6;
			// 
			// btnUndo
			// 
			this.btnUndo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnUndo.Location = new System.Drawing.Point(130, 206);
			this.btnUndo.Name = "btnUndo";
			this.btnUndo.Size = new System.Drawing.Size(72, 34);
			this.btnUndo.TabIndex = 7;
			this.btnUndo.Text = "Undo";
			this.btnUndo.UseVisualStyleBackColor = true;
			this.btnUndo.Click += new System.EventHandler(this.BtnUndo_Click);
			// 
			// btnRedo
			// 
			this.btnRedo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRedo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRedo.Location = new System.Drawing.Point(52, 206);
			this.btnRedo.Name = "btnRedo";
			this.btnRedo.Size = new System.Drawing.Size(72, 34);
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
			this.ClientSize = new System.Drawing.Size(877, 296);
			this.Controls.Add(this.splitContainer1);
			this.DoubleBuffered = true;
			this.ForeColor = System.Drawing.Color.LightGray;
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
	}
}