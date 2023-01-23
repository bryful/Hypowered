

namespace Hpd
{
    partial class NewControlDialog
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
			Hpd.HpdScriptCode hpdScriptCode6 = new Hpd.HpdScriptCode();
			Hpd.HpdScriptCode hpdScriptCode1 = new Hpd.HpdScriptCode();
			Hpd.HpdScriptCode hpdScriptCode2 = new Hpd.HpdScriptCode();
			Hpd.HpdScriptCode hpdScriptCode3 = new Hpd.HpdScriptCode();
			Hpd.HpdScriptCode hpdScriptCode4 = new Hpd.HpdScriptCode();
			this.cmbControl = new Hpd.HpdComboBox();
			this.hpdButton1 = new Hpd.HpdButton();
			this.tbName = new Hpd.HpdTextBox();
			this.hpdButton2 = new Hpd.HpdButton();
			this.tbText = new Hpd.HpdTextBox();
			this.SuspendLayout();
			// 
			// cmbControl
			// 
			this.cmbControl.Algnment = Hpd.HpdAlgnment.Near;
			this.cmbControl.CanColorCustum = false;
			this.cmbControl.Caption = "Control";
			this.cmbControl.CaptionWidth = 65;
			this.cmbControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbControl.FileName = "";
			this.cmbControl.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cmbControl.ForcusColor = System.Drawing.Color.White;
			this.cmbControl.FrameWeight = new System.Windows.Forms.Padding(1);
			this.cmbControl.IsDrawFocuse = true;
			this.cmbControl.IsDrawFrame = true;
			this.cmbControl.IsSaveFileName = false;
			this.cmbControl.LineAlgnment = Hpd.HpdAlgnment.Far;
			this.cmbControl.Lines = new string[] {
        "hpdComboBox1"};
			this.cmbControl.Location = new System.Drawing.Point(40, 26);
			this.cmbControl.Name = "cmbControl";
			this.cmbControl.Orientation = Hpd.HpdOrientation.Row;
			this.cmbControl.ScriptCode = hpdScriptCode6;
			this.cmbControl.SelectedIndex = -1;
			this.cmbControl.SelectedItem = null;
			this.cmbControl.Size = new System.Drawing.Size(267, 26);
			this.cmbControl.TabIndex = 4;
			this.cmbControl.Text = "hpdComboBox1";
			this.cmbControl.TextAligiment = System.Drawing.StringAlignment.Near;
			this.cmbControl.TextLineAligiment = System.Drawing.StringAlignment.Center;
			this.cmbControl.UnCheckedColor = System.Drawing.Color.White;
			// 
			// hpdButton1
			// 
			this.hpdButton1.Algnment = Hpd.HpdAlgnment.Near;
			this.hpdButton1.CanColorCustum = false;
			this.hpdButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.hpdButton1.FileName = "";
			this.hpdButton1.ForcusColor = System.Drawing.Color.White;
			this.hpdButton1.FrameWeight = new System.Windows.Forms.Padding(1);
			this.hpdButton1.IsDrawFocuse = true;
			this.hpdButton1.IsDrawFrame = true;
			this.hpdButton1.IsSaveFileName = false;
			this.hpdButton1.LineAlgnment = Hpd.HpdAlgnment.Center;
			this.hpdButton1.Lines = new string[] {
        "OK"};
			this.hpdButton1.Location = new System.Drawing.Point(207, 128);
			this.hpdButton1.Name = "hpdButton1";
			this.hpdButton1.Orientation = Hpd.HpdOrientation.Row;
			this.hpdButton1.ScriptCode = hpdScriptCode1;
			this.hpdButton1.Size = new System.Drawing.Size(100, 27);
			this.hpdButton1.TabIndex = 5;
			this.hpdButton1.Text = "OK";
			this.hpdButton1.TextAligiment = System.Drawing.StringAlignment.Near;
			this.hpdButton1.TextLineAligiment = System.Drawing.StringAlignment.Near;
			this.hpdButton1.UnCheckedColor = System.Drawing.Color.White;
			this.hpdButton1.Click += new System.EventHandler(this.hpdButton1_Click);
			// 
			// tbName
			// 
			this.tbName.Algnment = Hpd.HpdAlgnment.Near;
			this.tbName.CanColorCustum = false;
			this.tbName.Caption = "Name";
			this.tbName.CaptionWidth = 65;
			this.tbName.FileName = "";
			this.tbName.ForcusColor = System.Drawing.Color.White;
			this.tbName.FrameWeight = new System.Windows.Forms.Padding(1);
			this.tbName.IsDrawFocuse = true;
			this.tbName.IsDrawFrame = true;
			this.tbName.IsSaveFileName = false;
			this.tbName.LineAlgnment = Hpd.HpdAlgnment.Center;
			this.tbName.Lines = new string[] {
        "Name"};
			this.tbName.Location = new System.Drawing.Point(40, 57);
			this.tbName.Multiline = false;
			this.tbName.Name = "tbName";
			this.tbName.Orientation = Hpd.HpdOrientation.Row;
			this.tbName.ScriptCode = hpdScriptCode2;
			this.tbName.Size = new System.Drawing.Size(267, 23);
			this.tbName.TabIndex = 6;
			this.tbName.TextAligiment = System.Drawing.StringAlignment.Near;
			this.tbName.TextLineAligiment = System.Drawing.StringAlignment.Near;
			this.tbName.UnCheckedColor = System.Drawing.Color.White;
			// 
			// hpdButton2
			// 
			this.hpdButton2.Algnment = Hpd.HpdAlgnment.Near;
			this.hpdButton2.CanColorCustum = false;
			this.hpdButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.hpdButton2.FileName = "";
			this.hpdButton2.ForcusColor = System.Drawing.Color.White;
			this.hpdButton2.FrameWeight = new System.Windows.Forms.Padding(1);
			this.hpdButton2.IsDrawFocuse = true;
			this.hpdButton2.IsDrawFrame = true;
			this.hpdButton2.IsSaveFileName = false;
			this.hpdButton2.LineAlgnment = Hpd.HpdAlgnment.Center;
			this.hpdButton2.Lines = new string[] {
        "Cancel"};
			this.hpdButton2.Location = new System.Drawing.Point(101, 128);
			this.hpdButton2.Name = "hpdButton2";
			this.hpdButton2.Orientation = Hpd.HpdOrientation.Row;
			this.hpdButton2.ScriptCode = hpdScriptCode3;
			this.hpdButton2.Size = new System.Drawing.Size(100, 27);
			this.hpdButton2.TabIndex = 7;
			this.hpdButton2.Text = "Cancel";
			this.hpdButton2.TextAligiment = System.Drawing.StringAlignment.Near;
			this.hpdButton2.TextLineAligiment = System.Drawing.StringAlignment.Near;
			this.hpdButton2.UnCheckedColor = System.Drawing.Color.White;
			this.hpdButton2.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// tbText
			// 
			this.tbText.Algnment = Hpd.HpdAlgnment.Near;
			this.tbText.CanColorCustum = false;
			this.tbText.Caption = "Text";
			this.tbText.CaptionWidth = 65;
			this.tbText.FileName = "";
			this.tbText.ForcusColor = System.Drawing.Color.White;
			this.tbText.FrameWeight = new System.Windows.Forms.Padding(1);
			this.tbText.IsDrawFocuse = true;
			this.tbText.IsDrawFrame = true;
			this.tbText.IsSaveFileName = false;
			this.tbText.LineAlgnment = Hpd.HpdAlgnment.Center;
			this.tbText.Lines = new string[] {
        "Text"};
			this.tbText.Location = new System.Drawing.Point(40, 86);
			this.tbText.Multiline = false;
			this.tbText.Name = "tbText";
			this.tbText.Orientation = Hpd.HpdOrientation.Row;
			this.tbText.ScriptCode = hpdScriptCode4;
			this.tbText.Size = new System.Drawing.Size(267, 23);
			this.tbText.TabIndex = 8;
			this.tbText.TextAligiment = System.Drawing.StringAlignment.Near;
			this.tbText.TextLineAligiment = System.Drawing.StringAlignment.Near;
			this.tbText.UnCheckedColor = System.Drawing.Color.White;
			// 
			// NewControlDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(349, 183);
			this.ControlBox = false;
			this.Controls.Add(this.tbText);
			this.Controls.Add(this.hpdButton2);
			this.Controls.Add(this.tbName);
			this.Controls.Add(this.hpdButton1);
			this.Controls.Add(this.cmbControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.Name = "NewControlDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Control";
			this.ResumeLayout(false);

		}

		#endregion
		private Hpd.HpdComboBox cmbControl;
		private HpdButton hpdButton1;
		private HpdTextBox tbName;
		private HpdButton hpdButton2;
		private HpdTextBox tbText;
	}
}