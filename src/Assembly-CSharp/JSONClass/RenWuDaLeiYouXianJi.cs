using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008CE RID: 2254
	public class RenWuDaLeiYouXianJi : IJSONClass
	{
		// Token: 0x0600414B RID: 16715 RVA: 0x001BF260 File Offset: 0x001BD460
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.RenWuDaLeiYouXianJi.list)
			{
				try
				{
					RenWuDaLeiYouXianJi renWuDaLeiYouXianJi = new RenWuDaLeiYouXianJi();
					renWuDaLeiYouXianJi.Id = jsonobject["Id"].I;
					renWuDaLeiYouXianJi.QuJian = jsonobject["QuJian"].ToList();
					if (RenWuDaLeiYouXianJi.DataDict.ContainsKey(renWuDaLeiYouXianJi.Id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典RenWuDaLeiYouXianJi.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", renWuDaLeiYouXianJi.Id));
					}
					else
					{
						RenWuDaLeiYouXianJi.DataDict.Add(renWuDaLeiYouXianJi.Id, renWuDaLeiYouXianJi);
						RenWuDaLeiYouXianJi.DataList.Add(renWuDaLeiYouXianJi);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典RenWuDaLeiYouXianJi.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (RenWuDaLeiYouXianJi.OnInitFinishAction != null)
			{
				RenWuDaLeiYouXianJi.OnInitFinishAction();
			}
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400407D RID: 16509
		public static Dictionary<int, RenWuDaLeiYouXianJi> DataDict = new Dictionary<int, RenWuDaLeiYouXianJi>();

		// Token: 0x0400407E RID: 16510
		public static List<RenWuDaLeiYouXianJi> DataList = new List<RenWuDaLeiYouXianJi>();

		// Token: 0x0400407F RID: 16511
		public static Action OnInitFinishAction = new Action(RenWuDaLeiYouXianJi.OnInitFinish);

		// Token: 0x04004080 RID: 16512
		public int Id;

		// Token: 0x04004081 RID: 16513
		public List<int> QuJian = new List<int>();
	}
}
