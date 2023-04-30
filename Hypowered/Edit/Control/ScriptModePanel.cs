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
	public partial class ScriptModePanel : Control
	{
		public delegate void ScriptChangedHandler(object sender, ScriptChangedEventArgs e);
		protected event ScriptChangedHandler? ScriptChanged = null;
		protected virtual void OnEditChanged(ScriptChangedEventArgs e)
		{
			if (ScriptChanged != null)
			{
				ScriptChanged(this, e);
			}
		}
		private bool m_ScriptMode = false;
		public bool IsScript
		{
			get { return m_ScriptMode; }
			set
			{
				bool b = (m_ScriptMode != value);
				m_ScriptMode = value;
				this.Invalidate();
				if(b) { OnEditChanged(new ScriptChangedEventArgs(m_ScriptMode)); }
			}
		}
		protected Bitmap[] Action = new Bitmap[2];
		public ScriptModePanel()
		{
			this.Location = new Point(0, 0);
			this.Size = new Size(50, 20);
			this.MinimumSize = new Size(50, 20);
			this.MaximumSize = new Size(50, 20);
			Action[0] = Properties.Resources.ScriptMode0;
			Action[1] = Properties.Resources.ScriptMode1;
			InitializeComponent();
			base.BackColor = Color.FromArgb(64, 64, 64);
			base.ForeColor = Color.FromArgb(230, 230, 230);
			this.DoubleBuffered = true;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.UpdateStyles();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			using (SolidBrush sb = new SolidBrush(BackColor))
			{
				Graphics g = pe.Graphics;
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);
				int idx = 0;
				if(m_ScriptMode) { idx = 1; }
				g.DrawImage(Action[idx], 0, 0);
			}
		}
		private bool m_MD = false;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				m_ScriptMode = !m_ScriptMode;

				this.Invalidate();
				OnEditChanged(new ScriptChangedEventArgs(m_ScriptMode));
			}
			base.OnMouseDown(e);
		}
	}
	public class ScriptChangedEventArgs : EventArgs
	{
		public bool IsScript;
		public ScriptChangedEventArgs(bool v)
		{
			IsScript = v;
		}
	}
}
