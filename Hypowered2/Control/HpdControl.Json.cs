using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Hpd
{
	partial class HpdControl
	{
		public virtual JsonObject? ToJson()
		{
			JsonFile jf = new JsonFile();
			jf.SetValue(nameof(HpdType), (int)HpdType);
			jf.SetValue(nameof(ScriptCode), ScriptCode.ToJson());//Hpd.HpdScriptCode

			HpdButton? button = AsHpdButton;
			if (button!=null)
			{
				jf.SetValue(nameof(DialogResult), (Int32)button.DialogResult);
				jf.SetValue(nameof(FlatStyle), (Int32)button.FlatStyle);
			}
			HpdCheckBox? checkBox = AsHpdCheckBox;
			if (checkBox!=null)
			{
				jf.SetValue(nameof(DialogResult), (Boolean)checkBox.Checked);
			}

			jf.SetValue(nameof(Name), (String)Name);//System.String
			jf.SetValue(nameof(Text), (String)Text);//System.String
			jf.SetValue(nameof(IsDrawFrame), (Boolean)IsDrawFrame);//System.Boolean
			jf.SetValue(nameof(IsSaveFileName), (Boolean)IsSaveFileName);//System.Boolean
			if(IsSaveFileName)
				jf.SetValue(nameof(FileName), (String)FileName);//System.String
			jf.SetValue(nameof(Location), (Point)Location);//System.Drawing.Point
			jf.SetValue(nameof(Size), (Size)Size);//System.Drawing.Size
			jf.SetValue(nameof(BaseSize), (Size)BaseSize);//System.Drawing.Size
			jf.SetValue(nameof(SizePolicyHorizon), (int)SizePolicyHorizon);//Hpd.SizePolicy
			jf.SetValue(nameof(SizePolicyVertual), (int)SizePolicyVertual);//Hpd.SizePolicy
			jf.SetValue(nameof(Margin), (Padding)Margin);//System.Windows.Forms.Padding
			jf.SetValue(nameof(Padding), (Padding)Padding);//System.Windows.Forms.Padding
			jf.SetValue(nameof(Caption), (String)Caption);//System.String
			jf.SetValue(nameof(CaptionWidth), (Int32)CaptionWidth);//System.Int32
			jf.SetValue(nameof(Font), (Font)Font);//System.Drawing.Font
			jf.SetValue(nameof(Visible), (Boolean)Visible);//System.Boolean
			jf.SetValue(nameof(TextAligiment), (int)TextAligiment);//System.Drawing.StringAlignment
			jf.SetValue(nameof(TextLineAligiment), (int)TextLineAligiment);//System.Drawing.StringAlignment
			jf.SetValue(nameof(ForeColor), (Color)ForeColor);//System.Drawing.Color
			jf.SetValue(nameof(BackColor), (Color)BackColor);//System.Drawing.Color
			jf.SetValue(nameof(TabStop), (Boolean)TabStop);//System.Boolean
			jf.SetValue(nameof(Anchor), (int)Anchor);//System.Windows.Forms.AnchorStyles
			jf.SetValue(nameof(Dock), (int)Dock);//System.Windows.Forms.DockStyle
			jf.SetValue(nameof(CausesValidation), (Boolean)CausesValidation);//System.Boolean
			jf.SetValue(nameof(AccessibleDescription), (String)AccessibleDescription);//System.String
			jf.SetValue(nameof(AccessibleName), (String)AccessibleName);//System.String
			jf.SetValue(nameof(AccessibleRole), (int)AccessibleRole);//System.Windows.Forms.AccessibleRole
			jf.SetValue(nameof(MinimumSize), (Size)MinimumSize);//System.Drawing.Size
			jf.SetValue(nameof(AccessibleDefaultActionDescription), (String)AccessibleDefaultActionDescription);//System.String
			jf.SetValue(nameof(AllowDrop), (Boolean)AllowDrop);//System.Boolean
			jf.SetValue(nameof(AutoSize), (Boolean)AutoSize);//System.Boolean
			jf.SetValue(nameof(AutoScrollOffset), (Point)AutoScrollOffset);//System.Drawing.Point
			jf.SetValue(nameof(Capture), (Boolean)Capture);//System.Boolean
			jf.SetValue(nameof(Enabled), (Boolean)Enabled);//System.Boolean
			jf.SetValue(nameof(IsAccessible), (Boolean)IsAccessible);//System.Boolean
			jf.SetValue(nameof(MaximumSize), (Size)MaximumSize);//System.Drawing.Size
			jf.SetValue(nameof(RightToLeft), (int)RightToLeft);//System.Windows.Forms.RightToLeft
			jf.SetValue(nameof(TabIndex), (Int32)TabIndex);//System.Int32
			jf.SetValue(nameof(Top), (Int32)Top);//System.Int32
			jf.SetValue(nameof(UseWaitCursor), (Boolean)UseWaitCursor);//System.Boolean
			jf.SetValue(nameof(ImeMode), (int)ImeMode);//System.Windows.Forms.ImeMode
			return jf.Obj;
		}
		public virtual void FromJson(JsonObject jo)
		{
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("Name", typeof(String).Name);
			if (v != null) base.Name = (String)v;
			v = jf.ValueAuto("ScriptCode", typeof(JsonObject).Name);
			if (v != null) ScriptCode.FromJson((JsonObject)v);

			HpdButton? button = AsHpdButton;
			if (button != null)
			{
				v = jf.ValueAuto("DialogResult", typeof(Int32).Name);
				if (v != null) button.DialogResult = (DialogResult)v;
				v = jf.ValueAuto("FlatStyle", typeof(Int32).Name);
				if (v != null) button.FlatStyle = (FlatStyle)v;
			}
			HpdCheckBox? checkBox = AsHpdCheckBox;
			if (checkBox != null)
			{
				v = jf.ValueAuto("Checked", typeof(Boolean).Name);
				if (v != null) checkBox.Checked = (Boolean)v;
			}
			v = jf.ValueAuto("Text", typeof(String).Name);
			if (v != null) Text = (String)v;
			v = jf.ValueAuto("IsDrawFrame", typeof(Boolean).Name);
			if (v != null) IsDrawFrame = (Boolean)v;
			v = jf.ValueAuto("IsSaveFileName", typeof(Boolean).Name);
			if (v != null) IsSaveFileName = (Boolean)v;
			if (IsSaveFileName)
			{
				v = jf.ValueAuto("FileName", typeof(String).Name);
				if (v != null) FileName = (String)v;
			}
			v = jf.ValueAuto("Location", typeof(Point).Name);
			if (v != null) Location = (Point)v;
			v = jf.ValueAuto("Size", typeof(Size).Name);
			if (v != null) Size = (Size)v;
			v = jf.ValueAuto("BaseSize", typeof(Size).Name);
			if (v != null) BaseSize = (Size)v;
			v = jf.ValueAuto("SizePolicyHorizon", typeof(int).Name);
			if (v != null) SizePolicyHorizon = (SizePolicy)v;
			v = jf.ValueAuto("SizePolicyVertual", typeof(int).Name);
			if (v != null) SizePolicyVertual = (SizePolicy)v;
			v = jf.ValueAuto("Margin", typeof(Padding).Name);
			if (v != null) Margin = (Padding)v;
			v = jf.ValueAuto("Padding", typeof(Padding).Name);
			if (v != null) Padding = (Padding)v;
			v = jf.ValueAuto("Caption", typeof(String).Name);
			if (v != null) Caption = (String)v;
			v = jf.ValueAuto("CaptionWidth", typeof(Int32).Name);
			if (v != null) CaptionWidth = (Int32)v;
			v = jf.ValueAuto("Font", typeof(Font).Name);
			if (v != null) Font = (Font)v;
			v = jf.ValueAuto("Visible", typeof(Boolean).Name);
			if (v != null) Visible = (Boolean)v;
			v = jf.ValueAuto("TextAligiment", typeof(int).Name);
			if (v != null) TextAligiment = (StringAlignment)v;
			v = jf.ValueAuto("TextLineAligiment", typeof(int).Name);
			if (v != null) TextLineAligiment = (StringAlignment)v;
			v = jf.ValueAuto("ForeColor", typeof(Color).Name);
			if (v != null) ForeColor = (Color)v;
			v = jf.ValueAuto("BackColor", typeof(Color).Name);
			if (v != null) BackColor = (Color)v;
			v = jf.ValueAuto("TabStop", typeof(Boolean).Name);
			if (v != null) TabStop = (Boolean)v;
			v = jf.ValueAuto("Anchor", typeof(int).Name);
			if (v != null) Anchor = (AnchorStyles)v;
			v = jf.ValueAuto("Dock", typeof(int).Name);
			if (v != null) Dock = (DockStyle)v;
			v = jf.ValueAuto("CausesValidation", typeof(Boolean).Name);
			if (v != null) CausesValidation = (Boolean)v;
			v = jf.ValueAuto("AccessibleDescription", typeof(String).Name);
			if (v != null) AccessibleDescription = (String)v;
			v = jf.ValueAuto("AccessibleName", typeof(String).Name);
			if (v != null) AccessibleName = (String)v;
			v = jf.ValueAuto("AccessibleRole", typeof(int).Name);
			if (v != null) AccessibleRole = (AccessibleRole)v;
			v = jf.ValueAuto("MinimumSize", typeof(Size).Name);
			if (v != null) MinimumSize = (Size)v;
			v = jf.ValueAuto("AccessibleDefaultActionDescription", typeof(String).Name);
			if (v != null) AccessibleDefaultActionDescription = (String)v;
			v = jf.ValueAuto("AllowDrop", typeof(Boolean).Name);
			if (v != null) AllowDrop = (Boolean)v;
			v = jf.ValueAuto("AutoSize", typeof(Boolean).Name);
			if (v != null) AutoSize = (Boolean)v;
			v = jf.ValueAuto("AutoScrollOffset", typeof(Point).Name);
			if (v != null) AutoScrollOffset = (Point)v;
			v = jf.ValueAuto("Capture", typeof(Boolean).Name);
			if (v != null) Capture = (Boolean)v;
			v = jf.ValueAuto("Enabled", typeof(Boolean).Name);
			if (v != null) Enabled = (Boolean)v;
			v = jf.ValueAuto("IsAccessible", typeof(Boolean).Name);
			if (v != null) IsAccessible = (Boolean)v;
			v = jf.ValueAuto("MaximumSize", typeof(Size).Name);
			if (v != null) MaximumSize = (Size)v;
			v = jf.ValueAuto("RightToLeft", typeof(int).Name);
			if (v != null) RightToLeft = (RightToLeft)v;
			v = jf.ValueAuto("TabIndex", typeof(Int32).Name);
			if (v != null) TabIndex = (Int32)v;
			v = jf.ValueAuto("UseWaitCursor", typeof(Boolean).Name);
			if (v != null) UseWaitCursor = (Boolean)v;
			v = jf.ValueAuto("ImeMode", typeof(int).Name);
			if (v != null) ImeMode = (ImeMode)v;
		}
	}
}
