namespace Hypowered
{
	partial class AlertForm
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
			textBox1 = new TextBox();
			btnOK = new Button();
			btnFont = new Button();
			SuspendLayout();
			// 
			// textBox1
			// 
			textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			textBox1.BackColor = Color.FromArgb(64, 64, 64);
			textBox1.BorderStyle = BorderStyle.FixedSingle;
			textBox1.ForeColor = Color.FromArgb(230, 230, 230);
			textBox1.Location = new Point(12, 29);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.ReadOnly = true;
			textBox1.Size = new Size(474, 111);
			textBox1.TabIndex = 0;
			// 
			// btnOK
			// 
			btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnOK.DialogResult = DialogResult.OK;
			btnOK.FlatStyle = FlatStyle.Flat;
			btnOK.ForeColor = SystemColors.ButtonShadow;
			btnOK.Location = new Point(395, 146);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(75, 23);
			btnOK.TabIndex = 1;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = true;
			// 
			// btnFont
			// 
			btnFont.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnFont.DialogResult = DialogResult.OK;
			btnFont.FlatStyle = FlatStyle.Flat;
			btnFont.ForeColor = SystemColors.ButtonShadow;
			btnFont.Location = new Point(12, 146);
			btnFont.Name = "btnFont";
			btnFont.Size = new Size(75, 23);
			btnFont.TabIndex = 2;
			btnFont.Text = "Font";
			btnFont.UseVisualStyleBackColor = true;
			// 
			// AlertForm
			// 
			AutoScaleMode = AutoScaleMode.None;
			ClientSize = new Size(498, 175);
			CloseAction = CloseAction.DROK;
			Controls.Add(btnFont);
			Controls.Add(btnOK);
			Controls.Add(textBox1);
			IsShowTopMost = false;
			MaximizeBox = false;
			MdiChildrenMinimizedAnchorBottom = false;
			MinimizeBox = false;
			Name = "AlertForm";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Alert";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox textBox1;
		private Button btnOK;
		private Button btnFont;
	}
}