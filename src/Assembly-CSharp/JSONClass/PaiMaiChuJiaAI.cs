using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C50 RID: 3152
	public class PaiMaiChuJiaAI : IJSONClass
	{
		// Token: 0x06004CA9 RID: 19625 RVA: 0x002068B0 File Offset: 0x00204AB0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.PaiMaiChuJiaAI.list)
			{
				try
				{
					PaiMaiChuJiaAI paiMaiChuJiaAI = new PaiMaiChuJiaAI();
					paiMaiChuJiaAI.id = jsonobject["id"].I;
					paiMaiChuJiaAI.Type = jsonobject["Type"].ToList();
					paiMaiChuJiaAI.YingXiang = jsonobject["YingXiang"].ToList();
					paiMaiChuJiaAI.GaiLv = jsonobject["GaiLv"].ToList();
					if (PaiMaiChuJiaAI.DataDict.ContainsKey(paiMaiChuJiaAI.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典PaiMaiChuJiaAI.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", paiMaiChuJiaAI.id));
					}
					else
					{
						PaiMaiChuJiaAI.DataDict.Add(paiMaiChuJiaAI.id, paiMaiChuJiaAI);
						PaiMaiChuJiaAI.DataList.Add(paiMaiChuJiaAI);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典PaiMaiChuJiaAI.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (PaiMaiChuJiaAI.OnInitFinishAction != null)
			{
				PaiMaiChuJiaAI.OnInitFinishAction();
			}
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004B75 RID: 19317
		public static Dictionary<int, PaiMaiChuJiaAI> DataDict = new Dictionary<int, PaiMaiChuJiaAI>();

		// Token: 0x04004B76 RID: 19318
		public static List<PaiMaiChuJiaAI> DataList = new List<PaiMaiChuJiaAI>();

		// Token: 0x04004B77 RID: 19319
		public static Action OnInitFinishAction = new Action(PaiMaiChuJiaAI.OnInitFinish);

		// Token: 0x04004B78 RID: 19320
		public int id;

		// Token: 0x04004B79 RID: 19321
		public List<int> Type = new List<int>();

		// Token: 0x04004B7A RID: 19322
		public List<int> YingXiang = new List<int>();

		// Token: 0x04004B7B RID: 19323
		public List<int> GaiLv = new List<int>();
	}
}
