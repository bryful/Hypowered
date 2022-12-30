﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.Windows.Input;
using System.Text.Json.Nodes;

namespace Hypowered
{
	public class HyperPictLib
	{
		private string m_FileName = "";
		private HyperMainForm? m_form = null;
		public void SetMainForm(HyperMainForm? mf)
		{
			m_form= mf;
			if(m_form!=null)
			{
				m_FileName= m_form.FileName;
			}
		}
		private Bitmap[] m_Bitmaps = new Bitmap[404];
		private Bitmap[] m_UserBitmaps = new Bitmap[0];
		private string[] m_UserBitmapNames = new string[0];

		private readonly string PICTDIR = "pict/";
		#region name
		private string[] m_BitmapsNames = new string[]{
			"ICON_1000",//0
			"ICON_1001",//1
			"ICON_1002",//2
			"ICON_1003",//3
			"ICON_1004",//4
			"ICON_1005",//5
			"ICON_1006",//6
			"ICON_1007",//7
			"ICON_1008",//8
			"ICON_1009",//9
			"ICON_1011",//10
			"ICON_1012",//11
			"ICON_1013",//12
			"ICON_1014",//13
			"ICON_1015",//14
			"ICON_1016",//15
			"ICON_1017",//16
			"ICON_1018",//17
			"ICON_10181",//18
			"ICON_1019",//19
			"ICON_1020",//20
			"ICON_10610",//21
			"ICON_10935",//22
			"ICON_11045",//23
			"ICON_11216",//24
			"ICON_11260",//25
			"ICON_11645",//26
			"ICON_11714",//27
			"ICON_12195",//28
			"ICON_12411",//29
			"ICON_12722",//30
			"ICON_13149",//31
			"ICON_13744",//32
			"ICON_13745",//33
			"ICON_14767",//34
			"ICON_14953",//35
			"ICON_15279",//36
			"ICON_15420",//37
			"ICON_15972",//38
			"ICON_15993",//39
			"ICON_16321",//40
			"ICON_16344",//41
			"ICON_16560",//42
			"ICON_16692",//43
			"ICON_16735",//44
			"ICON_17169",//45
			"ICON_17214",//46
			"ICON_17264",//47
			"ICON_17343",//48
			"ICON_17357",//49
			"ICON_17481",//50
			"ICON_17779",//51
			"ICON_17838",//52
			"ICON_17890",//53
			"ICON_17896",//54
			"ICON_17937",//55
			"ICON_18222",//56
			"ICON_18223",//57
			"ICON_18607",//58
			"ICON_18814",//59
			"ICON_19162",//60
			"ICON_19381",//61
			"ICON_19638",//62
			"ICON_19678",//63
			"ICON_20000",//64
			"ICON_20001",//65
			"ICON_20002",//66
			"ICON_20003",//67
			"ICON_20004",//68
			"ICON_2002",//69
			"ICON_20098",//70
			"ICON_20186",//71
			"ICON_20689",//72
			"ICON_20965",//73
			"ICON_2101",//74
			"ICON_2102",//75
			"ICON_2103",//76
			"ICON_2104",//77
			"ICON_2105",//78
			"ICON_2106",//79
			"ICON_21060",//80
			"ICON_21209",//81
			"ICON_21437",//82
			"ICON_21449",//83
			"ICON_21573",//84
			"ICON_21574",//85
			"ICON_21575",//86
			"ICON_21576",//87
			"ICON_2162",//88
			"ICON_21700",//89
			"ICON_21711",//90
			"ICON_2181",//91
			"ICON_21847",//92
			"ICON_22308",//93
			"ICON_22855",//94
			"ICON_22978",//95
			"ICON_23078",//96
			"ICON_2335",//97
			"ICON_23613",//98
			"ICON_23717",//99
			"ICON_23718",//100
			"ICON_24081",//101
			"ICON_24317",//102
			"ICON_24694",//103
			"ICON_24753",//104
			"ICON_2478",//105
			"ICON_24830",//106
			"ICON_25002",//107
			"ICON_2507",//108
			"ICON_25309",//109
			"ICON_26020",//110
			"ICON_26425",//111
			"ICON_26635",//112
			"ICON_26665",//113
			"ICON_26865",//114
			"ICON_26884",//115
			"ICON_27009",//116
			"ICON_27056",//117
			"ICON_2730",//118
			"ICON_27328",//119
			"ICON_27774",//120
			"ICON_27969",//121
			"ICON_28022",//122
			"ICON_28023",//123
			"ICON_28024",//124
			"ICON_28226",//125
			"ICON_28654",//126
			"ICON_28810",//127
			"ICON_28811",//128
			"ICON_29019",//129
			"ICON_29114",//130
			"ICON_29183",//131
			"ICON_29484",//132
			"ICON_29589",//133
			"ICON_2980",//134
			"ICON_29903",//135
			"ICON_30215",//136
			"ICON_30557",//137
			"ICON_30696",//138
			"ICON_3071",//139
			"ICON_30970",//140
			"ICON_31685",//141
			"ICON_31885",//142
			"ICON_32462",//143
			"ICON_32488",//144
			"ICON_32650",//145
			"ICON_32670",//146
			"ICON_3333",//147
			"ICON_3358",//148
			"ICON_3430",//149
			"ICON_3584",//150
			"ICON_3835",//151
			"ICON_4263",//152
			"ICON_4432",//153
			"ICON_4895",//154
			"ICON_5472",//155
			"ICON_6043",//156
			"ICON_6044",//157
			"ICON_6179",//158
			"ICON_6460",//159
			"ICON_6491",//160
			"ICON_6544",//161
			"ICON_6560",//162
			"ICON_6720",//163
			"ICON_6724",//164
			"ICON_7012",//165
			"ICON_7142",//166
			"ICON_7417",//167
			"ICON_766",//168
			"ICON_8323",//169
			"ICON_8347",//170
			"ICON_8348",//171
			"ICON_8349",//172
			"ICON_8350",//173
			"ICON_8419",//174
			"ICON_8538",//175
			"ICON_8961",//176
			"ICON_8964",//177
			"ICON_8979",//178
			"ICON_8980",//179
			"ICON_902",//180
			"ICON_9104",//181
			"ICON_9120",//182
			"ICON_9301",//183
			"ICON_9761",//184
			"add",//185
			"add_circle",//186
			"all_inclusive",//187
			"apartment",//188
			"api",//189
			"arrow_back",//190
			"arrow_back_ios",//191
			"arrow_back_ios_new",//192
			"arrow_circle_down",//193
			"arrow_downward",//194
			"arrow_drop_down",//195
			"arrow_drop_down_circle",//196
			"arrow_drop_up",//197
			"arrow_forward",//198
			"arrow_forward_ios",//199
			"arrow_insert",//200
			"arrow_left",//201
			"arrow_right",//202
			"arrow_right_alt",//203
			"arrow_upward",//204
			"aspect_ratio",//205
			"assistant_navigation",//206
			"auto_awesome",//207
			"background_replace",//208
			"barcode",//209
			"barcode_scanner",//210
			"barefoot",//211
			"battery_4_bar",//212
			"beach_access",//213
			"block",//214
			"bluetooth",//215
			"bolt",//216
			"cable",//217
			"call",//218
			"call_missed_outgoing",//219
			"camera",//220
			"cell_tower",//221
			"change_history",//222
			"chat",//223
			"check",//224
			"check_box",//225
			"check_box_outline_blank",//226
			"check_circle",//227
			"check_small",//228
			"chevron_left",//229
			"chevron_right",//230
			"close",//231
			"close_fullscreen",//232
			"cloud_queue",//233
			"code",//234
			"construction",//235
			"contactless",//236
			"conversion_path",//237
			"currency_yen",//238
			"dark_mode",//239
			"database",//240
			"delete",//241
			"door_front",//242
			"double_arrow",//243
			"do_not_disturb_on",//244
			"draft",//245
			"draw",//246
			"eco",//247
			"edit",//248
			"edit_square",//249
			"eject",//250
			"emergency_home",//251
			"emoji_objects",//252
			"enable",//253
			"error_med",//254
			"event",//255
			"expand_less",//256
			"expand_more",//257
			"extension",//258
			"fast_forward",//259
			"fast_rewind",//260
			"file_download_done",//261
			"flare",//262
			"flatware",//263
			"flight",//264
			"folder",//265
			"folder_open",//266
			"footprint",//267
			"format_list_numbered",//268
			"forward",//269
			"front_hand",//270
			"grade",//271
			"grid_on",//272
			"group",//273
			"grouped_bar_chart",//274
			"groups",//275
			"handyman",//276
			"headphones",//277
			"help",//278
			"home",//279
			"icecream",//280
			"image",//281
			"info",//282
			"keyboard_control_key",//283
			"keyboard_double_arrow_down",//284
			"keyboard_double_arrow_left",//285
			"keyboard_double_arrow_right",//286
			"keyboard_double_arrow_up",//287
			"label",//288
			"language",//289
			"light",//290
			"light_mode",//291
			"link",//292
			"local_cafe",//293
			"local_florist",//294
			"lock_",//295
			"lock_clock",//296
			"lock_open",//297
			"mail",//298
			"medication",//299
			"memory",//300
			"menu",//301
			"mic_external_on",//302
			"mode_fan",//303
			"monitor",//304
			"mood",//305
			"mop",//306
			"mouse",//307
			"move_down",//308
			"move_up",//309
			"music_note",//310
			"nightlife",//311
			"notifications",//312
			"notifications_active",//313
			"open_with",//314
			"overview",//315
			"pan_tool",//316
			"pan_tool_alt",//317
			"pause",//318
			"pause_circle",//319
			"pedal_bike",//320
			"pending",//321
			"person",//322
			"pets",//323
			"photo_camera",//324
			"pin_drop",//325
			"play_arrow",//326
			"play_circle",//327
			"podcasts",//328
			"policy",//329
			"power",//330
			"power_off",//331
			"power_settings_new",//332
			"priority_high",//333
			"privacy_tip",//334
			"public_",//335
			"radio",//336
			"radio_button_checked",//337
			"radio_button_unchecked",//338
			"rainy",//339
			"receipt_long",//340
			"recycling",//341
			"redo",//342
			"refresh",//343
			"remove",//344
			"repeat",//345
			"replay",//346
			"reply",//347
			"restaurant",//348
			"rocket_launch",//349
			"run_circle",//350
			"safety_check",//351
			"schedule",//352
			"security",//353
			"select_all",//354
			"send",//355
			"sensors",//356
			"sentiment_dissatisfied",//357
			"sentiment_extremely_dissatisfied",//358
			"sentiment_neutral",//359
			"sentiment_satisfied",//360
			"sentiment_very_dissatisfied",//361
			"sentiment_very_satisfied",//362
			"settings",//363
			"settings_backup_restore",//364
			"shield",//365
			"shopping_bag",//366
			"shopping_cart",//367
			"sick",//368
			"skip_next",//369
			"skip_previous",//370
			"skull",//371
			"sos",//372
			"spatial_audio_off",//373
			"speaker",//374
			"sports_esports",//375
			"stars",//376
			"star_rate",//377
			"storage",//378
			"stream",//379
			"subdirectory_arrow_right",//380
			"sunny",//381
			"support",//382
			"switch_left",//383
			"switch_right",//384
			"sync",//385
			"task",//386
			"task_alt",//387
			"token",//388
			"tools_wrench",//389
			"topic",//390
			"umbrella",//391
			"undo",//392
			"unfold_less_double",//393
			"video_file",//394
			"video_label",//395
			"vital_signs",//396
			"volume_up",//397
			"volunteer_activism",//398
			"warning",//399
			"watch",//400
			"water",//401
			"water_drop",//402
			"yard"//403

		};
		#endregion
		public HyperPictLib()
		{
			#region bmp
			int idx = 0;
			m_Bitmaps[idx] = Properties.Resources.ICON_1000; idx++;//0
			m_Bitmaps[idx] = Properties.Resources.ICON_1001; idx++;//1
			m_Bitmaps[idx] = Properties.Resources.ICON_1002; idx++;//2
			m_Bitmaps[idx] = Properties.Resources.ICON_1003; idx++;//3
			m_Bitmaps[idx] = Properties.Resources.ICON_1004; idx++;//4
			m_Bitmaps[idx] = Properties.Resources.ICON_1005; idx++;//5
			m_Bitmaps[idx] = Properties.Resources.ICON_1006; idx++;//6
			m_Bitmaps[idx] = Properties.Resources.ICON_1007; idx++;//7
			m_Bitmaps[idx] = Properties.Resources.ICON_1008; idx++;//8
			m_Bitmaps[idx] = Properties.Resources.ICON_1009; idx++;//9
			m_Bitmaps[idx] = Properties.Resources.ICON_1011; idx++;//10
			m_Bitmaps[idx] = Properties.Resources.ICON_1012; idx++;//11
			m_Bitmaps[idx] = Properties.Resources.ICON_1013; idx++;//12
			m_Bitmaps[idx] = Properties.Resources.ICON_1014; idx++;//13
			m_Bitmaps[idx] = Properties.Resources.ICON_1015; idx++;//14
			m_Bitmaps[idx] = Properties.Resources.ICON_1016; idx++;//15
			m_Bitmaps[idx] = Properties.Resources.ICON_1017; idx++;//16
			m_Bitmaps[idx] = Properties.Resources.ICON_1018; idx++;//17
			m_Bitmaps[idx] = Properties.Resources.ICON_10181; idx++;//18
			m_Bitmaps[idx] = Properties.Resources.ICON_1019; idx++;//19
			m_Bitmaps[idx] = Properties.Resources.ICON_1020; idx++;//20
			m_Bitmaps[idx] = Properties.Resources.ICON_10610; idx++;//21
			m_Bitmaps[idx] = Properties.Resources.ICON_10935; idx++;//22
			m_Bitmaps[idx] = Properties.Resources.ICON_11045; idx++;//23
			m_Bitmaps[idx] = Properties.Resources.ICON_11216; idx++;//24
			m_Bitmaps[idx] = Properties.Resources.ICON_11260; idx++;//25
			m_Bitmaps[idx] = Properties.Resources.ICON_11645; idx++;//26
			m_Bitmaps[idx] = Properties.Resources.ICON_11714; idx++;//27
			m_Bitmaps[idx] = Properties.Resources.ICON_12195; idx++;//28
			m_Bitmaps[idx] = Properties.Resources.ICON_12411; idx++;//29
			m_Bitmaps[idx] = Properties.Resources.ICON_12722; idx++;//30
			m_Bitmaps[idx] = Properties.Resources.ICON_13149; idx++;//31
			m_Bitmaps[idx] = Properties.Resources.ICON_13744; idx++;//32
			m_Bitmaps[idx] = Properties.Resources.ICON_13745; idx++;//33
			m_Bitmaps[idx] = Properties.Resources.ICON_14767; idx++;//34
			m_Bitmaps[idx] = Properties.Resources.ICON_14953; idx++;//35
			m_Bitmaps[idx] = Properties.Resources.ICON_15279; idx++;//36
			m_Bitmaps[idx] = Properties.Resources.ICON_15420; idx++;//37
			m_Bitmaps[idx] = Properties.Resources.ICON_15972; idx++;//38
			m_Bitmaps[idx] = Properties.Resources.ICON_15993; idx++;//39
			m_Bitmaps[idx] = Properties.Resources.ICON_16321; idx++;//40
			m_Bitmaps[idx] = Properties.Resources.ICON_16344; idx++;//41
			m_Bitmaps[idx] = Properties.Resources.ICON_16560; idx++;//42
			m_Bitmaps[idx] = Properties.Resources.ICON_16692; idx++;//43
			m_Bitmaps[idx] = Properties.Resources.ICON_16735; idx++;//44
			m_Bitmaps[idx] = Properties.Resources.ICON_17169; idx++;//45
			m_Bitmaps[idx] = Properties.Resources.ICON_17214; idx++;//46
			m_Bitmaps[idx] = Properties.Resources.ICON_17264; idx++;//47
			m_Bitmaps[idx] = Properties.Resources.ICON_17343; idx++;//48
			m_Bitmaps[idx] = Properties.Resources.ICON_17357; idx++;//49
			m_Bitmaps[idx] = Properties.Resources.ICON_17481; idx++;//50
			m_Bitmaps[idx] = Properties.Resources.ICON_17779; idx++;//51
			m_Bitmaps[idx] = Properties.Resources.ICON_17838; idx++;//52
			m_Bitmaps[idx] = Properties.Resources.ICON_17890; idx++;//53
			m_Bitmaps[idx] = Properties.Resources.ICON_17896; idx++;//54
			m_Bitmaps[idx] = Properties.Resources.ICON_17937; idx++;//55
			m_Bitmaps[idx] = Properties.Resources.ICON_18222; idx++;//56
			m_Bitmaps[idx] = Properties.Resources.ICON_18223; idx++;//57
			m_Bitmaps[idx] = Properties.Resources.ICON_18607; idx++;//58
			m_Bitmaps[idx] = Properties.Resources.ICON_18814; idx++;//59
			m_Bitmaps[idx] = Properties.Resources.ICON_19162; idx++;//60
			m_Bitmaps[idx] = Properties.Resources.ICON_19381; idx++;//61
			m_Bitmaps[idx] = Properties.Resources.ICON_19638; idx++;//62
			m_Bitmaps[idx] = Properties.Resources.ICON_19678; idx++;//63
			m_Bitmaps[idx] = Properties.Resources.ICON_20000; idx++;//64
			m_Bitmaps[idx] = Properties.Resources.ICON_20001; idx++;//65
			m_Bitmaps[idx] = Properties.Resources.ICON_20002; idx++;//66
			m_Bitmaps[idx] = Properties.Resources.ICON_20003; idx++;//67
			m_Bitmaps[idx] = Properties.Resources.ICON_20004; idx++;//68
			m_Bitmaps[idx] = Properties.Resources.ICON_2002; idx++;//69
			m_Bitmaps[idx] = Properties.Resources.ICON_20098; idx++;//70
			m_Bitmaps[idx] = Properties.Resources.ICON_20186; idx++;//71
			m_Bitmaps[idx] = Properties.Resources.ICON_20689; idx++;//72
			m_Bitmaps[idx] = Properties.Resources.ICON_20965; idx++;//73
			m_Bitmaps[idx] = Properties.Resources.ICON_2101; idx++;//74
			m_Bitmaps[idx] = Properties.Resources.ICON_2102; idx++;//75
			m_Bitmaps[idx] = Properties.Resources.ICON_2103; idx++;//76
			m_Bitmaps[idx] = Properties.Resources.ICON_2104; idx++;//77
			m_Bitmaps[idx] = Properties.Resources.ICON_2105; idx++;//78
			m_Bitmaps[idx] = Properties.Resources.ICON_2106; idx++;//79
			m_Bitmaps[idx] = Properties.Resources.ICON_21060; idx++;//80
			m_Bitmaps[idx] = Properties.Resources.ICON_21209; idx++;//81
			m_Bitmaps[idx] = Properties.Resources.ICON_21437; idx++;//82
			m_Bitmaps[idx] = Properties.Resources.ICON_21449; idx++;//83
			m_Bitmaps[idx] = Properties.Resources.ICON_21573; idx++;//84
			m_Bitmaps[idx] = Properties.Resources.ICON_21574; idx++;//85
			m_Bitmaps[idx] = Properties.Resources.ICON_21575; idx++;//86
			m_Bitmaps[idx] = Properties.Resources.ICON_21576; idx++;//87
			m_Bitmaps[idx] = Properties.Resources.ICON_2162; idx++;//88
			m_Bitmaps[idx] = Properties.Resources.ICON_21700; idx++;//89
			m_Bitmaps[idx] = Properties.Resources.ICON_21711; idx++;//90
			m_Bitmaps[idx] = Properties.Resources.ICON_2181; idx++;//91
			m_Bitmaps[idx] = Properties.Resources.ICON_21847; idx++;//92
			m_Bitmaps[idx] = Properties.Resources.ICON_22308; idx++;//93
			m_Bitmaps[idx] = Properties.Resources.ICON_22855; idx++;//94
			m_Bitmaps[idx] = Properties.Resources.ICON_22978; idx++;//95
			m_Bitmaps[idx] = Properties.Resources.ICON_23078; idx++;//96
			m_Bitmaps[idx] = Properties.Resources.ICON_2335; idx++;//97
			m_Bitmaps[idx] = Properties.Resources.ICON_23613; idx++;//98
			m_Bitmaps[idx] = Properties.Resources.ICON_23717; idx++;//99
			m_Bitmaps[idx] = Properties.Resources.ICON_23718; idx++;//100
			m_Bitmaps[idx] = Properties.Resources.ICON_24081; idx++;//101
			m_Bitmaps[idx] = Properties.Resources.ICON_24317; idx++;//102
			m_Bitmaps[idx] = Properties.Resources.ICON_24694; idx++;//103
			m_Bitmaps[idx] = Properties.Resources.ICON_24753; idx++;//104
			m_Bitmaps[idx] = Properties.Resources.ICON_2478; idx++;//105
			m_Bitmaps[idx] = Properties.Resources.ICON_24830; idx++;//106
			m_Bitmaps[idx] = Properties.Resources.ICON_25002; idx++;//107
			m_Bitmaps[idx] = Properties.Resources.ICON_2507; idx++;//108
			m_Bitmaps[idx] = Properties.Resources.ICON_25309; idx++;//109
			m_Bitmaps[idx] = Properties.Resources.ICON_26020; idx++;//110
			m_Bitmaps[idx] = Properties.Resources.ICON_26425; idx++;//111
			m_Bitmaps[idx] = Properties.Resources.ICON_26635; idx++;//112
			m_Bitmaps[idx] = Properties.Resources.ICON_26665; idx++;//113
			m_Bitmaps[idx] = Properties.Resources.ICON_26865; idx++;//114
			m_Bitmaps[idx] = Properties.Resources.ICON_26884; idx++;//115
			m_Bitmaps[idx] = Properties.Resources.ICON_27009; idx++;//116
			m_Bitmaps[idx] = Properties.Resources.ICON_27056; idx++;//117
			m_Bitmaps[idx] = Properties.Resources.ICON_2730; idx++;//118
			m_Bitmaps[idx] = Properties.Resources.ICON_27328; idx++;//119
			m_Bitmaps[idx] = Properties.Resources.ICON_27774; idx++;//120
			m_Bitmaps[idx] = Properties.Resources.ICON_27969; idx++;//121
			m_Bitmaps[idx] = Properties.Resources.ICON_28022; idx++;//122
			m_Bitmaps[idx] = Properties.Resources.ICON_28023; idx++;//123
			m_Bitmaps[idx] = Properties.Resources.ICON_28024; idx++;//124
			m_Bitmaps[idx] = Properties.Resources.ICON_28226; idx++;//125
			m_Bitmaps[idx] = Properties.Resources.ICON_28654; idx++;//126
			m_Bitmaps[idx] = Properties.Resources.ICON_28810; idx++;//127
			m_Bitmaps[idx] = Properties.Resources.ICON_28811; idx++;//128
			m_Bitmaps[idx] = Properties.Resources.ICON_29019; idx++;//129
			m_Bitmaps[idx] = Properties.Resources.ICON_29114; idx++;//130
			m_Bitmaps[idx] = Properties.Resources.ICON_29183; idx++;//131
			m_Bitmaps[idx] = Properties.Resources.ICON_29484; idx++;//132
			m_Bitmaps[idx] = Properties.Resources.ICON_29589; idx++;//133
			m_Bitmaps[idx] = Properties.Resources.ICON_2980; idx++;//134
			m_Bitmaps[idx] = Properties.Resources.ICON_29903; idx++;//135
			m_Bitmaps[idx] = Properties.Resources.ICON_30215; idx++;//136
			m_Bitmaps[idx] = Properties.Resources.ICON_30557; idx++;//137
			m_Bitmaps[idx] = Properties.Resources.ICON_30696; idx++;//138
			m_Bitmaps[idx] = Properties.Resources.ICON_3071; idx++;//139
			m_Bitmaps[idx] = Properties.Resources.ICON_30970; idx++;//140
			m_Bitmaps[idx] = Properties.Resources.ICON_31685; idx++;//141
			m_Bitmaps[idx] = Properties.Resources.ICON_31885; idx++;//142
			m_Bitmaps[idx] = Properties.Resources.ICON_32462; idx++;//143
			m_Bitmaps[idx] = Properties.Resources.ICON_32488; idx++;//144
			m_Bitmaps[idx] = Properties.Resources.ICON_32650; idx++;//145
			m_Bitmaps[idx] = Properties.Resources.ICON_32670; idx++;//146
			m_Bitmaps[idx] = Properties.Resources.ICON_3333; idx++;//147
			m_Bitmaps[idx] = Properties.Resources.ICON_3358; idx++;//148
			m_Bitmaps[idx] = Properties.Resources.ICON_3430; idx++;//149
			m_Bitmaps[idx] = Properties.Resources.ICON_3584; idx++;//150
			m_Bitmaps[idx] = Properties.Resources.ICON_3835; idx++;//151
			m_Bitmaps[idx] = Properties.Resources.ICON_4263; idx++;//152
			m_Bitmaps[idx] = Properties.Resources.ICON_4432; idx++;//153
			m_Bitmaps[idx] = Properties.Resources.ICON_4895; idx++;//154
			m_Bitmaps[idx] = Properties.Resources.ICON_5472; idx++;//155
			m_Bitmaps[idx] = Properties.Resources.ICON_6043; idx++;//156
			m_Bitmaps[idx] = Properties.Resources.ICON_6044; idx++;//157
			m_Bitmaps[idx] = Properties.Resources.ICON_6179; idx++;//158
			m_Bitmaps[idx] = Properties.Resources.ICON_6460; idx++;//159
			m_Bitmaps[idx] = Properties.Resources.ICON_6491; idx++;//160
			m_Bitmaps[idx] = Properties.Resources.ICON_6544; idx++;//161
			m_Bitmaps[idx] = Properties.Resources.ICON_6560; idx++;//162
			m_Bitmaps[idx] = Properties.Resources.ICON_6720; idx++;//163
			m_Bitmaps[idx] = Properties.Resources.ICON_6724; idx++;//164
			m_Bitmaps[idx] = Properties.Resources.ICON_7012; idx++;//165
			m_Bitmaps[idx] = Properties.Resources.ICON_7142; idx++;//166
			m_Bitmaps[idx] = Properties.Resources.ICON_7417; idx++;//167
			m_Bitmaps[idx] = Properties.Resources.ICON_766; idx++;//168
			m_Bitmaps[idx] = Properties.Resources.ICON_8323; idx++;//169
			m_Bitmaps[idx] = Properties.Resources.ICON_8347; idx++;//170
			m_Bitmaps[idx] = Properties.Resources.ICON_8348; idx++;//171
			m_Bitmaps[idx] = Properties.Resources.ICON_8349; idx++;//172
			m_Bitmaps[idx] = Properties.Resources.ICON_8350; idx++;//173
			m_Bitmaps[idx] = Properties.Resources.ICON_8419; idx++;//174
			m_Bitmaps[idx] = Properties.Resources.ICON_8538; idx++;//175
			m_Bitmaps[idx] = Properties.Resources.ICON_8961; idx++;//176
			m_Bitmaps[idx] = Properties.Resources.ICON_8964; idx++;//177
			m_Bitmaps[idx] = Properties.Resources.ICON_8979; idx++;//178
			m_Bitmaps[idx] = Properties.Resources.ICON_8980; idx++;//179
			m_Bitmaps[idx] = Properties.Resources.ICON_902; idx++;//180
			m_Bitmaps[idx] = Properties.Resources.ICON_9104; idx++;//181
			m_Bitmaps[idx] = Properties.Resources.ICON_9120; idx++;//182
			m_Bitmaps[idx] = Properties.Resources.ICON_9301; idx++;//183
			m_Bitmaps[idx] = Properties.Resources.ICON_9761; idx++;//184
			m_Bitmaps[idx] = Properties.Resources.add; idx++;//185
			m_Bitmaps[idx] = Properties.Resources.add_circle; idx++;//186
			m_Bitmaps[idx] = Properties.Resources.all_inclusive; idx++;//187
			m_Bitmaps[idx] = Properties.Resources.apartment; idx++;//188
			m_Bitmaps[idx] = Properties.Resources.api; idx++;//189
			m_Bitmaps[idx] = Properties.Resources.arrow_back; idx++;//190
			m_Bitmaps[idx] = Properties.Resources.arrow_back_ios; idx++;//191
			m_Bitmaps[idx] = Properties.Resources.arrow_back_ios_new; idx++;//192
			m_Bitmaps[idx] = Properties.Resources.arrow_circle_down; idx++;//193
			m_Bitmaps[idx] = Properties.Resources.arrow_downward; idx++;//194
			m_Bitmaps[idx] = Properties.Resources.arrow_drop_down; idx++;//195
			m_Bitmaps[idx] = Properties.Resources.arrow_drop_down_circle; idx++;//196
			m_Bitmaps[idx] = Properties.Resources.arrow_drop_up; idx++;//197
			m_Bitmaps[idx] = Properties.Resources.arrow_forward; idx++;//198
			m_Bitmaps[idx] = Properties.Resources.arrow_forward_ios; idx++;//199
			m_Bitmaps[idx] = Properties.Resources.arrow_insert; idx++;//200
			m_Bitmaps[idx] = Properties.Resources.arrow_left; idx++;//201
			m_Bitmaps[idx] = Properties.Resources.arrow_right; idx++;//202
			m_Bitmaps[idx] = Properties.Resources.arrow_right_alt; idx++;//203
			m_Bitmaps[idx] = Properties.Resources.arrow_upward; idx++;//204
			m_Bitmaps[idx] = Properties.Resources.aspect_ratio; idx++;//205
			m_Bitmaps[idx] = Properties.Resources.assistant_navigation; idx++;//206
			m_Bitmaps[idx] = Properties.Resources.auto_awesome; idx++;//207
			m_Bitmaps[idx] = Properties.Resources.background_replace; idx++;//208
			m_Bitmaps[idx] = Properties.Resources.barcode; idx++;//209
			m_Bitmaps[idx] = Properties.Resources.barcode_scanner; idx++;//210
			m_Bitmaps[idx] = Properties.Resources.barefoot; idx++;//211
			m_Bitmaps[idx] = Properties.Resources.battery_4_bar; idx++;//212
			m_Bitmaps[idx] = Properties.Resources.beach_access; idx++;//213
			m_Bitmaps[idx] = Properties.Resources.block; idx++;//214
			m_Bitmaps[idx] = Properties.Resources.bluetooth; idx++;//215
			m_Bitmaps[idx] = Properties.Resources.bolt; idx++;//216
			m_Bitmaps[idx] = Properties.Resources.cable; idx++;//217
			m_Bitmaps[idx] = Properties.Resources.call; idx++;//218
			m_Bitmaps[idx] = Properties.Resources.call_missed_outgoing; idx++;//219
			m_Bitmaps[idx] = Properties.Resources.camera; idx++;//220
			m_Bitmaps[idx] = Properties.Resources.cell_tower; idx++;//221
			m_Bitmaps[idx] = Properties.Resources.change_history; idx++;//222
			m_Bitmaps[idx] = Properties.Resources.chat; idx++;//223
			m_Bitmaps[idx] = Properties.Resources.check; idx++;//224
			m_Bitmaps[idx] = Properties.Resources.check_box; idx++;//225
			m_Bitmaps[idx] = Properties.Resources.check_box_outline_blank; idx++;//226
			m_Bitmaps[idx] = Properties.Resources.check_circle; idx++;//227
			m_Bitmaps[idx] = Properties.Resources.check_small; idx++;//228
			m_Bitmaps[idx] = Properties.Resources.chevron_left; idx++;//229
			m_Bitmaps[idx] = Properties.Resources.chevron_right; idx++;//230
			m_Bitmaps[idx] = Properties.Resources.close; idx++;//231
			m_Bitmaps[idx] = Properties.Resources.close_fullscreen; idx++;//232
			m_Bitmaps[idx] = Properties.Resources.cloud_queue; idx++;//233
			m_Bitmaps[idx] = Properties.Resources.code; idx++;//234
			m_Bitmaps[idx] = Properties.Resources.construction; idx++;//235
			m_Bitmaps[idx] = Properties.Resources.contactless; idx++;//236
			m_Bitmaps[idx] = Properties.Resources.conversion_path; idx++;//237
			m_Bitmaps[idx] = Properties.Resources.currency_yen; idx++;//238
			m_Bitmaps[idx] = Properties.Resources.dark_mode; idx++;//239
			m_Bitmaps[idx] = Properties.Resources.database; idx++;//240
			m_Bitmaps[idx] = Properties.Resources.delete; idx++;//241
			m_Bitmaps[idx] = Properties.Resources.door_front; idx++;//242
			m_Bitmaps[idx] = Properties.Resources.double_arrow; idx++;//243
			m_Bitmaps[idx] = Properties.Resources.do_not_disturb_on; idx++;//244
			m_Bitmaps[idx] = Properties.Resources.draft; idx++;//245
			m_Bitmaps[idx] = Properties.Resources.draw; idx++;//246
			m_Bitmaps[idx] = Properties.Resources.eco; idx++;//247
			m_Bitmaps[idx] = Properties.Resources.edit; idx++;//248
			m_Bitmaps[idx] = Properties.Resources.edit_square; idx++;//249
			m_Bitmaps[idx] = Properties.Resources.eject; idx++;//250
			m_Bitmaps[idx] = Properties.Resources.emergency_home; idx++;//251
			m_Bitmaps[idx] = Properties.Resources.emoji_objects; idx++;//252
			m_Bitmaps[idx] = Properties.Resources.enable; idx++;//253
			m_Bitmaps[idx] = Properties.Resources.error_med; idx++;//254
			m_Bitmaps[idx] = Properties.Resources.event_; idx++;//255
			m_Bitmaps [idx] = Properties.Resources.expand_less; idx++;//256
			m_Bitmaps [idx] = Properties.Resources.expand_more; idx++;//257
			m_Bitmaps [idx] = Properties.Resources.extension; idx++;//258
			m_Bitmaps [idx] = Properties.Resources.fast_forward; idx++;//259
			m_Bitmaps [idx] = Properties.Resources.fast_rewind; idx++;//260
			m_Bitmaps [idx] = Properties.Resources.file_download_done; idx++;//261
			m_Bitmaps [idx] = Properties.Resources.flare; idx++;//262
			m_Bitmaps [idx] = Properties.Resources.flatware; idx++;//263
			m_Bitmaps [idx] = Properties.Resources.flight; idx++;//264
			m_Bitmaps [idx] = Properties.Resources.folder; idx++;//265
			m_Bitmaps [idx] = Properties.Resources.folder_open; idx++;//266
			m_Bitmaps [idx] = Properties.Resources.footprint; idx++;//267
			m_Bitmaps [idx] = Properties.Resources.format_list_numbered; idx++;//268
			m_Bitmaps [idx] = Properties.Resources.forward; idx++;//269
			m_Bitmaps [idx] = Properties.Resources.front_hand; idx++;//270
			m_Bitmaps [idx] = Properties.Resources.grade; idx++;//271
			m_Bitmaps [idx] = Properties.Resources.grid_on; idx++;//272
			m_Bitmaps [idx] = Properties.Resources.group; idx++;//273
			m_Bitmaps [idx] = Properties.Resources.grouped_bar_chart; idx++;//274
			m_Bitmaps [idx] = Properties.Resources.groups; idx++;//275
			m_Bitmaps [idx] = Properties.Resources.handyman; idx++;//276
			m_Bitmaps [idx] = Properties.Resources.headphones; idx++;//277
			m_Bitmaps [idx] = Properties.Resources.help; idx++;//278
			m_Bitmaps [idx] = Properties.Resources.home; idx++;//279
			m_Bitmaps [idx] = Properties.Resources.icecream; idx++;//280
			m_Bitmaps [idx] = Properties.Resources.image; idx++;//281
			m_Bitmaps [idx] = Properties.Resources.info; idx++;//282
			m_Bitmaps [idx] = Properties.Resources.keyboard_control_key; idx++;//283
			m_Bitmaps [idx] = Properties.Resources.keyboard_double_arrow_down; idx++;//284
			m_Bitmaps [idx] = Properties.Resources.keyboard_double_arrow_left; idx++;//285
			m_Bitmaps [idx] = Properties.Resources.keyboard_double_arrow_right; idx++;//286
			m_Bitmaps [idx] = Properties.Resources.keyboard_double_arrow_up; idx++;//287
			m_Bitmaps [idx] = Properties.Resources.label; idx++;//288
			m_Bitmaps [idx] = Properties.Resources.language; idx++;//289
			m_Bitmaps [idx] = Properties.Resources.light; idx++;//290
			m_Bitmaps [idx] = Properties.Resources.light_mode; idx++;//291
			m_Bitmaps [idx] = Properties.Resources.link; idx++;//292
			m_Bitmaps [idx] = Properties.Resources.local_cafe; idx++;//293
			m_Bitmaps [idx] = Properties.Resources.local_florist; idx++;//294
			m_Bitmaps [idx] = Properties.Resources.lock_; idx++;//295
			m_Bitmaps [idx] = Properties.Resources.lock_clock; idx++;//296
			m_Bitmaps [idx] = Properties.Resources.lock_open; idx++;//297
			m_Bitmaps [idx] = Properties.Resources.mail; idx++;//298
			m_Bitmaps [idx] = Properties.Resources.medication; idx++;//299
			m_Bitmaps [idx] = Properties.Resources.memory; idx++;//300
			m_Bitmaps [idx] = Properties.Resources.menu; idx++;//301
			m_Bitmaps [idx] = Properties.Resources.mic_external_on; idx++;//302
			m_Bitmaps [idx] = Properties.Resources.mode_fan; idx++;//303
			m_Bitmaps [idx] = Properties.Resources.monitor; idx++;//304
			m_Bitmaps [idx] = Properties.Resources.mood; idx++;//305
			m_Bitmaps [idx] = Properties.Resources.mop; idx++;//306
			m_Bitmaps [idx] = Properties.Resources.mouse; idx++;//307
			m_Bitmaps [idx] = Properties.Resources.move_down; idx++;//308
			m_Bitmaps [idx] = Properties.Resources.move_up; idx++;//309
			m_Bitmaps [idx] = Properties.Resources.music_note; idx++;//310
			m_Bitmaps [idx] = Properties.Resources.nightlife; idx++;//311
			m_Bitmaps [idx] = Properties.Resources.notifications; idx++;//312
			m_Bitmaps [idx] = Properties.Resources.notifications_active; idx++;//313
			m_Bitmaps [idx] = Properties.Resources.open_with; idx++;//314
			m_Bitmaps [idx] = Properties.Resources.overview; idx++;//315
			m_Bitmaps [idx] = Properties.Resources.pan_tool; idx++;//316
			m_Bitmaps [idx] = Properties.Resources.pan_tool_alt; idx++;//317
			m_Bitmaps [idx] = Properties.Resources.pause; idx++;//318
			m_Bitmaps [idx] = Properties.Resources.pause_circle; idx++;//319
			m_Bitmaps [idx] = Properties.Resources.pedal_bike; idx++;//320
			m_Bitmaps [idx] = Properties.Resources.pending; idx++;//321
			m_Bitmaps [idx] = Properties.Resources.person; idx++;//322
			m_Bitmaps [idx] = Properties.Resources.pets; idx++;//323
			m_Bitmaps [idx] = Properties.Resources.photo_camera; idx++;//324
			m_Bitmaps [idx] = Properties.Resources.pin_drop; idx++;//325
			m_Bitmaps [idx] = Properties.Resources.play_arrow; idx++;//326
			m_Bitmaps [idx] = Properties.Resources.play_circle; idx++;//327
			m_Bitmaps [idx] = Properties.Resources.podcasts; idx++;//328
			m_Bitmaps [idx] = Properties.Resources.policy; idx++;//329
			m_Bitmaps [idx] = Properties.Resources.power; idx++;//330
			m_Bitmaps [idx] = Properties.Resources.power_off; idx++;//331
			m_Bitmaps [idx] = Properties.Resources.power_settings_new; idx++;//332
			m_Bitmaps [idx] = Properties.Resources.priority_high; idx++;//333
			m_Bitmaps [idx] = Properties.Resources.privacy_tip; idx++;//334
			m_Bitmaps [idx] = Properties.Resources.public_; idx++;//335
			m_Bitmaps [idx] = Properties.Resources.radio; idx++;//336
			m_Bitmaps [idx] = Properties.Resources.radio_button_checked; idx++;//337
			m_Bitmaps [idx] = Properties.Resources.radio_button_unchecked; idx++;//338
			m_Bitmaps [idx] = Properties.Resources.rainy; idx++;//339
			m_Bitmaps [idx] = Properties.Resources.receipt_long; idx++;//340
			m_Bitmaps [idx] = Properties.Resources.recycling; idx++;//341
			m_Bitmaps [idx] = Properties.Resources.redo; idx++;//342
			m_Bitmaps [idx] = Properties.Resources.refresh; idx++;//343
			m_Bitmaps [idx] = Properties.Resources.remove_; idx++;//344
			m_Bitmaps [idx] = Properties.Resources.repeat; idx++;//345
			m_Bitmaps [idx] = Properties.Resources.replay; idx++;//346
			m_Bitmaps [idx] = Properties.Resources.reply; idx++;//347
			m_Bitmaps [idx] = Properties.Resources.restaurant; idx++;//348
			m_Bitmaps [idx] = Properties.Resources.rocket_launch; idx++;//349
			m_Bitmaps [idx] = Properties.Resources.run_circle; idx++;//350
			m_Bitmaps [idx] = Properties.Resources.safety_check; idx++;//351
			m_Bitmaps [idx] = Properties.Resources.schedule; idx++;//352
			m_Bitmaps [idx] = Properties.Resources.security; idx++;//353
			m_Bitmaps [idx] = Properties.Resources.select_all; idx++;//354
			m_Bitmaps [idx] = Properties.Resources.send; idx++;//355
			m_Bitmaps [idx] = Properties.Resources.sensors; idx++;//356
			m_Bitmaps [idx] = Properties.Resources.sentiment_dissatisfied; idx++;//357
			m_Bitmaps [idx] = Properties.Resources.sentiment_extremely_dissatisfied; idx++;//358
			m_Bitmaps [idx] = Properties.Resources.sentiment_neutral; idx++;//359
			m_Bitmaps [idx] = Properties.Resources.sentiment_satisfied; idx++;//360
			m_Bitmaps [idx] = Properties.Resources.sentiment_very_dissatisfied; idx++;//361
			m_Bitmaps [idx] = Properties.Resources.sentiment_very_satisfied; idx++;//362
			m_Bitmaps [idx] = Properties.Resources.settings; idx++;//363
			m_Bitmaps [idx] = Properties.Resources.settings_backup_restore; idx++;//364
			m_Bitmaps [idx] = Properties.Resources.shield; idx++;//365
			m_Bitmaps [idx] = Properties.Resources.shopping_bag; idx++;//366
			m_Bitmaps [idx] = Properties.Resources.shopping_cart; idx++;//367
			m_Bitmaps [idx] = Properties.Resources.sick; idx++;//368
			m_Bitmaps [idx] = Properties.Resources.skip_next; idx++;//369
			m_Bitmaps [idx] = Properties.Resources.skip_previous; idx++;//370
			m_Bitmaps [idx] = Properties.Resources.skull; idx++;//371
			m_Bitmaps [idx] = Properties.Resources.sos; idx++;//372
			m_Bitmaps [idx] = Properties.Resources.spatial_audio_off; idx++;//373
			m_Bitmaps [idx] = Properties.Resources.speaker; idx++;//374
			m_Bitmaps [idx] = Properties.Resources.sports_esports; idx++;//375
			m_Bitmaps [idx] = Properties.Resources.stars; idx++;//376
			m_Bitmaps [idx] = Properties.Resources.star_rate; idx++;//377
			m_Bitmaps [idx] = Properties.Resources.storage; idx++;//378
			m_Bitmaps [idx] = Properties.Resources.stream; idx++;//379
			m_Bitmaps [idx] = Properties.Resources.subdirectory_arrow_right; idx++;//380
			m_Bitmaps [idx] = Properties.Resources.sunny; idx++;//381
			m_Bitmaps [idx] = Properties.Resources.support; idx++;//382
			m_Bitmaps [idx] = Properties.Resources.switch_left; idx++;//383
			m_Bitmaps [idx] = Properties.Resources.switch_right; idx++;//384
			m_Bitmaps [idx] = Properties.Resources.sync; idx++;//385
			m_Bitmaps [idx] = Properties.Resources.task; idx++;//386
			m_Bitmaps [idx] = Properties.Resources.task_alt; idx++;//387
			m_Bitmaps [idx] = Properties.Resources.token; idx++;//388
			m_Bitmaps [idx] = Properties.Resources.tools_wrench; idx++;//389
			m_Bitmaps [idx] = Properties.Resources.topic; idx++;//390
			m_Bitmaps [idx] = Properties.Resources.umbrella; idx++;//391
			m_Bitmaps [idx] = Properties.Resources.undo; idx++;//392
			m_Bitmaps [idx] = Properties.Resources.unfold_less_double; idx++;//393
			m_Bitmaps [idx] = Properties.Resources.video_file; idx++;//394
			m_Bitmaps [idx] = Properties.Resources.video_label; idx++;//395
			m_Bitmaps [idx] = Properties.Resources.vital_signs; idx++;//396
			m_Bitmaps [idx] = Properties.Resources.volume_up; idx++;//397
			m_Bitmaps [idx] = Properties.Resources.volunteer_activism; idx++;//398
			m_Bitmaps [idx] = Properties.Resources.warning; idx++;//399
			m_Bitmaps [idx] = Properties.Resources.watch; idx++;//400
			m_Bitmaps [idx] = Properties.Resources.water; idx++;//401
			m_Bitmaps [idx] = Properties.Resources.water_drop; idx++;//402
			m_Bitmaps [idx] = Properties.Resources.yard; idx++;//403

		#endregion

		}
		// ***********************************************************************
		public bool AddUserPict(string filename)
		{
			bool ret = false;
			if (m_FileName == "") return ret;
			Bitmap? bmp = new Bitmap(filename);
			if (bmp == null) return ret;
			string ent = PICTDIR + Path.GetFileName(filename);
			ret = ZipUtil.AddFromFile(m_FileName, ent, filename);
			if(ret)
			{
				Array.Resize(ref m_UserBitmaps, m_UserBitmaps.Length + 1);
				Array.Resize(ref m_UserBitmapNames, m_UserBitmapNames.Length + 1);
				m_UserBitmaps[m_UserBitmaps.Length-1] = bmp;
				m_UserBitmapNames[m_UserBitmapNames.Length - 1] = Path.GetFileName(filename);
			}

			return ret;
		}
		// ***********************************************************************
		public JsonArray? ToJson()
		{
			JsonArray? arr = new JsonArray();
			if(m_UserBitmapNames.Length>0)
			{
				foreach(var s in m_UserBitmapNames)
				{
					arr.Add(s);
				}
			}
			return arr;
		}
		public void FromJson(JsonArray? ja)
		{
			if(ja==null) return;
			m_UserBitmaps = new Bitmap[0];
			m_UserBitmapNames = new string[0];
			if (ja.Count>0)
			{
				List<string> list = new List<string>();
				foreach(var s in ja)
				{
					if(s==null) continue;	
					string? str = s.GetValue<string?>();
					if(str!=null) list.Add(str);
				}
				m_UserBitmapNames = list.ToArray();
				if(m_UserBitmapNames.Length>0)
				{
					m_UserBitmaps = new Bitmap[m_UserBitmapNames.Length];
					for(int i=0; i< m_UserBitmaps.Length;i++)
					{
						m_UserBitmaps[i] = null;
					}
				}
			}
		}
		// ***********************************************************************
		private Bitmap? LoadUseBitmaps(int idx)
		{
			if((idx<0)||(idx>=m_UserBitmaps.Length)) return null;
			if (m_UserBitmaps[idx] == null)
			{
				if (m_FileName == "") return null;
				string ent = PICTDIR + m_UserBitmapNames[idx];
				Bitmap? bmp = ZipUtil.GetEntryBitmap(m_FileName, ent);
				if(bmp != null)
				{
					m_UserBitmaps[idx] = bmp;
				}
			}
			return m_UserBitmaps[idx];
		}
		public Bitmap? this[int idx]
		{
			get
			{
				if((idx >= m_Bitmaps.Length)&&(idx<Count))
				{
					int idx2 = idx - m_Bitmaps.Length;
					return LoadUseBitmaps(idx2);
				}
				else if( (idx>=0)&&(idx<m_Bitmaps.Length))
				{
					return m_Bitmaps[idx];
				}
				else
				{
					return null;
				}
			}
		}
		public string BitmapName(int idx)
		{
			if ((idx >= m_Bitmaps.Length) && (idx < Count))
			{
				return m_UserBitmapNames[idx - m_Bitmaps.Length];
			}
			else if ((idx >= 0) && (idx < m_Bitmaps.Length))
			{
				return m_BitmapsNames[idx];
			}
			else
			{
				return "";
			}
		}
		public string BitmapInfo(int idx)
		{
			if ((idx >= m_Bitmaps.Length) && (idx < Count))
			{
				int idx2 = idx - m_Bitmaps.Length;
				Bitmap? bmp = LoadUseBitmaps(idx2);
				if (bmp == null) return "Error";
				string s = $"Index:{idx}, Width:{m_UserBitmaps[idx2].Width}, Height:{m_UserBitmaps[idx2].Height}";
				return s;
			}
			else if ((idx >= 0) && (idx < m_Bitmaps.Length))
			{
				string s = $"Index:{idx}, Width:{m_Bitmaps[idx].Width}, Height:{m_Bitmaps[idx].Height}";
				return s;
			}
			else
			{
				return "";
			}
		}
		public Size BitmapSize(int idx)
		{
			if ((idx >= m_Bitmaps.Length) && (idx < Count))
			{
				int idx2 = idx - m_Bitmaps.Length;
				Bitmap? bmp = LoadUseBitmaps(idx2);
				if (bmp == null) return new Size(32,32);
				return bmp.Size;
			}
			else
			if ((idx >= 0) && (idx < m_Bitmaps.Length))
			{
				return m_Bitmaps[idx].Size;
			}
			else
			{
				return new Size(0,0);
			}
		}

