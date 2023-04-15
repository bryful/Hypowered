using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Hypowered
{
    public class FootageBase
    {
		private bool m_IsFolder = false;
		public bool IsFolder { get { return m_IsFolder; } }
		private bool m_Exists = false;
		public bool Exists { get { return m_Exists; } }
		private string m_Name= "";
		public string Name { get { return m_Name; } }
		private string m_Node = "";
		public string Node { get { return m_Node; } }
		private string m_Frame = "";
		public string Frame { get { return m_Frame; } }
		private int m_FrameValue = -1;
		public int FrameValue { get { return m_FrameValue; } }
		private string m_Ext = "";
		public string Ext { get { return m_Ext; } }
		public void Clear()
        {
			m_IsFolder = false;
			m_Exists = false;
			m_Name = "";
			m_Node = "";
            m_Frame = "";
            m_Ext = "";
        }
        public void SetParent()
        {
            m_Exists= true;
            m_IsFolder= true;
        }
        private int IndexOfFrame(string n)
        {
            int ret = -1;
            if (n.Length <= 0) return ret;
            for (int i = n.Length - 1; i >= 0; i--)
            {
                if (n[i] >= '0' && n[i] <= '9')
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
            Clear();
            if(File.Exists(nm))
            {
                m_IsFolder= false;
                m_Exists= true;
            }else if (Directory.Exists(nm))
            {
				m_IsFolder = true;
				m_Exists = true;
			}
			m_Name = Path.GetFileName(nm);
			m_Ext = Path.GetExtension(nm);
            string n = Path.GetFileNameWithoutExtension(nm);
            int idx = IndexOfFrame(n);
            if (idx < 0)
            {
                m_Node = n;
                m_Frame = "";
                m_FrameValue = -1;
            }
            else
            {
                m_Node = n.Substring(0, idx);
                m_Frame = n.Substring(idx);
                int v = -1;
                bool ok = int.TryParse(m_Frame, out v);
                if (ok) m_FrameValue = v;
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
            m_Node = fb.m_Node;
            m_Frame = fb.m_Frame;
            m_Ext = fb.m_Ext;
        }
        public bool Equal(FootageBase fb)
        {
            return (
                (m_Node == fb.m_Node) &&
                (m_Ext == fb.m_Ext)
                
				);
        }
    }
    public class FootageFiles
    {
		private bool m_IsParent = false;
		public bool IsParent { get { return m_IsParent; } }

		public bool IsFolder { get { return m_Base.IsFolder; } }
		public bool Exists { get { return m_Base.Exists; } }

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
                    string nd = m_Base.Node;
                    if(nd.Length>0)
                    {
                        if (nd[nd.Length - 1] != '_') nd = nd + "_";
                    }
                    return $"{nd}[{m_Frames[0]}-{m_Frames[m_Frames.Count-1]}]{m_Base.Ext}";
                }
            }
        }
		public string FullName
		{
			get
			{
                if(m_IsParent)
                {
                    return m_Directory;
                }
                else
                {
					if (m_Directory != "")
					{
						return Path.Combine(m_Directory, m_Base.Name);
					}
					else
					{
						return m_Base.Name;
					}
				}
			}
		}
		public override string ToString()
        {
            if(IsParent)
            {
				return $"<Parent>";
			}
			else if(IsFolder)
            {
				return $"<{CaptionName}>";
			}
			else
            {
				return CaptionName;
			}
		}
       
        public FootageFiles()
        {
        }
        public void SetParetn(string? p)
        {
            if (p != null)
            {
                m_IsParent = true;
                m_Directory = p;
                m_Base.SetParent();
            }
        }
        public FootageFiles(string nm)
        {
			m_Base.SetFileName(nm);
			string? pp = Path.GetDirectoryName(nm);
			if (pp != null) { m_Directory = pp; } else { m_Directory = ""; }
		}
        public bool Add(string nm)
        {
            bool ret = false;
			if (IsFolder) return ret;
			FootageBase f = new FootageBase(nm);
            if ((f.IsFolder) || (f.Frame == "") || (f.FrameValue < 0)) return ret;
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
