namespace Hypowered
{
	partial class PropertyForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyForm));
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnMainForm = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
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
			this.propertyGrid1.Size = new System.Drawing.Size(327, 467);
			this.propertyGrid1.TabIndex = 0;
			this.propertyGrid1.ViewBackColor = System.Drawing.Color.Black;
			this.propertyGrid1.ViewBorderColor = System.Drawing.Color.Gray;
			this.propertyGrid1.ViewForeColor = System.Drawing.Color.White;
			// 
			// toolStrip1
			// 
			this.toolStrip1.BackColor = System.Drawing.Color.Black;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.btnMainForm,
            this.toolStripSeparator2});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.toolStrip1.Size = new System.Drawing.Size(327, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton1.ForeColor = System.Drawing.Color.LightGray;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(36, 22);
			this.toolStripButton1.Text = "Hide";
			this.toolStripButton1.Click += new System.EventHandler(this.ToolStripButton1_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// btnMainForm
			// 
			this.btnMainForm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnMainForm.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnMainForm.Image = ((System.Drawing.Image)(resources.GetObject("btnMainForm.Image")));
			this.btnMainForm.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnMainForm.Name = "btnMainForm";
			this.btnMainForm.Size = new System.Drawing.Size(71, 22);
			this.btnMainForm.Text = "FormActive";
			this.btnMainForm.Click += new System.EventHandler(this.BtnMainForm_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// PropertyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(327, 467);
			this.ControlBox = false;
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.propertyGrid1);
			this.MaximizeBox = false;
			this.MdiChildrenMinimizedAnchorBottom = false;
			this.MinimizeBox = false;
			this.Name = "PropertyForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
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
		private ToolStripButton toolStripButton1;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripButton btnMainForm;
		private ToolStripSeparator toolStripSeparator2;
	}
}