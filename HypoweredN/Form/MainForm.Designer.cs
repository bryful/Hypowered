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
			menuStrip1 = new MenuStrip();
			fileToolStripMenuItem = new ToolStripMenuItem();
			newFormToolStripMenuItem = new ToolStripMenuItem();
			quitToolStripMenuItem = new ToolStripMenuItem();
			editToolStripMenuItem = new ToolStripMenuItem();
			formToolStripMenuItem = new ToolStripMenuItem();
			editControl1 = new EditControl();
			splitContainer1 = new SplitContainer();
			menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			SuspendLayout();
			// 
			// propertyGrid1
			// 
			propertyGrid1.CategoryForeColor = Color.Gainsboro;
			propertyGrid1.CategorySplitterColor = Color.FromArgb(80, 80, 80);
			propertyGrid1.DisabledItemForeColor = Color.Gray;
			propertyGrid1.Dock = DockStyle.Fill;
			propertyGrid1.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			propertyGrid1.HelpBackColor = Color.FromArgb(64, 64, 64);
			propertyGrid1.HelpForeColor = Color.FromArgb(230, 230, 230);
			propertyGrid1.LineColor = SystemColors.WindowFrame;
			propertyGrid1.Location = new Point(0, 0);
			propertyGrid1.Name = "propertyGrid1";
			propertyGrid1.SelectedItemWithFocusBackColor = SystemColors.ActiveBorder;
			propertyGrid1.SelectedObject = this;
			propertyGrid1.Size = new Size(341, 484);
			propertyGrid1.TabIndex = 1;
			propertyGrid1.ViewBackColor = Color.FromArgb(32, 32, 32);
			propertyGrid1.ViewBorderColor = Color.FromArgb(150, 150, 150);
			propertyGrid1.ViewForeColor = Color.FromArgb(230, 230, 230);
			// 
			// menuStrip1
			// 
			menuStrip1.AutoSize = false;
			menuStrip1.Dock = DockStyle.None;
			menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, formToolStripMenuItem });
			menuStrip1.Location = new Point(-2, 20);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new Size(561, 24);
			menuStrip1.TabIndex = 4;
			menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newFormToolStripMenuItem, quitToolStripMenuItem });
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new Size(37, 20);
			fileToolStripMenuItem.Text = "File";
			// 
			// newFormToolStripMenuItem
			// 
			newFormToolStripMenuItem.Name = "newFormToolStripMenuItem";
			newFormToolStripMenuItem.Size = new Size(125, 22);
			newFormToolStripMenuItem.Text = "NewForm";
			newFormToolStripMenuItem.Click += newFormToolStripMenuItem_Click;
			// 
			// quitToolStripMenuItem
			// 
			quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			quitToolStripMenuItem.Size = new Size(125, 22);
			quitToolStripMenuItem.Text = "Quit";
			quitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
			// 
			// editToolStripMenuItem
			// 
			editToolStripMenuItem.Name = "editToolStripMenuItem";
			editToolStripMenuItem.Size = new Size(39, 20);
			editToolStripMenuItem.Text = "Edit";
			// 
			// formToolStripMenuItem
			// 
			formToolStripMenuItem.Name = "formToolStripMenuItem";
			formToolStripMenuItem.Size = new Size(46, 20);
			formToolStripMenuItem.Text = "Form";
			// 
			// editControl1
			// 
			editControl1.BackColor = Color.FromArgb(64, 64, 64);
			editControl1.Dock = DockStyle.Fill;
			editControl1.ForeColor = Color.FromArgb(230, 230, 231);
			editControl1.FormListHeight = 107;
			editControl1.Location = new Point(0, 0);
			editControl1.Name = "editControl1";
			editControl1.PropertyGrid = propertyGrid1;
			editControl1.Size = new Size(202, 484);
			editControl1.TabIndex = 6;
			editControl1.Text = "editControl1";
			editControl1.Click += editControl1_Click;
			// 
			// splitContainer1
			// 
			splitContainer1.Location = new Point(8, 47);
			splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			splitContainer1.Panel1.Controls.Add(editControl1);
			// 
			// splitContainer1.Panel2
			// 
			splitContainer1.Panel2.Controls.Add(propertyGrid1);
			splitContainer1.Size = new Size(547, 484);
			splitContainer1.SplitterDistance = 202;
			splitContainer1.TabIndex = 7;
			// 
			// MainForm
			// 
			AllowDrop = true;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(64, 64, 64);
			ClientSize = new Size(558, 535);
			CloseAction = CloseAction.Hide;
			Controls.Add(splitContainer1);
			Controls.Add(menuStrip1);
			MainMenuStrip = menuStrip1;
			Name = "MainForm";
			StartPosition = FormStartPosition.Manual;
			Text = "Hypowered Main";
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private PropertyGrid propertyGrid1;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem editToolStripMenuItem;
		private ToolStripMenuItem formToolStripMenuItem;
		private ToolStripMenuItem newFormToolStripMenuItem;
		private ToolStripMenuItem quitToolStripMenuItem;
		private EditControl editControl1;
		private SplitContainer splitContainer1;
	}
}