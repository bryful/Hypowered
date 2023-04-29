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
			HScriptCode hScriptCode1 = new HScriptCode();
			hRadioButton1 = new HRadioButton();
			SuspendLayout();
			// 
			// hRadioButton1
			// 
			hRadioButton1.BackColor = Color.FromArgb(64, 64, 64);
			hRadioButton1.CenterX = 281D;
			hRadioButton1.CenterY = 261D;
			hRadioButton1.ColCount = 2;
			hRadioButton1.ForcusColor = Color.Blue;
			hRadioButton1.ForeColor = Color.FromArgb(230, 230, 230);
			hRadioButton1.GridSize = 2;
			hRadioButton1.Index = 1;
			hRadioButton1.IsAnti = false;
			hRadioButton1.IsShowForcus = true;
			hRadioButton1.Location = new Point(144, 240);
			hRadioButton1.Name = "hRadioButton1";
			hRadioButton1.RowCount = 2;
			hRadioButton1.ScriptCode = hScriptCode1;
			hRadioButton1.Selected = false;
			hRadioButton1.SelectedColor = Color.FromArgb(150, 100, 100);
			hRadioButton1.Size = new Size(274, 42);
			hRadioButton1.TabIndex = 1;
			hRadioButton1.Text = "hRadioButton1";
			hRadioButton1.TextAlign = StringAlignment.Center;
			hRadioButton1.TextLineAlign = StringAlignment.Center;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(908, 609);
			Controls.Add(hRadioButton1);
			MainMenuVisible = true;
			Name = "Form1";
			SelectedArray = (new bool[] { false, false });
			Text = "Form1";
			ResumeLayout(false);
		}

		#endregion

		private HRadioButton hRadioButton1;
	}
}