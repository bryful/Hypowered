namespace Hypowered
{
	partial class CreateFormDialog
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
			tbName = new TextBox();
			label1 = new Label();
			label2 = new Label();
			btnOK = new Button();
			btnCancel = new Button();
			btnDir = new Button();
			numWidth = new NumericUpDown();
			numHeight = new NumericUpDown();
			label3 = new Label();
			((System.ComponentModel.ISupportInitialize)numWidth).BeginInit();
			((System.ComponentModel.ISupportInitialize)numHeight).BeginInit();
			SuspendLayout();
			// 
			// tbName
			// 
			tbName.BackColor = Color.FromArgb(64, 64, 64);
			tbName.BorderStyle = BorderStyle.FixedSingle;
			tbName.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			tbName.ForeColor = Color.FromArgb(230, 230, 230);
			tbName.Location = new Point(74, 37);
			tbName.Name = "tbName";
			tbName.Size = new Size(203, 29);
			tbName.TabIndex = 0;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(16, 39);
			label1.Name = "label1";
			label1.Size = new Size(52, 21);
			label1.TabIndex = 1;
			label1.Text = "Name";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label2.Location = new Point(16, 85);
			label2.Name = "label2";
			label2.Size = new Size(52, 21);
			label2.TabIndex = 2;
			label2.Text = "Width";
			// 
			// btnOK
			// 
			btnOK.FlatStyle = FlatStyle.Flat;
			btnOK.Location = new Point(231, 136);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(75, 23);
			btnOK.TabIndex = 4;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			btnCancel.FlatStyle = FlatStyle.Flat;
			btnCancel.Location = new Point(133, 136);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(75, 23);
			btnCancel.TabIndex = 5;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnDir
			// 
			btnDir.FlatStyle = FlatStyle.Flat;
			btnDir.Location = new Point(283, 40);
			btnDir.Name = "btnDir";
			btnDir.Size = new Size(33, 23);
			btnDir.TabIndex = 6;
			btnDir.Text = "Dir";
			btnDir.UseVisualStyleBackColor = true;
			// 
			// numWidth
			// 
			numWidth.BackColor = Color.FromArgb(64, 64, 64);
			numWidth.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			numWidth.ForeColor = Color.FromArgb(230, 230, 230);
			numWidth.Location = new Point(74, 83);
			numWidth.Maximum = new decimal(new int[] { 1980, 0, 0, 0 });
			numWidth.Minimum = new decimal(new int[] { 150, 0, 0, 0 });
			numWidth.Name = "numWidth";
			numWidth.Size = new Size(85, 29);
			numWidth.TabIndex = 7;
			numWidth.Value = new decimal(new int[] { 640, 0, 0, 0 });
			// 
			// numHeight
			// 
			numHeight.BackColor = Color.FromArgb(64, 64, 64);
			numHeight.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			numHeight.ForeColor = Color.FromArgb(230, 230, 230);
			numHeight.Location = new Point(231, 85);
			numHeight.Maximum = new decimal(new int[] { 1980, 0, 0, 0 });
			numHeight.Minimum = new decimal(new int[] { 150, 0, 0, 0 });
			numHeight.Name = "numHeight";
			numHeight.Size = new Size(85, 29);
			numHeight.TabIndex = 9;
			numHeight.Value = new decimal(new int[] { 300, 0, 0, 0 });
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label3.Location = new Point(173, 87);
			label3.Name = "label3";
			label3.Size = new Size(56, 21);
			label3.TabIndex = 8;
			label3.Text = "Height";
			// 
			// CreateFormDialog
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			CanResize = false;
			ClientSize = new Size(343, 178);
			CloseAction = CloseAction.DRCancel;
			Controls.Add(numHeight);
			Controls.Add(label3);
			Controls.Add(numWidth);
			Controls.Add(btnDir);
			Controls.Add(btnCancel);
			Controls.Add(btnOK);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(tbName);
			Name = "CreateFormDialog";
			StartPosition = FormStartPosition.CenterParent;
			Text = "New Form";
			((System.ComponentModel.ISupportInitialize)numWidth).EndInit();
			((System.ComponentModel.ISupportInitialize)numHeight).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox tbName;
		private Label label1;
		private Label label2;
		private Button btnCancel;
		private Button btnOK;
		private Button btnDir;
		private NumericUpDown numWidth;
		private NumericUpDown numHeight;
		private Label label3;
	}
}