# Hypowered [ハイパワード]

UIをユーザーの手によって自由に編集できる汎用ツールです。<br>
イメージ的には昔のMacにあったHyperCardです。<br>
ボタン等コントロールに含まれたスクリプトを使って機能を拡張できます。
<br>
とりあえず、現状は仕様を決めるためのテスト段階です。<br>
<br>
C#スクリプトで実装しようと思ったがわけわかめな仕様で断念。ClearScriptによるJavaScriptの実装に変更しました。<br>
<br>
2回目の仕様変更です。<br>
前回コントロールを自働配置にしたのですが、最初に作る時は楽ですが修正が非常に面倒なので元の配置方法に変えました。<br>
MainFormをもちフォームは子として作るようにしました。複数のフォームを管理するためです。昔のMDI形式みたいな感じです。<br>
今回は、スクリプトよりファイルライブラリの完備を優先しています。Build-inするデータは前はpngでしたが、容量を考えてsvgメインにしようと思っています。ImageMagicKとSVG.NETを組み込んだので、読める画像フォーマットが膨大に増えました。

# 目標

* C#スクリプトによるプログラムが可能な事<br>ClearScriptによるJavaScriptに変更
* コーディング量がなるべく少なくなる形式
* Tool作成が目標なので、カード型データベースにはしない
* 初期のREALBasic(CrossBasic)みたいになるのか？

# Install


# Usage 簡単な使い方


# 組み込みオブジェクト

# Controls

# Dependency
Visual studio 2022 C#


# License
This software is released under the MIT License, see LICENSE

# Authors

bry-ful(Hiroshi Furuhashi)<br>
twitter:[bryful](https://twitter.com/bryful)<br>
bryful@gmail.com

# References

