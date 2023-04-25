﻿using System;
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
			jf.SetValue(nameof(CanPropertyGrid), (Boolean)CanPropertyGrid);//System.Boolean
			jf.SetValue(nameof(SelectedColor), (Color)SelectedColor);//System.Boolean
			jf.SetValue(nameof(TargetColor), (Color)TargetColor);//System.Boolean
			jf.SetValue(nameof(GridSize), (Int32)GridSize);

			jf.SetValue(nameof(MainMenu), MainMenu.ToJson());

			if (this.Controls.Count > 1)
			{
				JsonArray ja = new JsonArray();
				for(int i=1; i<this.Controls.Count; i++)
				{
					if (this.Controls[i] is HControl)
					{
						HControl hc = (HControl)this.Controls[i];
						switch (hc.HCType)
						{
							case HCType.Button:
								ja.Add(((HButton)hc).ToJson());
								break;
							case HCType.Label:
								ja.Add(((HLabel)hc).ToJson());
								break;
							case HCType.TextBox:
								ja.Add(((HTextBox)hc).ToJson());
								break;
							case HCType.PictureBox:
								ja.Add(((HPictureBox)hc).ToJson());
								break;
							case HCType.IconButton:
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
			v = jf.ValueAuto("SelectedColor", typeof(Color).Name);
			if (v != null) SelectedColor = (Color)v;
			v = jf.ValueAuto("TargetColor", typeof(Color).Name);
			if (v != null) TargetColor = (Color)v;

			v = jf.ValueObject("MainMenu");
			if (v != null) MainMenu.FromJson( (JsonObject)v);
			
			MainMenu.SetHForm(this);

			JsonArray? ja = jf.ValueArray("Controls");
			if ((ja != null)&&(ja.Count>0))
			{
				CHCType cht = new CHCType();
				foreach(var j in ja)
				{
					JsonObject? obj = (JsonObject?)j;
					if (obj != null)
					{
						if (obj.ContainsKey("HCType"))
						{
							string? mt = obj["HCType"].GetValue<string?>();
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
			v = jf.ValueAuto("MainMenuVisible", typeof(Boolean).Name);
			if (v != null) MainMenuVisible = (Boolean)v;

			v = jf.ValueAuto("GridSize", typeof(Int32).Name);
			if (v != null) GridSize = (Int32)v;

			if ((MainMenu.CloseMenu.ScriptItem.Code == "") && (MainMenu.CloseMenu.FuncType == null))
			{
				MainMenu.CloseMenu.FuncType = ThisClose;
			}
			if ((MainMenu.MainFormMenu.ScriptItem.Code == "") && (MainMenu.MainFormMenu.FuncType == null))
			{
				MainMenu.MainFormMenu.FuncType = ShowMainMenu;
			}
			ChkControl();
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
