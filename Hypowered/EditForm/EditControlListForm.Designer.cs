namespace Hypowered
{
	partial class EditControlListForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditControlListForm));
			this.controlListBox1 = new Hypowered.ControlListBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.btnHide = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// controlListBox1
			// 
			this.controlListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.controlListBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
			this.controlListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.controlListBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.controlListBox1.FormattingEnabled = true;
			this.controlListBox1.HyperForm = null;
			this.controlListBox1.ItemHeight = 15;
			this.controlListBox1.Location = new System.Drawing.Point(0, 30);
			this.controlListBox1.Name = "controlListBox1";
			this.controlListBox1.Size = new System.Drawing.Size(248, 377);
			this.controlListBox1.TabIndex = 0;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.btnHide});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(248, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(74, 22);
			this.toolStripButton1.Text = "Form Active";
			// 
			// btnHide
			// 
			this.btnHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnHide.Image = ((System.Drawing.Image)(resources.GetObject("btnHide.Image")));
			this.btnHide.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnHide.Name = "btnHide";
			this.btnHide.Size = new System.Drawing.Size(36, 22);
			this.btnHide.Text = "Hide";
			this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// EditControlListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.ClientSize = new System.Drawing.Size(248, 409);
			this.ControlBox = false;
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.controlListBox1);
			this.ForeColor = System.Drawing.Color.Gainsboro;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "EditControlListForm";
			this.Text = "EditControlList";
			this.TopMost = true;
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ControlListBox controlListBox1;
		private ToolStrip toolStrip1;
		private ToolStripButton toolStripButton1;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripButton btnHide;
	}
}