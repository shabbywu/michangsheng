using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using KBEngine;
using Newtonsoft.Json.Linq;
using UltimateSurvival;
using UnityEngine;
using YSGame;

public class jsonData : MonoBehaviour
{
	public class YSDictionary<TKey, TValue> : Dictionary<TKey, TValue>
	{
		public bool HasField(TKey key)
		{
			return ContainsKey(key);
		}
	}

	public enum SeidCount
	{
		buffCount = 500,
		skillCount = 500,
		StaticSkillCount = 500
	}

	public enum RandomFaceType
	{
		body,
		eye,
		eyebrow,
		face,
		Facefold,
		hair,
		mouth,
		mustache,
		nose,
		ornament
	}

	public enum InventoryNUM
	{
		Shop = 9,
		Max = 24,
		EXIventoryNum = 34,
		LinQiEXIventoryNum = 35,
		SkillMax = 30,
		FightEat = 32,
		PaiMai = 12,
		PaiMaiPlayer = 25,
		PaiMaiXianShi = 1,
		ShopEX = 10,
		LianDan = 29,
		LianDanDanLu = 18,
		LianDanFinish = 6,
		CaijiTeChan = 8,
		CaijiDiaoLuo = 2,
		FaceRandomTime = 500,
		NewLianDan = 37,
		NewJiaoYiNum = 15
	}

	public static jsonData instance;

	public static int QingJiaoItemIDSegment = 1000000000;

	public int saveState = -1;

	public List<int> hightLightSkillID = new List<int> { 10, 52, 53, 56, 58, 78, 124 };

	public List<string> NameColor = new List<string> { "d8d8ca", "cce281", "acfffe", "f1b7f8", "ffac5f", "ffb28b" };

	public List<string> WepenColor = new List<string> { "cce281", "f1b7f8", "ffb28b" };

	public List<string> TootipItemNameColor = new List<string> { "[d8d8ca]", "[b3d951]", "[71dbff]", "[ef6fff]", "[ff9d43]", "[ff744d]" };

	public List<string> TootipItemQualityColor = new List<string> { "[d8d8ca]", "[d7e281]", "[acfffe]", "[f1b7f8]", "[ffb143]", "[ffb28b]" };

	public bool SaveLock;

	public int ZombieBossID = 50;

	public Dictionary<int, Buff> Buff = new Dictionary<int, Buff>();

	public Dictionary<string, List<int>> faceTypeList = new Dictionary<string, List<int>>();

	public YSDictionary<string, JSONObject> skillJsonData = new YSDictionary<string, JSONObject>();

	public YSDictionary<string, JSONObject> ItemJsonData = new YSDictionary<string, JSONObject>();

	public YSDictionary<string, JSONObject> BuffJsonData = new YSDictionary<string, JSONObject>();

	public YSDictionary<string, JSONObject> firstNameJsonData = new YSDictionary<string, JSONObject>();

	public YSDictionary<string, JSONObject> LastNameJsonData = new YSDictionary<string, JSONObject>();

	public YSDictionary<string, JSONObject> LastWomenNameJsonData = new YSDictionary<string, JSONObject>();

	public JSONObject _skillJsonData;

	public JSONObject _ItemJsonData;

	public JSONObject _BuffJsonData;

	public List<YSDictionary<string, JSONObject>> _AIJsonDate = new List<YSDictionary<string, JSONObject>>();

	public Dictionary<int, JSONObject> AIJsonDate = new Dictionary<int, JSONObject>();

	public JSONObject NanZuJianBiao;

	public JSONObject NvZuJianBiao;

	public JSONObject TouXiangPianYi;

	public JSONObject StaticSkillJsonData;

	public JSONObject drawCardJsonData;

	public JSONObject MapRandomJsonData;

	public JSONObject DaDiTuYinCangJsonData;

	public JSONObject TaskJsonData;

	public JSONObject TaskInfoJsonData;

	public JSONObject ThreeSenceJsonData;

	public JSONObject AvatarJsonData;

	public JSONObject StrTextJsonData;

	public JSONObject TuJianChunWenBen;

	public JSONObject _firstNameJsonData;

	public JSONObject _LastNameJsonData;

	public JSONObject _LastWomenNameJsonData;

	public JSONObject _FaBaoFirstNameJsonData;

	public JSONObject _FaBaoLastNameJsonData;

	public JSONObject GuDingExchangeData;

	public JSONObject RandomExchangeData;

	public JSONObject DisableExchangeData;

	public JSONObject TipsExchangeData;

	public JSONObject ItemTypeExchangeData;

	public JSONObject LevelUpDataJsonData;

	public JSONObject BigMapLoadTalk;

	public JSONObject AvatarRandomJsonData;

	public JSONObject BackpackJsonData;

	public JSONObject AvatarBackpackJsonData;

	public JSONObject AvatarMoneyJsonData;

	public JSONObject DropTextJsonData;

	public JSONObject RunawayJsonData;

	public JSONObject BiguanJsonData;

	public JSONObject XinJinJsonData;

	public JSONObject XinJinGuanLianJsonData;

	public JSONObject DropInfoJsonData;

	public JSONObject SkillTextInfoJsonData;

	public JSONObject FightTypeInfoJsonData;

	public JSONObject StaticSkillTypeJsonData;

	public JSONObject FavorabilityInfoJsonData;

	public JSONObject FavorabilityAvatarInfoJsonData;

	public JSONObject QieCuoJsonData;

	public JSONObject DrawCardToLevelJsonData;

	public JSONObject StaticLVToLevelJsonData;

	public JSONObject AllMapCastTimeJsonData;

	public JSONObject SeaCastTimeJsonData;

	public JSONObject AllMapShiJianOptionJsonData;

	public JSONObject AllMapOptionJsonData;

	public JSONObject SuiJiTouXiangGeShuJsonData;

	public JSONObject SceneNameJsonData;

	public JSONObject helpJsonData;

	public JSONObject helpTextJsonData;

	public JSONObject NomelShopJsonData;

	public JSONObject HairRandomColorJsonData;

	public JSONObject MouthRandomColorJsonData;

	public JSONObject SaiHonRandomColorJsonData;

	public JSONObject WenShenRandomColorJsonData;

	public JSONObject YanZhuYanSeRandomColorJsonData;

	public JSONObject MianWenYanSeRandomColorJsonData;

	public JSONObject MeiMaoYanSeRandomColorJsonData;

	public JSONObject WuXianBiGuanJsonData;

	public JSONObject FuBenInfoJsonData;

	public JSONObject CreateAvatarJsonData;

	public JSONObject LinGenZiZhiJsonData;

	public JSONObject ChengHaoJsonData;

	public JSONObject TianFuDescJsonData;

	public JSONObject PaiMaiCanYuAvatar;

	public JSONObject PaiMaiAIJiaWei;

	public JSONObject PaiMaiCeLueSuiJiBiao;

	public JSONObject PaiMaiDuiHuaBiao;

	public JSONObject PaiMaiZhuChiBiao;

	public JSONObject PaiMaiMiaoShuBiao;

	public JSONObject PaiMaiBiao;

	public JSONObject JieDanBiao;

	public JSONObject YuanYingBiao;

	public JSONObject jiaoHuanShopGoods;

	public JSONObject LianDanDanFangBiao;

	public JSONObject LianDanItemLeiXin;

	public JSONObject LianDanSuccessItemLeiXin;

	public JSONObject DanduMiaoShu;

	public JSONObject CaiYaoShoYi;

	public JSONObject CaiYaoDiaoLuo;

	public JSONObject LiShiChuanWen;

	public JSONObject DongTaiChuanWenBaio;

	public JSONObject AllMapLuDainType;

	public JSONObject AllMapReset;

	public JSONObject StaticValueSay;

	public JSONObject ShiLiHaoGanDuName;

	public JSONObject AllMapCaiJiBiao;

	public JSONObject AllMapCaiJiMiaoShuBiao;

	public JSONObject AllMapCaiJiAddItemBiao;

	public JSONObject CreateAvatarMiaoShu;

	public JSONObject WuJiangBangDing;

	public JSONObject NTaskAllType;

	public JSONObject NTaskXiangXi;

	public JSONObject NTaskSuiJI;

	public JSONObject WuDaoJinJieJson;

	public JSONObject WuDaoJson;

	public JSONObject WuDaoAllTypeJson;

	public JSONObject WuDaoExBeiLuJson;

	public JSONObject NPCWuDaoJson;

	public JSONObject LingGuangJson;

	public JSONObject KillAvatarLingGuangJson;

	public JSONObject wupingfenlan;

	public JSONObject LianQiWuWeiBiao;

	public JSONObject CaiLiaoNengLiangBiao;

	public JSONObject LianQiHeCheng;

	public JSONObject LianQiEquipIconBiao;

	public JSONObject LianQiDuoDuanShangHaiBiao;

	public JSONObject LianQiJieSuoBiao;

	public JSONObject ChuanYingFuBiao;

	public JSONObject MenPaiFengLuBiao;

	public JSONObject ElderTaskItemType;

	public JSONObject ElderTaskDisableItem;

	public JSONObject ElderTaskItemCost;

	public JObject CaiLiaoShuXingBIAO;

	public JObject StaticNTaks;

	public JObject StaticNTaksTime;

	public JObject BadWord;

	public JObject BiGuanWuDao;

	public JObject ChengJiuJson;

