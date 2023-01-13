﻿using System.Collections;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript.JavaScript;
using System.Xml.Linq;
using System.Dynamic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Hypowered.HScript;
using System.Runtime.Remoting;

namespace Hypowered
{

    public class HyperScript
    {
		protected HyperApp? app = null;
		protected HyperMainForm? m_MainForm  = null;
		public HyperMainForm? MainForm
		{
			get { return m_MainForm; }
		}
		public void SetMainForm(HyperMainForm? mf)
		{
			m_MainForm = mf;
			app = new HyperApp(mf);
			if(engine!=null) engine.AddHostObject("app", app);
		}
		private V8ScriptEngine? engine = null;

        public HyperScript()
        {
			Init();
		}
		public void ExecuteScript(HyperScriptCode cd, ScriptKind sk)
		{
			if (engine == null) return;
			
			if((cd.GetScriptComplieZ(sk)==null))
			{
				string s = cd.GetScriptCode(sk);
				if (s != "") 
				{
					try
					{
						cd.SetScriptComplieZ(sk, engine.Compile(s));
					}catch(Exception e)
					{
						alert(e);
						return;
					}
				}
			}
			try
			{
				engine.Execute(cd.GetScriptComplieZ(sk));
			}
			catch (Exception e)
			{
				alert(e);
				return;
			}
			
		}
		public void ExecuteCode(string code)
		{
			if (engine == null) return;
			try
			{
				engine.Execute(code);
			}
			catch (Exception e)
			{
				alert(e);
			}
		}
		public void AddHostObject(string itemName,Object target )
		{
			if (engine != null) engine.AddHostObject(itemName, target);
		}
		public void AddHostObject(string itemName,HostItemFlags flgs, Object target)
		{
			if (engine != null) engine.AddHostObject(itemName, flgs, target);
		}
		public void AddHostType(Type types)
		{
			if (engine != null) engine.AddHostType(types);
		}
		public void Init()
		{
			if(engine != null)
			{
				engine.Dispose();
			}
			engine = new V8ScriptEngine();
			var typeCollection = new HostTypeCollection(
				"mscorlib",
				"System",
				"System.Core",
				"System.Drawing",
				"System.IO",
				"System.Collections",
				"System.Windows.Forms");

			engine.AddHostObject("dotnet", typeCollection);
			engine.AddHostObject("alert", (object)alert);
			engine.AddHostObject("write", (object)write);
			engine.AddHostObject("writeln", (object)writeLine);
			engine.AddHostObject("clr", (object)writeClear);
			engine.AddHostTypes(new Type[]
			{
				typeof(Enumerable),
				typeof(int),
				typeof(Int32),
				typeof(String),
				typeof(Array),
				typeof(Boolean),
				typeof(bool),
				typeof(Point),
				typeof(Size),
				typeof(Padding),
				typeof(Rectangle),
				typeof(Color),
				typeof(DateTime),
				typeof(Bitmap),
				typeof(HyperBaseForm),
				typeof(HyperMainForm),
				typeof(HyperFormList),
				typeof(List<HyperBaseForm>),
				typeof(HyperAppBase),
				typeof(HyperApp),
				typeof(HyperControl),
				typeof(ControlType),
				typeof(AppControlList),
				typeof(AppFormList),


			});
			if(app!=null) engine.AddHostObject("app",app);


		}
		public void InitForms(HyperMainForm mf)
		{
			if (engine != null)
			{
			}
		}
		private void InitControlsSub(HyperBaseForm? bf)
		{
			if ((engine == null) || (bf == null)) return;

			engine.Script[bf.Name] = new ExpandoObject();
			if (bf.Controls.Count > 0)
			{
				foreach (Control c in bf.Controls)
				{
					if (c is HyperControl)
					{
						engine.AddHostObject(bf.Name +"."+ c.Name, (HyperControl)c);
					}
				}
			}
		}
		public void InitControls(HyperMainForm? mf)
		{

			if ((engine == null) || (mf == null)) return;
			if (mf.Controls.Count > 0)
			{
				foreach (Control c in mf.Controls)
				{
					if (c is HyperControl)
					{
						engine.AddHostObject(c.Name, (HyperControl)c);
					}
				}
			}
			if(mf.forms.Count>0)
			{
				foreach (var c in mf.forms)
				{
					if (c is HyperBaseForm)
					{
						InitControlsSub((HyperBaseForm)c);
					}
				}
			}

			if (app != null)
			{
				app.ListupControls();
			}
			else
			{
				app = new HyperApp(mf);
				app.ListupControls();

			}
		}
		public void alert(object? s)
		{
			AlertForm dlg = new AlertForm();
			dlg.SelectedObject = s;
			dlg.ShowDialog();
			dlg.Dispose();
		}
		public void write(object? s)
		{
			if(m_MainForm!=null)
			{
				m_MainForm.OutputWrite(s);
			}
		}

		public void writeLine(object? s)
		{
			if (m_MainForm != null)
			{
				m_MainForm.OutputWriteLine(s);
			}
		}
		public void writeClear()
		{
			if (m_MainForm != null)
			{
				m_MainForm.OutputClear();
			}
		}
		public bool yesnoDialog(string cap,string title)
		{
			if (m_MainForm != null)
			{
				return answerDialog.Show(cap, title);
			}
			else
			{
				return false;
			}
		}
	}
}
