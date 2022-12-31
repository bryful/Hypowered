namespace Hypowered
{
	partial class HyperScriptEditor
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
			System.Windows.Media.SolidColorBrush solidColorBrush2 = new System.Windows.Media.SolidColorBrush();
			System.Windows.Media.SolidColorBrush solidColorBrush3 = new System.Windows.Media.SolidColorBrush();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HyperScriptEditor));
			this.editPad1 = new Hypowered.EditPad();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnOK = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.btnCancel = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.cmbScript = new System.Windows.Forms.ToolStripComboBox();
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
			this.editPad1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.editPad1.ForeColor = System.Drawing.Color.Black;
			solidColorBrush2.Color = System.Windows.Media.Color.FromArgb(((byte)(255)), ((byte)(0)), ((byte)(0)), ((byte)(0)));
			this.editPad1.Foreground = solidColorBrush2;
			this.editPad1.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
			solidColorBrush3.Color = System.Windows.Media.Color.FromArgb(((byte)(255)), ((byte)(0)), ((byte)(0)), ((byte)(0)));
			this.editPad1.LineNumbersForeground = solidColorBrush3;
			this.editPad1.Location = new System.Drawing.Point(0, 0);
			this.editPad1.Name = "editPad1";
			this.editPad1.Options = ((ICSharpCode.AvalonEdit.TextEditorOptions)(resources.GetObject("editPad1.Options")));
			this.editPad1.ShowColumnRuler = true;
			this.editPad1.ShowEndOfLine = true;
			this.editPad1.ShowLineNumbers = true;
			this.editPad1.ShowSpaces = false;
			this.editPad1.ShowTabs = true;
			this.editPad1.Size = new System.Drawing.Size(550, 533);
			this.editPad1.SyntaxHighlighting = null;
			this.editPad1.TabIndex = 0;
			this.editPad1.WordWrap = true;
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.btnOK,
            this.toolStripSeparator3,
            this.btnCancel,
            this.toolStripSeparator1,
            this.cmbScript});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.toolStrip.Size = new System.Drawing.Size(779, 25);
			this.toolStrip.TabIndex = 1;
			this.toolStrip.Text = "toolStrip1";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// btnOK
			// 
			this.btnOK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
			this.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(27, 22);
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click_1);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// btnCancel
			// 
			this.btnCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(46, 22);
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.MenuHide_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// cmbScript
			// 
			this.cmbScript.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbScript.Name = "cmbScript";
			this.cmbScript.Size = new System.Drawing.Size(180, 25);
			// 
			// controlBrowser1
			// 
			this.controlBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlBrowser1.Location = new System.Drawing.Point(0, 0);
			this.controlBrowser1.Name = "controlBrowser1";
			this.controlBrowser1.Size = new System.Drawing.Size(225, 533);
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
			this.splitContainer1.Size = new System.Drawing.Size(779, 533);
			this.splitContainer1.SplitterDistance = 550;
			this.splitContainer1.TabIndex = 3;
			// 
			// HyperScriptEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.ClientSize = new System.Drawing.Size(779, 562);
			this.ControlBox = false;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.toolStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "HyperScriptEditor";
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
		private ToolStripButton btnCancel;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripButton btnOK;
		private ToolStripComboBox cmbScript;
		private ControlBrowser controlBrowser1;
		private ToolStripSeparator toolStripSeparator3;
		private SplitContainer splitContainer1;
	}
}