	public JObject RandomMapType;

	public JObject RandomMapList;

	public JObject RandomMapEventList;

	public JObject RandomMapFirstName;

	public JObject ResetAvatarBackpackBanBen;

	public JObject EndlessSeaRandomData;

	public JObject EndlessSeaType;

	public JObject EndlessSeaData;

	public JObject EndlessSeaNPCData;

	public JObject EndlessSeaNPCGouChengData;

	public JObject EndlessSeaSafeLvData;

	public JObject EndlessSeaLinQiSafeLvData;

	public JObject EndlessSeaLuanLIuXinZhuang;

	public JObject EndlessSeaHaiYuData;

	public JObject EndlessSeaAIChuFa;

	public JObject EndlessSeaLuanLiuRandom;

	public JObject EndlessSeaLuanLiuRandomMap;

	public JObject EndlessSeaShiYe;

	public JObject LingZhouPinJie;

	public JObject NPCInterestingItem;

	public JObject AllItemLeiXin;

	public JObject SeaStaticIsland;

	public JObject LianQiEquipType;

	public JObject LianQiWuQiQuality;

	public JSONObject LianQiJieSuanBiao;

	public JSONObject LianQiShuXinLeiBie;

	public JSONObject LianQiLingWenBiao;

	[Obsolete]
	public JSONObject heroFaceJsonData;

	[Obsolete]
	public JSONObject heroFaceByIDJsonData;

	[Obsolete]
	public JSONObject heroJsonData;

	[Obsolete]
	public JSONObject PlayerGoodsSJsonData;

	[Obsolete]
	public JSONObject CheckInJsonData;

	[Obsolete]
	public JSONObject ItemGoodSeid1JsonData;

	[Obsolete]
	public JSONObject TalkingJsonData;

	[Obsolete]
	public JSONObject MessageJsonData;

	public JSONObject NPCChuShiHuaDate;

	public JSONObject NPCLeiXingDate;

	public JSONObject NPCChengHaoData;

	public JSONObject NPCChuShiShuZiDate;

	public JSONObject NPCImportantDate;

	public JSONObject NPCActionDate;

	public JSONObject NPCActionPanDingDate;

	public JSONObject NPCTagDate;

	public JSONObject NPCTuPuoDate;

	public JSONObject NpcFuBenMapBingDate;

	public JSONObject NpcThreeMapBingDate;

	public JSONObject NpcBigMapBingDate;

	public JSONObject NpcYaoShouDrop;

	public JSONObject NpcLevelShouYiDate;

	public JSONObject NpcXingGeDate;

	public JSONObject NpcYiWaiDeathDate;

	public JSONObject NpcQiYuDate;

	public JSONObject NpcBeiBaoTypeData;

	public JSONObject NpcShiJianData;

	public JSONObject NpcStatusDate;

	public JSONObject NpcPaiMaiData;

	public JSONObject NpcImprotantPanDingData;

	public JSONObject NpcHaoGanDuData;

	public JSONObject NpcCreateData;

	public JSONObject NpcJinHuoData;

	public JSONObject NpcImprotantEventData;

	public JSONObject NpcHaiShangCreateData;

	public JSONObject NpcTalkShouCiJiaoTanData;

	public JSONObject NpcTalkHouXuJiaoTanData;

	public JSONObject NpcTalkQiTaJiaoHuData;

	public JSONObject NpcBiaoBaiTiKuData;

	public JSONObject NpcBiaoBaiTiWenData;

	public JSONObject NpcTalkGuanYuTuPoData;

	public JSONObject NpcQingJiaoXiaoHaoData;

	public JSONObject NpcQingJiaoItemData;

	public JSONObject NpcWuDaoChiData;

	public JSONObject CyRandomTaskData;

	public JSONObject CyRandomTaskFailData;

	public JSONObject NewTaskMagData;

	public JSONObject FightAIData;

	public JSONObject LunDaoStateData;

	public JSONObject LunDaoSayData;

	public JSONObject LunDaoShouYiData;

	public JSONObject WuDaoZhiData;

	public JSONObject LunDaoSiXuData;

	public JSONObject LingGanMaxData;

	public JSONObject LingGanLevelData;

	public JSONObject WuDaoZhiJiaCheng;

	public JSONObject ShengWangLevelData;

	public JSONObject DiYuShengWangData;

	public JSONObject ShangJinPingFenData;

	public JSONObject ShengWangShangJinData;

	public JSONObject XuanShangMiaoShuData;

	public JSONObject ShiLiShenFenData;

	public JSONObject CyShiLiNameData;

	public JSONObject CyTeShuNpc;

	public JSONObject ScenePriceData;

	public JSONObject CyZiDuanData;

	public JSONObject CyPlayeQuestionData;

	public JSONObject CyNpcAnswerData;

	public JSONObject CyNpcDuiBaiData;

	public JSONObject CyNpcSendData;

	public JSONObject RenWuDaLeiYouXianJi;

	public JSONObject LunDaoReduceData;

	public JSONObject LingGanTimeMaxData;

	public JSONObject ShuangXiuMiShu;

	public JSONObject ShuangXiuJingYuanJiaZhi;

	public JSONObject ShuangXiuLianHuaSuDu;

	public JSONObject ShuangXiuJingJieBeiLv;

	public JSONObject DFLingYanLevel;

	public JSONObject DFZhenYanLevel;

	public JSONObject DFBuKeZhongZhi;

	public JSONObject SeaHaiYuJiZhiShuaXin;

	public JSONObject SeaJiZhiID;

	public JSONObject SeaJiZhiXingXiang;

	public JSONObject SeaHaiYuTanSuo;

	public JSONObject MapIndexData;

	public JSONObject GaoShiLeiXing;

	public JSONObject GaoShi;

	public JSONObject ZhuChengRenWu;

	public JSONObject PaiMaiPanDing;

	public JSONObject PaiMaiChuJia;

	public JSONObject PaiMaiCommandTips;

	public JSONObject PaiMaiDuiHuaAI;

	public JSONObject PaiMaiChuJiaAI;

	public JSONObject PaiMaiOldAvatar;

	public JSONObject PaiMaiNpcAddPriceSay;

	public JSONObject ChuanWenTypeData;

	public JSONObject LingHeCaiJi;

	public JSONObject LingMaiPinJie;

	public JSONObject HuaShenData;

	public JSONObject TianJieMiShuData;

	public JSONObject TianJieLeiJieType;

	public JSONObject TianJieLeiJieShangHai;

	public JSONObject TianJiDaBi;

	public JSONObject TianJiDaBiGongFangKeZhi;

	public JSONObject TianJiDaBiReward;

	public JSONObject ShengPing;

	public JSONObject JianLingXianSuo;

	public JSONObject JianLingZhenXiang;

	public JSONObject JianLingQingJiao;

	public bool IsResetAvatarFace;

	public ItemDatabase playerDatabase;

	public JSONObject[] SkillSeidJsonData = new JSONObject[500];

	public JSONObject[] BuffSeidJsonData = new JSONObject[500];

	public JSONObject[] VersionJsonData = new JSONObject[500];

	public JSONObject[] StaticSkillSeidJsonData = new JSONObject[500];

	public JSONObject[] WuDaoSeidJsonData = new JSONObject[500];

	public JSONObject[] ItemsSeidJsonData = new JSONObject[500];

	public JSONObject[] EquipSeidJsonData = new JSONObject[500];

	public JSONObject[] CrateAvatarSeidJsonData = new JSONObject[500];

	public JSONObject[] JieDanSeidJsonData = new JSONObject[500];

	public Dictionary<int, List<JSONObject>> FuBenJsonData = new Dictionary<int, List<JSONObject>>();

	public List<int> RandomList = new List<int>();

	public int randomListIndex;

	public RandomFace body;

	public RandomFace eye;

	public RandomFace eyebrow;

	public RandomFace face;

	public RandomFace Facefold;

	public RandomFace hair;

	public RandomFace hair2;

	public RandomFace mouth;

	public RandomFace mustache;

	public RandomFace nose;

	public RandomFace ornament;

	public GameObject TextError;

	public GameObject SkillHint;

	public bool reloadRandomAvatarFace;

	public static bool showGongGao = true;

	public JSONObject ItemFlagData;

	private static int rDieZiNameCount = 0;

	private void Awake()
	{
		instance = this;
	}

	public void Preload(int taskID)
	{
		try
		{
			LoadSync();
		}
		catch
		{
			PreloadManager.IsException = true;
			PreloadManager.Inst.TaskDone(taskID);
		}
		Loom.RunAsync(delegate
		{
			LoadAsync(taskID);
		});
	}

