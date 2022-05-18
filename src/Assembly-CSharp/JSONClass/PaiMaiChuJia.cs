using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C4F RID: 3151
	public class PaiMaiChuJia : IJSONClass
	{
		// Token: 0x06004CA5 RID: 19621 RVA: 0x0020678C File Offset: 0x0020498C
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

		// Token: 0x06004CA6 RID: 19622 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004B70 RID: 19312
		public static Dictionary<int, PaiMaiChuJia> DataDict = new Dictionary<int, PaiMaiChuJia>();

		// Token: 0x04004B71 RID: 19313
		public static List<PaiMaiChuJia> DataList = new List<PaiMaiChuJia>();

		// Token: 0x04004B72 RID: 19314
		public static Action OnInitFinishAction = new Action(PaiMaiChuJia.OnInitFinish);

		// Token: 0x04004B73 RID: 19315
		public int id;

		// Token: 0x04004B74 RID: 19316
		public int ZhanBi;
	}
}
