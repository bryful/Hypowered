using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
		private int m_Index = -1;
		[Category("Hypowered"), Browsable(true)]
		public int Index
		{
			get { return m_Index; }
			set
			{
				if (value < 0) { value = -1; }
				else if(value >= this.Controls.Count) { value = this.Controls.Count - 1; }
				bool b = (m_Index != value); 
				m_Index = value;
				for(int i=0; i<this.Controls.Count;i++)
				{
					if (this.Controls[i] is HRadioButtonItem)
					{
						HRadioButtonItem bi = (HRadioButtonItem)this.Controls[i];
						bi.Checked = (i == m_Index);
					}
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
			SetEventHandler(this);
		}
		public void ChkControls()
		{
			if (this.Controls.Count > 0)
			{
				int w = this.Width / m_ColCount;
				int h = this.Height / m_RowCount;

				for (int i = 0; i < Controls.Count; i++)
				{
					if (this.Controls[i] is HRadioButtonItem)
					{
						HRadioButtonItem bi = (HRadioButtonItem)this.Controls[i];
						int ww = i % m_ColCount;
						int hh = i / m_ColCount;
						bi.Size = new Size(w, h);
						bi.Location = new Point(ww*w, hh*h);
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
		protected override void OnResize(EventArgs e)
		{
			ChkControls();
			base.OnResize(e);
		}
		private void SetEventHandler(Control objControl)
		{
			// イベントの設定
			// (親フォームにはすでにデザイナでマウスのイベントハンドラが割り当ててあるので除外)
			if (objControl != this)
			{
				objControl.MouseDown += (sender, e) => this.OnMouseDown(e);
				objControl.MouseMove += (sender, e) => this.OnMouseMove(e);
				objControl.MouseUp += (sender, e) => this.OnMouseUp(e);
			}

			// さらに子コントロールを検出する
			if (objControl.Controls.Count > 0)
			{
				foreach (Control objChildControl in objControl.Controls)
				{
					SetEventHandler(objChildControl);
				}
			}
		}
	}
}
