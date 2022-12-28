﻿using Microsoft.ClearScript.V8;
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
    partial class HyperForm
	{
		public HyperScriptCode ScriptCode = new HyperScriptCode();
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

		protected int m_TargetIndex = -1;
		[Category("Hypowerd_Form")]
		public int TargetIndex
		{
			get { return m_TargetIndex; }
			set
			{
				if (m_TargetIndex != value)
				{
					m_TargetIndex = value;
					OnTargetChanged(new TargetChangedEventArgs(m_TargetIndex, TargetControl));
				}
				this.Invalidate();
			}
		}
		private string m_FileName = "Home";
		[Category("Hypowerd_Form")]
		public string FileName
		{
			get { return m_FileName; }
			set
			{
				m_FileName = value;
				base.Name = Path.GetFileNameWithoutExtension(value);
			}
		}
		[Category("Hypowerd_Form")]
		public new string Name
		{
			get { return base.Name; }
			set
			{
				if (m_FileName == "")
				{
					m_FileName = value;
				}
				else
				{
					string? d = Path.GetDirectoryName(m_FileName);
					string e = Path.GetExtension(m_FileName);
					m_FileName = value + e;
					if (d != null)
					{
						m_FileName = Path.Combine(d, m_FileName);
					}

				}
				base.Name = value;

			}
		}

		[Browsable(false)]
		public HyperControl? TargetControl
		{
			get
			{
				HyperControl? ret = null;
				if ((m_TargetIndex >= 0) && (m_TargetIndex < this.Controls.Count))
				{
					if (this.Controls[m_TargetIndex] is HyperControl)
					{
						ret = (HyperControl)this.Controls[m_TargetIndex];
					}
				}
				return ret;
			}
		}
		protected Padding m_FrameWeight = new Padding(1, 1, 1, 1);
		[Category("Hypowerd")]
		public Padding FrameWeight
		{
			get { return m_FrameWeight; }
			set { m_FrameWeight = value; this.Invalidate(); }
		}
		[Category("Hypowerd_Form")]
		public new Size Size
		{
			get { return base.Size; }
			set { base.Size = value; this.Invalidate(); }
		}
		private Color m_SelectedColor = Color.Red;
		[Category("Hypowerd_Color")]
		public Color SelectedColor
		{
			get { return m_SelectedColor; }
			set { m_SelectedColor = value; this.Invalidate(); }
		}
		private Color m_TargetColor = Color.Blue;
		[Category("Hypowerd_Color")]
		public Color TargetColor
		{
			get { return m_TargetColor; }
			set { m_TargetColor = value; this.Invalidate(); }
		}
		[Category("Hypowerd_Color")]
		public bool IsShowMenu
		{
			get { return m_menuBar.Visible; }
			set { m_menuBar.Visible = value; this.Invalidate(); }
		}
		// ***********************************************************************************
		[Browsable(false)]
		public new bool KeyPreview
		{
			get { return base.KeyPreview; }
			set { }
		}
		private bool ShouldSerializeKeyPreview()
		{
			return false;
		}
		public virtual JsonObject ToJson()
		{
			JsonObject jo = new JsonObject();
			JsonFile jf = new JsonFile(jo);
			jf.SetValue(nameof(FileName), FileName);//String
			jf.SetValue(nameof(Name), Name);//String
			jf.SetValue(nameof(Size), Size);//Size
			jf.SetValue(nameof(SelectedColor), SelectedColor);//Color
			jf.SetValue(nameof(TargetColor), TargetColor);//Color
			jf.SetValue(nameof(IsShowMenu), IsShowMenu);//Boolean
			jf.SetValue(nameof(KeyPreview), KeyPreview);//Boolean
			jf.SetValue(nameof(AllowTransparency), AllowTransparency);//Boolean
			jf.SetValue(nameof(AutoScaleBaseSize), AutoScaleBaseSize);//Size
			jf.SetValue(nameof(AutoScroll), AutoScroll);//Boolean
			jf.SetValue(nameof(AutoSize), AutoSize);//Boolean
			jf.SetValue(nameof(BackColor), BackColor);//Color
			jf.SetValue(nameof(FormBorderStyle), FormBorderStyle);//FormBorderStyle
			jf.SetValue(nameof(ControlBox), ControlBox);//Boolean
			jf.SetValue(nameof(Location), Location);//Point
			jf.SetValue(nameof(MaximumSize), MaximumSize);//Size
			jf.SetValue(nameof(Margin), Margin);//Padding
			jf.SetValue(nameof(MinimumSize), MinimumSize);//Size
			jf.SetValue(nameof(MaximizeBox), MaximizeBox);//Boolean
			jf.SetValue(nameof(MinimizeBox), MinimizeBox);//Boolean
			jf.SetValue(nameof(Opacity), Opacity);//Double
			jf.SetValue(nameof(ShowInTaskbar), ShowInTaskbar);//Boolean
			jf.SetValue(nameof(ShowIcon), ShowIcon);//Boolean
			jf.SetValue(nameof(SizeGripStyle), (int)SizeGripStyle);//SizeGripStyle
			jf.SetValue(nameof(StartPosition), (int)StartPosition);//FormStartPosition
			jf.SetValue(nameof(TabIndex), TabIndex);//Int32
			jf.SetValue(nameof(TabStop), TabStop);//Boolean
			jf.SetValue(nameof(Text), Text);//String
			jf.SetValue(nameof(TopMost), TopMost);//Boolean
			jf.SetValue(nameof(TransparencyKey), TransparencyKey);//Color
			jf.SetValue(nameof(WindowState), (int)WindowState);//FormWindowState
			jf.SetValue(nameof(AutoScaleMode), (int)AutoScaleMode);//AutoScaleMode
			jf.SetValue(nameof(AutoScrollMargin), AutoScrollMargin);//Size
			jf.SetValue(nameof(AutoScrollPosition), AutoScrollPosition);//Point
			jf.SetValue(nameof(AutoScrollMinSize), AutoScrollMinSize);//Size
			jf.SetValue(nameof(DisplayRectangle), DisplayRectangle);//Rectangle
			jf.SetValue(nameof(AllowDrop), AllowDrop);//Boolean
			jf.SetValue(nameof(Anchor), Anchor);//AnchorStyles
			jf.SetValue(nameof(AutoScrollOffset), AutoScrollOffset);//Point
			jf.SetValue(nameof(Dock), Dock);//DockStyle
			jf.SetValue(nameof(Enabled), Enabled);//Boolean
			jf.SetValue(nameof(Font), Font);//Font
			jf.SetValue(nameof(ForeColor), ForeColor);//Color
			jf.SetValue(nameof(UseWaitCursor), UseWaitCursor);//Boolean
			jf.SetValue(nameof(Visible), Visible);//Boolean
			jf.SetValue(nameof(Padding), Padding);//Padding
			jf.SetValue(nameof(ImeMode), ImeMode);//ImeMode
			jf.SetValue(nameof(FrameWeight), FrameWeight);

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
		public virtual string ToJsonCode()
		{
			return ToJson().ToJsonString();
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
								m_Script.Init();
								m_Script.InitControls(this.Controls);
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
		public virtual void FromJson(JsonObject jo)
		{
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("FileName", typeof(String).Name);
			if (v != null) FileName = (String)v;
			v = jf.ValueAuto("Name", typeof(String).Name);
			if (v != null) Name = (String)v;
			v = jf.ValueAuto("Size", typeof(Size).Name);
			if (v != null) Size = (Size)v;
			v = jf.ValueAuto("SelectedColor", typeof(Color).Name);
			if (v != null) SelectedColor = (Color)v;
			v = jf.ValueAuto("TargetColor", typeof(Color).Name);
			if (v != null) TargetColor = (Color)v;
			v = jf.ValueAuto("IsShowMenu", typeof(Boolean).Name);
			if (v != null) IsShowMenu = (Boolean)v;
			v = jf.ValueAuto("KeyPreview", typeof(Boolean).Name);
			if (v != null) KeyPreview = (Boolean)v;
			v = jf.ValueAuto("AllowTransparency", typeof(Boolean).Name);
			if (v != null) AllowTransparency = (Boolean)v;
			v = jf.ValueAuto("AutoScaleBaseSize", typeof(Size).Name);
			if (v != null) AutoScaleBaseSize = (Size)v;
			v = jf.ValueAuto("AutoScroll", typeof(Boolean).Name);
			if (v != null) AutoScroll = (Boolean)v;
			v = jf.ValueAuto("AutoSize", typeof(Boolean).Name);
			if (v != null) AutoSize = (Boolean)v;
			v = jf.ValueAuto("BackColor", typeof(Color).Name);
			if (v != null) BackColor = (Color)v;
			v = jf.ValueAuto("FormBorderStyle", typeof(Int32).Name);
			if (v != null) FormBorderStyle = (FormBorderStyle)v;
			v = jf.ValueAuto("ControlBox", typeof(Boolean).Name);
			if (v != null) ControlBox = (Boolean)v;
			v = jf.ValueAuto("HelpButton", typeof(Boolean).Name);
			if (v != null) HelpButton = (Boolean)v;
			v = jf.ValueAuto("Location", typeof(Point).Name);
			if (v != null) Location = (Point)v;
			v = jf.ValueAuto("MaximumSize", typeof(Size).Name);
			if (v != null) MaximumSize = (Size)v;
			v = jf.ValueAuto("Margin", typeof(Padding).Name);
			if (v != null) Margin = (Padding)v;
			v = jf.ValueAuto("MinimumSize", typeof(Size).Name);
			if (v != null) MinimumSize = (Size)v;
			v = jf.ValueAuto("MaximizeBox", typeof(Boolean).Name);
			if (v != null) MaximizeBox = (Boolean)v;
			v = jf.ValueAuto("MinimizeBox", typeof(Boolean).Name);
			if (v != null) MinimizeBox = (Boolean)v;
			v = jf.ValueAuto("Opacity", typeof(Double).Name);
			if (v != null) Opacity = (Double)v;
			v = jf.ValueAuto("ShowInTaskbar", typeof(Boolean).Name);
			if (v != null) ShowInTaskbar = (Boolean)v;
			v = jf.ValueAuto("ShowIcon", typeof(Boolean).Name);
			if (v != null) ShowIcon = (Boolean)v;
			v = jf.ValueAuto("SizeGripStyle", typeof(Int32).Name);
			if (v != null) SizeGripStyle = (SizeGripStyle)v;
			v = jf.ValueAuto("StartPosition", typeof(Int32).Name);
			if (v != null) StartPosition = (FormStartPosition)v;
			v = jf.ValueAuto("TabIndex", typeof(Int32).Name);
			if (v != null) TabIndex = (Int32)v;
			v = jf.ValueAuto("TabStop", typeof(Boolean).Name);
			if (v != null) TabStop = (Boolean)v;
			v = jf.ValueAuto("Text", typeof(String).Name);
			if (v != null) Text = (String)v;
			v = jf.ValueAuto("TopMost", typeof(Boolean).Name);
			if (v != null) TopMost = (Boolean)v;
			v = jf.ValueAuto("TransparencyKey", typeof(Color).Name);
			if (v != null) TransparencyKey = (Color)v;
			v = jf.ValueAuto("WindowState", typeof(Int32).Name);
			if (v != null) WindowState = (FormWindowState)v;
			v = jf.ValueAuto("AutoScrollMargin", typeof(Size).Name);
			if (v != null) AutoScrollMargin = (Size)v;
			v = jf.ValueAuto("AutoScrollPosition", typeof(Point).Name);
			if (v != null) AutoScrollPosition = (Point)v;
			v = jf.ValueAuto("AutoScrollMinSize", typeof(Size).Name);
			if (v != null) AutoScrollMinSize = (Size)v;
			v = jf.ValueAuto("AllowDrop", typeof(Boolean).Name);
			if (v != null) AllowDrop = (Boolean)v;
			v = jf.ValueAuto("Anchor", typeof(AnchorStyles).Name);
			if (v != null) Anchor = (AnchorStyles)v;
			v = jf.ValueAuto("AutoScrollOffset", typeof(Point).Name);
			if (v != null) AutoScrollOffset = (Point)v;
			v = jf.ValueAuto("CausesValidation", typeof(Boolean).Name);
			if (v != null) CausesValidation = (Boolean)v;
			v = jf.ValueAuto("Dock", typeof(DockStyle).Name);
			if (v != null) Dock = (DockStyle)v;
			v = jf.ValueAuto("Enabled", typeof(Boolean).Name);
			if (v != null) Enabled = (Boolean)v;
			v = jf.ValueAuto("Font", typeof(Font).Name);
			if (v != null) Font = (Font)v;
			v = jf.ValueAuto("ForeColor", typeof(Color).Name);
			if (v != null) ForeColor = (Color)v;
			v = jf.ValueAuto("UseWaitCursor", typeof(Boolean).Name);
			if (v != null) UseWaitCursor = (Boolean)v;
			v = jf.ValueAuto("Visible", typeof(Boolean).Name);
			if (v != null) Visible = (Boolean)v;
			v = jf.ValueAuto("Padding", typeof(Padding).Name);
			if (v != null) Padding = (Padding)v;
			v = jf.ValueAuto("ImeMode", typeof(ImeMode).Name);
			if (v != null) ImeMode = (ImeMode)v;
			v = jf.ValueAuto("FrameWeight", typeof(Padding).Name);
			if (v != null) FrameWeight = (Padding)v;

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

			JsonArray? ja = jf.ValueArray("Controls");
			if((ja!=null)&&(ja.Count>0))
			{
				foreach(var j in ja)
				{
					JsonObject? obj = (JsonObject?)j;
					if (obj != null)
					{
						if(obj.ContainsKey("MyType"))
						{
							int? mt = obj["MyType"].GetValue<int?>();
							if (mt != null)
							{
								ControlType ct = (ControlType)mt;
								HyperControl? hh = (HyperControl?)CreateControl(ct);
								if(hh!=null)
								{
									hh.FromJson(obj);
									this.Controls.Add(hh);

								}
							}
						}
					}
				}
				ChkControls();
			}
		}
	}
}
