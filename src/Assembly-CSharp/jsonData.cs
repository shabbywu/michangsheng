using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using KBEngine;
using Newtonsoft.Json.Linq;
using UltimateSurvival;
using UnityEngine;
using YSGame;

// Token: 0x020001C0 RID: 448
public class jsonData : MonoBehaviour
{
	// Token: 0x0600127F RID: 4735 RVA: 0x00070D1E File Offset: 0x0006EF1E
	private void Awake()
	{
		jsonData.instance = this;
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x00070D28 File Offset: 0x0006EF28
	public void Preload(int taskID)
	{
		try
		{
			this.LoadSync();
		}
		catch
		{
			PreloadManager.IsException = true;
			PreloadManager.Inst.TaskDone(taskID);
		}
		Loom.RunAsync(delegate
		{
			this.LoadAsync(taskID);
		});
	}

	// Token: 0x06001281 RID: 4737 RVA: 0x00070D8C File Offset: 0x0006EF8C
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

	// Token: 0x06001282 RID: 4738 RVA: 0x00070EB0 File Offset: 0x0006F0B0
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
			PreloadManager.Inst.TaskDone(taskID);
		}
	}

	// Token: 0x06001283 RID: 4739 RVA: 0x00070F10 File Offset: 0x0006F110
	private void InitLogic()
	{
		this.init("Effect/json/d_JiaoYiHui.py.gudingyiwu.json", out this.GuDingExchangeData);
		this.init("Effect/json/d_JiaoYiHui.py.suijiyiwu", out this.RandomExchangeData);
		this.init("Effect/json/d_JiaoYiHui.py.pingbiwupin", out this.DisableExchangeData);
		this.init("Effect/json/d_JiaoYiHui.py.tishi", out this.TipsExchangeData);
		this.init("Effect/json/d_JiaoYiHui.py.wupinleixing", out this.ItemTypeExchangeData);
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
		this.init("Effect/json/d_Map.py.DongTaiChuanWenBaio", out this.DongTaiChuanWenBaio);
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
		this.init("Effect/json/d_str.py.touxiangpianyi", out this.TouXiangPianYi);
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
		this.init("Effect/json/d_dujie.py.leijieleixing", out this.TianJieLeiJieType);
		this.init("Effect/json/d_dujie.py.leijieshanghai", out this.TianJieLeiJieShangHai);
		this.init("Effect/json/d_LianDan.py.linghecaiji", out this.LingHeCaiJi);
		this.init("Effect/json/d_LianDan.py.lingmaipinjie", out this.LingMaiPinJie);
		this.init("Effect/json/d_ChuanYin.py.suijiNPCrenwuchuanyin", out this.CyRandomTaskData);
		this.init("Effect/json/d_ChuanYin.py.renwushibaichuanyin", out this.CyRandomTaskFailData);
		this.init("Effect/json/d_task.py.xinrenwuguanli", out this.NewTaskMagData);
		this.init("Effect/json/d_AvatarAI.py.tianjidabi", out this.TianJiDaBi);
		this.init("Effect/json/d_AvatarAI.py.gongfangkezhi", out this.TianJiDaBiGongFangKeZhi);
		this.init("Effect/json/d_AvatarAI.py.tianjidabijiangli", out this.TianJiDaBiReward);
		this.init("Effect/json/d_AvatarAI.py.chuanwenleixing", out this.ChuanWenTypeData);
		this.init("Effect/json/d_ShengPing.py.shengping", out this.ShengPing);
		this.init("Effect/json/d_avatar.py.FengLu", out this.MenPaiFengLuBiao);
		this.init("Effect/json/d_items.py.wupingbiaoqian", out this.ItemFlagData);
		this.init("Effect/json/d_JianLing.py.XianSuo", out this.JianLingXianSuo);
		this.init("Effect/json/d_JianLing.py.ZhenXiang", out this.JianLingZhenXiang);
		this.init("Effect/json/d_JianLing.py.QingJiao", out this.JianLingQingJiao);
		this.init("Effect/json/d_ZhangLaoRenWu.py.wupintype", out this.ElderTaskItemType);
		this.init("Effect/json/d_ZhangLaoRenWu.py.weijinwupin", out this.ElderTaskDisableItem);
		this.init("Effect/json/d_ZhangLaoRenWu.py.timecost", out this.ElderTaskItemCost);
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

	// Token: 0x06001284 RID: 4740 RVA: 0x00072290 File Offset: 0x00070490
	public JSONObject getTaskInfo(int taskID, int index)
	{
		foreach (JSONObject jsonobject in jsonData.instance.TaskInfoJsonData.list)
		{
			if (jsonobject["TaskID"].I == taskID && jsonobject["TaskIndex"].I == index)
			{
				return jsonobject;
			}
		}
		return null;
	}

	// Token: 0x06001285 RID: 4741 RVA: 0x00072314 File Offset: 0x00070514
	public void loadAvatarBackpack(int id, int index)
	{
		this.AvatarBackpackJsonData = YSSaveGame.GetJsonObject("AvatarBackpackJsonData" + Tools.instance.getSaveID(id, index), null);
	}

	// Token: 0x06001286 RID: 4742 RVA: 0x00072338 File Offset: 0x00070538
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

	// Token: 0x06001287 RID: 4743 RVA: 0x00072468 File Offset: 0x00070668
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

	// Token: 0x06001288 RID: 4744 RVA: 0x00072624 File Offset: 0x00070824
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

	// Token: 0x06001289 RID: 4745 RVA: 0x000727C0 File Offset: 0x000709C0
	public List<int> GetItemXiangXiLeiXin(int itemID)
	{
		List<int> list = new List<int>();
		this.ItemJsonData[itemID.ToString()]["ItemFlag"].list.ForEach(delegate(JSONObject aa)
		{
			list.Add(aa.I);
		});
		return list;
	}

	// Token: 0x0600128A RID: 4746 RVA: 0x00072818 File Offset: 0x00070A18
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

	// Token: 0x0600128B RID: 4747 RVA: 0x000728AC File Offset: 0x00070AAC
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
					this.MonstarRemoveItem(monstarID, jsonobject["ItemID"].I, itemNum, false);
				}
			}
		}
	}

	// Token: 0x0600128C RID: 4748 RVA: 0x000729A4 File Offset: 0x00070BA4
	public void MonstarRemoveItem(int monstarID, int ItemID, int ItemNum, bool isPaiMai = false)
	{
		foreach (JSONObject jsonobject in this.AvatarBackpackJsonData[string.Concat(monstarID)]["Backpack"].list)
		{
			if ((!isPaiMai || (jsonobject.HasField("paiMaiPlayer") && jsonobject["paiMaiPlayer"].I >= 1)) && ItemID == jsonobject["ItemID"].I && jsonobject["Num"].I > 0)
			{
				if (jsonobject["Num"].I - ItemNum >= 0)
				{
					jsonobject.SetField("Num", jsonobject["Num"].I - ItemNum);
					break;
				}
				int itemNum = ItemNum - (int)jsonobject["Num"].n;
				jsonobject.SetField("Num", 0);
				this.MonstarRemoveItem(monstarID, ItemID, itemNum, false);
				break;
			}
		}
	}

	// Token: 0x0600128D RID: 4749 RVA: 0x00072AC8 File Offset: 0x00070CC8
	public bool MonstarIsDeath(int monstarID)
	{
		bool result = false;
		if (this.AvatarBackpackJsonData.HasField(string.Concat(monstarID)) && this.AvatarBackpackJsonData[string.Concat(monstarID)].HasField("death") && this.AvatarBackpackJsonData[string.Concat(monstarID)]["death"].n == 1f)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x0600128E RID: 4750 RVA: 0x00072B40 File Offset: 0x00070D40
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

	// Token: 0x0600128F RID: 4751 RVA: 0x00072C54 File Offset: 0x00070E54
	public void MonstarSetHaoGanDu(int monstarID, int haogandu)
	{
		float n = this.AvatarRandomJsonData[monstarID.ToString()]["HaoGanDu"].n;
		foreach (int num in this.getHaoGanDUGuanLian(monstarID))
		{
			int num2 = (int)this.AvatarRandomJsonData[num.ToString()]["HaoGanDu"].n;
			this.AvatarRandomJsonData[num.ToString()].SetField("HaoGanDu", Mathf.Clamp(num2 + haogandu, 0, 100));
		}
	}

	// Token: 0x06001290 RID: 4752 RVA: 0x00072D10 File Offset: 0x00070F10
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
					if (jsonobject["AvatrID"].I == monstarID)
					{
						this.AvatarAddBackpackByInfo(ref this.AvatarBackpackJsonData, jsonobject);
					}
				}
				return;
			}
		}
		this.AvatarBackpackJsonData[string.Concat(monstarID)].SetField("death", 1);
	}

	// Token: 0x06001291 RID: 4753 RVA: 0x00072E78 File Offset: 0x00071078
	public void refreshMonstar(int monstarID)
	{
		JSONObject jsonobject = this.randomAvatarFace(this.AvatarJsonData[string.Concat(monstarID)], null);
		this.AvatarRandomJsonData[string.Concat(monstarID)] = jsonobject.Copy();
	}

	// Token: 0x06001292 RID: 4754 RVA: 0x00072EC0 File Offset: 0x000710C0
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

	// Token: 0x06001293 RID: 4755 RVA: 0x00072F38 File Offset: 0x00071138
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

	// Token: 0x06001294 RID: 4756 RVA: 0x000730B0 File Offset: 0x000712B0
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
						Debug.LogError(info["AvatrID"].I + "背包配置出错物品id" + jsonobject.I);
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
			JSONObject obj2 = this.setAvatarBackpack(Tools.getUUID(), randomItem, info["randomNum"][num2].I, (int)info["CanSell"].n, (int)info["SellPercent"].n, (int)info["CanDrop"].n, Tools.CreateItemSeid(randomItem), 0);
			jsondata[string.Concat(info["AvatrID"].I)]["Backpack"].Add(obj2);
			num2++;
		}
	}

	// Token: 0x06001295 RID: 4757 RVA: 0x0007331C File Offset: 0x0007151C
	public void randomAvatarBackpack(int id, int index)
	{
		JSONObject jsonobject = new JSONObject();
		Avatar avatar = Tools.instance.getPlayer();
		List<JToken> list = Tools.FindAllJTokens(this.ResetAvatarBackpackBanBen, (JToken aa) => (int)aa["BanBenID"] > avatar.BanBenHao);
		foreach (JSONObject jsonobject2 in this.BackpackJsonData.list)
		{
			int avatarID = jsonobject2["AvatrID"].I;
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

	// Token: 0x06001296 RID: 4758 RVA: 0x00073494 File Offset: 0x00071694
	public int getSellPercent(int monstarID, int itemID)
	{
		int result = 100;
		foreach (JSONObject jsonobject in this.AvatarBackpackJsonData[string.Concat(monstarID)]["Backpack"].list)
		{
			if (jsonobject["ItemID"].I == itemID && jsonobject["Num"].I > 0)
			{
				return (int)jsonobject["SellPercent"].n;
			}
		}
		return result;
	}

	// Token: 0x06001297 RID: 4759 RVA: 0x00073540 File Offset: 0x00071740
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
					return jsonobject2["id"].I;
				}
				num3++;
			}
		}
		return 0;
	}

	// Token: 0x06001298 RID: 4760 RVA: 0x000736A4 File Offset: 0x000718A4
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

	// Token: 0x06001299 RID: 4761 RVA: 0x000737EC File Offset: 0x000719EC
	public string RandomFirstName()
	{
		int num = this.getRandom() % this.firstNameJsonData.Count + 1;
		return this.firstNameJsonData[string.Concat(num)]["Name"].Str;
	}

	// Token: 0x0600129A RID: 4762 RVA: 0x00073834 File Offset: 0x00071A34
	public string RandomManLastName()
	{
		int num = this.getRandom() % this.LastNameJsonData.Count + 1;
		return this.LastNameJsonData[string.Concat(num)]["Name"].Str;
	}

	// Token: 0x0600129B RID: 4763 RVA: 0x0007387C File Offset: 0x00071A7C
	public string RandomWomenLastName()
	{
		int num = this.getRandom() % this.LastWomenNameJsonData.Count + 1;
		return this.LastWomenNameJsonData[string.Concat(num)]["Name"].Str;
	}

	// Token: 0x0600129C RID: 4764 RVA: 0x000738C4 File Offset: 0x00071AC4
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
			List<int> suijiList = this.getSuijiList(jsonobject2["StrID"].str, "SuiJiSex" + info["SexType"].I);
			List<int> suijiList2 = this.getSuijiList(jsonobject2["StrID"].str, "Sex" + info["SexType"].I);
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
				num2 = SetAvatarFaceRandomInfo.inst.getFace(info["id"].I, type);
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

	// Token: 0x0600129D RID: 4765 RVA: 0x00073B4C File Offset: 0x00071D4C
	public void UpdateGuDingNpcFace(int id, JSONObject temp)
	{
		foreach (JSONObject jsonobject in jsonData.instance.SuiJiTouXiangGeShuJsonData.list)
		{
			int val = 0;
			int num = -100;
			SetAvatarFaceRandomInfo.InfoName type;
			if (Enum.TryParse<SetAvatarFaceRandomInfo.InfoName>(jsonobject["StrID"].str, true, out type))
			{
				num = SetAvatarFaceRandomInfo.inst.getFace(id, type);
			}
			if (num != -100)
			{
				val = num;
			}
			temp.SetField(jsonobject["StrID"].str, val);
		}
	}

	// Token: 0x0600129E RID: 4766 RVA: 0x00073BEC File Offset: 0x00071DEC
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

	// Token: 0x0600129F RID: 4767 RVA: 0x00073C98 File Offset: 0x00071E98
	public void initAvatarFace(int id, int index, int startIndex = 1)
	{
		this.AvatarRandomJsonData = YSSaveGame.GetJsonObject("AvatarRandomJsonData" + Tools.instance.getSaveID(id, index), null);
		if (YSSaveGame.GetInt("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(id, index), 0) == 0 || this.reloadRandomAvatarFace)
		{
			foreach (JSONObject jsonobject in this.AvatarJsonData.list)
			{
				if (jsonobject["id"].I != 1 && jsonobject["id"].I >= startIndex)
				{
					if (jsonobject["id"].I >= 20000)
					{
						break;
					}
					JSONObject jsonobject2 = this.randomAvatarFace(jsonobject, this.AvatarRandomJsonData.HasField(string.Concat(jsonobject["id"].I)) ? this.AvatarRandomJsonData[jsonobject["id"].I.ToString()] : null);
					this.AvatarRandomJsonData.SetField(string.Concat(jsonobject["id"].I), jsonobject2.Copy());
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

	// Token: 0x060012A0 RID: 4768 RVA: 0x00073E88 File Offset: 0x00072088
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
				JSONObject jsonobject2 = this.randomAvatarFace(jsonobject, this.AvatarRandomJsonData.HasField(string.Concat(jsonobject["id"].I)) ? this.AvatarRandomJsonData[jsonobject["id"].I.ToString()] : null);
				this.AvatarRandomJsonData.SetField(string.Concat(jsonobject["id"].I), jsonobject2.Clone());
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

	// Token: 0x060012A1 RID: 4769 RVA: 0x0007400C File Offset: 0x0007220C
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

	// Token: 0x060012A2 RID: 4770 RVA: 0x0007412C File Offset: 0x0007232C
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

	// Token: 0x060012A3 RID: 4771 RVA: 0x00074184 File Offset: 0x00072384
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

	// Token: 0x060012A4 RID: 4772 RVA: 0x000741DC File Offset: 0x000723DC
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

	// Token: 0x060012A5 RID: 4773 RVA: 0x00074234 File Offset: 0x00072434
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

	// Token: 0x060012A6 RID: 4774 RVA: 0x0007428C File Offset: 0x0007248C
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

	// Token: 0x060012A7 RID: 4775 RVA: 0x000742E4 File Offset: 0x000724E4
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

	// Token: 0x060012A8 RID: 4776 RVA: 0x0007433C File Offset: 0x0007253C
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

	// Token: 0x060012A9 RID: 4777 RVA: 0x00074394 File Offset: 0x00072594
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

	// Token: 0x060012AA RID: 4778 RVA: 0x000743EC File Offset: 0x000725EC
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

	// Token: 0x060012AB RID: 4779 RVA: 0x00074444 File Offset: 0x00072644
	public void InitBuff()
	{
		foreach (JSONObject jsonobject in this._BuffJsonData.list)
		{
			this.Buff.Add(jsonobject["buffid"].I, new Buff(jsonobject["buffid"].I));
		}
	}

	// Token: 0x060012AC RID: 4780 RVA: 0x000744C8 File Offset: 0x000726C8
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

	// Token: 0x060012AD RID: 4781 RVA: 0x0007459C File Offset: 0x0007279C
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

	// Token: 0x060012AE RID: 4782 RVA: 0x000745C8 File Offset: 0x000727C8
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

	// Token: 0x060012AF RID: 4783 RVA: 0x000745F9 File Offset: 0x000727F9
	public string getAvaterType(Entity entity)
	{
		return this.heroJsonData[string.Concat(entity.getDefinedProperty("roleTypeCell"))]["heroType"].str;
	}

	// Token: 0x060012B0 RID: 4784 RVA: 0x00074628 File Offset: 0x00072828
	public int getRandom()
	{
		byte[] array = new byte[8];
		new RNGCryptoServiceProvider().GetBytes(array);
		return Math.Abs(BitConverter.ToInt32(array, 0));
	}

	// Token: 0x060012B1 RID: 4785 RVA: 0x00074654 File Offset: 0x00072854
	public static int GetRandom()
	{
		byte[] array = new byte[8];
		new RNGCryptoServiceProvider().GetBytes(array);
		return Math.Abs(BitConverter.ToInt32(array, 0));
	}

	// Token: 0x060012B2 RID: 4786 RVA: 0x0007467F File Offset: 0x0007287F
	public int QuikeGetRandom()
	{
		this.randomListIndex++;
		if (this.randomListIndex >= this.RandomList.Count)
		{
			this.randomListIndex = 0;
		}
		return this.RandomList[this.randomListIndex];
	}

	// Token: 0x060012B3 RID: 4787 RVA: 0x000746BC File Offset: 0x000728BC
	public void setYSDictionor(JSONObject json, jsonData.YSDictionary<string, JSONObject> dict)
	{
		foreach (string text in json.keys)
		{
			dict[text] = json[text];
		}
	}

	// Token: 0x04000D0C RID: 3340
	public static jsonData instance;

	// Token: 0x04000D0D RID: 3341
	public static int QingJiaoItemIDSegment = 1000000000;

	// Token: 0x04000D0E RID: 3342
	public int saveState = -1;

	// Token: 0x04000D0F RID: 3343
	public List<int> hightLightSkillID = new List<int>
	{
		10,
		52,
		53,
		56,
		58,
		78,
		124
	};

	// Token: 0x04000D10 RID: 3344
	public List<string> NameColor = new List<string>
	{
		"d8d8ca",
		"cce281",
		"acfffe",
		"f1b7f8",
		"ffac5f",
		"ffb28b"
	};

	// Token: 0x04000D11 RID: 3345
	public List<string> WepenColor = new List<string>
	{
		"cce281",
		"f1b7f8",
		"ffb28b"
	};

	// Token: 0x04000D12 RID: 3346
	public List<string> TootipItemNameColor = new List<string>
	{
		"[d8d8ca]",
		"[b3d951]",
		"[71dbff]",
		"[ef6fff]",
		"[ff9d43]",
		"[ff744d]"
	};

	// Token: 0x04000D13 RID: 3347
	public List<string> TootipItemQualityColor = new List<string>
	{
		"[d8d8ca]",
		"[d7e281]",
		"[acfffe]",
		"[f1b7f8]",
		"[ffb143]",
		"[ffb28b]"
	};

	// Token: 0x04000D14 RID: 3348
	public bool SaveLock;

	// Token: 0x04000D15 RID: 3349
	public int ZombieBossID = 50;

	// Token: 0x04000D16 RID: 3350
	public Dictionary<int, Buff> Buff = new Dictionary<int, Buff>();

	// Token: 0x04000D17 RID: 3351
	public Dictionary<string, List<int>> faceTypeList = new Dictionary<string, List<int>>();

	// Token: 0x04000D18 RID: 3352
	public jsonData.YSDictionary<string, JSONObject> skillJsonData = new jsonData.YSDictionary<string, JSONObject>();

	// Token: 0x04000D19 RID: 3353
	public jsonData.YSDictionary<string, JSONObject> ItemJsonData = new jsonData.YSDictionary<string, JSONObject>();

	// Token: 0x04000D1A RID: 3354
	public jsonData.YSDictionary<string, JSONObject> BuffJsonData = new jsonData.YSDictionary<string, JSONObject>();

	// Token: 0x04000D1B RID: 3355
	public jsonData.YSDictionary<string, JSONObject> firstNameJsonData = new jsonData.YSDictionary<string, JSONObject>();

	// Token: 0x04000D1C RID: 3356
	public jsonData.YSDictionary<string, JSONObject> LastNameJsonData = new jsonData.YSDictionary<string, JSONObject>();

	// Token: 0x04000D1D RID: 3357
	public jsonData.YSDictionary<string, JSONObject> LastWomenNameJsonData = new jsonData.YSDictionary<string, JSONObject>();

	// Token: 0x04000D1E RID: 3358
	public JSONObject _skillJsonData;

	// Token: 0x04000D1F RID: 3359
	public JSONObject _ItemJsonData;

	// Token: 0x04000D20 RID: 3360
	public JSONObject _BuffJsonData;

	// Token: 0x04000D21 RID: 3361
	public List<jsonData.YSDictionary<string, JSONObject>> _AIJsonDate = new List<jsonData.YSDictionary<string, JSONObject>>();

	// Token: 0x04000D22 RID: 3362
	public Dictionary<int, JSONObject> AIJsonDate = new Dictionary<int, JSONObject>();

	// Token: 0x04000D23 RID: 3363
	public JSONObject NanZuJianBiao;

	// Token: 0x04000D24 RID: 3364
	public JSONObject NvZuJianBiao;

	// Token: 0x04000D25 RID: 3365
	public JSONObject TouXiangPianYi;

	// Token: 0x04000D26 RID: 3366
	public JSONObject StaticSkillJsonData;

	// Token: 0x04000D27 RID: 3367
	public JSONObject drawCardJsonData;

	// Token: 0x04000D28 RID: 3368
	public JSONObject MapRandomJsonData;

	// Token: 0x04000D29 RID: 3369
	public JSONObject DaDiTuYinCangJsonData;

	// Token: 0x04000D2A RID: 3370
	public JSONObject TaskJsonData;

	// Token: 0x04000D2B RID: 3371
	public JSONObject TaskInfoJsonData;

	// Token: 0x04000D2C RID: 3372
	public JSONObject ThreeSenceJsonData;

	// Token: 0x04000D2D RID: 3373
	public JSONObject AvatarJsonData;

	// Token: 0x04000D2E RID: 3374
	public JSONObject StrTextJsonData;

	// Token: 0x04000D2F RID: 3375
	public JSONObject TuJianChunWenBen;

	// Token: 0x04000D30 RID: 3376
	public JSONObject _firstNameJsonData;

	// Token: 0x04000D31 RID: 3377
	public JSONObject _LastNameJsonData;

	// Token: 0x04000D32 RID: 3378
	public JSONObject _LastWomenNameJsonData;

	// Token: 0x04000D33 RID: 3379
	public JSONObject _FaBaoFirstNameJsonData;

	// Token: 0x04000D34 RID: 3380
	public JSONObject _FaBaoLastNameJsonData;

	// Token: 0x04000D35 RID: 3381
	public JSONObject GuDingExchangeData;

	// Token: 0x04000D36 RID: 3382
	public JSONObject RandomExchangeData;

	// Token: 0x04000D37 RID: 3383
	public JSONObject DisableExchangeData;

	// Token: 0x04000D38 RID: 3384
	public JSONObject TipsExchangeData;

	// Token: 0x04000D39 RID: 3385
	public JSONObject ItemTypeExchangeData;

	// Token: 0x04000D3A RID: 3386
	public JSONObject LevelUpDataJsonData;

	// Token: 0x04000D3B RID: 3387
	public JSONObject BigMapLoadTalk;

	// Token: 0x04000D3C RID: 3388
	public JSONObject AvatarRandomJsonData;

	// Token: 0x04000D3D RID: 3389
	public JSONObject BackpackJsonData;

	// Token: 0x04000D3E RID: 3390
	public JSONObject AvatarBackpackJsonData;

	// Token: 0x04000D3F RID: 3391
	public JSONObject AvatarMoneyJsonData;

	// Token: 0x04000D40 RID: 3392
	public JSONObject DropTextJsonData;

	// Token: 0x04000D41 RID: 3393
	public JSONObject RunawayJsonData;

	// Token: 0x04000D42 RID: 3394
	public JSONObject BiguanJsonData;

	// Token: 0x04000D43 RID: 3395
	public JSONObject XinJinJsonData;

	// Token: 0x04000D44 RID: 3396
	public JSONObject XinJinGuanLianJsonData;

	// Token: 0x04000D45 RID: 3397
	public JSONObject DropInfoJsonData;

	// Token: 0x04000D46 RID: 3398
	public JSONObject SkillTextInfoJsonData;

	// Token: 0x04000D47 RID: 3399
	public JSONObject FightTypeInfoJsonData;

	// Token: 0x04000D48 RID: 3400
	public JSONObject StaticSkillTypeJsonData;

	// Token: 0x04000D49 RID: 3401
	public JSONObject FavorabilityInfoJsonData;

	// Token: 0x04000D4A RID: 3402
	public JSONObject FavorabilityAvatarInfoJsonData;

	// Token: 0x04000D4B RID: 3403
	public JSONObject QieCuoJsonData;

	// Token: 0x04000D4C RID: 3404
	public JSONObject DrawCardToLevelJsonData;

	// Token: 0x04000D4D RID: 3405
	public JSONObject StaticLVToLevelJsonData;

	// Token: 0x04000D4E RID: 3406
	public JSONObject AllMapCastTimeJsonData;

	// Token: 0x04000D4F RID: 3407
	public JSONObject SeaCastTimeJsonData;

	// Token: 0x04000D50 RID: 3408
	public JSONObject AllMapShiJianOptionJsonData;

	// Token: 0x04000D51 RID: 3409
	public JSONObject AllMapOptionJsonData;

	// Token: 0x04000D52 RID: 3410
	public JSONObject SuiJiTouXiangGeShuJsonData;

	// Token: 0x04000D53 RID: 3411
	public JSONObject SceneNameJsonData;

	// Token: 0x04000D54 RID: 3412
	public JSONObject helpJsonData;

	// Token: 0x04000D55 RID: 3413
	public JSONObject helpTextJsonData;

	// Token: 0x04000D56 RID: 3414
	public JSONObject NomelShopJsonData;

	// Token: 0x04000D57 RID: 3415
	public JSONObject HairRandomColorJsonData;

	// Token: 0x04000D58 RID: 3416
	public JSONObject MouthRandomColorJsonData;

	// Token: 0x04000D59 RID: 3417
	public JSONObject SaiHonRandomColorJsonData;

	// Token: 0x04000D5A RID: 3418
	public JSONObject WenShenRandomColorJsonData;

	// Token: 0x04000D5B RID: 3419
	public JSONObject YanZhuYanSeRandomColorJsonData;

	// Token: 0x04000D5C RID: 3420
	public JSONObject MianWenYanSeRandomColorJsonData;

	// Token: 0x04000D5D RID: 3421
	public JSONObject MeiMaoYanSeRandomColorJsonData;

	// Token: 0x04000D5E RID: 3422
	public JSONObject WuXianBiGuanJsonData;

	// Token: 0x04000D5F RID: 3423
	public JSONObject FuBenInfoJsonData;

	// Token: 0x04000D60 RID: 3424
	public JSONObject CreateAvatarJsonData;

	// Token: 0x04000D61 RID: 3425
	public JSONObject LinGenZiZhiJsonData;

	// Token: 0x04000D62 RID: 3426
	public JSONObject ChengHaoJsonData;

	// Token: 0x04000D63 RID: 3427
	public JSONObject TianFuDescJsonData;

	// Token: 0x04000D64 RID: 3428
	public JSONObject PaiMaiCanYuAvatar;

	// Token: 0x04000D65 RID: 3429
	public JSONObject PaiMaiAIJiaWei;

	// Token: 0x04000D66 RID: 3430
	public JSONObject PaiMaiCeLueSuiJiBiao;

	// Token: 0x04000D67 RID: 3431
	public JSONObject PaiMaiDuiHuaBiao;

	// Token: 0x04000D68 RID: 3432
	public JSONObject PaiMaiZhuChiBiao;

	// Token: 0x04000D69 RID: 3433
	public JSONObject PaiMaiMiaoShuBiao;

	// Token: 0x04000D6A RID: 3434
	public JSONObject PaiMaiBiao;

	// Token: 0x04000D6B RID: 3435
	public JSONObject JieDanBiao;

	// Token: 0x04000D6C RID: 3436
	public JSONObject YuanYingBiao;

	// Token: 0x04000D6D RID: 3437
	public JSONObject jiaoHuanShopGoods;

	// Token: 0x04000D6E RID: 3438
	public JSONObject LianDanDanFangBiao;

	// Token: 0x04000D6F RID: 3439
	public JSONObject LianDanItemLeiXin;

	// Token: 0x04000D70 RID: 3440
	public JSONObject LianDanSuccessItemLeiXin;

	// Token: 0x04000D71 RID: 3441
	public JSONObject DanduMiaoShu;

	// Token: 0x04000D72 RID: 3442
	public JSONObject CaiYaoShoYi;

	// Token: 0x04000D73 RID: 3443
	public JSONObject CaiYaoDiaoLuo;

	// Token: 0x04000D74 RID: 3444
	public JSONObject LiShiChuanWen;

	// Token: 0x04000D75 RID: 3445
	public JSONObject DongTaiChuanWenBaio;

	// Token: 0x04000D76 RID: 3446
	public JSONObject AllMapLuDainType;

	// Token: 0x04000D77 RID: 3447
	public JSONObject AllMapReset;

	// Token: 0x04000D78 RID: 3448
	public JSONObject StaticValueSay;

	// Token: 0x04000D79 RID: 3449
	public JSONObject ShiLiHaoGanDuName;

	// Token: 0x04000D7A RID: 3450
	public JSONObject AllMapCaiJiBiao;

	// Token: 0x04000D7B RID: 3451
	public JSONObject AllMapCaiJiMiaoShuBiao;

	// Token: 0x04000D7C RID: 3452
	public JSONObject AllMapCaiJiAddItemBiao;

	// Token: 0x04000D7D RID: 3453
	public JSONObject CreateAvatarMiaoShu;

	// Token: 0x04000D7E RID: 3454
	public JSONObject WuJiangBangDing;

	// Token: 0x04000D7F RID: 3455
	public JSONObject NTaskAllType;

	// Token: 0x04000D80 RID: 3456
	public JSONObject NTaskXiangXi;

	// Token: 0x04000D81 RID: 3457
	public JSONObject NTaskSuiJI;

	// Token: 0x04000D82 RID: 3458
	public JSONObject WuDaoJinJieJson;

	// Token: 0x04000D83 RID: 3459
	public JSONObject WuDaoJson;

	// Token: 0x04000D84 RID: 3460
	public JSONObject WuDaoAllTypeJson;

	// Token: 0x04000D85 RID: 3461
	public JSONObject WuDaoExBeiLuJson;

	// Token: 0x04000D86 RID: 3462
	public JSONObject NPCWuDaoJson;

	// Token: 0x04000D87 RID: 3463
	public JSONObject LingGuangJson;

	// Token: 0x04000D88 RID: 3464
	public JSONObject KillAvatarLingGuangJson;

	// Token: 0x04000D89 RID: 3465
	public JSONObject wupingfenlan;

	// Token: 0x04000D8A RID: 3466
	public JSONObject LianQiWuWeiBiao;

	// Token: 0x04000D8B RID: 3467
	public JSONObject CaiLiaoNengLiangBiao;

	// Token: 0x04000D8C RID: 3468
	public JSONObject LianQiHeCheng;

	// Token: 0x04000D8D RID: 3469
	public JSONObject LianQiEquipIconBiao;

	// Token: 0x04000D8E RID: 3470
	public JSONObject LianQiDuoDuanShangHaiBiao;

	// Token: 0x04000D8F RID: 3471
	public JSONObject LianQiJieSuoBiao;

	// Token: 0x04000D90 RID: 3472
	public JSONObject ChuanYingFuBiao;

	// Token: 0x04000D91 RID: 3473
	public JSONObject MenPaiFengLuBiao;

	// Token: 0x04000D92 RID: 3474
	public JSONObject ElderTaskItemType;

	// Token: 0x04000D93 RID: 3475
	public JSONObject ElderTaskDisableItem;

	// Token: 0x04000D94 RID: 3476
	public JSONObject ElderTaskItemCost;

	// Token: 0x04000D95 RID: 3477
	public JObject CaiLiaoShuXingBIAO;

	// Token: 0x04000D96 RID: 3478
	public JObject StaticNTaks;

	// Token: 0x04000D97 RID: 3479
	public JObject StaticNTaksTime;

	// Token: 0x04000D98 RID: 3480
	public JObject BadWord;

	// Token: 0x04000D99 RID: 3481
	public JObject BiGuanWuDao;

	// Token: 0x04000D9A RID: 3482
	public JObject ChengJiuJson;

	// Token: 0x04000D9B RID: 3483
	public JObject RandomMapType;

	// Token: 0x04000D9C RID: 3484
	public JObject RandomMapList;

	// Token: 0x04000D9D RID: 3485
	public JObject RandomMapEventList;

	// Token: 0x04000D9E RID: 3486
	public JObject RandomMapFirstName;

	// Token: 0x04000D9F RID: 3487
	public JObject ResetAvatarBackpackBanBen;

	// Token: 0x04000DA0 RID: 3488
	public JObject EndlessSeaRandomData;

	// Token: 0x04000DA1 RID: 3489
	public JObject EndlessSeaType;

	// Token: 0x04000DA2 RID: 3490
	public JObject EndlessSeaData;

	// Token: 0x04000DA3 RID: 3491
	public JObject EndlessSeaNPCData;

	// Token: 0x04000DA4 RID: 3492
	public JObject EndlessSeaNPCGouChengData;

	// Token: 0x04000DA5 RID: 3493
	public JObject EndlessSeaSafeLvData;

	// Token: 0x04000DA6 RID: 3494
	public JObject EndlessSeaLinQiSafeLvData;

	// Token: 0x04000DA7 RID: 3495
	public JObject EndlessSeaLuanLIuXinZhuang;

	// Token: 0x04000DA8 RID: 3496
	public JObject EndlessSeaHaiYuData;

	// Token: 0x04000DA9 RID: 3497
	public JObject EndlessSeaAIChuFa;

	// Token: 0x04000DAA RID: 3498
	public JObject EndlessSeaLuanLiuRandom;

	// Token: 0x04000DAB RID: 3499
	public JObject EndlessSeaLuanLiuRandomMap;

	// Token: 0x04000DAC RID: 3500
	public JObject EndlessSeaShiYe;

	// Token: 0x04000DAD RID: 3501
	public JObject LingZhouPinJie;

	// Token: 0x04000DAE RID: 3502
	public JObject NPCInterestingItem;

	// Token: 0x04000DAF RID: 3503
	public JObject AllItemLeiXin;

	// Token: 0x04000DB0 RID: 3504
	public JObject SeaStaticIsland;

	// Token: 0x04000DB1 RID: 3505
	public JObject LianQiEquipType;

	// Token: 0x04000DB2 RID: 3506
	public JObject LianQiWuQiQuality;

	// Token: 0x04000DB3 RID: 3507
	public JSONObject LianQiJieSuanBiao;

	// Token: 0x04000DB4 RID: 3508
	public JSONObject LianQiShuXinLeiBie;

	// Token: 0x04000DB5 RID: 3509
	public JSONObject LianQiLingWenBiao;

	// Token: 0x04000DB6 RID: 3510
	[Obsolete]
	public JSONObject heroFaceJsonData;

	// Token: 0x04000DB7 RID: 3511
	[Obsolete]
	public JSONObject heroFaceByIDJsonData;

	// Token: 0x04000DB8 RID: 3512
	[Obsolete]
	public JSONObject heroJsonData;

	// Token: 0x04000DB9 RID: 3513
	[Obsolete]
	public JSONObject PlayerGoodsSJsonData;

	// Token: 0x04000DBA RID: 3514
	[Obsolete]
	public JSONObject CheckInJsonData;

	// Token: 0x04000DBB RID: 3515
	[Obsolete]
	public JSONObject ItemGoodSeid1JsonData;

	// Token: 0x04000DBC RID: 3516
	[Obsolete]
	public JSONObject TalkingJsonData;

	// Token: 0x04000DBD RID: 3517
	[Obsolete]
	public JSONObject MessageJsonData;

	// Token: 0x04000DBE RID: 3518
	public JSONObject NPCChuShiHuaDate;

	// Token: 0x04000DBF RID: 3519
	public JSONObject NPCLeiXingDate;

	// Token: 0x04000DC0 RID: 3520
	public JSONObject NPCChengHaoData;

	// Token: 0x04000DC1 RID: 3521
	public JSONObject NPCChuShiShuZiDate;

	// Token: 0x04000DC2 RID: 3522
	public JSONObject NPCImportantDate;

	// Token: 0x04000DC3 RID: 3523
	public JSONObject NPCActionDate;

	// Token: 0x04000DC4 RID: 3524
	public JSONObject NPCActionPanDingDate;

	// Token: 0x04000DC5 RID: 3525
	public JSONObject NPCTagDate;

	// Token: 0x04000DC6 RID: 3526
	public JSONObject NPCTuPuoDate;

	// Token: 0x04000DC7 RID: 3527
	public JSONObject NpcFuBenMapBingDate;

	// Token: 0x04000DC8 RID: 3528
	public JSONObject NpcThreeMapBingDate;

	// Token: 0x04000DC9 RID: 3529
	public JSONObject NpcBigMapBingDate;

	// Token: 0x04000DCA RID: 3530
	public JSONObject NpcYaoShouDrop;

	// Token: 0x04000DCB RID: 3531
	public JSONObject NpcLevelShouYiDate;

	// Token: 0x04000DCC RID: 3532
	public JSONObject NpcXingGeDate;

	// Token: 0x04000DCD RID: 3533
	public JSONObject NpcYiWaiDeathDate;

	// Token: 0x04000DCE RID: 3534
	public JSONObject NpcQiYuDate;

	// Token: 0x04000DCF RID: 3535
	public JSONObject NpcBeiBaoTypeData;

	// Token: 0x04000DD0 RID: 3536
	public JSONObject NpcShiJianData;

	// Token: 0x04000DD1 RID: 3537
	public JSONObject NpcStatusDate;

	// Token: 0x04000DD2 RID: 3538
	public JSONObject NpcPaiMaiData;

	// Token: 0x04000DD3 RID: 3539
	public JSONObject NpcImprotantPanDingData;

	// Token: 0x04000DD4 RID: 3540
	public JSONObject NpcHaoGanDuData;

	// Token: 0x04000DD5 RID: 3541
	public JSONObject NpcCreateData;

	// Token: 0x04000DD6 RID: 3542
	public JSONObject NpcJinHuoData;

	// Token: 0x04000DD7 RID: 3543
	public JSONObject NpcImprotantEventData;

	// Token: 0x04000DD8 RID: 3544
	public JSONObject NpcHaiShangCreateData;

	// Token: 0x04000DD9 RID: 3545
	public JSONObject NpcTalkShouCiJiaoTanData;

	// Token: 0x04000DDA RID: 3546
	public JSONObject NpcTalkHouXuJiaoTanData;

	// Token: 0x04000DDB RID: 3547
	public JSONObject NpcTalkQiTaJiaoHuData;

	// Token: 0x04000DDC RID: 3548
	public JSONObject NpcBiaoBaiTiKuData;

	// Token: 0x04000DDD RID: 3549
	public JSONObject NpcBiaoBaiTiWenData;

	// Token: 0x04000DDE RID: 3550
	public JSONObject NpcTalkGuanYuTuPoData;

	// Token: 0x04000DDF RID: 3551
	public JSONObject NpcQingJiaoXiaoHaoData;

	// Token: 0x04000DE0 RID: 3552
	public JSONObject NpcQingJiaoItemData;

	// Token: 0x04000DE1 RID: 3553
	public JSONObject NpcWuDaoChiData;

	// Token: 0x04000DE2 RID: 3554
	public JSONObject CyRandomTaskData;

	// Token: 0x04000DE3 RID: 3555
	public JSONObject CyRandomTaskFailData;

	// Token: 0x04000DE4 RID: 3556
	public JSONObject NewTaskMagData;

	// Token: 0x04000DE5 RID: 3557
	public JSONObject FightAIData;

	// Token: 0x04000DE6 RID: 3558
	public JSONObject LunDaoStateData;

	// Token: 0x04000DE7 RID: 3559
	public JSONObject LunDaoSayData;

	// Token: 0x04000DE8 RID: 3560
	public JSONObject LunDaoShouYiData;

	// Token: 0x04000DE9 RID: 3561
	public JSONObject WuDaoZhiData;

	// Token: 0x04000DEA RID: 3562
	public JSONObject LunDaoSiXuData;

	// Token: 0x04000DEB RID: 3563
	public JSONObject LingGanMaxData;

	// Token: 0x04000DEC RID: 3564
	public JSONObject LingGanLevelData;

	// Token: 0x04000DED RID: 3565
	public JSONObject WuDaoZhiJiaCheng;

	// Token: 0x04000DEE RID: 3566
	public JSONObject ShengWangLevelData;

	// Token: 0x04000DEF RID: 3567
	public JSONObject DiYuShengWangData;

	// Token: 0x04000DF0 RID: 3568
	public JSONObject ShangJinPingFenData;

	// Token: 0x04000DF1 RID: 3569
	public JSONObject ShengWangShangJinData;

	// Token: 0x04000DF2 RID: 3570
	public JSONObject XuanShangMiaoShuData;

	// Token: 0x04000DF3 RID: 3571
	public JSONObject ShiLiShenFenData;

	// Token: 0x04000DF4 RID: 3572
	public JSONObject CyShiLiNameData;

	// Token: 0x04000DF5 RID: 3573
	public JSONObject CyTeShuNpc;

	// Token: 0x04000DF6 RID: 3574
	public JSONObject ScenePriceData;

	// Token: 0x04000DF7 RID: 3575
	public JSONObject CyZiDuanData;

	// Token: 0x04000DF8 RID: 3576
	public JSONObject CyPlayeQuestionData;

	// Token: 0x04000DF9 RID: 3577
	public JSONObject CyNpcAnswerData;

	// Token: 0x04000DFA RID: 3578
	public JSONObject CyNpcDuiBaiData;

	// Token: 0x04000DFB RID: 3579
	public JSONObject CyNpcSendData;

	// Token: 0x04000DFC RID: 3580
	public JSONObject RenWuDaLeiYouXianJi;

	// Token: 0x04000DFD RID: 3581
	public JSONObject LunDaoReduceData;

	// Token: 0x04000DFE RID: 3582
	public JSONObject LingGanTimeMaxData;

	// Token: 0x04000DFF RID: 3583
	public JSONObject ShuangXiuMiShu;

	// Token: 0x04000E00 RID: 3584
	public JSONObject ShuangXiuJingYuanJiaZhi;

	// Token: 0x04000E01 RID: 3585
	public JSONObject ShuangXiuLianHuaSuDu;

	// Token: 0x04000E02 RID: 3586
	public JSONObject ShuangXiuJingJieBeiLv;

	// Token: 0x04000E03 RID: 3587
	public JSONObject DFLingYanLevel;

	// Token: 0x04000E04 RID: 3588
	public JSONObject DFZhenYanLevel;

	// Token: 0x04000E05 RID: 3589
	public JSONObject DFBuKeZhongZhi;

	// Token: 0x04000E06 RID: 3590
	public JSONObject SeaHaiYuJiZhiShuaXin;

	// Token: 0x04000E07 RID: 3591
	public JSONObject SeaJiZhiID;

	// Token: 0x04000E08 RID: 3592
	public JSONObject SeaJiZhiXingXiang;

	// Token: 0x04000E09 RID: 3593
	public JSONObject SeaHaiYuTanSuo;

	// Token: 0x04000E0A RID: 3594
	public JSONObject MapIndexData;

	// Token: 0x04000E0B RID: 3595
	public JSONObject GaoShiLeiXing;

	// Token: 0x04000E0C RID: 3596
	public JSONObject GaoShi;

	// Token: 0x04000E0D RID: 3597
	public JSONObject ZhuChengRenWu;

	// Token: 0x04000E0E RID: 3598
	public JSONObject PaiMaiPanDing;

	// Token: 0x04000E0F RID: 3599
	public JSONObject PaiMaiChuJia;

	// Token: 0x04000E10 RID: 3600
	public JSONObject PaiMaiCommandTips;

	// Token: 0x04000E11 RID: 3601
	public JSONObject PaiMaiDuiHuaAI;

	// Token: 0x04000E12 RID: 3602
	public JSONObject PaiMaiChuJiaAI;

	// Token: 0x04000E13 RID: 3603
	public JSONObject PaiMaiOldAvatar;

	// Token: 0x04000E14 RID: 3604
	public JSONObject PaiMaiNpcAddPriceSay;

	// Token: 0x04000E15 RID: 3605
	public JSONObject ChuanWenTypeData;

	// Token: 0x04000E16 RID: 3606
	public JSONObject LingHeCaiJi;

	// Token: 0x04000E17 RID: 3607
	public JSONObject LingMaiPinJie;

	// Token: 0x04000E18 RID: 3608
	public JSONObject HuaShenData;

	// Token: 0x04000E19 RID: 3609
	public JSONObject TianJieMiShuData;

	// Token: 0x04000E1A RID: 3610
	public JSONObject TianJieLeiJieType;

	// Token: 0x04000E1B RID: 3611
	public JSONObject TianJieLeiJieShangHai;

	// Token: 0x04000E1C RID: 3612
	public JSONObject TianJiDaBi;

	// Token: 0x04000E1D RID: 3613
	public JSONObject TianJiDaBiGongFangKeZhi;

	// Token: 0x04000E1E RID: 3614
	public JSONObject TianJiDaBiReward;

	// Token: 0x04000E1F RID: 3615
	public JSONObject ShengPing;

	// Token: 0x04000E20 RID: 3616
	public JSONObject JianLingXianSuo;

	// Token: 0x04000E21 RID: 3617
	public JSONObject JianLingZhenXiang;

	// Token: 0x04000E22 RID: 3618
	public JSONObject JianLingQingJiao;

	// Token: 0x04000E23 RID: 3619
	public bool IsResetAvatarFace;

	// Token: 0x04000E24 RID: 3620
	public ItemDatabase playerDatabase;

	// Token: 0x04000E25 RID: 3621
	public JSONObject[] SkillSeidJsonData = new JSONObject[500];

	// Token: 0x04000E26 RID: 3622
	public JSONObject[] BuffSeidJsonData = new JSONObject[500];

	// Token: 0x04000E27 RID: 3623
	public JSONObject[] VersionJsonData = new JSONObject[500];

	// Token: 0x04000E28 RID: 3624
	public JSONObject[] StaticSkillSeidJsonData = new JSONObject[500];

	// Token: 0x04000E29 RID: 3625
	public JSONObject[] WuDaoSeidJsonData = new JSONObject[500];

	// Token: 0x04000E2A RID: 3626
	public JSONObject[] ItemsSeidJsonData = new JSONObject[500];

	// Token: 0x04000E2B RID: 3627
	public JSONObject[] EquipSeidJsonData = new JSONObject[500];

	// Token: 0x04000E2C RID: 3628
	public JSONObject[] CrateAvatarSeidJsonData = new JSONObject[500];

	// Token: 0x04000E2D RID: 3629
	public JSONObject[] JieDanSeidJsonData = new JSONObject[500];

	// Token: 0x04000E2E RID: 3630
	public Dictionary<int, List<JSONObject>> FuBenJsonData = new Dictionary<int, List<JSONObject>>();

	// Token: 0x04000E2F RID: 3631
	public List<int> RandomList = new List<int>();

	// Token: 0x04000E30 RID: 3632
	public int randomListIndex;

	// Token: 0x04000E31 RID: 3633
	public RandomFace body;

	// Token: 0x04000E32 RID: 3634
	public RandomFace eye;

	// Token: 0x04000E33 RID: 3635
	public RandomFace eyebrow;

	// Token: 0x04000E34 RID: 3636
	public RandomFace face;

	// Token: 0x04000E35 RID: 3637
	public RandomFace Facefold;

	// Token: 0x04000E36 RID: 3638
	public RandomFace hair;

	// Token: 0x04000E37 RID: 3639
	public RandomFace hair2;

	// Token: 0x04000E38 RID: 3640
	public RandomFace mouth;

	// Token: 0x04000E39 RID: 3641
	public RandomFace mustache;

	// Token: 0x04000E3A RID: 3642
	public RandomFace nose;

	// Token: 0x04000E3B RID: 3643
	public RandomFace ornament;

	// Token: 0x04000E3C RID: 3644
	public GameObject TextError;

	// Token: 0x04000E3D RID: 3645
	public GameObject SkillHint;

	// Token: 0x04000E3E RID: 3646
	public bool reloadRandomAvatarFace;

	// Token: 0x04000E3F RID: 3647
	public static bool showGongGao = true;

	// Token: 0x04000E40 RID: 3648
	public JSONObject ItemFlagData;

	// Token: 0x04000E41 RID: 3649
	private static int rDieZiNameCount = 0;

	// Token: 0x020012BF RID: 4799
	public class YSDictionary<TKey, TValue> : Dictionary<TKey, TValue>
	{
		// Token: 0x06007A6D RID: 31341 RVA: 0x002BCE3C File Offset: 0x002BB03C
		public bool HasField(TKey key)
		{
			return base.ContainsKey(key);
		}
	}

	// Token: 0x020012C0 RID: 4800
	public enum SeidCount
	{
		// Token: 0x04006689 RID: 26249
		buffCount = 500,
		// Token: 0x0400668A RID: 26250
		skillCount = 500,
		// Token: 0x0400668B RID: 26251
		StaticSkillCount = 500
	}

	// Token: 0x020012C1 RID: 4801
	public enum RandomFaceType
	{
		// Token: 0x0400668D RID: 26253
		body,
		// Token: 0x0400668E RID: 26254
		eye,
		// Token: 0x0400668F RID: 26255
		eyebrow,
		// Token: 0x04006690 RID: 26256
		face,
		// Token: 0x04006691 RID: 26257
		Facefold,
		// Token: 0x04006692 RID: 26258
		hair,
		// Token: 0x04006693 RID: 26259
		mouth,
		// Token: 0x04006694 RID: 26260
		mustache,
		// Token: 0x04006695 RID: 26261
		nose,
		// Token: 0x04006696 RID: 26262
		ornament
	}

	// Token: 0x020012C2 RID: 4802
	public enum InventoryNUM
	{
		// Token: 0x04006698 RID: 26264
		Shop = 9,
		// Token: 0x04006699 RID: 26265
		Max = 24,
		// Token: 0x0400669A RID: 26266
		EXIventoryNum = 34,
		// Token: 0x0400669B RID: 26267
		LinQiEXIventoryNum,
		// Token: 0x0400669C RID: 26268
		SkillMax = 30,
		// Token: 0x0400669D RID: 26269
		FightEat = 32,
		// Token: 0x0400669E RID: 26270
		PaiMai = 12,
		// Token: 0x0400669F RID: 26271
		PaiMaiPlayer = 25,
		// Token: 0x040066A0 RID: 26272
		PaiMaiXianShi = 1,
		// Token: 0x040066A1 RID: 26273
		ShopEX = 10,
		// Token: 0x040066A2 RID: 26274
		LianDan = 29,
		// Token: 0x040066A3 RID: 26275
		LianDanDanLu = 18,
		// Token: 0x040066A4 RID: 26276
		LianDanFinish = 6,
		// Token: 0x040066A5 RID: 26277
		CaijiTeChan = 8,
		// Token: 0x040066A6 RID: 26278
		CaijiDiaoLuo = 2,
		// Token: 0x040066A7 RID: 26279
		FaceRandomTime = 500,
		// Token: 0x040066A8 RID: 26280
		NewLianDan = 37,
		// Token: 0x040066A9 RID: 26281
		NewJiaoYiNum = 15
	}
}
