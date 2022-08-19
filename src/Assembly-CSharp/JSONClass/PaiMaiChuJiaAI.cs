using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008C2 RID: 2242
	public class PaiMaiChuJiaAI : IJSONClass
	{
		// Token: 0x0600411B RID: 16667 RVA: 0x001BDEEC File Offset: 0x001BC0EC
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

		// Token: 0x0600411C RID: 16668 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004021 RID: 16417
		public static Dictionary<int, PaiMaiChuJiaAI> DataDict = new Dictionary<int, PaiMaiChuJiaAI>();

		// Token: 0x04004022 RID: 16418
		public static List<PaiMaiChuJiaAI> DataList = new List<PaiMaiChuJiaAI>();

		// Token: 0x04004023 RID: 16419
		public static Action OnInitFinishAction = new Action(PaiMaiChuJiaAI.OnInitFinish);

		// Token: 0x04004024 RID: 16420
		public int id;

		// Token: 0x04004025 RID: 16421
		public List<int> Type = new List<int>();

		// Token: 0x04004026 RID: 16422
		public List<int> YingXiang = new List<int>();

		// Token: 0x04004027 RID: 16423
		public List<int> GaiLv = new List<int>();
	}
}
