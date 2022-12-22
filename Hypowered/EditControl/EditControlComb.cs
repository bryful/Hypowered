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
	public partial class EditControlComb : ComboBox
	{
		private ControlType m_ct = ControlType.Button;
		[Category("Hypowerd")]
		public ControlType ControlType
		{
			get { return (ControlType)this.SelectedIndex; }
			set
			{
				if ((int)value <=0) value= (ControlType)0;
				else if((int)value >=this.Items.Count) value = (ControlType)(this.Items.Count-1);
				this.SelectedIndex = (int)value;
				m_ct= value;
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
			m_ct = (ControlType)this.SelectedIndex;
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
