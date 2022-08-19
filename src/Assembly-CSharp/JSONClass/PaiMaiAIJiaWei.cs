using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008BD RID: 2237
	public class PaiMaiAIJiaWei : IJSONClass
	{
		// Token: 0x06004107 RID: 16647 RVA: 0x001BD398 File Offset: 0x001BB598
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.PaiMaiAIJiaWei.list)
			{
				try
				{
					PaiMaiAIJiaWei paiMaiAIJiaWei = new PaiMaiAIJiaWei();
					paiMaiAIJiaWei.id = jsonobject["id"].I;
					paiMaiAIJiaWei.Jiawei = jsonobject["Jiawei"].I;
					if (PaiMaiAIJiaWei.DataDict.ContainsKey(paiMaiAIJiaWei.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典PaiMaiAIJiaWei.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", paiMaiAIJiaWei.id));
					}
					else
					{
						PaiMaiAIJiaWei.DataDict.Add(paiMaiAIJiaWei.id, paiMaiAIJiaWei);
						PaiMaiAIJiaWei.DataList.Add(paiMaiAIJiaWei);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典PaiMaiAIJiaWei.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (PaiMaiAIJiaWei.OnInitFinishAction != null)
			{
				PaiMaiAIJiaWei.OnInitFinishAction();
			}
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003FE0 RID: 16352
		public static Dictionary<int, PaiMaiAIJiaWei> DataDict = new Dictionary<int, PaiMaiAIJiaWei>();

		// Token: 0x04003FE1 RID: 16353
		public static List<PaiMaiAIJiaWei> DataList = new List<PaiMaiAIJiaWei>();

		// Token: 0x04003FE2 RID: 16354
		public static Action OnInitFinishAction = new Action(PaiMaiAIJiaWei.OnInitFinish);

		// Token: 0x04003FE3 RID: 16355
		public int id;

		// Token: 0x04003FE4 RID: 16356
		public int Jiawei;
	}
}
