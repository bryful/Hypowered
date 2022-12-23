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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditControlForm));
			this.label1 = new System.Windows.Forms.Label();
			this.tbName = new System.Windows.Forms.TextBox();
			this.tbText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnScript = new System.Windows.Forms.Button();
			this.btnFont = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.editControlComb1 = new Hypowered.EditControlComb();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.lbInfo = new System.Windows.Forms.Label();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.ForeColor = System.Drawing.Color.Gainsboro;
			this.label1.Location = new System.Drawing.Point(33, 74);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 21);
			this.label1.TabIndex = 3;
			this.label1.Text = "Name";
			// 
			// tbName
			// 
			this.tbName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbName.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.tbName.ForeColor = System.Drawing.Color.Gainsboro;
			this.tbName.Location = new System.Drawing.Point(91, 71);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(206, 29);
			this.tbName.TabIndex = 4;
			// 
			// tbText
			// 
			this.tbText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.tbText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbText.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.tbText.ForeColor = System.Drawing.Color.Gainsboro;
			this.tbText.Location = new System.Drawing.Point(91, 106);
			this.tbText.Name = "tbText";
			this.tbText.Size = new System.Drawing.Size(206, 29);
			this.tbText.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.ForeColor = System.Drawing.Color.Gainsboro;
			this.label2.Location = new System.Drawing.Point(49, 109);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(36, 21);
			this.label2.TabIndex = 5;
			this.label2.Text = "Text";
			// 
			// btnScript
			// 
			this.btnScript.Enabled = false;
			this.btnScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnScript.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnScript.Location = new System.Drawing.Point(31, 165);
			this.btnScript.Name = "btnScript";
			this.btnScript.Size = new System.Drawing.Size(55, 40);
			this.btnScript.TabIndex = 7;
			this.btnScript.Text = "Script";
			this.btnScript.UseVisualStyleBackColor = true;
			// 
			// btnFont
			// 
			this.btnFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFont.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnFont.Location = new System.Drawing.Point(97, 165);
			this.btnFont.Name = "btnFont";
			this.btnFont.Size = new System.Drawing.Size(55, 40);
			this.btnFont.TabIndex = 8;
			this.btnFont.Text = "Font";
			this.btnFont.UseVisualStyleBackColor = true;
			this.btnFont.Click += new System.EventHandler(this.BtnFont_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnCancel.Location = new System.Drawing.Point(191, 165);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(55, 40);
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Enabled = false;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOK.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnOK.Location = new System.Drawing.Point(252, 165);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(55, 40);
			this.btnOK.TabIndex = 10;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label3.ForeColor = System.Drawing.Color.Gainsboro;
			this.label3.Location = new System.Drawing.Point(23, 41);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(62, 21);
			this.label3.TabIndex = 1;
			this.label3.Text = "Control";
			// 
			// editControlComb1
			// 
			this.editControlComb1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
			this.editControlComb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.editControlComb1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.editControlComb1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.editControlComb1.FormattingEnabled = true;
			this.editControlComb1.Location = new System.Drawing.Point(91, 39);
			this.editControlComb1.Name = "editControlComb1";
			this.editControlComb1.Size = new System.Drawing.Size(206, 23);
			this.editControlComb1.TabIndex = 2;
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton1.ForeColor = System.Drawing.Color.DarkGray;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(35, 22);
			this.toolStripButton1.Text = "New";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(335, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// lbInfo
			// 
			this.lbInfo.Location = new System.Drawing.Point(97, 138);
			this.lbInfo.Name = "lbInfo";
			this.lbInfo.Size = new System.Drawing.Size(200, 24);
			this.lbInfo.TabIndex = 11;
			this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// EditControlForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(335, 217);
			this.ControlBox = false;
			this.Controls.Add(this.lbInfo);
			this.Controls.Add(this.editControlComb1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnFont);
			this.Controls.Add(this.btnScript);
			this.Controls.Add(this.tbText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.toolStrip1);
			this.ForeColor = System.Drawing.Color.Gainsboro;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "EditControlForm";
			this.Text = "EditControlForm";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Label label1;
		private TextBox tbName;
		private TextBox tbText;
		private Label label2;
		private Button btnScript;
		private Button btnFont;
		private Button btnCancel;
		private Button btnOK;
		private Label label3;
		private EditControlComb editControlComb1;
		private ToolStripButton toolStripButton1;
		private ToolStrip toolStrip1;
		private Label lbInfo;
	}
}