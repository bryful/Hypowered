using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Hypowered
{
    public enum ScriptKind
    {
        Load = 0,
        MouseClick,
        MouseDoubleClick,
        SelectedIndexChanged,
        CurrentDirChanged,
        ValueChanged,
        KeyPress,
		Closed,
        DragDrop
	}
    [Flags]
    public enum InScriptBit
    {
        None = 0,
        Load
            = 0b_0000_0000_0001,
        MouseClick
            = 0b_0000_0000_0010,
        MouseDoubleClick
            = 0b_0000_0000_0100,
        SelectedIndexChanged
            = 0b_0000_0000_1000,
        CurrentDirChanged
            = 0b_0000_0001_0000,
        ValueChanged
            = 0b_0000_0010_0000,
        KeyPress
            = 0b_0000_0100_0000,
		Closed
			= 0b_0000_1000_0000,
		DragDrop
		    = 0b_0001_0000_0000,
	}
    public enum DragDropFileType
    {
        None = 0,
        FileOnly,
		DirectoryOnly,
		FileAndDirectory
	};
    public class HyperScriptCode
    {
        protected InScriptBit m_InScript = InScriptBit.None;
        public InScriptBit InScript
        {
            get { return m_InScript; }
        }
        protected string[] m_ValidSprictNames = new string[0];
        public string[] ValidSprictNames
        {
            get { return m_ValidSprictNames; }
        }
        protected ScriptKind[] m_ScriptKinds = new ScriptKind[0];
        public ScriptKind[] ScriptKinds
        {
            get { return m_ScriptKinds; }
        }
        public int Count
        {
            get { return m_ValidSprictNames.Length; }
        }
        public void SetInScript(InScriptBit ist)
        {
            m_InScript = ist;
            GetValidSprictNames(ist);
        }
        private string[] m_Codes = new string[0];
        private V8Script?[] m_compleZ = new V8Script[9];
        public string Code(ScriptKind sk)
        {
            return m_Codes[(int)sk];
		}
        public string Script_Load
        {
            get { return m_Codes[(int)ScriptKind.Load]; }
            set { m_Codes[(int)ScriptKind.Load] = value; }
        }
        public string Script_KeyPress
        {
            get { return m_Codes[(int)ScriptKind.KeyPress]; }
            set { m_Codes[(int)ScriptKind.KeyPress] = value; }
        }
        public string Script_MouseClick
        {
            get { return m_Codes[(int)ScriptKind.MouseClick]; }
            set { m_Codes[(int)ScriptKind.MouseClick] = value; }
        }
        public string Script_MouseDoubleClick
        {
            get { return m_Codes[(int)ScriptKind.MouseDoubleClick]; }
            set { m_Codes[(int)ScriptKind.MouseDoubleClick] = value; }
        }
        public string Script_SelectedIndexChanged
        {
            get { return m_Codes[(int)ScriptKind.SelectedIndexChanged]; }
            set { m_Codes[(int)ScriptKind.SelectedIndexChanged] = value; }
        }
        public string Script_CurrentDirChanged
        {
            get { return m_Codes[(int)ScriptKind.CurrentDirChanged]; }
            set { m_Codes[(int)ScriptKind.CurrentDirChanged] = value; }
        }
        public string Script_ValueChanged
        {
            get { return m_Codes[(int)ScriptKind.ValueChanged]; }
            set { m_Codes[(int)ScriptKind.ValueChanged] = value; }
        }
		public string Script_Closed
		{
			get { return m_Codes[(int)ScriptKind.Closed]; }
			set { m_Codes[(int)ScriptKind.Closed] = value; }
		}
		public string Script_DragDrop
		{
			get { return m_Codes[(int)ScriptKind.DragDrop]; }
			set { m_Codes[(int)ScriptKind.DragDrop] = value; }
		}
		public string GetScriptCode(ScriptKind ist)
        {
            string ret = "";
            ret = m_Codes[(int)ist];
            return ret;
        }
        public void SetScriptCode(ScriptKind ist, string code)
        {
            m_Codes[(int)ist] = code;
        }
		public V8Script? GetScriptComplieZ(ScriptKind ist)
		{
			V8Script? ret = null;
			ret = m_compleZ[(int)ist];
			return ret;
		}
		public void SetScriptComplieZ(ScriptKind ist, V8Script? cpl)
		{
			m_compleZ[(int)ist] = cpl;
		}
		public string GetScriptCode(int idx)
        {
            string ret = "";
            if (idx >= 0 && idx < m_ScriptKinds.Length)
            {
                ret = m_Codes[(int)m_ScriptKinds[idx]];
            }
            return ret;
        }
        public string[] GetScriptCodes()
        {
            string[] ret = new string[0];
            if (m_ScriptKinds.Length > 0)
            {
                ret = new string[m_ScriptKinds.Length];
                for (int i = 0; i < m_ScriptKinds.Length; i++)
                {
                    ret[i] = m_Codes[(int)m_ScriptKinds[i]];
                }
            }
            return ret;
        }
        public string SetScriptCode(int idx, string tx)
        {
            string ret = "";
            if (idx >= 0 && idx < m_ScriptKinds.Length)
            {
                int v = (int)m_ScriptKinds[idx];
				if (m_Codes[v] != tx)
				{
					m_Codes[v] = tx;
                    if (m_compleZ[v] != null) 
                    {
						m_compleZ[v].Dispose();
                        m_compleZ[v] = null;
                    }
				}
			}
            return ret;
        }
        public void SetScriptCodes(string[] txs)
        {
            if ((m_ScriptKinds.Length == txs.Length)&&(m_ScriptKinds.Length>0))
            {
                for (int i = 0; i < txs.Length; i++)
                {
                    SetScriptCode(i, txs[i]);
				}
            }
        }
        public HyperScriptCode()
        {
            int cnt = Enum.GetNames(typeof(ScriptKind)).Length;
			m_Codes=new string[cnt];
			m_compleZ = new V8Script[cnt];
            for(int i=0; i<cnt; i++)
            {
                m_Codes[i] = "";
                m_compleZ[i] = null;
			}

		}
        public int IndexOfScriptKind(ScriptKind sk)
        {
            int ret = -1;
            if(m_ScriptKinds.Length > 0)
            {
				for (int i=0; i< m_ScriptKinds.Length;i++)
                {
                    if (m_ScriptKinds[i]==sk)
                    {
                        ret = i;
                        break;
                    }
                }
			}
            return ret;
		}
		private void GetValidSprictNames(InScriptBit sc)
        {
            List<string> list = new List<string>();
            List<ScriptKind> slist = new List<ScriptKind>();
            string[] names = Enum.GetNames(typeof(ScriptKind));
            for (int i = 0; i < 16; i++)
            {
                InScriptBit v = (InScriptBit)(0x01 << i);
                if ((sc & v) == v)
                {
                    ScriptKind sk = (ScriptKind)i;
                    list.Add(names[i]);
                    slist.Add(sk);
                }

            }
            m_ValidSprictNames = list.ToArray();
            m_ScriptKinds = slist.ToArray();
        }
    }
}
