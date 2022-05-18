using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using KBEngine;
using Newtonsoft.Json.Linq;
using UltimateSurvival;
using UnityEngine;
using YSGame;

// Token: 0x020002BF RID: 703
public class jsonData : MonoBehaviour
{
	// Token: 0x0600152E RID: 5422 RVA: 0x00013577 File Offset: 0x00011777
	private void Awake()
	{
		jsonData.instance = this;
	}

	// Token: 0x0600152F RID: 5423 RVA: 0x000BE6B0 File Offset: 0x000BC8B0
	public void Preload(int taskID)
	{
		try
		{
			this.LoadSync();
		}
		catch
		{
			PreloadManager.IsException = true;
		}
		Loom.RunAsync(delegate
		{
			this.LoadAsync(taskID);
		});
	}

	// Token: 0x06001530 RID: 5424 RVA: 0x000BE704 File Offset: 0x000BC904
	private void LoadSync()
	{
		YSSaveGame.CheckAndDelOldSave();
		this.TextError = (Resources.Load("uiPrefab/TextError") as GameObject);
		this.SkillHint = (Resources.Load("uiPrefab/SkillHint") as GameObject);
		this.body = (RandomFace)Resources.Load("Effect/AvatarFace/body/body1");
		this.eye = (RandomFace)Resources.Load("Effect/AvatarFace/eye/eye");
		this.eyebrow = (RandomFace)Resources.Load("Effect/AvatarFace/eyebrow/eyebrow");
		this.face = (RandomFace)Resources.Load("Effect/AvatarFace/face/face");
		this.Facefold = (RandomFace)Resources.Load("Effect/AvatarFace/Facefold/Facefold");
		this.hair = (RandomFace)Resources.Load("Effect/AvatarFace/hair/hair");
		this.hair2 = (RandomFace)Resources.Load("Effect/AvatarFace/hair/hair2");
		this.mouth = (RandomFace)Resources.Load("Effect/AvatarFace/mouth/mouth");
		this.mustache = (RandomFace)Resources.Load("Effect/AvatarFace/mustache/mustache");
		this.nose = (RandomFace)Resources.Load("Effect/AvatarFace/nose/nose");
		this.ornament = (RandomFace)Resources.Load("Effect/AvatarFace/ornament/ornament");
	}

	// Token: 0x06001531 RID: 5425 RVA: 0x000BE828 File Offset: 0x000BCA28
	private void LoadAsync(int taskID)
	{
		try
		{
			this.InitLogic();
			PreloadManager.Inst.TaskDone(taskID);
		}
		catch (Exception arg)
		{
			PreloadManager.IsException = true;
			PreloadManager.ExceptionData += string.Format("{0}\n", arg);
		}
	}

