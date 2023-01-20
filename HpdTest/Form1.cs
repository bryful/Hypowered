using Hpd;

namespace HpdTest
{
	public partial class Form1 : HpdForm
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			SetIsEdit(this,!this.IsEdit);
		}
	}
}