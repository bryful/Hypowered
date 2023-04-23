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
			menuPanel1 = new MenuPanel();
			SuspendLayout();
			// 
			// menuPanel1
			// 
			menuPanel1.HForm = null;
			menuPanel1.Location = new Point(131, 112);
			menuPanel1.MainForm = null;
			menuPanel1.Name = "menuPanel1";
			menuPanel1.Size = new Size(170, 188);
			menuPanel1.TabIndex = 1;
			menuPanel1.Text = "menuPanel1";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(656, 472);
			Controls.Add(menuPanel1);
			MainMenuVisible = true;
			Name = "Form1";
			SelectedArray = (new bool[] { false, false });
			Text = "Form1";
			ResumeLayout(false);
		}

		#endregion

		private MenuPanel menuPanel1;
	}
}