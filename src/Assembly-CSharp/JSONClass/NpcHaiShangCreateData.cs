using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C2D RID: 3117
	public class NpcHaiShangCreateData : IJSONClass
	{
		// Token: 0x06004C1D RID: 19485 RVA: 0x00202230 File Offset: 0x00200430
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

		// Token: 0x06004C1E RID: 19486 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400499A RID: 18842
		public static Dictionary<int, NpcHaiShangCreateData> DataDict = new Dictionary<int, NpcHaiShangCreateData>();

		// Token: 0x0400499B RID: 18843
		public static List<NpcHaiShangCreateData> DataList = new List<NpcHaiShangCreateData>();

		// Token: 0x0400499C RID: 18844
		public static Action OnInitFinishAction = new Action(NpcHaiShangCreateData.OnInitFinish);

		// Token: 0x0400499D RID: 18845
		public int id;

		// Token: 0x0400499E RID: 18846
		public int LiuPai;

		// Token: 0x0400499F RID: 18847
		public int level;
	}
}
