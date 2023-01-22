namespace BRY
{
	public class TreeNodeEx : System.Windows.Forms.TreeNode
	{
		public TreeNodeEx(string text) :
			base(text)
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			isSubFoldersAdded = false;
		}

		private bool isSubFoldersAdded;
		public bool SubFoldersAdded
		{
			get
			{
				return isSubFoldersAdded;
			}
			set
			{
				isSubFoldersAdded = value;
			}
		}
	}
}