using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hypowered
{
	public class HyperFormList
	{
		// ****************************************************************************
		public delegate void FormChangedHandler(object sender, HyperChangedEventArgs e);
		public event FormChangedHandler? FormChanged;
		protected virtual void OnFormChanged(HyperChangedEventArgs e)
		{
			if (FormChanged != null)
			{
				FormChanged(this, e);
			}
		}
		public HyperMainForm? MainForm { get; set; } = null;
		private List<HyperBaseForm> m_Items= new List<HyperBaseForm>();
		private int m_TargetIndex = -1;
		// ************************************************************************
		public HyperFormList() 
		{

		}
		public int Count
		{
			get { return m_Items.Count; }
		}
		public List<HyperBaseForm> Items
		{
			get { return m_Items; }
			set { m_Items = value; }
		}
		public HyperBaseForm? this[int idx]
		{
			get 
			{
				HyperBaseForm? ret = null;
				if ((idx>=0)&&(idx<this.m_Items.Count))
				{
					ret= this.m_Items[idx];
				}
				return ret; 
			}
			set 
			{
				if (value != null)
				{
					if ((idx >= 0) && (idx < this.m_Items.Count))
					{
						this.m_Items[idx] = value;
					}
				}
			}
		}
		public void Clear()
		{
			if(m_Items.Count > 1) 
			{
				for(int i= m_Items.Count-1; i>=1;i--)
				{
					if (this.m_Items[i] is HyperMainForm)
					{
						//
					}
					else
					{
						m_Items[i].Dispose();
						m_Items.RemoveAt(i);
					}
				}
			}
		}

		public HyperBaseForm? TargetForm
		{
			get
			{
				if((m_TargetIndex >= 0)&&(m_TargetIndex<m_Items.Count))
				{
					return m_Items[m_TargetIndex];
				}
				else
				{
					return null;
				}
			}
			set
			{ 
				if (value != null)
				{
					m_TargetIndex = value.Index;
					if (TargetForm != null)
					{
						OnFormChanged(new HyperChangedEventArgs(TargetForm, TargetForm.TargetControl));
					}
				} 
			}
		}
		public int TargetIndex
		{
			get { return m_TargetIndex; }
			set
			{
				if ((value >= 0) && (value<m_Items.Count ))
				{
					if(m_TargetIndex != value)
					{
						m_TargetIndex = value;
						if (TargetForm != null)
						{
							OnFormChanged(new HyperChangedEventArgs(TargetForm, TargetForm.TargetControl));
						}
					}
				}
			}
		}
		// ************************************************************************
		public void Add(HyperBaseForm bf)
		{

			if (bf is HyperMainForm)
			{
				SetMain((HyperMainForm)bf);
				return;
			}
			m_Items.Add(bf);
			bf.Index = m_Items.Count - 1;
			bf.Activated += Bf_Activated;
		}

		private void Bf_Activated(object? sender, EventArgs e)
		{
			HyperBaseForm? c = (HyperBaseForm?)sender;
			if(c != null)
			{
				TargetIndex = c.Index;
			}
		}

		// ************************************************************************
		public void SetMain(HyperMainForm mf)
		{
			MainForm = mf;
			if(MainForm != null) 
			{
				if(m_Items.Count > 0) 
				{
					if (m_Items[0] is HyperMainForm)
					{
						m_Items[0] = mf;
					}
					else
					{
						m_Items.Insert(0, mf);
					}
				}
				else
				{
					m_Items.Add(mf);
				}
				mf.Index = 0;
				mf.Activated -= Bf_Activated;
				mf.Activated += Bf_Activated;
				OnFormChanged(new HyperChangedEventArgs(mf, mf.TargetControl));
			}
		}
		// ************************************************************************
		public void ChkForms()
		{
			if(m_Items.Count>0)
			{
				int idx = 0;
				foreach(var c in m_Items)
				{
					c.Index = idx;
					idx++;
				}
			}
		}
		// ************************************************************************
		public void SetSwapIndex(int s,int d) 
		{
			if(s==d) return;
			if ((s < 0) || (s >= m_Items.Count) || (d < 0) || (d >= m_Items.Count)) return;
			HyperBaseForm b = m_Items[s];
			m_Items[s] = m_Items[d];
			m_Items[d] = b;
			ChkForms();
		}
		public ToolStripMenuItem[] GetFormsForMenu(System.EventHandler func)
		{
			List<ToolStripMenuItem> list = new List<ToolStripMenuItem>();
			if (m_Items.Count > 0)
			{
				foreach (HyperBaseForm c in m_Items)
				{
					ToolStripMenuItem mi = new ToolStripMenuItem();
					if (TargetForm != null)
					{
						mi.Checked = (c.Index == TargetForm.Index);
					}
					mi.Text = c.Name;
					mi.Tag = (object)c;
					mi.Click += func;
					list.Add(mi);
				}
			}
			return list.ToArray();
		}
		public string[] GetForms()
		{
			List<string> list = new List<string>();
			if (m_Items.Count > 0)
			{
				foreach (HyperBaseForm c in m_Items)
				{
					list.Add(c.Name);
				}
			}
			return list.ToArray();
		}
	}
}
