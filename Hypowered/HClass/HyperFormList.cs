using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hypowered
{
	public class HyperFormList : ICollection<HyperBaseForm>
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
		private int m_TargetFormIndex = -1;
		// ************************************************************************
		public IEnumerator<HyperBaseForm> GetEnumerator()
		{
			return new HyperBaseFormEnumerator(this);
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new HyperBaseFormEnumerator(this);
		}
		// ************************************************************************
		public string[] GetAllContorolName()
		{
			List<string> result = new List<string>();
			if(m_Items.Count>0)
			{
				foreach(HyperBaseForm bf in m_Items)
				{
					if (bf.Controls.Count > 0)
					{
						foreach (HyperControl c in bf.Controls)
						{
							result.Add(c.Name);
						}
					}
				}
			}
			return result.ToArray();

		}
		public bool IsNameChk(string name)
		{
			if (m_Items.Count > 0)
			{
				foreach (HyperBaseForm bf in m_Items)
				{
					int idx = bf.IndexOfName(name);
					if(idx>=0) return true;
				}
			}
			return false;
		}
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
		public HyperBaseForm? item(int idx)
		{
			HyperBaseForm? ret =null;
			if((idx>=0)&&(idx<m_Items.Count))
			{
				ret = m_Items[idx];
			}
			return ret;
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
		public bool Contains(HyperBaseForm item)
		{
			bool found = false;

			foreach (HyperBaseForm bx in m_Items)
			{
				// Equality defined by the Box
				// class's implmentation of IEquatable<T>.
				if (bx.Equals(item))
				{
					found = true;
				}
			}

			return found;
		}
		public bool Contains(HyperBaseForm item, EqualityComparer<HyperBaseForm> comp)
		{
			bool found = false;

			foreach (HyperBaseForm bx in m_Items)
			{
				if (comp.Equals(bx, item))
				{
					found = true;
				}
			}

			return found;
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
				if((m_TargetFormIndex >= 0)&&(m_TargetFormIndex<m_Items.Count))
				{
					return m_Items[m_TargetFormIndex];
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
					m_TargetFormIndex = value.Index;
					if (TargetForm != null)
					{
						OnFormChanged(new HyperChangedEventArgs(TargetForm, TargetForm.TargetControl));
					}
				} 
			}
		}
		public int TargetFormIndex
		{
			get { return m_TargetFormIndex; }
			set
			{
				if ((value >= 0) && (value<m_Items.Count ))
				{
					if(m_TargetFormIndex != value)
					{
						m_TargetFormIndex = value;
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

			if (!Contains(bf))
			{
				if (bf is HyperMainForm)
				{
					SetMain((HyperMainForm)bf);
					return;
				}
				m_Items.Add(bf);
				bf.Index = m_Items.Count - 1;
				bf.Activated += Bf_Activated;
				OnFormChanged(new HyperChangedEventArgs(bf, bf.TargetControl));
			}
		}
		
		private void Bf_Activated(object? sender, EventArgs e)
		{
			HyperBaseForm? c = (HyperBaseForm?)sender;
			if(c != null)
			{
				TargetFormIndex = c.Index;
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
		public void CopyTo(HyperBaseForm[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException("The array cannot be null.");
			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
			if (Count > array.Length - arrayIndex)
				throw new ArgumentException("The destination array has fewer elements than the collection.");

			for (int i = 0; i < m_Items.Count; i++)
			{
				array[i + arrayIndex] = m_Items[i];
			}
		}
		public bool IsReadOnly
		{
			get { return false; }
		}
		public bool Remove(HyperBaseForm item)
		{
			bool result = false;

			// Iterate the inner collection to
			// find the box to be removed.
			for (int i = 0; i < m_Items.Count; i++)
			{

				HyperBaseForm curBox = (HyperBaseForm)m_Items[i];

				if (new HyperBaseFormSameDimensions().Equals(curBox, item))
				{
					m_Items.RemoveAt(i);
					result = true;
					break;
				}
			}
			return result;
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
	public class HyperBaseFormEnumerator : IEnumerator<HyperBaseForm>
	{
		private HyperFormList _collection;
		private int curIndex;
		private HyperBaseForm? curBox;

		public HyperBaseFormEnumerator(HyperFormList collection)
		{
			_collection = collection;
			curIndex = -1;
			//curBox = null;
		}

		public bool MoveNext()
		{
			//Avoids going beyond the end of the collection.
			if (++curIndex >= _collection.Count)
			{
				return false;
			}
			else
			{
				// Set current box to next item in collection.
				curBox = _collection[curIndex];
			}
			return true;
		}

		public void Reset() { curIndex = -1; }

		void IDisposable.Dispose() { }

		public HyperBaseForm Current
		{
			get { return curBox; }
		}

		object? IEnumerator.Current
		{
			get { return Current; }
		}
	}
	public class HyperBaseFormSameDimensions : EqualityComparer<HyperBaseForm>
	{

		public override bool Equals(HyperBaseForm b1, HyperBaseForm b2)
		{
			if (b1.Index == b2.Index)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode(HyperBaseForm bx)
		{
			int hCode = bx.Index;
			return hCode.GetHashCode();
		}
	}
}
