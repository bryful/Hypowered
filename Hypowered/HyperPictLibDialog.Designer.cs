namespace Hypowered
{
	partial class HyperPictLibDialog
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
			this.pictLibBox1 = new Hypowered.PictLibBox();
			this.btnLeft = new System.Windows.Forms.Button();
			this.btnRight = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// pictLibBox1
			// 
			this.pictLibBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictLibBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
			this.pictLibBox1.ForcusColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(133)))), ((int)(((byte)(222)))));
			this.pictLibBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.pictLibBox1.LeftBtn = this.btnLeft;
			this.pictLibBox1.Location = new System.Drawing.Point(50, 12);
			this.pictLibBox1.Name = "pictLibBox1";
			this.pictLibBox1.RightBtn = this.btnRight;
			this.pictLibBox1.Size = new System.Drawing.Size(700, 392);
			this.pictLibBox1.TabIndex = 1;
			this.pictLibBox1.TargetIndex = -1;
			this.pictLibBox1.Text = "pictLibBox1";
			this.pictLibBox1.TextBox = this.textBox1;
			// 
			// btnLeft
			// 
			this.btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLeft.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnLeft.Location = new System.Drawing.Point(20, 12);
			this.btnLeft.Name = "btnLeft";
			this.btnLeft.Size = new System.Drawing.Size(25, 392);
			this.btnLeft.TabIndex = 4;
			this.btnLeft.Text = "<";
			this.btnLeft.UseVisualStyleBackColor = true;
			// 
			// btnRight
			// 
			this.btnRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRight.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnRight.Location = new System.Drawing.Point(755, 12);
			this.btnRight.Name = "btnRight";
			this.btnRight.Size = new System.Drawing.Size(25, 392);
			this.btnRight.TabIndex = 5;
			this.btnRight.Text = ">";
			this.btnRight.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOK.ForeColor = System.Drawing.Color.Silver;
			this.btnOK.Location = new System.Drawing.Point(635, 410);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(115, 45);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.ForeColor = System.Drawing.Color.Silver;
			this.btnCancel.Location = new System.Drawing.Point(514, 410);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(115, 45);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.ForeColor = System.Drawing.Color.LightGray;
			this.textBox1.Location = new System.Drawing.Point(20, 422);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(287, 23);
			this.textBox1.TabIndex = 6;
			// 
			// HyperPictLibDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.ClientSize = new System.Drawing.Size(793, 465);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.btnRight);
			this.Controls.Add(this.btnLeft);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.pictLibBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "HyperPictLibDialog";
			this.Text = "HyperPictLibDialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private PictLibBox pictLibBox1;
		private Button btnOK;
		private Button btnCancel;
		private Button btnLeft;
		private Button btnRight;
		private TextBox textBox1;
	}
}