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
			this.menuControl = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnWrite = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuHide = new System.Windows.Forms.ToolStripButton();
			this.menuScript = new System.Windows.Forms.ToolStripDropDownButton();
			this.btnTopMost = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// editPad1
			// 
			this.editPad1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			solidColorBrush1.Color = System.Windows.Media.Color.FromArgb(((byte)(255)), ((byte)(221)), ((byte)(221)), ((byte)(221)));
			this.editPad1.Background = solidColorBrush1;
			this.editPad1.ConvertTabsToSpaces = true;
			this.editPad1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.editPad1.ForeColor = System.Drawing.Color.Black;
			solidColorBrush2.Color = System.Windows.Media.Color.FromArgb(((byte)(255)), ((byte)(0)), ((byte)(0)), ((byte)(0)));
			this.editPad1.Foreground = solidColorBrush2;
			this.editPad1.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
			solidColorBrush3.Color = System.Windows.Media.Color.FromArgb(((byte)(255)), ((byte)(0)), ((byte)(0)), ((byte)(0)));
			this.editPad1.LineNumbersForeground = solidColorBrush3;
			this.editPad1.Location = new System.Drawing.Point(0, 28);
			this.editPad1.Name = "editPad1";
			this.editPad1.Options = ((ICSharpCode.AvalonEdit.TextEditorOptions)(resources.GetObject("editPad1.Options")));
			this.editPad1.ShowColumnRuler = true;
			this.editPad1.ShowEndOfLine = true;
			this.editPad1.ShowLineNumbers = true;
			this.editPad1.ShowSpaces = false;
			this.editPad1.ShowTabs = true;
			this.editPad1.Size = new System.Drawing.Size(615, 531);
			this.editPad1.SyntaxHighlighting = null;
			this.editPad1.TabIndex = 0;
			this.editPad1.WordWrap = true;
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuControl,
            this.menuScript,
            this.toolStripSeparator2,
            this.btnWrite,
            this.toolStripSeparator3,
            this.btnTopMost,
            this.toolStripSeparator1,
            this.menuHide});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(615, 25);
			this.toolStrip.TabIndex = 1;
			this.toolStrip.Text = "toolStrip1";
			// 
			// menuControl
			// 
			this.menuControl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.menuControl.Image = ((System.Drawing.Image)(resources.GetObject("menuControl.Image")));
			this.menuControl.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.menuControl.Name = "menuControl";
			this.menuControl.Size = new System.Drawing.Size(59, 22);
			this.menuControl.Text = "Contorl";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// btnWrite
			// 
			this.btnWrite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnWrite.Image = ((System.Drawing.Image)(resources.GetObject("btnWrite.Image")));
			this.btnWrite.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnWrite.Name = "btnWrite";
			this.btnWrite.Size = new System.Drawing.Size(39, 22);
			this.btnWrite.Text = "Write";
			this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// menuHide
			// 
			this.menuHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.menuHide.Image = ((System.Drawing.Image)(resources.GetObject("menuHide.Image")));
			this.menuHide.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.menuHide.Name = "menuHide";
			this.menuHide.Size = new System.Drawing.Size(36, 22);
			this.menuHide.Text = "Hide";
			this.menuHide.Click += new System.EventHandler(this.MenuHide_Click);
			// 
			// menuScript
			// 
			this.menuScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.menuScript.Image = ((System.Drawing.Image)(resources.GetObject("menuScript.Image")));
			this.menuScript.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.menuScript.Name = "menuScript";
			this.menuScript.Size = new System.Drawing.Size(50, 22);
			this.menuScript.Text = "Script";
			// 
			// btnTopMost
			// 
			this.btnTopMost.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnTopMost.Image = ((System.Drawing.Image)(resources.GetObject("btnTopMost.Image")));
			this.btnTopMost.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnTopMost.Name = "btnTopMost";
			this.btnTopMost.Size = new System.Drawing.Size(57, 22);
			this.btnTopMost.Text = "TopMost";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// HyperScriptEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.ClientSize = new System.Drawing.Size(615, 562);
			this.ControlBox = false;
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.editPad1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "HyperScriptEditor";
			this.Text = "HyperScriptEditor";
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private EditPad editPad1;
		private ToolStrip toolStrip;
		private ToolStripButton menuHide;
		private ToolStripDropDownButton menuControl;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripButton btnWrite;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripDropDownButton menuScript;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripButton btnTopMost;
	}
}