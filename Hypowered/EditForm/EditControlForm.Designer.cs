namespace Hypowered
{
	partial class EditControlForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.tbName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.editControlComb1 = new Hypowered.EditControlComb();
			this.lbInfo = new System.Windows.Forms.Label();
			this.btnIcon = new System.Windows.Forms.Button();
			this.btnFont = new System.Windows.Forms.Button();
			this.btnScript = new System.Windows.Forms.Button();
			this.btnContent = new System.Windows.Forms.Button();
			this.btnConnect = new System.Windows.Forms.Button();
			this.tbText = new System.Windows.Forms.TextBox();
			this.tbDes = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnOpenFile = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.ForeColor = System.Drawing.Color.Gainsboro;
			this.label1.Location = new System.Drawing.Point(21, 102);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 21);
			this.label1.TabIndex = 5;
			this.label1.Text = "Name";
			// 
			// tbName
			// 
			this.tbName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbName.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.tbName.ForeColor = System.Drawing.Color.Gainsboro;
			this.tbName.Location = new System.Drawing.Point(79, 99);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(265, 29);
			this.tbName.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.ForeColor = System.Drawing.Color.Gainsboro;
			this.label2.Location = new System.Drawing.Point(37, 137);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(36, 21);
			this.label2.TabIndex = 7;
			this.label2.Text = "Text";
			// 
			// btnCancel
			// 
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnCancel.Location = new System.Drawing.Point(274, 172);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(70, 25);
			this.btnCancel.TabIndex = 15;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Enabled = false;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOK.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnOK.Location = new System.Drawing.Point(274, 203);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(70, 25);
			this.btnOK.TabIndex = 16;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label3.ForeColor = System.Drawing.Color.Gainsboro;
			this.label3.Location = new System.Drawing.Point(11, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(62, 21);
			this.label3.TabIndex = 1;
			this.label3.Text = "Control";
			// 
			// editControlComb1
			// 
			this.editControlComb1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
			this.editControlComb1.ControlType = Hypowered.ControlType.Button;
			this.editControlComb1.DropDownHeight = 200;
			this.editControlComb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.editControlComb1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.editControlComb1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.editControlComb1.FormattingEnabled = true;
			this.editControlComb1.IntegralHeight = false;
			this.editControlComb1.Location = new System.Drawing.Point(79, 42);
			this.editControlComb1.Name = "editControlComb1";
			this.editControlComb1.Size = new System.Drawing.Size(265, 23);
			this.editControlComb1.TabIndex = 2;
			// 
			// lbInfo
			// 
			this.lbInfo.Location = new System.Drawing.Point(79, 231);
			this.lbInfo.Name = "lbInfo";
			this.lbInfo.Size = new System.Drawing.Size(265, 23);
			this.lbInfo.TabIndex = 17;
			this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnIcon
			// 
			this.btnIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnIcon.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnIcon.Location = new System.Drawing.Point(122, 172);
			this.btnIcon.Name = "btnIcon";
			this.btnIcon.Size = new System.Drawing.Size(70, 25);
			this.btnIcon.TabIndex = 11;
			this.btnIcon.Text = "Icon";
			this.btnIcon.UseVisualStyleBackColor = true;
			this.btnIcon.Click += new System.EventHandler(this.btnIcon_Click);
			// 
			// btnFont
			// 
			this.btnFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFont.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnFont.Location = new System.Drawing.Point(46, 173);
			this.btnFont.Name = "btnFont";
			this.btnFont.Size = new System.Drawing.Size(70, 25);
			this.btnFont.TabIndex = 9;
			this.btnFont.Text = "Font";
			this.btnFont.UseVisualStyleBackColor = true;
			this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
			// 
			// btnScript
			// 
			this.btnScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnScript.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnScript.Location = new System.Drawing.Point(46, 203);
			this.btnScript.Name = "btnScript";
			this.btnScript.Size = new System.Drawing.Size(70, 25);
			this.btnScript.TabIndex = 10;
			this.btnScript.Text = "Script";
			this.btnScript.UseVisualStyleBackColor = true;
			this.btnScript.Click += new System.EventHandler(this.btnScript_Click);
			// 
			// btnContent
			// 
			this.btnContent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnContent.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnContent.Location = new System.Drawing.Point(122, 203);
			this.btnContent.Name = "btnContent";
			this.btnContent.Size = new System.Drawing.Size(70, 25);
			this.btnContent.TabIndex = 12;
			this.btnContent.Text = "Content";
			this.btnContent.UseVisualStyleBackColor = true;
			this.btnContent.Click += new System.EventHandler(this.btnContent_Click);
			// 
			// btnConnect
			// 
			this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnConnect.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnConnect.Location = new System.Drawing.Point(198, 172);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(70, 25);
			this.btnConnect.TabIndex = 13;
			this.btnConnect.Text = "Connect";
			this.btnConnect.UseVisualStyleBackColor = true;
			// 
			// tbText
			// 
			this.tbText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.tbText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbText.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.tbText.ForeColor = System.Drawing.Color.Gainsboro;
			this.tbText.Location = new System.Drawing.Point(79, 134);
			this.tbText.Name = "tbText";
			this.tbText.Size = new System.Drawing.Size(265, 29);
			this.tbText.TabIndex = 8;
			// 
			// tbDes
			// 
			this.tbDes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.tbDes.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbDes.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.tbDes.ForeColor = System.Drawing.Color.Gainsboro;
			this.tbDes.Location = new System.Drawing.Point(12, 71);
			this.tbDes.Name = "tbDes";
			this.tbDes.ReadOnly = true;
			this.tbDes.Size = new System.Drawing.Size(332, 22);
			this.tbDes.TabIndex = 3;
			this.tbDes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label4.ForeColor = System.Drawing.Color.Gainsboro;
			this.label4.Location = new System.Drawing.Point(21, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(0, 21);
			this.label4.TabIndex = 4;
			// 
			// btnOpenFile
			// 
			this.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOpenFile.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnOpenFile.Location = new System.Drawing.Point(198, 203);
			this.btnOpenFile.Name = "btnOpenFile";
			this.btnOpenFile.Size = new System.Drawing.Size(70, 25);
			this.btnOpenFile.TabIndex = 14;
			this.btnOpenFile.Text = "OpenFile";
			this.btnOpenFile.UseVisualStyleBackColor = true;
			this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
			// 
			// EditControlForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(367, 256);
			this.ControlBox = false;
			this.Controls.Add(this.btnOpenFile);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.tbDes);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.btnContent);
			this.Controls.Add(this.btnScript);
			this.Controls.Add(this.btnFont);
			this.Controls.Add(this.btnIcon);
			this.Controls.Add(this.lbInfo);
			this.Controls.Add(this.editControlComb1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.tbText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbName);
			this.Controls.Add(this.label1);
			this.ForeColor = System.Drawing.Color.Gainsboro;
			this.Name = "EditControlForm";
			this.Text = "EditControlForm";
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.tbName, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.tbText, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.btnOK, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.editControlComb1, 0);
			this.Controls.SetChildIndex(this.lbInfo, 0);
			this.Controls.SetChildIndex(this.btnIcon, 0);
			this.Controls.SetChildIndex(this.btnFont, 0);
			this.Controls.SetChildIndex(this.btnScript, 0);
			this.Controls.SetChildIndex(this.btnContent, 0);
			this.Controls.SetChildIndex(this.btnConnect, 0);
			this.Controls.SetChildIndex(this.tbDes, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.btnOpenFile, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Label label1;
		private TextBox tbName;
		private Label label2;
		private Button btnCancel;
		private Button btnOK;
		private Label label3;
		private EditControlComb editControlComb1;
		private Label lbInfo;
		private Button btnIcon;
		private Button btnFont;
		private Button btnScript;
		private Button btnContent;
		private Button btnConnect;
		private TextBox tbText;
		private TextBox tbDes;
		private Label label4;
		private Button btnOpenFile;
	}
}