using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C4B RID: 3147
	public class PaiMaiAIJiaWei : IJSONClass
	{
		// Token: 0x06004C95 RID: 19605 RVA: 0x00205E8C File Offset: 0x0020408C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.PaiMaiAIJiaWei.list)
			{
				try
				{
					PaiMaiAIJiaWei paiMaiAIJiaWei = new PaiMaiAIJiaWei();
					paiMaiAIJiaWei.id = jsonobject["id"].I;
					paiMaiAIJiaWei.Jiawei = jsonobject["Jiawei"].I;
					if (PaiMaiAIJiaWei.DataDict.ContainsKey(paiMaiAIJiaWei.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典PaiMaiAIJiaWei.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", paiMaiAIJiaWei.id));
					}
					else
					{
						PaiMaiAIJiaWei.DataDict.Add(paiMaiAIJiaWei.id, paiMaiAIJiaWei);
						PaiMaiAIJiaWei.DataList.Add(paiMaiAIJiaWei);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典PaiMaiAIJiaWei.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (PaiMaiAIJiaWei.OnInitFinishAction != null)
			{
				PaiMaiAIJiaWei.OnInitFinishAction();
			}
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004B34 RID: 19252
		public static Dictionary<int, PaiMaiAIJiaWei> DataDict = new Dictionary<int, PaiMaiAIJiaWei>();

		// Token: 0x04004B35 RID: 19253
		public static List<PaiMaiAIJiaWei> DataList = new List<PaiMaiAIJiaWei>();

		// Token: 0x04004B36 RID: 19254
		public static Action OnInitFinishAction = new Action(PaiMaiAIJiaWei.OnInitFinish);

		// Token: 0x04004B37 RID: 19255
		public int id;

		// Token: 0x04004B38 RID: 19256
		public int Jiawei;
	}
}