	private void LoadSync()
	{
		YSSaveGame.CheckAndDelOldSave();
		ref GameObject textError = ref TextError;
		Object obj = Resources.Load("uiPrefab/TextError");
		textError = (GameObject)(object)((obj is GameObject) ? obj : null);
		ref GameObject skillHint = ref SkillHint;
		Object obj2 = Resources.Load("uiPrefab/SkillHint");
		skillHint = (GameObject)(object)((obj2 is GameObject) ? obj2 : null);
		body = (RandomFace)(object)Resources.Load("Effect/AvatarFace/body/body1");
		eye = (RandomFace)(object)Resources.Load("Effect/AvatarFace/eye/eye");
		eyebrow = (RandomFace)(object)Resources.Load("Effect/AvatarFace/eyebrow/eyebrow");
		face = (RandomFace)(object)Resources.Load("Effect/AvatarFace/face/face");
		Facefold = (RandomFace)(object)Resources.Load("Effect/AvatarFace/Facefold/Facefold");
		hair = (RandomFace)(object)Resources.Load("Effect/AvatarFace/hair/hair");
		hair2 = (RandomFace)(object)Resources.Load("Effect/AvatarFace/hair/hair2");
		mouth = (RandomFace)(object)Resources.Load("Effect/AvatarFace/mouth/mouth");
		mustache = (RandomFace)(object)Resources.Load("Effect/AvatarFace/mustache/mustache");
		nose = (RandomFace)(object)Resources.Load("Effect/AvatarFace/nose/nose");
		ornament = (RandomFace)(object)Resources.Load("Effect/AvatarFace/ornament/ornament");
	}

	private void LoadAsync(int taskID)
	{
		try
		{
			InitLogic();
			PreloadManager.Inst.TaskDone(taskID);
		}
		catch (Exception arg)
		{
			PreloadManager.IsException = true;
			PreloadManager.ExceptionData += $"{arg}\n";
			PreloadManager.Inst.TaskDone(taskID);
		}
	}

