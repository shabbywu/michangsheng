using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C55 RID: 3157
	public class PaiMaiNpcAddPriceSay : IJSONClass
	{
		// Token: 0x06004CBD RID: 19645 RVA: 0x00206F8C File Offset: 0x0020518C
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

		// Token: 0x06004CBE RID: 19646 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004B99 RID: 19353
		public static Dictionary<int, PaiMaiNpcAddPriceSay> DataDict = new Dictionary<int, PaiMaiNpcAddPriceSay>();

		// Token: 0x04004B9A RID: 19354
		public static List<PaiMaiNpcAddPriceSay> DataList = new List<PaiMaiNpcAddPriceSay>();

		// Token: 0x04004B9B RID: 19355
		public static Action OnInitFinishAction = new Action(PaiMaiNpcAddPriceSay.OnInitFinish);

		// Token: 0x04004B9C RID: 19356
		public int id;

		// Token: 0x04004B9D RID: 19357
		public int Type;

		// Token: 0x04004B9E RID: 19358
		public string ChuJiaDuiHua;
	}
}
