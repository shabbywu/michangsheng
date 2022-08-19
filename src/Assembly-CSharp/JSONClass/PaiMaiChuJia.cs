using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008C1 RID: 2241
	public class PaiMaiChuJia : IJSONClass
	{
		// Token: 0x06004117 RID: 16663 RVA: 0x001BDDA0 File Offset: 0x001BBFA0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.PaiMaiChuJia.list)
			{
				try
				{
					PaiMaiChuJia paiMaiChuJia = new PaiMaiChuJia();
					paiMaiChuJia.id = jsonobject["id"].I;
					paiMaiChuJia.ZhanBi = jsonobject["ZhanBi"].I;
					if (PaiMaiChuJia.DataDict.ContainsKey(paiMaiChuJia.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典PaiMaiChuJia.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", paiMaiChuJia.id));
					}
					else
					{
						PaiMaiChuJia.DataDict.Add(paiMaiChuJia.id, paiMaiChuJia);
						PaiMaiChuJia.DataList.Add(paiMaiChuJia);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典PaiMaiChuJia.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (PaiMaiChuJia.OnInitFinishAction != null)
			{
				PaiMaiChuJia.OnInitFinishAction();
			}
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400401C RID: 16412
		public static Dictionary<int, PaiMaiChuJia> DataDict = new Dictionary<int, PaiMaiChuJia>();

		// Token: 0x0400401D RID: 16413
		public static List<PaiMaiChuJia> DataList = new List<PaiMaiChuJia>();

		// Token: 0x0400401E RID: 16414
		public static Action OnInitFinishAction = new Action(PaiMaiChuJia.OnInitFinish);

		// Token: 0x0400401F RID: 16415
		public int id;

		// Token: 0x04004020 RID: 16416
		public int ZhanBi;
	}
}
