namespace Hypowered
{
	partial class HyperMainForm
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
		protected void InitializeComponent()
		{
			System.Windows.Media.SolidColorBrush solidColorBrush1 = new System.Windows.Media.SolidColorBrush();
			Microsoft.ClearScript.PropertyBag propertyBag1 = new Microsoft.ClearScript.PropertyBag();
			ICSharpCode.AvalonEdit.Document.TextDocument textDocument1 = new ICSharpCode.AvalonEdit.Document.TextDocument();
			System.ComponentModel.Design.ServiceContainer serviceContainer1 = new System.ComponentModel.Design.ServiceContainer();
			ICSharpCode.AvalonEdit.Document.UndoStack undoStack1 = new ICSharpCode.AvalonEdit.Document.UndoStack();
			System.Dynamic.ExpandoObject expandoObject1 = new System.Dynamic.ExpandoObject();
			System.Windows.Media.SolidColorBrush solidColorBrush2 = new System.Windows.Media.SolidColorBrush();
			System.Windows.Media.SolidColorBrush solidColorBrush3 = new System.Windows.Media.SolidColorBrush();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HyperMainForm));
			this.hyperEditor1 = new Hypowered.HyperEditor();
			this.SuspendLayout();
			// 
			// hyperEditor1
			// 
			this.hyperEditor1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
			solidColorBrush1.Color = System.Windows.Media.Color.FromArgb(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
			this.hyperEditor1.Background = solidColorBrush1;
			this.hyperEditor1.bag = propertyBag1;
			this.hyperEditor1.CanColorCustum = false;
			this.hyperEditor1.ConvertTabsToSpaces = false;
			textDocument1.FileName = null;
			textDocument1.ServiceProvider = serviceContainer1;
			textDocument1.Text = "hyperEditor1";
			undoStack1.SizeLimit = 2147483647;
			textDocument1.UndoStack = undoStack1;
			this.hyperEditor1.Document = textDocument1;
			this.hyperEditor1.DragDropFileType = Hypowered.DragDropFileType.None;
			this.hyperEditor1.eo = expandoObject1;
			this.hyperEditor1.FileName = "";
			this.hyperEditor1.ForcusColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(133)))), ((int)(((byte)(222)))));
			this.hyperEditor1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			solidColorBrush2.Color = System.Windows.Media.Color.FromArgb(((byte)(255)), ((byte)(0)), ((byte)(0)), ((byte)(0)));
			this.hyperEditor1.Foreground = solidColorBrush2;
			this.hyperEditor1.FrameWeight = new System.Windows.Forms.Padding(1);
			this.hyperEditor1.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
			this.hyperEditor1.IsDrawFocuse = true;
			this.hyperEditor1.IsDrawFrame = true;
			this.hyperEditor1.IsEditMode = false;
			this.hyperEditor1.IsSaveFileName = false;
			solidColorBrush3.Color = System.Windows.Media.Color.FromArgb(((byte)(255)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
			this.hyperEditor1.LineNumbersForeground = solidColorBrush3;
			this.hyperEditor1.Lines = new string[] {
        ""};
			this.hyperEditor1.Location = new System.Drawing.Point(162, 125);
			this.hyperEditor1.Name = "hyperEditor1";
			this.hyperEditor1.Offset = 0;
			this.hyperEditor1.Options = ((ICSharpCode.AvalonEdit.TextEditorOptions)(resources.GetObject("hyperEditor1.Options")));
			this.hyperEditor1.ParentForm = null;
			this.hyperEditor1.Script_CurrentDirChanged = "";
			this.hyperEditor1.Script_DragDrop = "";
			this.hyperEditor1.Script_MouseClick = "";
			this.hyperEditor1.Script_MouseDoubleClick = "";
			this.hyperEditor1.Script_SelectedIndexChanged = "";
			this.hyperEditor1.Script_ValueChanged = "";
			this.hyperEditor1.Selected = false;
			this.hyperEditor1.SelectedText = "";
			this.hyperEditor1.SelectionLength = 0;
			this.hyperEditor1.SelectionStart = 0;
			this.hyperEditor1.ShowColumnRuler = false;
			this.hyperEditor1.ShowEndOfLine = false;
			this.hyperEditor1.ShowLineNumbers = false;
			this.hyperEditor1.ShowSpaces = false;
			this.hyperEditor1.ShowTabs = true;
			this.hyperEditor1.Size = new System.Drawing.Size(378, 160);
			this.hyperEditor1.strings = ((System.Collections.Specialized.StringCollection)(resources.GetObject("hyperEditor1.strings")));
			this.hyperEditor1.TabIndex = 0;
			this.hyperEditor1.TextAligiment = System.Drawing.StringAlignment.Near;
			this.hyperEditor1.TextLineAligiment = System.Drawing.StringAlignment.Center;
			this.hyperEditor1.UnCheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
			this.hyperEditor1.WordWrap = false;
			// 
			// HyperMainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.hyperEditor1);
			this.DoubleBuffered = true;
			this.Name = "HyperMainForm";
			this.ResumeLayout(false);

		}

		#endregion

		private HyperEditor hyperEditor1;
	}
}