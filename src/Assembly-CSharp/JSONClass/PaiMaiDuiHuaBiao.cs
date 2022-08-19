using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008C5 RID: 2245
	public class PaiMaiDuiHuaBiao : IJSONClass
	{
		// Token: 0x06004127 RID: 16679 RVA: 0x001BE3AC File Offset: 0x001BC5AC
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

		// Token: 0x06004128 RID: 16680 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004037 RID: 16439
		public static Dictionary<int, PaiMaiDuiHuaBiao> DataDict = new Dictionary<int, PaiMaiDuiHuaBiao>();

		// Token: 0x04004038 RID: 16440
		public static List<PaiMaiDuiHuaBiao> DataList = new List<PaiMaiDuiHuaBiao>();

		// Token: 0x04004039 RID: 16441
		public static Action OnInitFinishAction = new Action(PaiMaiDuiHuaBiao.OnInitFinish);

		// Token: 0x0400403A RID: 16442
		public int id;

		// Token: 0x0400403B RID: 16443
		public string Text;

		// Token: 0x0400403C RID: 16444
		public List<int> huanhua = new List<int>();
	}
}
