namespace Hypowered
{
	partial class PictItemDialog
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
			btnOK = new Button();
			btnCancel = new Button();
			textBox1 = new TextBox();
			pictItemListTwin1 = new PictItemListTwin();
			SuspendLayout();
			// 
			// btnOK
			// 
			btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnOK.FlatStyle = FlatStyle.Flat;
			btnOK.Location = new Point(617, 470);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(108, 32);
			btnOK.TabIndex = 1;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnCancel.FlatStyle = FlatStyle.Flat;
			btnCancel.Location = new Point(486, 470);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(108, 32);
			btnCancel.TabIndex = 2;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			textBox1.BackColor = Color.FromArgb(64, 64, 64);
			textBox1.BorderStyle = BorderStyle.None;
			textBox1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			textBox1.ForeColor = Color.FromArgb(230, 230, 230);
			textBox1.Location = new Point(10, 438);
			textBox1.Name = "textBox1";
			textBox1.ReadOnly = true;
			textBox1.Size = new Size(750, 22);
			textBox1.TabIndex = 3;
			// 
			// pictItemListTwin1
			// 
			pictItemListTwin1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			pictItemListTwin1.IsBuildin = true;
			pictItemListTwin1.Location = new Point(10, 25);
			pictItemListTwin1.Name = "pictItemListTwin1";
			pictItemListTwin1.Size = new Size(753, 407);
			pictItemListTwin1.TabIndex = 5;
			pictItemListTwin1.Text = "pictItemListTwin1";
			// 
			// PictItemDialog
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(775, 514);
			CloseAction = CloseAction.DRCancel;
			Controls.Add(pictItemListTwin1);
			Controls.Add(textBox1);
			Controls.Add(btnCancel);
			Controls.Add(btnOK);
			Name = "PictItemDialog";
			Text = "PictItemDialog";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private Button btnOK;
		private Button btnCancel;
		private TextBox textBox1;
		private PictItemListTwin pictItemListTwin1;
	}
}