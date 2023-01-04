namespace Hypowered
{
	partial class EditPictLibDialog
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
			this.tbName = new System.Windows.Forms.TextBox();
			this.tbInfo = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
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
			this.pictLibBox1.Location = new System.Drawing.Point(50, 31);
			this.pictLibBox1.Name = "pictLibBox1";
			this.pictLibBox1.RightBtn = this.btnRight;
			this.pictLibBox1.Size = new System.Drawing.Size(577, 349);
			this.pictLibBox1.TabIndex = 1;
			this.pictLibBox1.TargetIndex = -1;
			this.pictLibBox1.Text = "pictLibBox1";
			this.pictLibBox1.TextBox_FileName = this.tbName;
			this.pictLibBox1.TextBox_Info = this.tbInfo;
			this.pictLibBox1.UserPictColor = System.Drawing.Color.Red;
			// 
			// btnLeft
			// 
			this.btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLeft.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnLeft.Location = new System.Drawing.Point(20, 31);
			this.btnLeft.Name = "btnLeft";
			this.btnLeft.Size = new System.Drawing.Size(25, 349);
			this.btnLeft.TabIndex = 4;
			this.btnLeft.Text = "<";
			this.btnLeft.UseVisualStyleBackColor = true;
			// 
			// btnRight
			// 
			this.btnRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRight.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnRight.Location = new System.Drawing.Point(632, 31);
			this.btnRight.Name = "btnRight";
			this.btnRight.Size = new System.Drawing.Size(25, 349);
			this.btnRight.TabIndex = 5;
			this.btnRight.Text = ">";
			this.btnRight.UseVisualStyleBackColor = true;
			// 
			// tbName
			// 
			this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbName.ForeColor = System.Drawing.Color.LightGray;
			this.tbName.Location = new System.Drawing.Point(50, 395);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(324, 23);
			this.tbName.TabIndex = 6;
			// 
			// tbInfo
			// 
			this.tbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.tbInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbInfo.ForeColor = System.Drawing.Color.LightGray;
			this.tbInfo.Location = new System.Drawing.Point(50, 424);
			this.tbInfo.Name = "tbInfo";
			this.tbInfo.Size = new System.Drawing.Size(324, 23);
			this.tbInfo.TabIndex = 7;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOK.ForeColor = System.Drawing.Color.Silver;
			this.btnOK.Location = new System.Drawing.Point(512, 405);
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
			this.btnCancel.Location = new System.Drawing.Point(391, 405);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(115, 45);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// HyperPictLibDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.ClientSize = new System.Drawing.Size(670, 460);
			this.Controls.Add(this.tbInfo);
			this.Controls.Add(this.tbName);
			this.Controls.Add(this.btnRight);
			this.Controls.Add(this.btnLeft);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.pictLibBox1);
			this.Name = "HyperPictLibDialog";
			this.Text = "HyperPictLibDialog";
			this.Controls.SetChildIndex(this.pictLibBox1, 0);
			this.Controls.SetChildIndex(this.btnOK, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.btnLeft, 0);
			this.Controls.SetChildIndex(this.btnRight, 0);
			this.Controls.SetChildIndex(this.tbName, 0);
			this.Controls.SetChildIndex(this.tbInfo, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private PictLibBox pictLibBox1;
		private Button btnOK;
		private Button btnCancel;
		private Button btnLeft;
		private Button btnRight;
		private TextBox tbName;
		private TextBox tbInfo;
	}
}