namespace Hypowered
{
	partial class AddMenuDialog
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
			lb1 = new Label();
			tbName = new TextBox();
			tbText = new TextBox();
			lb2 = new Label();
			cmbSubMenu = new ComboBox();
			btnOK = new Button();
			btnCancel = new Button();
			cbAtSubmenu = new CheckBox();
			SuspendLayout();
			// 
			// lb1
			// 
			lb1.AutoSize = true;
			lb1.Location = new Point(35, 39);
			lb1.Name = "lb1";
			lb1.Size = new Size(38, 15);
			lb1.TabIndex = 0;
			lb1.Text = "Name";
			// 
			// tbName
			// 
			tbName.BackColor = Color.FromArgb(64, 64, 64);
			tbName.BorderStyle = BorderStyle.FixedSingle;
			tbName.ForeColor = Color.FromArgb(230, 230, 230);
			tbName.Location = new Point(88, 37);
			tbName.Name = "tbName";
			tbName.Size = new Size(134, 23);
			tbName.TabIndex = 1;
			// 
			// tbText
			// 
			tbText.BackColor = Color.FromArgb(64, 64, 64);
			tbText.BorderStyle = BorderStyle.FixedSingle;
			tbText.ForeColor = Color.FromArgb(230, 230, 230);
			tbText.Location = new Point(88, 66);
			tbText.Name = "tbText";
			tbText.Size = new Size(134, 23);
			tbText.TabIndex = 3;
			// 
			// lb2
			// 
			lb2.AutoSize = true;
			lb2.Location = new Point(35, 68);
			lb2.Name = "lb2";
			lb2.Size = new Size(28, 15);
			lb2.TabIndex = 2;
			lb2.Text = "Text";
			// 
			// cmbSubMenu
			// 
			cmbSubMenu.BackColor = Color.FromArgb(64, 64, 64);
			cmbSubMenu.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbSubMenu.FlatStyle = FlatStyle.Flat;
			cmbSubMenu.ForeColor = Color.FromArgb(230, 230, 230);
			cmbSubMenu.FormattingEnabled = true;
			cmbSubMenu.Location = new Point(88, 97);
			cmbSubMenu.Name = "cmbSubMenu";
			cmbSubMenu.Size = new Size(134, 23);
			cmbSubMenu.TabIndex = 5;
			// 
			// btnOK
			// 
			btnOK.FlatStyle = FlatStyle.Flat;
			btnOK.Location = new Point(147, 126);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(75, 23);
			btnOK.TabIndex = 6;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.FlatStyle = FlatStyle.Flat;
			btnCancel.Location = new Point(66, 126);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(75, 23);
			btnCancel.TabIndex = 7;
			btnCancel.Text = "Cendel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// cbAtSubmenu
			// 
			cbAtSubmenu.AutoSize = true;
			cbAtSubmenu.Location = new Point(35, 101);
			cbAtSubmenu.Name = "cbAtSubmenu";
			cbAtSubmenu.Size = new Size(46, 19);
			cbAtSubmenu.TabIndex = 8;
			cbAtSubmenu.Text = "Sub";
			cbAtSubmenu.UseVisualStyleBackColor = true;
			// 
			// AddMenuDialog
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(255, 168);
			CloseAction = CloseAction.DRCancel;
			Controls.Add(cbAtSubmenu);
			Controls.Add(btnCancel);
			Controls.Add(btnOK);
			Controls.Add(cmbSubMenu);
			Controls.Add(tbText);
			Controls.Add(lb2);
			Controls.Add(tbName);
			Controls.Add(lb1);
			IsShowTopMost = false;
			Name = "AddMenuDialog";
			StartPosition = FormStartPosition.CenterParent;
			Text = "AddMenuDialog";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lb1;
		private TextBox tbName;
		private TextBox tbText;
		private Label lb2;
		private ComboBox cmbSubMenu;
		private Button btnOK;
		private Button btnCancel;
		private CheckBox cbAtSubmenu;
	}
}