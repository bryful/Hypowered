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
			HScriptCode hScriptCode1 = new HScriptCode();
			scriptEditor1 = new ScriptEditor();
			hButton1 = new HButton();
			propertyGrid1 = new PropertyGrid();
			SuspendLayout();
			// 
			// scriptEditor1
			// 
			scriptEditor1.EditMode = false;
			scriptEditor1.Location = new Point(130, 68);
			scriptEditor1.Name = "scriptEditor1";
			scriptEditor1.Size = new Size(496, 361);
			scriptEditor1.TabIndex = 1;
			scriptEditor1.Target = null;
			scriptEditor1.Text = "scriptEditor1";
			// 
			// hButton1
			// 
			hButton1.BackColor = Color.FromArgb(64, 64, 64);
			hButton1.CenterX = 74D;
			hButton1.CenterY = 154D;
			hButton1.Checked = false;
			hButton1.CheckedColor = Color.FromArgb(160, 160, 160);
			hButton1.DownColor = Color.FromArgb(180, 180, 180);
			hButton1.ForcusColor = Color.Blue;
			hButton1.ForeColor = Color.FromArgb(230, 230, 230);
			hButton1.GridSize = 2;
			hButton1.Index = -1;
			hButton1.IsAnti = false;
			hButton1.IsCheckMode = false;
			hButton1.IsShowForcus = true;
			hButton1.Location = new Point(24, 104);
			hButton1.Name = "hButton1";
			hButton1.ScriptCode = hScriptCode1;
			hButton1.Selected = false;
			hButton1.SelectedColor = Color.FromArgb(150, 100, 100);
			hButton1.Size = new Size(100, 100);
			hButton1.TabIndex = 2;
			hButton1.Text = "hButton1";
			hButton1.TextAlign = StringAlignment.Center;
			hButton1.TextLineAlign = StringAlignment.Center;
			// 
			// propertyGrid1
			// 
			propertyGrid1.Location = new Point(676, 82);
			propertyGrid1.Name = "propertyGrid1";
			propertyGrid1.SelectedObject = scriptEditor1;
			propertyGrid1.Size = new Size(220, 487);
			propertyGrid1.TabIndex = 3;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(908, 609);
			Controls.Add(propertyGrid1);
			Controls.Add(hButton1);
			Controls.Add(scriptEditor1);
			MainMenuVisible = true;
			Name = "Form1";
			SelectedArray = (new bool[] { false, false, false, false });
			Text = "Form1";
			ResumeLayout(false);
		}

		#endregion

		private ScriptEditor scriptEditor1;
		private HButton hButton1;
		private PropertyGrid propertyGrid1;
	}
}