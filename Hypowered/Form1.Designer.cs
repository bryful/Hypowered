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
			hListBox1 = new HListBox();
			SuspendLayout();
			// 
			// hListBox1
			// 
			hListBox1.BackColor = Color.FromArgb(64, 64, 64);
			hListBox1.CenterHeight = 213D;
			hListBox1.CenterWidth = 313D;
			hListBox1.ForcusColor = Color.Blue;
			hListBox1.ForeColor = Color.FromArgb(230, 230, 230);
			hListBox1.GridSize = 2;
			hListBox1.Index = -1;
			hListBox1.IsEdit = false;
			hListBox1.IsEditColor = Color.Red;
			hListBox1.Items = (new string[] { "aaa", "bbb", "ccc" });
			hListBox1.Location = new Point(190, 118);
			hListBox1.Name = "hListBox1";
			hListBox1.SelectedColor = Color.FromArgb(100, 100, 100);
			hListBox1.Size = new Size(246, 190);
			hListBox1.TabIndex = 0;
			hListBox1.TargetColor = Color.FromArgb(150, 100, 100);
			hListBox1.Text = "hListBox1";
			hListBox1.TextAlign = StringAlignment.Near;
			hListBox1.TextLineAlign = StringAlignment.Center;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(hListBox1);
			Name = "Form1";
			Text = "Form1";
			ResumeLayout(false);
		}

		#endregion

		private HListBox hListBox1;
	}
}