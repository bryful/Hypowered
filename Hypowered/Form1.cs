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
	}
}