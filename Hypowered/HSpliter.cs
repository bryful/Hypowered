using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class HSpliter :SplitContainer
	{
		private int BackupSize = 0;
		private int BackupD = 0;
		public new bool Panel2Collapsed
		{
			get { return base.Panel2Collapsed; }
			set { }
		}
		public bool IsOpen
		{
			get { return !base.Panel2Collapsed; }

			set 
			{
				bool b = !value;
				if(base.Panel2Collapsed!=b)
				{
					base.Panel2Collapsed = b;
					if (this.Orientation == Orientation.Horizontal)
					{
						
						if (b==false)
						{

							this.Height = this.Height + BackupSize + this.SplitterWidth;
					
						}
						else
						{
							this.Height = BackupD;
						}
					}
					else
					{
						int w = this.SplitterDistance;
						if (b == false)
						{
							this.Width = this.Width + BackupSize + this.SplitterWidth;
						}
						else
						{
							this.Width = BackupD;
						}
					}
				}
			}
		}


		public HSpliter()
		{

			this.SplitterMoved += HSpliter_SplitterMoved;
			ChkSize();
		}

		private void HSpliter_SplitterMoved(object? sender, SplitterEventArgs e)
		{
			ChkSize();
		}
		private void ChkSize()
		{
			BackupD = this.SplitterDistance;
			if (this.Orientation == Orientation.Horizontal)
			{
				BackupSize = this.Height - this.SplitterDistance - this.SplitterWidth;
			}
			else
			{
				BackupSize = this.Width - this.SplitterDistance - this.SplitterWidth;
			}
		}
	}
}
