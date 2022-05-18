using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C57 RID: 3159
	public class PaiMaiPanDing : IJSONClass
	{
		// Token: 0x06004CC5 RID: 19653 RVA: 0x00207218 File Offset: 0x00205418
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

		// Token: 0x06004CC6 RID: 19654 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004BA6 RID: 19366
		public static Dictionary<int, PaiMaiPanDing> DataDict = new Dictionary<int, PaiMaiPanDing>();

		// Token: 0x04004BA7 RID: 19367
		public static List<PaiMaiPanDing> DataList = new List<PaiMaiPanDing>();

		// Token: 0x04004BA8 RID: 19368
		public static Action OnInitFinishAction = new Action(PaiMaiPanDing.OnInitFinish);

		// Token: 0x04004BA9 RID: 19369
		public int id;

		// Token: 0x04004BAA RID: 19370
		public int JiaWei;

		// Token: 0x04004BAB RID: 19371
		public int ShiZaiBiDe;

		// Token: 0x04004BAC RID: 19372
		public int YueYueYuShi;

		// Token: 0x04004BAD RID: 19373
		public int LueGanXingQu;
	}
}
