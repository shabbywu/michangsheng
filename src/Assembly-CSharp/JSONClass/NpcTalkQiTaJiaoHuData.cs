using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C3E RID: 3134
	public class NpcTalkQiTaJiaoHuData : IJSONClass
	{
		// Token: 0x06004C61 RID: 19553 RVA: 0x002040C0 File Offset: 0x002022C0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcTalkQiTaJiaoHuData.list)
			{
				try
				{
					NpcTalkQiTaJiaoHuData npcTalkQiTaJiaoHuData = new NpcTalkQiTaJiaoHuData();
					npcTalkQiTaJiaoHuData.id = jsonobject["id"].I;
					npcTalkQiTaJiaoHuData.JingJie = jsonobject["JingJie"].I;
					npcTalkQiTaJiaoHuData.XingGe = jsonobject["XingGe"].I;
					npcTalkQiTaJiaoHuData.TalkTanChaShiBai = jsonobject["TalkTanChaShiBai"].Str;
					npcTalkQiTaJiaoHuData.TalkTanChaZhanDou = jsonobject["TalkTanChaZhanDou"].Str;
					npcTalkQiTaJiaoHuData.TalkJieSha = jsonobject["TalkJieSha"].Str;
					npcTalkQiTaJiaoHuData.TalkJieShouQieCuo = jsonobject["TalkJieShouQieCuo"].Str;
					npcTalkQiTaJiaoHuData.TalkQieCuoShengLi = jsonobject["TalkQieCuoShengLi"].Str;
					npcTalkQiTaJiaoHuData.TalkQieCuoShiBai = jsonobject["TalkQieCuoShiBai"].Str;
					npcTalkQiTaJiaoHuData.TalkZengLiJiXu = jsonobject["TalkZengLiJiXu"].Str;
					npcTalkQiTaJiaoHuData.TalkZengLiChengGong = jsonobject["TalkZengLiChengGong"].Str;
					npcTalkQiTaJiaoHuData.TalkZengLiShiBaigao = jsonobject["TalkZengLiShiBaigao"].Str;
					npcTalkQiTaJiaoHuData.TalkZengLiShiBaidi = jsonobject["TalkZengLiShiBaidi"].Str;
					npcTalkQiTaJiaoHuData.TalkZengLiLaJi = jsonobject["TalkZengLiLaJi"].Str;
					npcTalkQiTaJiaoHuData.TalkNPCWeiXie = jsonobject["TalkNPCWeiXie"].Str;
					npcTalkQiTaJiaoHuData.TalkWeiXieChengGong1 = jsonobject["TalkWeiXieChengGong1"].Str;
					npcTalkQiTaJiaoHuData.TalkWeiXieChengGong2 = jsonobject["TalkWeiXieChengGong2"].Str;
					npcTalkQiTaJiaoHuData.TalkWeiXieChengGong3 = jsonobject["TalkWeiXieChengGong3"].Str;
					npcTalkQiTaJiaoHuData.TalkWeiXieChengGong4 = jsonobject["TalkWeiXieChengGong4"].Str;
					npcTalkQiTaJiaoHuData.TalkWeiXieShiBai1 = jsonobject["TalkWeiXieShiBai1"].Str;
					npcTalkQiTaJiaoHuData.TalkWeiXieShiBai2 = jsonobject["TalkWeiXieShiBai2"].Str;
					npcTalkQiTaJiaoHuData.TalkWeiXieShiBai3 = jsonobject["TalkWeiXieShiBai3"].Str;
					npcTalkQiTaJiaoHuData.TalkWeiXieShiBai4 = jsonobject["TalkWeiXieShiBai4"].Str;
					npcTalkQiTaJiaoHuData.TalkWeiXieShiBai5 = jsonobject["TalkWeiXieShiBai5"].Str;
					npcTalkQiTaJiaoHuData.TalkWeiXieShiBai6 = jsonobject["TalkWeiXieShiBai6"].Str;
					npcTalkQiTaJiaoHuData.TalkMenaceProbSuc = jsonobject["TalkMenaceProbSuc"].Str;
					npcTalkQiTaJiaoHuData.TalkMenaceProbFailed1 = jsonobject["TalkMenaceProbFailed1"].Str;
					npcTalkQiTaJiaoHuData.TalkMenaceProbFailed2 = jsonobject["TalkMenaceProbFailed2"].Str;
					npcTalkQiTaJiaoHuData.TalkQingJiaoChengGong = jsonobject["TalkQingJiaoChengGong"].Str;
					npcTalkQiTaJiaoHuData.TalkQingJiaoShiBaiQF = jsonobject["TalkQingJiaoShiBaiQF"].Str;
					npcTalkQiTaJiaoHuData.TalkQingJiaoShiBaiSW = jsonobject["TalkQingJiaoShiBaiSW"].Str;
					npcTalkQiTaJiaoHuData.TalkYaoQingLunDao = jsonobject["TalkYaoQingLunDao"].Str;
					npcTalkQiTaJiaoHuData.TalkJieShouLunDao = jsonobject["TalkJieShouLunDao"].Str;
					npcTalkQiTaJiaoHuData.TalkLunDaoCGPuTong = jsonobject["TalkLunDaoCGPuTong"].Str;
					npcTalkQiTaJiaoHuData.TalkLunDaoChengGong = jsonobject["TalkLunDaoChengGong"].Str;
					npcTalkQiTaJiaoHuData.TalkLunDaoSBPuTong = jsonobject["TalkLunDaoSBPuTong"].Str;
					npcTalkQiTaJiaoHuData.TalkLunDaoShiBai = jsonobject["TalkLunDaoShiBai"].Str;
					npcTalkQiTaJiaoHuData.TalkJuJueLunDao = jsonobject["TalkJuJueLunDao"].Str;
					npcTalkQiTaJiaoHuData.TalkJieYaoQingQieCuo = jsonobject["TalkJieYaoQingQieCuo"].Str;
					npcTalkQiTaJiaoHuData.TalkJuJueDaTing = jsonobject["TalkJuJueDaTing"].Str;
					npcTalkQiTaJiaoHuData.TalkJieShouHaoYou = jsonobject["TalkJieShouHaoYou"].Str;
					npcTalkQiTaJiaoHuData.TalkTiWen = jsonobject["TalkTiWen"].Str;
					npcTalkQiTaJiaoHuData.TalkBiaoBaiCG = jsonobject["TalkBiaoBaiCG"].Str;
					npcTalkQiTaJiaoHuData.TalkBiaoBaiCG2 = jsonobject["TalkBiaoBaiCG2"].Str;
					npcTalkQiTaJiaoHuData.TalkBBShiBai1 = jsonobject["TalkBBShiBai1"].Str;
					npcTalkQiTaJiaoHuData.TalkBBShiBai2 = jsonobject["TalkBBShiBai2"].Str;
					npcTalkQiTaJiaoHuData.TalkQueRen = jsonobject["TalkQueRen"].Str;
					npcTalkQiTaJiaoHuData.TalkWanXiao = jsonobject["TalkWanXiao"].Str;
					npcTalkQiTaJiaoHuData.TalkShouYao = jsonobject["TalkShouYao"].Str;
					npcTalkQiTaJiaoHuData.TalkDaoLv = jsonobject["TalkDaoLv"].Str;
					npcTalkQiTaJiaoHuData.TalkJuJueJiaoYi = jsonobject["TalkJuJueJiaoYi"].Str;
					npcTalkQiTaJiaoHuData.TalkLingMaiF = jsonobject["TalkLingMaiF"].Str;
					npcTalkQiTaJiaoHuData.TalkLingMaiZDF = jsonobject["TalkLingMaiZDF"].Str;
					npcTalkQiTaJiaoHuData.TalkLingMaiZDNF = jsonobject["TalkLingMaiZDNF"].Str;
					npcTalkQiTaJiaoHuData.TalkDaBiQianD = jsonobject["TalkDaBiQianD"].Str;
					npcTalkQiTaJiaoHuData.TalkDaBiQianZ = jsonobject["TalkDaBiQianZ"].Str;
					npcTalkQiTaJiaoHuData.TalkDaBiQianG = jsonobject["TalkDaBiQianG"].Str;
					npcTalkQiTaJiaoHuData.TalkDaBiSheng = jsonobject["TalkDaBiSheng"].Str;
					npcTalkQiTaJiaoHuData.TalkDaBiBai = jsonobject["TalkDaBiBai"].Str;
					npcTalkQiTaJiaoHuData.TalkDaBiQianShiFu = jsonobject["TalkDaBiQianShiFu"].Str;
					npcTalkQiTaJiaoHuData.TalkDaBiHouShiFuS = jsonobject["TalkDaBiHouShiFuS"].Str;
					npcTalkQiTaJiaoHuData.TalkDaBiHouShiFuB = jsonobject["TalkDaBiHouShiFuB"].Str;
					npcTalkQiTaJiaoHuData.TalkDaBiQianDaoLv = jsonobject["TalkDaBiQianDaoLv"].Str;
					npcTalkQiTaJiaoHuData.TalkDaBiHouDaoLvS = jsonobject["TalkDaBiHouDaoLvS"].Str;
					npcTalkQiTaJiaoHuData.TalkDaBiHouDaoLvB = jsonobject["TalkDaBiHouDaoLvB"].Str;
					npcTalkQiTaJiaoHuData.TalkChengHu = jsonobject["TalkChengHu"].Str;
					npcTalkQiTaJiaoHuData.TalkYJMMPBZQ = jsonobject["TalkYJMMPBZQ"].Str;
					if (NpcTalkQiTaJiaoHuData.DataDict.ContainsKey(npcTalkQiTaJiaoHuData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcTalkQiTaJiaoHuData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcTalkQiTaJiaoHuData.id));
					}
					else
					{
						NpcTalkQiTaJiaoHuData.DataDict.Add(npcTalkQiTaJiaoHuData.id, npcTalkQiTaJiaoHuData);
						NpcTalkQiTaJiaoHuData.DataList.Add(npcTalkQiTaJiaoHuData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcTalkQiTaJiaoHuData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcTalkQiTaJiaoHuData.OnInitFinishAction != null)
			{
				NpcTalkQiTaJiaoHuData.OnInitFinishAction();
			}
		}

		// Token: 0x06004C62 RID: 19554 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004A5D RID: 19037
		public static Dictionary<int, NpcTalkQiTaJiaoHuData> DataDict = new Dictionary<int, NpcTalkQiTaJiaoHuData>();

		// Token: 0x04004A5E RID: 19038
		public static List<NpcTalkQiTaJiaoHuData> DataList = new List<NpcTalkQiTaJiaoHuData>();

		// Token: 0x04004A5F RID: 19039
		public static Action OnInitFinishAction = new Action(NpcTalkQiTaJiaoHuData.OnInitFinish);

		// Token: 0x04004A60 RID: 19040
		public int id;

		// Token: 0x04004A61 RID: 19041
		public int JingJie;

		// Token: 0x04004A62 RID: 19042
		public int XingGe;

		// Token: 0x04004A63 RID: 19043
		public string TalkTanChaShiBai;

		// Token: 0x04004A64 RID: 19044
		public string TalkTanChaZhanDou;

		// Token: 0x04004A65 RID: 19045
		public string TalkJieSha;

		// Token: 0x04004A66 RID: 19046
		public string TalkJieShouQieCuo;

		// Token: 0x04004A67 RID: 19047
		public string TalkQieCuoShengLi;

		// Token: 0x04004A68 RID: 19048
		public string TalkQieCuoShiBai;

		// Token: 0x04004A69 RID: 19049
		public string TalkZengLiJiXu;

		// Token: 0x04004A6A RID: 19050
		public string TalkZengLiChengGong;

		// Token: 0x04004A6B RID: 19051
		public string TalkZengLiShiBaigao;

		// Token: 0x04004A6C RID: 19052
		public string TalkZengLiShiBaidi;

		// Token: 0x04004A6D RID: 19053
		public string TalkZengLiLaJi;

		// Token: 0x04004A6E RID: 19054
		public string TalkNPCWeiXie;

		// Token: 0x04004A6F RID: 19055
		public string TalkWeiXieChengGong1;

		// Token: 0x04004A70 RID: 19056
		public string TalkWeiXieChengGong2;

		// Token: 0x04004A71 RID: 19057
		public string TalkWeiXieChengGong3;

		// Token: 0x04004A72 RID: 19058
		public string TalkWeiXieChengGong4;

		// Token: 0x04004A73 RID: 19059
		public string TalkWeiXieShiBai1;

		// Token: 0x04004A74 RID: 19060
		public string TalkWeiXieShiBai2;

		// Token: 0x04004A75 RID: 19061
		public string TalkWeiXieShiBai3;

		// Token: 0x04004A76 RID: 19062
		public string TalkWeiXieShiBai4;

		// Token: 0x04004A77 RID: 19063
		public string TalkWeiXieShiBai5;

		// Token: 0x04004A78 RID: 19064
		public string TalkWeiXieShiBai6;

		// Token: 0x04004A79 RID: 19065
		public string TalkMenaceProbSuc;

		// Token: 0x04004A7A RID: 19066
		public string TalkMenaceProbFailed1;

		// Token: 0x04004A7B RID: 19067
		public string TalkMenaceProbFailed2;

		// Token: 0x04004A7C RID: 19068
		public string TalkQingJiaoChengGong;

		// Token: 0x04004A7D RID: 19069
		public string TalkQingJiaoShiBaiQF;

		// Token: 0x04004A7E RID: 19070
		public string TalkQingJiaoShiBaiSW;

		// Token: 0x04004A7F RID: 19071
		public string TalkYaoQingLunDao;

		// Token: 0x04004A80 RID: 19072
		public string TalkJieShouLunDao;

		// Token: 0x04004A81 RID: 19073
		public string TalkLunDaoCGPuTong;

		// Token: 0x04004A82 RID: 19074
		public string TalkLunDaoChengGong;

		// Token: 0x04004A83 RID: 19075
		public string TalkLunDaoSBPuTong;

		// Token: 0x04004A84 RID: 19076
		public string TalkLunDaoShiBai;

		// Token: 0x04004A85 RID: 19077
		public string TalkJuJueLunDao;

		// Token: 0x04004A86 RID: 19078
		public string TalkJieYaoQingQieCuo;

		// Token: 0x04004A87 RID: 19079
		public string TalkJuJueDaTing;

		// Token: 0x04004A88 RID: 19080
		public string TalkJieShouHaoYou;

		// Token: 0x04004A89 RID: 19081
		public string TalkTiWen;

		// Token: 0x04004A8A RID: 19082
		public string TalkBiaoBaiCG;

		// Token: 0x04004A8B RID: 19083
		public string TalkBiaoBaiCG2;

		// Token: 0x04004A8C RID: 19084
		public string TalkBBShiBai1;

		// Token: 0x04004A8D RID: 19085
		public string TalkBBShiBai2;

		// Token: 0x04004A8E RID: 19086
		public string TalkQueRen;

		// Token: 0x04004A8F RID: 19087
		public string TalkWanXiao;

		// Token: 0x04004A90 RID: 19088
		public string TalkShouYao;

		// Token: 0x04004A91 RID: 19089
		public string TalkDaoLv;

		// Token: 0x04004A92 RID: 19090
		public string TalkJuJueJiaoYi;

		// Token: 0x04004A93 RID: 19091
		public string TalkLingMaiF;

		// Token: 0x04004A94 RID: 19092
		public string TalkLingMaiZDF;

		// Token: 0x04004A95 RID: 19093
		public string TalkLingMaiZDNF;

		// Token: 0x04004A96 RID: 19094
		public string TalkDaBiQianD;

		// Token: 0x04004A97 RID: 19095
		public string TalkDaBiQianZ;

		// Token: 0x04004A98 RID: 19096
		public string TalkDaBiQianG;

		// Token: 0x04004A99 RID: 19097
		public string TalkDaBiSheng;

		// Token: 0x04004A9A RID: 19098
		public string TalkDaBiBai;

		// Token: 0x04004A9B RID: 19099
		public string TalkDaBiQianShiFu;

		// Token: 0x04004A9C RID: 19100
		public string TalkDaBiHouShiFuS;

		// Token: 0x04004A9D RID: 19101
		public string TalkDaBiHouShiFuB;

		// Token: 0x04004A9E RID: 19102
		public string TalkDaBiQianDaoLv;

		// Token: 0x04004A9F RID: 19103
		public string TalkDaBiHouDaoLvS;

		// Token: 0x04004AA0 RID: 19104
		public string TalkDaBiHouDaoLvB;

		// Token: 0x04004AA1 RID: 19105
		public string TalkChengHu;

		// Token: 0x04004AA2 RID: 19106
		public string TalkYJMMPBZQ;
	}
}
