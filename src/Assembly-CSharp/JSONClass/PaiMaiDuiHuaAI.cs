using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C52 RID: 3154
	public class PaiMaiDuiHuaAI : IJSONClass
	{
		// Token: 0x06004CB1 RID: 19633 RVA: 0x00206B3C File Offset: 0x00204D3C
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

		// Token: 0x06004CB2 RID: 19634 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004B82 RID: 19330
		public static Dictionary<int, PaiMaiDuiHuaAI> DataDict = new Dictionary<int, PaiMaiDuiHuaAI>();

		// Token: 0x04004B83 RID: 19331
		public static List<PaiMaiDuiHuaAI> DataList = new List<PaiMaiDuiHuaAI>();

		// Token: 0x04004B84 RID: 19332
		public static Action OnInitFinishAction = new Action(PaiMaiDuiHuaAI.OnInitFinish);

		// Token: 0x04004B85 RID: 19333
		public int id;

		// Token: 0x04004B86 RID: 19334
		public int CeLue;

		// Token: 0x04004B87 RID: 19335
		public int JieGuo;

		// Token: 0x04004B88 RID: 19336
		public int MuBiao;

		// Token: 0x04004B89 RID: 19337
		public string DuiHua;

		// Token: 0x04004B8A RID: 19338
		public string HuiFu;
	}
}
