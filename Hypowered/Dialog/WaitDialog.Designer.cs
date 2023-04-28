namespace Hypowered
{
	partial class WaitDialog
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
			label1 = new Label();
			SuspendLayout();
			// 
			// label1
			// 
			label1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label1.ForeColor = Color.Gainsboro;
			label1.Location = new Point(9, 9);
			label1.Name = "label1";
			label1.Size = new Size(175, 25);
			label1.TabIndex = 0;
			label1.Text = "Please wait a while";
			label1.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// WaitDialog
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Gray;
			ClientSize = new Size(196, 43);
			Controls.Add(label1);
			ForeColor = Color.Gainsboro;
			FormBorderStyle = FormBorderStyle.None;
			Name = "WaitDialog";
			Text = "MesDialog";
			ResumeLayout(false);
		}

		#endregion

		private Label label1;
	}
}