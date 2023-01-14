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
		// ****************************************************************************
		public EditControlList? ControlList = null;
		public ScriptEditor? ScriptEdit = null;
		public JSOutputForm? OutputForm = null;
		public JSInputForm? InputForm = null;
		public EditAlignmentForm? AlignmentForm = null;

		public Rectangle ControlListBounds = new Rectangle(-1, -1, 0,0);
		public Rectangle ScriptEditBounds = new Rectangle(-1, -1, 0, 0);
		public Rectangle InputFormBounds = new Rectangle(-1, -1, 0, 0);
		public Rectangle OutputFormBounds = new Rectangle(-1, -1, 0, 0);
		public Rectangle AlignmentFormBounds = new Rectangle(-1, -1, 0, 0);
		public Font? InputFormFont = null;
		public Font? OutputFormFont = null;
		// ****************************************************************************

		// ****************************************************************************
		public void ExecuteCode(string code)
		{
			if (code != "")
			{

				Script.ExecuteCode(code);
			}
		}
		/*
		public void ExecuteScript(HyperScriptCode sc, ScriptKind sk)
		{
			Script.ExecuteScript(sc,sk);
		}
		*/
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
			if (v != null) Script_load = (String)v;

			v = jf.ValueAuto("Script_Shutdown", typeof(String).Name);
			if (v != null) Script_Closed = (String)v;

			JsonObject? mm = jf.ValueObject("Menu");
			if (mm != null)
			{
				m_menuBar.FromJson(mm);
			}
			
		}
	}
}
