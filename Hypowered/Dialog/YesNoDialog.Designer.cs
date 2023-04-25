namespace Hypowered
{
	partial class YesNoDialog
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
			label1 = new Label();
			btnOK = new Button();
			btnCancel = new Button();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(27, 41);
			label1.Name = "label1";
			label1.Size = new Size(52, 21);
			label1.TabIndex = 0;
			label1.Text = "label1";
			// 
			// btnOK
			// 
			btnOK.DialogResult = DialogResult.OK;
			btnOK.FlatStyle = FlatStyle.Flat;
			btnOK.Location = new Point(170, 76);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(75, 23);
			btnOK.TabIndex = 1;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.FlatStyle = FlatStyle.Flat;
			btnCancel.Location = new Point(80, 76);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(75, 23);
			btnCancel.TabIndex = 2;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// YesNoDialog
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(290, 110);
			CloseAction = CloseAction.DRCancel;
			Controls.Add(btnCancel);
			Controls.Add(btnOK);
			Controls.Add(label1);
			IsShowTopMost = false;
			Name = "YesNoDialog";
			Text = "YesNoDialog";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private Button btnOK;
		private Button btnCancel;
	}
}