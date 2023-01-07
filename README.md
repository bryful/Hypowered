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

他にも
* Hypowered -open "failename"<br> hypfファイルを読みます。ファイルが無いと終了します。
* Hypowered -create "failename"<br> 空のhypfファイルを作成します。 -newデモできます
* Hypowered -call コマンドライン<br> 別プロセスで呼び出します。 引数は -callを除いた物が渡されます。

# Usage

# Dependency
Visual studio 2022 C#


# License
This software is released under the MIT License, see LICENSE

# Authors

bry-ful(Hiroshi Furuhashi)<br>
twitter:[bryful](https://twitter.com/bryful)<br>
bryful@gmail.com

# References

