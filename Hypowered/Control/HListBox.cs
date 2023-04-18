using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Hypowered
{
	public class HListBox : HControl
	{
		#region Prop
		private bool m_IsSaveItems = true;
		[Category("Hypowered"), Browsable(true)]
		private bool IsSaveItems
		{
			get { return m_IsSaveItems; }
			set
			{
				m_IsSaveItems = value;
			}
		}
		private int m_ListHeight = 18;
		[Category("Hypowered"), Browsable(true)]
		private int ListHeight
		{
			get { return m_ListHeight; }
			set
			{
				m_ListHeight = value;
				ChkSize();
				this.Invalidate();
			}
		}
		private int m_DispY = 0;
		[Category("Hypowered"), Browsable(false)]
		public int DispY
		{
			get { return m_DispY; }
			set
			{
				if(value<0) { value = 0; }
				else if(value>m_DispYMax) { value = m_DispYMax; }

				if(m_DispY != value)
				{
					m_DispY = value;
					VScrol.Value = value;
				}
				this.Invalidate();
			}
		}
		private int m_DispYMax = 0;
		private int m_SelectedIndex = -1;
		[Category("Hypowered"), Browsable(true)]
		public int SelectedIndex
		{
			get { return m_SelectedIndex; }
			set 
			{
				if((value>=0)&&(value< m_Items.Count))
				{
					m_SelectedIndex = value;
				}
				else
				{
					m_SelectedIndex = -1;
				}
				this.Invalidate();
			}
		}
		// ********************************************************
		private List<HListBoxItem> m_Items = new List<HListBoxItem>();
		[Category("Hypowered"),Browsable(true)]
		public string[] Items
		{
			get { return ToArray(); }
			set
			{
				FromArray(value);
				ChkSize();
				this.Invalidate();
			}
		}
		public string[] ToArray()
		{
			string []ret = new string[m_Items.Count];
			for(int i=0; i< m_Items.Count; i++)
			{
				ret[i] = m_Items[i].Text;
			}
			return ret;
		}
		public void FromArray(string[] sa)
		{
			m_Items.Clear();
			if(sa.Length > 0)
			{
				foreach(string s in sa)
				{
					this.m_Items.Add(new HListBoxItem(s));
				}
			}
		}
		private Color m_SelectedColor = Color.FromArgb(100, 100, 100);
		[Category("Hypowered_Color"), Browsable(true)]
		public Color SelectedColor
		{
			get { return m_SelectedColor; }
			set
			{
				m_SelectedColor = value; this.Invalidate();
			}
		}
		/// <summary>
		/// ターゲットになった時の色。ListBoxで使用
		/// </summary>
		[Category("Hypowered_Color"), Browsable(true)]
		public System.Drawing.Color TargetColor
		{
			get { return m_TargetColor; }
			set { m_TargetColor = value; this.Invalidate(); }
		}
		[Category("Hypowered"), Browsable(false)]
		private VScrol VScrol { get; set; } = new VScrol();
		#endregion
		// ***********************************************
		public HListBox()
		{
			m_HType = HType.ListBox;
			TextAlign = StringAlignment.Near;
			base.Size = new Size(200, 150);
			VScrol.Size = new Size(20, this.Height-20);
			VScrol.Location = new Point(this.Width - VScrol.Width-3, 10);
			VScrol.Value = m_DispY; ;
			VScrol.ValueChanged += (sender, e) =>
			{
				if(DispY != e.Value)
				{
					DispY = e.Value;
				}
			};
			ChkSize();
			ChkGridSize();
			this.Controls.Add(VScrol);
		}
		// ***********************************************
		public void Add(string s, bool IsSel=false)
		{
			m_Items.Add(new HListBoxItem(s,IsSel));
		}
		// ***********************************************
		public void AddRange(string [] sa, bool IsSel = false)
		{
			if (sa.Length>0)
			{
				if (sa.Length > 0)
				{
					foreach (string s in sa)
					{
						this.m_Items.Add(new HListBoxItem(s,IsSel));
					}
				}
			}
		}
		// ***********************************************
		public void RemoveAt(int idx)
		{
			if((idx>=0)&&(idx<m_Items.Count))
			{
				m_Items.RemoveAt(idx);
			}
		}

		// ***********************************************
		public void Swap(int idx0,int idx1)
		{
			if ((idx0 != idx1) 
				&& (idx0 >= 0) && (idx0 < m_Items.Count)
				&& (idx1 >= 0) && (idx1 < m_Items.Count))
			{
				HListBoxItem s = new HListBoxItem(m_Items[idx0]);
				m_Items[idx0] = new HListBoxItem(m_Items[idx1]);
				m_Items[idx1] = s;
			}
		}
		// ***********************************************
		public void Clear()
		{
			m_Items.Clear();
		}
		// ***********************************************
		private void DrawItem(Graphics g,SolidBrush sb, Pen p, int idx,Rectangle r)
		{
			if ((idx >= m_Items.Count) || (idx < 0)) return;
			if (idx==m_SelectedIndex)
			{
				sb.Color = m_TargetColor;
			}else if (m_Items[idx].Selected==true)
			{
				sb.Color = m_SelectedColor;
			}
			else
			{
				sb.Color = BackColor;
			}
			g.FillRectangle(sb, r);
			if (m_Items[idx].Text!="") 
			{
				sb.Color = ForeColor;
				Rectangle r2 = new Rectangle(r.Left+20, r.Top, r.Width - 20, r.Height);
				g.DrawString(m_Items[idx].Text, this.Font, sb, r2, StringFormat);
			}

		}
		// ***********************************************
		protected override void OnPaint(PaintEventArgs pe)
		{
			using(SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = pe.Graphics;
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);

				if (m_Items.Count > 0) {
					g.SetClip(new Rectangle(3, 3, this.Width - 6- VScrol.Width, this.Height - 6));
					int st = m_DispY / m_ListHeight;
					for (int i = st;i< m_Items.Count;i++)
					{
						Rectangle rct = new Rectangle(
							3,
							i * m_ListHeight - m_DispY +3,
							this.Width-6-VScrol.Width-16, 
							m_ListHeight);
						DrawItem(g, sb, p, i, rct);
					}
					g.SetClip(this.ClientRectangle);
				}

				Rectangle rr = new Rectangle(2,2,this.Width-4-VScrol.Width-3,this.Height-4);
				p.Color = ForeColor;
				g.DrawRectangle(p,rr);

				DrawIsEdit(g, p);

			}

		}
		// ***********************************************
		private void ChkSize()
		{
			int hh = m_Items.Count * m_ListHeight;
			m_DispYMax = hh - (this.Height - 4);
			if (m_DispYMax < 0) m_DispYMax = 0;
			if (m_DispY > m_DispYMax) m_DispY = m_DispYMax;
			VScrol.ValueMax = m_DispYMax;

		}
		protected override void OnResize(EventArgs e)
		{
			ChkGridSize();
			VScrol.Size = new Size(20, this.Height-20);
			VScrol.Location = new Point(this.Width - VScrol.Width-3, 10);
			ChkSize();

			this.Refresh();
			base.OnResize(e);
		}
		// ****************************************************************************
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (IsAltKey)
			{
				SetIsEdit(true);
				this.Invalidate();
				if (HForm != null)
				{
					HForm.TargetIndex = this.Index;
					HForm.Invalidate();
				}
				return;
			}
			if (m_IsEdit == true)
			{
				if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
				{


					m_MD = true;
					m_MDLoc = this.Location;
					m_MDSize = this.Size;
					m_MDP = this.PointToScreen(new Point(e.X, e.Y));
					m_MDResize = ((e.X > this.Width - 10) && (e.Y > this.Height - 10));


					return;
				}

			}
			else
			{
				int idx = (e.Y + m_DispY-2) / m_ListHeight;
				if((idx>=0)&&(idx<m_Items.Count))
				{
					m_SelectedIndex = idx;
					if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
					{
						for(int i = 0;i<m_Items.Count;i++)
						{
							m_Items[i].Selected = false;
						}
					}
					m_Items[idx].Selected = true;

				}
				this.Invalidate();
			}
		}
		// ****************************************************************************
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			int idx = e.Delta / 120;

			DispY -= idx * m_ListHeight * 2;
		}
		// ****************************************************************************
		public override JsonObject? ToJson()
		{
			JsonFile? jf = new JsonFile(base.ToJson());

			if(m_IsSaveItems) jf.SetValue("Items", ToArray());
			jf.SetValue("ListHeight", ListHeight);
			jf.SetValue("SelectedColor", SelectedColor);
			jf.SetValue("TargetColor", TargetColor);

			return jf.Obj;
		}
		public override void FromJson(JsonObject jo)
		{
			base.FromJson(jo);
			JsonFile jf = new JsonFile(jo);
			object? v = null;
			if (m_IsSaveItems)
			{
				v = jf.ValueAuto("Items", typeof(String[]).Name);
				if (v != null) FromArray((String[])v);
			}
			v = jf.ValueAuto("ListHeight", typeof(Int32).Name);
			if (v != null) ListHeight = (Int32)v;
			v = jf.ValueAuto("SelectedColor", typeof(Color).Name);
			if (v != null) SelectedColor = (Color)v;
			v = jf.ValueAuto("TargetColor", typeof(Color).Name);
			if (v != null) TargetColor = (Color)v;

			ChkSize();
		}
	}
	public class HListBoxItem
	{
		public string Text { get; set; } = "";
		public bool Selected { get; set; } = false;
		public Object? Tag { get; set; } = null;
		public string Comment { get; set; } = "";
		public HListBoxItem() 
		{
		}
		public HListBoxItem(string s)
		{
			Text = s;
		}
		public HListBoxItem(string s,bool sel)
		{
			Text = s;
			Selected = sel;
		}
		public HListBoxItem(HListBoxItem v)
		{
			Text = v.Text;
			Selected = v.Selected;
			Tag = v.Tag;
			Comment = v.Comment;
		}

	}
}