	private void InitLogic()
	{
		init("Effect/json/d_JiaoYiHui.py.gudingyiwu.json", out GuDingExchangeData);
		init("Effect/json/d_JiaoYiHui.py.suijiyiwu", out RandomExchangeData);
		init("Effect/json/d_JiaoYiHui.py.pingbiwupin", out DisableExchangeData);
		init("Effect/json/d_JiaoYiHui.py.tishi", out TipsExchangeData);
		init("Effect/json/d_JiaoYiHui.py.wupinleixing", out ItemTypeExchangeData);
		init("Effect/json/d_AvatarAI.py.NPCwudaochi", out NpcWuDaoChiData);
		init("Effect/json/d_Map.py.SceneName", out SceneNameJsonData);
		init("Effect/json/d_avatar_inittab.py.heroFace", out heroFaceJsonData);
		init("Effect/json/d_avatar_inittab.py.datas", out heroJsonData);
		init("Effect/json/d_items.py.goodsDatas", out PlayerGoodsSJsonData);
		init("Effect/json/d_checkin.py.datas", out CheckInJsonData);
		init("Effect/json/d_items.py.good_seid1", out ItemGoodSeid1JsonData);
		init("Effect/json/d_avatar_inittab.py.drawProbability", out drawCardJsonData);
		init("Effect/json/d_staticSkill.py.datas", out StaticSkillJsonData);
		init("Effect/json/d_Map.py.RandomMap", out MapRandomJsonData);
		init("Effect/json/d_Map.py.dadituyincang", out DaDiTuYinCangJsonData);
		init("Effect/json/d_dialogs.py.datas", out TalkingJsonData);
		init("Effect/json/d_dialogs.py.message", out MessageJsonData);
		init("Effect/json/d_avatar.py.datas", out AvatarJsonData);
		init("Effect/json/d_ThreeScene.py.datas", out ThreeSenceJsonData);
		init("Effect/json/d_randomName.py.firstName", out _firstNameJsonData);
		init("Effect/json/d_randomName.py.lastName", out _LastNameJsonData);
		init("Effect/json/d_randomName.py.WomanLastName", out _LastWomenNameJsonData);
		init("Effect/json/d_randomName.py.fabaofirstname", out _FaBaoFirstNameJsonData);
		init("Effect/json/d_randomName.py.fabaolastname", out _FaBaoLastNameJsonData);
		init("Effect/json/d_ChuanYin.py.shilimingcheng", out CyShiLiNameData);
		init("Effect/json/d_ChuanYin.py.teshuNPC", out CyTeShuNpc);
		init("Effect/json/d_avatar.py.levelData", out LevelUpDataJsonData);
		init("Effect/json/d_avatar.py.Backpack", out BackpackJsonData);
		init("Effect/json/d_avatar.py.Money", out AvatarMoneyJsonData);
		init("Effect/json/d_avatar.py.dropText", out DropTextJsonData);
		init("Effect/json/d_avatar.py.wujiangbangding", out WuJiangBangDing);
		init("Effect/json/d_avatar.py.runaway", out RunawayJsonData);
		init("Effect/json/d_avatar.py.Biguan", out BiguanJsonData);
		init("Effect/json/d_avatar.py.xinjin", out XinJinJsonData);
		init("Effect/json/d_avatar.py.xinjinGuanlian", out XinJinGuanLianJsonData);
		init("Effect/json/d_task.py.Task", out TaskJsonData);
		init("Effect/json/d_task.py.TaskInfo", out TaskInfoJsonData);
		init("Effect/json/d_str.py.Text", out StrTextJsonData);
		init("Effect/json/d_avatar.py.dropInfo", out DropInfoJsonData);
		init("Effect/json/d_skills.py.TextInfo", out SkillTextInfoJsonData);
		init("Effect/json/d_avatar.py.FightType", out FightTypeInfoJsonData);
		init("Effect/json/d_staticSkill.py.StaticSkillType", out StaticSkillTypeJsonData);
		init("Effect/json/d_ThreeScene.py.favorability", out FavorabilityInfoJsonData);
		init("Effect/json/d_ThreeScene.py.AvatarRelevance", out FavorabilityAvatarInfoJsonData);
		init("Effect/json/d_avatar.py.choupaiyujineng", out DrawCardToLevelJsonData);
		init("Effect/json/d_ThreeScene.py.qiecuo", out QieCuoJsonData);
		init("Effect/json/d_avatar.py.butongjinjiecengshu", out StaticLVToLevelJsonData);
		init("Effect/json/d_avatar.py.daditushijian", out AllMapCastTimeJsonData);
		init("Effect/json/d_Map.py.ShiJian", out AllMapShiJianOptionJsonData);
		init("Effect/json/d_Map.py.XuanXiang", out AllMapOptionJsonData);
		init("Effect/json/d_str.py.xinXiangSuiji", out SuiJiTouXiangGeShuJsonData);
		init("Effect/json/d_Map.py.help", out helpJsonData);
		init("Effect/json/d_Map.py.helpText", out helpTextJsonData);
		init("Effect/json/d_ThreeScene.py.shop", out NomelShopJsonData);
		init("Effect/json/d_ChuanYin.py.ziduan", out CyZiDuanData);
		init("Effect/json/d_ChuanYin.py.wanjiafaxin", out CyPlayeQuestionData);
		init("Effect/json/d_ChuanYin.py.shuijiNPCdafu", out CyNpcAnswerData);
		init("Effect/json/d_ChuanYin.py.chuanyinfuduibai", out CyNpcDuiBaiData);
		init("Effect/json/d_ChuanYin.py.NPCxiaoxichufa", out CyNpcSendData);
		init("Effect/json/d_str.py.YanZhuYanSe", out YanZhuYanSeRandomColorJsonData);
		init("Effect/json/d_str.py.MianWenYanSe", out MianWenYanSeRandomColorJsonData);
		init("Effect/json/d_str.py.MeiMaoYanSe", out MeiMaoYanSeRandomColorJsonData);
		init("Effect/json/d_str.py.toufayanse", out HairRandomColorJsonData);
		init("Effect/json/d_str.py.ZuiChunYanSe", out MouthRandomColorJsonData);
		init("Effect/json/d_str.py.SaiHongYanSe", out SaiHonRandomColorJsonData);
		init("Effect/json/d_str.py.WenShenYanSe", out WenShenRandomColorJsonData);
		init("Effect/json/d_ThreeScene.py.wuxianbiguan", out WuXianBiGuanJsonData);
		init("Effect/json/d_Map.py.fubeninfo", out FuBenInfoJsonData);
		init("Effect/json/d_Map.py.LiShiChuanWenBaio", out LiShiChuanWen);
		init("Effect/json/d_Map.py.DongTaiChuanWenBaio", out DongTaiChuanWenBaio);
		init("Effect/json/d_createAvatar.py.tianfucitiao", out CreateAvatarJsonData);
		init("Effect/json/d_createAvatar.py.linggenzizhi", out LinGenZiZhiJsonData);
		init("Effect/json/d_avatar.py.chengHaoBiao", out ChengHaoJsonData);
		init("Effect/json/d_createAvatar.py.tianfumiaoshu", out TianFuDescJsonData);
		init("Effect/json/d_PaiMai.py.canyuAvatar", out PaiMaiCanYuAvatar);
		init("Effect/json/d_PaiMai.py.AIXinLiJiaWei", out PaiMaiAIJiaWei);
		init("Effect/json/d_PaiMai.py.AICeLueBiao", out PaiMaiCeLueSuiJiBiao);
		init("Effect/json/d_PaiMai.py.AIDuiBai", out PaiMaiDuiHuaBiao);
		init("Effect/json/d_PaiMai.py.ZhuChiRenDuiBai", out PaiMaiZhuChiBiao);
		init("Effect/json/d_PaiMai.py.PaiMaiPinMiaoShu", out PaiMaiMiaoShuBiao);
		init("Effect/json/d_PaiMai.py.PaiMaiHuiData", out PaiMaiBiao);
		init("Effect/json/d_staticSkill.py.JieDanData", out JieDanBiao);
		init("Effect/json/d_staticSkill.py.YuanYingData", out YuanYingBiao);
		init("Effect/json/d_ThreeScene.py.duihuanwuping", out jiaoHuanShopGoods);
		init("Effect/json/d_LianDan.py.DanFangBiao", out LianDanDanFangBiao);
		init("Effect/json/d_LianDan.py.yaocaizhonglei", out LianDanItemLeiXin);
		init("Effect/json/d_LianDan.py.chenggongmiaoshu", out LianDanSuccessItemLeiXin);
		init("Effect/json/d_LianDan.py.DanDuMiaoShu", out DanduMiaoShu);
		init("Effect/json/d_LianDan.py.caijishouyi", out CaiYaoShoYi);
		init("Effect/json/d_LianDan.py.caijibiao", out CaiYaoDiaoLuo);
		init("Effect/json/d_Map.py.zhuxianzhilubiao", out AllMapLuDainType);
		init("Effect/json/d_Map.py.DaDiTuGouChengBiao", out AllMapReset);
		init("Effect/json/d_str.py.quanJuBianLiangDuiHua", out StaticValueSay);
		init("Effect/json/d_str.py.shilihaogandumingchengbiao", out ShiLiHaoGanDuName);
		init("Effect/json/d_Map.py.zhilusuijiCaiJi", out AllMapCaiJiBiao);
		init("Effect/json/d_Map.py.zhixianmiaoshuBiao", out AllMapCaiJiMiaoShuBiao);
		init("Effect/json/d_Map.py.caijibaifenbi", out AllMapCaiJiAddItemBiao);
		init("Effect/json/d_createAvatar.py.tianfubeijinbiaoshu", out CreateAvatarMiaoShu);
		init("Effect/json/d_task.py.renwudalei", out NTaskAllType);
		init("Effect/json/d_task.py.shuzhisuiji", out NTaskSuiJI);
		init("Effect/json/d_task.py.xiangxiRenwu", out NTaskXiangXi);
		init("Effect/json/d_WuDao.py.wudaoType", out WuDaoAllTypeJson);
		init("Effect/json/d_WuDao.py.wudaojiacheng", out WuDaoExBeiLuJson);
		init("Effect/json/d_WuDao.py.jingyanbiao", out WuDaoJinJieJson);
		init("Effect/json/d_WuDao.py.wudao", out WuDaoJson);
		init("Effect/json/d_ChuanYin.py.chuanyingfu", out ChuanYingFuBiao);
		init("Effect/json/d_avatar.py.NPCWuDao", out NPCWuDaoJson);
		init("Effect/json/d_WuDao.py.ganwubiao", out LingGuangJson);
		init("Effect/json/d_WuDao.py.jishawudao", out KillAvatarLingGuangJson);
		init("Effect/json/d_EndlessSea.py.zougezixiaohao", out SeaCastTimeJsonData);
		init("Effect/json/d_items.py.fenleileixin", out wupingfenlan);
		init("Effect/json/d_LianQi.py.wuweibiao", out LianQiWuWeiBiao);
		init("Effect/json/d_LianQi.py.hechengbiao", out LianQiHeCheng);
		init("Effect/json/d_LianQi.py.cailiangnengliang", out CaiLiaoNengLiangBiao);
		init("Effect/json/d_LianQi.py.lingwenbiao", out LianQiLingWenBiao);
		init("Effect/json/d_LianQi.py.lianqiequipIcon", out LianQiEquipIconBiao);
		init("Effect/json/d_LianQi.py.lianqishuxing", out LianQiShuXinLeiBie);
		init("Effect/json/d_LianQi.py.lianqijiesuanbiao", out LianQiJieSuanBiao);
		init("Effect/json/d_LianQi.py.lianqiduoduanshanghai", out LianQiDuoDuanShangHaiBiao);
		init("Effect/json/d_LianQi.py.lianqijiesuo", out LianQiJieSuoBiao);
		init("Effect/json/d_str.py.nanzujian", out NanZuJianBiao);
		init("Effect/json/d_str.py.nvzujian", out NvZuJianBiao);
		init("Effect/json/d_str.py.touxiangpianyi", out TouXiangPianYi);
		init("Effect/json/d_LunDao.py.LunDaoZhuangTai", out LunDaoStateData);
		init("Effect/json/d_LunDao.py.LunDaoDuiHua", out LunDaoSayData);
		init("Effect/json/d_LunDao.py.LunDaoShouYi", out LunDaoShouYiData);
		init("Effect/json/d_LunDao.py.WuDaoDianExp", out WuDaoZhiData);
		init("Effect/json/d_LunDao.py.LunDaoLingGuangXiaoLv", out LunDaoSiXuData);
		init("Effect/json/d_LunDao.py.LingGanShangXian", out LingGanMaxData);
		init("Effect/json/d_LunDao.py.LingGanLevel", out LingGanLevelData);
		init("Effect/json/d_LunDao.py.WuDaoZhijiaCheng", out WuDaoZhiJiaCheng);
		init("Effect/json/d_AvatarAI.py.NPCchushihua", out NPCChuShiHuaDate);
		init("Effect/json/d_AvatarAI.py.NPCleixing", out NPCLeiXingDate);
		init("Effect/json/d_AvatarAI.py.NPCchenghao", out NPCChengHaoData);
		init("Effect/json/d_AvatarAI.py.chushishuzhi", out NPCChuShiShuZiDate);
		init("Effect/json/d_AvatarAI.py.gudingNPC", out NPCImportantDate);
		init("Effect/json/d_AvatarAI.py.NPCxingdong", out NPCActionDate);
		init("Effect/json/d_AvatarAI.py.qianzhipanding", out NPCActionPanDingDate);
		init("Effect/json/d_AvatarAI.py.NPCbiaoqian", out NPCTagDate);
		init("Effect/json/d_AvatarAI.py.tupojilv", out NPCTuPuoDate);
		init("Effect/json/d_AvatarAI.py.sanjichangjing", out NpcThreeMapBingDate);
		init("Effect/json/d_AvatarAI.py.fubenbangding", out NpcFuBenMapBingDate);
		init("Effect/json/d_AvatarAI.py.jingjieshouyi", out NpcLevelShouYiDate);
		init("Effect/json/d_AvatarAI.py.NPCxingge", out NpcXingGeDate);
		init("Effect/json/d_AvatarAI.py.daditu", out NpcBigMapBingDate);
		init("Effect/json/d_AvatarAI.py.yaoshoudiaoluo", out NpcYaoShouDrop);
		init("Effect/json/d_AvatarAI.py.NPCyiwaisiwang", out NpcYiWaiDeathDate);
		init("Effect/json/d_AvatarAI.py.NPCqiyu", out NpcQiYuDate);
		init("Effect/json/d_AvatarAI.py.beibaoleixing", out NpcBeiBaoTypeData);
		init("Effect/json/d_AvatarAI.py.NPCshijian", out NpcShiJianData);
		init("Effect/json/d_AvatarAI.py.NPCzhuangtai", out NpcStatusDate);
		init("Effect/json/d_AvatarAI.py.paimaihangguanlian", out NpcPaiMaiData);
		init("Effect/json/d_AvatarAI.py.NPCpanding", out NpcImprotantPanDingData);
		init("Effect/json/d_AvatarAI.py.haogandu", out NpcHaoGanDuData);
		init("Effect/json/d_AvatarAI.py.NPCshengcheng", out NpcCreateData);
		init("Effect/json/d_AvatarAI.py.beibaoshuaxin", out NpcJinHuoData);
		init("Effect/json/d_AvatarAI.py.gudingNPCshijian", out NpcImprotantEventData);
		init("Effect/json/d_AvatarAI.py.haishangNPC", out NpcHaiShangCreateData);
		init("Effect/json/d_NPCTalk.py.shoucijiaotan", out NpcTalkShouCiJiaoTanData);
		init("Effect/json/d_NPCTalk.py.houxujiaotan", out NpcTalkHouXuJiaoTanData);
		init("Effect/json/d_NPCTalk.py.qitajiaohu", out NpcTalkQiTaJiaoHuData);
		init("Effect/json/d_NPCTalk.py.guanyutupo", out NpcTalkGuanYuTuPoData);
		init("Effect/json/d_task.py.xiaodituludian", out MapIndexData);
		init("Effect/json/d_NPCTalk.py.biaobaitiku", out NpcBiaoBaiTiKuData);
		init("Effect/json/d_NPCTalk.py.biaobaitiwen", out NpcBiaoBaiTiWenData);
		init("Effect/json/d_AvatarAI.py.qingfenxiaohao", out NpcQingJiaoXiaoHaoData);
		init("Effect/json/d_ShengWang.py.shengwangdengji", out ShengWangLevelData);
		init("Effect/json/d_ShengWang.py.diyushengwang", out DiYuShengWangData);
		init("Effect/json/d_ShengWang.py.shangjinpingfen", out ShangJinPingFenData);
		init("Effect/json/d_ShengWang.py.shengwangshangjin", out ShengWangShangJinData);
		init("Effect/json/d_ShengWang.py.xuanshangmiaoshu", out XuanShangMiaoShuData);
		init("Effect/json/d_ShengWang.py.shilishenfen", out ShiLiShenFenData);
		init("Effect/json/d_PaiMai.py.laoNPCteshuchuli", out PaiMaiOldAvatar);
		init("Effect/json/d_Map.py.changjingjiage", out ScenePriceData);
		init("Effect/json/d_shuangxiu.py.mishu", out ShuangXiuMiShu);
		init("Effect/json/d_shuangxiu.py.lianhuasudu", out ShuangXiuLianHuaSuDu);
		init("Effect/json/d_shuangxiu.py.jingyuanjiazhi", out ShuangXiuJingYuanJiaZhi);
		init("Effect/json/d_shuangxiu.py.jingjiebeilv", out ShuangXiuJingJieBeiLv);
		init("Effect/json/d_shuangxiu.py.jingjiebeilv", out ShuangXiuJingJieBeiLv);
		init("Effect/json/d_Dongfu.py.Lingyanlevel", out DFLingYanLevel);
		init("Effect/json/d_Dongfu.py.Zhenyanlevel", out DFZhenYanLevel);
		init("Effect/json/d_Dongfu.py.Bukezhongzhi", out DFBuKeZhongZhi);
		init("Effect/json/d_LunDao.py.WuDaoShuaiJian", out LunDaoReduceData);
		init("Effect/json/d_Map.py.daditujiazaiduihua", out BigMapLoadTalk);
		init("Effect/json/d_LunDao.py.LingGanTimeShangXian", out LingGanTimeMaxData);
		init("Effect/json/d_EndlessSea.py.haiyujizhishuaxin", out SeaHaiYuJiZhiShuaXin);
		init("Effect/json/d_EndlessSea.py.jizhiid", out SeaJiZhiID);
		init("Effect/json/d_EndlessSea.py.jizhixingxiang", out SeaJiZhiXingXiang);
		init("Effect/json/d_EndlessSea.py.haiyutansuo", out SeaHaiYuTanSuo);
		init("Effect/json/d_GaoShi.py.gaoshitype", out GaoShiLeiXing);
		init("Effect/json/d_GaoShi.py.gaoshi", out GaoShi);
		init("Effect/json/d_TuJian.py.chunwenben", out TuJianChunWenBen);
		init("Effect/json/d_task.py.yindaozhuchengrenwu", out ZhuChengRenWu);
		init("Effect/json/d_AI.py.AIpanduanshunxu", out FightAIData);
		init("Effect/json/d_PaiMai.py.paimaipanding", out PaiMaiPanDing);
		init("Effect/json/d_PaiMai.py.NPCchujiaduihua", out PaiMaiNpcAddPriceSay);
		init("Effect/json/d_PaiMai.py.chujiabiao", out PaiMaiChuJia);
		init("Effect/json/d_PaiMai.py.paimaimiaoshu", out PaiMaiCommandTips);
		init("Effect/json/d_PaiMai.py.celueduihua", out PaiMaiDuiHuaAI);
		init("Effect/json/d_PaiMai.py.chujiacelue", out PaiMaiChuJiaAI);
		init("Effect/json/d_task.py.renwudaleiyouxianji", out RenWuDaLeiYouXianJi);
		init("Effect/json/d_staticSkill.py.HuaShenData", out HuaShenData);
		init("Effect/json/d_dujie.py.mishu", out TianJieMiShuData);
		init("Effect/json/d_dujie.py.leijieleixing", out TianJieLeiJieType);
		init("Effect/json/d_dujie.py.leijieshanghai", out TianJieLeiJieShangHai);
		init("Effect/json/d_LianDan.py.linghecaiji", out LingHeCaiJi);
		init("Effect/json/d_LianDan.py.lingmaipinjie", out LingMaiPinJie);
		init("Effect/json/d_ChuanYin.py.suijiNPCrenwuchuanyin", out CyRandomTaskData);
		init("Effect/json/d_ChuanYin.py.renwushibaichuanyin", out CyRandomTaskFailData);
		init("Effect/json/d_task.py.xinrenwuguanli", out NewTaskMagData);
		init("Effect/json/d_AvatarAI.py.tianjidabi", out TianJiDaBi);
		init("Effect/json/d_AvatarAI.py.gongfangkezhi", out TianJiDaBiGongFangKeZhi);
		init("Effect/json/d_AvatarAI.py.tianjidabijiangli", out TianJiDaBiReward);
		init("Effect/json/d_AvatarAI.py.chuanwenleixing", out ChuanWenTypeData);
		init("Effect/json/d_ShengPing.py.shengping", out ShengPing);
		init("Effect/json/d_avatar.py.FengLu", out MenPaiFengLuBiao);
		init("Effect/json/d_items.py.wupingbiaoqian", out ItemFlagData);
		init("Effect/json/d_JianLing.py.XianSuo", out JianLingXianSuo);
		init("Effect/json/d_JianLing.py.ZhenXiang", out JianLingZhenXiang);
		init("Effect/json/d_JianLing.py.QingJiao", out JianLingQingJiao);
		init("Effect/json/d_ZhangLaoRenWu.py.wupintype", out ElderTaskItemType);
		init("Effect/json/d_ZhangLaoRenWu.py.weijinwupin", out ElderTaskDisableItem);
		init("Effect/json/d_ZhangLaoRenWu.py.timecost", out ElderTaskItemCost);
		InitJObject("Effect/json/d_task.py.gudingshijianrenwu", out StaticNTaks);
		InitJObject("Effect/json/d_task.py.gudingshuaxinshijian", out StaticNTaksTime);
		InitJObject("Effect/json/d_badword.py.BadWord", out BadWord);
		InitJObject("Effect/json/d_WuDao.py.biguanwudao", out BiGuanWuDao);
		InitJObject("Effect/json/d_str.py.chengjiu", out ChengJiuJson);
		InitJObject("Effect/json/d_EndlessSea.py.fubenEvent", out RandomMapEventList);
		InitJObject("Effect/json/d_EndlessSea.py.fubenname", out RandomMapFirstName);
		InitJObject("Effect/json/d_EndlessSea.py.fubenBiao", out RandomMapList);
		InitJObject("Effect/json/d_EndlessSea.py.fubenleixin", out RandomMapType);
		InitJObject("Effect/json/d_Map.py.chongzhiwujiang", out ResetAvatarBackpackBanBen);
		InitJObject("Effect/json/d_EndlessSea.py.luanliuxinzhuang", out EndlessSeaLuanLIuXinZhuang);
		InitJObject("Effect/json/d_EndlessSea.py.weixiandengji", out EndlessSeaType);
		InitJObject("Effect/json/d_EndlessSea.py.haiyushuxing", out EndlessSeaData);
		InitJObject("Effect/json/d_EndlessSea.py.anquandengji", out EndlessSeaSafeLvData);
		InitJObject("Effect/json/LuanLiuMap", out EndlessSeaLuanLiuRandom);
		InitJObject("Effect/json/LuanLiuRMap", out EndlessSeaLuanLiuRandomMap);
		InitJObject("Effect/json/d_EndlessSea.py.lingzhoupingjiebiao", out LingZhouPinJie);
		InitJObject("Effect/json/d_EndlessSea.py.linqiluanliubiao", out EndlessSeaLinQiSafeLvData);
		InitJObject("Effect/json/d_EndlessSea.py.npcsuijishijian", out EndlessSeaNPCData);
		InitJObject("Effect/json/d_EndlessSea.py.haiyugoucheng", out EndlessSeaNPCGouChengData);
		InitJObject("Effect/json/d_EndlessSea.py.dahaiiyuyongyou", out EndlessSeaHaiYuData);
		InitJObject("Effect/json/d_EndlessSea.py.aiyuchufaleixing", out EndlessSeaAIChuFa);
		InitJObject("Effect/json/d_EndlessSea.py.shenshishiye", out EndlessSeaShiYe);
		InitJObject("Effect/json/d_items.py.wupingbiaoqian", out AllItemLeiXin);
		InitJObject("Effect/json/d_avatar.py.npcjiageshougou", out NPCInterestingItem);
		InitJObject("Effect/json/d_EndlessSea.py.gudingdaoyu", out SeaStaticIsland);
		InitJObject("Effect/json/d_LianQi.py.zhuangbeizhonglei", out LianQiEquipType);
		InitJObject("Effect/json/d_LianQi.py.pingzhibiao", out LianQiWuQiQuality);
		for (int i = 0; i < 500; i++)
		{
			RandomList.Add(GetRandom());
		}
		AvatarRandomJsonData = new JSONObject(JSONObject.Type.OBJECT);
		for (int j = 1; j < 100; j++)
		{
			JSONObject jsondata = new JSONObject();
			init("Effect/json/d_AI.py.AI" + j, out jsondata);
			if (jsondata.Count > 0)
			{
				new YSDictionary<string, JSONObject>();
				AIJsonDate[j] = jsondata;
			}
		}
		for (int k = 1; k < 200; k++)
		{
			List<JSONObject> list = new List<JSONObject>();
			JSONObject jsondata2 = new JSONObject();
			init("Effect/json/d_FuBen" + k + ".py.RandomMap", out jsondata2);
			if (jsondata2 != null)
			{
				JSONObject jsondata3 = new JSONObject();
				init("Effect/json/d_FuBen" + k + ".py.ShiJian", out jsondata3);
				JSONObject jsondata4 = new JSONObject();
				init("Effect/json/d_FuBen" + k + ".py.XuanXiang", out jsondata4);
				JSONObject jsondata5 = new JSONObject();
				init("Effect/json/d_FuBen" + k + ".py.fubentime", out jsondata5);
				list.Add(jsondata2);
				list.Add(jsondata3);
				list.Add(jsondata4);
				list.Add(jsondata5);
			}
			FuBenJsonData[k] = list;
		}
		init("Effect/json/d_skills.py.datas", out _skillJsonData);
		init("Effect/json/d_buff.py.datas", out _BuffJsonData);
		init("Effect/json/d_items.py.datas", out _ItemJsonData);
		init("Effect/json/d_items.py.qingjiaowuping", out NpcQingJiaoItemData);
		foreach (JSONObject item in NpcQingJiaoItemData.list)
		{
			_ItemJsonData.AddField(item["id"].I.ToString(), item);
		}
		setYSDictionor(_ItemJsonData, ItemJsonData);
		setYSDictionor(_BuffJsonData, BuffJsonData);
		setYSDictionor(_skillJsonData, skillJsonData);
		setYSDictionor(_firstNameJsonData, firstNameJsonData);
		setYSDictionor(_LastNameJsonData, LastNameJsonData);
		setYSDictionor(_LastWomenNameJsonData, LastWomenNameJsonData);
		initSkillSeid();
		initBuffSeid();
		initVersionSeid();
		initStaticSkillSeid();
		initItemsSeid();
		initEquipSeid();
		initCreateAvatarSeid();
		initWuDaoSeid();
		initJieDanSeid();
		InitBuff();
		YSJSONHelper.InitJSONClassData();
	}

