using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008B8 RID: 2232
	public class NpcYiWaiDeathDate : IJSONClass
	{
		// Token: 0x060040F3 RID: 16627 RVA: 0x001BC99C File Offset: 0x001BAB9C
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

		// Token: 0x060040F4 RID: 16628 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003FA6 RID: 16294
		public static Dictionary<int, NpcYiWaiDeathDate> DataDict = new Dictionary<int, NpcYiWaiDeathDate>();

		// Token: 0x04003FA7 RID: 16295
		public static List<NpcYiWaiDeathDate> DataList = new List<NpcYiWaiDeathDate>();

		// Token: 0x04003FA8 RID: 16296
		public static Action OnInitFinishAction = new Action(NpcYiWaiDeathDate.OnInitFinish);

		// Token: 0x04003FA9 RID: 16297
		public int id;

		// Token: 0x04003FAA RID: 16298
		public int HaoGanDu;

		// Token: 0x04003FAB RID: 16299
		public int SiWangJiLv;

		// Token: 0x04003FAC RID: 16300
		public int SiWangLeiXing;
	}
}
