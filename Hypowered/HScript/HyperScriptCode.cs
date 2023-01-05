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
        Startup = 0,
        MouseClick,
        MouseDoubleClick,
        SelectedIndexChanged,
        CurrentDirChanged,
        ValueChanged,
        KeyPress,
		Shutdown,
	}
    [Flags]
    public enum InScriptBit
    {
        None = 0,
        Startup
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
		Shutdown
			= 0b_0000_1000_0000,
	}
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
        private string[] m_Codes = new string[] { "", "", "", "", "", "", "","" };
        public string Code(ScriptKind sk)
        {
            return m_Codes[(int)sk];
		}
        public string Script_Startup
        {
            get { return m_Codes[(int)ScriptKind.Startup]; }
            set { m_Codes[(int)ScriptKind.Startup] = value; }
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
		public string Script_Shutdown
		{
			get { return m_Codes[(int)ScriptKind.Shutdown]; }
			set { m_Codes[(int)ScriptKind.Shutdown] = value; }
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
                m_Codes[(int)m_ScriptKinds[idx]] = tx;
            }
            return ret;
        }
        public void SetScriptCodes(string[] txs)
        {
            if (m_ScriptKinds.Length == txs.Length)
            {
                for (int i = 0; i < txs.Length; i++)
                {
                    m_Codes[(int)m_ScriptKinds[i]] = txs[i];
                }
            }
        }
        public HyperScriptCode()
        {
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
