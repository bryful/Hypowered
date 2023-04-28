namespace Hypowered
{
	partial class Form1
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
			scriptEditor1 = new ScriptEditor();
			editControl1 = new EditControl();
			SuspendLayout();
			// 
			// scriptEditor1
			// 
			scriptEditor1.EditorFont = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
			scriptEditor1.GlobalMode = false;
			scriptEditor1.Location = new Point(12, 65);
			scriptEditor1.Name = "scriptEditor1";
			scriptEditor1.ScriptMode = false;
			scriptEditor1.Size = new Size(524, 515);
			scriptEditor1.TabIndex = 1;
			scriptEditor1.Target = null;
			scriptEditor1.Text = "scriptEditor1";
			// 
			// editControl1
			// 
			editControl1.BackColor = Color.FromArgb(64, 64, 64);
			editControl1.ForeColor = Color.FromArgb(220, 220, 220);
			editControl1.IsScript = false;
			editControl1.Location = new Point(597, 111);
			editControl1.MainDistance = 90;
			editControl1.MainForm = null;
			editControl1.MenuDistance = 224;
			editControl1.Name = "editControl1";
			editControl1.Size = new Size(197, 320);
			editControl1.TabIndex = 2;
			editControl1.Text = "editControl1";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(908, 609);
			Controls.Add(editControl1);
			Controls.Add(scriptEditor1);
			MainMenuVisible = true;
			Name = "Form1";
			SelectedArray = (new bool[] { false, false, false });
			Text = "Form1";
			ResumeLayout(false);
		}

		#endregion

		private ScriptEditor scriptEditor1;
		private EditControl editControl1;
	}
}