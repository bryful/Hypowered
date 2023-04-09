namespace Hypowered
{
	partial class AddControlDialog
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
			hTypeCombo1 = new HTypeCombo();
			label1 = new Label();
			label2 = new Label();
			textBox1 = new TextBox();
			btnOK = new Button();
			btnCancel = new Button();
			SuspendLayout();
			// 
			// hTypeCombo1
			// 
			hTypeCombo1.DropDownStyle = ComboBoxStyle.DropDownList;
			hTypeCombo1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			hTypeCombo1.FormattingEnabled = true;
			hTypeCombo1.HType = HType.Button;
			hTypeCombo1.Items.AddRange(new object[] { "Button", "Label", "TextBox" });
			hTypeCombo1.Location = new Point(75, 39);
			hTypeCombo1.Name = "hTypeCombo1";
			hTypeCombo1.Size = new Size(192, 29);
			hTypeCombo1.TabIndex = 0;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(27, 42);
			label1.Name = "label1";
			label1.Size = new Size(42, 21);
			label1.TabIndex = 1;
			label1.Text = "Type";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label2.Location = new Point(17, 77);
			label2.Name = "label2";
			label2.Size = new Size(52, 21);
			label2.TabIndex = 2;
			label2.Text = "Name";
			// 
			// textBox1
			// 
			textBox1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			textBox1.Location = new Point(75, 74);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(192, 29);
			textBox1.TabIndex = 3;
			// 
			// btnOK
			// 
			btnOK.Enabled = false;
			btnOK.FlatStyle = FlatStyle.Flat;
			btnOK.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			btnOK.Location = new Point(192, 109);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(75, 30);
			btnOK.TabIndex = 4;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.FlatStyle = FlatStyle.Flat;
			btnCancel.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			btnCancel.Location = new Point(98, 109);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(75, 30);
			btnCancel.TabIndex = 5;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// AddControlDialog
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(292, 155);
			CloseAction = CloseAction.DRCancel;
			Controls.Add(btnCancel);
			Controls.Add(btnOK);
			Controls.Add(textBox1);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(hTypeCombo1);
			Name = "AddControlDialog";
			Text = "AddControlDialog";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private HTypeCombo hTypeCombo1;
		private Label label1;
		private Label label2;
		private TextBox textBox1;
		private Button btnOK;
		private Button btnCancel;
	}
}