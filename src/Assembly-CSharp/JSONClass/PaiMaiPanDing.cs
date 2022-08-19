using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008C9 RID: 2249
	public class PaiMaiPanDing : IJSONClass
	{
		// Token: 0x06004137 RID: 16695 RVA: 0x001BE9BC File Offset: 0x001BCBBC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.PaiMaiPanDing.list)
			{
				try
				{
					PaiMaiPanDing paiMaiPanDing = new PaiMaiPanDing();
					paiMaiPanDing.id = jsonobject["id"].I;
					paiMaiPanDing.JiaWei = jsonobject["JiaWei"].I;
					paiMaiPanDing.ShiZaiBiDe = jsonobject["ShiZaiBiDe"].I;
					paiMaiPanDing.YueYueYuShi = jsonobject["YueYueYuShi"].I;
					paiMaiPanDing.LueGanXingQu = jsonobject["LueGanXingQu"].I;
					if (PaiMaiPanDing.DataDict.ContainsKey(paiMaiPanDing.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典PaiMaiPanDing.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", paiMaiPanDing.id));
					}
					else
					{
						PaiMaiPanDing.DataDict.Add(paiMaiPanDing.id, paiMaiPanDing);
						PaiMaiPanDing.DataList.Add(paiMaiPanDing);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典PaiMaiPanDing.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (PaiMaiPanDing.OnInitFinishAction != null)
			{
				PaiMaiPanDing.OnInitFinishAction();
			}
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004052 RID: 16466
		public static Dictionary<int, PaiMaiPanDing> DataDict = new Dictionary<int, PaiMaiPanDing>();

		// Token: 0x04004053 RID: 16467
		public static List<PaiMaiPanDing> DataList = new List<PaiMaiPanDing>();

		// Token: 0x04004054 RID: 16468
		public static Action OnInitFinishAction = new Action(PaiMaiPanDing.OnInitFinish);

		// Token: 0x04004055 RID: 16469
		public int id;

		// Token: 0x04004056 RID: 16470
		public int JiaWei;

		// Token: 0x04004057 RID: 16471
		public int ShiZaiBiDe;

		// Token: 0x04004058 RID: 16472
		public int YueYueYuShi;

		// Token: 0x04004059 RID: 16473
		public int LueGanXingQu;
	}
}
