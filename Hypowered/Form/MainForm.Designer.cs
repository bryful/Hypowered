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
			toolStripMenuItem4 = new ToolStripSeparator();
			renameFormMenu = new ToolStripMenuItem();
			dupulicateFormToolStripMenuItem = new ToolStripMenuItem();
			closeFormMenu = new ToolStripMenuItem();
			toolStripMenuItem1 = new ToolStripSeparator();
			scriptFormToolStripMenuItem = new ToolStripMenuItem();
			quitMenu = new ToolStripMenuItem();
			toolStripMenuItem5 = new ToolStripSeparator();
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
			splitLeft = new SplitContainer();
			editHypowerd1 = new EditHypowerd();
			menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitLeft).BeginInit();
			splitLeft.Panel1.SuspendLayout();
			splitLeft.Panel2.SuspendLayout();
			splitLeft.SuspendLayout();
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
			propertyGrid1.Size = new Size(300, 553);
			propertyGrid1.TabIndex = 1;
			propertyGrid1.ViewBackColor = Color.FromArgb(32, 32, 32);
			propertyGrid1.ViewBorderColor = Color.FromArgb(150, 150, 150);
			propertyGrid1.ViewForeColor = Color.FromArgb(230, 230, 230);
			// 
			// menuStrip1
			// 
			menuStrip1.AutoSize = false;
			menuStrip1.Dock = DockStyle.None;
			menuStrip1.Items.AddRange(new ToolStripItem[] { formMenu, controlMenu });
			menuStrip1.Location = new Point(8, 20);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new Size(862, 24);
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
			// toolStripMenuItem4
			// 
			toolStripMenuItem4.Name = "toolStripMenuItem4";
			toolStripMenuItem4.Size = new Size(155, 6);
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
			// scriptFormToolStripMenuItem
			// 
			scriptFormToolStripMenuItem.Name = "scriptFormToolStripMenuItem";
			scriptFormToolStripMenuItem.Size = new Size(158, 22);
			scriptFormToolStripMenuItem.Text = "ScriptForm";
			// 
			// quitMenu
			// 
			quitMenu.Name = "quitMenu";
			quitMenu.Size = new Size(158, 22);
			quitMenu.Text = "Quit";
			// 
			// toolStripMenuItem5
			// 
			toolStripMenuItem5.Name = "toolStripMenuItem5";
			toolStripMenuItem5.Size = new Size(155, 6);
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
			newControlToolStripMenuItem.Size = new Size(152, 22);
			newControlToolStripMenuItem.Text = "NewControl";
			// 
			// deleteControlMenu
			// 
			deleteControlMenu.Name = "deleteControlMenu";
			deleteControlMenu.Size = new Size(152, 22);
			deleteControlMenu.Text = "DeleteControl";
			// 
			// toolStripMenuItem2
			// 
			toolStripMenuItem2.Name = "toolStripMenuItem2";
			toolStripMenuItem2.Size = new Size(149, 6);
			// 
			// controlUpMenu
			// 
			controlUpMenu.Name = "controlUpMenu";
			controlUpMenu.Size = new Size(152, 22);
			controlUpMenu.Text = "UpControl";
			// 
			// downControlMenu
			// 
			downControlMenu.Name = "downControlMenu";
			downControlMenu.Size = new Size(152, 22);
			downControlMenu.Text = "DownControl";
			// 
			// topControlMenu
			// 
			topControlMenu.Name = "topControlMenu";
			topControlMenu.Size = new Size(152, 22);
			topControlMenu.Text = "TopControl";
			// 
			// bottomControlMenu
			// 
			bottomControlMenu.Name = "bottomControlMenu";
			bottomControlMenu.Size = new Size(152, 22);
			bottomControlMenu.Text = "BottomControl";
			// 
			// toolStripMenuItem3
			// 
			toolStripMenuItem3.Name = "toolStripMenuItem3";
			toolStripMenuItem3.Size = new Size(149, 6);
			// 
			// editControlMenu
			// 
			editControlMenu.Name = "editControlMenu";
			editControlMenu.Size = new Size(152, 22);
			editControlMenu.Text = "EditControl";
			// 
			// scriptControlMenu
			// 
			scriptControlMenu.Name = "scriptControlMenu";
			scriptControlMenu.Size = new Size(152, 22);
			scriptControlMenu.Text = "ScriptControl";
			// 
			// splitLeft
			// 
			splitLeft.Location = new Point(12, 47);
			splitLeft.Name = "splitLeft";
			// 
			// splitLeft.Panel1
			// 
			splitLeft.Panel1.Controls.Add(editHypowerd1);
			// 
			// splitLeft.Panel2
			// 
			splitLeft.Panel2.Controls.Add(propertyGrid1);
			splitLeft.Size = new Size(475, 553);
			splitLeft.SplitterDistance = 171;
			splitLeft.TabIndex = 0;
			// 
			// editHypowerd1
			// 
			editHypowerd1.BackColor = Color.FromArgb(64, 64, 64);
			editHypowerd1.Dock = DockStyle.Fill;
			editHypowerd1.ForeColor = Color.FromArgb(230, 230, 230);
			editHypowerd1.Location = new Point(0, 0);
			editHypowerd1.MainForm = null;
			editHypowerd1.Name = "editHypowerd1";
			editHypowerd1.Size = new Size(171, 553);
			editHypowerd1.TabIndex = 2;
			editHypowerd1.Text = "editHypowerd1";
			// 
			// MainForm
			// 
			AllowDrop = true;
			AutoScaleMode = AutoScaleMode.None;
			BackColor = Color.FromArgb(64, 64, 64);
			ClientSize = new Size(491, 601);
			CloseAction = CloseAction.Hide;
			Controls.Add(splitLeft);
			Controls.Add(menuStrip1);
			MainMenuStrip = menuStrip1;
			Name = "MainForm";
			Text = "Hypowered Main";
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			splitLeft.Panel1.ResumeLayout(false);
			splitLeft.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitLeft).EndInit();
			splitLeft.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private PropertyGrid propertyGrid1;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem formMenu;
		private ToolStripMenuItem controlMenu;
		private ToolStripMenuItem newFormMenu;
		private ToolStripMenuItem quitMenu;
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
		private ToolStripMenuItem deleteControlMenu;
		private ToolStripSeparator toolStripMenuItem4;
		private ToolStripSeparator toolStripMenuItem5;
		private SplitContainer splitLeft;
		private EditHypowerd editHypowerd1;
	}
}