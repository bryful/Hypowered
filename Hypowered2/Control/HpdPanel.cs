﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Hpd
{
	public partial class HpdPanel : HpdControl
	{
		protected HpdOrientation m_Orientation = HpdOrientation.Vertical;
		[Category("Hypowered_layout")]
		public HpdOrientation Orientation
		{
			get { return m_Orientation; }
			set
			{
				bool b = (m_Orientation != value);
				m_Orientation = value;
				if ((b) && (MainForm != null)) MainForm.AutoLayout();
			}
		}

		public HpdPanel()
		{
			SetHpdType(HpdType.Panel);
			m_SizePolicyHorizon = SizePolicy.Expanding;
			m_SizePolicyVertual = SizePolicy.Expanding;
			this.Size = new Size(23 * 2, 23);
			SetBaseSize(0, 0);

			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}

		public HpdControl? AddControl(string Name, HpdType ht)
		{
			if (MainForm != null)
			{
				return HU.AddControl(MainForm, this, Name, ht);
			}
			else
			{
				return null;
			}
		}
		public HpdControl? AddControl()
		{
			HpdControl? ret = null;
			if (MainForm != null)
			{
				ret = HU.AddControl(MainForm, this);
			}
			return ret;
		}
		public bool ControlMoveUp(HpdControl hc)
		{
			return HU.ControlMoveUp(hc);

		}
		public bool ControlMoveDown(HpdControl hc)
		{
			return HU.ControlMoveDown(hc);
		}
		public HpdControl? ControlRemove(HpdControl hc)
		{
			return HU.ControlRemove(hc);
		}
		public HpdControl? CutCtrl()
		{
			if (MainForm != null)
			{
				return HU.CutCtrl(MainForm);
			}
			else { return null; }
		}
		public HpdControl? PasteCtrl()
		{
			if (MainForm != null)
			{
				return HU.PasteCtrl(MainForm);
			}
			else { return null; }
		}
		public override JsonObject? ToJson()
		{
			JsonFile jf = new JsonFile();
			jf.SetValue(nameof(HpdType), (int)HpdType);
			jf.SetValue(nameof(Name), (String)Name);//System.String
			jf.SetValue(nameof(Visible), (Boolean)Visible);//System.Boolean
			jf.SetValue(nameof(Orientation), (Int32)Orientation);

			JsonArray? arr = new JsonArray();
			if(this.Controls.Count>0)
			{
				foreach(Control c in this.Controls)
				{
					if(c is HpdControl)
					{

					}
				}
			}

			return jf.Obj;
		}
	}
}
