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
	public partial class HyperPictureBox : HyperControl
	{
		[Category("Hypowerd")]
		public new bool IsEditMode
		{
			get { return base.m_IsEditMode; }
			set
			{
				base.IsEditMode = value;
				m_pv.IsEditMode= value;
				m_pv.Visible = !base.m_IsEditMode;
				m_pv.Invalidate();
				this.Invalidate();
			}
		}
		[Category("Hypowerd")]
		public bool IsEditModePV
		{
			get { return m_pv.IsEditMode; }
			set
			{
				m_pv.IsEditMode = value;
				m_pv.Visible = !base.m_IsEditMode;
				m_pv.Invalidate();
			}
		}
		[Category("Hypowerd_Color")]
		public Color BaseColor
		{
			get { return m_pv.BaseColor; }
			set { m_pv.BaseColor = value;}
		}
		[Category("Hypowerd_PictureBox")]
		public String FileName
		{
			get { return m_pv.FileName; }
			set { m_pv.FileName = value; }
		}
		[Category("Hypowerd_PictureBox")]
		public Bitmap? Bitmap
		{
			get { return m_pv.Bitmap; }
			set
			{
				m_pv.Bitmap = value;
			}
		}
		[Category("Hypowerd_PictureBox")]
		public float Ratio
		{
			get { return m_pv.Ratio; }
			set
			{
				m_pv.Ratio = value; 
			}
		}
		[Category("Hypowerd_PictureBox")]
		public bool AutoFit
		{
			get { return m_pv.AutoFit; }
			set
			{
				m_pv.AutoFit = value;
			}
		}
		[Category("Hypowerd_PictureBox")]
		public BorderStyle BorderStyle
		{
			get { return m_pv.BorderStyle; }
			set
			{
				m_pv.BorderStyle = value;
			}
		}
		private PictureBoxTarge m_pv = new PictureBoxTarge();
		public HyperPictureBox()
		{
			AllowDrop= true;
			SetMyType(ControlType.PictureBox);
			ScriptCode.SetInScript(InScript.MouseDoubleClick);
			this.Size = new Size(306, 306);
			m_pv.Size = new Size(300, 300);
			m_pv.Location = new Point(3, 3);
			m_FrameWeight = new Padding(0, 0, 0, 0);
			m_pv.BackColor = this.BackColor;
			m_pv.ForeColor = this.ForeColor;
			m_pv.BaseColor = Color.Transparent;
			m_pv.BorderStyle = BorderStyle.None;
			m_pv.IsEditMode = m_IsEditMode;
			this.Controls.Add(m_pv);
			InitializeComponent();
			m_pv.Visible = false;
		}
		private void ChkSize()
		{
			m_pv.Size = new Size(this.Width-6, this.Height-6);
			m_pv.Location = new Point(3, 3);
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
		}
		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			if (drgevent == null) return;
			if ((drgevent.Data.GetDataPresent(DataFormats.FileDrop))&&(m_IsEditMode==false))
			{
				drgevent.Effect = DragDropEffects.Copy;
			}
			else
			{
				//ファイル以外は受け付けない
				drgevent.Effect = DragDropEffects.None;
				base.OnDragEnter(drgevent);
			}
		}
		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			if ((drgevent == null)||(m_IsEditMode==true)) return;
			string[] fileName =
			(string[])drgevent.Data.GetData(DataFormats.FileDrop, false);

			foreach (var s in fileName)
			{
				if (m_pv.OpenFile(s))
				{
					m_pv.Visible= true;
					break;
				}
			}
			base.OnDragDrop(drgevent);
		}
		public override JsonObject ToJson()
		{
			JsonFile jf = new JsonFile(base.ToJson());
			jf.SetValue(nameof(MyType), (int?)MyType);//Nullable`1
			jf.SetValue(nameof(BaseColor), BaseColor);//Color
			jf.SetValue(nameof(FileName), FileName);//Color
			jf.SetValue(nameof(AutoFit), AutoFit);//Color
			jf.SetValue(nameof(Ratio), Ratio);//Color
			jf.SetValue(nameof(BorderStyle), (int)BorderStyle);//Color

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			v = jf.ValueAuto("BaseColor", typeof(Color).Name);
			if (v != null) BaseColor = (Color)v;
			v = jf.ValueAuto("FileName", typeof(String).Name);
			if (v != null) FileName = (String)v;
			v = jf.ValueAuto("AutoFit", typeof(Boolean).Name);
			if (v != null) AutoFit = (Boolean)v;
			v = jf.ValueAuto("Ratio", typeof(float).Name);
			if (v != null) Ratio = (float)v;
			v = jf.ValueAuto("BorderStyle", typeof(Int32).Name);
			if (v != null) BorderStyle = (BorderStyle)v;
		}
	}
}
