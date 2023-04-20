namespace Hypowered
{
	partial class ScriptEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptEditor));
			roslynEdit1 = new RoslynEdit();
			toolStrip1 = new ToolStrip();
			cmbTarget = new ToolStripComboBox();
			btnEditStart = new ToolStripButton();
			lbName = new ToolStripLabel();
			cmbScriptType = new ToolStripComboBox();
			btnEditEnd = new ToolStripButton();
			toolStripSeparator1 = new ToolStripSeparator();
			btnFont = new ToolStripButton();
			toolStripSeparator2 = new ToolStripSeparator();
			btnV8Execute = new ToolStripButton();
			toolStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// roslynEdit1
			// 
			roslynEdit1.Anchor = AnchorStyles.None;
			roslynEdit1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			roslynEdit1.Location = new Point(9, 52);
			roslynEdit1.Name = "roslynEdit1";
			roslynEdit1.Size = new Size(749, 311);
			roslynEdit1.TabIndex = 1;
			// 
			// toolStrip1
			// 
			toolStrip1.Anchor = AnchorStyles.None;
			toolStrip1.AutoSize = false;
			toolStrip1.Dock = DockStyle.None;
			toolStrip1.Items.AddRange(new ToolStripItem[] { cmbTarget, btnEditStart, lbName, cmbScriptType, btnEditEnd, toolStripSeparator1, btnFont, toolStripSeparator2, btnV8Execute });
			toolStrip1.Location = new Point(9, 24);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new Size(749, 25);
			toolStrip1.TabIndex = 2;
			toolStrip1.Text = "toolStrip1";
			// 
			// cmbTarget
			// 
			cmbTarget.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbTarget.FlatStyle = FlatStyle.Flat;
			cmbTarget.MaxDropDownItems = 20;
			cmbTarget.Name = "cmbTarget";
			cmbTarget.Size = new Size(121, 25);
			// 
			// btnEditStart
			// 
			btnEditStart.AutoSize = false;
			btnEditStart.BackColor = SystemColors.ControlLight;
			btnEditStart.DisplayStyle = ToolStripItemDisplayStyle.Text;
			btnEditStart.Image = (Image)resources.GetObject("btnEditStart.Image");
			btnEditStart.ImageTransparentColor = Color.Magenta;
			btnEditStart.Name = "btnEditStart";
			btnEditStart.Size = new Size(100, 22);
			btnEditStart.Text = "EditScript";
			// 
			// lbName
			// 
			lbName.BackColor = SystemColors.ControlLight;
			lbName.ImageAlign = ContentAlignment.MiddleRight;
			lbName.Name = "lbName";
			lbName.Size = new Size(44, 22);
			lbName.Text = "(None)";
			// 
			// cmbScriptType
			// 
			cmbScriptType.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbScriptType.FlatStyle = FlatStyle.Flat;
			cmbScriptType.Name = "cmbScriptType";
			cmbScriptType.Size = new Size(121, 25);
			// 
			// btnEditEnd
			// 
			btnEditEnd.BackColor = SystemColors.ControlLight;
			btnEditEnd.DisplayStyle = ToolStripItemDisplayStyle.Text;
			btnEditEnd.Enabled = false;
			btnEditEnd.Image = (Image)resources.GetObject("btnEditEnd.Image");
			btnEditEnd.ImageTransparentColor = Color.Magenta;
			btnEditEnd.Name = "btnEditEnd";
			btnEditEnd.Size = new Size(51, 22);
			btnEditEnd.Text = "EditEnd";
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new Size(6, 25);
			// 
			// btnFont
			// 
			btnFont.DisplayStyle = ToolStripItemDisplayStyle.Text;
			btnFont.Image = (Image)resources.GetObject("btnFont.Image");
			btnFont.ImageTransparentColor = Color.Magenta;
			btnFont.Name = "btnFont";
			btnFont.Size = new Size(35, 22);
			btnFont.Text = "Font";
			// 
			// toolStripSeparator2
			// 
			toolStripSeparator2.Alignment = ToolStripItemAlignment.Right;
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new Size(6, 25);
			// 
			// btnV8Execute
			// 
			btnV8Execute.Alignment = ToolStripItemAlignment.Right;
			btnV8Execute.AutoSize = false;
			btnV8Execute.BackColor = SystemColors.ControlLight;
			btnV8Execute.DisplayStyle = ToolStripItemDisplayStyle.Text;
			btnV8Execute.Image = (Image)resources.GetObject("btnV8Execute.Image");
			btnV8Execute.ImageTransparentColor = Color.Magenta;
			btnV8Execute.Margin = new Padding(5, 1, 5, 2);
			btnV8Execute.Name = "btnV8Execute";
			btnV8Execute.Padding = new Padding(5, 0, 5, 0);
			btnV8Execute.Size = new Size(100, 22);
			btnV8Execute.Text = "Execute";
			// 
			// ScriptEditor
			// 
			AutoScaleMode = AutoScaleMode.None;
			ClientSize = new Size(762, 391);
			CloseAction = CloseAction.Hide;
			ControlBox = false;
			Controls.Add(toolStrip1);
			Controls.Add(roslynEdit1);
			MaximizeBox = false;
			MdiChildrenMinimizedAnchorBottom = false;
			MinimizeBox = false;
			Name = "ScriptEditor";
			ShowIcon = false;
			ShowInTaskbar = false;
			Text = "ScriptEditor";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private RoslynEdit roslynEdit1;
		private ToolStrip toolStrip1;
		private ToolStripButton btnEditStart;
		private ToolStripComboBox cmbScriptType;
		private ToolStripLabel lbName;
		private ToolStripButton btnFont;
		private ToolStripButton btnEditEnd;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripButton btnV8Execute;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripComboBox cmbTarget;
	}
}