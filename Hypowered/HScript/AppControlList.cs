using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using Microsoft.ClearScript;

namespace Hypowered.HScript
{
    public class AppControlList
    {
		[Category("Hypowered")]
		public StringCollection strings { get; set; } = new StringCollection();
		[Category("Hypowered")]
		public ExpandoObject eo { get; set; } = new ExpandoObject();
		[Category("Hypowered")]
		public PropertyBag bag { get; set; } = new PropertyBag();
		public dynamic itemsEO = new ExpandoObject();
        private HyperControl[] m_items = new HyperControl[0];
        public HyperControl[] items { get { return m_items; } }
        public int length { get { return m_items.Length; } }
		public int numItems { get { return m_items.Length; } }
		[ScriptUsage(ScriptAccess.None)]
		public void clear()
        {
            m_items = new HyperControl[0];
            itemsEO = new ExpandoObject();
            bag = new PropertyBag();
        }
        public HyperControl? this[int idx]
        {
            get
            {
                HyperControl? ret = null;
                if (idx >= 0 && idx < m_items.Length)
                {
                    ret = m_items[idx];
                }
                return ret;
            }
            set
            {
                if (value != null && value is HyperControl)
                {
                    if (idx >= 0 && idx < m_items.Length)
                    {

                        m_items[idx] = value;
                    }
                }
            }
        }
        // *********************************************************
        public AppControlList()
        {

        }
        // *********************************************************
        public AppControlList(HyperBaseForm bf)
        {
            ListupControls(bf);
        }
        // *********************************************************
        
        public int indexOf(string n, ControlType? ct = null)
        {
            int ret = -1;
            for (int i = 0; i < m_items.Length; i++)
            {
                if (ct == null || m_items[i].ControlType == ct)
                {
                    if (string.Compare(n, m_items[i].Name, true) == 0)
                    {
                        ret = i; break;
                    }
                }
            }
            return ret;
        }
         public HyperControl? findFromName(string nm, ControlType? ct = null)
        {
            HyperControl? ret = null;
            int idx = indexOf(nm, ct);
            if (idx >= 0)
            {
                ret = m_items[idx];
            }
            return ret;
        }
        public HyperControl? item(string nm)
        {
            return findFromName(nm, null);
		}
		public HyperControl[] findType(ControlType ct)
        {
            List<HyperControl> list = new List<HyperControl>();
            if (m_items.Length > 0)
            {
                for (int i = 0; i < m_items.Length; i++)
                {
                    if (m_items[i].ControlType == ct)
                    {
                        list.Add(m_items[i]);
                    }
                }
            }
            return list.ToArray();
        }
		[ScriptUsage(ScriptAccess.None)]
		public virtual void ListupControls(HyperBaseForm bf)
        {
            clear();
            var dic = (IDictionary<string, dynamic>)itemsEO;
            if (bf != null)
            {
                List<HyperControl> list = new List<HyperControl>();
                if (bf.Controls.Count > 0)
                {
                    foreach (var c in bf.Controls)
                    {
                        if (c is HyperControl)
                        {
                            HyperControl hc = (HyperControl)c;
                            list.Add(hc);
                            dic[hc.Name] = hc;
                            bag[hc.Name] = hc;
                        }
                    }
                }
                m_items = list.ToArray();
            }
        }
    }
}
