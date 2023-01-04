using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class HyperMenuItem
	{
		public bool Checked = false;
		public int Index = -1;
		public string Caption = "File";
		public int Width = 70;
		public int Left = 0;
		public int Right { get { return Left + Width; } }
		public FuncType? Func = null;
		public Keys Key = Keys.None;
		public bool Visibled = true;
		//public ContextMenuStrip? Menu = null;
		public List<HyperMenuItem?> Items = new List<HyperMenuItem?>();
		public HyperMenuItem(Control? fm, string c, FuncItem? f)
		{
			Caption = c;
			if(f != null)
			{
				Func = f.Func;
				Key = f.KeysFirst;
			}
			if (fm != null)
			{
				Bitmap bmp = new Bitmap(500, 25);
				Graphics g = Graphics.FromImage(bmp);
				SizeF sz = g.MeasureString(Caption, fm.Font, 500, new StringFormat());
				Width = (int)(sz.Width + 0.5)+10;
				bmp.Dispose();
			}
		}
		public void Add(HyperMenuItem? mi)
		{
			Items.Add(mi);
		}
		public void Remove(HyperMenuItem mi)
		{
			Items.Remove(mi);
		}
		public void Clear()
		{
			Items.Clear();
		}
		public HyperMenuItem? Get(int index)
		{
			if ((index >= 0) && (index < Items.Count))
			{
				return Items[index];
			}
			else
			{
				return null;
			}
		}
		public ContextMenuStrip MakeMenu(HyperMainForm? mf=null )
		{
			ContextMenuStrip ret = new ContextMenuStrip();
			if (Items.Count > 0)
			{
				
				foreach (HyperMenuItem? mi in Items)
				{
					if (mi == null)
					{
						ret.Items.Add(new ToolStripSeparator());
					}
					else
					{
						ToolStripMenuItem mc = new ToolStripMenuItem();
						if((mf!=null)&&(mi.Func!=null))
						{
							string nm = mi.Func.Method.Name;
							if ((mf.ControlList != null) && (nm == "ShowControlList"))
							{
								mc.Checked = (mf.ControlList.Visible);
							}
							
						}
						mc.Checked= mi.Checked;
						mc.Text = mi.Caption;
						mc.Click += Mc_Click;
						mc.ShortcutKeys = mi.Key;
						mc.Tag = (Object?)mi.Func;
						ret.Items.Add(mc);
					}
				}
			}
			return ret;

		}

		private void Mc_Click(object? sender, EventArgs e)
		{
			ToolStripMenuItem? m = (ToolStripMenuItem?)sender;
			if (m != null)
			{
				if ((m.Tag != null) && (m.Tag is FuncType))
				{
					try
					{
						((FuncType)m.Tag)();
					}
					catch
					{

					}
				}
			}
		}
	}

	public class HyperMenuItems
	{
		static public readonly int Leftmargin = 20;
		private List<HyperMenuItem?> m_Items = new List<HyperMenuItem?>();
		public int Count
		{
			get { return m_Items.Count; }
		}
		public HyperMenuItem? this[int idx]
		{
			get
			{
				if((idx>=0)&&(idx< m_Items.Count))
				{
					return m_Items[idx];
				}else
				{
					return null;
				}
			}
		}
		public List<HyperMenuItem?> Items
		{
			get{ return m_Items; }
		}
		public void Add(HyperMenuItem? item)
		{
			m_Items.Add(item);
			ChkWidth();
		}
		public void RemoveAt(int idx)
		{
			m_Items.RemoveAt(idx);
			ChkWidth();
		}
		public void Remove(HyperMenuItem mi)
		{
			m_Items.Remove(mi);
			ChkWidth();
		}
		public void Clear()
		{
			m_Items.Clear();
		}
		private void ChkWidth()
		{
			if(m_Items.Count>0)
			{
				int x = Leftmargin;
				int idx = 0;
				foreach(HyperMenuItem? item in m_Items)
				{
					if(item ==null) continue;
					item.Index=idx;
					item.Left = x;
					x += item.Width;
					idx++;
				}
			}
		}
		public bool GetMenuVisibled(int index)
		{
			bool ret = false;
			if ((m_Items!=null)&&(index >= 0) && (index < m_Items.Count))
			{
				ret = m_Items[index].Checked;
			}
			return ret;
		}
		public void SetMenuVisibled(int index, bool on)
		{
			if ((m_Items != null) && (index >= 0) && (index < m_Items.Count))
			{
				m_Items[index].Checked = on;
				ChkWidth();
			}
		}
	}
}
