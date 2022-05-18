using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C3C RID: 3132
	public class NpcTalkGuanYuTuPoData : IJSONClass
	{
		// Token: 0x06004C59 RID: 19545 RVA: 0x00203DC0 File Offset: 0x00201FC0
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

		// Token: 0x06004C5A RID: 19546 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004A4D RID: 19021
		public static Dictionary<int, NpcTalkGuanYuTuPoData> DataDict = new Dictionary<int, NpcTalkGuanYuTuPoData>();

		// Token: 0x04004A4E RID: 19022
		public static List<NpcTalkGuanYuTuPoData> DataList = new List<NpcTalkGuanYuTuPoData>();

		// Token: 0x04004A4F RID: 19023
		public static Action OnInitFinishAction = new Action(NpcTalkGuanYuTuPoData.OnInitFinish);

		// Token: 0x04004A50 RID: 19024
		public int id;

		// Token: 0x04004A51 RID: 19025
		public int WanJiaJingJie;

		// Token: 0x04004A52 RID: 19026
		public int JingJie;

		// Token: 0x04004A53 RID: 19027
		public int XingGe;

		// Token: 0x04004A54 RID: 19028
		public string TuPoTalk;
	}
}
