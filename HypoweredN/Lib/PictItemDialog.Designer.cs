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
			pictItemList1 = new PictItemList();
			btnOK = new Button();
			btnCancel = new Button();
			textBox1 = new TextBox();
			SuspendLayout();
			// 
			// pictItemList1
			// 
			pictItemList1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			pictItemList1.BackColor = Color.FromArgb(64, 64, 64);
			pictItemList1.ForeColor = Color.FromArgb(230, 230, 230);
			pictItemList1.IndexColor = Color.FromArgb(150, 200, 200);
			pictItemList1.LineColor = Color.FromArgb(150, 150, 150);
			pictItemList1.Location = new Point(12, 50);
			pictItemList1.Name = "pictItemList1";
			pictItemList1.SidePushColor = Color.FromArgb(200, 200, 200);
			pictItemList1.Size = new Size(748, 327);
			pictItemList1.TabIndex = 0;
			pictItemList1.Text = "pictItemList1";
			// 
			// btnOK
			// 
			btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnOK.FlatStyle = FlatStyle.Flat;
			btnOK.Location = new Point(613, 383);
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
			btnCancel.Location = new Point(474, 383);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(108, 32);
			btnCancel.TabIndex = 2;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBox1.BackColor = Color.FromArgb(64, 64, 64);
			textBox1.BorderStyle = BorderStyle.None;
			textBox1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			textBox1.ForeColor = Color.FromArgb(230, 230, 230);
			textBox1.Location = new Point(12, 22);
			textBox1.Name = "textBox1";
			textBox1.ReadOnly = true;
			textBox1.Size = new Size(748, 22);
			textBox1.TabIndex = 3;
			// 
			// PictItemDialog
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(773, 421);
			CloseAction = CloseAction.DRCancel;
			Controls.Add(textBox1);
			Controls.Add(btnCancel);
			Controls.Add(btnOK);
			Controls.Add(pictItemList1);
			Name = "PictItemDialog";
			Text = "PictItemDialog";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private PictItemList pictItemList1;
		private Button btnOK;
		private Button btnCancel;
		private TextBox textBox1;
	}
}