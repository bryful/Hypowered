using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public partial class ScriptEditor : Control
	{
		public RoslynEdit RoslynEdit { get; } =    new RoslynEdit();
		public Button btnEditStart { get; } = new Button();
		public Button btnEditEnd { get; } = new Button();
		public Button btnExecute { get; } = new Button();
		public ComboBox cmbEvent { get; } = new ComboBox();

		public bool EditMode
		{
			get { return btnEditEnd.Enabled; }
			set 
			{ 
				btnEditEnd.Enabled = value;
				btnEditStart.Enabled = ! value;
			}
		}

		public ScriptEditor()
		{
			InitializeComponent();
			btnEditEnd.Enabled = false;
			btnEditStart.Name = "btnEditStart";
			btnEditStart.Text = "EditStart";
			btnEditStart.FlatStyle = FlatStyle.Flat;

			btnEditEnd.Name = "btnEditEnd";
			btnEditEnd.Text = "EditEnd";
			btnEditEnd.FlatStyle = FlatStyle.Flat;

			btnExecute.Name = "btnExecute";
			btnExecute.Text = "Execute";
			btnExecute.FlatStyle = FlatStyle.Flat;

			ChkLayout();
			this.Controls.Add(btnEditStart);
			this.Controls.Add(btnEditEnd);
			this.Controls.Add(cmbEvent);
			this.Controls.Add(btnExecute);
			this.Controls.Add(RoslynEdit);
		}
		private void ChkLayout()
		{
			int x = 0;
			btnEditStart.Size = new Size(70, 23);
			btnEditStart.Location = new Point(0,0);
			x += btnEditStart.Width + 2;
			btnEditEnd.Size = new Size(70, 23);
			btnEditEnd.Location = new Point(75, 0);
			x += btnEditEnd.Width + 6;

			cmbEvent.Size = new Size(100, 23);
			cmbEvent.Location = new Point(145, 0);

			btnExecute.Size = new Size(70, 23);
			btnExecute.Location = new Point(this.Width-75, 0);
			
			RoslynEdit.Size = new Size(this.Width, this.Height - 25);
			RoslynEdit.Location = new Point(0, 25);
		}
		protected override void OnResize(EventArgs e)
		{
			ChkLayout();
			base.OnResize(e);
		}
	}
	
}
