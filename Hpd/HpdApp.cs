using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hpd
{
	public class HpdApp
	{
		private HpdMainForm? m_MainForm = null;
		public HpdMainForm? MainForm { get { return m_MainForm; } }

		private HpdFormCollection m_Forms = new HpdFormCollection();
		public HpdFormCollection Forms { get { return m_Forms; } }

		public HpdApp()
		{
			m_Forms.Clear();
		}
		public void SetMainForm(HpdMainForm? mf) 
		{
			m_Forms.Clear();
			m_MainForm = mf;
			if(m_MainForm!= null)
			{

				m_Forms.Add(m_MainForm);
				m_MainForm.ItemsRefresh();
			}

		}
		public void AddFrom(HpdForm fm)
		{
			m_Forms.Add(fm);
			fm.ItemsRefresh();
		}
		public void ItemsRefresh()
		{
			if(m_Forms.Count>=0)
			{
				foreach(HpdForm s in m_Forms)
				{
					s.ItemsRefresh();
				}
			}
		}
	}
}
