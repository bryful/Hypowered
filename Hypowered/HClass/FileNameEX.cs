using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hypowered.HClass
{
    public class FileNameEX
    {
        private string m_FullPath = "";
        public string FullPath { get { return m_FullPath; } }
        private string m_RelativePath = "";
        public string RelativePath { get { return m_RelativePath; } }
        public void Clear()
        {
            m_FullPath = "";
            m_RelativePath = "";
        }
        public string Path
        {
            get
            {
                string ret = m_FullPath;
                if (File.Exists(m_FullPath) == false)
                {
                    if (File.Exists(m_RelativePath))
                    {
                        ret = m_RelativePath;
                    }
                    else
                    {
                        ret = "";
                    }
                }
                return ret;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_FullPath = "";
                    m_RelativePath = "";
                    return;
                }
                FileInfo fi = new FileInfo(value);
                m_FullPath = fi.FullName;
                m_RelativePath = "";
                FileInfo? fip = new FileInfo(Application.ExecutablePath);
                if (fip != null)
                {
                    string? s = System.IO.Path.GetDirectoryName(fip.FullName);
                    if (s != null)
                    {
                        m_RelativePath = System.IO.Path.GetRelativePath(m_FullPath, s);
                    }
                }
            }
        }
        public FileNameEX()
        {

        }
        public FileNameEX(string p)
        {
            Path = p;
        }
    }
}
