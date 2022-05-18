using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C4E RID: 3150
	public class PaiMaiCeLueSuiJiBiao : IJSONClass
	{
		// Token: 0x06004CA1 RID: 19617 RVA: 0x00206438 File Offset: 0x00204638
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.PaiMaiCeLueSuiJiBiao.list)
			{
				try
				{
					PaiMaiCeLueSuiJiBiao paiMaiCeLueSuiJiBiao = new PaiMaiCeLueSuiJiBiao();
					paiMaiCeLueSuiJiBiao.id = jsonobject["id"].I;
					paiMaiCeLueSuiJiBiao.itemType = jsonobject["itemType"].I;
					paiMaiCeLueSuiJiBiao.itemQuality = jsonobject["itemQuality"].I;
					paiMaiCeLueSuiJiBiao.Lv1 = jsonobject["Lv1"].ToList();
					paiMaiCeLueSuiJiBiao.Lv2 = jsonobject["Lv2"].ToList();
					paiMaiCeLueSuiJiBiao.Lv3 = jsonobject["Lv3"].ToList();
					paiMaiCeLueSuiJiBiao.Lv4 = jsonobject["Lv4"].ToList();
					paiMaiCeLueSuiJiBiao.Lv5 = jsonobject["Lv5"].ToList();
					paiMaiCeLueSuiJiBiao.Lv6 = jsonobject["Lv6"].ToList();
					paiMaiCeLueSuiJiBiao.Lv7 = jsonobject["Lv7"].ToList();
					paiMaiCeLueSuiJiBiao.Lv8 = jsonobject["Lv8"].ToList();
					paiMaiCeLueSuiJiBiao.Lv9 = jsonobject["Lv9"].ToList();
					paiMaiCeLueSuiJiBiao.Lv10 = jsonobject["Lv10"].ToList();
					paiMaiCeLueSuiJiBiao.Lv11 = jsonobject["Lv11"].ToList();
					paiMaiCeLueSuiJiBiao.Lv12 = jsonobject["Lv12"].ToList();
					paiMaiCeLueSuiJiBiao.Lv13 = jsonobject["Lv13"].ToList();
					paiMaiCeLueSuiJiBiao.Lv14 = jsonobject["Lv14"].ToList();
					paiMaiCeLueSuiJiBiao.Lv15 = jsonobject["Lv15"].ToList();
					if (PaiMaiCeLueSuiJiBiao.DataDict.ContainsKey(paiMaiCeLueSuiJiBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典PaiMaiCeLueSuiJiBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", paiMaiCeLueSuiJiBiao.id));
					}
					else
					{
						PaiMaiCeLueSuiJiBiao.DataDict.Add(paiMaiCeLueSuiJiBiao.id, paiMaiCeLueSuiJiBiao);
						PaiMaiCeLueSuiJiBiao.DataList.Add(paiMaiCeLueSuiJiBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典PaiMaiCeLueSuiJiBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (PaiMaiCeLueSuiJiBiao.OnInitFinishAction != null)
			{
				PaiMaiCeLueSuiJiBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06004CA2 RID: 19618 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004B5B RID: 19291
		public static Dictionary<int, PaiMaiCeLueSuiJiBiao> DataDict = new Dictionary<int, PaiMaiCeLueSuiJiBiao>();

		// Token: 0x04004B5C RID: 19292
		public static List<PaiMaiCeLueSuiJiBiao> DataList = new List<PaiMaiCeLueSuiJiBiao>();

		// Token: 0x04004B5D RID: 19293
		public static Action OnInitFinishAction = new Action(PaiMaiCeLueSuiJiBiao.OnInitFinish);

		// Token: 0x04004B5E RID: 19294
		public int id;

		// Token: 0x04004B5F RID: 19295
		public int itemType;

		// Token: 0x04004B60 RID: 19296
		public int itemQuality;

		// Token: 0x04004B61 RID: 19297
		public List<int> Lv1 = new List<int>();

		// Token: 0x04004B62 RID: 19298
		public List<int> Lv2 = new List<int>();

		// Token: 0x04004B63 RID: 19299
		public List<int> Lv3 = new List<int>();

		// Token: 0x04004B64 RID: 19300
		public List<int> Lv4 = new List<int>();

		// Token: 0x04004B65 RID: 19301
		public List<int> Lv5 = new List<int>();

		// Token: 0x04004B66 RID: 19302
		public List<int> Lv6 = new List<int>();

		// Token: 0x04004B67 RID: 19303
		public List<int> Lv7 = new List<int>();

		// Token: 0x04004B68 RID: 19304
		public List<int> Lv8 = new List<int>();

		// Token: 0x04004B69 RID: 19305
		public List<int> Lv9 = new List<int>();

		// Token: 0x04004B6A RID: 19306
		public List<int> Lv10 = new List<int>();

		// Token: 0x04004B6B RID: 19307
		public List<int> Lv11 = new List<int>();

		// Token: 0x04004B6C RID: 19308
		public List<int> Lv12 = new List<int>();

		// Token: 0x04004B6D RID: 19309
		public List<int> Lv13 = new List<int>();

		// Token: 0x04004B6E RID: 19310
		public List<int> Lv14 = new List<int>();

		// Token: 0x04004B6F RID: 19311
		public List<int> Lv15 = new List<int>();
	}
}
