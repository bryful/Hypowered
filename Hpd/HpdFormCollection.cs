using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Hpd
{
	public class HpdFormCollection : ICollection
	{
		/// <summary>
		/// 管理対象のオブジェクト
		/// </summary>
		private List<HpdForm> m_Items = new List<HpdForm>();
		public HpdForm? this[int idx]
		{
			get 
			{
				HpdForm? ret = null;
				if((idx>=0)&&(idx<this.m_Items.Count))
				{
					ret = (HpdForm)m_Items[idx];
				}
				return ret;
			}
		}

		/// <summary>
		/// ICollection.Countプロパティ
		/// </summary>
		int ICollection.Count
		{
			get
			{
				return m_Items.Count;
			}
		}
		public int Count
		{
			get { return m_Items.Count; }
		}
		/// <summary>
		/// ICollection.SyncRootプロパティ
		/// </summary>
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>
		/// ICollection.IsSynchronizedプロパティ
		/// </summary>
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}
		public bool Add(HpdForm obj)
		{
			int idx = IndexOf(obj.Name);
			if (idx < 0)
			{
				this.m_Items.Add(obj);
				return true;
			}
			else
			{
				return false;
			}
		}
		/// Removeメソッド
		/// </summary>
		/// <param name="index">削除対象のオブジェクト</param>
		public void Remove(int index)
		{
			if ((index >= m_Items.Count) && (index < 0))
			{
				this.m_Items.RemoveAt(index);
			}
		}
		internal void Clear()
		{
			this.m_Items.Clear();
		}
		public int IndexOf(string key)
		{
			int ret = -1;
			int idx = 0;
			foreach (var c in m_Items)
			{
				if (c is HpdForm)
				{
					HpdForm hf = (HpdForm)c;
					if (hf.Name == key)
					{
						ret = idx;
						break;
					}
				}
				idx++;
			}
			return ret;
		}
		public HpdForm? Find(string key)
		{
			HpdForm? ret = null;
			int idx = IndexOf(key);
			if (idx >= 0) ret = m_Items[idx];
			return ret;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public HpdFormCollection()
		{
			// コンストラクタ
			this.m_Items.Clear();
		}

		/// <summary>
		/// ICollection.CopyTo
		/// </summary>
		/// <param name="list">コピー先のリスト</param>
		/// <param name="index">開始インデックス</param>
		void ICollection.CopyTo(Array list, int index)
		{
			foreach (var obj in this.m_Items)
			{
				list.SetValue(obj, index);
				index = index + 1;
			}
		}

		/// <summary>
		/// 列挙子取得処理
		/// </summary>
		/// <returns>当クラスオブジェクトの列挙子オブジェクト</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new HFEnumerator(m_Items);
		}
	}
	public class HFEnumerator : IEnumerator
	{
		/// <summary>
		/// 走査対象になるリスト
		/// </summary>
		private List<HpdForm> _list = new List<HpdForm>();
		/// <summary>
		/// 現在のアクセス位置(インデックス)
		/// </summary>
		private int _cursor;

		/// <summary>
		/// 列挙子の現在の位置
		/// </summary>
		object IEnumerator.Current
		{
			get
			{
				if ((_cursor < 0) || (_cursor == _list.Count))
				{
					throw new InvalidOperationException();
				}
				return _list[_cursor];
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public HFEnumerator()
		{
			// コンストラクタ
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="list">走査対象のリスト</param>
		public HFEnumerator(List<HpdForm> list)
		{
			//
			this._cursor = -1;
			this._list = list;
		}

		/// <summary>
		/// 列挙子リセット
		/// </summary>
		void IEnumerator.Reset()
		{
			this._cursor = -1;
		}

		/// <summary>
		/// 列挙子をコレクションの次の要素に進める
		/// </summary>
		/// <returns>
		/// 列挙子が次の要素に正常に進んだ場合は true。
		/// 列挙子がコレクションの末尾を越えた場合は false
		/// </returns>
		bool IEnumerator.MoveNext()
		{
			if (this._cursor < _list.Count)
			{
				// 列挙子を1つ進める
				this._cursor++;
			}

			return (!(this._cursor == this._list.Count));
		}
	}
}
