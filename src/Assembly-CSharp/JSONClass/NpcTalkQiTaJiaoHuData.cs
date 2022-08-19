using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008B0 RID: 2224
	public class NpcTalkQiTaJiaoHuData : IJSONClass
	{
		// Token: 0x060040D3 RID: 16595 RVA: 0x001BB2D8 File Offset: 0x001B94D8
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
					npcTalkQiTaJiaoHuData.TalkNZGJBZQ = jsonobject["TalkNZGJBZQ"].Str;
					npcTalkQiTaJiaoHuData.TalkNZFSGL = jsonobject["TalkNZFSGL"].Str;
					npcTalkQiTaJiaoHuData.TalkLJQDY = jsonobject["TalkLJQDY"].Str;
					npcTalkQiTaJiaoHuData.TalkLJQDL = jsonobject["TalkLJQDL"].Str;
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

		// Token: 0x060040D4 RID: 16596 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F05 RID: 16133
		public static Dictionary<int, NpcTalkQiTaJiaoHuData> DataDict = new Dictionary<int, NpcTalkQiTaJiaoHuData>();

		// Token: 0x04003F06 RID: 16134
		public static List<NpcTalkQiTaJiaoHuData> DataList = new List<NpcTalkQiTaJiaoHuData>();

		// Token: 0x04003F07 RID: 16135
		public static Action OnInitFinishAction = new Action(NpcTalkQiTaJiaoHuData.OnInitFinish);

		// Token: 0x04003F08 RID: 16136
		public int id;

		// Token: 0x04003F09 RID: 16137
		public int JingJie;

		// Token: 0x04003F0A RID: 16138
		public int XingGe;

		// Token: 0x04003F0B RID: 16139
		public string TalkTanChaShiBai;

		// Token: 0x04003F0C RID: 16140
		public string TalkTanChaZhanDou;

		// Token: 0x04003F0D RID: 16141
		public string TalkJieSha;

		// Token: 0x04003F0E RID: 16142
		public string TalkJieShouQieCuo;

		// Token: 0x04003F0F RID: 16143
		public string TalkQieCuoShengLi;

		// Token: 0x04003F10 RID: 16144
		public string TalkQieCuoShiBai;

		// Token: 0x04003F11 RID: 16145
		public string TalkZengLiJiXu;

		// Token: 0x04003F12 RID: 16146
		public string TalkZengLiChengGong;

		// Token: 0x04003F13 RID: 16147
		public string TalkZengLiShiBaigao;

		// Token: 0x04003F14 RID: 16148
		public string TalkZengLiShiBaidi;

		// Token: 0x04003F15 RID: 16149
		public string TalkZengLiLaJi;

		// Token: 0x04003F16 RID: 16150
		public string TalkNPCWeiXie;

		// Token: 0x04003F17 RID: 16151
		public string TalkWeiXieChengGong1;

		// Token: 0x04003F18 RID: 16152
		public string TalkWeiXieChengGong2;

		// Token: 0x04003F19 RID: 16153
		public string TalkWeiXieChengGong3;

		// Token: 0x04003F1A RID: 16154
		public string TalkWeiXieChengGong4;

		// Token: 0x04003F1B RID: 16155
		public string TalkWeiXieShiBai1;

		// Token: 0x04003F1C RID: 16156
		public string TalkWeiXieShiBai2;

		// Token: 0x04003F1D RID: 16157
		public string TalkWeiXieShiBai3;

		// Token: 0x04003F1E RID: 16158
		public string TalkWeiXieShiBai4;

		// Token: 0x04003F1F RID: 16159
		public string TalkWeiXieShiBai5;

		// Token: 0x04003F20 RID: 16160
		public string TalkWeiXieShiBai6;

		// Token: 0x04003F21 RID: 16161
		public string TalkMenaceProbSuc;

		// Token: 0x04003F22 RID: 16162
		public string TalkMenaceProbFailed1;

		// Token: 0x04003F23 RID: 16163
		public string TalkMenaceProbFailed2;

		// Token: 0x04003F24 RID: 16164
		public string TalkQingJiaoChengGong;

		// Token: 0x04003F25 RID: 16165
		public string TalkQingJiaoShiBaiQF;

		// Token: 0x04003F26 RID: 16166
		public string TalkQingJiaoShiBaiSW;

		// Token: 0x04003F27 RID: 16167
		public string TalkYaoQingLunDao;

		// Token: 0x04003F28 RID: 16168
		public string TalkJieShouLunDao;

		// Token: 0x04003F29 RID: 16169
		public string TalkLunDaoCGPuTong;

		// Token: 0x04003F2A RID: 16170
		public string TalkLunDaoChengGong;

		// Token: 0x04003F2B RID: 16171
		public string TalkLunDaoSBPuTong;

		// Token: 0x04003F2C RID: 16172
		public string TalkLunDaoShiBai;

		// Token: 0x04003F2D RID: 16173
		public string TalkJuJueLunDao;

		// Token: 0x04003F2E RID: 16174
		public string TalkJieYaoQingQieCuo;

		// Token: 0x04003F2F RID: 16175
		public string TalkJuJueDaTing;

		// Token: 0x04003F30 RID: 16176
		public string TalkJieShouHaoYou;

		// Token: 0x04003F31 RID: 16177
		public string TalkTiWen;

		// Token: 0x04003F32 RID: 16178
		public string TalkBiaoBaiCG;

		// Token: 0x04003F33 RID: 16179
		public string TalkBiaoBaiCG2;

		// Token: 0x04003F34 RID: 16180
		public string TalkBBShiBai1;

		// Token: 0x04003F35 RID: 16181
		public string TalkBBShiBai2;

		// Token: 0x04003F36 RID: 16182
		public string TalkQueRen;

		// Token: 0x04003F37 RID: 16183
		public string TalkWanXiao;

		// Token: 0x04003F38 RID: 16184
		public string TalkShouYao;

		// Token: 0x04003F39 RID: 16185
		public string TalkDaoLv;

		// Token: 0x04003F3A RID: 16186
		public string TalkJuJueJiaoYi;

		// Token: 0x04003F3B RID: 16187
		public string TalkLingMaiF;

		// Token: 0x04003F3C RID: 16188
		public string TalkLingMaiZDF;

		// Token: 0x04003F3D RID: 16189
		public string TalkLingMaiZDNF;

		// Token: 0x04003F3E RID: 16190
		public string TalkDaBiQianD;

		// Token: 0x04003F3F RID: 16191
		public string TalkDaBiQianZ;

		// Token: 0x04003F40 RID: 16192
		public string TalkDaBiQianG;

		// Token: 0x04003F41 RID: 16193
		public string TalkDaBiSheng;

		// Token: 0x04003F42 RID: 16194
		public string TalkDaBiBai;

		// Token: 0x04003F43 RID: 16195
		public string TalkDaBiQianShiFu;

		// Token: 0x04003F44 RID: 16196
		public string TalkDaBiHouShiFuS;

		// Token: 0x04003F45 RID: 16197
		public string TalkDaBiHouShiFuB;

		// Token: 0x04003F46 RID: 16198
		public string TalkDaBiQianDaoLv;

		// Token: 0x04003F47 RID: 16199
		public string TalkDaBiHouDaoLvS;

		// Token: 0x04003F48 RID: 16200
		public string TalkDaBiHouDaoLvB;

		// Token: 0x04003F49 RID: 16201
		public string TalkChengHu;

		// Token: 0x04003F4A RID: 16202
		public string TalkYJMMPBZQ;

		// Token: 0x04003F4B RID: 16203
		public string TalkNZGJBZQ;

		// Token: 0x04003F4C RID: 16204
		public string TalkNZFSGL;

		// Token: 0x04003F4D RID: 16205
		public string TalkLJQDY;

		// Token: 0x04003F4E RID: 16206
		public string TalkLJQDL;
	}
}
