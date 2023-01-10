using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
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
		private HControlList m_items = new HControlList();
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
		public HyperControl? item(string name,ControlType? ct=null)
		{
			return m_items.nameType(name,ct);
		}
		public HyperControl[] findType( ControlType ct)
		{
			return m_items.findType(ct);
		}
		public HyperControl? item(int idx)
		{
			return m_items[idx];
		}
		public HyperAppBase(HyperBaseForm? fm)
		{
			this.form = fm;
			ListupControls();
		}
		public virtual void ListupControls()
		{
			if (form != null) m_items.ListupControls(form);
		}
	}
	public class HyperApp : HyperAppBase
	{
		private HFormList m_forms = new HFormList();
		public HyperBaseForm []forms { get { return m_forms.items; } }
		public dynamic formsEO { get { return m_forms.itemsEO; }  }
		public IDictionary<string, dynamic> formsD
		{
			get { return (IDictionary<string, dynamic>)m_forms.itemsEO; }
		}
		public int numForms { get { return m_forms.length; } }
		public HyperApp(HyperBaseForm? main):base(main)
		{
			base.form = main;
			ListupControls();
		}
		public override void ListupControls()
		{
			base.ListupControls();
			if(main!= null)
			{
				m_forms.ListupForms(main);
			}
		}
		public void exit()
		{
			Application.Exit();
		}
		public void alert(object? s)
		{
			using (AlertForm dlg = new AlertForm())
			{
				dlg.SelectedObject = s;
				dlg.ShowDialog();
			}
		}
		public void write(object? s)
		{
			if (main != null)
			{
				main.OutputWrite(s);
			}
		}

		public void writeln(object? s)
		{
			if (main != null)
			{
				main.OutputWriteLine(s);
			}
		}
		public void cls()
		{
			if (main != null)
			{
				main.OutputClear();
			}
		}
		public bool loadForm(string fn)
		{
			if (main != null)
			{
				return main.LoadFromHYPF(fn);
			}
			else
			{
				return false;
			}
		}
		public bool openForm(string fn)
		{
			if (main != null)
			{
				return main.OpenFromHYPF(fn);
			}
			else
			{
				return false;
			}
		}
		public string? executablePath()
		{
			return Path.GetDirectoryName(Application.ExecutablePath);
		}
		public string hypfPath()
		{
			if (main != null)
			{
				return main.HYPF_Folder;
			}
			else
			{
				return "";
			}
		}
		public string homeHypf()
		{
			if (main != null)
			{
				return main.HOME_HYPF_FILE;
			}
			else
			{
				return "";
			}
		}
		public void loadHome()
		{
			loadForm(homeHypf());
		}
		public bool yesnoDialog(string cap, string title)
		{
			if (main != null)
			{
				return answerDialog.Show(cap, title);
			}
			else
			{
				return false;
			}
		}
	}
}
