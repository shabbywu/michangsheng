using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000896 RID: 2198
	public class NpcBeiBaoTypeData : IJSONClass
	{
		// Token: 0x0600406B RID: 16491 RVA: 0x001B7E04 File Offset: 0x001B6004
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcBeiBaoTypeData.list)
			{
				try
				{
					NpcBeiBaoTypeData npcBeiBaoTypeData = new NpcBeiBaoTypeData();
					npcBeiBaoTypeData.id = jsonobject["id"].I;
					npcBeiBaoTypeData.BagTpye = jsonobject["BagTpye"].I;
					npcBeiBaoTypeData.JinJie = jsonobject["JinJie"].I;
					npcBeiBaoTypeData.ShopType = jsonobject["ShopType"].ToList();
					npcBeiBaoTypeData.quality = jsonobject["quality"].ToList();
					if (NpcBeiBaoTypeData.DataDict.ContainsKey(npcBeiBaoTypeData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcBeiBaoTypeData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcBeiBaoTypeData.id));
					}
					else
					{
						NpcBeiBaoTypeData.DataDict.Add(npcBeiBaoTypeData.id, npcBeiBaoTypeData);
						NpcBeiBaoTypeData.DataList.Add(npcBeiBaoTypeData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcBeiBaoTypeData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcBeiBaoTypeData.OnInitFinishAction != null)
			{
				NpcBeiBaoTypeData.OnInitFinishAction();
			}
		}

		// Token: 0x0600406C RID: 16492 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003DDD RID: 15837
		public static Dictionary<int, NpcBeiBaoTypeData> DataDict = new Dictionary<int, NpcBeiBaoTypeData>();

		// Token: 0x04003DDE RID: 15838
		public static List<NpcBeiBaoTypeData> DataList = new List<NpcBeiBaoTypeData>();

		// Token: 0x04003DDF RID: 15839
		public static Action OnInitFinishAction = new Action(NpcBeiBaoTypeData.OnInitFinish);

		// Token: 0x04003DE0 RID: 15840
		public int id;

		// Token: 0x04003DE1 RID: 15841
		public int BagTpye;

		// Token: 0x04003DE2 RID: 15842
		public int JinJie;

		// Token: 0x04003DE3 RID: 15843
		public List<int> ShopType = new List<int>();

		// Token: 0x04003DE4 RID: 15844
		public List<int> quality = new List<int>();
	}
}
