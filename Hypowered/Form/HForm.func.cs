using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Hypowered
{
	partial class HForm
	{
		// ************************************************************
		public int IndexOfControl(string key)
		{
			return this.Controls.IndexOfKey(key);
		}
		// ************************************************************
		public string ControlNewName(HCType ht)
		{
			string? nm = Enum.GetName(typeof(HCType), ht);
			if (nm == null) nm = "ctrl";
			int idx = 1;
			string s = $"{nm}{idx}";
			while (this.Controls.IndexOfKey(s) >= 0)
			{
				s = $"{nm}{idx}";
				idx++;
			};
			return s;
		}
		private Point pDef = new Point(100, 100);
		// ************************************************************
		public HControl CreateControl(HCType ht)
		{
			HControl hc;
			switch (ht)
			{

				case HCType.Label:
					hc = new HLabel();
					hc.Location = pDef;
					hc.Size = new Size(75, 25);
					break;
				case HCType.TextBox:
					hc = new HTextBox();
					hc.Location = pDef;
					hc.Size = new Size(75, 25);
					break;
				case HCType.PictureBox:
					hc = new HPictureBox();
					hc.Location = pDef;
					hc.Size = new Size(200, 200);
					break;
				case HCType.IconButton:
					hc = new HIconButton();
					hc.Location = pDef;
					break;
				case HCType.ListBox:
					hc = new HListBox();
					hc.Location = pDef;
					hc.Size = new Size(250, 200);
					break;
				case HCType.Button:
				default:
					hc = new HButton();
					hc.Location = pDef;
					hc.Size = new Size(75, 25);
					break;
			}
			hc.SetHForm(this);
			hc.SelectedChanged += (sender, e) => { OnSelectedChanged(new SelectedChangedEventArgs(SelectedArray)); };
			hc.ControlNameChanged += (semder, e) =>
			{
				OnControlNameChanged(new ControlChangedEventArgs(
					e.Name,
					Index,
					e.Index
					));
			};

			return hc;
		}
		public void AddControl(HCType ht, string nm, string tx)
		{
			HControl hControl = CreateControl(ht);
			hControl.Name = nm;
			if (tx != "") tx = nm;
			hControl.Text = tx;
			hControl.SetIsEdit(this.IsEdit);
			this.Controls.Add(hControl);
			this.Controls.SetChildIndex(hControl, 1);
			ChkControl();
			pDef.X += 10;
			if (pDef.X > 250) pDef.X = 100;
			pDef.Y += 10;
			if (pDef.Y > 250) pDef.Y = 100;
			OnControlChanged(new EventArgs());
			SaveToHypf();
		}
		private HCType m_HTypeDef = HCType.Button;
		public void AddControl()
		{
			using (AddControlDialog dlg = new AddControlDialog())
			{
				dlg.HForm = this;
				dlg.TopMost = this.TopMost;
				dlg.Caption = "Add Control Dialog";
				dlg.HType = m_HTypeDef;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					AddControl(dlg.HType, dlg.CName, dlg.CText);
					m_HTypeDef = dlg.HType;
				}
			}

		}
		public void ChkControl()
		{
			if (this.Controls.Count > 0)
			{
				foreach (Control c in this.Controls)
				{
					if (c is HMainMenu)
					{
						if (this.Controls.GetChildIndex(c) != 0)
						{
							this.Controls.SetChildIndex(c, 0);
						}
					}
				}
				int idx = 0;
				foreach (Control c in this.Controls)
				{
					if (c is HMainMenu)
					{
						((HMainMenu)c).Index = idx;
					}
					else if (c is HControl)
					{
						((HControl)c).Index = idx;
					}
					idx++;
				}
			}
		}
		public string[] ControlList()
		{
			List<string> list = new List<string>();
			if (this.Controls.Count > 0)
			{
				foreach (Control c in this.Controls)
				{
					if ((c is HMainMenu) || (c is HControl))
					{
						list.Add(c.Name);
					}
				}
			}
			return list.ToArray();
		}
		public Control[] ControlArray()
		{
			List<Control> list = new List<Control>();
			if (this.Controls.Count > 0)
			{
				foreach (Control c in this.Controls)
				{
					if ((c is HMainMenu) || (c is HControl))
					{
						list.Add(c);
					}
				}
			}
			return list.ToArray();
		}

		public void ControlUp(int[] sels)
		{
			if (sels.Length <= 0) return;
			if (sels[0] == 1) return;
			int idx = sels[0] - 1;
			for (int i = 0; i < sels.Length; i++)
			{
				this.Controls.SetChildIndex(
					this.Controls[sels[i]],
					idx
					);
				idx++;
			}
			OnControlChanged(EventArgs.Empty);
		}
		public void ControlTop(int[] sels)
		{
			if (sels.Length <= 0) return;
			if (sels[0] == 1) return;
			int idx = 1;
			for (int i = 0; i < sels.Length; i++)
			{
				this.Controls.SetChildIndex(
					this.Controls[sels[i]],
					idx
					);
				idx++;
			}
			OnControlChanged(EventArgs.Empty);
		}
		public void ControlDown(int[] sels)
		{
			if (sels.Length <= 0) return;
			if (sels[sels.Length - 1] == this.Controls.Count - 1) return;
			int idx = sels[sels.Length - 1] + 1;
			for (int i = sels.Length - 1; i >= 0; i--)
			{
				this.Controls.SetChildIndex(
					this.Controls[sels[i]],
					idx
					);
				idx--;
			}
			OnControlChanged(EventArgs.Empty);
		}
		public void ControlBottom(int[] sels)
		{
			if (sels.Length <= 0) return;
			if (sels[sels.Length - 1] == this.Controls.Count - 1) return;
			int idx = this.Controls.Count - 1;
			for (int i = sels.Length - 1; i >= 0; i--)
			{
				this.Controls.SetChildIndex(
					this.Controls[sels[i]],
					idx
					);
				idx--;
			}
			OnControlChanged(EventArgs.Empty);
		}
		// *******************************************************************
		private HControl[] SelectedControls()
		{
			List<HControl> list = new List<HControl>();
			if(this.Controls.Count > 1)
			{
				for(int i=1; i<this.Controls.Count; i++)
				{
					if (this.Controls[i] is HControl)
					{
						HControl hc = (HControl)this.Controls[i];
						if (hc.Selected)
						{
							list.Add(hc);
						}
					}
				}
			}
			return list.ToArray();
		}
		// *******************************************************************
		public void ControlMove(ArrowDown ad, int MoveScale)
		{
			HControl[] cs = SelectedControls();
			if(cs.Length==0) return;
			foreach (HControl hc in cs)
			{
				hc.MovePos(ad, MoveScale);
			}
			OnControlChanged(new EventArgs());
		}
		// *******************************************************************
		public void ControlResizeLeftTop(ArrowDown ad, int MoveScale)
		{
			HControl[] cs = SelectedControls();
			if (cs.Length == 0) return;
			foreach (HControl hc in cs)
			{
				hc.ResizeLeftTop(ad, MoveScale);
			}
			OnControlChanged(new EventArgs());
		}
		public void ControlResizeRightBottom(ArrowDown ad, int MoveScale)
		{
			HControl[] cs = SelectedControls();
			if (cs.Length == 0) return;
			foreach (HControl hc in cs)
			{
				hc.ResizeRightBottom(ad, MoveScale);
			}
			OnControlChanged(new EventArgs());
		}
		// ********************************************************************
		public bool SaveToHypf()
		{
			if (ItemsLib.FileName == "")
			{
				if (MainForm != null) MainForm.NewForm();
				if (ItemsLib.FileName == "") return false;
			}

			base.Name = ItemsLib.Name;
			base.Text = base.Name;
			string js = ToJsonCode();
			return ItemsLib.SetText(MainForm.HYPF_JSON, js);
		}
		// ********************************************************************
		public bool LoadFromHypf()
		{
			bool ret = false;
			if (ItemsLib.FileName == "")
			{
				return ret;
			}
			string? js = ItemsLib.GetText(MainForm.HYPF_JSON);
			if ((js == null) || (js == "")) return ret;
			var doc = JsonNode.Parse(js);
			if (doc != null)
			{
				JsonObject? jo = (JsonObject?)doc;
				if (jo != null)
				{
					FromJson(jo);
					ret = true;
				}
			}
			base.Name = ItemsLib.Name;
			base.Text = base.Name;
			return ret;
		}
		// ********************************************************************
		public bool RemoveControl(Control? c)
		{
			try
			{
				this.Controls.Remove(c);
				ChkControl();
				OnControlChanged(new EventArgs());
				return true;
			}
			catch
			{
				return false;
			}
		}
		public bool RemoveControl()
		{
			if (m_TargetControl != null)
			{
				bool ret = RemoveControl(m_TargetControl);
				if (ret)
				{
					m_TargetControl = null;
					OnTargetControlChanged(new TargetControlChangedArgs(null));
					this.Invalidate(); 
				}
				return ret;
			}
			else
			{
				return false;
			}
		}
		public bool RenameForm(string nm)
		{
			bool ret = ItemsLib.Rename(nm);
			if (ret)
			{
				base.Name = ItemsLib.Name;
				OnFormNameChange(new FormChangedEventArgs(base.Name, Index));
			}
			return ret;
		}
	}
}
