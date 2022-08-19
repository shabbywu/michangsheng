using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008C0 RID: 2240
	public class PaiMaiCeLueSuiJiBiao : IJSONClass
	{
		// Token: 0x06004113 RID: 16659 RVA: 0x001BDA24 File Offset: 0x001BBC24
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

		// Token: 0x06004114 RID: 16660 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004007 RID: 16391
		public static Dictionary<int, PaiMaiCeLueSuiJiBiao> DataDict = new Dictionary<int, PaiMaiCeLueSuiJiBiao>();

		// Token: 0x04004008 RID: 16392
		public static List<PaiMaiCeLueSuiJiBiao> DataList = new List<PaiMaiCeLueSuiJiBiao>();

		// Token: 0x04004009 RID: 16393
		public static Action OnInitFinishAction = new Action(PaiMaiCeLueSuiJiBiao.OnInitFinish);

		// Token: 0x0400400A RID: 16394
		public int id;

		// Token: 0x0400400B RID: 16395
		public int itemType;

		// Token: 0x0400400C RID: 16396
		public int itemQuality;

		// Token: 0x0400400D RID: 16397
		public List<int> Lv1 = new List<int>();

		// Token: 0x0400400E RID: 16398
		public List<int> Lv2 = new List<int>();

		// Token: 0x0400400F RID: 16399
		public List<int> Lv3 = new List<int>();

		// Token: 0x04004010 RID: 16400
		public List<int> Lv4 = new List<int>();

		// Token: 0x04004011 RID: 16401
		public List<int> Lv5 = new List<int>();

		// Token: 0x04004012 RID: 16402
		public List<int> Lv6 = new List<int>();

		// Token: 0x04004013 RID: 16403
		public List<int> Lv7 = new List<int>();

		// Token: 0x04004014 RID: 16404
		public List<int> Lv8 = new List<int>();

		// Token: 0x04004015 RID: 16405
		public List<int> Lv9 = new List<int>();

		// Token: 0x04004016 RID: 16406
		public List<int> Lv10 = new List<int>();

		// Token: 0x04004017 RID: 16407
		public List<int> Lv11 = new List<int>();

		// Token: 0x04004018 RID: 16408
		public List<int> Lv12 = new List<int>();

		// Token: 0x04004019 RID: 16409
		public List<int> Lv13 = new List<int>();

		// Token: 0x0400401A RID: 16410
		public List<int> Lv14 = new List<int>();

		// Token: 0x0400401B RID: 16411
		public List<int> Lv15 = new List<int>();
	}
}