	public JSONObject getTaskInfo(int taskID, int index)
	{
		foreach (JSONObject item in instance.TaskInfoJsonData.list)
		{
			if (item["TaskID"].I == taskID && item["TaskIndex"].I == index)
			{
				return item;
			}
		}
		return null;
	}

	public void loadAvatarBackpack(int id, int index)
	{
		AvatarBackpackJsonData = YSSaveGame.GetJsonObject("AvatarBackpackJsonData" + Tools.instance.getSaveID(id, index));
	}

	public void MonstarAddItem(int monstarID, string uuid, int ItemID, int ItemNum, JSONObject Seid, int paiMaiPlayer = 0)
	{
		foreach (JSONObject item in AvatarBackpackJsonData[string.Concat(monstarID)]["Backpack"].list)
		{
			if (item["UUID"].str == uuid)
			{
				item.SetField("Num", (int)item["Num"].n + ItemNum);
				return;
			}
		}
		JSONObject jSONObject = BackpackJsonData.list.Find((JSONObject aa) => aa["AvatrID"].I == monstarID);
		int sellPercent = 100;
		if (jSONObject != null)
		{
			sellPercent = jSONObject["SellPercent"].I;
		}
		JSONObject obj = setAvatarBackpack(uuid, ItemID, ItemNum, 1, sellPercent, 1, Seid, paiMaiPlayer);
		AvatarBackpackJsonData[string.Concat(monstarID)]["Backpack"].Add(obj);
	}