		public int IndexOf(string name)
		{
			int ret = -1;
			int idx = 0;
			foreach(string nm in m_BitmapsNames)
			{
				if(nm == name)
				{
					ret = idx;
					return ret;
				}
				idx++;
			}
			foreach (string nm in m_UserBitmapNames)
			{
				if (nm == name)
				{
					ret = idx;
					return ret;
				}
				idx++;
			}
			return ret;
		}
		public Bitmap? Find(string name)
		{
			int idx = IndexOf(name);
			if((idx>=0)&&(idx< m_BitmapsNames.Length))
			{
				return m_Bitmaps[idx];
			}
			else if(idx<Count)
			{
				return m_UserBitmaps[idx- m_BitmapsNames.Length];
			}
			else
			{
				return null;
			}
		}
		public int Count
		{
			get { return m_BitmapsNames.Length + m_UserBitmapNames.Length; }
		}
		public bool IsUserPict(int idx)
		{
			return (idx >= m_BitmapsNames.Length);
		}
		public Bitmap Thum(int idx,int width=48, int height=48)
		{
			Bitmap ret = new Bitmap(width, height);
			if ((idx < 0) || (idx >=Count)) return ret;
			Graphics g = Graphics.FromImage(ret);
			Bitmap bmp;
			if ((idx >= 0) && (idx < m_BitmapsNames.Length))
			{
				bmp = m_Bitmaps[idx];
			}
			else if (idx < Count)
			{
				bmp = m_UserBitmaps[idx - m_BitmapsNames.Length];
			}
			else
			{
				return ret;
			}
			bmp.SetResolution(96, 96);
			int w = bmp.Width;
			int h = bmp.Height;
			if((w<= ret.Width) &&(h<=ret.Height))
			{
				int cx = ret.Width/2 - w / 2;
				int cy = ret.Height/2 - h / 2;

				g.DrawImage(bmp, new Rectangle(cx, cy, w, h));
			}
			else
			{
				double scal = width / (double)w;
				if (h > w) scal = height / (double)h;
				int w2 = (int)((double)w * scal);
				int h2 = (int)((double)h * scal);
				Rectangle rr = new Rectangle(
					ret.Width / 2 - w2 / 2,
					ret.Height / 2 - h2 / 2,
					w2, h2);
				g.DrawImage(bmp, rr);
			}
			return ret;
		}
	}
}
