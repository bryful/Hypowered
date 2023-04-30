using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace Hypowered
{
	public partial class HRadioButton : HControl
	{
		private int m_RowCount = 1;
		[Category("Hypowered"),Browsable(true)]
		public int RowCount
		{
			get { return m_RowCount; }
			set 
			{
				if(value<1) { value = 1; }
				SetControls(m_ColCount, value);
			}

		}
		private int m_ColCount = 1;
		[Category("Hypowered"), Browsable(true)]
		public int ColCount
		{
			get { return m_ColCount; }
			set
			{
				if (value < 1) { value = 1; }
				SetControls(value, m_RowCount);
			}

		}
		private bool m_MultiChecked = false;
		[Category("Hypowered"), Browsable(true)]
		public  bool MultiChecked
		{
			get { return m_MultiChecked; }
			set
			{
				m_MultiChecked = value;
				if(m_MultiChecked==false)
				{
					//bool[] rr = Indeies;

				}
			}
		}
		private int m_SelectedIndex = -1;
		[Category("Hypowered"), Browsable(true)]
		public new int SelectedIndex
		{
			get { return m_SelectedIndex; }
			set
			{
				if (value < 0) { value = -1; }
				else if (value >= this.Controls.Count) { value = this.Controls.Count - 1; }
				bool b = (m_SelectedIndex != value);
				m_SelectedIndex = value;
				if (m_MultiChecked == false)
				{
					bool[] list = new bool[this.Controls.Count];
					for (int i = 0; i < this.Controls.Count; i++) list[i] = false;
					list[m_SelectedIndex] = true;
					SetChecked(list);
				}
				else
				{
					bool[] list = GetChecked();
					if(list[m_SelectedIndex]!=false)
					{
						list[m_SelectedIndex] = true;
						SetChecked(list);
					}
				}
			}
		}
		
		public void AddIndex(int idx)
		{
			m_SelectedIndex = idx;
			if (m_MultiChecked)
			{
				if((idx>=0)&&(idx<this.Controls.Count))
				{
					HRadioButtonItem rbi = (HRadioButtonItem)this.Controls[idx];
					rbi.Checked = !rbi.Checked;
				}
			}
			else
			{
				for(int i =0;  i < this.Controls.Count; i++)
				{
					HRadioButtonItem rbi = (HRadioButtonItem)this.Controls[i];
					if ((i == idx))
					{
						rbi.Checked = !rbi.Checked;
					}
					else
					{
						rbi.Checked = false;
					}
				}

			}
		}
		private bool [] GetChecked()
		{
			List<bool> list = new List<bool>();
			if (this.Controls.Count > 0)
			{
				for (int i = 0; i < this.Controls.Count; i++)
				{
					if (this.Controls[i] is HRadioButtonItem)
					{
						HRadioButtonItem bi = (HRadioButtonItem)this.Controls[i];
						list.Add(bi.Checked);
					}
				}
			}
			return list.ToArray();
		}
		private void  SetChecked(bool[] list)
		{
			if (this.Controls.Count > 0)
			{
				for (int i = 0; i < this.Controls.Count; i++)
				{
					if (this.Controls[i] is HRadioButtonItem)
					{
						HRadioButtonItem bi = (HRadioButtonItem)this.Controls[i];
						if((i>=0) && (i<list.Length))
						{
							bi.Checked = list[i];
						}
						else
						{
							bi.Checked = false;
						}
						bi.Invalidate();
					}
				}
				this.Invalidate();
			}
		}
		[Category("Hypowered"), Browsable(true)]
		public  bool[] Indeies
		{
			get 
			{
				return GetChecked();	
			}
			set
			{
				bool[] list = value;
				if (m_MultiChecked == false)
				{
					int idx = -1;

					if ((m_SelectedIndex>=0)&&(m_SelectedIndex<list.Length))
					{
						if (list[m_SelectedIndex] ==true)
						{
							idx = m_SelectedIndex;
						}
					}
					if(idx<0)
					{
						for (int i = 0; i < list.Length; i++)
						{
							if (list[i] == true)
							{
								idx = i;
								break;
							}
						}
					}
					for (int i = 0; i < list.Length; i++) list[i] = false;
					if((idx>=0) && (idx<list.Length)) list[idx] = true;
					m_SelectedIndex = idx;
				}
				SetChecked(list);
			}
		}
		[Category("Hyepowered_Draw"), Browsable(true)]
		public new bool IsAnti
		{
			get { return m_IsAnti; }
			set
			{
				m_IsAnti = value;
				if (this.Controls.Count > 0)
				{
					for (int i = 0; i < this.Controls.Count; i++)
					{
						if (this.Controls[i] is HRadioButtonItem)
						{
							((HRadioButtonItem)this.Controls[i]).IsAnti = m_IsAnti;
						}
					}
				}

				this.Invalidate();
			}
		}

		[Category("Hyepowered_Draw"), Browsable(true)]
		public new bool IsEdit
		{
			get { return m_IsEdit; }
			set
			{
				SetIsEdit(value);
			}
		}
		public override void SetIsEdit(bool b)
		{
			m_IsEdit = b;
			if (this.Controls.Count > 0)
			{
				for (int i = 0; i < this.Controls.Count; i++)
				{
					if (this.Controls[i] is HRadioButtonItem)
					{
						((HRadioButtonItem)this.Controls[i]).IsEdit = m_IsEdit;
					}
				}
			}
			if (m_IsEdit == false)
			{
				m_Selected = false;
			}
			this.Invalidate();
		}
		[Category("Hyepowered_Draw"), Browsable(true)]
		public new Font Font
		{
			get { return base.Font; }
			set
			{
				base.Font = value;
				if (this.Controls.Count > 0)
				{
					for (int i = 0; i < this.Controls.Count; i++)
					{
						this.Controls[i].Font = value;
						this.Invalidate();
					}
				}

			}
		}
		[Category("Hypowered"), Browsable(true)]
		public string[] Lines
		{
			get
			{
				string[] ret = new string[this.Controls.Count];
				if(this.Controls.Count > 0)
				{
					for(int i=0; i<this.Controls.Count;i++)
					{
						ret[i] = this.Controls[i].Text;
					}
				}
				return ret;
			}
			set
			{
				int len = value.Length;
				if (len == 0) return;
				if(len >= this.Controls.Count) { len = this.Controls.Count - 1; }
				for(int i=0; i< len; i++)
				{
					this.Controls[i].Text = value[i];
					this.Controls[i].Invalidate();
				}

			}
		}
		public HRadioButton()
		{
			m_HCType = HCType.RadioButton;
			ScriptCode.Setup(HScriptType.Click);

			this.Size = new Size(150, 50);
			InitializeComponent();
			SetControls(2, 2);
			//SetEventHandler();
		}
		public void ChkControls()
		{
			if (this.Controls.Count > 0)
			{
				int w = (this.Width-6) / m_ColCount;
				int h = (this.Height-6) / m_RowCount;

				for (int i = 0; i < Controls.Count; i++)
				{
					if (this.Controls[i] is HRadioButtonItem)
					{
						HRadioButtonItem bi = (HRadioButtonItem)this.Controls[i];
						bi.Index = i;
						int ww = i % m_ColCount;
						int hh = i / m_ColCount;
						bi.Size = new Size(w-5, h);
						bi.Location = new Point(ww * w + 3, hh * h + 3);
					}
				}
			}
		}
		public void SetControls(int col, int row)
		{
			int mx = col * row;
			m_ColCount = col;
			m_RowCount = row;
			if(this.Controls.Count!=mx)
			{
				if(this.Controls.Count<mx)
				{
					for(int i= this.Controls.Count; i<mx;i++)
					{
						HRadioButtonItem item = new HRadioButtonItem();
						item.Name = $"rbtn{i}";
						item.Text = item.Name;
						item.HRadioButton = this;
						this.Controls.Add(item);
					}
				}
				else
				{
					for (int i = this.Controls.Count-1; i >=mx; i--)
					{
						this.Controls.RemoveAt(i);
					}
				}
				ChkControls();
			}
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(base.BackColor))
			using (Pen p = new Pen(base.ForeColor))
			{
				Graphics g = pe.Graphics;
				if (m_IsAnti)
				{
					g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
					g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
				}

				// 塗り
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);



				DrawCtrlRect(g, p);
			}
		}
		protected override void OnResize(EventArgs e)
		{
			ChkControls();
			base.OnResize(e);
		}
		private void SetEventHandler()
		{
			// イベントの設定
			// (親フォームにはすでにデザイナでマウスのイベントハンドラが割り当ててあるので除外)
			if (this.Controls.Count > 0)
			{
				foreach (Control objChildControl in this.Controls)
				{
					objChildControl.MouseDown += (sender, e) =>
					{
						if(m_IsEdit==true)
						this.OnMouseDown(e); 
					};
					objChildControl.MouseMove += (sender, e) =>
					{
						if (m_IsEdit == true)
							this.OnMouseMove(e);
					};
					objChildControl.MouseUp += (sender, e) =>
					{
						if (m_IsEdit == true)
							this.OnMouseUp(e);
					};
				}
			}
			/*
			// さらに子コントロールを検出する
			*/
		}
		public void CallOnMouseDown(MouseEventArgs e)
		{
			OnMouseDown(e);
		}
		public void CallOnMouseMove(MouseEventArgs e)
		{
			OnMouseMove(e);
		}
		public void CallOnMouseUp(MouseEventArgs e)
		{
			OnMouseUp(e);
		}
	}
}
