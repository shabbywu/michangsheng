using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C53 RID: 3155
	public class PaiMaiDuiHuaBiao : IJSONClass
	{
		// Token: 0x06004CB5 RID: 19637 RVA: 0x00206CD0 File Offset: 0x00204ED0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.PaiMaiDuiHuaBiao.list)
			{
				try
				{
					PaiMaiDuiHuaBiao paiMaiDuiHuaBiao = new PaiMaiDuiHuaBiao();
					paiMaiDuiHuaBiao.id = jsonobject["id"].I;
					paiMaiDuiHuaBiao.Text = jsonobject["Text"].Str;
					paiMaiDuiHuaBiao.huanhua = jsonobject["huanhua"].ToList();
					if (PaiMaiDuiHuaBiao.DataDict.ContainsKey(paiMaiDuiHuaBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典PaiMaiDuiHuaBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", paiMaiDuiHuaBiao.id));
					}
					else
					{
						PaiMaiDuiHuaBiao.DataDict.Add(paiMaiDuiHuaBiao.id, paiMaiDuiHuaBiao);
						PaiMaiDuiHuaBiao.DataList.Add(paiMaiDuiHuaBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典PaiMaiDuiHuaBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (PaiMaiDuiHuaBiao.OnInitFinishAction != null)
			{
				PaiMaiDuiHuaBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06004CB6 RID: 19638 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004B8B RID: 19339
		public static Dictionary<int, PaiMaiDuiHuaBiao> DataDict = new Dictionary<int, PaiMaiDuiHuaBiao>();

		// Token: 0x04004B8C RID: 19340
		public static List<PaiMaiDuiHuaBiao> DataList = new List<PaiMaiDuiHuaBiao>();

		// Token: 0x04004B8D RID: 19341
		public static Action OnInitFinishAction = new Action(PaiMaiDuiHuaBiao.OnInitFinish);

		// Token: 0x04004B8E RID: 19342
		public int id;

		// Token: 0x04004B8F RID: 19343
		public string Text;

		// Token: 0x04004B90 RID: 19344
		public List<int> huanhua = new List<int>();
	}
}
