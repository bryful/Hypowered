namespace Hypowered
{
	partial class ConsoleForm
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
			tbOutput = new TextBox();
			btnClear = new Button();
			btnFont = new Button();
			SuspendLayout();
			// 
			// tbOutput
			// 
			tbOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			tbOutput.BackColor = Color.FromArgb(64, 64, 64);
			tbOutput.BorderStyle = BorderStyle.FixedSingle;
			tbOutput.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			tbOutput.ForeColor = Color.FromArgb(230, 230, 230);
			tbOutput.Location = new Point(14, 27);
			tbOutput.Margin = new Padding(5);
			tbOutput.MaxLength = 65536;
			tbOutput.Multiline = true;
			tbOutput.Name = "tbOutput";
			tbOutput.ScrollBars = ScrollBars.Vertical;
			tbOutput.Size = new Size(433, 90);
			tbOutput.TabIndex = 1;
			tbOutput.Text = "123456";
			// 
			// btnClear
			// 
			btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnClear.FlatStyle = FlatStyle.Flat;
			btnClear.ForeColor = Color.FromArgb(150, 150, 150);
			btnClear.Location = new Point(14, 122);
			btnClear.Name = "btnClear";
			btnClear.Size = new Size(50, 24);
			btnClear.TabIndex = 2;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = true;
			// 
			// btnFont
			// 
			btnFont.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnFont.FlatStyle = FlatStyle.Flat;
			btnFont.ForeColor = Color.FromArgb(150, 150, 150);
			btnFont.Location = new Point(70, 122);
			btnFont.Name = "btnFont";
			btnFont.Size = new Size(50, 24);
			btnFont.TabIndex = 3;
			btnFont.Text = "Font";
			btnFont.UseVisualStyleBackColor = true;
			// 
			// ConsoleForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(461, 151);
			CloseAction = CloseAction.Hide;
			ControlBox = false;
			Controls.Add(btnFont);
			Controls.Add(btnClear);
			Controls.Add(tbOutput);
			MaximizeBox = false;
			MdiChildrenMinimizedAnchorBottom = false;
			MinimizeBox = false;
			Name = "ConsoleForm";
			ShowIcon = false;
			ShowInTaskbar = false;
			Text = "ConsoleForm";
			TopMost = true;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private TextBox tbOutput;
		private Button btnClear;
		private Button btnFont;
	}
}