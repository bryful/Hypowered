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
	public class ControlTypeEventArgs : EventArgs
	{
		public ControlType Value;
		public ControlTypeEventArgs(ControlType v)
		{
			Value = v;
		}
	}
	
	public partial class EditControlComb : ComboBox
	{
		private int m_ControlTypeCount = -1;
		public int ControlTypeCount { get { return m_ControlTypeCount; } }
		public delegate void ControlTypeChangedHandler(object sender, ControlTypeEventArgs e);
		public event ControlTypeChangedHandler? ControlTypeChanged;
		protected virtual void OnControlTypeChanged(ControlTypeEventArgs e)
		{
			if (ControlTypeChanged != null)
			{
				ControlTypeChanged(this, e);
			}
		}
		private ControlType m_ct = ControlType.Button;
		[Category("Hypowered")]
		public ControlType ControlType
		{
			get { return (ControlType)this.SelectedIndex; }
			set
			{
				if (SelectedIndex != (int)value)
				{
					this.SelectedIndex = (int)value;
					m_ct = value;
					OnControlTypeChanged(new ControlTypeEventArgs(m_ct));
				}
			}
		}

		public EditControlComb()
		{

			BackColor = ColU.ToColor(HyperColor.Back);
			ForeColor = ColU.ToColor(HyperColor.Fore);
			this.DropDownHeight = 250;
			InitializeComponent();
			Init();
		}
		public void Init()
		{
			base.DropDownStyle = ComboBoxStyle.DropDownList;
			base.FlatStyle = FlatStyle.Flat;
			base.Items.Clear();
			string[] ks = Enum.GetNames(typeof(ControlType));
			base.Items.AddRange(ks);
			m_ControlTypeCount = ks.Length;
			this.SelectedIndex= (int)m_ct;
		}
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			if (m_ct != (ControlType)this.SelectedIndex)
			{
				m_ct = (ControlType)this.SelectedIndex;
				OnControlTypeChanged(new ControlTypeEventArgs(m_ct));
			}
		}
		private bool ShouldSerializeDropDownStyle()
		{
			return false;
		}
		private ComboBoxStyle m_Dummy_cs = ComboBoxStyle.DropDownList;
		[Browsable(false)]
		public new ComboBoxStyle DropDownStyle
		{
			get { return m_Dummy_cs; }
			set { m_Dummy_cs = value; }
		}
		private bool ShouldSerializeFlatStyle()
		{
			return false;
		}
		private FlatStyle m_Dummy_fs = FlatStyle.Flat;
		[Browsable(false)]
		public new FlatStyle FlatStyle
		{
			get { return m_Dummy_fs; }
			set { m_Dummy_fs = value; }
		}
		private bool ShouldSerializeItems()
		{
			return false;
		}
		[Browsable(false)]
		public new ObjectCollection Items
		{
			get { return base.Items; }
		}
	}
}
