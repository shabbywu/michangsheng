using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C24 RID: 3108
	public class NpcBeiBaoTypeData : IJSONClass
	{
		// Token: 0x06004BF9 RID: 19449 RVA: 0x00201198 File Offset: 0x001FF398
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

		// Token: 0x06004BFA RID: 19450 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004936 RID: 18742
		public static Dictionary<int, NpcBeiBaoTypeData> DataDict = new Dictionary<int, NpcBeiBaoTypeData>();

		// Token: 0x04004937 RID: 18743
		public static List<NpcBeiBaoTypeData> DataList = new List<NpcBeiBaoTypeData>();

		// Token: 0x04004938 RID: 18744
		public static Action OnInitFinishAction = new Action(NpcBeiBaoTypeData.OnInitFinish);

		// Token: 0x04004939 RID: 18745
		public int id;

		// Token: 0x0400493A RID: 18746
		public int BagTpye;

		// Token: 0x0400493B RID: 18747
		public int JinJie;

		// Token: 0x0400493C RID: 18748
		public List<int> ShopType = new List<int>();

		// Token: 0x0400493D RID: 18749
		public List<int> quality = new List<int>();
	}
}
