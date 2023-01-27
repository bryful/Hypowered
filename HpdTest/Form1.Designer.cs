using Hpd;
namespace HpdTest
{
    partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hpdButton1 = new Hpd.HpdButton();
			this.hpdComboBox1 = new Hpd.HpdComboBox();
			this.hpdPanel1 = new Hpd.HpdPanel();
			this.hpdListBox1 = new Hpd.HpdListBox();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
			this.menuStrip1.Size = new System.Drawing.Size(614, 24);
			this.menuStrip1.TabIndex = 12;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cCToolStripMenuItem,
            this.quitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 24);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// cCToolStripMenuItem
			// 
			this.cCToolStripMenuItem.Name = "cCToolStripMenuItem";
			this.cCToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
			this.cCToolStripMenuItem.Text = "CC";
			this.cCToolStripMenuItem.Click += new System.EventHandler(this.cCToolStripMenuItem_Click);
			// 
			// quitToolStripMenuItem
			// 
			this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			this.quitToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
			this.quitToolStripMenuItem.Text = "Quit";
			// 
			// hpdButton1
			// 
			this.hpdButton1.AsComboBox = null;
			this.hpdButton1.AsListBox = null;
			this.hpdButton1.AsTextBox = null;
			this.hpdButton1.BaseSize = new System.Drawing.Size(79, 25);
			this.hpdButton1.Caption = "caption";
			this.hpdButton1.CaptionWidth = 0;
			this.hpdButton1.DialogResult = System.Windows.Forms.DialogResult.None;
			this.hpdButton1.FileName = "";
			this.hpdButton1.IsDrawFrame = false;
			this.hpdButton1.IsSaveFileName = false;
			this.hpdButton1.Lines = new string[] {
        "hpdButton1"};
			this.hpdButton1.Location = new System.Drawing.Point(3, 337);
			this.hpdButton1.Multiline = true;
			this.hpdButton1.Name = "hpdButton1";
			this.hpdButton1.SelectedIndex = -1;
			this.hpdButton1.Size = new System.Drawing.Size(608, 25);
			this.hpdButton1.SizePolicyHorizon = Hpd.SizePolicy.Expanding;
			this.hpdButton1.SizePolicyVertual = Hpd.SizePolicy.Fixed;
			this.hpdButton1.TabIndex = 13;
			this.hpdButton1.TabStop = false;
			this.hpdButton1.Text = "hpdButton1";
			this.hpdButton1.TextAligiment = System.Drawing.StringAlignment.Near;
			this.hpdButton1.TextLineAligiment = System.Drawing.StringAlignment.Near;
			// 
			// hpdComboBox1
			// 
			this.hpdComboBox1.AsButton = null;
			this.hpdComboBox1.AsListBox = null;
			this.hpdComboBox1.AsTextBox = null;
			this.hpdComboBox1.BaseSize = new System.Drawing.Size(75, 23);
			this.hpdComboBox1.Caption = "caption";
			this.hpdComboBox1.CaptionWidth = 0;
			this.hpdComboBox1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.hpdComboBox1.FileName = "";
			this.hpdComboBox1.IsDrawFrame = false;
			this.hpdComboBox1.IsSaveFileName = false;
			this.hpdComboBox1.Lines = new string[] {
        ""};
			this.hpdComboBox1.Location = new System.Drawing.Point(3, 308);
			this.hpdComboBox1.Multiline = true;
			this.hpdComboBox1.Name = "hpdComboBox1";
			this.hpdComboBox1.SelectedIndex = -1;
			this.hpdComboBox1.Size = new System.Drawing.Size(608, 23);
			this.hpdComboBox1.SizePolicyHorizon = Hpd.SizePolicy.Expanding;
			this.hpdComboBox1.SizePolicyVertual = Hpd.SizePolicy.Fixed;
			this.hpdComboBox1.TabIndex = 14;
			this.hpdComboBox1.TabStop = false;
			this.hpdComboBox1.TextAligiment = System.Drawing.StringAlignment.Near;
			this.hpdComboBox1.TextLineAligiment = System.Drawing.StringAlignment.Near;
			// 
			// hpdPanel1
			// 
			this.hpdPanel1.AsButton = null;
			this.hpdPanel1.AsComboBox = null;
			this.hpdPanel1.AsListBox = null;
			this.hpdPanel1.AsTextBox = null;
			this.hpdPanel1.BaseSize = new System.Drawing.Size(0, 0);
			this.hpdPanel1.Caption = "caption";
			this.hpdPanel1.CaptionWidth = 0;
			this.hpdPanel1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.hpdPanel1.FileName = "";
			this.hpdPanel1.IsDrawFrame = false;
			this.hpdPanel1.IsSaveFileName = false;
			this.hpdPanel1.Lines = new string[] {
        "hpdPanel1"};
			this.hpdPanel1.Location = new System.Drawing.Point(3, 168);
			this.hpdPanel1.Multiline = true;
			this.hpdPanel1.Name = "hpdPanel1";
			this.hpdPanel1.Orientation = Hpd.HpdOrientation.Vertical;
			this.hpdPanel1.SelectedIndex = -1;
			this.hpdPanel1.Size = new System.Drawing.Size(608, 134);
			this.hpdPanel1.SizePolicyHorizon = Hpd.SizePolicy.Expanding;
			this.hpdPanel1.SizePolicyVertual = Hpd.SizePolicy.Expanding;
			this.hpdPanel1.TabIndex = 15;
			this.hpdPanel1.Text = "hpdPanel1";
			this.hpdPanel1.TextAligiment = System.Drawing.StringAlignment.Near;
			this.hpdPanel1.TextLineAligiment = System.Drawing.StringAlignment.Near;
			// 
			// hpdListBox1
			// 
			this.hpdListBox1.AsButton = null;
			this.hpdListBox1.AsComboBox = null;
			this.hpdListBox1.AsTextBox = null;
			this.hpdListBox1.BaseSize = new System.Drawing.Size(120, 96);
			this.hpdListBox1.Caption = "caption";
			this.hpdListBox1.CaptionWidth = 0;
			this.hpdListBox1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.hpdListBox1.FileName = "";
			this.hpdListBox1.IsDrawFrame = false;
			this.hpdListBox1.IsSaveFileName = false;
			this.hpdListBox1.Lines = new string[] {
        "hpdListBox1"};
			this.hpdListBox1.Location = new System.Drawing.Point(3, 27);
			this.hpdListBox1.Multiline = true;
			this.hpdListBox1.Name = "hpdListBox1";
			this.hpdListBox1.SelectedIndex = -1;
			this.hpdListBox1.Size = new System.Drawing.Size(608, 135);
			this.hpdListBox1.SizePolicyHorizon = Hpd.SizePolicy.Expanding;
			this.hpdListBox1.SizePolicyVertual = Hpd.SizePolicy.Expanding;
			this.hpdListBox1.TabIndex = 16;
			this.hpdListBox1.TabStop = false;
			this.hpdListBox1.Text = "hpdListBox1";
			this.hpdListBox1.TextAligiment = System.Drawing.StringAlignment.Near;
			this.hpdListBox1.TextLineAligiment = System.Drawing.StringAlignment.Near;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(614, 365);
			this.Controls.Add(this.hpdListBox1);
			this.Controls.Add(this.hpdPanel1);
			this.Controls.Add(this.hpdComboBox1);
			this.Controls.Add(this.hpdButton1);
			this.Controls.Add(this.menuStrip1);
			this.DoubleBuffered = true;
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(126, 192);
			this.Name = "Form1";
			this.Text = "Form1";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private MenuStrip menuStrip1;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem quitToolStripMenuItem;
		private ToolStripMenuItem cCToolStripMenuItem;
		private HpdButton hpdButton1;
		private HpdComboBox hpdComboBox1;
		private HpdPanel hpdPanel1;
		private HpdListBox hpdListBox1;
	}
}