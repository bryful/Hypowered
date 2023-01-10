using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Microsoft.ClearScript;

namespace Hypowered.HScript
{
    public class HyperAppBase
    {
        public HyperBaseForm? form = null;
        public HyperMainForm? main
        {
            get
            {
                if (form is HyperMainForm)
                {
                    return (HyperMainForm?)form;
                }
                else
                {
                    return null;
                }
            }
        }
		[Category("Hypowered")]
		public StringCollection strings { get; set; } = new StringCollection();
		[Category("Hypowered")]
		public ExpandoObject eo { get; set; } = new ExpandoObject();
        private AppControlList m_items = new AppControlList();
        public dynamic itemEO
        {
            get { return m_items.itemsEO; }
        }
        public IDictionary<string, dynamic> itemD
        {
            get { return (IDictionary<string, dynamic>)m_items.itemsEO; }
        }
        public HyperControl[] items { get { return m_items.items; } }
        //public HControlList items { get { return m_items; } }
        public int numItems { get { return m_items.length; } }
        public HyperControl? item(string name, ControlType? ct = null)
        {
            return m_items.findFromName(name, ct);
        }
        public HyperControl[] findType(ControlType ct)
        {
            return m_items.findType(ct);
        }
        public HyperControl? item(int idx)
        {
            return m_items[idx];
        }
        public HyperAppBase(HyperBaseForm? fm)
        {
            form = fm;
            ListupControls();
        }
		[ScriptUsage(ScriptAccess.None)]
		public virtual void ListupControls()
        {
            if (form != null) m_items.ListupControls(form);
        }
    }
}
