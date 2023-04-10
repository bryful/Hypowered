namespace Hypowered
{
	partial class Form1
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
			listBox1 = new ListBox();
			textBox1 = new TextBox();
			hTextBox1 = new HTextBox();
			SuspendLayout();
			// 
			// listBox1
			// 
			listBox1.FormattingEnabled = true;
			listBox1.ItemHeight = 15;
			listBox1.Location = new Point(134, 101);
			listBox1.Name = "listBox1";
			listBox1.SelectionMode = SelectionMode.MultiSimple;
			listBox1.Size = new Size(120, 94);
			listBox1.TabIndex = 0;
			// 
			// textBox1
			// 
			textBox1.Location = new Point(302, 235);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.PlaceholderText = "1313";
			textBox1.Size = new Size(178, 87);
			textBox1.TabIndex = 1;
			textBox1.Text = "2656516";
			// 
			// hTextBox1
			// 
			hTextBox1.BackColor = Color.FromArgb(64, 64, 64);
			hTextBox1.CenterHeight = 101.5D;
			hTextBox1.CenterWidth = 563D;
			hTextBox1.ForeColor = Color.FromArgb(230, 230, 230);
			hTextBox1.GridSize = 2;
			hTextBox1.Index = -1;
			hTextBox1.IsEdit = false;
			hTextBox1.IsEditColor = Color.Yellow;
			hTextBox1.Location = new Point(500, 88);
			hTextBox1.Name = "hTextBox1";
			hTextBox1.Size = new Size(126, 27);
			hTextBox1.TabIndex = 2;
			hTextBox1.Text = "hTextBox1";
			hTextBox1.TextAlign = StringAlignment.Near;
			hTextBox1.TextLineAlign = StringAlignment.Center;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(hTextBox1);
			Controls.Add(textBox1);
			Controls.Add(listBox1);
			Name = "Form1";
			Text = "Form1";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ListBox listBox1;
		private TextBox textBox1;
		private HTextBox hTextBox1;
	}
}