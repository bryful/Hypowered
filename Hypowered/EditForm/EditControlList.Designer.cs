namespace Hypowered
{
	partial class EditControlList
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
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.controlPanel1 = new Hypowered.ControlPanel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
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
			this.propertyGrid1.Size = new System.Drawing.Size(221, 469);
			this.propertyGrid1.TabIndex = 6;
			this.propertyGrid1.ViewBackColor = System.Drawing.Color.Black;
			this.propertyGrid1.ViewBorderColor = System.Drawing.Color.Gray;
			this.propertyGrid1.ViewForeColor = System.Drawing.Color.White;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(12, 31);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.controlPanel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
			this.splitContainer1.Size = new System.Drawing.Size(428, 469);
			this.splitContainer1.SplitterDistance = 203;
			this.splitContainer1.TabIndex = 7;
			// 
			// controlPanel1
			// 
			this.controlPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
			this.controlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlPanel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.controlPanel1.Location = new System.Drawing.Point(0, 0);
			this.controlPanel1.Name = "controlPanel1";
			this.controlPanel1.PropertyGrid = this.propertyGrid1;
			this.controlPanel1.Size = new System.Drawing.Size(203, 469);
			this.controlPanel1.TabIndex = 0;
			this.controlPanel1.Text = "controlPanel1";
			// 
			// EditControlList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.ClientSize = new System.Drawing.Size(452, 512);
			this.ControlBox = false;
			this.Controls.Add(this.splitContainer1);
			this.ForeColor = System.Drawing.Color.Gainsboro;
			this.Name = "EditControlList";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "EditControlList";
			this.Controls.SetChildIndex(this.splitContainer1, 0);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private PropertyGrid propertyGrid1;
		private SplitContainer splitContainer1;
		private ControlPanel controlPanel1;
	}
}