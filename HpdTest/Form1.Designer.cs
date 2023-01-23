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
			Hpd.HpdScriptCode hpdScriptCode1 = new Hpd.HpdScriptCode();
			Hpd.HpdScriptCode hpdScriptCode2 = new Hpd.HpdScriptCode();
			Hpd.HpdScriptCode hpdScriptCode3 = new Hpd.HpdScriptCode();
			this.button3 = new System.Windows.Forms.Button();
			this.roslynPadControl1 = new BRY.RoslynPadControl();
			this.button1 = new System.Windows.Forms.Button();
			this.hpdButton1 = new Hpd.HpdButton();
			this.hpdButton2 = new Hpd.HpdButton();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hpdControlTree1 = new Hpd.HpdControlTree();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(21, 73);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(131, 45);
			this.button3.TabIndex = 7;
			this.button3.Text = "button3";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// roslynPadControl1
			// 
			this.roslynPadControl1.Location = new System.Drawing.Point(176, 26);
			this.roslynPadControl1.Name = "roslynPadControl1";
			this.roslynPadControl1.Size = new System.Drawing.Size(223, 306);
			this.roslynPadControl1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(66, 287);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 9;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// hpdButton1
			// 
			this.hpdButton1.CanColorCustum = false;
			this.hpdButton1.ControlName = "hpdButton1";
			this.hpdButton1.FileName = "";
			this.hpdButton1.ForcusColor = System.Drawing.Color.White;
			this.hpdButton1.FrameWeight = new System.Windows.Forms.Padding(1);
			this.hpdButton1.IsDrawFocuse = true;
			this.hpdButton1.IsDrawFrame = true;
			this.hpdButton1.IsSaveFileName = false;
			this.hpdButton1.Lines = new string[] {
        "hpdButton1"};
			this.hpdButton1.Location = new System.Drawing.Point(21, 124);
			this.hpdButton1.Name = "hpdButton1";
			this.hpdButton1.ScriptCode = hpdScriptCode1;
			this.hpdButton1.Size = new System.Drawing.Size(131, 48);
			this.hpdButton1.TabIndex = 10;
			this.hpdButton1.Text = "hpdButton1";
			this.hpdButton1.TextAligiment = System.Drawing.StringAlignment.Near;
			this.hpdButton1.TextLineAligiment = System.Drawing.StringAlignment.Near;
			this.hpdButton1.UnCheckedColor = System.Drawing.Color.White;
			// 
			// hpdButton2
			// 
			this.hpdButton2.CanColorCustum = false;
			this.hpdButton2.ControlName = "hpdButton2";
			this.hpdButton2.FileName = "";
			this.hpdButton2.ForcusColor = System.Drawing.Color.White;
			this.hpdButton2.FrameWeight = new System.Windows.Forms.Padding(1);
			this.hpdButton2.IsDrawFocuse = true;
			this.hpdButton2.IsDrawFrame = true;
			this.hpdButton2.IsSaveFileName = false;
			this.hpdButton2.Lines = new string[] {
        "hpdButton2"};
			this.hpdButton2.Location = new System.Drawing.Point(21, 178);
			this.hpdButton2.Name = "hpdButton2";
			this.hpdButton2.ScriptCode = hpdScriptCode2;
			this.hpdButton2.Size = new System.Drawing.Size(131, 66);
			this.hpdButton2.TabIndex = 11;
			this.hpdButton2.Text = "hpdButton2";
			this.hpdButton2.TextAligiment = System.Drawing.StringAlignment.Near;
			this.hpdButton2.TextLineAligiment = System.Drawing.StringAlignment.Near;
			this.hpdButton2.UnCheckedColor = System.Drawing.Color.White;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(716, 24);
			this.menuStrip1.TabIndex = 12;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// quitToolStripMenuItem
			// 
			this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			this.quitToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
			this.quitToolStripMenuItem.Text = "Quit";
			// 
			// hpdControlTree1
			// 
			this.hpdControlTree1.CanColorCustum = false;
			this.hpdControlTree1.ControlName = "hpdControlTree1";
			this.hpdControlTree1.FileName = "";
			this.hpdControlTree1.ForcusColor = System.Drawing.Color.White;
			this.hpdControlTree1.Form = this;
			this.hpdControlTree1.FrameWeight = new System.Windows.Forms.Padding(1);
			this.hpdControlTree1.IsDrawFocuse = true;
			this.hpdControlTree1.IsDrawFrame = true;
			this.hpdControlTree1.IsSaveFileName = false;
			this.hpdControlTree1.Lines = new string[] {
        "hpdControlTree1"};
			this.hpdControlTree1.Location = new System.Drawing.Point(405, 27);
			this.hpdControlTree1.Name = "hpdControlTree1";
			this.hpdControlTree1.ScriptCode = hpdScriptCode3;
			this.hpdControlTree1.SelectedControl = null;
			this.hpdControlTree1.SelectedNode = null;
			this.hpdControlTree1.Size = new System.Drawing.Size(181, 265);
			this.hpdControlTree1.TabIndex = 13;
			this.hpdControlTree1.Text = "hpdControlTree1";
			this.hpdControlTree1.TextAligiment = System.Drawing.StringAlignment.Near;
			this.hpdControlTree1.TextLineAligiment = System.Drawing.StringAlignment.Near;
			this.hpdControlTree1.UnCheckedColor = System.Drawing.Color.White;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(716, 388);
			this.Controls.Add(this.hpdControlTree1);
			this.Controls.Add(this.hpdButton2);
			this.Controls.Add(this.hpdButton1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.roslynPadControl1);
			this.Controls.Add(this.menuStrip1);
			this.DoubleBuffered = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "Form1";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		public Button button3;
		public BRY.RoslynPadControl roslynPadControl1;
		private Button button1;
		private Hpd.HpdButton hpdButton1;
		private Hpd.HpdButton hpdButton2;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem quitToolStripMenuItem;
		private Hpd.HpdControlTree hpdControlTree1;
	}
}