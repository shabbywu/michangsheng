using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008A7 RID: 2215
	public class NpcPaiMaiData : IJSONClass
	{
		// Token: 0x060040AF RID: 16559 RVA: 0x001BA128 File Offset: 0x001B8328
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcPaiMaiData.list)
			{
				try
				{
					NpcPaiMaiData npcPaiMaiData = new NpcPaiMaiData();
					npcPaiMaiData.id = jsonobject["id"].I;
					npcPaiMaiData.PaiMaiID = jsonobject["PaiMaiID"].I;
					npcPaiMaiData.ItemNum = jsonobject["ItemNum"].I;
					npcPaiMaiData.Type = jsonobject["Type"].ToList();
					npcPaiMaiData.quality = jsonobject["quality"].ToList();
					if (NpcPaiMaiData.DataDict.ContainsKey(npcPaiMaiData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcPaiMaiData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcPaiMaiData.id));
					}
					else
					{
						NpcPaiMaiData.DataDict.Add(npcPaiMaiData.id, npcPaiMaiData);
						NpcPaiMaiData.DataList.Add(npcPaiMaiData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcPaiMaiData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcPaiMaiData.OnInitFinishAction != null)
			{
				NpcPaiMaiData.OnInitFinishAction();
			}
		}

		// Token: 0x060040B0 RID: 16560 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003EA2 RID: 16034
		public static Dictionary<int, NpcPaiMaiData> DataDict = new Dictionary<int, NpcPaiMaiData>();

		// Token: 0x04003EA3 RID: 16035
		public static List<NpcPaiMaiData> DataList = new List<NpcPaiMaiData>();

		// Token: 0x04003EA4 RID: 16036
		public static Action OnInitFinishAction = new Action(NpcPaiMaiData.OnInitFinish);

		// Token: 0x04003EA5 RID: 16037
		public int id;

		// Token: 0x04003EA6 RID: 16038
		public int PaiMaiID;

		// Token: 0x04003EA7 RID: 16039
		public int ItemNum;

		// Token: 0x04003EA8 RID: 16040
		public List<int> Type = new List<int>();

		// Token: 0x04003EA9 RID: 16041
		public List<int> quality = new List<int>();
	}
}
