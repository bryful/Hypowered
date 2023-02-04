namespace Hpd
{
    partial class Main
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
			// 
			// Main
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BaseSize = new System.Drawing.Size(97, 92);
			this.ClientRectangleEx = new System.Drawing.Rectangle(0, 24, 581, 315);
			this.ClientSize = new System.Drawing.Size(581, 339);
			this.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.DoubleBuffered = true;
			this.MenuVisible = true;
			this.Name = "Main";
			this.Orientation = Hpd.HpdOrientation.Horizontal;
			this.Text = "Main";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
	}
}