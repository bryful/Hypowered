# Hypowered [ハイパワード]

UIをユーザーの手によって自由に編集できる汎用ツールです。<br>
イメージ的には昔のMacにあったHyperCardです。<br>
ボタン等コントロールに含まれたスクリプトを使って機能を拡張できます。
<br>
とりあえず、現状は仕様を決めるためのテスト段階です。<br>
<br>
C#スクリプトで実装しようと思ったがわけわかめな仕様で断念。ClearScriptによるJavaScriptの実装に変更しました。

# 目標

* C#スクリプトによるプログラムが可能な事<br>ClearScriptによるJavaScriptに変更
* コーディング量がなるべく少なくなる形式
* Tool作成が目標なので、カード型データベースにはしない
* 初期のREALBasic(CrossBasic)みたいになるのか？

# Install
インストーラーを作っていないので適当な場所へコピー。<br>
実行ファイルと同じフォルダにHomeファイル(Hypowered.hypf)を書き込むのでProgram Filesは避けてください。<br>
Homeファイル(Hypowered.hypf)は実行ファイル名の拡張子を.hypfに変えた物になります。<br>

<br>
環境変数"HypoweredHome"が設定されているとそこにあるHomeファイル(Hypowered.hypf)を読みます。<br>
実行ファイル名を変えるとそれに合わせて読みに行く環境変数名も変わります。<br>
<br>
コマンドラインで各種設定を行えます。<br>

* Hypowered -inst 拡張子の登録を行います。
* Hypowered -uninst 拡張子の登録を解除します。
* Hypowered -envset 環境変数"HypoweredHome"に設定するディレクトリを選びます。
* Hypowered -envdelete 環境変数"HypoweredHome"を削除します。

他にも
* Hypowered -open "failename"<br> hypfファイルを読みます。ファイルが無いと終了します。
* Hypowered -create "failename"<br> 空のhypfファイルを作成します。 -newでもできます
* Hypowered -call コマンドライン<br> 別プロセスで呼び出します。 引数は -callを除いた物が渡されます。

# Usage 簡単な使い方

* 起動後、メニューから新規フォームで新しいフォームを作成。<br>新規の際にhypfファイルを保存します。保存は自動的に行われます。<br>
* Controlメニューから編集モードを選び編集モードに移行します。編集モードはメニューが赤線になります。
* Controlメニューの新規コントロールで使うコントロールを作成します。
* コントロールリストで各種プロパティを設定出来ます。
* スクリプトを編集します。ターゲットを選択してスクリプト編集で編集開始。<br>スクリプト編集フォームの上のイベントリストからスクリプトを各項目を指定して記述します。
* Executeボタンで仮実行できます。EndEditで編集終了です。<br>
<br>
コントロールの名前はコントロールパネルでは変更できません。コントロールをダブルクリックしてコントロール編集フォームでのみ名前の変更が出来ます。同時にスクリプトエンジンの初期化も行います。<br>
<br>
スクリプトはV8 JavaScriptですが、実際はC#のライブラリを主に使うことになります。<br>命名規則がC#とJavaScriptでは違うので注意。<br>
<br>
JavaScriptのグローバルにコントロールがその名前で登録されています。
```
button.Width = 100;
button.Height = 25;
```
といった感じに簡単に扱えます。

# 組み込みオブジェクト

以下のオブジェクトがスクリプトのルートに登録されています。

* alert(object) アラート表示ダイアログ。<br>文字列だけで無くC#オブジェクトでToString()がある物なら表示されます。JavaScriptオブジェクトはまともに表示されません。
* write(object) 簡易consoleに出力されます。
* writeln(object)簡易consoleに改行付きで出力されます。writeLineじゃないので注意
* cls() 簡易コンソールをクリアします。
* app アプリケーションの組み込みオブジェクトです。

その他以下のオブジェクトがあります。<br>
プレビュー版はバージョン毎にかなり変わります。v1.0.0で固定化する予定です。<br>
<br>
特別な物として
* value
* app.result

があります。これは各コントロールのイベント中のみ有効になるもので、例えばListBoxのSelectedIndexChandedエベント中のスクリプト内では、SelectItemの値が入ります。
valueはJavaScriptオブジェクトでapp.resultはC#オブジェクトになります。

* app.alert()
* app.appFolder
* app.appPath
* app.bag
* app.cls()
* app.colorDialog()
* app.currentPath
* app.eo
* app.executablePath
* app.exit()
* app.findType()
* app.folderSelectDialog()
* app.fontDialog()
* app.fontDialog()
* app.form
* app.forms
* app.formsD
* app.formsEO
* app.getenv()
* app.getenv()
* app.homeFolder
* app.homeHypf
* app.hypfFolder
* app.item()
* app.itemD
* app.itemEO
* app.items[idx].bag
* app.items[idx].eo
* app.items[idx].findFromName()
* app.items[idx].findType()
* app.items[idx].indexOf()
* app.items[idx].item()
* app.items[idx].items
* app.items[idx].itemsEO
* app.items[idx].length
* app.items[idx].numItems
* app.items[idx].strings
* app.loadForm()
* app.loadHome()
* app.main
* app.members()
* app.numForms
* app.numItems
* app.openFileDialog()
* app.openFileDialog()
* app.openForm()
* app.openHome()
* app.result
* app.saveFileDialog()
* app.saveFileDialog()
* app.SetResult()
* app.strings
* app.strings
* app.toString()
* app.write()
* app.writeln()
* app.yesnoDialog()
* value



# Controls
以下の標準コントロールが使用可能です。<br>

| 名前         | 種類                 | 説明                                                |
| ------------ | -------------------- | --------------------------------------------------- |
| Button       | ボタン               | 通常のボタン。テキストと画像が表示出来ます。        |
| Label        | ラベル               | テキストを表示します                                |
| TextBox      | テキストボックス     | C# のTextBox。　1行表示。Muitiline=trueで複数行可能 |
| CheckBox     | チェックボックス     | 通常のチェックボックス                              |
| RadioButton  | ラジオボタンパネル   | 複数のラジオボタンを表示するパネル                  |
| ListBox      | リストボックス       | C#のリストボックスです                              |
| DropdownList | ドロップダウンリスト | ドロップダウンリスト                                |
| DriveIcons   | ドライブ選択アイコン | ドライブを選択するアイコンです。                    |
| DirList      | ディレクトリリスト   | ディレクトリを表示するリストボックスです。          |
| FileList     | ファイルリスト       | ファイルを表示するリストボックス                    |
| PictureBox   | 画像表示             | 画像を表示するコントロール。Targa/Jpeg/PNG/Tiff     |
| Icon         | アイコン表示         | 内蔵画像を表示                                      |
| Design       | デザイン             | 飾りデザインを表示するコントロール                  |
| Html         | Html表示             | C#のWebBrowser。MarkdigでmarkDownも表示可能         |
| FootageList  | フッテージ表示リスト | 連番画像を表示専用のディレクトリファイルリスト      |
| Editor       | テキストエディタ     | C#のAValonEditorです。                              |

# Dependency
Visual studio 2022 C#


# License
This software is released under the MIT License, see LICENSE

# Authors

bry-ful(Hiroshi Furuhashi)<br>
twitter:[bryful](https://twitter.com/bryful)<br>
bryful@gmail.com

# References

