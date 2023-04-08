namespace Hypowered
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			propertyGrid1 = new PropertyGrid();
			listBox1 = new ListBox();
			listBox2 = new ListBox();
			menuStrip1 = new MenuStrip();
			fileToolStripMenuItem = new ToolStripMenuItem();
			menuStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// propertyGrid1
			// 
			propertyGrid1.Location = new Point(169, 42);
			propertyGrid1.Name = "propertyGrid1";
			propertyGrid1.Size = new Size(338, 404);
			propertyGrid1.TabIndex = 1;
			// 
			// listBox1
			// 
			listBox1.FormattingEnabled = true;
			listBox1.ItemHeight = 15;
			listBox1.Location = new Point(12, 42);
			listBox1.Name = "listBox1";
			listBox1.Size = new Size(151, 124);
			listBox1.TabIndex = 2;
			// 
			// listBox2
			// 
			listBox2.FormattingEnabled = true;
			listBox2.ItemHeight = 15;
			listBox2.Location = new Point(12, 172);
			listBox2.Name = "listBox2";
			listBox2.Size = new Size(151, 274);
			listBox2.TabIndex = 3;
			// 
			// menuStrip1
			// 
			menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
			menuStrip1.Location = new Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new Size(519, 24);
			menuStrip1.TabIndex = 4;
			menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new Size(37, 20);
			fileToolStripMenuItem.Text = "File";
			// 
			// MainForm
			// 
			AllowDrop = true;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(519, 480);
			Controls.Add(listBox2);
			Controls.Add(listBox1);
			Controls.Add(propertyGrid1);
			Controls.Add(menuStrip1);
			FormBorderStyle = FormBorderStyle.SizableToolWindow;
			MainMenuStrip = menuStrip1;
			Name = "MainForm";
			RightToLeftLayout = true;
			StartPosition = FormStartPosition.Manual;
			Text = "Hypowered Main";
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private PropertyGrid propertyGrid1;
		private ListBox listBox1;
		private ListBox listBox2;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem fileToolStripMenuItem;
	}
}