using System.Collections;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript.JavaScript;
using System.Xml.Linq;
using System.Text.Json.Nodes;

namespace Hypowered
{
	public partial class Form1 : HyperMainForm
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

		private void Button1_Click_1(object sender, EventArgs e)
		{
			//JsonFile.JsonSave(TargetControl.Name + ".json", TargetControl);
			//string s = PropUtil.GetPropList(this);
			//string s = TargetControl.ToJsonCode();
			//Clipboard.SetText(s);

			//string s = typeof(ControlType).Name;
			
			//MessageBox.Show(s);

			//JsonFile.JsonSave(TargetControl.Name + ".json", TargetControl);
			string s = PropUtil.GetPropList(m_menuBar);
			//string s = TargetControl.ToJsonCode();
			Clipboard.SetText(s);
			MessageBox.Show(s);
		}

		private void button1_Click_2(object sender, EventArgs e)
		{
			/*
			JsonFile jf = new JsonFile();
			jf.SetValue("AAA", "‚ ‚©‚³‚½‚È");
			jf.SetValue("bbb", 12);
			string? js = jf.ToJson();
			if (js == null) return;
			ZipUtil.SetEntryFromStr("aaa.zip", "jf",js);
			textBox1.Text = js+"\r\n************************\r\n";

			string? ss = ZipUtil.GetEntryToStr("aaa.zip", "jf");
			if(ss == null) return;
			textBox1.Text += ss;
			*/
		}

		private void button1_Click_3(object sender, EventArgs e)
		{
			
		}
	}
}