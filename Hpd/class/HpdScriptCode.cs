using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Immutable;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RoslynPad.Roslyn;
using RoslynPad.Editor;
using System.Text.Json.Nodes;

namespace Hpd
{
    [Flags]
    public enum ScriptTypeBit
    {
        None
            = 0b_0000_0000_0000_0000,
        Load
            = 0b_0000_0000_0000_0001,
        Closed
            = 0b_0000_0000_0000_0010,
        MouseClick
            = 0b_0000_0000_0000_0100,
        MouseDoubleClick
            = 0b_0000_0000_0000_1000,
        ValueChanged
            = 0b_0000_0000_0001_0000,
        DragDrop
            = 0b_0000_0000_0010_0000,
        KeyPress
            = 0b_0000_0000_0100_0000,
    }
    public enum ScriptType
    {
        Load,
        Closed,
        MouseClick,
        MouseDoubleClick,
        ValueChanged,
        DragDrop,
        KeyPress,

    }
    public class HpdScriptCodeBase
    {
        //public ScriptType? ScriptType =null;
        //public string ScriptTypeName = "";

        public string Code { get; set; } = "";
        public Script? Script { get; set; } = null;
        public HpdScriptCodeBase()
        {

        }
        public HpdScriptCodeBase(/*ScriptType st,string n,*/ string code, Script? script)
        {
            //ScriptType = st;
            //ScriptTypeName = n;
            Code = code;
            Script = script;
        }
    }
    public class HpdScriptCode
    {
        private ScriptTypeBit m_ScriptTypeBit = ScriptTypeBit.None;
        public ScriptTypeBit ScriptTypeBit { get { return m_ScriptTypeBit; } }
        private ScriptType[] m_ScriptTypes = new ScriptType[0];
        public ScriptType[] ScriptTypes { get { return m_ScriptTypes; } }
        private string[] m_ScriptTypeNames = new string[0];
        public string[] ScriptTypeNames { get { return m_ScriptTypeNames; } }

        private HpdScriptCodeBase[] m_scripts = new HpdScriptCodeBase[0];
        public HpdScriptCodeBase Scripts(ScriptType st)
        {
            int idx = (int)st;
            HpdScriptCodeBase ret = new HpdScriptCodeBase();
            if (idx >= 0 && idx < m_scripts.Length)
            {
                ret = m_scripts[idx];
            }
            return ret;
        }
        public HpdScriptCode()
        {
            Init();
        }
        public HpdScriptCode(ScriptTypeBit tb)
        {
            Init();
            SetSTypes(tb);
        }
        public void Init()
        {
            m_scripts = new HpdScriptCodeBase[Enum.GetValues(typeof(ScriptType)).Length];
            for (int i = 0; i < m_scripts.Length; i++)
            {
                m_scripts[i] = new HpdScriptCodeBase();
            }

        }
        public void SetSTypes(ScriptTypeBit tb)
        {
            m_ScriptTypeBit = tb;
            if (tb == 0)
            {

                m_ScriptTypes = new ScriptType[0];
                m_ScriptTypeNames = new string[0];
                return;
            }
            List<ScriptType> ts = new List<ScriptType>();
            List<string> ss = new List<string>();
            string[] ns = Enum.GetNames(typeof(ScriptType));
            int bit = 1;
            for (int i = 0; i < ns.Length; i++)
            {
                if (((int)tb & bit) == bit)
                {
                    ts.Add((ScriptType)i);
                    ss.Add(ns[i]);
                }
                bit <<= 1;
            }
            m_ScriptTypes = ts.ToArray();
            m_ScriptTypeNames = ss.ToArray();
        }
        public JsonObject? ToJson()
        {
            JsonObject obj = new JsonObject();

            obj.Add("ScriptTypeBit", (int)ScriptTypeBit);
            string[] ns = Enum.GetNames(typeof(ScriptType));
            for (int i = 0; i < ns.Length; i++)
            {
                obj.Add(ns[i], m_scripts[0].Code);
            }
            return obj;
        }
        public void FromJson(JsonObject? obj)
        {
            Init();
            m_ScriptTypeBit = ScriptTypeBit.None;
            if (obj == null) return;
            object? o = null;
            string key = "ScriptTypeBit";
            if (obj.ContainsKey(key))
            {
                o = obj[key].GetValue<int?>();
                if (o != null)
                {
                    SetSTypes((ScriptTypeBit)o);
                }
            }
            string[] ns = Enum.GetNames(typeof(ScriptType));
            for (int i = 0; i < ns.Length; i++)
            {
                key = ns[i];
                o = null;
                if (obj.ContainsKey(key))
                {
                    o = obj[key].GetValue<string?>();
                }
                m_scripts[i].Code = "";
                m_scripts[i].Script = null;
                if (o != null)
                {
                    m_scripts[i].Code = (string)o;
                }
            }

        }

    }
}
