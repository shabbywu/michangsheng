using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008C4 RID: 2244
	public class PaiMaiDuiHuaAI : IJSONClass
	{
		// Token: 0x06004123 RID: 16675 RVA: 0x001BE1F0 File Offset: 0x001BC3F0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.PaiMaiDuiHuaAI.list)
			{
				try
				{
					PaiMaiDuiHuaAI paiMaiDuiHuaAI = new PaiMaiDuiHuaAI();
					paiMaiDuiHuaAI.id = jsonobject["id"].I;
					paiMaiDuiHuaAI.CeLue = jsonobject["CeLue"].I;
					paiMaiDuiHuaAI.JieGuo = jsonobject["JieGuo"].I;
					paiMaiDuiHuaAI.MuBiao = jsonobject["MuBiao"].I;
					paiMaiDuiHuaAI.DuiHua = jsonobject["DuiHua"].Str;
					paiMaiDuiHuaAI.HuiFu = jsonobject["HuiFu"].Str;
					if (PaiMaiDuiHuaAI.DataDict.ContainsKey(paiMaiDuiHuaAI.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典PaiMaiDuiHuaAI.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", paiMaiDuiHuaAI.id));
					}
					else
					{
						PaiMaiDuiHuaAI.DataDict.Add(paiMaiDuiHuaAI.id, paiMaiDuiHuaAI);
						PaiMaiDuiHuaAI.DataList.Add(paiMaiDuiHuaAI);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典PaiMaiDuiHuaAI.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (PaiMaiDuiHuaAI.OnInitFinishAction != null)
			{
				PaiMaiDuiHuaAI.OnInitFinishAction();
			}
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400402E RID: 16430
		public static Dictionary<int, PaiMaiDuiHuaAI> DataDict = new Dictionary<int, PaiMaiDuiHuaAI>();

		// Token: 0x0400402F RID: 16431
		public static List<PaiMaiDuiHuaAI> DataList = new List<PaiMaiDuiHuaAI>();

		// Token: 0x04004030 RID: 16432
		public static Action OnInitFinishAction = new Action(PaiMaiDuiHuaAI.OnInitFinish);

		// Token: 0x04004031 RID: 16433
		public int id;

		// Token: 0x04004032 RID: 16434
		public int CeLue;

		// Token: 0x04004033 RID: 16435
		public int JieGuo;

		// Token: 0x04004034 RID: 16436
		public int MuBiao;

		// Token: 0x04004035 RID: 16437
		public string DuiHua;

		// Token: 0x04004036 RID: 16438
		public string HuiFu;
	}
}
