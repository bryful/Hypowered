using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows.Forms.Layout;

namespace Hypowered
{
	partial class HForm
	{
		public override JsonObject? ToJson()
		{
			JsonFile? jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(MainMenuVisible), (Boolean)MainMenuVisible);//System.Boolean
			jf.SetValue(nameof(IsShowEdit), (Boolean)IsShowEdit);//System.Boolean

			if (this.Controls.Count > 1)
			{
				JsonArray ja = new JsonArray();
				for(int i=1; i<this.Controls.Count; i++)
				{
					if (this.Controls[i] is HControl)
					{
						HControl hc = (HControl)this.Controls[i];
						switch (hc.HType)
						{
							case HType.Button:
								ja.Add(((HButton)hc).ToJson());
								break;
							case HType.Label:
								ja.Add(((HLabel)hc).ToJson());
								break;
							case HType.TextBox:
								ja.Add(((HTextBox)hc).ToJson());
								break;
							case HType.PictureBox:
								ja.Add(((HPictureBox)hc).ToJson());
								break;
							case HType.IconButton:
								ja.Add(((HIconButton)hc).ToJson());
								break;
							default:
								ja.Add(hc.ToJson());
								break;

						}
					}
				}
				jf.SetValue("Controls", ja);
			}


			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("MainMenuVisible", typeof(Boolean).Name);
			if (v != null) MainMenuVisible = (Boolean)v;
			v = jf.ValueAuto("IsShowEdit", typeof(Boolean).Name);
			if (v != null) IsShowEdit = (Boolean)v;

			JsonArray? ja = jf.ValueArray("Controls");
			if ((ja != null)&&(ja.Count>0))
			{
				CHType cht = new CHType();
				foreach(var j in ja)
				{
					JsonObject? obj = (JsonObject?)j;
					if (obj != null)
					{
						if (obj.ContainsKey("HType"))
						{
							string? mt = obj["HType"].GetValue<string?>();
							if (mt != null)
							{
								cht.ValueStr = mt;
								HControl hc = CreateControl(cht.Value);
								hc.FromJson(obj);
								this.Controls.Add(hc);
							}
						}
					}
				}
				ChkControl();

			}
		}
		// ****************************************************
		public virtual string ToJsonCode()
		{
			JsonObject? jo = ToJson();
			if (jo == null) return "";
			var options = new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
				WriteIndented = true
			};
			return jo.ToJsonString(options);
		}
	}
}
