﻿namespace Hypowered
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
			this.SuspendLayout();
			// 
			// Editor
			// 
			this.Editor.Location = new System.Drawing.Point(234, 234);
			this.Editor.Text = "HyperMenuBar";
			// 
			// Form1
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(785, 493);
			this.DoubleBuffered = true;
			this.IsShowMenu = true;
			this.Name = "Form1";
			this.TargetIndex = 0;
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion
	}
}