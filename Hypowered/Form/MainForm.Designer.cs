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
			formMenu = new ToolStripMenuItem();
			openFormMenu = new ToolStripMenuItem();
			newFormMenu = new ToolStripMenuItem();
			renameFormMenu = new ToolStripMenuItem();
			dupulicateFormToolStripMenuItem = new ToolStripMenuItem();
			scriptFormToolStripMenuItem = new ToolStripMenuItem();
			closeFormMenu = new ToolStripMenuItem();
			toolStripMenuItem1 = new ToolStripSeparator();
			quitMenu = new ToolStripMenuItem();
			controlMenu = new ToolStripMenuItem();
			newControlToolStripMenuItem = new ToolStripMenuItem();
			deleteControlMenu = new ToolStripMenuItem();
			toolStripMenuItem2 = new ToolStripSeparator();
			controlUpMenu = new ToolStripMenuItem();
			downControlMenu = new ToolStripMenuItem();
			topControlMenu = new ToolStripMenuItem();
			bottomControlMenu = new ToolStripMenuItem();
			toolStripMenuItem3 = new ToolStripSeparator();
			editControlMenu = new ToolStripMenuItem();
			scriptControlMenu = new ToolStripMenuItem();
			windowToolStripMenuItem = new ToolStripMenuItem();
			scriptEditorMenu = new ToolStripMenuItem();
			consoleMenu = new ToolStripMenuItem();
			editControl1 = new EditControl();
			splitContainer1 = new SplitContainer();
			toolStripMenuItem4 = new ToolStripSeparator();
			toolStripMenuItem5 = new ToolStripSeparator();
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
			propertyGrid1.Size = new Size(282, 768);
			propertyGrid1.TabIndex = 1;
			propertyGrid1.ViewBackColor = Color.FromArgb(32, 32, 32);
			propertyGrid1.ViewBorderColor = Color.FromArgb(150, 150, 150);
			propertyGrid1.ViewForeColor = Color.FromArgb(230, 230, 230);
			// 
			// menuStrip1
			// 
			menuStrip1.AutoSize = false;
			menuStrip1.Dock = DockStyle.None;
			menuStrip1.Items.AddRange(new ToolStripItem[] { formMenu, controlMenu, windowToolStripMenuItem });
			menuStrip1.Location = new Point(-2, 20);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new Size(487, 24);
			menuStrip1.TabIndex = 4;
			menuStrip1.Text = "menuStrip1";
			// 
			// formMenu
			// 
			formMenu.DropDownItems.AddRange(new ToolStripItem[] { openFormMenu, newFormMenu, toolStripMenuItem4, renameFormMenu, dupulicateFormToolStripMenuItem, closeFormMenu, toolStripMenuItem1, scriptFormToolStripMenuItem, quitMenu, toolStripMenuItem5 });
			formMenu.Name = "formMenu";
			formMenu.Size = new Size(46, 20);
			formMenu.Text = "Form";
			// 
			// openFormMenu
			// 
			openFormMenu.Name = "openFormMenu";
			openFormMenu.Size = new Size(158, 22);
			openFormMenu.Text = "OpenForm";
			// 
			// newFormMenu
			// 
			newFormMenu.Name = "newFormMenu";
			newFormMenu.Size = new Size(158, 22);
			newFormMenu.Text = "NewForm";
			// 
			// renameFormMenu
			// 
			renameFormMenu.Name = "renameFormMenu";
			renameFormMenu.Size = new Size(158, 22);
			renameFormMenu.Text = "RenameForm";
			// 
			// dupulicateFormToolStripMenuItem
			// 
			dupulicateFormToolStripMenuItem.Name = "dupulicateFormToolStripMenuItem";
			dupulicateFormToolStripMenuItem.Size = new Size(158, 22);
			dupulicateFormToolStripMenuItem.Text = "DupulicateForm";
			// 
			// scriptFormToolStripMenuItem
			// 
			scriptFormToolStripMenuItem.Name = "scriptFormToolStripMenuItem";
			scriptFormToolStripMenuItem.Size = new Size(158, 22);
			scriptFormToolStripMenuItem.Text = "ScriptForm";
			// 
			// closeFormMenu
			// 
			closeFormMenu.Name = "closeFormMenu";
			closeFormMenu.Size = new Size(158, 22);
			closeFormMenu.Text = "CloseForm";
			// 
			// toolStripMenuItem1
			// 
			toolStripMenuItem1.Name = "toolStripMenuItem1";
			toolStripMenuItem1.Size = new Size(155, 6);
			// 
			// quitMenu
			// 
			quitMenu.Name = "quitMenu";
			quitMenu.Size = new Size(158, 22);
			quitMenu.Text = "Quit";
			// 
			// controlMenu
			// 
			controlMenu.DropDownItems.AddRange(new ToolStripItem[] { newControlToolStripMenuItem, deleteControlMenu, toolStripMenuItem2, controlUpMenu, downControlMenu, topControlMenu, bottomControlMenu, toolStripMenuItem3, editControlMenu, scriptControlMenu });
			controlMenu.Name = "controlMenu";
			controlMenu.Size = new Size(58, 20);
			controlMenu.Text = "Control";
			// 
			// newControlToolStripMenuItem
			// 
			newControlToolStripMenuItem.Name = "newControlToolStripMenuItem";
			newControlToolStripMenuItem.Size = new Size(180, 22);
			newControlToolStripMenuItem.Text = "NewControl";
			// 
			// deleteControlMenu
			// 
			deleteControlMenu.Name = "deleteControlMenu";
			deleteControlMenu.Size = new Size(180, 22);
			deleteControlMenu.Text = "DeleteControl";
			// 
			// toolStripMenuItem2
			// 
			toolStripMenuItem2.Name = "toolStripMenuItem2";
			toolStripMenuItem2.Size = new Size(177, 6);
			// 
			// controlUpMenu
			// 
			controlUpMenu.Name = "controlUpMenu";
			controlUpMenu.Size = new Size(180, 22);
			controlUpMenu.Text = "UpControl";
			// 
			// downControlMenu
			// 
			downControlMenu.Name = "downControlMenu";
			downControlMenu.Size = new Size(180, 22);
			downControlMenu.Text = "DownControl";
			// 
			// topControlMenu
			// 
			topControlMenu.Name = "topControlMenu";
			topControlMenu.Size = new Size(180, 22);
			topControlMenu.Text = "TopControl";
			// 
			// bottomControlMenu
			// 
			bottomControlMenu.Name = "bottomControlMenu";
			bottomControlMenu.Size = new Size(180, 22);
			bottomControlMenu.Text = "BottomControl";
			// 
			// toolStripMenuItem3
			// 
			toolStripMenuItem3.Name = "toolStripMenuItem3";
			toolStripMenuItem3.Size = new Size(177, 6);
			// 
			// editControlMenu
			// 
			editControlMenu.Name = "editControlMenu";
			editControlMenu.Size = new Size(180, 22);
			editControlMenu.Text = "EditControl";
			// 
			// scriptControlMenu
			// 
			scriptControlMenu.Name = "scriptControlMenu";
			scriptControlMenu.Size = new Size(180, 22);
			scriptControlMenu.Text = "ScriptControl";
			// 
			// windowToolStripMenuItem
			// 
			windowToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { scriptEditorMenu, consoleMenu });
			windowToolStripMenuItem.Name = "windowToolStripMenuItem";
			windowToolStripMenuItem.Size = new Size(63, 20);
			windowToolStripMenuItem.Text = "Window";
			// 
			// scriptEditorMenu
			// 
			scriptEditorMenu.Name = "scriptEditorMenu";
			scriptEditorMenu.Size = new Size(180, 22);
			scriptEditorMenu.Text = "ScriptEditor";
			// 
			// consoleMenu
			// 
			consoleMenu.Name = "consoleMenu";
			consoleMenu.Size = new Size(180, 22);
			consoleMenu.Text = "Console";
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
			editControl1.Size = new Size(188, 768);
			editControl1.TabIndex = 6;
			editControl1.Text = "editControl1";
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
			splitContainer1.Size = new Size(474, 768);
			splitContainer1.SplitterDistance = 188;
			splitContainer1.TabIndex = 7;
			// 
			// toolStripMenuItem4
			// 
			toolStripMenuItem4.Name = "toolStripMenuItem4";
			toolStripMenuItem4.Size = new Size(155, 6);
			// 
			// toolStripMenuItem5
			// 
			toolStripMenuItem5.Name = "toolStripMenuItem5";
			toolStripMenuItem5.Size = new Size(155, 6);
			// 
			// MainForm
			// 
			AllowDrop = true;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(64, 64, 64);
			ClientSize = new Size(494, 859);
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
		private ToolStripMenuItem formMenu;
		private ToolStripMenuItem controlMenu;
		private ToolStripMenuItem newFormMenu;
		private ToolStripMenuItem quitMenu;
		private EditControl editControl1;
		private SplitContainer splitContainer1;
		private ToolStripMenuItem openFormMenu;
		private ToolStripMenuItem renameFormMenu;
		private ToolStripMenuItem dupulicateFormToolStripMenuItem;
		private ToolStripMenuItem closeFormMenu;
		private ToolStripSeparator toolStripMenuItem1;
		private ToolStripMenuItem scriptFormToolStripMenuItem;
		private ToolStripMenuItem newControlToolStripMenuItem;
		private ToolStripSeparator toolStripMenuItem2;
		private ToolStripMenuItem controlUpMenu;
		private ToolStripMenuItem downControlMenu;
		private ToolStripMenuItem topControlMenu;
		private ToolStripMenuItem bottomControlMenu;
		private ToolStripSeparator toolStripMenuItem3;
		private ToolStripMenuItem editControlMenu;
		private ToolStripMenuItem scriptControlMenu;
		private ToolStripMenuItem windowToolStripMenuItem;
		private ToolStripMenuItem scriptEditorMenu;
		private ToolStripMenuItem consoleMenu;
		private ToolStripMenuItem deleteControlMenu;
		private ToolStripSeparator toolStripMenuItem4;
		private ToolStripSeparator toolStripMenuItem5;
	}
}