using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;


namespace Hypowered
{
	public class ConnectData
	{
		public string Name { get; set; } = "";
		public string Prop { get; set; } = "";
		public ControlType ControlType { get; set; } = ControlType.Label;
		public string ConnectName { get; set; } = "";
		public bool Enabled
		{
			get 
			{
				return ((Name != "") && (Prop != "") && (ConnectName != "")&&(Name!=ConnectName));
			}
		}
		private string GetCTName()
		{
			string? p = Enum.GetName(typeof(ControlType), ControlType);
			if(p != null)
			{
				return p;
			}
			else
			{
				return "";
			}
		}
		public ConnectData()
		{
		}
		public ConnectData(JsonObject? jo)
		{
			FromJson(jo);
		}
		public ConnectData(string name, ControlType ct, string connectName)
		{
			if (name != connectName)
			{
				Name = name;
				ControlType = ct;
				Prop = GetCTName();
				ConnectName = connectName;
			}
		}
		public JsonObject ToJson()
		{
			var json = new JsonObject();
			json.Add("Name", Name);
			json.Add("ControlType",(int)ControlType);
			json.Add("ConnectName", ConnectName);
			return json;
		}
		public bool FromJson(JsonObject? obj)
		{
			Name = "";
			Prop = "";
			ConnectName = "";
			if (obj == null) return false;
			if (obj.ContainsKey("Name"))
			{
				string? n = obj["Name"].GetValue<string?>();
				if (n != null) Name = n;
			}
			if (obj.ContainsKey("Prop"))
			{
				int? n = obj["ControlType"].GetValue<int?>();
				if (n != null)
				{
					ControlType = (ControlType)n;
					Prop = GetCTName();
				}
			}
			if (obj.ContainsKey("ConnectName"))
			{
				string? n = obj["ConnectName"].GetValue<string?>();
				if (n != null) ConnectName = n;
			}
			return ((Name != "") && (Prop != "") && (ConnectName != ""));
		}
	}
	public class ConnectList
	{
		private List<ConnectData> m_Items= new List<ConnectData>();

		public int Count
		{
			get { return m_Items.Count; }
		}
		public ConnectList() 
		{
		}
		public int IndexOf(ConnectData cd)
		{
			int ret = -1;
			int cnt = 0;
			foreach (ConnectData data in m_Items)
			{
				if ((data.Name == cd.Name)
					&& (data.ControlType == cd.ControlType)
					&& (data.ConnectName == cd.ConnectName))
				{
					ret = cnt;
					break;
				}
				cnt++;
			}
			return ret;
		}
		public int IndexOfName(string s,ControlType ct, string d)
		{
			int ret = -1;
			int cnt = 0;
			foreach (ConnectData data in m_Items)
			{
				if ((data.Name == s)&&(data.ControlType == ct)&&(data.ConnectName==d))
				{
					ret = cnt;
					break;
				}
				cnt++;
			}
			return ret;
		}
		public bool AddConnect(Control.ControlCollection cc, string s, ControlType ct, string d)
		{
			bool ret = false;
			if(s==d) return false;
			int idx = IndexOfName(s,ct,d);
			if (idx >= 0) return true;
			Control[] cs1 = cc.Find(s, false);
			Control[] cs2 = cc.Find(d, false);
			if ((cs1.Length <= 0) || (cs2.Length <= 0)) return ret;
			string? sn = Enum.GetName(typeof(ControlType), ct);
			if(sn==null) return false;
			PropertyInfo? pi = cs1[0].GetType().GetProperty(sn);
			if (pi == null) return ret;
			if (pi.GetType() == cs2[0].GetType())
			{
				pi.SetValue(cs1, cs2[0]);
				ConnectData cd = new ConnectData(s, ct, d);
				m_Items.Add(cd);
				ret = true;
			}
			return ret;
		}
		public bool ConnectAt(Control.ControlCollection cc,int idx)
		{
			bool ret = false;
			if((idx<0) || (idx>=m_Items.Count)) return ret;

			Control[] cs1 = cc.Find(m_Items[idx].Name, false);
			Control[] cs2 = cc.Find(m_Items[idx].ConnectName, false);
			if ((cs1.Length <= 0) || (cs2.Length <= 0)) return ret;
			PropertyInfo? pi = cs1[0].GetType().GetProperty(m_Items[idx].Prop);
			if (pi == null) return ret;
			if (pi.GetType() == cs2[0].GetType())
			{
				pi.SetValue(cs1, cs2[0]);
				ret = true;
			}
			return ret;
		}
		public bool ConnectAll(Control.ControlCollection cc)
		{
			bool ret = false;
			if((cc.Count>0)&&(m_Items.Count>0))
			{
				int cnt = m_Items.Count;
				for(int i= m_Items.Count-1; i>=0; i--)
				{
					if( ConnectAt(cc,i)==false)
					{
						m_Items.RemoveAt(i);
					}
				}
				ret = (cnt == m_Items.Count);
			}
			return ret;
		}
		public JsonArray? ToJsonArray()
		{
			JsonArray ret = new JsonArray();
			if(m_Items.Count>0)
			{
				foreach(var cd in m_Items) 
				{
					JsonObject? obj = cd.ToJson();
					if(obj != null)
					{
						
						ret.Add(obj);
					}
				}
			}
			return ret;
		}
		public void FromJsonArray(JsonArray? ja)
		{
			if ((ja == null) || (ja.Count <= 0)) return;
			m_Items.Clear();
			foreach(var jo in ja)
			{
				if(jo==null) continue;
				ConnectData cs = new ConnectData(jo.AsObject());
				if(cs.Enabled)
				{
					int idx= IndexOf(cs);
					if (idx < 0)
					{
						m_Items.Add(cs);
					}
				}
			}
		}
	}
}
