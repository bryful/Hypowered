namespace Hypowered
{
	partial class ScriptEditForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptEditForm));
			this.editPad1 = new Hypowered.EditPad();
			this.controlBrowser1 = new Hypowered.ControlBrowser();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btnScriptEdit1 = new System.Windows.Forms.Button();
			this.btnExec1 = new System.Windows.Forms.Button();
			this.btnEditEnd1 = new System.Windows.Forms.Button();
			this.cmbScript1 = new System.Windows.Forms.ComboBox();
			this.cmbWord1 = new System.Windows.Forms.ComboBox();
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
			this.editPad1.Size = new System.Drawing.Size(537, 412);
			this.editPad1.TabIndex = 0;
			this.editPad1.WordWrap = false;
			// 
			// controlBrowser1
			// 
			this.controlBrowser1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.controlBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlBrowser1.EditPad = this.editPad1;
			this.controlBrowser1.ForeColor = System.Drawing.Color.Gainsboro;
			this.controlBrowser1.Location = new System.Drawing.Point(0, 0);
			this.controlBrowser1.Name = "controlBrowser1";
			this.controlBrowser1.Size = new System.Drawing.Size(252, 412);
			this.controlBrowser1.SplitterDistance = 124;
			this.controlBrowser1.TabIndex = 2;
			this.controlBrowser1.Text = "controlBrowser1";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(12, 56);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.editPad1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.controlBrowser1);
			this.splitContainer1.Size = new System.Drawing.Size(793, 412);
			this.splitContainer1.SplitterDistance = 537;
			this.splitContainer1.TabIndex = 3;
			// 
			// btnScriptEdit1
			// 
			this.btnScriptEdit1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnScriptEdit1.Location = new System.Drawing.Point(12, 30);
			this.btnScriptEdit1.Name = "btnScriptEdit1";
			this.btnScriptEdit1.Size = new System.Drawing.Size(75, 23);
			this.btnScriptEdit1.TabIndex = 4;
			this.btnScriptEdit1.Text = "StartEdit";
			this.btnScriptEdit1.UseVisualStyleBackColor = true;
			this.btnScriptEdit1.Click += new System.EventHandler(this.BtnScriptEdit_Click);
			// 
			// btnExec1
			// 
			this.btnExec1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnExec1.Location = new System.Drawing.Point(245, 30);
			this.btnExec1.Name = "btnExec1";
			this.btnExec1.Size = new System.Drawing.Size(75, 23);
			this.btnExec1.TabIndex = 5;
			this.btnExec1.Text = "Execute";
			this.btnExec1.UseVisualStyleBackColor = true;
			this.btnExec1.Click += new System.EventHandler(this.btnExec_Click);
			// 
			// btnEditEnd1
			// 
			this.btnEditEnd1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnEditEnd1.Location = new System.Drawing.Point(474, 29);
			this.btnEditEnd1.Name = "btnEditEnd1";
			this.btnEditEnd1.Size = new System.Drawing.Size(75, 23);
			this.btnEditEnd1.TabIndex = 6;
			this.btnEditEnd1.Text = "EndEdit";
			this.btnEditEnd1.UseVisualStyleBackColor = true;
			this.btnEditEnd1.Click += new System.EventHandler(this.BtnEditEnd_Click);
			// 
			// cmbScript1
			// 
			this.cmbScript1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.cmbScript1.DropDownHeight = 250;
			this.cmbScript1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbScript1.DropDownWidth = 200;
			this.cmbScript1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmbScript1.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmbScript1.FormattingEnabled = true;
			this.cmbScript1.IntegralHeight = false;
			this.cmbScript1.Location = new System.Drawing.Point(93, 31);
			this.cmbScript1.Name = "cmbScript1";
			this.cmbScript1.Size = new System.Drawing.Size(146, 23);
			this.cmbScript1.TabIndex = 8;
			this.cmbScript1.SizeChanged += new System.EventHandler(this.CmbScript_SelectedIndexChanged);
			// 
			// cmbWord1
			// 
			this.cmbWord1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.cmbWord1.DropDownHeight = 500;
			this.cmbWord1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbWord1.DropDownWidth = 400;
			this.cmbWord1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmbWord1.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmbWord1.FormattingEnabled = true;
			this.cmbWord1.IntegralHeight = false;
			this.cmbWord1.Location = new System.Drawing.Point(326, 30);
			this.cmbWord1.Name = "cmbWord1";
			this.cmbWord1.Size = new System.Drawing.Size(142, 23);
			this.cmbWord1.TabIndex = 9;
			// 
			// ScriptEditForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.CanResize = true;
			this.ClientSize = new System.Drawing.Size(817, 480);
			this.ControlBox = false;
			this.Controls.Add(this.cmbWord1);
			this.Controls.Add(this.cmbScript1);
			this.Controls.Add(this.btnEditEnd1);
			this.Controls.Add(this.btnExec1);
			this.Controls.Add(this.btnScriptEdit1);
			this.Controls.Add(this.splitContainer1);
			this.Name = "ScriptEditForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "HyperScriptEditForm";
			this.Controls.SetChildIndex(this.splitContainer1, 0);
			this.Controls.SetChildIndex(this.btnScriptEdit1, 0);
			this.Controls.SetChildIndex(this.btnExec1, 0);
			this.Controls.SetChildIndex(this.btnEditEnd1, 0);
			this.Controls.SetChildIndex(this.cmbScript1, 0);
			this.Controls.SetChildIndex(this.cmbWord1, 0);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private EditPad editPad1;
		private ControlBrowser controlBrowser1;
		private SplitContainer splitContainer1;
		private Button btnScriptEdit1;
		private Button btnExec1;
		private Button btnEditEnd1;
		private ComboBox cmbScript1;
		private ComboBox cmbWord1;
	}
}