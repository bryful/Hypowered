using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hpd
{
	public partial class ListBoxHpd : ListBox
	{
		public ListBoxHpd()
		{
			InitializeComponent();
			this.DrawMode = DrawMode.OwnerDrawFixed;
		}
		public new DrawMode DrawMode
		{
			get { return base.DrawMode; }
			set { }
		}
		public ListItem[] GetItems()
		{
			List<ListItem> list = new List<ListItem>();
			if (this.Items.Count > 0)
			{
				foreach (object? c in this.Items)
				{
					if (c == null)
					{
						list.Add(new ListItem());
					}
					else if (c is string)
					{
						list.Add(new ListItem((string)c));
					}
					else if (c is ListItem)
					{
						list.Add((ListItem)c);
					}
					else
					{
						list.Add(new ListItem());
					}

				}
			}
			return list.ToArray();
		}
		public void SetItems(ListItem[] list)
		{
			this.Items.Clear();
			this.Items.AddRange(list);
		}
		public string? SelectedText
		{
			get
			{
				string? ret = null;
				if ((SelectedItem != null))
				{
					if (SelectedItem is string)
					{
						ret = (string)SelectedItem;
					}
					else if (SelectedItem is ListItem)
					{
						ret = ((ListItem)SelectedItem).Text;
					}
				}
				return ret;
			}
			set
			{
				this.SelectedIndex = IndexOfText(value);

			}
		}
		public ListItem? SelectedListItem
		{
			get
			{
				ListItem? ret = null;
				if ((SelectedItem != null))
				{
					if (SelectedItem is ListItem)
					{
						ret = (ListItem)SelectedItem;
					}
				}
				return ret;
			}
			set
			{
				SelectedIndex = IndexOfListItem(value);

			}
		}
		public int IndexOfText(string? tx)
		{
			int ret = -1;
			if ((this.Items.Count <= 0) || (tx == null)) return ret;
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.Items[i] == null) continue;
				if (this.Items[i] is string)
				{
					if (tx == this.Items[i].ToString())
					{
						ret = i;
						break;
					}
				}
				else if (this.Items[i] is ListItem)
				{
					if (tx == ((ListItem)this.Items[i]).Text)
					{
						ret = i;
						break;
					}
				}
			}
			return ret;
		}
		public int IndexOfListItem(ListItem? li)
		{
			int ret = -1;
			if ((this.Items.Count <= 0) || (li == null)) return ret;
			ret = IndexOfText(li.Text);
			return ret;
		}
		private void ComboDrawItem(object? sender, System.Windows.Forms.DrawItemEventArgs e)
		{
			e.DrawBackground();

			ListBoxHpd? cmb = (ListBoxHpd?)sender;
			if (cmb != null)
			{
				using (SolidBrush sb = new SolidBrush(cmb.ForeColor))
				{
					string txt = "";
					e.DrawBackground();

					if ((e.Index >= 0) && (e.Index < cmb.Items.Count))
					{
						if (cmb.Items[e.Index] == null)
						{
							txt = "null";
						}
						else if (cmb.Items[e.Index] is ListItem)
						{
							txt = ((ListItem)cmb.Items[e.Index]).Text;
						}
						else if (cmb.Items[e.Index] is string)
						{
							txt = (string)cmb.Items[e.Index];
						}
						else
						{
							try
							{
								txt = cmb.Items[e.Index].ToString();
							}
							catch (Exception ex)
							{
								txt = ex.ToString();
							}
						}
					}
					e.Graphics.DrawString(txt, this.Font, sb, e.Bounds);

				}
				//フォーカスを示す四角形を描画
				e.DrawFocusRectangle();
			}
		}


	}
}
