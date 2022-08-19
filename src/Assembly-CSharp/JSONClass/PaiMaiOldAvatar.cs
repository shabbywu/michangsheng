using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008C8 RID: 2248
	public class PaiMaiOldAvatar : IJSONClass
	{
		// Token: 0x06004133 RID: 16691 RVA: 0x001BE830 File Offset: 0x001BCA30
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.PaiMaiOldAvatar.list)
			{
				try
				{
					PaiMaiOldAvatar paiMaiOldAvatar = new PaiMaiOldAvatar();
					paiMaiOldAvatar.id = jsonobject["id"].I;
					paiMaiOldAvatar.LingShi = jsonobject["LingShi"].I;
					paiMaiOldAvatar.GaiLv = jsonobject["GaiLv"].I;
					paiMaiOldAvatar.PaiMaiID = jsonobject["PaiMaiID"].ToList();
					if (PaiMaiOldAvatar.DataDict.ContainsKey(paiMaiOldAvatar.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典PaiMaiOldAvatar.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", paiMaiOldAvatar.id));
					}
					else
					{
						PaiMaiOldAvatar.DataDict.Add(paiMaiOldAvatar.id, paiMaiOldAvatar);
						PaiMaiOldAvatar.DataList.Add(paiMaiOldAvatar);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典PaiMaiOldAvatar.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (PaiMaiOldAvatar.OnInitFinishAction != null)
			{
				PaiMaiOldAvatar.OnInitFinishAction();
			}
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400404B RID: 16459
		public static Dictionary<int, PaiMaiOldAvatar> DataDict = new Dictionary<int, PaiMaiOldAvatar>();

		// Token: 0x0400404C RID: 16460
		public static List<PaiMaiOldAvatar> DataList = new List<PaiMaiOldAvatar>();

		// Token: 0x0400404D RID: 16461
		public static Action OnInitFinishAction = new Action(PaiMaiOldAvatar.OnInitFinish);

		// Token: 0x0400404E RID: 16462
		public int id;

		// Token: 0x0400404F RID: 16463
		public int LingShi;

		// Token: 0x04004050 RID: 16464
		public int GaiLv;

		// Token: 0x04004051 RID: 16465
		public List<int> PaiMaiID = new List<int>();
	}
}
