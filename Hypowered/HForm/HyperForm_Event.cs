using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypowered
{
	public class TargetChangedEventArgs : EventArgs
	{
		public int Index;
		public HyperControl? control;
		public TargetChangedEventArgs(int idx, HyperControl? c)
		{
			Index= idx;
			control = c;
		}
	}
	
	partial class HyperForm
	{
		private PropertyForm? PropForm = null;
		private EditControlListForm? ControlListForm = null;

		protected App m_App = new App();
		[Category("Hypowerd_Form")]
		public App App
		{
			get { return m_App; }
			set { m_App = value; }
		}
		// ****************************************************************************
		public delegate void TargetChangedHandler(object sender, TargetChangedEventArgs e);
		public event TargetChangedHandler? TargetChanged;
		protected virtual void OnTargetChanged(TargetChangedEventArgs e)
		{
			if (TargetChanged != null)
			{
				TargetChanged(this, e);
			}
		}
		public event EventHandler? ControlChanged;
		protected virtual void OnControlChanged(EventArgs e)
		{
			if (ControlChanged != null)
			{
				ControlChanged(this, e);
			}
		}
		// ****************************************************************************
		public void ChkTarget(HyperControl? hc)
		{
			int TI = m_TargetIndex;
			m_isMultSelect = false;
			if (this.Controls.Count > 0)
			{
				if (hc == null)
				{
					m_TargetIndex = -1;
					foreach (Control c in this.Controls)
					{
						if (c is HyperControl)
						{
							HyperControl h = (HyperControl)c;
							h.Selected = false;
							h.ParentIndex = -1;
						}
					}
				}
				else
				{

					foreach (Control c in this.Controls)
					{
						if (c is HyperControl)
						{
							HyperControl h = (HyperControl)c;

							if (h == hc)
							{
								m_TargetIndex = hc.Index;
								h.Selected = true;
							}
						}
					}
				}
				this.Invalidate();
			}
			// ********************************************
			// イベントの発生
			if (TI != m_TargetIndex)
			{
				if (m_TargetIndex >= 0)
				{
					OnTargetChanged(new TargetChangedEventArgs(
						m_TargetIndex,
						TargetControl
						));
				}
			}
		}
		/// <summary>
		/// コントロールが複数選択してるとtrue
		/// </summary>
		private bool m_isMultSelect = false;
		/// <summary>
		/// コントロールの選択状態のチェック
		/// 引数で渡されたコントロールがターゲットになる
		/// </summary>
		/// <param name="hc">選ばれたコントロール　nullなら無選択状態になる</param>
		public void ChkTargetSelected(HyperControl? hc)
		{
			int TI = m_TargetIndex;
			if (this.Controls.Count > 0)
			{
				if (hc == null)
				{
					m_isMultSelect = false;
					m_TargetIndex = -1;
					foreach (Control c in this.Controls)
					{
						if (c is HyperControl)
						{
							HyperControl h = (HyperControl)c;
							h.Selected = false;
							h.ParentIndex = -1;
						}
					}
				}
				else
				{
					bool IsShift = ((Control.ModifierKeys & Keys.Shift) == Keys.Shift);
					if (IsShift)
					{
						m_isMultSelect = true;
					}
					else
					{
						if (hc.Selected == false) { m_isMultSelect = false; }
					}

					foreach (Control c in this.Controls)
					{
						if (c is HyperControl)
						{
							HyperControl h = (HyperControl)c;
							if (m_isMultSelect == false)
							{
								h.Selected = false;
								h.ParentIndex = -1;
							}
							if (h == hc)
							{
								m_TargetIndex = hc.Index;
								h.Selected = true;
							}

						}
					}
					// 複数選択の処理
					if (m_isMultSelect)
					{
						foreach (Control c in this.Controls)
						{
							if (c is HyperControl)
							{
								HyperControl h = (HyperControl)c;
								if (h.Selected && (h.Index != hc.Index))
								{
									h.ParentLocation = new Point(h.Left - hc.Left, h.Top - hc.Top);
									h.ParentIndex = hc.Index;
								}
								else
								{
									h.ParentLocation = new Point(0, 0);
									h.ParentIndex = -1;
								}
							}
						}
					}
				}
				this.Invalidate();
			}
			else
			{
				m_isMultSelect = false;
			}
			// ********************************************
			// イベントの発生
			if(TI != m_TargetIndex)
			{
				OnTargetChanged(new TargetChangedEventArgs(
					m_TargetIndex,
					TargetControl
					));
			}
		}

		/// <summary>
		/// 選択されたコントロールを同時に動かす
		/// </summary>
		public void MoveSelected()
		{
			if (m_TargetIndex < 0) return;
			if (m_isMultSelect == false) return;
			if (this.Controls.Count == 0) return;
			HyperControl pp = (HyperControl)this.Controls[m_TargetIndex];
			foreach (Control c in this.Controls)
			{
				if (c is HyperControl)
				{
					if (c is HyperMenuBar) continue;
					HyperControl h = (HyperControl)c;
					if (h == null) continue;
					if (h.Index != m_TargetIndex)
					{
						try
						{
							int x = h.ParentLocation.X + pp.Left;
							int y = h.ParentLocation.Y + pp.Top;
							h.Location = new Point(x, y);
						}
						catch { }
					}
				}
			}
		}

		// ****************************************************************************
		private MDPos m_MDPos = MDPos.None;
		private Point m_MDP = new Point(0, 0);
		private Point m_MDLoc = new Point(0, 0);
		private Size m_MDSize = new Size(0, 0);

		public void DoMouseDown(MouseEventArgs e)
		{
			this.OnMouseDown(e);
		}
		public void DoMouseMove(MouseEventArgs e)
		{
			this.OnMouseMove(e);
		}
		public void DoMouseUp(MouseEventArgs e)
		{
			this.OnMouseUp(e);
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (m_IsEditMode) ChkTargetSelected(null);
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				MDPos p = CU.GetMDPos(e.X, e.Y, this.Size);
				if (p != MDPos.None)
				{
					m_MDPos = p;
					m_MDP = new Point(e.X, e.Y);
					m_MDLoc = this.Location;
					m_MDSize = this.Size;
					return;
				}
			}
			base.OnMouseDown(e);
			Debug.WriteLine("MOuseDown");
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (m_MDPos != MDPos.None)
			{
				int ax = e.X - m_MDP.X;
				int ay = e.Y - m_MDP.Y;
				switch (m_MDPos)
				{
					case MDPos.BottomRight:
						this.Size = new Size(
							m_MDSize.Width + ax,
							m_MDSize.Height + ay);
						break;
					case MDPos.Right:
						this.Size = new Size(
							m_MDSize.Width + ax,
							m_MDSize.Height);
						break;
					case MDPos.Center:
					default:
						this.Location = new Point(
							this.Location.X + ax,
							this.Location.Y + ay);
						break;
				}
				return;
			}
			base.OnMouseMove(e);
			Debug.WriteLine("MOuseMove");
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_MDPos != MDPos.None)
			{
				m_MDPos = MDPos.None;
			}
			base.OnMouseUp(e);
			Debug.WriteLine("MOuseUp");
		}
		// ****************************************************************************
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.Invalidate();

		}
		// ******************************************************************************
		public void SetEventHandler(Control objControl)
		{
			objControl.MouseDown += (sender, e) => this.OnMouseDown(e);
			objControl.MouseMove += (sender, e) => this.OnMouseMove(e);
			objControl.MouseUp += (sender, e) => this.OnMouseUp(e);

		}
		protected override bool ProcessDialogKey(Keys keyData)
		{
#if DEBUG
			this.Text = String.Format("{0}", keyData.ToString());
#endif
			FuncItem? fi = Funcs.FindKeys(keyData);
			if ((fi != null) && (fi.Func != null))
			{
				if (fi.Func()) this.Invalidate();
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}
	}
}
