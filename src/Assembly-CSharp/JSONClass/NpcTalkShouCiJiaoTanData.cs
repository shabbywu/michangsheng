using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008B1 RID: 2225
	public class NpcTalkShouCiJiaoTanData : IJSONClass
	{
		// Token: 0x060040D7 RID: 16599 RVA: 0x001BBA2C File Offset: 0x001B9C2C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcTalkShouCiJiaoTanData.list)
			{
				try
				{
					NpcTalkShouCiJiaoTanData npcTalkShouCiJiaoTanData = new NpcTalkShouCiJiaoTanData();
					npcTalkShouCiJiaoTanData.id = jsonobject["id"].I;
					npcTalkShouCiJiaoTanData.JingJie = jsonobject["JingJie"].I;
					npcTalkShouCiJiaoTanData.ShengWang = jsonobject["ShengWang"].I;
					npcTalkShouCiJiaoTanData.XingGe = jsonobject["XingGe"].I;
					npcTalkShouCiJiaoTanData.HaoGanDu = jsonobject["HaoGanDu"].I;
					npcTalkShouCiJiaoTanData.FirstTalk = jsonobject["FirstTalk"].Str;
					if (NpcTalkShouCiJiaoTanData.DataDict.ContainsKey(npcTalkShouCiJiaoTanData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcTalkShouCiJiaoTanData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcTalkShouCiJiaoTanData.id));
					}
					else
					{
						NpcTalkShouCiJiaoTanData.DataDict.Add(npcTalkShouCiJiaoTanData.id, npcTalkShouCiJiaoTanData);
						NpcTalkShouCiJiaoTanData.DataList.Add(npcTalkShouCiJiaoTanData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcTalkShouCiJiaoTanData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcTalkShouCiJiaoTanData.OnInitFinishAction != null)
			{
				NpcTalkShouCiJiaoTanData.OnInitFinishAction();
			}
		}

		// Token: 0x060040D8 RID: 16600 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F4F RID: 16207
		public static Dictionary<int, NpcTalkShouCiJiaoTanData> DataDict = new Dictionary<int, NpcTalkShouCiJiaoTanData>();

		// Token: 0x04003F50 RID: 16208
		public static List<NpcTalkShouCiJiaoTanData> DataList = new List<NpcTalkShouCiJiaoTanData>();

		// Token: 0x04003F51 RID: 16209
		public static Action OnInitFinishAction = new Action(NpcTalkShouCiJiaoTanData.OnInitFinish);

		// Token: 0x04003F52 RID: 16210
		public int id;

		// Token: 0x04003F53 RID: 16211
		public int JingJie;

		// Token: 0x04003F54 RID: 16212
		public int ShengWang;

		// Token: 0x04003F55 RID: 16213
		public int XingGe;

		// Token: 0x04003F56 RID: 16214
		public int HaoGanDu;

		// Token: 0x04003F57 RID: 16215
		public string FirstTalk;
	}
}
