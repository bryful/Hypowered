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
	public partial class PictItemListTwin : Control
	{
		public delegate void PictItemChangedHandler(object sender, PictItemEventArgs e);
		public event PictItemChangedHandler? PictItemChanged;
		protected virtual void OnPictItemChanged(PictItemEventArgs e)
		{
			if (PictItemChanged != null)
			{
				PictItemChanged(this, e);
			}
		}
		// ***************************************************************************
		public void SetMainForm(MainForm? mainForm)
		{
			m_plist[0].SetMainForm(mainForm);
			m_plist[1].SetMainForm(mainForm);
		}
		public ItemsLib? MainItemsLib
		{
			get { return m_plist[0].ItemsLib; }
		}
		public void SetMainItemsLib(ItemsLib? m)
		{
			m_plist[0].SetItemsLib(m);
		}
		public ItemsLib? FormItemsLib
		{
			get { return m_plist[1].ItemsLib; }
		}
		public void SetFormItemsLib(ItemsLib? m)
		{
			m_plist[1].SetItemsLib(m);
		}
		public PictItem? MainPictIems(int idx) { return m_plist[0].PictIems(idx); }
		public PictItem? FormPictIems(int idx) { return m_plist[1].PictIems(idx); }
		public string TargetPictName
		{
			get
			{
				if (m_IsBuildin)
				{
					return m_plist[0].TargetPictName;
				}
				else
				{
					return m_plist[1].TargetPictName;
				}
			}
			set
			{
				m_plist[0].TargetPictName = value;
				if (m_plist[0].Index>=0) 
				{
					IsBuildin = true;
				}else
				{
					m_plist[1].TargetPictName = value;
					if(m_plist[1].Index>=0)
					{
						IsBuildin = false;
					}
				}
			}
		}
		public PictItem? TargetPictItem
		{
			get
			{
				if (m_IsBuildin)
				{
					return m_plist[0].TargetPictItem;
				}
				else
				{
					return m_plist[1].TargetPictItem;
				}
			}
		}
		// ***************************************************************************
		private bool m_IsBuildin = true;
		public bool IsBuildin
		{
			get { return m_IsBuildin; }
			set
			{
				m_IsBuildin = value;
				m_plist[0].Visible = m_IsBuildin;
				m_plist[1].Visible = !m_IsBuildin;
			}
		}
		private TogglePanel m_tpanel = new TogglePanel();
		private PictItemList[] m_plist = new PictItemList[2]; 
		public PictItemListTwin()
		{
			m_tpanel.Count = 2;
			m_tpanel.Texts[0] = "Build-In";
			m_tpanel.Texts[1] = "Form";
			m_tpanel.Location = new Point(0, 0);
			m_tpanel.BtnWidth = 75;
			m_tpanel.Height = 20;
			m_plist[0] = new PictItemList();
			m_plist[1] = new PictItemList();
			m_plist[0].Visible = m_IsBuildin;
			m_plist[1].Visible = ! m_IsBuildin;
			InitializeComponent();
			ChkSize();
			this.Controls.Add(m_tpanel);
			this.Controls.Add(m_plist[0]);
			this.Controls.Add(m_plist[1]);

			m_plist[0].PictItemChanged += (sender, e) => { OnPictItemChanged(e); };
			m_plist[1].PictItemChanged += (sender, e) => { OnPictItemChanged(e); };
			m_tpanel.IndexChanged += (sender, e) =>
			{
				IsBuildin = (e.Index == 0);
				OnPictItemChanged(new PictItemEventArgs(TargetPictItem));
			};
		}
		private void ChkSize()
		{
			m_plist[0].Location = new Point(0, m_tpanel.Height+2);
			m_plist[0].Size = new Size(this.Width, this.Height - m_tpanel.Height - 2);
			m_plist[1].Location = new Point(0, m_tpanel.Height + 2);
			m_plist[1].Size = new Size(this.Width, this.Height - m_tpanel.Height - 2);

		}
		protected override void OnResize(EventArgs e)
		{
			ChkSize();
			base.OnResize(e);
		}

	}
}
