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
			editControl1 = new EditControl();
			roslynEdit1 = new RoslynEdit();
			splitMain = new SplitContainer();
			splitLeft = new SplitContainer();
			togglePanel1 = new TogglePanel();
			comboBox1 = new ComboBox();
			button2 = new Button();
			menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
			splitMain.Panel1.SuspendLayout();
			splitMain.Panel2.SuspendLayout();
			splitMain.SuspendLayout();
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
			propertyGrid1.Size = new Size(278, 516);
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
			menuStrip1.Size = new Size(1168, 24);
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
			// editControl1
			// 
			editControl1.BackColor = Color.FromArgb(64, 64, 64);
			editControl1.Dock = DockStyle.Fill;
			editControl1.ForeColor = Color.FromArgb(230, 230, 231);
			editControl1.HForm = null;
			editControl1.Location = new Point(0, 0);
			editControl1.MainDistance = 216;
			editControl1.MainForm = null;
			editControl1.MenuDistance = 366;
			editControl1.Name = "editControl1";
			editControl1.PropertyGrid = propertyGrid1;
			editControl1.Size = new Size(160, 516);
			editControl1.TabIndex = 6;
			editControl1.Text = "editControl1";
			// 
			// roslynEdit1
			// 
			roslynEdit1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			roslynEdit1.Location = new Point(3, 33);
			roslynEdit1.Name = "roslynEdit1";
			roslynEdit1.Size = new Size(411, 483);
			roslynEdit1.TabIndex = 8;
			// 
			// splitMain
			// 
			splitMain.Location = new Point(12, 47);
			splitMain.Name = "splitMain";
			// 
			// splitMain.Panel1
			// 
			splitMain.Panel1.Controls.Add(splitLeft);
			// 
			// splitMain.Panel2
			// 
			splitMain.Panel2.Controls.Add(togglePanel1);
			splitMain.Panel2.Controls.Add(comboBox1);
			splitMain.Panel2.Controls.Add(button2);
			splitMain.Panel2.Controls.Add(roslynEdit1);
			splitMain.Size = new Size(863, 516);
			splitMain.SplitterDistance = 442;
			splitMain.TabIndex = 7;
			// 
			// splitLeft
			// 
			splitLeft.Dock = DockStyle.Fill;
			splitLeft.Location = new Point(0, 0);
			splitLeft.Name = "splitLeft";
			// 
			// splitLeft.Panel1
			// 
			splitLeft.Panel1.Controls.Add(editControl1);
			// 
			// splitLeft.Panel2
			// 
			splitLeft.Panel2.Controls.Add(propertyGrid1);
			splitLeft.Size = new Size(442, 516);
			splitLeft.SplitterDistance = 160;
			splitLeft.TabIndex = 0;
			// 
			// togglePanel1
			// 
			togglePanel1.BackColor = Color.FromArgb(64, 64, 64);
			togglePanel1.BtnWidth = 50;
			togglePanel1.Count = 2;
			togglePanel1.ForeColor = Color.FromArgb(230, 230, 230);
			togglePanel1.Index = 1;
			togglePanel1.Location = new Point(3, 8);
			togglePanel1.MaximumSize = new Size(100, 20);
			togglePanel1.MinimumSize = new Size(100, 20);
			togglePanel1.Name = "togglePanel1";
			togglePanel1.Size = new Size(100, 20);
			togglePanel1.TabIndex = 13;
			togglePanel1.Text = "togglePanel1";
			togglePanel1.Texts = (new string[] { "Start", "End", "page3", "page4", "page5", "page6", "page7", "page8", "page9", "page10" });
			// 
			// comboBox1
			// 
			comboBox1.FormattingEnabled = true;
			comboBox1.Location = new Point(109, 5);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new Size(99, 23);
			comboBox1.TabIndex = 12;
			// 
			// button2
			// 
			button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			button2.FlatStyle = FlatStyle.Flat;
			button2.Location = new Point(322, 4);
			button2.Name = "button2";
			button2.Size = new Size(92, 23);
			button2.TabIndex = 10;
			button2.Text = "Execute";
			button2.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			AllowDrop = true;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(64, 64, 64);
			ClientSize = new Size(887, 575);
			CloseAction = CloseAction.Hide;
			Controls.Add(splitMain);
			Controls.Add(menuStrip1);
			MainMenuStrip = menuStrip1;
			Name = "MainForm";
			StartPosition = FormStartPosition.Manual;
			Text = "Hypowered Main";
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			splitMain.Panel1.ResumeLayout(false);
			splitMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
			splitMain.ResumeLayout(false);
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
		private EditControl editControl1;
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
		private RoslynEdit roslynEdit1;
		private SplitContainer splitMain;
		private SplitContainer splitLeft;
		private ComboBox comboBox1;
		private Button button2;
		private TogglePanel togglePanel1;
	}
}