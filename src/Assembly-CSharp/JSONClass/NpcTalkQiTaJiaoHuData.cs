using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcTalkQiTaJiaoHuData : IJSONClass
{
	public static Dictionary<int, NpcTalkQiTaJiaoHuData> DataDict = new Dictionary<int, NpcTalkQiTaJiaoHuData>();

	public static List<NpcTalkQiTaJiaoHuData> DataList = new List<NpcTalkQiTaJiaoHuData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int JingJie;

	public int XingGe;

	public string TalkTanChaShiBai;

	public string TalkTanChaZhanDou;

	public string TalkJieSha;

	public string TalkJieShouQieCuo;

	public string TalkQieCuoShengLi;

	public string TalkQieCuoShiBai;

	public string TalkZengLiJiXu;

	public string TalkZengLiChengGong;

	public string TalkZengLiShiBaigao;

	public string TalkZengLiShiBaidi;

	public string TalkZengLiLaJi;

	public string TalkNPCWeiXie;

	public string TalkWeiXieChengGong1;

	public string TalkWeiXieChengGong2;

	public string TalkWeiXieChengGong3;

	public string TalkWeiXieChengGong4;

	public string TalkWeiXieShiBai1;

	public string TalkWeiXieShiBai2;

	public string TalkWeiXieShiBai3;

	public string TalkWeiXieShiBai4;

	public string TalkWeiXieShiBai5;

	public string TalkWeiXieShiBai6;

	public string TalkMenaceProbSuc;

	public string TalkMenaceProbFailed1;

	public string TalkMenaceProbFailed2;

	public string TalkQingJiaoChengGong;

	public string TalkQingJiaoShiBaiQF;

	public string TalkQingJiaoShiBaiSW;

	public string TalkYaoQingLunDao;

	public string TalkJieShouLunDao;

	public string TalkLunDaoCGPuTong;

	public string TalkLunDaoChengGong;

	public string TalkLunDaoSBPuTong;

	public string TalkLunDaoShiBai;

	public string TalkJuJueLunDao;

	public string TalkJieYaoQingQieCuo;

	public string TalkJuJueDaTing;

	public string TalkJieShouHaoYou;

	public string TalkTiWen;

	public string TalkBiaoBaiCG;

	public string TalkBiaoBaiCG2;

	public string TalkBBShiBai1;

	public string TalkBBShiBai2;

	public string TalkQueRen;

	public string TalkWanXiao;

	public string TalkShouYao;

	public string TalkDaoLv;

	public string TalkJuJueJiaoYi;

	public string TalkLingMaiF;

	public string TalkLingMaiZDF;

	public string TalkLingMaiZDNF;

	public string TalkDaBiQianD;

	public string TalkDaBiQianZ;

	public string TalkDaBiQianG;

	public string TalkDaBiSheng;

	public string TalkDaBiBai;

	public string TalkDaBiQianShiFu;

	public string TalkDaBiHouShiFuS;

	public string TalkDaBiHouShiFuB;

	public string TalkDaBiQianDaoLv;

	public string TalkDaBiHouDaoLvS;

	public string TalkDaBiHouDaoLvB;

	public string TalkChengHu;

	public string TalkYJMMPBZQ;

	public string TalkNZGJBZQ;

	public string TalkNZFSGL;

	public string TalkLJQDY;

	public string TalkLJQDL;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcTalkQiTaJiaoHuData.list)
		{
			try
			{
				NpcTalkQiTaJiaoHuData npcTalkQiTaJiaoHuData = new NpcTalkQiTaJiaoHuData();
				npcTalkQiTaJiaoHuData.id = item["id"].I;
				npcTalkQiTaJiaoHuData.JingJie = item["JingJie"].I;
				npcTalkQiTaJiaoHuData.XingGe = item["XingGe"].I;
				npcTalkQiTaJiaoHuData.TalkTanChaShiBai = item["TalkTanChaShiBai"].Str;
				npcTalkQiTaJiaoHuData.TalkTanChaZhanDou = item["TalkTanChaZhanDou"].Str;
				npcTalkQiTaJiaoHuData.TalkJieSha = item["TalkJieSha"].Str;
				npcTalkQiTaJiaoHuData.TalkJieShouQieCuo = item["TalkJieShouQieCuo"].Str;
				npcTalkQiTaJiaoHuData.TalkQieCuoShengLi = item["TalkQieCuoShengLi"].Str;
				npcTalkQiTaJiaoHuData.TalkQieCuoShiBai = item["TalkQieCuoShiBai"].Str;
				npcTalkQiTaJiaoHuData.TalkZengLiJiXu = item["TalkZengLiJiXu"].Str;
				npcTalkQiTaJiaoHuData.TalkZengLiChengGong = item["TalkZengLiChengGong"].Str;
				npcTalkQiTaJiaoHuData.TalkZengLiShiBaigao = item["TalkZengLiShiBaigao"].Str;
				npcTalkQiTaJiaoHuData.TalkZengLiShiBaidi = item["TalkZengLiShiBaidi"].Str;
				npcTalkQiTaJiaoHuData.TalkZengLiLaJi = item["TalkZengLiLaJi"].Str;
				npcTalkQiTaJiaoHuData.TalkNPCWeiXie = item["TalkNPCWeiXie"].Str;
				npcTalkQiTaJiaoHuData.TalkWeiXieChengGong1 = item["TalkWeiXieChengGong1"].Str;
				npcTalkQiTaJiaoHuData.TalkWeiXieChengGong2 = item["TalkWeiXieChengGong2"].Str;
				npcTalkQiTaJiaoHuData.TalkWeiXieChengGong3 = item["TalkWeiXieChengGong3"].Str;
				npcTalkQiTaJiaoHuData.TalkWeiXieChengGong4 = item["TalkWeiXieChengGong4"].Str;
				npcTalkQiTaJiaoHuData.TalkWeiXieShiBai1 = item["TalkWeiXieShiBai1"].Str;
				npcTalkQiTaJiaoHuData.TalkWeiXieShiBai2 = item["TalkWeiXieShiBai2"].Str;
				npcTalkQiTaJiaoHuData.TalkWeiXieShiBai3 = item["TalkWeiXieShiBai3"].Str;
				npcTalkQiTaJiaoHuData.TalkWeiXieShiBai4 = item["TalkWeiXieShiBai4"].Str;
				npcTalkQiTaJiaoHuData.TalkWeiXieShiBai5 = item["TalkWeiXieShiBai5"].Str;
				npcTalkQiTaJiaoHuData.TalkWeiXieShiBai6 = item["TalkWeiXieShiBai6"].Str;
				npcTalkQiTaJiaoHuData.TalkMenaceProbSuc = item["TalkMenaceProbSuc"].Str;
				npcTalkQiTaJiaoHuData.TalkMenaceProbFailed1 = item["TalkMenaceProbFailed1"].Str;
				npcTalkQiTaJiaoHuData.TalkMenaceProbFailed2 = item["TalkMenaceProbFailed2"].Str;
				npcTalkQiTaJiaoHuData.TalkQingJiaoChengGong = item["TalkQingJiaoChengGong"].Str;
				npcTalkQiTaJiaoHuData.TalkQingJiaoShiBaiQF = item["TalkQingJiaoShiBaiQF"].Str;
				npcTalkQiTaJiaoHuData.TalkQingJiaoShiBaiSW = item["TalkQingJiaoShiBaiSW"].Str;
				npcTalkQiTaJiaoHuData.TalkYaoQingLunDao = item["TalkYaoQingLunDao"].Str;
				npcTalkQiTaJiaoHuData.TalkJieShouLunDao = item["TalkJieShouLunDao"].Str;
				npcTalkQiTaJiaoHuData.TalkLunDaoCGPuTong = item["TalkLunDaoCGPuTong"].Str;
				npcTalkQiTaJiaoHuData.TalkLunDaoChengGong = item["TalkLunDaoChengGong"].Str;
				npcTalkQiTaJiaoHuData.TalkLunDaoSBPuTong = item["TalkLunDaoSBPuTong"].Str;
				npcTalkQiTaJiaoHuData.TalkLunDaoShiBai = item["TalkLunDaoShiBai"].Str;
				npcTalkQiTaJiaoHuData.TalkJuJueLunDao = item["TalkJuJueLunDao"].Str;
				npcTalkQiTaJiaoHuData.TalkJieYaoQingQieCuo = item["TalkJieYaoQingQieCuo"].Str;
				npcTalkQiTaJiaoHuData.TalkJuJueDaTing = item["TalkJuJueDaTing"].Str;
				npcTalkQiTaJiaoHuData.TalkJieShouHaoYou = item["TalkJieShouHaoYou"].Str;
				npcTalkQiTaJiaoHuData.TalkTiWen = item["TalkTiWen"].Str;
				npcTalkQiTaJiaoHuData.TalkBiaoBaiCG = item["TalkBiaoBaiCG"].Str;
				npcTalkQiTaJiaoHuData.TalkBiaoBaiCG2 = item["TalkBiaoBaiCG2"].Str;
				npcTalkQiTaJiaoHuData.TalkBBShiBai1 = item["TalkBBShiBai1"].Str;
				npcTalkQiTaJiaoHuData.TalkBBShiBai2 = item["TalkBBShiBai2"].Str;
				npcTalkQiTaJiaoHuData.TalkQueRen = item["TalkQueRen"].Str;
				npcTalkQiTaJiaoHuData.TalkWanXiao = item["TalkWanXiao"].Str;
				npcTalkQiTaJiaoHuData.TalkShouYao = item["TalkShouYao"].Str;
				npcTalkQiTaJiaoHuData.TalkDaoLv = item["TalkDaoLv"].Str;
				npcTalkQiTaJiaoHuData.TalkJuJueJiaoYi = item["TalkJuJueJiaoYi"].Str;
				npcTalkQiTaJiaoHuData.TalkLingMaiF = item["TalkLingMaiF"].Str;
				npcTalkQiTaJiaoHuData.TalkLingMaiZDF = item["TalkLingMaiZDF"].Str;
				npcTalkQiTaJiaoHuData.TalkLingMaiZDNF = item["TalkLingMaiZDNF"].Str;
				npcTalkQiTaJiaoHuData.TalkDaBiQianD = item["TalkDaBiQianD"].Str;
				npcTalkQiTaJiaoHuData.TalkDaBiQianZ = item["TalkDaBiQianZ"].Str;
				npcTalkQiTaJiaoHuData.TalkDaBiQianG = item["TalkDaBiQianG"].Str;
				npcTalkQiTaJiaoHuData.TalkDaBiSheng = item["TalkDaBiSheng"].Str;
				npcTalkQiTaJiaoHuData.TalkDaBiBai = item["TalkDaBiBai"].Str;
				npcTalkQiTaJiaoHuData.TalkDaBiQianShiFu = item["TalkDaBiQianShiFu"].Str;
				npcTalkQiTaJiaoHuData.TalkDaBiHouShiFuS = item["TalkDaBiHouShiFuS"].Str;
				npcTalkQiTaJiaoHuData.TalkDaBiHouShiFuB = item["TalkDaBiHouShiFuB"].Str;
				npcTalkQiTaJiaoHuData.TalkDaBiQianDaoLv = item["TalkDaBiQianDaoLv"].Str;
				npcTalkQiTaJiaoHuData.TalkDaBiHouDaoLvS = item["TalkDaBiHouDaoLvS"].Str;
				npcTalkQiTaJiaoHuData.TalkDaBiHouDaoLvB = item["TalkDaBiHouDaoLvB"].Str;
				npcTalkQiTaJiaoHuData.TalkChengHu = item["TalkChengHu"].Str;
				npcTalkQiTaJiaoHuData.TalkYJMMPBZQ = item["TalkYJMMPBZQ"].Str;
				npcTalkQiTaJiaoHuData.TalkNZGJBZQ = item["TalkNZGJBZQ"].Str;
				npcTalkQiTaJiaoHuData.TalkNZFSGL = item["TalkNZFSGL"].Str;
				npcTalkQiTaJiaoHuData.TalkLJQDY = item["TalkLJQDY"].Str;
				npcTalkQiTaJiaoHuData.TalkLJQDL = item["TalkLJQDL"].Str;
				if (DataDict.ContainsKey(npcTalkQiTaJiaoHuData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcTalkQiTaJiaoHuData.DataDict添加数据时出现重复的键，Key:{npcTalkQiTaJiaoHuData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcTalkQiTaJiaoHuData.id, npcTalkQiTaJiaoHuData);
				DataList.Add(npcTalkQiTaJiaoHuData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcTalkQiTaJiaoHuData.DataDict添加数据时出现异常，已跳过，请检查配表");
				PreloadManager.LogException($"异常信息:\n{arg}");
				PreloadManager.LogException($"数据序列化:\n{item}");
			}
		}
		if (OnInitFinishAction != null)
		{
			OnInitFinishAction();
		}
	}

	private static void OnInitFinish()
	{
	}
}
