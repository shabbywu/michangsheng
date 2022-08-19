using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008B6 RID: 2230
	public class NpcXingGeDate : IJSONClass
	{
		// Token: 0x060040EB RID: 16619 RVA: 0x001BC680 File Offset: 0x001BA880
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcXingGeDate.list)
			{
				try
				{
					NpcXingGeDate npcXingGeDate = new NpcXingGeDate();
					npcXingGeDate.id = jsonobject["id"].I;
					npcXingGeDate.zhengxie = jsonobject["zhengxie"].I;
					if (NpcXingGeDate.DataDict.ContainsKey(npcXingGeDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcXingGeDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcXingGeDate.id));
					}
					else
					{
						NpcXingGeDate.DataDict.Add(npcXingGeDate.id, npcXingGeDate);
						NpcXingGeDate.DataList.Add(npcXingGeDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcXingGeDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcXingGeDate.OnInitFinishAction != null)
			{
				NpcXingGeDate.OnInitFinishAction();
			}
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F98 RID: 16280
		public static Dictionary<int, NpcXingGeDate> DataDict = new Dictionary<int, NpcXingGeDate>();

		// Token: 0x04003F99 RID: 16281
		public static List<NpcXingGeDate> DataList = new List<NpcXingGeDate>();

		// Token: 0x04003F9A RID: 16282
		public static Action OnInitFinishAction = new Action(NpcXingGeDate.OnInitFinish);

		// Token: 0x04003F9B RID: 16283
		public int id;

		// Token: 0x04003F9C RID: 16284
		public int zhengxie;
	}
}
