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
		public HyperLib Lib = new HyperLib();
		public HyperScript Script = new HyperScript();
		public HyperScriptCode ScriptCode = new HyperScriptCode();
		// ****************************************************************************
		public EditControlList? ControlList = null;
		public HyperScriptEditor? ScriptEdit = null;
		public JSOutputForm? OutputForm = null;
		public JSInputForm? InputForm = null;

		public Rectangle ControlListBounds = new Rectangle(-1, -1, 0,0);
		public Rectangle ScriptEditBounds = new Rectangle(-1, -1, 0, 0);
		public Rectangle InputFormBounds = new Rectangle(-1, -1, 0, 0);
		public Rectangle OutputFormBounds = new Rectangle(-1, -1, 0, 0);
		// ****************************************************************************

		// ****************************************************************************
		public void ExecuteCode(string code)
		{
			if (code != "")
			{

				Script.ExecuteCode(code);
			}
		}
		public void ExecuteStartup()
		{
			Script.ExecuteCode(ScriptCode.Code(ScriptKind.Load));
		}
		public void ExecuteMouseDoubleClick()
		{
			Script.ExecuteCode(ScriptCode.Code(ScriptKind.MouseDoubleClick));
		}
		public void ExecuteKeyPress()
		{
			Script.ExecuteCode(ScriptCode.Code(ScriptKind.KeyPress));
		}
		public void ExecuteShutdown()
		{
			Script.ExecuteCode(ScriptCode.Code(ScriptKind.Closed));
		}
		public void InitScript()
		{
			Script.Init();
			Script.InitForms(this);
			Script.InitControls(this);
		}
		public void SetInScript(InScriptBit s)
		{
			ScriptCode.SetInScript(s);
		}
		[Category("Hypowered_Script")]
		public InScriptBit InScript
		{
			get { return ScriptCode.InScript; }
		}
		[Category("Hypowered_Script")]
		public int ScriptCount
		{
			get { return ScriptCode.Count; }
		}
		[Category("Hypowered_Script")]
		public string[] ValidSprictNames
		{
			get { return ScriptCode.ValidSprictNames; }
		}
		[Category("Hypowered_Script")]
		public ScriptKind[] ScriptKinds
		{
			get { return ScriptCode.ScriptKinds; }
		}
		[Category("Hypowered_Script")]
		[Bindable(false)]
		public string Script_MouseDoubleClick
		{
			get { return ScriptCode.Script_MouseDoubleClick; }
			set { ScriptCode.Script_MouseDoubleClick = value; }
		}
		[Category("Hypowered_Script")]
		[Bindable(false)]
		public string Script_Startup
		{
			get { return ScriptCode.Script_Load; }
			set { ScriptCode.Script_Load = value; }
		}
		[Category("Hypowered_Script")]
		[Bindable(false)]
		public string Script_KeyPress
		{
			get { return ScriptCode.Script_KeyPress; }
			set { ScriptCode.Script_KeyPress = value; }
		}
		[Category("Hypowered_Script")]
		[Bindable(false)]
		public string Script_Shutdown
		{
			get { return ScriptCode.Script_Closed; }
			set { ScriptCode.Script_Closed = value; }
		}
		[Category("Hypowered_Form")]
		public new string Name
		{
			get { return base.Name; }
			set
			{
			}
		}

		[Category("Hypowered_Color")]
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

			jf.SetValue(nameof(Script_MouseDoubleClick), Script_MouseDoubleClick);//string
			jf.SetValue(nameof(Script_KeyPress), Script_KeyPress);//string
			jf.SetValue(nameof(Script_Startup), Script_Startup);//string
			jf.SetValue(nameof(Script_Shutdown), Script_Shutdown);//string


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
								Script.InitControls(this);
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
			if (v != null) SetName( (String)v);
			v = jf.ValueAuto("IsShowMenu", typeof(Boolean).Name);
			if (v != null) IsShowMenu = (Boolean)v;

			v = jf.ValueAuto("Script_MouseDoubleClick", typeof(String).Name);
			if (v != null) Script_MouseDoubleClick = (String)v;

			v = jf.ValueAuto("Script_KeyPress", typeof(String).Name);
			if (v != null) Script_KeyPress = (String)v;

			v = jf.ValueAuto("Script_Startup", typeof(String).Name);
			if (v != null) Script_Startup = (String)v;

			v = jf.ValueAuto("Script_Shutdown", typeof(String).Name);
			if (v != null) Script_Shutdown = (String)v;

			JsonObject? mm = jf.ValueObject("Menu");
			if (mm != null)
			{
				m_menuBar.FromJson(mm);
			}
			
		}
	}
}
