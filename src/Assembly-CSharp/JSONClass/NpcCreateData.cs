using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200089D RID: 2205
	public class NpcCreateData : IJSONClass
	{
		// Token: 0x06004087 RID: 16519 RVA: 0x001B8C10 File Offset: 0x001B6E10
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcCreateData.list)
			{
				try
				{
					NpcCreateData npcCreateData = new NpcCreateData();
					npcCreateData.id = jsonobject["id"].I;
					npcCreateData.NumA = jsonobject["NumA"].I;
					npcCreateData.NumB = jsonobject["NumB"].I;
					npcCreateData.EventValue = jsonobject["EventValue"].ToList();
					if (NpcCreateData.DataDict.ContainsKey(npcCreateData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcCreateData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcCreateData.id));
					}
					else
					{
						NpcCreateData.DataDict.Add(npcCreateData.id, npcCreateData);
						NpcCreateData.DataList.Add(npcCreateData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcCreateData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcCreateData.OnInitFinishAction != null)
			{
				NpcCreateData.OnInitFinishAction();
			}
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003E29 RID: 15913
		public static Dictionary<int, NpcCreateData> DataDict = new Dictionary<int, NpcCreateData>();

		// Token: 0x04003E2A RID: 15914
		public static List<NpcCreateData> DataList = new List<NpcCreateData>();

		// Token: 0x04003E2B RID: 15915
		public static Action OnInitFinishAction = new Action(NpcCreateData.OnInitFinish);

		// Token: 0x04003E2C RID: 15916
		public int id;

		// Token: 0x04003E2D RID: 15917
		public int NumA;

		// Token: 0x04003E2E RID: 15918
		public int NumB;

		// Token: 0x04003E2F RID: 15919
		public List<int> EventValue = new List<int>();
	}
}
