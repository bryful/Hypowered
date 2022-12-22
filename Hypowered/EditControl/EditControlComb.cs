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
		[Category("Hypowerd")]
		public ControlType ControlType
		{
			get { return (ControlType)this.SelectedIndex; }
			set
			{
				if ((int)value <=0) value= (ControlType)0;
				else if((int)value >=this.Items.Count) value = (ControlType)(this.Items.Count-1);
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
			InitializeComponent();
			Init();
		}
		public void Init()
		{
			base.DropDownStyle = ComboBoxStyle.DropDownList;
			base.FlatStyle = FlatStyle.Flat;
			this.Items.Clear();
			string[] ks = Enum.GetNames(typeof(ControlType));
			this.Items.AddRange(ks);
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
		[Browsable(false)]
		public new ComboBoxStyle DropDownStyle
		{
			get { return base.DropDownStyle; }
		}
		private bool ShouldSerializeFlatStyle()
		{
			return false;
		}
		[Browsable(false)]
		public new FlatStyle FlatStyle
		{
			get { return base.FlatStyle; }
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
