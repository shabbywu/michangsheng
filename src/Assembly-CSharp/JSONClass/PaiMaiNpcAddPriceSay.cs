using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008C7 RID: 2247
	public class PaiMaiNpcAddPriceSay : IJSONClass
	{
		// Token: 0x0600412F RID: 16687 RVA: 0x001BE6CC File Offset: 0x001BC8CC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.PaiMaiNpcAddPriceSay.list)
			{
				try
				{
					PaiMaiNpcAddPriceSay paiMaiNpcAddPriceSay = new PaiMaiNpcAddPriceSay();
					paiMaiNpcAddPriceSay.id = jsonobject["id"].I;
					paiMaiNpcAddPriceSay.Type = jsonobject["Type"].I;
					paiMaiNpcAddPriceSay.ChuJiaDuiHua = jsonobject["ChuJiaDuiHua"].Str;
					if (PaiMaiNpcAddPriceSay.DataDict.ContainsKey(paiMaiNpcAddPriceSay.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典PaiMaiNpcAddPriceSay.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", paiMaiNpcAddPriceSay.id));
					}
					else
					{
						PaiMaiNpcAddPriceSay.DataDict.Add(paiMaiNpcAddPriceSay.id, paiMaiNpcAddPriceSay);
						PaiMaiNpcAddPriceSay.DataList.Add(paiMaiNpcAddPriceSay);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典PaiMaiNpcAddPriceSay.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (PaiMaiNpcAddPriceSay.OnInitFinishAction != null)
			{
				PaiMaiNpcAddPriceSay.OnInitFinishAction();
			}
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004045 RID: 16453
		public static Dictionary<int, PaiMaiNpcAddPriceSay> DataDict = new Dictionary<int, PaiMaiNpcAddPriceSay>();

		// Token: 0x04004046 RID: 16454
		public static List<PaiMaiNpcAddPriceSay> DataList = new List<PaiMaiNpcAddPriceSay>();

		// Token: 0x04004047 RID: 16455
		public static Action OnInitFinishAction = new Action(PaiMaiNpcAddPriceSay.OnInitFinish);

		// Token: 0x04004048 RID: 16456
		public int id;

		// Token: 0x04004049 RID: 16457
		public int Type;

		// Token: 0x0400404A RID: 16458
		public string ChuJiaDuiHua;
	}
}
