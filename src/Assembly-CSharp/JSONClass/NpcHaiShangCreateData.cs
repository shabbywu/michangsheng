using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200089F RID: 2207
	public class NpcHaiShangCreateData : IJSONClass
	{
		// Token: 0x0600408F RID: 16527 RVA: 0x001B9080 File Offset: 0x001B7280
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcHaiShangCreateData.list)
			{
				try
				{
					NpcHaiShangCreateData npcHaiShangCreateData = new NpcHaiShangCreateData();
					npcHaiShangCreateData.id = jsonobject["id"].I;
					npcHaiShangCreateData.LiuPai = jsonobject["LiuPai"].I;
					npcHaiShangCreateData.level = jsonobject["level"].I;
					if (NpcHaiShangCreateData.DataDict.ContainsKey(npcHaiShangCreateData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcHaiShangCreateData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcHaiShangCreateData.id));
					}
					else
					{
						NpcHaiShangCreateData.DataDict.Add(npcHaiShangCreateData.id, npcHaiShangCreateData);
						NpcHaiShangCreateData.DataList.Add(npcHaiShangCreateData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcHaiShangCreateData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcHaiShangCreateData.OnInitFinishAction != null)
			{
				NpcHaiShangCreateData.OnInitFinishAction();
			}
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003E41 RID: 15937
		public static Dictionary<int, NpcHaiShangCreateData> DataDict = new Dictionary<int, NpcHaiShangCreateData>();

		// Token: 0x04003E42 RID: 15938
		public static List<NpcHaiShangCreateData> DataList = new List<NpcHaiShangCreateData>();

		// Token: 0x04003E43 RID: 15939
		public static Action OnInitFinishAction = new Action(NpcHaiShangCreateData.OnInitFinish);

		// Token: 0x04003E44 RID: 15940
		public int id;

		// Token: 0x04003E45 RID: 15941
		public int LiuPai;

		// Token: 0x04003E46 RID: 15942
		public int level;
	}
}