	// Token: 0x06001532 RID: 5426 RVA: 0x000BE87C File Offset: 0x000BCA7C
	private void InitLogic()
	{
		this.init("Effect/json/d_AvatarAI.py.NPCwudaochi", out this.NpcWuDaoChiData);
		this.init("Effect/json/d_Map.py.SceneName", out this.SceneNameJsonData);
		this.init("Effect/json/d_avatar_inittab.py.heroFace", out this.heroFaceJsonData);
		this.init("Effect/json/d_avatar_inittab.py.datas", out this.heroJsonData);
		this.init("Effect/json/d_items.py.goodsDatas", out this.PlayerGoodsSJsonData);
		this.init("Effect/json/d_checkin.py.datas", out this.CheckInJsonData);
		this.init("Effect/json/d_items.py.good_seid1", out this.ItemGoodSeid1JsonData);
		this.init("Effect/json/d_avatar_inittab.py.drawProbability", out this.drawCardJsonData);
		this.init("Effect/json/d_staticSkill.py.datas", out this.StaticSkillJsonData);
		this.init("Effect/json/d_Map.py.RandomMap", out this.MapRandomJsonData);
		this.init("Effect/json/d_Map.py.dadituyincang", out this.DaDiTuYinCangJsonData);
		this.init("Effect/json/d_dialogs.py.datas", out this.TalkingJsonData);
		this.init("Effect/json/d_dialogs.py.message", out this.MessageJsonData);
		this.init("Effect/json/d_avatar.py.datas", out this.AvatarJsonData);
		this.init("Effect/json/d_ThreeScene.py.datas", out this.ThreeSenceJsonData);
		this.init("Effect/json/d_randomName.py.firstName", out this._firstNameJsonData);
		this.init("Effect/json/d_randomName.py.lastName", out this._LastNameJsonData);
		this.init("Effect/json/d_randomName.py.WomanLastName", out this._LastWomenNameJsonData);
		this.init("Effect/json/d_randomName.py.fabaofirstname", out this._FaBaoFirstNameJsonData);
		this.init("Effect/json/d_randomName.py.fabaolastname", out this._FaBaoLastNameJsonData);
		this.init("Effect/json/d_ChuanYin.py.shilimingcheng", out this.CyShiLiNameData);
		this.init("Effect/json/d_ChuanYin.py.teshuNPC", out this.CyTeShuNpc);
		this.init("Effect/json/d_avatar.py.levelData", out this.LevelUpDataJsonData);
		this.init("Effect/json/d_avatar.py.Backpack", out this.BackpackJsonData);
		this.init("Effect/json/d_avatar.py.Money", out this.AvatarMoneyJsonData);
		this.init("Effect/json/d_avatar.py.dropText", out this.DropTextJsonData);
		this.init("Effect/json/d_avatar.py.wujiangbangding", out this.WuJiangBangDing);
		this.init("Effect/json/d_avatar.py.runaway", out this.RunawayJsonData);
		this.init("Effect/json/d_avatar.py.Biguan", out this.BiguanJsonData);
		this.init("Effect/json/d_avatar.py.xinjin", out this.XinJinJsonData);
		this.init("Effect/json/d_avatar.py.xinjinGuanlian", out this.XinJinGuanLianJsonData);
		this.init("Effect/json/d_task.py.Task", out this.TaskJsonData);
		this.init("Effect/json/d_task.py.TaskInfo", out this.TaskInfoJsonData);
		this.init("Effect/json/d_str.py.Text", out this.StrTextJsonData);
		this.init("Effect/json/d_avatar.py.dropInfo", out this.DropInfoJsonData);
		this.init("Effect/json/d_skills.py.TextInfo", out this.SkillTextInfoJsonData);
		this.init("Effect/json/d_avatar.py.FightType", out this.FightTypeInfoJsonData);
		this.init("Effect/json/d_staticSkill.py.StaticSkillType", out this.StaticSkillTypeJsonData);
		this.init("Effect/json/d_ThreeScene.py.favorability", out this.FavorabilityInfoJsonData);
		this.init("Effect/json/d_ThreeScene.py.AvatarRelevance", out this.FavorabilityAvatarInfoJsonData);
		this.init("Effect/json/d_avatar.py.choupaiyujineng", out this.DrawCardToLevelJsonData);
		this.init("Effect/json/d_ThreeScene.py.qiecuo", out this.QieCuoJsonData);
		this.init("Effect/json/d_avatar.py.butongjinjiecengshu", out this.StaticLVToLevelJsonData);
		this.init("Effect/json/d_avatar.py.daditushijian", out this.AllMapCastTimeJsonData);
		this.init("Effect/json/d_Map.py.ShiJian", out this.AllMapShiJianOptionJsonData);
		this.init("Effect/json/d_Map.py.XuanXiang", out this.AllMapOptionJsonData);
		this.init("Effect/json/d_str.py.xinXiangSuiji", out this.SuiJiTouXiangGeShuJsonData);
		this.init("Effect/json/d_Map.py.help", out this.helpJsonData);
		this.init("Effect/json/d_Map.py.helpText", out this.helpTextJsonData);
		this.init("Effect/json/d_ThreeScene.py.shop", out this.NomelShopJsonData);
		this.init("Effect/json/d_ChuanYin.py.ziduan", out this.CyZiDuanData);
		this.init("Effect/json/d_ChuanYin.py.wanjiafaxin", out this.CyPlayeQuestionData);
		this.init("Effect/json/d_ChuanYin.py.shuijiNPCdafu", out this.CyNpcAnswerData);
		this.init("Effect/json/d_ChuanYin.py.chuanyinfuduibai", out this.CyNpcDuiBaiData);
		this.init("Effect/json/d_ChuanYin.py.NPCxiaoxichufa", out this.CyNpcSendData);
		this.init("Effect/json/d_str.py.YanZhuYanSe", out this.YanZhuYanSeRandomColorJsonData);
		this.init("Effect/json/d_str.py.MianWenYanSe", out this.MianWenYanSeRandomColorJsonData);
		this.init("Effect/json/d_str.py.MeiMaoYanSe", out this.MeiMaoYanSeRandomColorJsonData);
		this.init("Effect/json/d_str.py.toufayanse", out this.HairRandomColorJsonData);
		this.init("Effect/json/d_str.py.ZuiChunYanSe", out this.MouthRandomColorJsonData);
		this.init("Effect/json/d_str.py.SaiHongYanSe", out this.SaiHonRandomColorJsonData);
		this.init("Effect/json/d_str.py.WenShenYanSe", out this.WenShenRandomColorJsonData);
		this.init("Effect/json/d_ThreeScene.py.wuxianbiguan", out this.WuXianBiGuanJsonData);
		this.init("Effect/json/d_Map.py.fubeninfo", out this.FuBenInfoJsonData);
		this.init("Effect/json/d_Map.py.LiShiChuanWenBaio", out this.LiShiChuanWen);
		this.init("Effect/json/d_createAvatar.py.tianfucitiao", out this.CreateAvatarJsonData);
		this.init("Effect/json/d_createAvatar.py.linggenzizhi", out this.LinGenZiZhiJsonData);
		this.init("Effect/json/d_avatar.py.chengHaoBiao", out this.ChengHaoJsonData);
		this.init("Effect/json/d_createAvatar.py.tianfumiaoshu", out this.TianFuDescJsonData);
		this.init("Effect/json/d_PaiMai.py.canyuAvatar", out this.PaiMaiCanYuAvatar);
		this.init("Effect/json/d_PaiMai.py.AIXinLiJiaWei", out this.PaiMaiAIJiaWei);
		this.init("Effect/json/d_PaiMai.py.AICeLueBiao", out this.PaiMaiCeLueSuiJiBiao);
		this.init("Effect/json/d_PaiMai.py.AIDuiBai", out this.PaiMaiDuiHuaBiao);
		this.init("Effect/json/d_PaiMai.py.ZhuChiRenDuiBai", out this.PaiMaiZhuChiBiao);
		this.init("Effect/json/d_PaiMai.py.PaiMaiPinMiaoShu", out this.PaiMaiMiaoShuBiao);
		this.init("Effect/json/d_PaiMai.py.PaiMaiHuiData", out this.PaiMaiBiao);
		this.init("Effect/json/d_staticSkill.py.JieDanData", out this.JieDanBiao);
		this.init("Effect/json/d_staticSkill.py.YuanYingData", out this.YuanYingBiao);
		this.init("Effect/json/d_ThreeScene.py.duihuanwuping", out this.jiaoHuanShopGoods);
		this.init("Effect/json/d_LianDan.py.DanFangBiao", out this.LianDanDanFangBiao);
		this.init("Effect/json/d_LianDan.py.yaocaizhonglei", out this.LianDanItemLeiXin);
		this.init("Effect/json/d_LianDan.py.chenggongmiaoshu", out this.LianDanSuccessItemLeiXin);
		this.init("Effect/json/d_LianDan.py.DanDuMiaoShu", out this.DanduMiaoShu);
		this.init("Effect/json/d_LianDan.py.caijishouyi", out this.CaiYaoShoYi);
		this.init("Effect/json/d_LianDan.py.caijibiao", out this.CaiYaoDiaoLuo);
		this.init("Effect/json/d_Map.py.zhuxianzhilubiao", out this.AllMapLuDainType);
		this.init("Effect/json/d_Map.py.DaDiTuGouChengBiao", out this.AllMapReset);
		this.init("Effect/json/d_str.py.quanJuBianLiangDuiHua", out this.StaticValueSay);
		this.init("Effect/json/d_str.py.shilihaogandumingchengbiao", out this.ShiLiHaoGanDuName);
		this.init("Effect/json/d_Map.py.zhilusuijiCaiJi", out this.AllMapCaiJiBiao);
		this.init("Effect/json/d_Map.py.zhixianmiaoshuBiao", out this.AllMapCaiJiMiaoShuBiao);
		this.init("Effect/json/d_Map.py.caijibaifenbi", out this.AllMapCaiJiAddItemBiao);
		this.init("Effect/json/d_createAvatar.py.tianfubeijinbiaoshu", out this.CreateAvatarMiaoShu);
		this.init("Effect/json/d_task.py.renwudalei", out this.NTaskAllType);
		this.init("Effect/json/d_task.py.shuzhisuiji", out this.NTaskSuiJI);
		this.init("Effect/json/d_task.py.xiangxiRenwu", out this.NTaskXiangXi);
		this.init("Effect/json/d_WuDao.py.wudaoType", out this.WuDaoAllTypeJson);
		this.init("Effect/json/d_WuDao.py.wudaojiacheng", out this.WuDaoExBeiLuJson);
		this.init("Effect/json/d_WuDao.py.jingyanbiao", out this.WuDaoJinJieJson);
		this.init("Effect/json/d_WuDao.py.wudao", out this.WuDaoJson);
		this.init("Effect/json/d_ChuanYin.py.chuanyingfu", out this.ChuanYingFuBiao);
		this.init("Effect/json/d_avatar.py.NPCWuDao", out this.NPCWuDaoJson);
		this.init("Effect/json/d_WuDao.py.ganwubiao", out this.LingGuangJson);
		this.init("Effect/json/d_WuDao.py.jishawudao", out this.KillAvatarLingGuangJson);
		this.init("Effect/json/d_EndlessSea.py.zougezixiaohao", out this.SeaCastTimeJsonData);
		this.init("Effect/json/d_items.py.fenleileixin", out this.wupingfenlan);
		this.init("Effect/json/d_LianQi.py.wuweibiao", out this.LianQiWuWeiBiao);
		this.init("Effect/json/d_LianQi.py.hechengbiao", out this.LianQiHeCheng);
		this.init("Effect/json/d_LianQi.py.cailiangnengliang", out this.CaiLiaoNengLiangBiao);
		this.init("Effect/json/d_LianQi.py.lingwenbiao", out this.LianQiLingWenBiao);
		this.init("Effect/json/d_LianQi.py.lianqiequipIcon", out this.LianQiEquipIconBiao);
		this.init("Effect/json/d_LianQi.py.lianqishuxing", out this.LianQiShuXinLeiBie);
		this.init("Effect/json/d_LianQi.py.lianqijiesuanbiao", out this.LianQiJieSuanBiao);
		this.init("Effect/json/d_LianQi.py.lianqiduoduanshanghai", out this.LianQiDuoDuanShangHaiBiao);
		this.init("Effect/json/d_LianQi.py.lianqijiesuo", out this.LianQiJieSuoBiao);
		this.init("Effect/json/d_str.py.nanzujian", out this.NanZuJianBiao);
		this.init("Effect/json/d_str.py.nvzujian", out this.NvZuJianBiao);
		this.init("Effect/json/d_LunDao.py.LunDaoZhuangTai", out this.LunDaoStateData);
		this.init("Effect/json/d_LunDao.py.LunDaoDuiHua", out this.LunDaoSayData);
		this.init("Effect/json/d_LunDao.py.LunDaoShouYi", out this.LunDaoShouYiData);
		this.init("Effect/json/d_LunDao.py.WuDaoDianExp", out this.WuDaoZhiData);
		this.init("Effect/json/d_LunDao.py.LunDaoLingGuangXiaoLv", out this.LunDaoSiXuData);
		this.init("Effect/json/d_LunDao.py.LingGanShangXian", out this.LingGanMaxData);
		this.init("Effect/json/d_LunDao.py.LingGanLevel", out this.LingGanLevelData);
		this.init("Effect/json/d_LunDao.py.WuDaoZhijiaCheng", out this.WuDaoZhiJiaCheng);
		this.init("Effect/json/d_AvatarAI.py.NPCchushihua", out this.NPCChuShiHuaDate);
		this.init("Effect/json/d_AvatarAI.py.NPCleixing", out this.NPCLeiXingDate);
		this.init("Effect/json/d_AvatarAI.py.NPCchenghao", out this.NPCChengHaoData);
		this.init("Effect/json/d_AvatarAI.py.chushishuzhi", out this.NPCChuShiShuZiDate);
		this.init("Effect/json/d_AvatarAI.py.gudingNPC", out this.NPCImportantDate);
		this.init("Effect/json/d_AvatarAI.py.NPCxingdong", out this.NPCActionDate);
		this.init("Effect/json/d_AvatarAI.py.qianzhipanding", out this.NPCActionPanDingDate);
		this.init("Effect/json/d_AvatarAI.py.NPCbiaoqian", out this.NPCTagDate);
		this.init("Effect/json/d_AvatarAI.py.tupojilv", out this.NPCTuPuoDate);
		this.init("Effect/json/d_AvatarAI.py.sanjichangjing", out this.NpcThreeMapBingDate);
		this.init("Effect/json/d_AvatarAI.py.fubenbangding", out this.NpcFuBenMapBingDate);
		this.init("Effect/json/d_AvatarAI.py.jingjieshouyi", out this.NpcLevelShouYiDate);
		this.init("Effect/json/d_AvatarAI.py.NPCxingge", out this.NpcXingGeDate);
		this.init("Effect/json/d_AvatarAI.py.daditu", out this.NpcBigMapBingDate);
		this.init("Effect/json/d_AvatarAI.py.yaoshoudiaoluo", out this.NpcYaoShouDrop);
		this.init("Effect/json/d_AvatarAI.py.NPCyiwaisiwang", out this.NpcYiWaiDeathDate);
		this.init("Effect/json/d_AvatarAI.py.NPCqiyu", out this.NpcQiYuDate);
		this.init("Effect/json/d_AvatarAI.py.beibaoleixing", out this.NpcBeiBaoTypeData);
		this.init("Effect/json/d_AvatarAI.py.NPCshijian", out this.NpcShiJianData);
		this.init("Effect/json/d_AvatarAI.py.NPCzhuangtai", out this.NpcStatusDate);
		this.init("Effect/json/d_AvatarAI.py.paimaihangguanlian", out this.NpcPaiMaiData);
		this.init("Effect/json/d_AvatarAI.py.NPCpanding", out this.NpcImprotantPanDingData);
		this.init("Effect/json/d_AvatarAI.py.haogandu", out this.NpcHaoGanDuData);
		this.init("Effect/json/d_AvatarAI.py.NPCshengcheng", out this.NpcCreateData);
		this.init("Effect/json/d_AvatarAI.py.beibaoshuaxin", out this.NpcJinHuoData);
		this.init("Effect/json/d_AvatarAI.py.gudingNPCshijian", out this.NpcImprotantEventData);
		this.init("Effect/json/d_AvatarAI.py.haishangNPC", out this.NpcHaiShangCreateData);
		this.init("Effect/json/d_NPCTalk.py.shoucijiaotan", out this.NpcTalkShouCiJiaoTanData);
		this.init("Effect/json/d_NPCTalk.py.houxujiaotan", out this.NpcTalkHouXuJiaoTanData);
		this.init("Effect/json/d_NPCTalk.py.qitajiaohu", out this.NpcTalkQiTaJiaoHuData);
		this.init("Effect/json/d_NPCTalk.py.guanyutupo", out this.NpcTalkGuanYuTuPoData);
		this.init("Effect/json/d_task.py.xiaodituludian", out this.MapIndexData);
		this.init("Effect/json/d_NPCTalk.py.biaobaitiku", out this.NpcBiaoBaiTiKuData);
		this.init("Effect/json/d_NPCTalk.py.biaobaitiwen", out this.NpcBiaoBaiTiWenData);
		this.init("Effect/json/d_AvatarAI.py.qingfenxiaohao", out this.NpcQingJiaoXiaoHaoData);
		this.init("Effect/json/d_ShengWang.py.shengwangdengji", out this.ShengWangLevelData);
		this.init("Effect/json/d_ShengWang.py.diyushengwang", out this.DiYuShengWangData);
		this.init("Effect/json/d_ShengWang.py.shangjinpingfen", out this.ShangJinPingFenData);
		this.init("Effect/json/d_ShengWang.py.shengwangshangjin", out this.ShengWangShangJinData);
		this.init("Effect/json/d_ShengWang.py.xuanshangmiaoshu", out this.XuanShangMiaoShuData);
		this.init("Effect/json/d_ShengWang.py.shilishenfen", out this.ShiLiShenFenData);
		this.init("Effect/json/d_PaiMai.py.laoNPCteshuchuli", out this.PaiMaiOldAvatar);
		this.init("Effect/json/d_Map.py.changjingjiage", out this.ScenePriceData);
		this.init("Effect/json/d_shuangxiu.py.mishu", out this.ShuangXiuMiShu);
		this.init("Effect/json/d_shuangxiu.py.lianhuasudu", out this.ShuangXiuLianHuaSuDu);
		this.init("Effect/json/d_shuangxiu.py.jingyuanjiazhi", out this.ShuangXiuJingYuanJiaZhi);
		this.init("Effect/json/d_shuangxiu.py.jingjiebeilv", out this.ShuangXiuJingJieBeiLv);
		this.init("Effect/json/d_shuangxiu.py.jingjiebeilv", out this.ShuangXiuJingJieBeiLv);
		this.init("Effect/json/d_Dongfu.py.Lingyanlevel", out this.DFLingYanLevel);
		this.init("Effect/json/d_Dongfu.py.Zhenyanlevel", out this.DFZhenYanLevel);
		this.init("Effect/json/d_Dongfu.py.Bukezhongzhi", out this.DFBuKeZhongZhi);
		this.init("Effect/json/d_LunDao.py.WuDaoShuaiJian", out this.LunDaoReduceData);
		this.init("Effect/json/d_Map.py.daditujiazaiduihua", out this.BigMapLoadTalk);
		this.init("Effect/json/d_LunDao.py.LingGanTimeShangXian", out this.LingGanTimeMaxData);
		this.init("Effect/json/d_EndlessSea.py.haiyujizhishuaxin", out this.SeaHaiYuJiZhiShuaXin);
		this.init("Effect/json/d_EndlessSea.py.jizhiid", out this.SeaJiZhiID);
		this.init("Effect/json/d_EndlessSea.py.jizhixingxiang", out this.SeaJiZhiXingXiang);
		this.init("Effect/json/d_EndlessSea.py.haiyutansuo", out this.SeaHaiYuTanSuo);
		this.init("Effect/json/d_GaoShi.py.gaoshitype", out this.GaoShiLeiXing);
		this.init("Effect/json/d_GaoShi.py.gaoshi", out this.GaoShi);
		this.init("Effect/json/d_TuJian.py.chunwenben", out this.TuJianChunWenBen);
		this.init("Effect/json/d_task.py.yindaozhuchengrenwu", out this.ZhuChengRenWu);
		this.init("Effect/json/d_AI.py.AIpanduanshunxu", out this.FightAIData);
		this.init("Effect/json/d_PaiMai.py.paimaipanding", out this.PaiMaiPanDing);
		this.init("Effect/json/d_PaiMai.py.NPCchujiaduihua", out this.PaiMaiNpcAddPriceSay);
		this.init("Effect/json/d_PaiMai.py.chujiabiao", out this.PaiMaiChuJia);
		this.init("Effect/json/d_PaiMai.py.paimaimiaoshu", out this.PaiMaiCommandTips);
		this.init("Effect/json/d_PaiMai.py.celueduihua", out this.PaiMaiDuiHuaAI);
		this.init("Effect/json/d_PaiMai.py.chujiacelue", out this.PaiMaiChuJiaAI);
		this.init("Effect/json/d_task.py.renwudaleiyouxianji", out this.RenWuDaLeiYouXianJi);
		this.init("Effect/json/d_staticSkill.py.HuaShenData", out this.HuaShenData);
		this.init("Effect/json/d_dujie.py.mishu", out this.TianJieMiShuData);
		this.init("Effect/json/d_LianDan.py.linghecaiji", out this.LingHeCaiJi);
		this.init("Effect/json/d_LianDan.py.lingmaipinjie", out this.LingMaiPinJie);
		this.init("Effect/json/d_ChuanYin.py.suijiNPCrenwuchuanyin", out this.CyRandomTaskData);
		this.init("Effect/json/d_ChuanYin.py.renwushibaichuanyin", out this.CyRandomTaskFailData);
		this.init("Effect/json/d_task.py.xinrenwuguanli", out this.NewTaskMagData);
		this.init("Effect/json/d_AvatarAI.py.tianjidabi", out this.TianJiDaBi);
		this.init("Effect/json/d_AvatarAI.py.gongfangkezhi", out this.TianJiDaBiGongFangKeZhi);
		this.init("Effect/json/d_AvatarAI.py.tianjidabijiangli", out this.TianJiDaBiReward);
		this.init("Effect/json/d_ShengPing.py.shengping", out this.ShengPing);
		this.init("Effect/json/d_avatar.py.FengLu", out this.MenPaiFengLuBiao);
		this.init("Effect/json/d_JianLing.py.XianSuo", out this.JianLingXianSuo);
		this.init("Effect/json/d_JianLing.py.ZhenXiang", out this.JianLingZhenXiang);
		this.init("Effect/json/d_JianLing.py.QingJiao", out this.JianLingQingJiao);
		this.InitJObject("Effect/json/d_task.py.gudingshijianrenwu", out this.StaticNTaks);
		this.InitJObject("Effect/json/d_task.py.gudingshuaxinshijian", out this.StaticNTaksTime);
		this.InitJObject("Effect/json/d_badword.py.BadWord", out this.BadWord);
		this.InitJObject("Effect/json/d_WuDao.py.biguanwudao", out this.BiGuanWuDao);
		this.InitJObject("Effect/json/d_str.py.chengjiu", out this.ChengJiuJson);
		this.InitJObject("Effect/json/d_EndlessSea.py.fubenEvent", out this.RandomMapEventList);
		this.InitJObject("Effect/json/d_EndlessSea.py.fubenname", out this.RandomMapFirstName);
		this.InitJObject("Effect/json/d_EndlessSea.py.fubenBiao", out this.RandomMapList);
		this.InitJObject("Effect/json/d_EndlessSea.py.fubenleixin", out this.RandomMapType);
		this.InitJObject("Effect/json/d_Map.py.chongzhiwujiang", out this.ResetAvatarBackpackBanBen);
		this.InitJObject("Effect/json/d_EndlessSea.py.luanliuxinzhuang", out this.EndlessSeaLuanLIuXinZhuang);
		this.InitJObject("Effect/json/d_EndlessSea.py.weixiandengji", out this.EndlessSeaType);
		this.InitJObject("Effect/json/d_EndlessSea.py.haiyushuxing", out this.EndlessSeaData);
		this.InitJObject("Effect/json/d_EndlessSea.py.anquandengji", out this.EndlessSeaSafeLvData);
		this.InitJObject("Effect/json/LuanLiuMap", out this.EndlessSeaLuanLiuRandom);
		this.InitJObject("Effect/json/LuanLiuRMap", out this.EndlessSeaLuanLiuRandomMap);
		this.InitJObject("Effect/json/d_EndlessSea.py.lingzhoupingjiebiao", out this.LingZhouPinJie);
		this.InitJObject("Effect/json/d_EndlessSea.py.linqiluanliubiao", out this.EndlessSeaLinQiSafeLvData);
		this.InitJObject("Effect/json/d_EndlessSea.py.npcsuijishijian", out this.EndlessSeaNPCData);
		this.InitJObject("Effect/json/d_EndlessSea.py.haiyugoucheng", out this.EndlessSeaNPCGouChengData);
		this.InitJObject("Effect/json/d_EndlessSea.py.dahaiiyuyongyou", out this.EndlessSeaHaiYuData);
		this.InitJObject("Effect/json/d_EndlessSea.py.aiyuchufaleixing", out this.EndlessSeaAIChuFa);
		this.InitJObject("Effect/json/d_EndlessSea.py.shenshishiye", out this.EndlessSeaShiYe);
		this.InitJObject("Effect/json/d_items.py.wupingbiaoqian", out this.AllItemLeiXin);
		this.InitJObject("Effect/json/d_avatar.py.npcjiageshougou", out this.NPCInterestingItem);
		this.InitJObject("Effect/json/d_EndlessSea.py.gudingdaoyu", out this.SeaStaticIsland);
		this.InitJObject("Effect/json/d_LianQi.py.zhuangbeizhonglei", out this.LianQiEquipType);
		this.InitJObject("Effect/json/d_LianQi.py.pingzhibiao", out this.LianQiWuQiQuality);
		for (int i = 0; i < 500; i++)
		{
			this.RandomList.Add(jsonData.GetRandom());
		}
		this.AvatarRandomJsonData = new JSONObject(JSONObject.Type.OBJECT);
		for (int j = 1; j < 100; j++)
		{
			JSONObject jsonobject = new JSONObject();
			this.init("Effect/json/d_AI.py.AI" + j, out jsonobject);
			if (jsonobject.Count > 0)
			{
				new jsonData.YSDictionary<string, JSONObject>();
				this.AIJsonDate[j] = jsonobject;
			}
		}
		for (int k = 1; k < 200; k++)
		{
			List<JSONObject> list = new List<JSONObject>();
			JSONObject jsonobject2 = new JSONObject();
			this.init("Effect/json/d_FuBen" + k + ".py.RandomMap", out jsonobject2);
			if (jsonobject2 != null)
			{
				JSONObject item = new JSONObject();
				this.init("Effect/json/d_FuBen" + k + ".py.ShiJian", out item);
				JSONObject item2 = new JSONObject();
				this.init("Effect/json/d_FuBen" + k + ".py.XuanXiang", out item2);
				JSONObject item3 = new JSONObject();
				this.init("Effect/json/d_FuBen" + k + ".py.fubentime", out item3);
				list.Add(jsonobject2);
				list.Add(item);
				list.Add(item2);
				list.Add(item3);
			}
			this.FuBenJsonData[k] = list;
		}
		this.init("Effect/json/d_skills.py.datas", out this._skillJsonData);
		this.init("Effect/json/d_buff.py.datas", out this._BuffJsonData);
		this.init("Effect/json/d_items.py.datas", out this._ItemJsonData);
		this.init("Effect/json/d_items.py.qingjiaowuping", out this.NpcQingJiaoItemData);
		foreach (JSONObject jsonobject3 in this.NpcQingJiaoItemData.list)
		{
			this._ItemJsonData.AddField(jsonobject3["id"].I.ToString(), jsonobject3);
		}
		this.setYSDictionor(this._ItemJsonData, this.ItemJsonData);
		this.setYSDictionor(this._BuffJsonData, this.BuffJsonData);
		this.setYSDictionor(this._skillJsonData, this.skillJsonData);
		this.setYSDictionor(this._firstNameJsonData, this.firstNameJsonData);
		this.setYSDictionor(this._LastNameJsonData, this.LastNameJsonData);
		this.setYSDictionor(this._LastWomenNameJsonData, this.LastWomenNameJsonData);
		this.initSkillSeid();
		this.initBuffSeid();
		this.initVersionSeid();
		this.initStaticSkillSeid();
		this.initItemsSeid();
		this.initEquipSeid();
		this.initCreateAvatarSeid();
		this.initWuDaoSeid();
		this.initJieDanSeid();
		this.InitBuff();
		YSJSONHelper.InitJSONClassData();
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x000BFB10 File Offset: 0x000BDD10
	public JSONObject getTaskInfo(int taskID, int index)
	{
		foreach (JSONObject jsonobject in jsonData.instance.TaskInfoJsonData.list)
		{
			if ((int)jsonobject["TaskID"].n == taskID && (int)jsonobject["TaskIndex"].n == index)
			{
				return jsonobject;
			}
		}
		return null;
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x0001357F File Offset: 0x0001177F
	public void loadAvatarBackpack(int id, int index)
	{
		this.AvatarBackpackJsonData = YSSaveGame.GetJsonObject("AvatarBackpackJsonData" + Tools.instance.getSaveID(id, index), null);
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x000BFB94 File Offset: 0x000BDD94
	public void MonstarAddItem(int monstarID, string uuid, int ItemID, int ItemNum, JSONObject Seid, int paiMaiPlayer = 0)
	{
		foreach (JSONObject jsonobject in this.AvatarBackpackJsonData[string.Concat(monstarID)]["Backpack"].list)
		{
			if (jsonobject["UUID"].str == uuid)
			{
				jsonobject.SetField("Num", (int)jsonobject["Num"].n + ItemNum);
				return;
			}
		}
		JSONObject jsonobject2 = this.BackpackJsonData.list.Find((JSONObject aa) => aa["AvatrID"].I == monstarID);
		int sellPercent = 100;
		if (jsonobject2 != null)
		{
			sellPercent = jsonobject2["SellPercent"].I;
		}
		JSONObject obj = this.setAvatarBackpack(uuid, ItemID, ItemNum, 1, sellPercent, 1, Seid, paiMaiPlayer);
		this.AvatarBackpackJsonData[string.Concat(monstarID)]["Backpack"].Add(obj);
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x000BFCC4 File Offset: 0x000BDEC4
	public void MonstarCreatInterstingType(int MonstarID)
	{
		int i = this.AvatarJsonData[MonstarID.ToString()]["XinQuType"].I;
		JSONObject jsonobject = this.AvatarBackpackJsonData[string.Concat(MonstarID)];
		jsonobject.SetField("XinQuType", new JSONObject(JSONObject.Type.ARRAY));
		foreach (KeyValuePair<string, JToken> keyValuePair in this.NPCInterestingItem)
		{
			if ((int)keyValuePair.Value["type"] == i)
			{
				List<int> list = new List<int>();
				foreach (JToken jtoken in ((JArray)keyValuePair.Value["xihao"]))
				{
					list.Add((int)jtoken);
				}
				for (int j = 0; j < (int)keyValuePair.Value["num"]; j++)
				{
					JSONObject jsonobject2 = new JSONObject(JSONObject.Type.OBJECT);
					int num = list[jsonData.GetRandom() % list.Count];
					list.Remove(num);
					jsonobject2.SetField("type", num);
					jsonobject2.SetField("percent", (int)keyValuePair.Value["percent"]);
					jsonobject["XinQuType"].Add(jsonobject2);
				}
			}
		}
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x000BFE80 File Offset: 0x000BE080
	public int GetMonstarInterestingItem(int MonstarID, int itemID, JSONObject Seid = null)
	{
		if (!this.AvatarBackpackJsonData[string.Concat(MonstarID)].HasField("XinQuType"))
		{
			this.MonstarCreatInterstingType(MonstarID);
		}
		List<int> itemXiangXiLeiXin = this.GetItemXiangXiLeiXin(itemID);
		int num = 0;
		if (Seid != null && Seid.HasField("ItemFlag"))
		{
			List<int> list = Tools.JsonListToList(Seid["ItemFlag"]);
			using (List<JSONObject>.Enumerator enumerator = this.AvatarBackpackJsonData[string.Concat(MonstarID)]["XinQuType"].list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JSONObject jsonobject = enumerator.Current;
					if (list.Contains(jsonobject["type"].I) && jsonobject["percent"].I > num)
					{
						num = jsonobject[1].I;
					}
				}
				return num;
			}
		}
		foreach (JSONObject jsonobject2 in this.AvatarBackpackJsonData[string.Concat(MonstarID)]["XinQuType"].list)
		{
			if (itemXiangXiLeiXin.Contains(jsonobject2["type"].I) && jsonobject2["percent"].I > num)
			{
				num = jsonobject2[1].I;
			}
		}
		return num;
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x000C001C File Offset: 0x000BE21C
	public List<int> GetItemXiangXiLeiXin(int itemID)
	{
		List<int> list = new List<int>();
		this.ItemJsonData[itemID.ToString()]["ItemFlag"].list.ForEach(delegate(JSONObject aa)
		{
			list.Add(aa.I);
		});
		return list;
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x000C0074 File Offset: 0x000BE274
	public List<JSONObject> GetMonsatrBackpack(int monstarID)
	{
		List<JSONObject> list = new List<JSONObject>();
		foreach (JSONObject jsonobject in jsonData.instance.AvatarBackpackJsonData[string.Concat(monstarID)]["Backpack"].list)
		{
			if ((int)jsonobject["Num"].n > 0)
			{
				list.Add(jsonobject);
			}
		}
		return list;
	}

	// Token: 0x0600153A RID: 5434 RVA: 0x000C0108 File Offset: 0x000BE308
	public void MonstarRemoveItem(int monstarID, string UUID, int ItemNum, bool isPaiMai = false)
	{
		foreach (JSONObject jsonobject in this.AvatarBackpackJsonData[string.Concat(monstarID)]["Backpack"].list)
		{
			if (jsonobject["UUID"].str == UUID)
			{
				if ((int)jsonobject["Num"].n - ItemNum >= 0)
				{
					jsonobject.SetField("Num", (int)jsonobject["Num"].n - ItemNum);
				}
				else
				{
					int itemNum = ItemNum - (int)jsonobject["Num"].n;
					jsonobject.SetField("Num", 0);
					this.MonstarRemoveItem(monstarID, (int)jsonobject["ItemID"].n, itemNum, false);
				}
			}
		}
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x000C0204 File Offset: 0x000BE404
	public void MonstarRemoveItem(int monstarID, int ItemID, int ItemNum, bool isPaiMai = false)
	{
		foreach (JSONObject jsonobject in this.AvatarBackpackJsonData[string.Concat(monstarID)]["Backpack"].list)
		{
			if ((!isPaiMai || (jsonobject.HasField("paiMaiPlayer") && (int)jsonobject["paiMaiPlayer"].n >= 1)) && ItemID == (int)jsonobject["ItemID"].n && (int)jsonobject["Num"].n > 0)
			{
				if ((int)jsonobject["Num"].n - ItemNum >= 0)
				{
					jsonobject.SetField("Num", (int)jsonobject["Num"].n - ItemNum);
					break;
				}
				int itemNum = ItemNum - (int)jsonobject["Num"].n;
				jsonobject.SetField("Num", 0);
				this.MonstarRemoveItem(monstarID, ItemID, itemNum, false);
				break;
			}
		}
	}

	// Token: 0x0600153C RID: 5436 RVA: 0x000C032C File Offset: 0x000BE52C
	public bool MonstarIsDeath(int monstarID)
	{
		bool result = false;
		if (this.AvatarBackpackJsonData.HasField(string.Concat(monstarID)) && this.AvatarBackpackJsonData[string.Concat(monstarID)].HasField("death") && this.AvatarBackpackJsonData[string.Concat(monstarID)]["death"].n == 1f)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x000C03A4 File Offset: 0x000BE5A4
	public List<int> getHaoGanDUGuanLian(int monstarID)
	{
		List<int> list = new List<int>();
		bool flag = false;
		foreach (JSONObject jsonobject in this.FavorabilityAvatarInfoJsonData.list)
		{
			foreach (JSONObject jsonobject2 in jsonobject["AvatarID"].list)
			{
				if (monstarID == (int)jsonobject2.n)
				{
					flag = true;
					foreach (JSONObject jsonobject3 in jsonobject["AvatarID"].list)
					{
						list.Add((int)jsonobject3.n);
					}
				}
			}
		}
		if (!flag)
		{
			list.Add(monstarID);
		}
		return list;
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x000C04B8 File Offset: 0x000BE6B8
	public void MonstarSetHaoGanDu(int monstarID, int haogandu)
	{
		float n = this.AvatarRandomJsonData[monstarID.ToString()]["HaoGanDu"].n;
		foreach (int num in this.getHaoGanDUGuanLian(monstarID))
		{
			int num2 = (int)this.AvatarRandomJsonData[num.ToString()]["HaoGanDu"].n;
			this.AvatarRandomJsonData[num.ToString()].SetField("HaoGanDu", Mathf.Clamp(num2 + haogandu, 0, 100));
		}
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x000C0574 File Offset: 0x000BE774
	public void setMonstarDeath(int monstarID, bool isNeed = true)
	{
		if (monstarID >= 20000 || NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(monstarID))
		{
			return;
		}
		if (isNeed && !NpcJieSuanManager.inst.isCanJieSuan)
		{
			return;
		}
		if ((int)this.AvatarJsonData[string.Concat(monstarID)]["IsRefresh"].n == 1)
		{
			this.refreshMonstar(monstarID);
			try
			{
				if (this.AvatarBackpackJsonData[string.Concat(monstarID)].HasField("Backpack"))
				{
					this.AvatarBackpackJsonData[string.Concat(monstarID)].RemoveField("Backpack");
				}
			}
			catch (Exception)
			{
				Debug.LogError("移除怪物背包出现问题，检查怪物ID:" + monstarID);
			}
			this.InitAvatarBackpack(ref this.AvatarBackpackJsonData, monstarID);
			using (List<JSONObject>.Enumerator enumerator = this.BackpackJsonData.list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JSONObject jsonobject = enumerator.Current;
					if ((int)jsonobject["AvatrID"].n == monstarID)
					{
						this.AvatarAddBackpackByInfo(ref this.AvatarBackpackJsonData, jsonobject);
					}
				}
				return;
			}
		}
		this.AvatarBackpackJsonData[string.Concat(monstarID)].SetField("death", 1);
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x000C06DC File Offset: 0x000BE8DC
	public void refreshMonstar(int monstarID)
	{
		JSONObject value = this.randomAvatarFace(this.AvatarJsonData[string.Concat(monstarID)], null);
		this.AvatarRandomJsonData[string.Concat(monstarID)] = value;
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x000C0720 File Offset: 0x000BE920
	public JSONObject setAvatarBackpack(string uuid, int itemid, int num, int canSell, int sellPercent, int canDrop, JSONObject Seid, int paiMaiPlayer = 0)
	{
		JSONObject jsonobject = new JSONObject();
		jsonobject.AddField("UUID", uuid);
		jsonobject.AddField("ItemID", itemid);
		jsonobject.AddField("Num", num);
		jsonobject.AddField("CanSell", canSell);
		jsonobject.AddField("SellPercent", sellPercent);
		jsonobject.AddField("CanDrop", canDrop);
		jsonobject.AddField("paiMaiPlayer", paiMaiPlayer);
		jsonobject.AddField("Seid", Seid);
		return jsonobject;
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x000C0798 File Offset: 0x000BE998
	public void InitAvatarBackpack(ref JSONObject jsondata, int avatarID)
	{
		jsondata.SetField(string.Concat(avatarID), new JSONObject());
		jsondata[string.Concat(avatarID)].SetField("Backpack", new JSONObject(JSONObject.Type.ARRAY));
		int num = 1;
		try
		{
			num = (int)this.AvatarJsonData[string.Concat(avatarID)]["MoneyType"].n;
		}
		catch (Exception)
		{
			Debug.LogError("初始化怪物背包出现问题，检查怪物ID:" + avatarID);
			UIPopTip.Inst.Pop("初始化怪物背包出现问题，检查怪物ID:" + avatarID, PopTipIconType.叹号);
		}
		if (num < 50)
		{
			int num2 = (int)this.AvatarMoneyJsonData[string.Concat(num)]["Max"].n - (int)this.AvatarMoneyJsonData[string.Concat(num)]["Min"].n;
			int num3 = this.QuikeGetRandom() % num2;
			jsondata[string.Concat(avatarID)].SetField("money", (int)this.AvatarMoneyJsonData[string.Concat(num)]["Min"].n + num3);
		}
		jsondata[string.Concat(avatarID)].SetField("death", 0);
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x000C0910 File Offset: 0x000BEB10
	public void AvatarAddBackpackByInfo(ref JSONObject jsondata, JSONObject info)
	{
		if (info["ItemID"].list.Count != 0)
		{
			int num = 0;
			using (List<JSONObject>.Enumerator enumerator = info["ItemID"].list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JSONObject jsonobject = enumerator.Current;
					try
					{
						JSONObject obj = this.setAvatarBackpack(Tools.getUUID(), jsonobject.I, info["randomNum"][num].I, info["CanSell"].I, info["SellPercent"].I, info["CanDrop"].I, Tools.CreateItemSeid(jsonobject.I), 0);
						jsondata[string.Concat(info["AvatrID"].I)]["Backpack"].Add(obj);
					}
					catch (Exception ex)
					{
						Debug.LogError(ex);
						Debug.LogError((int)info["AvatrID"].n + "背包配置出错物品id" + (int)jsonobject.n);
					}
					num++;
				}
				return;
			}
		}
		int num2 = 0;
		foreach (JSONObject jsonobject2 in info["randomNum"].list)
		{
			int randomItem = this.getRandomItem((int)info["Type"].n, (int)info["quality"].n);
			JSONObject obj2 = this.setAvatarBackpack(Tools.getUUID(), randomItem, (int)info["randomNum"][num2].n, (int)info["CanSell"].n, (int)info["SellPercent"].n, (int)info["CanDrop"].n, Tools.CreateItemSeid(randomItem), 0);
			jsondata[string.Concat((int)info["AvatrID"].n)]["Backpack"].Add(obj2);
			num2++;
		}
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x000C0B80 File Offset: 0x000BED80
	public void randomAvatarBackpack(int id, int index)
	{
		JSONObject jsonobject = new JSONObject();
		Avatar avatar = Tools.instance.getPlayer();
		List<JToken> list = Tools.FindAllJTokens(this.ResetAvatarBackpackBanBen, (JToken aa) => (int)aa["BanBenID"] > avatar.BanBenHao);
		foreach (JSONObject jsonobject2 in this.BackpackJsonData.list)
		{
			int avatarID = (int)jsonobject2["AvatrID"].n;
			if (list.Find((JToken aa) => (int)aa["avatar"] == avatarID) == null && this.AvatarBackpackJsonData != null && this.AvatarBackpackJsonData.HasField(string.Concat(avatarID)))
			{
				jsonobject.SetField(string.Concat(avatarID), this.AvatarBackpackJsonData[string.Concat(avatarID)]);
			}
			else
			{
				if (!jsonobject.HasField(string.Concat(avatarID)))
				{
					this.InitAvatarBackpack(ref jsonobject, avatarID);
				}
				this.AvatarAddBackpackByInfo(ref jsonobject, jsonobject2);
			}
		}
		YSSaveGame.save("AvatarBackpackJsonData" + Tools.instance.getSaveID(id, index), jsonobject, "-1");
		this.AvatarBackpackJsonData = jsonobject;
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x000C0CF8 File Offset: 0x000BEEF8
	public int getSellPercent(int monstarID, int itemID)
	{
		int result = 100;
		foreach (JSONObject jsonobject in this.AvatarBackpackJsonData[string.Concat(monstarID)]["Backpack"].list)
		{
			if ((int)jsonobject["ItemID"].n == itemID && (int)jsonobject["Num"].n > 0)
			{
				return (int)jsonobject["SellPercent"].n;
			}
		}
		return result;
	}

	// Token: 0x06001546 RID: 5446 RVA: 0x000C0DA8 File Offset: 0x000BEFA8
	public int getRandomItem(int type, int quality)
	{
		int num = 0;
		foreach (JSONObject jsonobject in this._ItemJsonData.list)
		{
			if ((int)jsonobject["ShopType"].n == type && (int)jsonobject["quality"].n == quality)
			{
				num++;
			}
		}
		int num2 = 0;
		try
		{
			num2 = this.QuikeGetRandom() % num;
		}
		catch (Exception)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"背包出错类型：",
				type,
				"品质：",
				quality
			}));
			return 0;
		}
		int num3 = 0;
		foreach (JSONObject jsonobject2 in this._ItemJsonData.list)
		{
			if ((int)jsonobject2["ShopType"].n == type && (int)jsonobject2["quality"].n == quality)
			{
				if (num3 == num2)
				{
					return (int)jsonobject2["id"].n;
				}
				num3++;
			}
		}
		return 0;
	}

	// Token: 0x06001547 RID: 5447 RVA: 0x000C0F10 File Offset: 0x000BF110
	public string randomName(JSONObject info)
	{
		int num = this.QuikeGetRandom() % this.firstNameJsonData.Count + 1;
		string str = (info["FirstName"].Str == "") ? this.firstNameJsonData[string.Concat(num)]["Name"].Str : info["FirstName"].Str;
		string text;
		if (info["SexType"].I == 2)
		{
			int num2 = this.QuikeGetRandom() % this.LastWomenNameJsonData.Count + 1;
			text = this.LastWomenNameJsonData[string.Concat(num2)]["Name"].str;
		}
		else
		{
			int num3 = this.QuikeGetRandom() % this.LastNameJsonData.Count + 1;
			text = this.LastNameJsonData[string.Concat(num3)]["Name"].str;
		}
		if (text.Length == 6)
		{
			if (jsonData.rDieZiNameCount % 50 == 0)
			{
				text += text;
			}
			jsonData.rDieZiNameCount++;
		}
		string text2 = str + text;
		if (Tools.instance.CheckBadWord(text2))
		{
			return text2;
		}
		return this.randomName(info);
	}

	// Token: 0x06001548 RID: 5448 RVA: 0x000C1058 File Offset: 0x000BF258
	public string RandomFirstName()
	{
		int num = this.getRandom() % this.firstNameJsonData.Count + 1;
		return this.firstNameJsonData[string.Concat(num)]["Name"].Str;
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x000C10A0 File Offset: 0x000BF2A0
	public string RandomManLastName()
	{
		int num = this.getRandom() % this.LastNameJsonData.Count + 1;
		return this.LastNameJsonData[string.Concat(num)]["Name"].Str;
	}

	// Token: 0x0600154A RID: 5450 RVA: 0x000C10E8 File Offset: 0x000BF2E8
	public string RandomWomenLastName()
	{
		int num = this.getRandom() % this.LastWomenNameJsonData.Count + 1;
		return this.LastWomenNameJsonData[string.Concat(num)]["Name"].Str;
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x000C1130 File Offset: 0x000BF330
	public JSONObject randomAvatarFace(JSONObject info, JSONObject AvatarOldJson = null)
	{
		JSONObject jsonobject = new JSONObject();
		if (info["Name"].str == "")
		{
			jsonobject.AddField("Name", this.randomName(info));
		}
		else
		{
			jsonobject.AddField("Name", info["Name"].str);
		}
		if ((int)info["face"].n != 0)
		{
			int num = (int)info["fightFace"].n;
		}
		jsonobject.AddField("Sex", (int)info["SexType"].n);
		foreach (JSONObject jsonobject2 in jsonData.instance.SuiJiTouXiangGeShuJsonData.list)
		{
			List<int> suijiList = this.getSuijiList(jsonobject2["StrID"].str, "SuiJiSex" + (int)info["SexType"].n);
			List<int> suijiList2 = this.getSuijiList(jsonobject2["StrID"].str, "Sex" + (int)info["SexType"].n);
			int val = 0;
			if (suijiList.Count > 0)
			{
				int index = this.QuikeGetRandom() % suijiList.Count;
				if (!suijiList2.Contains(suijiList[index]))
				{
					val = 0;
				}
				else
				{
					val = suijiList[index];
				}
			}
			int num2 = -100;
			SetAvatarFaceRandomInfo.InfoName type;
			if (Enum.TryParse<SetAvatarFaceRandomInfo.InfoName>(jsonobject2["StrID"].str, true, out type))
			{
				num2 = SetAvatarFaceRandomInfo.inst.getFace((int)info["id"].n, type);
			}
			if (num2 != -100)
			{
				val = num2;
			}
			jsonobject.AddField(jsonobject2["StrID"].str, val);
		}
		if (AvatarOldJson == null)
		{
			jsonobject.AddField("HaoGanDu", 20);
			if (Tools.instance.getPlayer() == null)
			{
				jsonobject.AddField("BirthdayTime", "0001-1-1");
			}
			else
			{
				jsonobject.AddField("BirthdayTime", Tools.instance.getPlayer().worldTimeMag.nowTime);
			}
		}
		else
		{
			jsonobject.AddField("HaoGanDu", AvatarOldJson["HaoGanDu"].I);
			jsonobject.AddField("BirthdayTime", AvatarOldJson["BirthdayTime"].str);
		}
		return jsonobject;
	}

	// Token: 0x0600154C RID: 5452 RVA: 0x000C13B8 File Offset: 0x000BF5B8
	public List<int> getSuijiList(string name, string sex)
	{
		if (this.faceTypeList.ContainsKey(name + sex))
		{
			return this.faceTypeList[name + sex];
		}
		List<JSONObject> list = this.SuiJiTouXiangGeShuJsonData[name][sex].list;
		List<int> list2 = new List<int>();
		for (int i = 0; i < list.Count / 2; i++)
		{
			for (int j = (int)list[i * 2].n; j <= (int)list[i * 2 + 1].n; j++)
			{
				list2.Add(j);
			}
		}
		this.faceTypeList[name + sex] = list2;
		return list2;
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x000C1464 File Offset: 0x000BF664
	public void initAvatarFace(int id, int index, int startIndex = 1)
	{
		this.AvatarRandomJsonData = YSSaveGame.GetJsonObject("AvatarRandomJsonData" + Tools.instance.getSaveID(id, index), null);
		if (YSSaveGame.GetInt("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(id, index), 0) == 0 || this.reloadRandomAvatarFace)
		{
			foreach (JSONObject jsonobject in this.AvatarJsonData.list)
			{
				if ((int)jsonobject["id"].n != 1 && (int)jsonobject["id"].n >= startIndex)
				{
					if (jsonobject["id"].I >= 20000)
					{
						break;
					}
					JSONObject jsonobject2 = this.randomAvatarFace(jsonobject, this.AvatarRandomJsonData.HasField(string.Concat((int)jsonobject["id"].n)) ? this.AvatarRandomJsonData[((int)jsonobject["id"].n).ToString()] : null);
					this.AvatarRandomJsonData.SetField(string.Concat((int)jsonobject["id"].n), jsonobject2.Clone());
				}
			}
			if (this.AvatarRandomJsonData.HasField("1"))
			{
				this.AvatarRandomJsonData.SetField("10000", this.AvatarRandomJsonData["1"]);
			}
			YSSaveGame.save("AvatarRandomJsonData" + Tools.instance.getSaveID(id, index), this.AvatarRandomJsonData, "-1");
			YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(id, index), 1, "-1");
			jsonData.instance.randomAvatarBackpack(id, index);
		}
	}

	// Token: 0x0600154E RID: 5454 RVA: 0x000C1658 File Offset: 0x000BF858
	public void initDouFaFace(int id, int index)
	{
		foreach (JSONObject jsonobject in this.AvatarJsonData.list)
		{
			int i = jsonobject["id"].I;
			if (i != 1)
			{
				if (i >= 20000)
				{
					break;
				}
				JSONObject jsonobject2 = this.randomAvatarFace(jsonobject, this.AvatarRandomJsonData.HasField(string.Concat((int)jsonobject["id"].n)) ? this.AvatarRandomJsonData[((int)jsonobject["id"].n).ToString()] : null);
				this.AvatarRandomJsonData.SetField(string.Concat((int)jsonobject["id"].n), jsonobject2.Clone());
			}
		}
		if (this.AvatarRandomJsonData.HasField("1"))
		{
			this.AvatarRandomJsonData.SetField("10000", this.AvatarRandomJsonData["1"]);
		}
		YSSaveGame.save("AvatarRandomJsonData" + Tools.instance.getSaveID(id, index), this.AvatarRandomJsonData, "-1");
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(id, index), 1, "-1");
		jsonData.instance.randomAvatarBackpack(id, index);
	}

	// Token: 0x0600154F RID: 5455 RVA: 0x000135A3 File Offset: 0x000117A3
	public void saveAvatarFace(int id, int index)
	{
		YSSaveGame.save("AvatarRandomJsonData" + Tools.instance.getSaveID(id, index), this.AvatarRandomJsonData, "-1");
	}

	// Token: 0x06001550 RID: 5456 RVA: 0x000C17E0 File Offset: 0x000BF9E0
	public void loadAvatarFace(int id, int index)
	{
		this.AvatarRandomJsonData = YSSaveGame.GetJsonObject("AvatarRandomJsonData" + Tools.instance.getSaveID(id, index), null);
		jsonData.instance.loadAvatarBackpack(id, index);
		if (this.AvatarRandomJsonData.Count != this.AvatarJsonData.Count || this.reloadRandomAvatarFace)
		{
			YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(id, index), 0, "-1");
			this.initAvatarFace(id, index, 1);
			this.IsResetAvatarFace = true;
			List<int> list = new List<int>();
			foreach (KeyValuePair<string, JToken> keyValuePair in this.ResetAvatarBackpackBanBen)
			{
				if (!list.Contains((int)keyValuePair.Value["BanBenID"]))
				{
					list.Add((int)keyValuePair.Value["BanBenID"]);
				}
			}
			Tools.instance.getPlayer().BanBenHao = list.Max();
		}
	}

	// Token: 0x06001551 RID: 5457 RVA: 0x000C1900 File Offset: 0x000BFB00
	public void initSkillSeid()
	{
		for (int i = 1; i < 500; i++)
		{
			try
			{
				this.init("Effect/json/d_skills.py.skill_seid" + i, out this.SkillSeidJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x06001552 RID: 5458 RVA: 0x000C1958 File Offset: 0x000BFB58
	public void initBuffSeid()
	{
		for (int i = 1; i < 500; i++)
		{
			try
			{
				this.init("Effect/json/d_buff.py.buff_seid" + i, out this.BuffSeidJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x06001553 RID: 5459 RVA: 0x000C19B0 File Offset: 0x000BFBB0
	public void initVersionSeid()
	{
		for (int i = 4; i < 500; i++)
		{
			try
			{
				this.init("Effect/json/d_BanBen.py.banben" + i, out this.VersionJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x06001554 RID: 5460 RVA: 0x000C1A08 File Offset: 0x000BFC08
	public void initStaticSkillSeid()
	{
		for (int i = 1; i < 500; i++)
		{
			try
			{
				this.init("Effect/json/d_staticSkill.py.Static_seid" + i, out this.StaticSkillSeidJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x06001555 RID: 5461 RVA: 0x000C1A60 File Offset: 0x000BFC60
	public void initWuDaoSeid()
	{
		for (int i = 1; i < 500; i++)
		{
			try
			{
				this.init("Effect/json/d_WuDao.py.WuDao_seid" + i, out this.WuDaoSeidJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x06001556 RID: 5462 RVA: 0x000C1AB8 File Offset: 0x000BFCB8
	public void initItemsSeid()
	{
		for (int i = 1; i < 500; i++)
		{
			try
			{
				this.init("Effect/json/d_items.py.good_seid" + i, out this.ItemsSeidJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x06001557 RID: 5463 RVA: 0x000C1B10 File Offset: 0x000BFD10
	public void initEquipSeid()
	{
		for (int i = 1; i < 500; i++)
		{
			try
			{
				this.init("Effect/json/d_items.py.equip_seid" + i, out this.EquipSeidJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x06001558 RID: 5464 RVA: 0x000C1B68 File Offset: 0x000BFD68
	public void initCreateAvatarSeid()
	{
		for (int i = 1; i < 500; i++)
		{
			try
			{
				this.init("Effect/json/d_createAvatar.py.tianfutexinSeid" + i, out this.CrateAvatarSeidJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x06001559 RID: 5465 RVA: 0x000C1BC0 File Offset: 0x000BFDC0
	public void initJieDanSeid()
	{
		for (int i = 1; i < 500; i++)
		{
			try
			{
				this.init("Effect/json/d_staticSkill.py.JieDan_seid" + i, out this.JieDanSeidJsonData[i]);
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x0600155A RID: 5466 RVA: 0x000C1C18 File Offset: 0x000BFE18
	public void InitBuff()
	{
		foreach (JSONObject jsonobject in this._BuffJsonData.list)
		{
			this.Buff.Add((int)jsonobject["buffid"].n, new Buff((int)jsonobject["buffid"].n));
		}
	}

	// Token: 0x0600155B RID: 5467 RVA: 0x000C1C9C File Offset: 0x000BFE9C
	public void intHeroFace()
	{
		this.heroFaceByIDJsonData = new JSONObject(JSONObject.Type.OBJECT);
		for (int i = 0; i < this.heroFaceJsonData.list.Count; i++)
		{
			JSONObject jsonobject = this.heroFaceJsonData.list[i];
			JSONObject jsonobject2 = new JSONObject(JSONObject.Type.ARRAY);
			for (int j = 0; j < this.heroFaceJsonData.list.Count; j++)
			{
				JSONObject jsonobject3 = this.heroFaceJsonData.list[j];
				if (jsonobject3["HeroId"].n == jsonobject["HeroId"].n)
				{
					jsonobject2.Add(jsonobject3);
				}
			}
			this.heroFaceByIDJsonData[string.Concat(jsonobject["HeroId"].n)] = jsonobject2;
		}
	}

	// Token: 0x0600155C RID: 5468 RVA: 0x000C1D70 File Offset: 0x000BFF70
	public void InitJObject(string path, out JObject jsondata)
	{
		string text = ModResources.LoadText(path);
		if (string.IsNullOrWhiteSpace(text))
		{
			jsondata = new JObject();
			return;
		}
		jsondata = JObject.Parse(text);
	}

	// Token: 0x0600155D RID: 5469 RVA: 0x000C1D9C File Offset: 0x000BFF9C
	public void init(string path, out JSONObject jsondata)
	{
		string text = ModResources.LoadText(path);
		if (string.IsNullOrWhiteSpace(text))
		{
			jsondata = new JSONObject(JSONObject.Type.OBJECT);
			return;
		}
		jsondata = new JSONObject(text, -2, false, false);
	}

	// Token: 0x0600155E RID: 5470 RVA: 0x000135CB File Offset: 0x000117CB
	public string getAvaterType(Entity entity)
	{
		return this.heroJsonData[string.Concat(entity.getDefinedProperty("roleTypeCell"))]["heroType"].str;
	}

	// Token: 0x0600155F RID: 5471 RVA: 0x000AF784 File Offset: 0x000AD984
	public int getRandom()
	{
		byte[] array = new byte[8];
		new RNGCryptoServiceProvider().GetBytes(array);
		return Math.Abs(BitConverter.ToInt32(array, 0));
	}

	// Token: 0x06001560 RID: 5472 RVA: 0x000AF784 File Offset: 0x000AD984
	public static int GetRandom()
	{
		byte[] array = new byte[8];
		new RNGCryptoServiceProvider().GetBytes(array);
		return Math.Abs(BitConverter.ToInt32(array, 0));
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x000135F7 File Offset: 0x000117F7
	public int QuikeGetRandom()
	{
		this.randomListIndex++;
		if (this.randomListIndex >= this.RandomList.Count)
		{
			this.randomListIndex = 0;
		}
		return this.RandomList[this.randomListIndex];
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x000C1DD0 File Offset: 0x000BFFD0
	public void setYSDictionor(JSONObject json, jsonData.YSDictionary<string, JSONObject> dict)
	{
		foreach (string text in json.keys)
		{
			dict[text] = json[text];
		}
	}

	// Token: 0x04001034 RID: 4148
	public static jsonData instance;

	// Token: 0x04001035 RID: 4149
	public int saveState = -1;

	// Token: 0x04001036 RID: 4150
	public List<int> hightLightSkillID = new List<int>
	{
		10,
		52,
		53,
		56,
		58,
		78
	};

	// Token: 0x04001037 RID: 4151
	public List<string> NameColor = new List<string>
	{
		"d8d8ca",
		"cce281",
		"acfffe",
		"f1b7f8",
		"ffac5f",
		"ffb28b"
	};

	// Token: 0x04001038 RID: 4152
	public List<string> WepenColor = new List<string>
	{
		"cce281",
		"f1b7f8",
		"ffb28b"
	};

	// Token: 0x04001039 RID: 4153
	public List<string> TootipItemNameColor = new List<string>
	{
		"[d8d8ca]",
		"[b3d951]",
		"[71dbff]",
		"[ef6fff]",
		"[ff9d43]",
		"[ff744d]"
	};

	// Token: 0x0400103A RID: 4154
	public List<string> TootipItemQualityColor = new List<string>
	{
		"[d8d8ca]",
		"[d7e281]",
		"[acfffe]",
		"[f1b7f8]",
		"[ffb143]",
		"[ffb28b]"
	};

	// Token: 0x0400103B RID: 4155
	public bool SaveLock;

	// Token: 0x0400103C RID: 4156
	public int ZombieBossID = 50;

	// Token: 0x0400103D RID: 4157
	public Dictionary<int, Buff> Buff = new Dictionary<int, Buff>();

	// Token: 0x0400103E RID: 4158
	public Dictionary<string, List<int>> faceTypeList = new Dictionary<string, List<int>>();

	// Token: 0x0400103F RID: 4159
	public jsonData.YSDictionary<string, JSONObject> skillJsonData = new jsonData.YSDictionary<string, JSONObject>();

	// Token: 0x04001040 RID: 4160
	public jsonData.YSDictionary<string, JSONObject> ItemJsonData = new jsonData.YSDictionary<string, JSONObject>();

	// Token: 0x04001041 RID: 4161
	public jsonData.YSDictionary<string, JSONObject> BuffJsonData = new jsonData.YSDictionary<string, JSONObject>();

	// Token: 0x04001042 RID: 4162
	public jsonData.YSDictionary<string, JSONObject> firstNameJsonData = new jsonData.YSDictionary<string, JSONObject>();

	// Token: 0x04001043 RID: 4163
	public jsonData.YSDictionary<string, JSONObject> LastNameJsonData = new jsonData.YSDictionary<string, JSONObject>();

	// Token: 0x04001044 RID: 4164
	public jsonData.YSDictionary<string, JSONObject> LastWomenNameJsonData = new jsonData.YSDictionary<string, JSONObject>();

	// Token: 0x04001045 RID: 4165
	public JSONObject _skillJsonData;

	// Token: 0x04001046 RID: 4166
	public JSONObject _ItemJsonData;

	// Token: 0x04001047 RID: 4167
	public JSONObject _BuffJsonData;

	// Token: 0x04001048 RID: 4168
	public List<jsonData.YSDictionary<string, JSONObject>> _AIJsonDate = new List<jsonData.YSDictionary<string, JSONObject>>();

	// Token: 0x04001049 RID: 4169
	public Dictionary<int, JSONObject> AIJsonDate = new Dictionary<int, JSONObject>();

	// Token: 0x0400104A RID: 4170
	public JSONObject NanZuJianBiao;

	// Token: 0x0400104B RID: 4171
	public JSONObject NvZuJianBiao;

	// Token: 0x0400104C RID: 4172
	public JSONObject StaticSkillJsonData;

	// Token: 0x0400104D RID: 4173
	public JSONObject drawCardJsonData;

	// Token: 0x0400104E RID: 4174
	public JSONObject MapRandomJsonData;

	// Token: 0x0400104F RID: 4175
	public JSONObject DaDiTuYinCangJsonData;

	// Token: 0x04001050 RID: 4176
	public JSONObject TaskJsonData;

	// Token: 0x04001051 RID: 4177
	public JSONObject TaskInfoJsonData;

	// Token: 0x04001052 RID: 4178
	public JSONObject ThreeSenceJsonData;

	// Token: 0x04001053 RID: 4179
	public JSONObject AvatarJsonData;

	// Token: 0x04001054 RID: 4180
	public JSONObject StrTextJsonData;

	// Token: 0x04001055 RID: 4181
	public JSONObject TuJianChunWenBen;

	// Token: 0x04001056 RID: 4182
	public JSONObject _firstNameJsonData;

	// Token: 0x04001057 RID: 4183
	public JSONObject _LastNameJsonData;

	// Token: 0x04001058 RID: 4184
	public JSONObject _LastWomenNameJsonData;

	// Token: 0x04001059 RID: 4185
	public JSONObject _FaBaoFirstNameJsonData;

	// Token: 0x0400105A RID: 4186
	public JSONObject _FaBaoLastNameJsonData;

	// Token: 0x0400105B RID: 4187
	public JSONObject LevelUpDataJsonData;

	// Token: 0x0400105C RID: 4188
	public JSONObject BigMapLoadTalk;

	// Token: 0x0400105D RID: 4189
	public JSONObject AvatarRandomJsonData;

	// Token: 0x0400105E RID: 4190
	public JSONObject BackpackJsonData;

	// Token: 0x0400105F RID: 4191
	public JSONObject AvatarBackpackJsonData;

	// Token: 0x04001060 RID: 4192
	public JSONObject AvatarMoneyJsonData;

	// Token: 0x04001061 RID: 4193
	public JSONObject DropTextJsonData;

	// Token: 0x04001062 RID: 4194
	public JSONObject RunawayJsonData;

	// Token: 0x04001063 RID: 4195
	public JSONObject BiguanJsonData;

	// Token: 0x04001064 RID: 4196
	public JSONObject XinJinJsonData;

	// Token: 0x04001065 RID: 4197
	public JSONObject XinJinGuanLianJsonData;

	// Token: 0x04001066 RID: 4198
	public JSONObject DropInfoJsonData;

	// Token: 0x04001067 RID: 4199
	public JSONObject SkillTextInfoJsonData;

	// Token: 0x04001068 RID: 4200
	public JSONObject FightTypeInfoJsonData;

	// Token: 0x04001069 RID: 4201
	public JSONObject StaticSkillTypeJsonData;

	// Token: 0x0400106A RID: 4202
	public JSONObject FavorabilityInfoJsonData;

	// Token: 0x0400106B RID: 4203
	public JSONObject FavorabilityAvatarInfoJsonData;

	// Token: 0x0400106C RID: 4204
	public JSONObject QieCuoJsonData;

	// Token: 0x0400106D RID: 4205
	public JSONObject DrawCardToLevelJsonData;

	// Token: 0x0400106E RID: 4206
	public JSONObject StaticLVToLevelJsonData;

	// Token: 0x0400106F RID: 4207
	public JSONObject AllMapCastTimeJsonData;

	// Token: 0x04001070 RID: 4208
	public JSONObject SeaCastTimeJsonData;

	// Token: 0x04001071 RID: 4209
	public JSONObject AllMapShiJianOptionJsonData;

	// Token: 0x04001072 RID: 4210
	public JSONObject AllMapOptionJsonData;

	// Token: 0x04001073 RID: 4211
	public JSONObject SuiJiTouXiangGeShuJsonData;

	// Token: 0x04001074 RID: 4212
	public JSONObject SceneNameJsonData;

	// Token: 0x04001075 RID: 4213
	public JSONObject helpJsonData;

	// Token: 0x04001076 RID: 4214
	public JSONObject helpTextJsonData;

	// Token: 0x04001077 RID: 4215
	public JSONObject NomelShopJsonData;

	// Token: 0x04001078 RID: 4216
	public JSONObject HairRandomColorJsonData;

	// Token: 0x04001079 RID: 4217
	public JSONObject MouthRandomColorJsonData;

	// Token: 0x0400107A RID: 4218
	public JSONObject SaiHonRandomColorJsonData;

	// Token: 0x0400107B RID: 4219
	public JSONObject WenShenRandomColorJsonData;

	// Token: 0x0400107C RID: 4220
	public JSONObject YanZhuYanSeRandomColorJsonData;

	// Token: 0x0400107D RID: 4221
	public JSONObject MianWenYanSeRandomColorJsonData;

	// Token: 0x0400107E RID: 4222
	public JSONObject MeiMaoYanSeRandomColorJsonData;

	// Token: 0x0400107F RID: 4223
	public JSONObject WuXianBiGuanJsonData;

	// Token: 0x04001080 RID: 4224
	public JSONObject FuBenInfoJsonData;

	// Token: 0x04001081 RID: 4225
	public JSONObject CreateAvatarJsonData;

	// Token: 0x04001082 RID: 4226
	public JSONObject LinGenZiZhiJsonData;

	// Token: 0x04001083 RID: 4227
	public JSONObject ChengHaoJsonData;

	// Token: 0x04001084 RID: 4228
	public JSONObject TianFuDescJsonData;

	// Token: 0x04001085 RID: 4229
	public JSONObject PaiMaiCanYuAvatar;

	// Token: 0x04001086 RID: 4230
	public JSONObject PaiMaiAIJiaWei;

	// Token: 0x04001087 RID: 4231
	public JSONObject PaiMaiCeLueSuiJiBiao;

	// Token: 0x04001088 RID: 4232
	public JSONObject PaiMaiDuiHuaBiao;

	// Token: 0x04001089 RID: 4233
	public JSONObject PaiMaiZhuChiBiao;

	// Token: 0x0400108A RID: 4234
	public JSONObject PaiMaiMiaoShuBiao;

	// Token: 0x0400108B RID: 4235
	public JSONObject PaiMaiBiao;

	// Token: 0x0400108C RID: 4236
	public JSONObject JieDanBiao;

	// Token: 0x0400108D RID: 4237
	public JSONObject YuanYingBiao;

	// Token: 0x0400108E RID: 4238
	public JSONObject jiaoHuanShopGoods;

	// Token: 0x0400108F RID: 4239
	public JSONObject LianDanDanFangBiao;

	// Token: 0x04001090 RID: 4240
	public JSONObject LianDanItemLeiXin;

	// Token: 0x04001091 RID: 4241
	public JSONObject LianDanSuccessItemLeiXin;

	// Token: 0x04001092 RID: 4242
	public JSONObject DanduMiaoShu;

	// Token: 0x04001093 RID: 4243
	public JSONObject CaiYaoShoYi;

	// Token: 0x04001094 RID: 4244
	public JSONObject CaiYaoDiaoLuo;

	// Token: 0x04001095 RID: 4245
	public JSONObject LiShiChuanWen;

	// Token: 0x04001096 RID: 4246
	public JSONObject AllMapLuDainType;

	// Token: 0x04001097 RID: 4247
	public JSONObject AllMapReset;

	// Token: 0x04001098 RID: 4248
	public JSONObject StaticValueSay;

	// Token: 0x04001099 RID: 4249
	public JSONObject ShiLiHaoGanDuName;

	// Token: 0x0400109A RID: 4250
	public JSONObject AllMapCaiJiBiao;

	// Token: 0x0400109B RID: 4251
	public JSONObject AllMapCaiJiMiaoShuBiao;

	// Token: 0x0400109C RID: 4252
	public JSONObject AllMapCaiJiAddItemBiao;

	// Token: 0x0400109D RID: 4253
	public JSONObject CreateAvatarMiaoShu;

	// Token: 0x0400109E RID: 4254
	public JSONObject WuJiangBangDing;

	// Token: 0x0400109F RID: 4255
	public JSONObject NTaskAllType;

	// Token: 0x040010A0 RID: 4256
	public JSONObject NTaskXiangXi;

	// Token: 0x040010A1 RID: 4257
	public JSONObject NTaskSuiJI;

	// Token: 0x040010A2 RID: 4258
	public JSONObject WuDaoJinJieJson;

	// Token: 0x040010A3 RID: 4259
	public JSONObject WuDaoJson;

	// Token: 0x040010A4 RID: 4260
	public JSONObject WuDaoAllTypeJson;

	// Token: 0x040010A5 RID: 4261
	public JSONObject WuDaoExBeiLuJson;

	// Token: 0x040010A6 RID: 4262
	public JSONObject NPCWuDaoJson;

	// Token: 0x040010A7 RID: 4263
	public JSONObject LingGuangJson;

	// Token: 0x040010A8 RID: 4264
	public JSONObject KillAvatarLingGuangJson;

	// Token: 0x040010A9 RID: 4265
	public JSONObject wupingfenlan;

	// Token: 0x040010AA RID: 4266
	public JSONObject LianQiWuWeiBiao;

	// Token: 0x040010AB RID: 4267
	public JSONObject CaiLiaoNengLiangBiao;

	// Token: 0x040010AC RID: 4268
	public JSONObject LianQiHeCheng;

	// Token: 0x040010AD RID: 4269
	public JSONObject LianQiEquipIconBiao;

	// Token: 0x040010AE RID: 4270
	public JSONObject LianQiDuoDuanShangHaiBiao;

	// Token: 0x040010AF RID: 4271
	public JSONObject LianQiJieSuoBiao;

	// Token: 0x040010B0 RID: 4272
	public JSONObject ChuanYingFuBiao;

	// Token: 0x040010B1 RID: 4273
	public JSONObject MenPaiFengLuBiao;

	// Token: 0x040010B2 RID: 4274
	public JObject CaiLiaoShuXingBIAO;

	// Token: 0x040010B3 RID: 4275
	public JObject StaticNTaks;

	// Token: 0x040010B4 RID: 4276
	public JObject StaticNTaksTime;

	// Token: 0x040010B5 RID: 4277
	public JObject BadWord;

	// Token: 0x040010B6 RID: 4278
	public JObject BiGuanWuDao;

	// Token: 0x040010B7 RID: 4279
	public JObject ChengJiuJson;

	// Token: 0x040010B8 RID: 4280
	public JObject RandomMapType;

	// Token: 0x040010B9 RID: 4281
	public JObject RandomMapList;

	// Token: 0x040010BA RID: 4282
	public JObject RandomMapEventList;

	// Token: 0x040010BB RID: 4283
	public JObject RandomMapFirstName;

	// Token: 0x040010BC RID: 4284
	public JObject ResetAvatarBackpackBanBen;

	// Token: 0x040010BD RID: 4285
	public JObject EndlessSeaRandomData;

	// Token: 0x040010BE RID: 4286
	public JObject EndlessSeaType;

	// Token: 0x040010BF RID: 4287
	public JObject EndlessSeaData;

	// Token: 0x040010C0 RID: 4288
	public JObject EndlessSeaNPCData;

	// Token: 0x040010C1 RID: 4289
	public JObject EndlessSeaNPCGouChengData;

	// Token: 0x040010C2 RID: 4290
	public JObject EndlessSeaSafeLvData;

	// Token: 0x040010C3 RID: 4291
	public JObject EndlessSeaLinQiSafeLvData;

	// Token: 0x040010C4 RID: 4292
	public JObject EndlessSeaLuanLIuXinZhuang;

	// Token: 0x040010C5 RID: 4293
	public JObject EndlessSeaHaiYuData;

	// Token: 0x040010C6 RID: 4294
	public JObject EndlessSeaAIChuFa;

	// Token: 0x040010C7 RID: 4295
	public JObject EndlessSeaLuanLiuRandom;

	// Token: 0x040010C8 RID: 4296
	public JObject EndlessSeaLuanLiuRandomMap;

	// Token: 0x040010C9 RID: 4297
	public JObject EndlessSeaShiYe;

	// Token: 0x040010CA RID: 4298
	public JObject LingZhouPinJie;

	// Token: 0x040010CB RID: 4299
	public JObject NPCInterestingItem;

	// Token: 0x040010CC RID: 4300
	public JObject AllItemLeiXin;

	// Token: 0x040010CD RID: 4301
	public JObject SeaStaticIsland;

	// Token: 0x040010CE RID: 4302
	public JObject LianQiEquipType;

	// Token: 0x040010CF RID: 4303
	public JObject LianQiWuQiQuality;

	// Token: 0x040010D0 RID: 4304
	public JSONObject LianQiJieSuanBiao;

	// Token: 0x040010D1 RID: 4305
	public JSONObject LianQiShuXinLeiBie;

	// Token: 0x040010D2 RID: 4306
	public JSONObject LianQiLingWenBiao;

	// Token: 0x040010D3 RID: 4307
	[Obsolete]
	public JSONObject heroFaceJsonData;

	// Token: 0x040010D4 RID: 4308
	[Obsolete]
	public JSONObject heroFaceByIDJsonData;

	// Token: 0x040010D5 RID: 4309
	[Obsolete]
	public JSONObject heroJsonData;

	// Token: 0x040010D6 RID: 4310
	[Obsolete]
	public JSONObject PlayerGoodsSJsonData;

	// Token: 0x040010D7 RID: 4311
	[Obsolete]
	public JSONObject CheckInJsonData;

	// Token: 0x040010D8 RID: 4312
	[Obsolete]
	public JSONObject ItemGoodSeid1JsonData;

	// Token: 0x040010D9 RID: 4313
	[Obsolete]
	public JSONObject TalkingJsonData;

	// Token: 0x040010DA RID: 4314
	[Obsolete]
	public JSONObject MessageJsonData;

	// Token: 0x040010DB RID: 4315
	public JSONObject NPCChuShiHuaDate;

	// Token: 0x040010DC RID: 4316
	public JSONObject NPCLeiXingDate;

	// Token: 0x040010DD RID: 4317
	public JSONObject NPCChengHaoData;

	// Token: 0x040010DE RID: 4318
	public JSONObject NPCChuShiShuZiDate;

	// Token: 0x040010DF RID: 4319
	public JSONObject NPCImportantDate;

	// Token: 0x040010E0 RID: 4320
	public JSONObject NPCActionDate;

	// Token: 0x040010E1 RID: 4321
	public JSONObject NPCActionPanDingDate;

	// Token: 0x040010E2 RID: 4322
	public JSONObject NPCTagDate;

	// Token: 0x040010E3 RID: 4323
	public JSONObject NPCTuPuoDate;

	// Token: 0x040010E4 RID: 4324
	public JSONObject NpcFuBenMapBingDate;

	// Token: 0x040010E5 RID: 4325
	public JSONObject NpcThreeMapBingDate;

	// Token: 0x040010E6 RID: 4326
	public JSONObject NpcBigMapBingDate;

	// Token: 0x040010E7 RID: 4327
	public JSONObject NpcYaoShouDrop;

	// Token: 0x040010E8 RID: 4328
	public JSONObject NpcLevelShouYiDate;

	// Token: 0x040010E9 RID: 4329
	public JSONObject NpcXingGeDate;

	// Token: 0x040010EA RID: 4330
	public JSONObject NpcYiWaiDeathDate;

	// Token: 0x040010EB RID: 4331
	public JSONObject NpcQiYuDate;

	// Token: 0x040010EC RID: 4332
	public JSONObject NpcBeiBaoTypeData;

	// Token: 0x040010ED RID: 4333
	public JSONObject NpcShiJianData;

	// Token: 0x040010EE RID: 4334
	public JSONObject NpcStatusDate;

	// Token: 0x040010EF RID: 4335
	public JSONObject NpcPaiMaiData;

	// Token: 0x040010F0 RID: 4336
	public JSONObject NpcImprotantPanDingData;

	// Token: 0x040010F1 RID: 4337
	public JSONObject NpcHaoGanDuData;

	// Token: 0x040010F2 RID: 4338
	public JSONObject NpcCreateData;

	// Token: 0x040010F3 RID: 4339
	public JSONObject NpcJinHuoData;

	// Token: 0x040010F4 RID: 4340
	public JSONObject NpcImprotantEventData;

	// Token: 0x040010F5 RID: 4341
	public JSONObject NpcHaiShangCreateData;

	// Token: 0x040010F6 RID: 4342
	public JSONObject NpcTalkShouCiJiaoTanData;

	// Token: 0x040010F7 RID: 4343
	public JSONObject NpcTalkHouXuJiaoTanData;

	// Token: 0x040010F8 RID: 4344
	public JSONObject NpcTalkQiTaJiaoHuData;

	// Token: 0x040010F9 RID: 4345
	public JSONObject NpcBiaoBaiTiKuData;

	// Token: 0x040010FA RID: 4346
	public JSONObject NpcBiaoBaiTiWenData;

	// Token: 0x040010FB RID: 4347
	public JSONObject NpcTalkGuanYuTuPoData;

	// Token: 0x040010FC RID: 4348
	public JSONObject NpcQingJiaoXiaoHaoData;

	// Token: 0x040010FD RID: 4349
	public JSONObject NpcQingJiaoItemData;

	// Token: 0x040010FE RID: 4350
	public JSONObject NpcWuDaoChiData;

	// Token: 0x040010FF RID: 4351
	public JSONObject CyRandomTaskData;

	// Token: 0x04001100 RID: 4352
	public JSONObject CyRandomTaskFailData;

	// Token: 0x04001101 RID: 4353
	public JSONObject NewTaskMagData;

	// Token: 0x04001102 RID: 4354
	public JSONObject FightAIData;

	// Token: 0x04001103 RID: 4355
	public JSONObject LunDaoStateData;

	// Token: 0x04001104 RID: 4356
	public JSONObject LunDaoSayData;

	// Token: 0x04001105 RID: 4357
	public JSONObject LunDaoShouYiData;

	// Token: 0x04001106 RID: 4358
	public JSONObject WuDaoZhiData;

	// Token: 0x04001107 RID: 4359
	public JSONObject LunDaoSiXuData;

	// Token: 0x04001108 RID: 4360
	public JSONObject LingGanMaxData;

	// Token: 0x04001109 RID: 4361
	public JSONObject LingGanLevelData;

	// Token: 0x0400110A RID: 4362
	public JSONObject WuDaoZhiJiaCheng;

	// Token: 0x0400110B RID: 4363
	public JSONObject ShengWangLevelData;

	// Token: 0x0400110C RID: 4364
	public JSONObject DiYuShengWangData;

	// Token: 0x0400110D RID: 4365
	public JSONObject ShangJinPingFenData;

	// Token: 0x0400110E RID: 4366
	public JSONObject ShengWangShangJinData;

	// Token: 0x0400110F RID: 4367
	public JSONObject XuanShangMiaoShuData;

	// Token: 0x04001110 RID: 4368
	public JSONObject ShiLiShenFenData;

	// Token: 0x04001111 RID: 4369
	public JSONObject CyShiLiNameData;

	// Token: 0x04001112 RID: 4370
	public JSONObject CyTeShuNpc;

	// Token: 0x04001113 RID: 4371
	public JSONObject ScenePriceData;

	// Token: 0x04001114 RID: 4372
	public JSONObject CyZiDuanData;

	// Token: 0x04001115 RID: 4373
	public JSONObject CyPlayeQuestionData;

	// Token: 0x04001116 RID: 4374
	public JSONObject CyNpcAnswerData;

	// Token: 0x04001117 RID: 4375
	public JSONObject CyNpcDuiBaiData;

	// Token: 0x04001118 RID: 4376
	public JSONObject CyNpcSendData;

	// Token: 0x04001119 RID: 4377
	public JSONObject RenWuDaLeiYouXianJi;

	// Token: 0x0400111A RID: 4378
	public JSONObject LunDaoReduceData;

	// Token: 0x0400111B RID: 4379
	public JSONObject LingGanTimeMaxData;

	// Token: 0x0400111C RID: 4380
	public JSONObject ShuangXiuMiShu;

	// Token: 0x0400111D RID: 4381
	public JSONObject ShuangXiuJingYuanJiaZhi;

	// Token: 0x0400111E RID: 4382
	public JSONObject ShuangXiuLianHuaSuDu;

	// Token: 0x0400111F RID: 4383
	public JSONObject ShuangXiuJingJieBeiLv;

	// Token: 0x04001120 RID: 4384
	public JSONObject DFLingYanLevel;

	// Token: 0x04001121 RID: 4385
	public JSONObject DFZhenYanLevel;

	// Token: 0x04001122 RID: 4386
	public JSONObject DFBuKeZhongZhi;

	// Token: 0x04001123 RID: 4387
	public JSONObject SeaHaiYuJiZhiShuaXin;

	// Token: 0x04001124 RID: 4388
	public JSONObject SeaJiZhiID;

	// Token: 0x04001125 RID: 4389
	public JSONObject SeaJiZhiXingXiang;

	// Token: 0x04001126 RID: 4390
	public JSONObject SeaHaiYuTanSuo;

	// Token: 0x04001127 RID: 4391
	public JSONObject MapIndexData;

	// Token: 0x04001128 RID: 4392
	public JSONObject GaoShiLeiXing;

	// Token: 0x04001129 RID: 4393
	public JSONObject GaoShi;

	// Token: 0x0400112A RID: 4394
	public JSONObject ZhuChengRenWu;

	// Token: 0x0400112B RID: 4395
	public JSONObject PaiMaiPanDing;

	// Token: 0x0400112C RID: 4396
	public JSONObject PaiMaiChuJia;

	// Token: 0x0400112D RID: 4397
	public JSONObject PaiMaiCommandTips;

	// Token: 0x0400112E RID: 4398
	public JSONObject PaiMaiDuiHuaAI;

	// Token: 0x0400112F RID: 4399
	public JSONObject PaiMaiChuJiaAI;

	// Token: 0x04001130 RID: 4400
	public JSONObject PaiMaiOldAvatar;

	// Token: 0x04001131 RID: 4401
	public JSONObject PaiMaiNpcAddPriceSay;

	// Token: 0x04001132 RID: 4402
	public JSONObject LingHeCaiJi;

	// Token: 0x04001133 RID: 4403
	public JSONObject LingMaiPinJie;

	// Token: 0x04001134 RID: 4404
	public JSONObject HuaShenData;

	// Token: 0x04001135 RID: 4405
	public JSONObject TianJieMiShuData;

	// Token: 0x04001136 RID: 4406
	public JSONObject TianJiDaBi;

	// Token: 0x04001137 RID: 4407
	public JSONObject TianJiDaBiGongFangKeZhi;

	// Token: 0x04001138 RID: 4408
	public JSONObject TianJiDaBiReward;

	// Token: 0x04001139 RID: 4409
	public JSONObject ShengPing;

	// Token: 0x0400113A RID: 4410
	public JSONObject JianLingXianSuo;

	// Token: 0x0400113B RID: 4411
	public JSONObject JianLingZhenXiang;

	// Token: 0x0400113C RID: 4412
	public JSONObject JianLingQingJiao;

	// Token: 0x0400113D RID: 4413
	public bool IsResetAvatarFace;

	// Token: 0x0400113E RID: 4414
	public ItemDatabase playerDatabase;

	// Token: 0x0400113F RID: 4415
	public JSONObject[] SkillSeidJsonData = new JSONObject[500];

	// Token: 0x04001140 RID: 4416
	public JSONObject[] BuffSeidJsonData = new JSONObject[500];

	// Token: 0x04001141 RID: 4417
	public JSONObject[] VersionJsonData = new JSONObject[500];

	// Token: 0x04001142 RID: 4418
	public JSONObject[] StaticSkillSeidJsonData = new JSONObject[500];

	// Token: 0x04001143 RID: 4419
	public JSONObject[] WuDaoSeidJsonData = new JSONObject[500];

	// Token: 0x04001144 RID: 4420
	public JSONObject[] ItemsSeidJsonData = new JSONObject[500];

	// Token: 0x04001145 RID: 4421
	public JSONObject[] EquipSeidJsonData = new JSONObject[500];

	// Token: 0x04001146 RID: 4422
	public JSONObject[] CrateAvatarSeidJsonData = new JSONObject[500];

	// Token: 0x04001147 RID: 4423
	public JSONObject[] JieDanSeidJsonData = new JSONObject[500];

	// Token: 0x04001148 RID: 4424
	public Dictionary<int, List<JSONObject>> FuBenJsonData = new Dictionary<int, List<JSONObject>>();

	// Token: 0x04001149 RID: 4425
	public List<int> RandomList = new List<int>();

	// Token: 0x0400114A RID: 4426
	public int randomListIndex;

	// Token: 0x0400114B RID: 4427
	public RandomFace body;

	// Token: 0x0400114C RID: 4428
	public RandomFace eye;

	// Token: 0x0400114D RID: 4429
	public RandomFace eyebrow;

	// Token: 0x0400114E RID: 4430
	public RandomFace face;

	// Token: 0x0400114F RID: 4431
	public RandomFace Facefold;

	// Token: 0x04001150 RID: 4432
	public RandomFace hair;

	// Token: 0x04001151 RID: 4433
	public RandomFace hair2;

	// Token: 0x04001152 RID: 4434
	public RandomFace mouth;

	// Token: 0x04001153 RID: 4435
	public RandomFace mustache;

	// Token: 0x04001154 RID: 4436
	public RandomFace nose;

	// Token: 0x04001155 RID: 4437
	public RandomFace ornament;

	// Token: 0x04001156 RID: 4438
	public GameObject TextError;

	// Token: 0x04001157 RID: 4439
	public GameObject SkillHint;

	// Token: 0x04001158 RID: 4440
	public bool reloadRandomAvatarFace;

	// Token: 0x04001159 RID: 4441
	public static bool showGongGao = true;

	// Token: 0x0400115A RID: 4442
	private static int rDieZiNameCount = 0;

	// Token: 0x020002C0 RID: 704
	public class YSDictionary<TKey, TValue> : Dictionary<TKey, TValue>
	{
		// Token: 0x06001565 RID: 5477 RVA: 0x00013640 File Offset: 0x00011840
		public bool HasField(TKey key)
		{
			return base.ContainsKey(key);
		}
	}

	// Token: 0x020002C1 RID: 705
	public enum SeidCount
	{
		// Token: 0x0400115C RID: 4444
		buffCount = 500,
		// Token: 0x0400115D RID: 4445
		skillCount = 500,
		// Token: 0x0400115E RID: 4446
		StaticSkillCount = 500
	}

	// Token: 0x020002C2 RID: 706
	public enum RandomFaceType
	{
		// Token: 0x04001160 RID: 4448
		body,
		// Token: 0x04001161 RID: 4449
		eye,
		// Token: 0x04001162 RID: 4450
		eyebrow,
		// Token: 0x04001163 RID: 4451
		face,
		// Token: 0x04001164 RID: 4452
		Facefold,
		// Token: 0x04001165 RID: 4453
		hair,
		// Token: 0x04001166 RID: 4454
		mouth,
		// Token: 0x04001167 RID: 4455
		mustache,
		// Token: 0x04001168 RID: 4456
		nose,
		// Token: 0x04001169 RID: 4457
		ornament
	}

	// Token: 0x020002C3 RID: 707
	public enum InventoryNUM
	{
		// Token: 0x0400116B RID: 4459
		Shop = 9,
		// Token: 0x0400116C RID: 4460
		Max = 24,
		// Token: 0x0400116D RID: 4461
		EXIventoryNum = 34,
		// Token: 0x0400116E RID: 4462
		LinQiEXIventoryNum,
		// Token: 0x0400116F RID: 4463
		SkillMax = 30,
		// Token: 0x04001170 RID: 4464
		FightEat = 32,
		// Token: 0x04001171 RID: 4465
		PaiMai = 12,
		// Token: 0x04001172 RID: 4466
		PaiMaiPlayer = 25,
		// Token: 0x04001173 RID: 4467
		PaiMaiXianShi = 1,
		// Token: 0x04001174 RID: 4468
		ShopEX = 10,
		// Token: 0x04001175 RID: 4469
		LianDan = 29,
		// Token: 0x04001176 RID: 4470
		LianDanDanLu = 18,
		// Token: 0x04001177 RID: 4471
		LianDanFinish = 6,
		// Token: 0x04001178 RID: 4472
		CaijiTeChan = 8,
		// Token: 0x04001179 RID: 4473
		CaijiDiaoLuo = 2,
		// Token: 0x0400117A RID: 4474
		FaceRandomTime = 500,
		// Token: 0x0400117B RID: 4475
		NewLianDan = 37,
		// Token: 0x0400117C RID: 4476
		NewJiaoYiNum = 15
	}
}
