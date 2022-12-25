﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hypowered
{
	public class SelectedIndexChangedEventArgs : EventArgs
	{
		public int SelectedIndex;
		public SelectedIndexChangedEventArgs(int v)
		{
			SelectedIndex = v;
		}
	}
	public partial class HyperDropdownList : HyperControl
	{
		public delegate void SelectedIndexChangedHandler(object sender, SelectedIndexChangedEventArgs e);
		public event SelectedIndexChangedHandler? SelectedIndexChanged;
		protected virtual void OnSelectedIndexChanged(SelectedIndexChangedEventArgs e)
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, e);
			}
			if ((HyperForm != null) && (m_ScriptCode != ""))
			{
				HyperForm.ExecuteCode(m_ScriptCode);
			}
		}
		private int m_SelectedIndex = -1;
		[Category("Hypowerd_DropdownList")]
		public int SelectedIndex
		{
			get { return m_SelectedIndex; }
			set 
			{ 
				SetSelectedIndex(value);
			}
		}
		public void SetSelectedIndex(int idx)
		{
			if (idx < -1) idx = -1;
			if (idx >= m_Items.Count) idx = m_Items.Count - 1;
			if (m_SelectedIndex != idx)
			{
				m_SelectedIndex = idx;
				OnSelectedIndexChanged(new SelectedIndexChangedEventArgs(idx));
			}
			this.Invalidate();
		}
		private StringCollection m_Items = new StringCollection();
		[Category("Hypowerd_DropdownList")]
		public StringCollection Items
		{
			get { return m_Items; }
			set
			{
				m_Items = value;
			}
		}

		public HyperDropdownList()
		{
			SetMyType(ControlType.DropdownList);
			m_ScriptCode = "//DropdownList";
			this.Location = new Point(100, 100);
			this.Size = ControlDef.DefSize;
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(BackColor))
			using (Pen p = new Pen(ForeColor))
			{
				Graphics g = pe.Graphics;
				// 背景色
				sb.Color = BackColor;
				g.FillRectangle(sb, this.ClientRectangle);

				if (m_SelectedIndex>=0)
				{
					StringFormat sf = new StringFormat();
					sf.Alignment = StringAlignment.Near;
					sf.LineAlignment = StringAlignment.Center;
					sb.Color = ForeColor;
					g.DrawString(
						m_Items[m_SelectedIndex], 
						this.Font, 
						sb, 
						ReRect(this.ClientRectangle, 3), sf);
				}

				// 外枠
				Rectangle rr = ReRect(this.ClientRectangle, 2);
				p.Color = ForeColor;
				g.DrawRectangle(p, rr);

				if (this.Focused)
				{
					rr = ReRect(this.ClientRectangle, 1);
					p.Color = m_ForcusColor;
					g.DrawRectangle(p, rr);
				}
				DrawType(g, sb);

			}
		}
		public ContextMenuStrip MakeMenu()
		{
			ContextMenuStrip ret = new ContextMenuStrip();
			if (m_Items.Count > 0)
			{
				int idx = 0;
				foreach (string? mi in m_Items)
				{
					if (mi == null) continue;
					ToolStripMenuItem mc = new ToolStripMenuItem();
					mc.BackColor = this.BackColor;
					mc.ForeColor = this.ForeColor;
					mc.Text = mi;
					mc.Click += Mc_Click;
					mc.Tag = (Object?)idx;
					ret.Items.Add(mc);
					idx++;
				}
			}
			return ret;

		}

		private void Mc_Click(object? sender, EventArgs e)
		{
			ToolStripMenuItem? m = (ToolStripMenuItem?)sender;
			if(m!=null)
			{
				if(m.Tag is int)
				{
					SetSelectedIndex((int)m.Tag);
				}
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (m_IsEditMode==false)
			{
				ContextMenuStrip m = MakeMenu();
				if (m!=null)
				{
					m.SetBounds(0, this.Height, this.Width, m.Height);
					m.Show(this, 0, this.Height);
				}
			}
			
		}
	}
}