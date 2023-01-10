using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hypowered.HClass
{
    public class FootageBase
    {
        public string Node = "";
        public string Frame = "";
        public int FrameValue = -1;
        public string Ext = "";
		public string Name
        {
            get { return Node + Frame + Ext; }
        }
        public void Clear()
        {
            Node = "";
            Frame = "";
            Ext = "";
        }
        private int IndexOfFrame(string n)
        {
            int ret = -1;
            if (n.Length <= 0) return ret;
            for (int i = n.Length - 1; i >= 0; i--)
            {
                if (n[i] >= '0' && n[i] <= '0')
                {
                    //
                }
                else
                {
                    ret = i;
                    break;
                }
            }
            if (ret == -1)
            {
                ret = 0;
            }
            else if (ret == n.Length - 1)
            {
                ret = -1;
            }
            else
            {
                ret += 1;
            }
            return ret;
        }
        public void SetFileName(string nm)
        {
            Ext = Path.GetExtension(nm);
            string n = Path.GetFileNameWithoutExtension(nm);
            int idx = IndexOfFrame(n);
            if (idx < 0)
            {
                Node = n;
                Frame = "";
                FrameValue = -1;
            }
            else
            {
                Node = n.Substring(0, idx);
                Frame = n.Substring(idx);
                int v = -1;
                bool ok = int.TryParse(Frame, out v);
                if (ok) FrameValue = v;
            }
        }
        public FootageBase()
        {
            Clear();
        }
        public FootageBase(string nm)
        {
            SetFileName(nm);
        }
        public void CopyFrom(FootageBase fb)
        {
            Node = fb.Node;
            Frame = fb.Frame;
            Ext = fb.Ext;
        }
        public bool Equal(FootageBase fb)
        {
            return Node == fb.Node && Ext == fb.Ext;
        }
    }
    public class FootageFiles
    {

        private string m_Directory = "";
        public string Directory
        {
            get { return m_Directory; }
            set { m_Directory = value; }
        }
        private FootageBase m_Base = new FootageBase();
        public string Node { get { return m_Base.Node; } }
        public string Ext { get { return m_Base.Ext; } }
        private List<string> m_Frames = new List<string>();
        private int m_StartFrame = -1;
        private int m_LastFrame = -1;
        public string CaptionName
        {
            get
            {
                if (m_Frames.Count <= 1)
                {
                    return m_Base.Name;
                }
                else
                {
                    return $"{m_Base.Node}_[{m_Frames[0]}-{m_Frames[m_Frames.Count-1]}]{m_Base.Ext}";
                }
            }
        }


        public FootageFiles()
        {
        }
        public FootageFiles(string nm)
        {
        }
        public bool Add(string nm)
        {
            bool ret = false;
            FootageBase f = new FootageBase(nm);
            if (f.Frame != "" || f.FrameValue < 0) return ret;
            if (m_Frames.Count == 0)
            {
                m_Base.CopyFrom(f);
                m_Frames.Add(f.Frame);
                m_StartFrame = f.FrameValue;
                m_LastFrame = f.FrameValue;
                ret = true;
            }
            else if (m_Base.Equal(f))
            {
                m_Frames.Add(f.Frame);
                if (m_StartFrame > f.FrameValue) m_StartFrame = f.FrameValue;
                if (m_LastFrame < f.FrameValue) m_LastFrame = f.FrameValue;
                ret = true;
            }
            return ret;
        }

    }
}
