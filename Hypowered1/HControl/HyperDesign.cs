using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Hypowered
{
	public enum DesignType
	{
		Fill,
		Frame,
		Zebra,
		Triangle,
	}
	
	public partial class HyperDesign : HyperControl
	{
		protected DesignType m_DesignType = DesignType.Fill;
		[Category("Hypowered_Design")]
		public DesignType DesignType
		{
			get { return m_DesignType; }
			set { m_DesignType = value; this.Invalidate(); }
		}
		protected float m_Rot = 45;
		[Category("Hypowered_Design")]
		public float Rot
		{
			get { return m_Rot; }
			set { m_Rot = value; this.Invalidate(); }
		}
		protected float m_ZebraWeight = 20;
		[Category("Hypowered_Design")]
		public float ZebraWeight
		{
			get { return m_ZebraWeight; }
			set { m_ZebraWeight = value; this.Invalidate(); }
		}
		protected TriangleStyle m_TriangleStyle = TriangleStyle.Top;
		[Category("Hypowered_Design")]
		public TriangleStyle TriangleStyle
		{
			get { return m_TriangleStyle; }
			set { m_TriangleStyle = value; this.Invalidate(); }
		}
		public HyperDesign()
		{
			SetControlType(Hypowered.ControlType.Design);
			
			SetInScript(InScriptBit.DragDrop);
			this.Location = new Point(150, 150);
			this.Size = new Size(200,200);
			BackColor= Color.Transparent;
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			using(SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g= pe.Graphics;
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				switch(m_DesignType)
				{
					case DesignType.Fill:
						sb.Color = ForeColor;
						g.FillRectangle(sb, this.ClientRectangle);
						break;
					case DesignType.Frame:
						sb.Color = BackColor;
						g.FillRectangle(sb, this.ClientRectangle);
						p.Color = ForeColor;
						DrawFrame(g, p, this.ClientRectangle);
						break;
					case DesignType.Zebra:
						sb.Color = BackColor;
						g.FillRectangle(sb, this.ClientRectangle);
						sb.Color = ForeColor;
						Ds.DrawZebra(g,sb,this.ClientRectangle,m_ZebraWeight,m_Rot);
						break;
					case DesignType.Triangle:
						sb.Color = BackColor;
						g.FillRectangle(sb, this.ClientRectangle);
						sb.Color = ForeColor;
						if (m_TriangleStyle == TriangleStyle.Center)
						{
							PointF c = new PointF((float)(this.Right - (float)this.Left) / 2, (float)(this.Bottom - (float)this.Top) / 2);

							float r = (float)this.Width/ 2;
							if (r > (float)this.Height / 2) r = (float)this.Height / 2;
							Ds.Triangle(g, null, sb, c, r,m_Rot);
						}
						else
						{
							Ds.Triangle(g, null, sb, this.ClientRectangle, m_TriangleStyle);
						}
						break;
				}
				DrawEditMode(g, p, sb);
			}
		}
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(ControlType), (int?)ControlType);//Nullable`1

			jf.SetValue(nameof(DesignType), (int)DesignType);//Color
			jf.SetValue(nameof(Rot), Rot);//Color
			jf.SetValue(nameof(ZebraWeight), ZebraWeight);//Color
			jf.SetValue(nameof(TriangleStyle), (int)TriangleStyle);//Color
			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("DesignType", typeof(int).Name);
			if (v != null) DesignType = (DesignType)v;
			v = jf.ValueAuto("Rot", typeof(float).Name);
			if (v != null) Rot = (float)v;
			v = jf.ValueAuto("ZebraWeight", typeof(float).Name);
			if (v != null) ZebraWeight = (float)v;
			v = jf.ValueAuto("TriangleStyle", typeof(int).Name);
			if (v != null) TriangleStyle = (TriangleStyle)v;
		}

	}
}
