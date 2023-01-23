

namespace Hpd
{
    partial class HpdPropertyForm
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
			Hpd.HpdScriptCode hpdScriptCode1 = new Hpd.HpdScriptCode();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.controlTree1 = new Hpd.HpdControlTree();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(270, 526);
			this.propertyGrid1.TabIndex = 0;
			// 
			// controlTree1
			// 
			this.controlTree1.Algnment = Hpd.HpdAlgnment.Near;
			this.controlTree1.CanColorCustum = false;
			this.controlTree1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlTree1.FileName = "";
			this.controlTree1.ForcusColor = System.Drawing.Color.White;
			this.controlTree1.Form = null;
			this.controlTree1.FrameWeight = new System.Windows.Forms.Padding(1);
			this.controlTree1.IsDrawFocuse = true;
			this.controlTree1.IsDrawFrame = true;
			this.controlTree1.IsSaveFileName = false;
			this.controlTree1.LineAlgnment = Hpd.HpdAlgnment.Center;
			this.controlTree1.Lines = new string[] {
        ""};
			this.controlTree1.Location = new System.Drawing.Point(0, 0);
			this.controlTree1.Name = "controlTree1";
			this.controlTree1.Orientation = Hpd.HpdOrientation.Row;
			this.controlTree1.ScriptCode = hpdScriptCode1;
			this.controlTree1.SelectedControl = null;
			this.controlTree1.SelectedNode = null;
			this.controlTree1.Size = new System.Drawing.Size(197, 526);
			this.controlTree1.TabIndex = 3;
			this.controlTree1.TextAligiment = System.Drawing.StringAlignment.Near;
			this.controlTree1.TextLineAligiment = System.Drawing.StringAlignment.Near;
			this.controlTree1.UnCheckedColor = System.Drawing.Color.White;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.controlTree1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
			this.splitContainer1.Size = new System.Drawing.Size(471, 526);
			this.splitContainer1.SplitterDistance = 197;
			this.splitContainer1.TabIndex = 4;
			// 
			// HpdPropertyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(471, 526);
			this.Controls.Add(this.splitContainer1);
			this.Name = "HpdPropertyForm";
			this.Text = "HpdPropertyForm";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private PropertyGrid propertyGrid1;
		private HpdControlTree controlTree1;
		private SplitContainer splitContainer1;
	}
}