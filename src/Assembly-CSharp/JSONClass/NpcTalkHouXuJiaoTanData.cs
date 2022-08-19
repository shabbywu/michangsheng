using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008AF RID: 2223
	public class NpcTalkHouXuJiaoTanData : IJSONClass
	{
		// Token: 0x060040CF RID: 16591 RVA: 0x001BB130 File Offset: 0x001B9330
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcTalkHouXuJiaoTanData.list)
			{
				try
				{
					NpcTalkHouXuJiaoTanData npcTalkHouXuJiaoTanData = new NpcTalkHouXuJiaoTanData();
					npcTalkHouXuJiaoTanData.id = jsonobject["id"].I;
					npcTalkHouXuJiaoTanData.JingJie = jsonobject["JingJie"].I;
					npcTalkHouXuJiaoTanData.XingGe = jsonobject["XingGe"].I;
					npcTalkHouXuJiaoTanData.HaoGanDu = jsonobject["HaoGanDu"].I;
					npcTalkHouXuJiaoTanData.OtherTalk = jsonobject["OtherTalk"].Str;
					if (NpcTalkHouXuJiaoTanData.DataDict.ContainsKey(npcTalkHouXuJiaoTanData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcTalkHouXuJiaoTanData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcTalkHouXuJiaoTanData.id));
					}
					else
					{
						NpcTalkHouXuJiaoTanData.DataDict.Add(npcTalkHouXuJiaoTanData.id, npcTalkHouXuJiaoTanData);
						NpcTalkHouXuJiaoTanData.DataList.Add(npcTalkHouXuJiaoTanData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcTalkHouXuJiaoTanData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcTalkHouXuJiaoTanData.OnInitFinishAction != null)
			{
				NpcTalkHouXuJiaoTanData.OnInitFinishAction();
			}
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003EFD RID: 16125
		public static Dictionary<int, NpcTalkHouXuJiaoTanData> DataDict = new Dictionary<int, NpcTalkHouXuJiaoTanData>();

		// Token: 0x04003EFE RID: 16126
		public static List<NpcTalkHouXuJiaoTanData> DataList = new List<NpcTalkHouXuJiaoTanData>();

		// Token: 0x04003EFF RID: 16127
		public static Action OnInitFinishAction = new Action(NpcTalkHouXuJiaoTanData.OnInitFinish);

		// Token: 0x04003F00 RID: 16128
		public int id;

		// Token: 0x04003F01 RID: 16129
		public int JingJie;

		// Token: 0x04003F02 RID: 16130
		public int XingGe;

		// Token: 0x04003F03 RID: 16131
		public int HaoGanDu;

		// Token: 0x04003F04 RID: 16132
		public string OtherTalk;
	}
}
