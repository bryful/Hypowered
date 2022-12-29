using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.Windows.Input;

namespace Hypowered
{
	public class HyperPictLib
	{
		private Bitmap[] m_Bitmaps = new Bitmap[290];
		private Bitmap[] m_UserBitmaps = new Bitmap[0];
		private string[] m_UserBitmapsNames = new string[0];
		#region name
		private string[] m_BitmapsNames = new string[]{
"add",
"add_circle",
"all_inclusive",
"api",
"arrow_back",
"arrow_back_ios",
"arrow_forward",
"arrow_forward_ios",
"aspect_ratio",
"auto_awesome",
"background_replace",
"barcode",
"barcode_scanner",
"battery_4_bar",
"bluetooth",
"bolt",
"cable",
"call",
"call_missed_outgoing",
"camera",
"cell_tower",
"chat",
"check",
"check_circle",
"close",
"construction",
"conversion_path",
"database",
"delete",
"double_arrow",
"draft",
"draw",
"edit",
"edit_square",
"eject",
"extension",
"flight",
"folder",
"folder_open",
"format_list_numbered",
"front_hand",
"grid_on",
"grouped_bar_chart",
"handyman",
"headphones",
"help",
"home",
"ICON_1000",
"ICON_1001",
"ICON_1002",
"ICON_1003",
"ICON_1004",
"ICON_1005",
"ICON_1006",
"ICON_1007",
"ICON_1008",
"ICON_1009",
"ICON_1011",
"ICON_1012",
"ICON_1013",
"ICON_1014",
"ICON_1015",
"ICON_1016",
"ICON_1017",
"ICON_1018",
"ICON_10181",
"ICON_1019",
"ICON_1020",
"ICON_10610",
"ICON_10935",
"ICON_11045",
"ICON_11216",
"ICON_11260",
"ICON_11645",
"ICON_11714",
"ICON_12195",
"ICON_12411",
"ICON_12722",
"ICON_13149",
"ICON_13744",
"ICON_13745",
"ICON_14767",
"ICON_14953",
"ICON_15279",
"ICON_15420",
"ICON_15972",
"ICON_15993",
"ICON_16321",
"ICON_16344",
"ICON_16560",
"ICON_16692",
"ICON_16735",
"ICON_17169",
"ICON_17214",
"ICON_17264",
"ICON_17343",
"ICON_17357",
"ICON_17481",
"ICON_17779",
"ICON_17838",
"ICON_17890",
"ICON_17896",
"ICON_17937",
"ICON_18222",
"ICON_18223",
"ICON_18607",
"ICON_18814",
"ICON_19162",
"ICON_19381",
"ICON_19638",
"ICON_19678",
"ICON_20000",
"ICON_20001",
"ICON_20002",
"ICON_20003",
"ICON_20004",
"ICON_2002",
"ICON_20098",
"ICON_20186",
"ICON_20689",
"ICON_20965",
"ICON_2101",
"ICON_2102",
"ICON_2103",
"ICON_2104",
"ICON_2105",
"ICON_2106",
"ICON_21060",
"ICON_21209",
"ICON_21437",
"ICON_21449",
"ICON_21573",
"ICON_21574",
"ICON_21575",
"ICON_21576",
"ICON_2162",
"ICON_21700",
"ICON_21711",
"ICON_2181",
"ICON_21847",
"ICON_22308",
"ICON_22855",
"ICON_22978",
"ICON_23078",
"ICON_2335",
"ICON_23613",
"ICON_23717",
"ICON_23718",
"ICON_24081",
"ICON_24317",
"ICON_24694",
"ICON_24753",
"ICON_2478",
"ICON_24830",
"ICON_25002",
"ICON_2507",
"ICON_25309",
"ICON_26020",
"ICON_26425",
"ICON_26635",
"ICON_26665",
"ICON_26865",
"ICON_26884",
"ICON_27009",
"ICON_27056",
"ICON_2730",
"ICON_27328",
"ICON_27774",
"ICON_27969",
"ICON_28022",
"ICON_28023",
"ICON_28024",
"ICON_28226",
"ICON_28654",
"ICON_28810",
"ICON_28811",
"ICON_29019",
"ICON_29114",
"ICON_29183",
"ICON_29484",
"ICON_29589",
"ICON_2980",
"ICON_29903",
"ICON_30215",
"ICON_30557",
"ICON_30696",
"ICON_3071",
"ICON_30970",
"ICON_31685",
"ICON_31885",
"ICON_32462",
"ICON_32488",
"ICON_32650",
"ICON_32670",
"ICON_3333",
"ICON_3358",
"ICON_3430",
"ICON_3584",
"ICON_3835",
"ICON_4263",
"ICON_4432",
"ICON_4895",
"ICON_5472",
"ICON_6043",
"ICON_6044",
"ICON_6179",
"ICON_6460",
"ICON_6491",
"ICON_6544",
"ICON_6560",
"ICON_6720",
"ICON_6724",
"ICON_7012",
"ICON_7142",
"ICON_7417",
"ICON_766",
"ICON_8323",
"ICON_8347",
"ICON_8348",
"ICON_8349",
"ICON_8350",
"ICON_8419",
"ICON_8538",
"ICON_8961",
"ICON_8964",
"ICON_8979",
"ICON_8980",
"ICON_902",
"ICON_9104",
"ICON_9120",
"ICON_9301",
"ICON_9761",
"image",
"info",
"keyboard_double_arrow_right",
"language",
"link",
"local_florist",
"lock",
"lock_clock",
"lock_open",
"mail",
"menu",
"mood",
"mouse",
"music_note",
"notifications",
"notifications_active",
"overview",
"pan_tool",
"pan_tool_alt",
"pedal_bike",
"person",
"photo_camera",
"pin_drop",
"podcasts",
"power",
"power_settings_new",
"priority_high",
"radio_button_checked",
"radio_button_unchecked",
"receipt_long",
"recycling",
"restaurant",
"run_circle",
"safety_check",
"schedule",
"send",
"sentiment_dissatisfied",
"sentiment_extremely_dissatisfied",
"sentiment_neutral",
"sentiment_satisfied",
"sentiment_very_dissatisfied",
"sentiment_very_satisfied",
"settings",
"shopping_bag",
"shopping_cart",
"skull",
"spatial_audio_off",
"sports_esports",
"support",
"sync",
"task",
"topic",
"video_file",
"vital_signs",
"volume_up",
"volunteer_activism",
"warning",
"watch",
		};
		#endregion
		public HyperPictLib()
		{
			#region bmp
			m_Bitmaps[000] = Properties.Resources.add;
			m_Bitmaps[001] = Properties.Resources.add_circle;
			m_Bitmaps[002] = Properties.Resources.all_inclusive;
			m_Bitmaps[003] = Properties.Resources.api;
			m_Bitmaps[004] = Properties.Resources.arrow_back;
			m_Bitmaps[005] = Properties.Resources.arrow_back_ios;
			m_Bitmaps[006] = Properties.Resources.arrow_forward;
			m_Bitmaps[007] = Properties.Resources.arrow_forward_ios;
			m_Bitmaps[008] = Properties.Resources.aspect_ratio;
			m_Bitmaps[009] = Properties.Resources.auto_awesome;
			m_Bitmaps[010] = Properties.Resources.background_replace;
			m_Bitmaps[011] = Properties.Resources.barcode;
			m_Bitmaps[012] = Properties.Resources.barcode_scanner;
			m_Bitmaps[013] = Properties.Resources.battery_4_bar;
			m_Bitmaps[014] = Properties.Resources.bluetooth;
			m_Bitmaps[015] = Properties.Resources.bolt;
			m_Bitmaps[016] = Properties.Resources.cable;
			m_Bitmaps[017] = Properties.Resources.call;
			m_Bitmaps[018] = Properties.Resources.call_missed_outgoing;
			m_Bitmaps[019] = Properties.Resources.camera;
			m_Bitmaps[020] = Properties.Resources.cell_tower;
			m_Bitmaps[021] = Properties.Resources.chat;
			m_Bitmaps[022] = Properties.Resources.check;
			m_Bitmaps[023] = Properties.Resources.check_circle;
			m_Bitmaps[024] = Properties.Resources.close;
			m_Bitmaps[025] = Properties.Resources.construction;
			m_Bitmaps[026] = Properties.Resources.conversion_path;
			m_Bitmaps[027] = Properties.Resources.database;
			m_Bitmaps[028] = Properties.Resources.delete;
			m_Bitmaps[029] = Properties.Resources.double_arrow;
			m_Bitmaps[030] = Properties.Resources.draft;
			m_Bitmaps[031] = Properties.Resources.draw;
			m_Bitmaps[032] = Properties.Resources.edit;
			m_Bitmaps[033] = Properties.Resources.edit_square;
			m_Bitmaps[034] = Properties.Resources.eject;
			m_Bitmaps[035] = Properties.Resources.extension;
			m_Bitmaps[036] = Properties.Resources.flight;
			m_Bitmaps[037] = Properties.Resources.folder;
			m_Bitmaps[038] = Properties.Resources.folder_open;
			m_Bitmaps[039] = Properties.Resources.format_list_numbered;
			m_Bitmaps[040] = Properties.Resources.front_hand;
			m_Bitmaps[041] = Properties.Resources.grid_on;
			m_Bitmaps[042] = Properties.Resources.grouped_bar_chart;
			m_Bitmaps[043] = Properties.Resources.handyman;
			m_Bitmaps[044] = Properties.Resources.headphones;
			m_Bitmaps[045] = Properties.Resources.help;
			m_Bitmaps[046] = Properties.Resources.home;
			m_Bitmaps[047] = Properties.Resources.ICON_1000;
			m_Bitmaps[048] = Properties.Resources.ICON_1001;
			m_Bitmaps[049] = Properties.Resources.ICON_1002;
			m_Bitmaps[050] = Properties.Resources.ICON_1003;
			m_Bitmaps[051] = Properties.Resources.ICON_1004;
			m_Bitmaps[052] = Properties.Resources.ICON_1005;
			m_Bitmaps[053] = Properties.Resources.ICON_1006;
			m_Bitmaps[054] = Properties.Resources.ICON_1007;
			m_Bitmaps[055] = Properties.Resources.ICON_1008;
			m_Bitmaps[056] = Properties.Resources.ICON_1009;
			m_Bitmaps[057] = Properties.Resources.ICON_1011;
			m_Bitmaps[058] = Properties.Resources.ICON_1012;
			m_Bitmaps[059] = Properties.Resources.ICON_1013;
			m_Bitmaps[060] = Properties.Resources.ICON_1014;
			m_Bitmaps[061] = Properties.Resources.ICON_1015;
			m_Bitmaps[062] = Properties.Resources.ICON_1016;
			m_Bitmaps[063] = Properties.Resources.ICON_1017;
			m_Bitmaps[064] = Properties.Resources.ICON_1018;
			m_Bitmaps[065] = Properties.Resources.ICON_10181;
			m_Bitmaps[066] = Properties.Resources.ICON_1019;
			m_Bitmaps[067] = Properties.Resources.ICON_1020;
			m_Bitmaps[068] = Properties.Resources.ICON_10610;
			m_Bitmaps[069] = Properties.Resources.ICON_10935;
			m_Bitmaps[070] = Properties.Resources.ICON_11045;
			m_Bitmaps[071] = Properties.Resources.ICON_11216;
			m_Bitmaps[072] = Properties.Resources.ICON_11260;
			m_Bitmaps[073] = Properties.Resources.ICON_11645;
			m_Bitmaps[074] = Properties.Resources.ICON_11714;
			m_Bitmaps[075] = Properties.Resources.ICON_12195;
			m_Bitmaps[076] = Properties.Resources.ICON_12411;
			m_Bitmaps[077] = Properties.Resources.ICON_12722;
			m_Bitmaps[078] = Properties.Resources.ICON_13149;
			m_Bitmaps[079] = Properties.Resources.ICON_13744;
			m_Bitmaps[080] = Properties.Resources.ICON_13745;
			m_Bitmaps[081] = Properties.Resources.ICON_14767;
			m_Bitmaps[082] = Properties.Resources.ICON_14953;
			m_Bitmaps[083] = Properties.Resources.ICON_15279;
			m_Bitmaps[084] = Properties.Resources.ICON_15420;
			m_Bitmaps[085] = Properties.Resources.ICON_15972;
			m_Bitmaps[086] = Properties.Resources.ICON_15993;
			m_Bitmaps[087] = Properties.Resources.ICON_16321;
			m_Bitmaps[088] = Properties.Resources.ICON_16344;
			m_Bitmaps[089] = Properties.Resources.ICON_16560;
			m_Bitmaps[090] = Properties.Resources.ICON_16692;
			m_Bitmaps[091] = Properties.Resources.ICON_16735;
			m_Bitmaps[092] = Properties.Resources.ICON_17169;
			m_Bitmaps[093] = Properties.Resources.ICON_17214;
			m_Bitmaps[094] = Properties.Resources.ICON_17264;
			m_Bitmaps[095] = Properties.Resources.ICON_17343;
			m_Bitmaps[096] = Properties.Resources.ICON_17357;
			m_Bitmaps[097] = Properties.Resources.ICON_17481;
			m_Bitmaps[098] = Properties.Resources.ICON_17779;
			m_Bitmaps[099] = Properties.Resources.ICON_17838;
			m_Bitmaps[100] = Properties.Resources.ICON_17890;
			m_Bitmaps[101] = Properties.Resources.ICON_17896;
			m_Bitmaps[102] = Properties.Resources.ICON_17937;
			m_Bitmaps[103] = Properties.Resources.ICON_18222;
			m_Bitmaps[104] = Properties.Resources.ICON_18223;
			m_Bitmaps[105] = Properties.Resources.ICON_18607;
			m_Bitmaps[106] = Properties.Resources.ICON_18814;
			m_Bitmaps[107] = Properties.Resources.ICON_19162;
			m_Bitmaps[108] = Properties.Resources.ICON_19381;
			m_Bitmaps[109] = Properties.Resources.ICON_19638;
			m_Bitmaps[110] = Properties.Resources.ICON_19678;
			m_Bitmaps[111] = Properties.Resources.ICON_20000;
			m_Bitmaps[112] = Properties.Resources.ICON_20001;
			m_Bitmaps[113] = Properties.Resources.ICON_20002;
			m_Bitmaps[114] = Properties.Resources.ICON_20003;
			m_Bitmaps[115] = Properties.Resources.ICON_20004;
			m_Bitmaps[116] = Properties.Resources.ICON_2002;
			m_Bitmaps[117] = Properties.Resources.ICON_20098;
			m_Bitmaps[118] = Properties.Resources.ICON_20186;
			m_Bitmaps[119] = Properties.Resources.ICON_20689;
			m_Bitmaps[120] = Properties.Resources.ICON_20965;
			m_Bitmaps[121] = Properties.Resources.ICON_2101;
			m_Bitmaps[122] = Properties.Resources.ICON_2102;
			m_Bitmaps[123] = Properties.Resources.ICON_2103;
			m_Bitmaps[124] = Properties.Resources.ICON_2104;
			m_Bitmaps[125] = Properties.Resources.ICON_2105;
			m_Bitmaps[126] = Properties.Resources.ICON_2106;
			m_Bitmaps[127] = Properties.Resources.ICON_21060;
			m_Bitmaps[128] = Properties.Resources.ICON_21209;
			m_Bitmaps[129] = Properties.Resources.ICON_21437;
			m_Bitmaps[130] = Properties.Resources.ICON_21449;
			m_Bitmaps[131] = Properties.Resources.ICON_21573;
			m_Bitmaps[132] = Properties.Resources.ICON_21574;
			m_Bitmaps[133] = Properties.Resources.ICON_21575;
			m_Bitmaps[134] = Properties.Resources.ICON_21576;
			m_Bitmaps[135] = Properties.Resources.ICON_2162;
			m_Bitmaps[136] = Properties.Resources.ICON_21700;
			m_Bitmaps[137] = Properties.Resources.ICON_21711;
			m_Bitmaps[138] = Properties.Resources.ICON_2181;
			m_Bitmaps[139] = Properties.Resources.ICON_21847;
			m_Bitmaps[140] = Properties.Resources.ICON_22308;
			m_Bitmaps[141] = Properties.Resources.ICON_22855;
			m_Bitmaps[142] = Properties.Resources.ICON_22978;
			m_Bitmaps[143] = Properties.Resources.ICON_23078;
			m_Bitmaps[144] = Properties.Resources.ICON_2335;
			m_Bitmaps[145] = Properties.Resources.ICON_23613;
			m_Bitmaps[146] = Properties.Resources.ICON_23717;
			m_Bitmaps[147] = Properties.Resources.ICON_23718;
			m_Bitmaps[148] = Properties.Resources.ICON_24081;
			m_Bitmaps[149] = Properties.Resources.ICON_24317;
			m_Bitmaps[150] = Properties.Resources.ICON_24694;
			m_Bitmaps[151] = Properties.Resources.ICON_24753;
			m_Bitmaps[152] = Properties.Resources.ICON_2478;
			m_Bitmaps[153] = Properties.Resources.ICON_24830;
			m_Bitmaps[154] = Properties.Resources.ICON_25002;
			m_Bitmaps[155] = Properties.Resources.ICON_2507;
			m_Bitmaps[156] = Properties.Resources.ICON_25309;
			m_Bitmaps[157] = Properties.Resources.ICON_26020;
			m_Bitmaps[158] = Properties.Resources.ICON_26425;
			m_Bitmaps[159] = Properties.Resources.ICON_26635;
			m_Bitmaps[160] = Properties.Resources.ICON_26665;
			m_Bitmaps[161] = Properties.Resources.ICON_26865;
			m_Bitmaps[162] = Properties.Resources.ICON_26884;
			m_Bitmaps[163] = Properties.Resources.ICON_27009;
			m_Bitmaps[164] = Properties.Resources.ICON_27056;
			m_Bitmaps[165] = Properties.Resources.ICON_2730;
			m_Bitmaps[166] = Properties.Resources.ICON_27328;
			m_Bitmaps[167] = Properties.Resources.ICON_27774;
			m_Bitmaps[168] = Properties.Resources.ICON_27969;
			m_Bitmaps[169] = Properties.Resources.ICON_28022;
			m_Bitmaps[170] = Properties.Resources.ICON_28023;
			m_Bitmaps[171] = Properties.Resources.ICON_28024;
			m_Bitmaps[172] = Properties.Resources.ICON_28226;
			m_Bitmaps[173] = Properties.Resources.ICON_28654;
			m_Bitmaps[174] = Properties.Resources.ICON_28810;
			m_Bitmaps[175] = Properties.Resources.ICON_28811;
			m_Bitmaps[176] = Properties.Resources.ICON_29019;
			m_Bitmaps[177] = Properties.Resources.ICON_29114;
			m_Bitmaps[178] = Properties.Resources.ICON_29183;
			m_Bitmaps[179] = Properties.Resources.ICON_29484;
			m_Bitmaps[180] = Properties.Resources.ICON_29589;
			m_Bitmaps[181] = Properties.Resources.ICON_2980;
			m_Bitmaps[182] = Properties.Resources.ICON_29903;
			m_Bitmaps[183] = Properties.Resources.ICON_30215;
			m_Bitmaps[184] = Properties.Resources.ICON_30557;
			m_Bitmaps[185] = Properties.Resources.ICON_30696;
			m_Bitmaps[186] = Properties.Resources.ICON_3071;
			m_Bitmaps[187] = Properties.Resources.ICON_30970;
			m_Bitmaps[188] = Properties.Resources.ICON_31685;
			m_Bitmaps[189] = Properties.Resources.ICON_31885;
			m_Bitmaps[190] = Properties.Resources.ICON_32462;
			m_Bitmaps[191] = Properties.Resources.ICON_32488;
			m_Bitmaps[192] = Properties.Resources.ICON_32650;
			m_Bitmaps[193] = Properties.Resources.ICON_32670;
			m_Bitmaps[194] = Properties.Resources.ICON_3333;
			m_Bitmaps[195] = Properties.Resources.ICON_3358;
			m_Bitmaps[196] = Properties.Resources.ICON_3430;
			m_Bitmaps[197] = Properties.Resources.ICON_3584;
			m_Bitmaps[198] = Properties.Resources.ICON_3835;
			m_Bitmaps[199] = Properties.Resources.ICON_4263;
			m_Bitmaps[200] = Properties.Resources.ICON_4432;
			m_Bitmaps[201] = Properties.Resources.ICON_4895;
			m_Bitmaps[202] = Properties.Resources.ICON_5472;
			m_Bitmaps[203] = Properties.Resources.ICON_6043;
			m_Bitmaps[204] = Properties.Resources.ICON_6044;
			m_Bitmaps[205] = Properties.Resources.ICON_6179;
			m_Bitmaps[206] = Properties.Resources.ICON_6460;
			m_Bitmaps[207] = Properties.Resources.ICON_6491;
			m_Bitmaps[208] = Properties.Resources.ICON_6544;
			m_Bitmaps[209] = Properties.Resources.ICON_6560;
			m_Bitmaps[210] = Properties.Resources.ICON_6720;
			m_Bitmaps[211] = Properties.Resources.ICON_6724;
			m_Bitmaps[212] = Properties.Resources.ICON_7012;
			m_Bitmaps[213] = Properties.Resources.ICON_7142;
			m_Bitmaps[214] = Properties.Resources.ICON_7417;
			m_Bitmaps[215] = Properties.Resources.ICON_766;
			m_Bitmaps[216] = Properties.Resources.ICON_8323;
			m_Bitmaps[217] = Properties.Resources.ICON_8347;
			m_Bitmaps[218] = Properties.Resources.ICON_8348;
			m_Bitmaps[219] = Properties.Resources.ICON_8349;
			m_Bitmaps[220] = Properties.Resources.ICON_8350;
			m_Bitmaps[221] = Properties.Resources.ICON_8419;
			m_Bitmaps[222] = Properties.Resources.ICON_8538;
			m_Bitmaps[223] = Properties.Resources.ICON_8961;
			m_Bitmaps[224] = Properties.Resources.ICON_8964;
			m_Bitmaps[225] = Properties.Resources.ICON_8979;
			m_Bitmaps[226] = Properties.Resources.ICON_8980;
			m_Bitmaps[227] = Properties.Resources.ICON_902;
			m_Bitmaps[228] = Properties.Resources.ICON_9104;
			m_Bitmaps[229] = Properties.Resources.ICON_9120;
			m_Bitmaps[230] = Properties.Resources.ICON_9301;
			m_Bitmaps[231] = Properties.Resources.ICON_9761;
			m_Bitmaps[232] = Properties.Resources.image;
			m_Bitmaps[233] = Properties.Resources.info;
			m_Bitmaps[234] = Properties.Resources.keyboard_double_arrow_right;
			m_Bitmaps[235] = Properties.Resources.language;
			m_Bitmaps[236] = Properties.Resources.link;
			m_Bitmaps[237] = Properties.Resources.local_florist;
			m_Bitmaps[238] = Properties.Resources.lock_;
			m_Bitmaps[239] = Properties.Resources.lock_clock;
			m_Bitmaps[240] = Properties.Resources.lock_open;
			m_Bitmaps[241] = Properties.Resources.mail;
			m_Bitmaps[242] = Properties.Resources.menu;
			m_Bitmaps[243] = Properties.Resources.mood;
			m_Bitmaps[244] = Properties.Resources.mouse;
			m_Bitmaps[245] = Properties.Resources.music_note;
			m_Bitmaps[246] = Properties.Resources.notifications;
			m_Bitmaps[247] = Properties.Resources.notifications_active;
			m_Bitmaps[248] = Properties.Resources.overview;
			m_Bitmaps[249] = Properties.Resources.pan_tool;
			m_Bitmaps[250] = Properties.Resources.pan_tool_alt;
			m_Bitmaps[251] = Properties.Resources.pedal_bike;
			m_Bitmaps[252] = Properties.Resources.person;
			m_Bitmaps[253] = Properties.Resources.photo_camera;
			m_Bitmaps[254] = Properties.Resources.pin_drop;
			m_Bitmaps[255] = Properties.Resources.podcasts;
			m_Bitmaps[256] = Properties.Resources.power;
			m_Bitmaps[257] = Properties.Resources.power_settings_new;
			m_Bitmaps[258] = Properties.Resources.priority_high;
			m_Bitmaps[259] = Properties.Resources.radio_button_checked;
			m_Bitmaps[260] = Properties.Resources.radio_button_unchecked;
			m_Bitmaps[261] = Properties.Resources.receipt_long;
			m_Bitmaps[262] = Properties.Resources.recycling;
			m_Bitmaps[263] = Properties.Resources.restaurant;
			m_Bitmaps[264] = Properties.Resources.run_circle;
			m_Bitmaps[265] = Properties.Resources.safety_check;
			m_Bitmaps[266] = Properties.Resources.schedule;
			m_Bitmaps[267] = Properties.Resources.send;
			m_Bitmaps[268] = Properties.Resources.sentiment_dissatisfied;
			m_Bitmaps[269] = Properties.Resources.sentiment_extremely_dissatisfied;
			m_Bitmaps[270] = Properties.Resources.sentiment_neutral;
			m_Bitmaps[271] = Properties.Resources.sentiment_satisfied;
			m_Bitmaps[272] = Properties.Resources.sentiment_very_dissatisfied;
			m_Bitmaps[273] = Properties.Resources.sentiment_very_satisfied;
			m_Bitmaps[274] = Properties.Resources.settings;
			m_Bitmaps[275] = Properties.Resources.shopping_bag;
			m_Bitmaps[276] = Properties.Resources.shopping_cart;
			m_Bitmaps[277] = Properties.Resources.skull;
			m_Bitmaps[278] = Properties.Resources.spatial_audio_off;
			m_Bitmaps[279] = Properties.Resources.sports_esports;
			m_Bitmaps[280] = Properties.Resources.support;
			m_Bitmaps[281] = Properties.Resources.sync;
			m_Bitmaps[282] = Properties.Resources.task;
			m_Bitmaps[283] = Properties.Resources.topic;
			m_Bitmaps[284] = Properties.Resources.video_file;
			m_Bitmaps[285] = Properties.Resources.vital_signs;
			m_Bitmaps[286] = Properties.Resources.volume_up;
			m_Bitmaps[287] = Properties.Resources.volunteer_activism;
			m_Bitmaps[288] = Properties.Resources.warning;
			m_Bitmaps[289] = Properties.Resources.watch;

			#endregion

		}
		public Bitmap? this[int idx]
		{
			get
			{
				if( (idx>=0)&&(idx<m_Bitmaps.Length))
				{
					return m_Bitmaps[idx];
				}
				else
				{
					return null;
				}
			}
		}
		public string? BitmapName(int idx)
		{
			if ((idx >= 0) && (idx < m_Bitmaps.Length))
			{
				return m_BitmapsNames[idx];
			}
			else
			{
				return null;
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
					break;
				}
			}
			return ret;
		}
		public Bitmap? Find(string name)
		{
			int idx = IndexOf(name);
			if(idx>=0)
			{
				return m_Bitmaps[idx];
			}
			else
			{
				return null;
			}
		}
		public int Count
		{
			get { return m_Bitmaps.Length; }
		}
		public Bitmap Thum(int idx,int width=48, int height=48)
		{
			Bitmap ret = new Bitmap(width, height);
			if ((idx < 0) || (idx >= m_Bitmaps.Length)) return ret;
			Graphics g = Graphics.FromImage(ret);
			Bitmap bmp = m_Bitmaps[idx];
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
