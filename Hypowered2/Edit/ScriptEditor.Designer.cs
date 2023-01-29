namespace Hpd
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptEditor));
			this.roslynEdit1 = new Hpd.RoslynEdit();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.lbName = new System.Windows.Forms.ToolStripLabel();
			this.cmbTarget = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnFont = new System.Windows.Forms.ToolStripButton();
			this.btnV8Execute = new System.Windows.Forms.ToolStripButton();
			this.btnHide = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// roslynEdit1
			// 
			this.roslynEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.roslynEdit1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.roslynEdit1.Location = new System.Drawing.Point(0, 28);
			this.roslynEdit1.Name = "roslynEdit1";
			this.roslynEdit1.Size = new System.Drawing.Size(790, 345);
			this.roslynEdit1.TabIndex = 1;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.lbName,
            this.cmbTarget,
            this.btnHide,
            this.toolStripButton3,
            this.toolStripSeparator1,
            this.btnFont,
            this.toolStripSeparator2,
            this.btnV8Execute});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(790, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.AutoSize = false;
			this.toolStripButton1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(100, 22);
			this.toolStripButton1.Text = "EditScript";
			// 
			// lbName
			// 
			this.lbName.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lbName.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbName.Name = "lbName";
			this.lbName.Size = new System.Drawing.Size(44, 22);
			this.lbName.Text = "(None)";
			// 
			// cmbTarget
			// 
			this.cmbTarget.Name = "cmbTarget";
			this.cmbTarget.Size = new System.Drawing.Size(121, 25);
			// 
			// toolStripButton3
			// 
			this.toolStripButton3.BackColor = System.Drawing.SystemColors.ControlLight;
			this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
			this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new System.Drawing.Size(51, 22);
			this.toolStripButton3.Text = "EditEnd";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// btnFont
			// 
			this.btnFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnFont.Image = ((System.Drawing.Image)(resources.GetObject("btnFont.Image")));
			this.btnFont.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnFont.Name = "btnFont";
			this.btnFont.Size = new System.Drawing.Size(35, 22);
			this.btnFont.Text = "Font";
			// 
			// btnV8Execute
			// 
			this.btnV8Execute.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.btnV8Execute.AutoSize = false;
			this.btnV8Execute.BackColor = System.Drawing.SystemColors.ControlLight;
			this.btnV8Execute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnV8Execute.Image = ((System.Drawing.Image)(resources.GetObject("btnV8Execute.Image")));
			this.btnV8Execute.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnV8Execute.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
			this.btnV8Execute.Name = "btnV8Execute";
			this.btnV8Execute.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.btnV8Execute.Size = new System.Drawing.Size(100, 22);
			this.btnV8Execute.Text = "Execute";
			this.btnV8Execute.Click += new System.EventHandler(this.btnV8Execute_Click);
			// 
			// btnHide
			// 
			this.btnHide.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.btnHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnHide.Image = ((System.Drawing.Image)(resources.GetObject("btnHide.Image")));
			this.btnHide.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnHide.Name = "btnHide";
			this.btnHide.Size = new System.Drawing.Size(36, 22);
			this.btnHide.Text = "Hide";
			this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// ScriptEditor
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(790, 373);
			this.ControlBox = false;
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.roslynEdit1);
			this.MaximizeBox = false;
			this.MdiChildrenMinimizedAnchorBottom = false;
			this.MinimizeBox = false;
			this.Name = "ScriptEditor";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "ScriptEditor";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private RoslynEdit roslynEdit1;
		private ToolStrip toolStrip1;
		private ToolStripButton toolStripButton1;
		private ToolStripComboBox cmbTarget;
		private ToolStripLabel lbName;
		private ToolStripButton btnFont;
		private ToolStripButton toolStripButton3;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripButton btnV8Execute;
		private ToolStripButton btnHide;
		private ToolStripSeparator toolStripSeparator2;
	}
}