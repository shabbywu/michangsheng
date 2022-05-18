using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C46 RID: 3142
	public class NpcYiWaiDeathDate : IJSONClass
	{
		// Token: 0x06004C81 RID: 19585 RVA: 0x002055B8 File Offset: 0x002037B8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcYiWaiDeathDate.list)
			{
				try
				{
					NpcYiWaiDeathDate npcYiWaiDeathDate = new NpcYiWaiDeathDate();
					npcYiWaiDeathDate.id = jsonobject["id"].I;
					npcYiWaiDeathDate.HaoGanDu = jsonobject["HaoGanDu"].I;
					npcYiWaiDeathDate.SiWangJiLv = jsonobject["SiWangJiLv"].I;
					npcYiWaiDeathDate.SiWangLeiXing = jsonobject["SiWangLeiXing"].I;
					if (NpcYiWaiDeathDate.DataDict.ContainsKey(npcYiWaiDeathDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcYiWaiDeathDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcYiWaiDeathDate.id));
					}
					else
					{
						NpcYiWaiDeathDate.DataDict.Add(npcYiWaiDeathDate.id, npcYiWaiDeathDate);
						NpcYiWaiDeathDate.DataList.Add(npcYiWaiDeathDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcYiWaiDeathDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcYiWaiDeathDate.OnInitFinishAction != null)
			{
				NpcYiWaiDeathDate.OnInitFinishAction();
			}
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004AFA RID: 19194
		public static Dictionary<int, NpcYiWaiDeathDate> DataDict = new Dictionary<int, NpcYiWaiDeathDate>();

		// Token: 0x04004AFB RID: 19195
		public static List<NpcYiWaiDeathDate> DataList = new List<NpcYiWaiDeathDate>();

		// Token: 0x04004AFC RID: 19196
		public static Action OnInitFinishAction = new Action(NpcYiWaiDeathDate.OnInitFinish);

		// Token: 0x04004AFD RID: 19197
		public int id;

		// Token: 0x04004AFE RID: 19198
		public int HaoGanDu;

		// Token: 0x04004AFF RID: 19199
		public int SiWangJiLv;

		// Token: 0x04004B00 RID: 19200
		public int SiWangLeiXing;
	}
}
