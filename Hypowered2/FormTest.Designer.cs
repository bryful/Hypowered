namespace Hpd
{
	partial class FormTest
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
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.SuspendLayout();
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(94, 182);
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(272, 45);
			this.trackBar1.TabIndex = 0;
			this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.trackBar1.Value = 5;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(51, 84);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(315, 23);
			this.progressBar1.TabIndex = 1;
			// 
			// FormTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(462, 303);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.trackBar1);
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Name = "FormTest";
			this.Text = "AAAA";
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private TrackBar trackBar1;
		private ProgressBar progressBar1;
	}
}