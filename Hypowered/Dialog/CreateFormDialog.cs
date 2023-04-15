using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Hypowered
{
	public partial class CreateFormDialog : BaseForm
	{
		private string m_BasePath = "";

		public string BasePath
		{
			get 
			{
				return m_BasePath;
			}
			set
			{
				m_BasePath = value;
			}
		}
		public string FullFormName
		{
			get 
			{
				string n = Path.GetFileNameWithoutExtension(tbName.Text);
				return Path.Combine(m_BasePath,n + MainForm.DefEXT); 
			}
			set
			{
				string? s = Path.GetDirectoryName(value);
				if(s!=null) m_BasePath = s;
				tbName.Text = Path.GetFileNameWithoutExtension(value);
			}
		}
		public string FormName
		{
			get { return Path.GetFileNameWithoutExtension(tbName.Text)+ MainForm.DefEXT; }
			set
			{
				tbName.Text= value;
			}
		}

		public Size FormSize
		{
			get
			{
				return new Size(
				(int)numWidth.Value,
				(int)numHeight.Value);
			}
			set
			{
				if (value.Width < 50) value.Width = 50;
				if (value.Height < 50) value.Height = 50;
				numWidth.Value = value.Width;
				numHeight.Value = value.Height;
			}
		}
		public CreateFormDialog()
		{
			InitializeComponent();
			btnCancel.Click += (sender, e) => { this.DialogResult = DialogResult.Cancel; };
			btnOK.Click += (sender, e) =>
			{
				if((numWidth.Value <70)|| (numHeight.Value < 70))
				{
					MessageBox.Show("Size Error!");
					return;
				}
				if (tbName.Text == "")
				{
					MessageBox.Show("Please enter the form name.");
					return; 
				}

				if (tbName.Name.Equals("Home",StringComparison.OrdinalIgnoreCase))
				{
					MessageBox.Show("\"Home\" is reserved and cannot be used.");
					return; 
				}
				
				if(File.Exists(FullFormName))
				{
					MessageBox.Show("A file with the same name already exists.");
					return;
				}

				this.DialogResult = DialogResult.OK;
			};
			btnDir.Click += (sender, e) => { SelecrDir(); };
		}
		private void SelecrDir()
		{
			using (CommonOpenFileDialog dlg = new CommonOpenFileDialog
			{
				Title = "Save Flder",
				// フォルダ選択ダイアログの場合は true
				IsFolderPicker = true,
				// ダイアログが表示されたときの初期ディレクトリを指定
				InitialDirectory = m_BasePath,

				// ユーザーが最近したアイテムの一覧を表示するかどうか
				AddToMostRecentlyUsedList = false,
				// ユーザーがフォルダやライブラリなどのファイルシステム以外の項目を選択できるようにするかどうか
				AllowNonFileSystemItems = false,
				// 最近使用されたフォルダが利用不可能な場合にデフォルトとして使用されるフォルダとパスを設定する
				//DefaultDirectory = ,
				// 存在するファイルのみ許可するかどうか
				EnsureFileExists = true,
				// 存在するパスのみ許可するかどうか
				EnsurePathExists = true,
				// 読み取り専用ファイルを許可するかどうか
				EnsureReadOnly = false,
				// 有効なファイル名のみ許可するかどうか（ファイル名を検証するかどうか）
				EnsureValidNames = true,
				// 複数選択を許可するかどうか
				Multiselect = false,
				// PC やネットワークなどの場所を表示するかどうか
				ShowPlacesList = true
			})
			{
				if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
				{
					if(dlg.FileName!=null)
						m_BasePath = dlg.FileName;
					Debug.WriteLine(dlg.FileName);
				}

			}

		}
	}
}
