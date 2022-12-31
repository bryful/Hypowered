namespace Hypowered
{
	partial class HyperControlList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HyperControlList));
			this.controlListBox1 = new Hypowered.ControlListBox();
			this.btnDel = new System.Windows.Forms.Button();
			this.btnUp = new System.Windows.Forms.Button();
			this.btnDown = new System.Windows.Forms.Button();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.menuForm = new System.Windows.Forms.ToolStripDropDownButton();
			this.btnHide = new System.Windows.Forms.ToolStripButton();
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
			this.controlListBox1.ItemHeight = 15;
			this.controlListBox1.Location = new System.Drawing.Point(12, 60);
			this.controlListBox1.Name = "controlListBox1";
			this.controlListBox1.Size = new System.Drawing.Size(194, 422);
			this.controlListBox1.TabIndex = 0;
			// 
			// btnDel
			// 
			this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDel.Location = new System.Drawing.Point(12, 31);
			this.btnDel.Name = "btnDel";
			this.btnDel.Size = new System.Drawing.Size(48, 23);
			this.btnDel.TabIndex = 2;
			this.btnDel.Text = "Del";
			this.btnDel.UseVisualStyleBackColor = true;
			// 
			// btnUp
			// 
			this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnUp.Location = new System.Drawing.Point(104, 31);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(48, 23);
			this.btnUp.TabIndex = 3;
			this.btnUp.Text = "Up";
			this.btnUp.UseVisualStyleBackColor = true;
			// 
			// btnDown
			// 
			this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDown.Location = new System.Drawing.Point(158, 31);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(48, 23);
			this.btnDown.TabIndex = 4;
			this.btnDown.Text = "Down";
			this.btnDown.UseVisualStyleBackColor = true;
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton1.ForeColor = System.Drawing.Color.Black;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(44, 22);
			this.toolStripButton1.Text = "Active";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuForm,
            this.toolStripButton1,
            this.btnHide});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(218, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
			// 
			// menuForm
			// 
			this.menuForm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.menuForm.ForeColor = System.Drawing.Color.Black;
			this.menuForm.Image = ((System.Drawing.Image)(resources.GetObject("menuForm.Image")));
			this.menuForm.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.menuForm.Name = "menuForm";
			this.menuForm.Size = new System.Drawing.Size(47, 22);
			this.menuForm.Text = "Form";
			this.menuForm.Click += new System.EventHandler(this.menuForm_Click);
			// 
			// btnHide
			// 
			this.btnHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnHide.ForeColor = System.Drawing.Color.Black;
			this.btnHide.Image = ((System.Drawing.Image)(resources.GetObject("btnHide.Image")));
			this.btnHide.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnHide.Name = "btnHide";
			this.btnHide.Size = new System.Drawing.Size(36, 22);
			this.btnHide.Text = "Hide";
			this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
			// 
			// HyperControlList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.ClientSize = new System.Drawing.Size(218, 493);
			this.ControlBox = false;
			this.Controls.Add(this.btnDown);
			this.Controls.Add(this.btnUp);
			this.Controls.Add(this.btnDel);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.controlListBox1);
			this.ForeColor = System.Drawing.Color.Gainsboro;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "HyperControlList";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "EditControlList";
			this.TopMost = true;
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ControlListBox controlListBox1;
		private Button btnDel;
		private Button btnUp;
		private Button btnDown;
		private ToolStripButton toolStripButton1;
		private ToolStrip toolStrip1;
		private ToolStripDropDownButton menuForm;
		private ToolStripButton btnHide;
	}
}