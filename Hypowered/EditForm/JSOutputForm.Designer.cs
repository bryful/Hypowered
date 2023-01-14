namespace Hypowered
{
	partial class JSOutputForm
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btnFont = new Hypowered.FlatBtn();
			this.btnCLS = new Hypowered.FlatBtn();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.ForeColor = System.Drawing.Color.Gainsboro;
			this.textBox1.Location = new System.Drawing.Point(12, 31);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size(876, 248);
			this.textBox1.TabIndex = 1;
			// 
			// btnFont
			// 
			this.btnFont.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnFont.Location = new System.Drawing.Point(25, 5);
			this.btnFont.Name = "btnFont";
			this.btnFont.Size = new System.Drawing.Size(52, 18);
			this.btnFont.TabIndex = 4;
			this.btnFont.Text = "FONT";
			this.btnFont.Click += new System.EventHandler(this.BtnFont_Click);
			// 
			// btnCLS
			// 
			this.btnCLS.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnCLS.Location = new System.Drawing.Point(77, 5);
			this.btnCLS.Name = "btnCLS";
			this.btnCLS.Size = new System.Drawing.Size(52, 18);
			this.btnCLS.TabIndex = 5;
			this.btnCLS.Text = "CLS";
			this.btnCLS.Click += new System.EventHandler(this.BtnCLS_Click);
			// 
			// JSOutputForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CanResize = true;
			this.ClientSize = new System.Drawing.Size(900, 291);
			this.Controls.Add(this.btnCLS);
			this.Controls.Add(this.btnFont);
			this.Controls.Add(this.textBox1);
			this.Name = "JSOutputForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "JSOutputForm";
			this.Controls.SetChildIndex(this.textBox1, 0);
			this.Controls.SetChildIndex(this.btnFont, 0);
			this.Controls.SetChildIndex(this.btnCLS, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private TextBox textBox1;
		private FlatBtn btnFont;
		private FlatBtn btnCLS;
	}
}