using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hpd
{
	/// <summary>
	/// 関数ポインタ代わりのデリゲート
	/// </summary>
	/// <returns></returns>
	public delegate bool FuncType();
	// ***************************************************************************
	/// <summary>
	/// 機能をカプセル化したクラス
	/// </summary>
	public class FuncItem
	{
		private string m_EngName = "";
		/// <summary>
		/// 機能の英語名、関数の名前から獲得
		/// </summary>
		public string EngName { get { return m_EngName; }  }
		private string m_JapName = "";
		/// <summary>
		/// 機能の日本語名
		/// </summary>
		public string JapName 
		{ 
			get { return m_JapName; }
			set { m_JapName = value; }
		}

		/// <summary>
		/// 機能の名前。OSによって英語名・日本名を切り替える
		/// </summary>
		public string Caption
		{
			get
			{
				string ret = m_EngName;
				if ((JapName!="")&& (System.Globalization.CultureInfo.CurrentUICulture.Name == "ja-JP"))
				{
					ret = JapName;
				}
				return ret;
			}
		}

		/// <summary>
		/// 実際の機能。関数ポインタ
		/// </summary>
		public FuncType? Func { get; set; }
		/// <summary>
		/// ショートカットキー
		/// </summary>
		private Keys[] m_Keys = new Keys[2];

		/// <summary>
		/// ショートカットキー
		/// </summary>
		public Keys[] KeyArray
		{
			get { return m_Keys; }
			set
			{
				m_Keys = new Keys[2];
				m_Keys[1] = Keys.None;
				m_Keys[0] = Keys.None;
				if (value.Length >= 1) m_Keys[0] = value[0];
				if (value.Length >= 2) m_Keys[1] = value[1];
				
			}
		}
		public Keys KeysFirst
		{
			get { return m_Keys[0]; }
			set { m_Keys[0] = value; }
		}
		public Keys KeysSecond
		{
			get { return m_Keys[1]; }
			set { m_Keys[1] = value; }
		}
		public Keys GetKey(int idx)
		{
			if ((idx >= 0) && (idx < m_Keys.Length)) {
				return m_Keys[idx];
			}
			else
			{
				return Keys.None;
			}
		}
		public void SetKey(int idx,Keys k)
		{
			if ((idx >= 0) && (idx < m_Keys.Length))
			{
				m_Keys[idx] = k;
			}
		}
		public FuncItem(FuncType fnc, Keys key, string JapN="")
		{
			Func = fnc;
			m_EngName = fnc.Method.Name;
			m_JapName = JapN;
			m_Keys = new Keys[] { key,Keys.None };
		}
		public FuncItem(FuncType fnc, Keys key0, Keys key1, string japN = "")
		{
			Func = fnc;
			m_EngName = fnc.Method.Name;
			m_JapName = japN;
			m_Keys = new Keys[] { key0,key1 };
		}
		public FuncItem(FuncItem item)
		{
			Copy(item);
		}
		public void Copy(FuncItem item)
		{
			Func = item.Func;
			m_EngName = item.EngName;
			m_JapName = item.JapName;
			m_Keys = new Keys[item.KeyArray.Length];
			if(item.KeyArray.Length > 0)
			{
				for (int i=0; i< item.KeyArray.Length;i++)
				{
					m_Keys[i] = item.m_Keys[i];
				}
			}
		}
		/// <summary>
		/// 同じショートカットキーがあるか
		/// </summary>
		/// <param name="k">探すKeys</param>
		/// <returns></returns>
		public bool IsKey(Keys k)
		{
			bool ret = false;
			if(m_Keys.Length>0)
			{
				foreach(Keys k2 in m_Keys)
				{
					if(k2==k)
					{
						ret = true;
						break;
					}
				}
			}
			return ret;
		}
	}

	// ***************************************************************************
	/// <summary>
	/// 機能を管理するクラス
	/// </summary>
	public class HpdFuncs
	{
		private  FuncItem[] m_FuncItems = new FuncItem[0];
		public FuncItem[] FuncItems
		{
			get { return m_FuncItems; }
			set { m_FuncItems = value; }
		}
		public FuncItem? Items(int idx)
		{
			FuncItem? ret = null;
			if((idx>=0)&&(idx< m_FuncItems.Length))
			{
				ret = m_FuncItems[idx];
			}
			return ret;
		}
		public void SetItems(int idx,FuncItem f)
		{
			if ((idx >= 0) && (idx < m_FuncItems.Length))
			{
				m_FuncItems[idx]=f;
			}
		}
		public void SetJapName(int idx,string js)
		{
			if ((idx >= 0) && (idx < m_FuncItems.Length))
			{
				m_FuncItems[idx].JapName =js;
			}
		}
		public int Count
		{
			get
			{
				return m_FuncItems.Length;
			}
		}
		// ********************************************************************
		public string[] Names
		{
			get
			{
				string[] ret = new string[m_FuncItems.Length];
				if(m_FuncItems.Length>0)
				{
					for(int i=0; i< m_FuncItems.Length;i++)
					{
						if ((m_FuncItems[i] != null) && (m_FuncItems[i].Func != null)){
							ret[i] = m_FuncItems[i].Func.Method.Name;
						}else
						{
							ret[i] = "";
						}
					}
				}
				return ret;
			}
		}
		// ********************************************************************
		public HpdFuncs()
		{
			m_FuncItems = new FuncItem[0]; ;
		}
		// ********************************************************************
		public void CopyFrom(HpdFuncs f)
		{
			m_FuncItems = new FuncItem[ f.m_FuncItems.Length];
			if(f.m_FuncItems.Length>0)
			{
				for(int i=0; i<f.m_FuncItems.Length;i++)
				{

					m_FuncItems[i] = new FuncItem(f.m_FuncItems[i]);
				}
			}

		}
		// ********************************************************************
		/// <summary>
		/// ショートカットキーを探す
		/// </summary>
		/// <param name="k">FunctionItem自体</param>
		/// <returns></returns>
		public FuncItem? FindKeys(Keys k)
		{
			FuncItem? ret =null;
			if(m_FuncItems.Length>0)
			{
				foreach(FuncItem item in m_FuncItems)
				{
					if (item.IsKey(k) == true)
					{
						ret = item;
						break;
					}
				}
			}
			return ret;
		}
		/// <summary>
		/// 初期化
		/// </summary>
		private void Init()
		{
			m_FuncItems = new FuncItem[0]; ;
		}
		/// <summary>
		/// 関数を探す
		/// </summary>
		/// <param name="name">インデックス</param>
		/// <returns></returns>
		public int IndexOfFunc(string name)
		{
			int ret = -1;
			if(m_FuncItems.Length > 0)
			{
				for(int i= 0; i < m_FuncItems.Length; i++)
				{
					if (string.Compare(m_FuncItems[i].EngName, name, true) == 0)
					{
						ret=i;
						break;
					}
				}
			}
			return ret;
		}
		/// <summary>
		/// 関数を探す
		/// </summary>
		/// <param name="Engn"></param>
		/// <returns></returns>
		public FuncItem? FindFunc(string name)
		{
			FuncItem? ret = null;
			int idx = IndexOfFunc(name);
			if (idx >= 0) ret = m_FuncItems[idx];
			return ret;
		}
		/// <summary>
		/// 関数配列を設定
		/// </summary>
		/// <param name="fs"></param>
		public void SetFuncItems(FuncItem[] fs)
		{
			m_FuncItems = fs;
		}
		/// <summary>
		/// 関数配列をJsonに
		/// </summary>
		/// <returns></returns>
		public string ToJson()
		{
			JsonObject jo = new JsonObject();
			JsonArray ja = new JsonArray();

			foreach(FuncItem item in m_FuncItems)
			{
				JsonObject jo2 = new JsonObject();
				jo2.Add("name", item.EngName);
				jo2.Add("jap", item.JapName);
				JsonArray ja2 = new JsonArray();
				if(item.KeyArray.Length>0)
				{
					foreach(Keys k in item.KeyArray)
					{
						ja2.Add((int)k);
					}
				}
				jo2.Add("keys", ja2);
				ja.Add(jo2);
			}
			jo.Add("funcList", ja);
			var options = new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
				WriteIndented = true
			};
			return jo.ToJsonString(options);
		}
		public bool FromJson(string json)
		{
			bool ret = false;
			try
			{
				if ((json == null) || (json == "")) return ret;
				var doc = JsonNode.Parse(json);
				if (doc == null) return ret;
				JsonObject jo = (JsonObject)doc;


				string key = "funcList";
				if (jo.ContainsKey(key) == false) return ret;
				JsonArray? ja = jo[key].AsArray();
				if(ja==null) return ret;
				List<string> names = new List<string>();
				List<string> jnames = new List<string>();
				List<Keys[]> ks = new List<Keys[]>();

				if (ja.Count>0)
				{
					foreach(var item in ja)
					{
						JsonObject? jn = (JsonObject?)item;
						if (jn == null) continue;
						key = "name";
						if (jn.ContainsKey(key) == false) continue;
						string nm = jn[key].GetValue<string>();
						key = "jap";
						string jnm = "";
						if (jn.ContainsKey(key) == true)
						{
							jnm= jn[key].GetValue<string>();
						}
						key = "keys";
						Keys[] kk = new Keys[0];
						if(jn.ContainsKey(key))
						{
							JsonArray ja2 = jn[key].AsArray();
							if(ja2.Count>0)
							{
								kk = new Keys[2];
								kk[0] = Keys.None;
								kk[1] = Keys.None;
								if (ja2.Count >= 1) kk[0] = (Keys)ja2[0].GetValue<int>();
								if (ja2.Count >= 2) 
									kk[1] = (Keys)ja2[1].GetValue<int>();
							}
						}
						names.Add(nm);
						jnames.Add(jnm);
						ks.Add(kk);
					}
					if(names.Count > 0)
					{
						for (int i=0; i< names.Count;i++)
						{
							int idx = IndexOfFunc(names[i]);
							if(idx>=0)
							{
								m_FuncItems[idx].JapName = jnames[i];
								m_FuncItems[idx].KeyArray = ks[i];
							}
						}
					}
					ret = true;
				}
			}
			catch
			{
				ret = false;
			}
			return ret;
		}
		// **********************
		public bool Save(string p)
		{
			bool ret = false;
			try
			{
				if(File.Exists(p)) File.Delete(p);
				File.WriteAllText(p, ToJson());
				ret = File.Exists(p);
			}
			catch
			{
				ret = false;
			}
			return ret;
		}
		public bool Load(string p)
		{
			bool ret = false;

			try
			{
				if (File.Exists(p) == true)
				{
					Encoding enc = new UTF8Encoding(false);
					string str = File.ReadAllText(p,enc);
					if (str != "")
					{
						ret = FromJson(str);
					}
				}
			}
			catch
			{
				ret = false;
			}
			return ret;
		}
	}
}
