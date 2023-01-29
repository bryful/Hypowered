namespace Hpd
{
	partial class ConsoleForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsoleForm));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnClear = new System.Windows.Forms.ToolStripButton();
			this.btnFont = new System.Windows.Forms.ToolStripButton();
			this.btnHide = new System.Windows.Forms.ToolStripButton();
			this.tbOutput = new System.Windows.Forms.TextBox();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.btnFont,
            this.btnHide});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(809, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnClear
			// 
			this.btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
			this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(37, 22);
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
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
			// tbOutput
			// 
			this.tbOutput.BackColor = System.Drawing.Color.Black;
			this.tbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbOutput.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.tbOutput.ForeColor = System.Drawing.Color.Gainsboro;
			this.tbOutput.Location = new System.Drawing.Point(0, 25);
			this.tbOutput.Margin = new System.Windows.Forms.Padding(5);
			this.tbOutput.MaxLength = 65536;
			this.tbOutput.Multiline = true;
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbOutput.Size = new System.Drawing.Size(809, 221);
			this.tbOutput.TabIndex = 1;
			this.tbOutput.Text = "123456";
			// 
			// ConsoleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(809, 246);
			this.ControlBox = false;
			this.Controls.Add(this.tbOutput);
			this.Controls.Add(this.toolStrip1);
			this.MaximizeBox = false;
			this.MdiChildrenMinimizedAnchorBottom = false;
			this.MinimizeBox = false;
			this.Name = "ConsoleForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "ConsoleForm";
			this.TopMost = true;
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ToolStrip toolStrip1;
		private TextBox tbOutput;
		private ToolStripButton btnClear;
		private ToolStripButton btnFont;
		private ToolStripButton btnHide;
	}
}