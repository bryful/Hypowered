namespace Hypowered
{
	partial class RenameFormDialog
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
			tbOrg = new TextBox();
			label1 = new Label();
			label2 = new Label();
			tbNew = new TextBox();
			btnOK = new Button();
			btnCancel = new Button();
			SuspendLayout();
			// 
			// tbOrg
			// 
			tbOrg.BackColor = Color.FromArgb(64, 64, 64);
			tbOrg.BorderStyle = BorderStyle.FixedSingle;
			tbOrg.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			tbOrg.ForeColor = Color.FromArgb(230, 230, 230);
			tbOrg.Location = new Point(105, 43);
			tbOrg.Name = "tbOrg";
			tbOrg.Size = new Size(166, 29);
			tbOrg.TabIndex = 0;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(32, 47);
			label1.Name = "label1";
			label1.Size = new Size(66, 21);
			label1.TabIndex = 1;
			label1.Text = "Original";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label2.Location = new Point(32, 82);
			label2.Name = "label2";
			label2.Size = new Size(42, 21);
			label2.TabIndex = 3;
			label2.Text = "New";
			// 
			// tbNew
			// 
			tbNew.BackColor = Color.FromArgb(64, 64, 64);
			tbNew.BorderStyle = BorderStyle.FixedSingle;
			tbNew.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			tbNew.ForeColor = Color.FromArgb(230, 230, 230);
			tbNew.Location = new Point(105, 78);
			tbNew.Name = "tbNew";
			tbNew.Size = new Size(166, 29);
			tbNew.TabIndex = 2;
			// 
			// btnOK
			// 
			btnOK.Enabled = false;
			btnOK.FlatStyle = FlatStyle.Flat;
			btnOK.Location = new Point(196, 121);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(75, 23);
			btnOK.TabIndex = 4;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.FlatStyle = FlatStyle.Flat;
			btnCancel.Location = new Point(115, 121);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(75, 23);
			btnCancel.TabIndex = 5;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// RenameFormDialog
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(327, 156);
			CloseAction = CloseAction.DRCancel;
			Controls.Add(btnCancel);
			Controls.Add(btnOK);
			Controls.Add(label2);
			Controls.Add(tbNew);
			Controls.Add(label1);
			Controls.Add(tbOrg);
			Name = "RenameFormDialog";
			Text = "RenameFormDialog";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox tbOrg;
		private Label label1;
		private Label label2;
		private TextBox tbNew;
		private Button btnOK;
		private Button btnCancel;
	}
}