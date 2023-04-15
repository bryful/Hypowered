using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Hypowered
{
	partial class HForm
	{
		public override JsonObject? ToJson()
		{
			JsonFile? jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(MainMenuVisible), (Boolean)MainMenuVisible);//System.Boolean
			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("MainMenuVisible", typeof(Boolean).Name);
			if (v != null) MainMenuVisible = (Boolean)v;

		}
	}
}
