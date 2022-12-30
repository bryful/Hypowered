using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms.Layout;

namespace Hypowered
{
    partial class HyperMainForm
	{
		// ****************************************************************************
		public HyperPictLib PictLib = new HyperPictLib();
		public HyperScript Script = new HyperScript();
		public HyperScriptCode ScriptCode = new HyperScriptCode();
		// ****************************************************************************
		public HyperPropForm? PropForm = null;
		public HyperControlList? ControlList = null;

		public Rectangle PropFormBounds = new Rectangle(-1, -1, 0, 0);
		public Rectangle ControlListBounds = new Rectangle(-1, -1, 0,0);
		public Rectangle ScriptEditBounds = new Rectangle(-1, -1, 0, 0);
		// ****************************************************************************
		// ****************************************************************************
		public void ExecuteCode(string code)
		{
			if (code != "")
			{

				Script.ExecuteCode(code);
			}
		}
		public void InitScript()
		{
			Script.Init();
			Script.InitControls(this.Controls);
		}
		public void SetInScript(InScript s)
		{
			ScriptCode.SetInScript(s);
		}
		[Category("Hypowerd_Script")]
		public InScript InScript
		{
			get { return ScriptCode.InScript; }
		}
		[Category("Hypowerd_Script")]
		public int ScriptCount
		{
			get { return ScriptCode.Count; }
		}
		[Category("Hypowerd_Script")]
		public string[] ValidSprictNames
		{
			get { return ScriptCode.ValidSprictNames; }
		}
		[Category("Hypowerd_Script")]
		public ScriptKind[] ScriptKinds
		{
			get { return ScriptCode.ScriptKinds; }
		}
		[Category("Hypowerd_Script")]
		public string Script_MouseClick
		{
			get { return ScriptCode.Script_MouseClick; }
			set { ScriptCode.Script_MouseClick = value; }
		}
		[Category("Hypowerd_Script")]
		public string Script_Startup
		{
			get { return ScriptCode.Script_Startup; }
			set { ScriptCode.Script_Startup = value; }
		}
		[Category("Hypowerd_Script")]
		public string Script_KeyPress
		{
			get { return ScriptCode.Script_KeyPress; }
			set { ScriptCode.Script_KeyPress = value; }
		}
		protected string m_FileName = "";
		[Category("Hypowerd_Form")]
		public string FileName
		{
			get { return m_FileName; }
		}
		[Category("Hypowerd_Form")]
		public new string Name
		{
			get { return base.Name; }
			set
			{
			}
		}

		[Category("Hypowerd_Color")]
		public bool IsShowMenu
		{
			get { return m_menuBar.Visible; }
			set { m_menuBar.Visible = value; this.Invalidate(); }
		}
		// ***********************************************************************************
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(Name), Name);//String
			jf.SetValue(nameof(IsShowMenu), IsShowMenu);//Boolean

			jf.SetValue(nameof(Script_MouseClick), Script_MouseClick);//string
			jf.SetValue(nameof(Script_KeyPress), Script_KeyPress);//string
			jf.SetValue(nameof(Script_Startup), Script_Startup);//string


			jf.SetValue("Menu", m_menuBar.ToJson());

			if (this.Controls.Count > 0)
			{
				JsonArray ja = new JsonArray();
				foreach(Control c in this.Controls) 
				{
					if (c is HyperMenuBar) continue;
					if(c is HyperControl)
					{
						
						HyperControl hc= (HyperControl)c;
						ja.Add(hc.ToJson());
					}
				}
				jf.SetValue("Controls", ja);
			}


			return jf.Obj;
		}

		// ****************************************************
		public bool SaveToFile(string p)
		{
			bool ret = false;
			try
			{
				string? js = ToJsonCode();
				if (js != null)
				{
					File.WriteAllText(p, js);
					ret = true;
				}
			}
			catch
			{
				ret = false;
			}
			return ret;
		}
		// ****************************************************
		public bool LoadFromFile(string p)
		{
			bool ret = false;

			try
			{
				if (File.Exists(p) == true)
				{
					string str = File.ReadAllText(p);
					if (str != "")
					{
						var doc = JsonNode.Parse(str);
						if (doc != null)
						{
							var Obj = (JsonObject?)doc;
							if(Obj != null)
							{
								FromJson(Obj);
								Script.Init();
								Script.InitControls(this.Controls);
								ret = true;
							}
						}
					}
				}
			}
			catch
			{
				ret = false;
			}
			return ret;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("Name", typeof(String).Name);
			if (v != null) base.Name = (String)v;
			v = jf.ValueAuto("IsShowMenu", typeof(Boolean).Name);
			if (v != null) IsShowMenu = (Boolean)v;

			v = jf.ValueAuto("Script_MouseClick", typeof(String).Name);
			if (v != null) Script_MouseClick = (String)v;

			v = jf.ValueAuto("Script_KeyPress", typeof(String).Name);
			if (v != null) Script_KeyPress = (String)v;

			v = jf.ValueAuto("Script_Startup", typeof(String).Name);
			if (v != null) Script_Startup = (String)v;
			JsonObject? mm = jf.ValueObject("Menu");
			if (mm != null)
			{
				m_menuBar.FromJson(mm);
			}
		}
	}
}
