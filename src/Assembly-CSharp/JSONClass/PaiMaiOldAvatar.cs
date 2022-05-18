using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C56 RID: 3158
	public class PaiMaiOldAvatar : IJSONClass
	{
		// Token: 0x06004CC1 RID: 19649 RVA: 0x002070C8 File Offset: 0x002052C8
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

		// Token: 0x06004CC2 RID: 19650 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004B9F RID: 19359
		public static Dictionary<int, PaiMaiOldAvatar> DataDict = new Dictionary<int, PaiMaiOldAvatar>();

		// Token: 0x04004BA0 RID: 19360
		public static List<PaiMaiOldAvatar> DataList = new List<PaiMaiOldAvatar>();

		// Token: 0x04004BA1 RID: 19361
		public static Action OnInitFinishAction = new Action(PaiMaiOldAvatar.OnInitFinish);

		// Token: 0x04004BA2 RID: 19362
		public int id;

		// Token: 0x04004BA3 RID: 19363
		public int LingShi;

		// Token: 0x04004BA4 RID: 19364
		public int GaiLv;

		// Token: 0x04004BA5 RID: 19365
		public List<int> PaiMaiID = new List<int>();
	}
}
