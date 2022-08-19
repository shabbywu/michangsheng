using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008AE RID: 2222
	public class NpcTalkGuanYuTuPoData : IJSONClass
	{
		// Token: 0x060040CB RID: 16587 RVA: 0x001BAF88 File Offset: 0x001B9188
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcTalkGuanYuTuPoData.list)
			{
				try
				{
					NpcTalkGuanYuTuPoData npcTalkGuanYuTuPoData = new NpcTalkGuanYuTuPoData();
					npcTalkGuanYuTuPoData.id = jsonobject["id"].I;
					npcTalkGuanYuTuPoData.WanJiaJingJie = jsonobject["WanJiaJingJie"].I;
					npcTalkGuanYuTuPoData.JingJie = jsonobject["JingJie"].I;
					npcTalkGuanYuTuPoData.XingGe = jsonobject["XingGe"].I;
					npcTalkGuanYuTuPoData.TuPoTalk = jsonobject["TuPoTalk"].Str;
					if (NpcTalkGuanYuTuPoData.DataDict.ContainsKey(npcTalkGuanYuTuPoData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcTalkGuanYuTuPoData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcTalkGuanYuTuPoData.id));
					}
					else
					{
						NpcTalkGuanYuTuPoData.DataDict.Add(npcTalkGuanYuTuPoData.id, npcTalkGuanYuTuPoData);
						NpcTalkGuanYuTuPoData.DataList.Add(npcTalkGuanYuTuPoData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcTalkGuanYuTuPoData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcTalkGuanYuTuPoData.OnInitFinishAction != null)
			{
				NpcTalkGuanYuTuPoData.OnInitFinishAction();
			}
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003EF5 RID: 16117
		public static Dictionary<int, NpcTalkGuanYuTuPoData> DataDict = new Dictionary<int, NpcTalkGuanYuTuPoData>();

		// Token: 0x04003EF6 RID: 16118
		public static List<NpcTalkGuanYuTuPoData> DataList = new List<NpcTalkGuanYuTuPoData>();

		// Token: 0x04003EF7 RID: 16119
		public static Action OnInitFinishAction = new Action(NpcTalkGuanYuTuPoData.OnInitFinish);

		// Token: 0x04003EF8 RID: 16120
		public int id;

		// Token: 0x04003EF9 RID: 16121
		public int WanJiaJingJie;

		// Token: 0x04003EFA RID: 16122
		public int JingJie;

		// Token: 0x04003EFB RID: 16123
		public int XingGe;

		// Token: 0x04003EFC RID: 16124
		public string TuPoTalk;
	}
}
