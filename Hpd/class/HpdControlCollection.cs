﻿using System;
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
    public class HpdControlCollection : ICollection
    {
        /// <summary>
        /// 管理対象のオブジェクト
        /// </summary>
        private List<HpdControl> m_Items = new List<HpdControl>();
        public HpdControl? this[int idx]
        {
            get
            {
                HpdControl? ret = null;
                if (idx >= 0 && idx < m_Items.Count)
                {
                    ret = m_Items[idx];
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
        public void Add(HpdControl obj)
        {
            m_Items.Add(obj);
        }
        /// Removeメソッド
        /// </summary>
        /// <param name="index">削除対象のオブジェクト</param>
        public void Remove(int index)
        {
            if (index >= m_Items.Count && index < 0)
            {
                m_Items.RemoveAt(index);
            }
        }
        public void Remove(string key, HpdType? ht = null)
        {
            int idx = IndexOf(key, ht);
            if (idx >= 0) Remove(idx);
        }
        public void Clear()
        {
            m_Items.Clear();
        }
        public int IndexOf(string key, HpdType? ht = null)
        {
            int ret = -1;
            int idx = 0;
            foreach (var c in m_Items)
            {
                if (c is HpdControl)
                {
                    HpdControl hc = c;
                    if (hc.Name == key)
                    {
                        if (ht == null)
                        {
                            ret = idx;
                            break;
                        }
                        else
                        {
                            if (hc.HpdType == ht)
                            {
                                ret = idx;
                                break;
                            }
                        }
                    }

                }
                idx++;
            }
            return ret;
        }
        public HpdControl? Find(string key, HpdType? ht = null)
        {
            HpdControl? ret = null;
            int idx = IndexOf(key, ht);
            if (idx >= 0) ret = m_Items[idx];
            return ret;
        }
        public void Listup(HpdForm fm)
        {
            m_Items.Clear();
            if (fm != null && fm.Controls.Count > 0)
            {
                int idx = 0;
                foreach (var c in fm.Controls)
                {
                    if (c is HpdControl)
                    {
                        HpdControl hc = (HpdControl)c;
                        Add(hc);
                    }
                    idx++;
                }
            }
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HpdControlCollection()
        {
            // コンストラクタ
            m_Items.Clear();
        }

        /// <summary>
        /// ICollection.CopyTo
        /// </summary>
        /// <param name="list">コピー先のリスト</param>
        /// <param name="index">開始インデックス</param>
        void ICollection.CopyTo(Array list, int index)
        {
            foreach (var obj in m_Items)
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
            return new HCEnumerator(m_Items);
        }
    }
    public class HCEnumerator : IEnumerator
    {
        /// <summary>
        /// 走査対象になるリスト
        /// </summary>
        private List<HpdControl> m_Items = new List<HpdControl>();
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
                if (_cursor < 0 || _cursor == m_Items.Count)
                {
                    throw new InvalidOperationException();
                }
                return m_Items[_cursor];
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HCEnumerator()
        {
            // コンストラクタ
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">走査対象のリスト</param>
        public HCEnumerator(List<HpdControl> list)
        {
            //
            _cursor = -1;
            m_Items = list;
        }

        /// <summary>
        /// 列挙子リセット
        /// </summary>
        void IEnumerator.Reset()
        {
            _cursor = -1;
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
            if (_cursor < m_Items.Count)
            {
                // 列挙子を1つ進める
                _cursor++;
            }

            return !(_cursor == m_Items.Count);
        }
    }
}
