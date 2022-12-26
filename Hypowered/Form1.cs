using System.Collections;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript.JavaScript;
using System.Xml.Linq;

namespace Hypowered
{
	public partial class Form1 : HyperForm
	{
		public Form1()
		{
			InitializeComponent();

			ChkControls();
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			/*

			var properties = hyperControl1.GetType().GetProperties();
			List<string> values = new List<string>();
			foreach ( var property in properties )
			{
				values.Add(property.Name +":\t" +property.ToString());
			}
			textBox1.Lines= values.ToArray();	
			*/
			//int width = this.Width;
			//textBox1.Text = nameof(width) + ":" + width.GetType().Name;

		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}
}