	public void MonstarCreatInterstingType(int MonstarID)
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		int i = AvatarJsonData[MonstarID.ToString()]["XinQuType"].I;
		JSONObject jSONObject = AvatarBackpackJsonData[string.Concat(MonstarID)];
		jSONObject.SetField("XinQuType", new JSONObject(JSONObject.Type.ARRAY));
		foreach (KeyValuePair<string, JToken> item in NPCInterestingItem)
		{
			if ((int)item.Value[(object)"type"] != i)
			{
				continue;
			}
			List<int> list = new List<int>();
			foreach (JToken item2 in (JArray)item.Value[(object)"xihao"])
			{
				list.Add((int)item2);
			}
			for (int j = 0; j < (int)item.Value[(object)"num"]; j++)
			{
				JSONObject jSONObject2 = new JSONObject(JSONObject.Type.OBJECT);
				int num = list[GetRandom() % list.Count];
				list.Remove(num);
				jSONObject2.SetField("type", num);
				jSONObject2.SetField("percent", (int)item.Value[(object)"percent"]);
				jSONObject["XinQuType"].Add(jSONObject2);
			}
		}
	}

	public int GetMonstarInterestingItem(int MonstarID, int itemID, JSONObject Seid = null)
	{
		if (!AvatarBackpackJsonData[string.Concat(MonstarID)].HasField("XinQuType"))
		{
			MonstarCreatInterstingType(MonstarID);
		}
		List<int> itemXiangXiLeiXin = GetItemXiangXiLeiXin(itemID);
		int num = 0;
		if (Seid != null && Seid.HasField("ItemFlag"))
		{
			List<int> list = Tools.JsonListToList(Seid["ItemFlag"]);
			foreach (JSONObject item in AvatarBackpackJsonData[string.Concat(MonstarID)]["XinQuType"].list)
			{
				if (list.Contains(item["type"].I) && item["percent"].I > num)
				{
					num = item[1].I;
				}
			}
		}
		else
		{
			foreach (JSONObject item2 in AvatarBackpackJsonData[string.Concat(MonstarID)]["XinQuType"].list)
			{
				if (itemXiangXiLeiXin.Contains(item2["type"].I) && item2["percent"].I > num)
				{
					num = item2[1].I;
				}
			}
		}
		return num;
	}

	public List<int> GetItemXiangXiLeiXin(int itemID)
	{
		List<int> list = new List<int>();
		ItemJsonData[itemID.ToString()]["ItemFlag"].list.ForEach(delegate(JSONObject aa)
		{
			list.Add(aa.I);
		});
		return list;
	}

	public List<JSONObject> GetMonsatrBackpack(int monstarID)
	{
		List<JSONObject> list = new List<JSONObject>();
		foreach (JSONObject item in instance.AvatarBackpackJsonData[string.Concat(monstarID)]["Backpack"].list)
		{
			if ((int)item["Num"].n > 0)
			{
				list.Add(item);
			}
		}
		return list;
	}

	public void MonstarRemoveItem(int monstarID, string UUID, int ItemNum, bool isPaiMai = false)
	{
		foreach (JSONObject item in AvatarBackpackJsonData[string.Concat(monstarID)]["Backpack"].list)
		{
			if (item["UUID"].str == UUID)
			{
				if ((int)item["Num"].n - ItemNum >= 0)
				{
					item.SetField("Num", (int)item["Num"].n - ItemNum);
					continue;
				}
				int itemNum = ItemNum - (int)item["Num"].n;
				item.SetField("Num", 0);
				MonstarRemoveItem(monstarID, item["ItemID"].I, itemNum);
			}
		}
	}

	public void MonstarRemoveItem(int monstarID, int ItemID, int ItemNum, bool isPaiMai = false)
	{
		foreach (JSONObject item in AvatarBackpackJsonData[string.Concat(monstarID)]["Backpack"].list)
		{
			if ((!isPaiMai || (item.HasField("paiMaiPlayer") && item["paiMaiPlayer"].I >= 1)) && ItemID == item["ItemID"].I && item["Num"].I > 0)
			{
				if (item["Num"].I - ItemNum >= 0)
				{
					item.SetField("Num", item["Num"].I - ItemNum);
					break;
				}
				int itemNum = ItemNum - (int)item["Num"].n;
				item.SetField("Num", 0);
				MonstarRemoveItem(monstarID, ItemID, itemNum);
				break;
			}
		}
	}

	public bool MonstarIsDeath(int monstarID)
	{
		bool result = false;
		if (AvatarBackpackJsonData.HasField(string.Concat(monstarID)) && AvatarBackpackJsonData[string.Concat(monstarID)].HasField("death") && AvatarBackpackJsonData[string.Concat(monstarID)]["death"].n == 1f)
		{
			result = true;
		}
		return result;
	}

	public List<int> getHaoGanDUGuanLian(int monstarID)
	{
		List<int> list = new List<int>();
		bool flag = false;
		foreach (JSONObject item in FavorabilityAvatarInfoJsonData.list)
		{
			foreach (JSONObject item2 in item["AvatarID"].list)
			{
				if (monstarID != (int)item2.n)
				{
					continue;
				}
				flag = true;
				foreach (JSONObject item3 in item["AvatarID"].list)
				{
					list.Add((int)item3.n);
				}
			}
		}
		if (!flag)
		{
			list.Add(monstarID);
		}
		return list;
	}

	public void MonstarSetHaoGanDu(int monstarID, int haogandu)
	{
		_ = AvatarRandomJsonData[monstarID.ToString()]["HaoGanDu"].n;
		foreach (int item in getHaoGanDUGuanLian(monstarID))
		{
			int num = (int)AvatarRandomJsonData[item.ToString()]["HaoGanDu"].n;
			AvatarRandomJsonData[item.ToString()].SetField("HaoGanDu", Mathf.Clamp(num + haogandu, 0, 100));
		}
	}

	public void setMonstarDeath(int monstarID, bool isNeed = true)
	{
		if (monstarID >= 20000 || NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(monstarID) || (isNeed && !NpcJieSuanManager.inst.isCanJieSuan))
		{
			return;
		}
		if ((int)AvatarJsonData[string.Concat(monstarID)]["IsRefresh"].n == 1)
		{
			refreshMonstar(monstarID);
			try
			{
				if (AvatarBackpackJsonData[string.Concat(monstarID)].HasField("Backpack"))
				{
					AvatarBackpackJsonData[string.Concat(monstarID)].RemoveField("Backpack");
				}
			}
			catch (Exception)
			{
				Debug.LogError((object)("移除怪物背包出现问题，检查怪物ID:" + monstarID));
			}
			InitAvatarBackpack(ref AvatarBackpackJsonData, monstarID);
			{
				foreach (JSONObject item in BackpackJsonData.list)
				{
					if (item["AvatrID"].I == monstarID)
					{
						AvatarAddBackpackByInfo(ref AvatarBackpackJsonData, item);
					}
				}
				return;
			}
		}
		AvatarBackpackJsonData[string.Concat(monstarID)].SetField("death", 1);
	}

	public void refreshMonstar(int monstarID)
	{
		JSONObject jSONObject = randomAvatarFace(AvatarJsonData[string.Concat(monstarID)]);
		AvatarRandomJsonData[string.Concat(monstarID)] = jSONObject.Copy();
	}

	public JSONObject setAvatarBackpack(string uuid, int itemid, int num, int canSell, int sellPercent, int canDrop, JSONObject Seid, int paiMaiPlayer = 0)
	{
		JSONObject jSONObject = new JSONObject();
		jSONObject.AddField("UUID", uuid);
		jSONObject.AddField("ItemID", itemid);
		jSONObject.AddField("Num", num);
		jSONObject.AddField("CanSell", canSell);
		jSONObject.AddField("SellPercent", sellPercent);
		jSONObject.AddField("CanDrop", canDrop);
		jSONObject.AddField("paiMaiPlayer", paiMaiPlayer);
		jSONObject.AddField("Seid", Seid);
		return jSONObject;
	}

	public void InitAvatarBackpack(ref JSONObject jsondata, int avatarID)
	{
		jsondata.SetField(string.Concat(avatarID), new JSONObject());
		jsondata[string.Concat(avatarID)].SetField("Backpack", new JSONObject(JSONObject.Type.ARRAY));
		int num = 1;
		try
		{
			num = (int)AvatarJsonData[string.Concat(avatarID)]["MoneyType"].n;
		}
		catch (Exception)
		{
			Debug.LogError((object)("初始化怪物背包出现问题，检查怪物ID:" + avatarID));
			UIPopTip.Inst.Pop("初始化怪物背包出现问题，检查怪物ID:" + avatarID);
		}
		int num2 = num;
		int num3 = num;
		if (num < 50)
		{
			num2 = (int)AvatarMoneyJsonData[string.Concat(num)]["Max"].n - (int)AvatarMoneyJsonData[string.Concat(num)]["Min"].n;
			num3 = QuikeGetRandom() % num2;
			jsondata[string.Concat(avatarID)].SetField("money", (int)AvatarMoneyJsonData[string.Concat(num)]["Min"].n + num3);
		}
		jsondata[string.Concat(avatarID)].SetField("death", 0);
	}

	public void AvatarAddBackpackByInfo(ref JSONObject jsondata, JSONObject info)
	{
		if (info["ItemID"].list.Count != 0)
		{
			int num = 0;
			{
				foreach (JSONObject item in info["ItemID"].list)
				{
					try
					{
						JSONObject obj = setAvatarBackpack(Tools.getUUID(), item.I, info["randomNum"][num].I, info["CanSell"].I, info["SellPercent"].I, info["CanDrop"].I, Tools.CreateItemSeid(item.I));
						jsondata[string.Concat(info["AvatrID"].I)]["Backpack"].Add(obj);
					}
					catch (Exception ex)
					{
						Debug.LogError((object)ex);
						Debug.LogError((object)(info["AvatrID"].I + "背包配置出错物品id" + item.I));
					}
					num++;
				}
				return;
			}
		}
		int num2 = 0;
		foreach (JSONObject item2 in info["randomNum"].list)
		{
			_ = item2;
			int randomItem = getRandomItem((int)info["Type"].n, (int)info["quality"].n);
			JSONObject obj2 = setAvatarBackpack(Tools.getUUID(), randomItem, info["randomNum"][num2].I, (int)info["CanSell"].n, (int)info["SellPercent"].n, (int)info["CanDrop"].n, Tools.CreateItemSeid(randomItem));
			jsondata[string.Concat(info["AvatrID"].I)]["Backpack"].Add(obj2);
			num2++;
		}
	}

	public void randomAvatarBackpack(int id, int index)
	{
		JSONObject jsondata = new JSONObject();
		Avatar avatar = Tools.instance.getPlayer();
		List<JToken> list = Tools.FindAllJTokens((JToken)(object)ResetAvatarBackpackBanBen, (JToken aa) => (int)aa[(object)"BanBenID"] > avatar.BanBenHao);
		foreach (JSONObject item in BackpackJsonData.list)
		{
			int avatarID = item["AvatrID"].I;
			if (list.Find((JToken aa) => (int)aa[(object)"avatar"] == avatarID) == null && AvatarBackpackJsonData != null && AvatarBackpackJsonData.HasField(string.Concat(avatarID)))
			{
				jsondata.SetField(string.Concat(avatarID), AvatarBackpackJsonData[string.Concat(avatarID)]);
				continue;
			}
			if (!jsondata.HasField(string.Concat(avatarID)))
			{
				InitAvatarBackpack(ref jsondata, avatarID);
			}
			AvatarAddBackpackByInfo(ref jsondata, item);
		}
		YSSaveGame.save("AvatarBackpackJsonData" + Tools.instance.getSaveID(id, index), jsondata);
		AvatarBackpackJsonData = jsondata;
	}

	public int getSellPercent(int monstarID, int itemID)
	{
		int result = 100;
		foreach (JSONObject item in AvatarBackpackJsonData[string.Concat(monstarID)]["Backpack"].list)
		{
			if (item["ItemID"].I == itemID && item["Num"].I > 0)
			{
				return (int)item["SellPercent"].n;
			}
		}
		return result;
	}

	public int getRandomItem(int type, int quality)
	{
		int num = 0;
		foreach (JSONObject item in _ItemJsonData.list)
		{
			if ((int)item["ShopType"].n == type && (int)item["quality"].n == quality)
			{
				num++;
			}
		}
		int num2 = 0;
		try
		{
			num2 = QuikeGetRandom() % num;
		}
		catch (Exception)
		{
			Debug.LogError((object)("背包出错类型：" + type + "品质：" + quality));
			return 0;
		}
		int num3 = 0;
		foreach (JSONObject item2 in _ItemJsonData.list)
		{
			if ((int)item2["ShopType"].n == type && (int)item2["quality"].n == quality)
			{
				if (num3 == num2)
				{
					return item2["id"].I;
				}
				num3++;
			}
		}
		return 0;
	}

	public string randomName(JSONObject info)
	{
		int num = QuikeGetRandom() % firstNameJsonData.Count + 1;
		string obj = ((info["FirstName"].Str == "") ? firstNameJsonData[string.Concat(num)]["Name"].Str : info["FirstName"].Str);
		string text;
		if (info["SexType"].I == 2)
		{
			int num2 = QuikeGetRandom() % LastWomenNameJsonData.Count + 1;
			text = LastWomenNameJsonData[string.Concat(num2)]["Name"].str;
		}
		else
		{
			int num3 = QuikeGetRandom() % LastNameJsonData.Count + 1;
			text = LastNameJsonData[string.Concat(num3)]["Name"].str;
		}
		if (text.Length == 6)
		{
			if (rDieZiNameCount % 50 == 0)
			{
				text += text;
			}
			rDieZiNameCount++;
		}
		string text2 = obj + text;
		if (Tools.instance.CheckBadWord(text2))
		{
			return text2;
		}
		return randomName(info);
	}

	public string RandomFirstName()
	{
		int num = getRandom() % firstNameJsonData.Count + 1;
		return firstNameJsonData[string.Concat(num)]["Name"].Str;
	}

	public string RandomManLastName()
	{
		int num = getRandom() % LastNameJsonData.Count + 1;
		return LastNameJsonData[string.Concat(num)]["Name"].Str;
	}

	public string RandomWomenLastName()
	{
		int num = getRandom() % LastWomenNameJsonData.Count + 1;
		return LastWomenNameJsonData[string.Concat(num)]["Name"].Str;
	}

	public JSONObject randomAvatarFace(JSONObject info, JSONObject AvatarOldJson = null)
	{
		JSONObject jSONObject = new JSONObject();
		if (info["Name"].str == "")
		{
			jSONObject.AddField("Name", randomName(info));
		}
		else
		{
			jSONObject.AddField("Name", info["Name"].str);
		}
		if ((int)info["face"].n != 0)
		{
			_ = (int)info["fightFace"].n;
		}
		jSONObject.AddField("Sex", (int)info["SexType"].n);
		foreach (JSONObject item in instance.SuiJiTouXiangGeShuJsonData.list)
		{
			List<int> suijiList = getSuijiList(item["StrID"].str, "SuiJiSex" + info["SexType"].I);
			List<int> suijiList2 = getSuijiList(item["StrID"].str, "Sex" + info["SexType"].I);
			int val = 0;
			if (suijiList.Count > 0)
			{
				int index = QuikeGetRandom() % suijiList.Count;
				val = (suijiList2.Contains(suijiList[index]) ? suijiList[index] : 0);
			}
			int num = -100;
			if (Enum.TryParse<SetAvatarFaceRandomInfo.InfoName>(item["StrID"].str, ignoreCase: true, out var result))
			{
				num = SetAvatarFaceRandomInfo.inst.getFace(info["id"].I, result);
			}
			if (num != -100)
			{
				val = num;
			}
			jSONObject.AddField(item["StrID"].str, val);
		}
		if (AvatarOldJson == null)
		{
			jSONObject.AddField("HaoGanDu", 20);
			if (Tools.instance.getPlayer() == null)
			{
				jSONObject.AddField("BirthdayTime", "0001-1-1");
			}
			else
			{
				jSONObject.AddField("BirthdayTime", Tools.instance.getPlayer().worldTimeMag.nowTime);
			}
		}
		else
		{
			jSONObject.AddField("HaoGanDu", AvatarOldJson["HaoGanDu"].I);
			jSONObject.AddField("BirthdayTime", AvatarOldJson["BirthdayTime"].str);
		}
		return jSONObject;
	}

	public void UpdateGuDingNpcFace(int id, JSONObject temp)
	{
		foreach (JSONObject item in instance.SuiJiTouXiangGeShuJsonData.list)
		{
			int val = 0;
			int num = -100;
			if (Enum.TryParse<SetAvatarFaceRandomInfo.InfoName>(item["StrID"].str, ignoreCase: true, out var result))
			{
				num = SetAvatarFaceRandomInfo.inst.getFace(id, result);
			}
			if (num != -100)
			{
				val = num;
			}
			temp.SetField(item["StrID"].str, val);
		}
	}

	public List<int> getSuijiList(string name, string sex)
	{
		if (faceTypeList.ContainsKey(name + sex))
		{
			return faceTypeList[name + sex];
		}
		List<JSONObject> list = SuiJiTouXiangGeShuJsonData[name][sex].list;
		List<int> list2 = new List<int>();
		for (int i = 0; i < list.Count / 2; i++)
		{
			for (int j = (int)list[i * 2].n; j <= (int)list[i * 2 + 1].n; j++)
			{
				list2.Add(j);
			}
		}
		faceTypeList[name + sex] = list2;
		return list2;
	}

	public void initAvatarFace(int id, int index, int startIndex = 1)
	{
		AvatarRandomJsonData = YSSaveGame.GetJsonObject("AvatarRandomJsonData" + Tools.instance.getSaveID(id, index));
		if (YSSaveGame.GetInt("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(id, index)) != 0 && !reloadRandomAvatarFace)
		{
			return;
		}
		foreach (JSONObject item in AvatarJsonData.list)
		{
			if (item["id"].I != 1 && item["id"].I >= startIndex)
			{
				if (item["id"].I >= 20000)
				{
					break;
				}
				JSONObject jSONObject = randomAvatarFace(item, AvatarRandomJsonData.HasField(string.Concat(item["id"].I)) ? AvatarRandomJsonData[item["id"].I.ToString()] : null);
				AvatarRandomJsonData.SetField(string.Concat(item["id"].I), jSONObject.Copy());
			}
		}
		if (AvatarRandomJsonData.HasField("1"))
		{
			AvatarRandomJsonData.SetField("10000", AvatarRandomJsonData["1"]);
		}
		YSSaveGame.save("AvatarRandomJsonData" + Tools.instance.getSaveID(id, index), AvatarRandomJsonData);
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(id, index), 1);
		instance.randomAvatarBackpack(id, index);
	}

	public void initDouFaFace(int id, int index)
	{
		foreach (JSONObject item in AvatarJsonData.list)
		{
			int i = item["id"].I;
			if (i != 1)
			{
				if (i >= 20000)
				{
					break;
				}
				JSONObject jSONObject = randomAvatarFace(item, AvatarRandomJsonData.HasField(string.Concat(item["id"].I)) ? AvatarRandomJsonData[item["id"].I.ToString()] : null);
				AvatarRandomJsonData.SetField(string.Concat(item["id"].I), jSONObject.Clone());
			}
		}
		if (AvatarRandomJsonData.HasField("1"))
		{
			AvatarRandomJsonData.SetField("10000", AvatarRandomJsonData["1"]);
		}
		YSSaveGame.save("AvatarRandomJsonData" + Tools.instance.getSaveID(id, index), AvatarRandomJsonData);
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(id, index), 1);
		instance.randomAvatarBackpack(id, index);
	}

	public void loadAvatarFace(int id, int index)
	{
		AvatarRandomJsonData = YSSaveGame.GetJsonObject("AvatarRandomJsonData" + Tools.instance.getSaveID(id, index));
		instance.loadAvatarBackpack(id, index);
		if (AvatarRandomJsonData.Count == AvatarJsonData.Count && !reloadRandomAvatarFace)
		{
			return;
		}
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(id, index), 0);
		initAvatarFace(id, index);
		IsResetAvatarFace = true;
		List<int> list = new List<int>();
		foreach (KeyValuePair<string, JToken> item in ResetAvatarBackpackBanBen)
		{
			if (!list.Contains((int)item.Value[(object)"BanBenID"]))
			{
				list.Add((int)item.Value[(object)"BanBenID"]);
			}
		}
		Tools.instance.getPlayer().BanBenHao = list.Max();
	}

	public void initSkillSeid()
	{
		for (int i = 1; i < 500; i++)
		{
			try
			{
				init("Effect/json/d_skills.py.skill_seid" + i, out SkillSeidJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	public void initBuffSeid()
	{
		for (int i = 1; i < 500; i++)
		{
			try
			{
				init("Effect/json/d_buff.py.buff_seid" + i, out BuffSeidJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	public void initVersionSeid()
	{
		for (int i = 4; i < 500; i++)
		{
			try
			{
				init("Effect/json/d_BanBen.py.banben" + i, out VersionJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	public void initStaticSkillSeid()
	{
		for (int i = 1; i < 500; i++)
		{
			try
			{
				init("Effect/json/d_staticSkill.py.Static_seid" + i, out StaticSkillSeidJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	public void initWuDaoSeid()
	{
		for (int i = 1; i < 500; i++)
		{
			try
			{
				init("Effect/json/d_WuDao.py.WuDao_seid" + i, out WuDaoSeidJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	public void initItemsSeid()
	{
		for (int i = 1; i < 500; i++)
		{
			try
			{
				init("Effect/json/d_items.py.good_seid" + i, out ItemsSeidJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	public void initEquipSeid()
	{
		for (int i = 1; i < 500; i++)
		{
			try
			{
				init("Effect/json/d_items.py.equip_seid" + i, out EquipSeidJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	public void initCreateAvatarSeid()
	{
		for (int i = 1; i < 500; i++)
		{
			try
			{
				init("Effect/json/d_createAvatar.py.tianfutexinSeid" + i, out CrateAvatarSeidJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	public void initJieDanSeid()
	{
		for (int i = 1; i < 500; i++)
		{
			try
			{
				init("Effect/json/d_staticSkill.py.JieDan_seid" + i, out JieDanSeidJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	public void InitBuff()
	{
		foreach (JSONObject item in _BuffJsonData.list)
		{
			Buff.Add(item["buffid"].I, new Buff(item["buffid"].I));
		}
	}

	public void intHeroFace()
	{
		heroFaceByIDJsonData = new JSONObject(JSONObject.Type.OBJECT);
		for (int i = 0; i < heroFaceJsonData.list.Count; i++)
		{
			JSONObject jSONObject = heroFaceJsonData.list[i];
			JSONObject jSONObject2 = new JSONObject(JSONObject.Type.ARRAY);
			for (int j = 0; j < heroFaceJsonData.list.Count; j++)
			{
				JSONObject jSONObject3 = heroFaceJsonData.list[j];
				if (jSONObject3["HeroId"].n == jSONObject["HeroId"].n)
				{
					jSONObject2.Add(jSONObject3);
				}
			}
			heroFaceByIDJsonData[string.Concat(jSONObject["HeroId"].n)] = jSONObject2;
		}
	}

	public void InitJObject(string path, out JObject jsondata)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		string text = ModResources.LoadText(path);
		if (string.IsNullOrWhiteSpace(text))
		{
			jsondata = new JObject();
		}
		else
		{
			jsondata = JObject.Parse(text);
		}
	}

	public void init(string path, out JSONObject jsondata)
	{
		string text = ModResources.LoadText(path);
		if (string.IsNullOrWhiteSpace(text))
		{
			jsondata = new JSONObject(JSONObject.Type.OBJECT);
		}
		else
		{
			jsondata = new JSONObject(text);
		}
	}

	public string getAvaterType(Entity entity)
	{
		return heroJsonData[string.Concat(entity.getDefinedProperty("roleTypeCell"))]["heroType"].str;
	}

	public int getRandom()
	{
		byte[] array = new byte[8];
		new RNGCryptoServiceProvider().GetBytes(array);
		return Math.Abs(BitConverter.ToInt32(array, 0));
	}

	public static int GetRandom()
	{
		byte[] array = new byte[8];
		new RNGCryptoServiceProvider().GetBytes(array);
		return Math.Abs(BitConverter.ToInt32(array, 0));
	}

	public int QuikeGetRandom()
	{
		randomListIndex++;
		if (randomListIndex >= RandomList.Count)
		{
			randomListIndex = 0;
		}
		return RandomList[randomListIndex];
	}

	public void setYSDictionor(JSONObject json, YSDictionary<string, JSONObject> dict)
	{
		foreach (string key in json.keys)
		{
			dict[key] = json[key];
		}
	}
}
