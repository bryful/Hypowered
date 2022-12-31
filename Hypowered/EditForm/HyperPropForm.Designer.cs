namespace Hypowered
{
	partial class HyperPropForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HyperPropForm));
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.lbCaption = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.menuControl = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuTopMost = new System.Windows.Forms.ToolStripDropDownButton();
			this.btnTopMost = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnActive = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.btnHide = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.BackColor = System.Drawing.Color.DimGray;
			this.propertyGrid1.CanShowVisualStyleGlyphs = false;
			this.propertyGrid1.CategoryForeColor = System.Drawing.Color.LightGray;
			this.propertyGrid1.CommandsBorderColor = System.Drawing.SystemColors.ControlLight;
			this.propertyGrid1.CommandsDisabledLinkColor = System.Drawing.Color.DimGray;
			this.propertyGrid1.CommandsForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.propertyGrid1.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid1.HelpBackColor = System.Drawing.Color.Black;
			this.propertyGrid1.HelpForeColor = System.Drawing.Color.Silver;
			this.propertyGrid1.LineColor = System.Drawing.Color.DimGray;
			this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			this.propertyGrid1.SelectedItemWithFocusForeColor = System.Drawing.Color.White;
			this.propertyGrid1.SelectedObject = this;
			this.propertyGrid1.Size = new System.Drawing.Size(327, 581);
			this.propertyGrid1.TabIndex = 0;
			this.propertyGrid1.ViewBackColor = System.Drawing.Color.Black;
			this.propertyGrid1.ViewBorderColor = System.Drawing.Color.Gray;
			this.propertyGrid1.ViewForeColor = System.Drawing.Color.White;
			// 
			// toolStrip1
			// 
			this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbCaption,
            this.toolStripSeparator4,
            this.menuControl,
            this.toolStripSeparator1,
            this.menuTopMost,
            this.toolStripSeparator2,
            this.btnActive,
            this.toolStripSeparator3,
            this.btnHide});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(327, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// lbCaption
			// 
			this.lbCaption.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.lbCaption.ForeColor = System.Drawing.Color.Gainsboro;
			this.lbCaption.Name = "lbCaption";
			this.lbCaption.Size = new System.Drawing.Size(48, 22);
			this.lbCaption.Text = "Caption";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// menuControl
			// 
			this.menuControl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.menuControl.ForeColor = System.Drawing.Color.Gainsboro;
			this.menuControl.Image = ((System.Drawing.Image)(resources.GetObject("menuControl.Image")));
			this.menuControl.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.menuControl.Name = "menuControl";
			this.menuControl.Size = new System.Drawing.Size(59, 22);
			this.menuControl.Text = "Control";
			this.menuControl.Click += new System.EventHandler(this.menuControl_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// menuTopMost
			// 
			this.menuTopMost.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.menuTopMost.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnTopMost});
			this.menuTopMost.ForeColor = System.Drawing.Color.LightGray;
			this.menuTopMost.Image = ((System.Drawing.Image)(resources.GetObject("menuTopMost.Image")));
			this.menuTopMost.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.menuTopMost.Name = "menuTopMost";
			this.menuTopMost.Size = new System.Drawing.Size(72, 22);
			this.menuTopMost.Text = "TopMoset";
			// 
			// btnTopMost
			// 
			this.btnTopMost.Checked = true;
			this.btnTopMost.CheckState = System.Windows.Forms.CheckState.Checked;
			this.btnTopMost.Name = "btnTopMost";
			this.btnTopMost.Size = new System.Drawing.Size(120, 22);
			this.btnTopMost.Text = "TopMost";
			this.btnTopMost.Click += new System.EventHandler(this.btnTopMost_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// btnActive
			// 
			this.btnActive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnActive.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnActive.Image = ((System.Drawing.Image)(resources.GetObject("btnActive.Image")));
			this.btnActive.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnActive.Name = "btnActive";
			this.btnActive.Size = new System.Drawing.Size(44, 22);
			this.btnActive.Text = "Active";
			this.btnActive.Click += new System.EventHandler(this.btnActive_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// btnHide
			// 
			this.btnHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnHide.ForeColor = System.Drawing.Color.LightGray;
			this.btnHide.Image = ((System.Drawing.Image)(resources.GetObject("btnHide.Image")));
			this.btnHide.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnHide.Name = "btnHide";
			this.btnHide.Size = new System.Drawing.Size(36, 22);
			this.btnHide.Text = "Hide";
			this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
			// 
			// HyperPropForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(327, 581);
			this.ControlBox = false;
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.propertyGrid1);
			this.MaximizeBox = false;
			this.MdiChildrenMinimizedAnchorBottom = false;
			this.MinimizeBox = false;
			this.Name = "HyperPropForm";
			this.RightToLeftLayout = true;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "PropertyForm";
			this.TopMost = true;
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private PropertyGrid propertyGrid1;
		private ToolStrip toolStrip1;
		private ToolStripButton btnHide;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripButton btnActive;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripDropDownButton menuControl;
		private ToolStripLabel lbCaption;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripSeparator toolStripSeparator4;
		private ToolStripDropDownButton menuTopMost;
		private ToolStripMenuItem btnTopMost;
	}
}