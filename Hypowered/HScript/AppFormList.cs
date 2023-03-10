using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ClearScript;

namespace Hypowered.HScript
{
    public class AppFormList
    {
		[Category("Hypowered")]
		public PropertyBag bag { get; set; } = new PropertyBag();
		
        public dynamic itemsEO = new ExpandoObject();
        private HyperBaseForm[] m_items = new HyperBaseForm[0];
        public HyperBaseForm[] items { get { return m_items; } }
		public HyperBaseForm ? item(int idx)
        {
            HyperBaseForm? ret = null;
			if ((idx>=0)&&(idx<m_items.Length))
            {
                ret = m_items[idx];
            }
            return ret;
        }
		public HyperBaseForm? item(string name)
		{
			HyperBaseForm? ret = null;
            int idx = indexOfName(name);
            if(idx>=0) ret = m_items[idx];
			return ret;
		}
		public int length { get { return m_items.Length; } }
		[ScriptUsage(ScriptAccess.None)]
		public void clear()
        {
            m_items = new HyperBaseForm[0];
            itemsEO = new ExpandoObject();
        }
        public HyperBaseForm? this[int idx]
        {
            get
            {
                HyperBaseForm? ret = null;
                if (idx >= 0 && idx < m_items.Length)
                {
                    ret = m_items[idx];
                }
                return ret;
            }
            set
            {
                if (idx >= 0 && idx < m_items.Length)
                {
                    if (value != null && value is HyperBaseForm)
                        m_items[idx] = value;
                }
            }
        }
        // *********************************************************
        public AppFormList()
        {

        }
        // *********************************************************
        public AppFormList(HyperMainForm mf)
        {
            ListupForms(mf);
        }
        // *********************************************************
        public int indexOfName(string n)
        {
            int ret = -1;
            for (int i = 0; i < m_items.Length; i++)
            {
                if (string.Compare(n, m_items[i].Name, true) == 0)
                {
                    ret = i; break;
                }
            }
            return ret;
        }
        public HyperBaseForm? indexOf(int idx)
        {
            HyperBaseForm? ret = null;
            if (idx >= 0 && idx < m_items.Length)
            {
                ret = m_items[idx];
            }
            return ret;
        }
        public HyperBaseForm? findFromName(string nm)
        {
            HyperBaseForm? ret = null;
            int idx = indexOfName(nm);
            if (idx >= 0)
            {
                ret = m_items[idx];
            }
            return ret;
        }
		[ScriptUsage(ScriptAccess.None)]
		public virtual void ListupForms(HyperMainForm mf)
        {
            clear();
            if (mf == null) return;
            var dic = (IDictionary<string, dynamic>)itemsEO;
            if (mf != null)
            {
                List<HyperBaseForm> list = new List<HyperBaseForm>();
                if (mf.forms.Count > 0)
                {
                    foreach (var c in mf.forms)
                    {
                        if (c is HyperBaseForm)
                        {
                            list.Add(c);
                            dic[c.Name] = c;
                            bag[c.Name] = c;
                        }
                    }
                }
                m_items = list.ToArray();
            }
        }
    }
}
