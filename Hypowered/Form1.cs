using Elfie.Serialization;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RoslynPad.Editor;
using RoslynPad.Roslyn;
using System.Collections;
using System.Reflection;
using System.Security.Cryptography;

namespace Hypowered
{
	public partial class Form1 : HyperForm
	{
		RoslynHost host = new RoslynHost();
		public Form1()
		{
			InitializeComponent();
			
			
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			using (SaveFileDialog sfd = new SaveFileDialog())
			{
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					if (sfd.ShowDialog() == DialogResult.OK)
					{
						ZipUtil.AddFromFile(ofd.FileName, sfd.FileName);
					}
				}
			}
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			using OpenFileDialog ofd = new OpenFileDialog();
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				string[] d = ZipUtil.EntryList(ofd.FileName);
				MessageBox.Show(string.Join(", ", d));
			}
		}

		private void Button3_Click(object sender, EventArgs e)
		{
			using OpenFileDialog ofd = new OpenFileDialog();
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				Bitmap? b = ZipUtil.GetEntryBitmap(ofd.FileName, "ウィンドウ管理.png");
				if(b != null)
				{
					pictureBox1.Image= b;
				}
			}
		}
	}
}