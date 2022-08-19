using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000880 RID: 2176
	public class LingGanTimeMaxData : IJSONClass
	{
		// Token: 0x06004013 RID: 16403 RVA: 0x001B56B8 File Offset: 0x001B38B8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LingGanTimeMaxData.list)
			{
				try
				{
					LingGanTimeMaxData lingGanTimeMaxData = new LingGanTimeMaxData();
					lingGanTimeMaxData.id = jsonobject["id"].I;
					lingGanTimeMaxData.MaxTime = jsonobject["MaxTime"].I;
					if (LingGanTimeMaxData.DataDict.ContainsKey(lingGanTimeMaxData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LingGanTimeMaxData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lingGanTimeMaxData.id));
					}
					else
					{
						LingGanTimeMaxData.DataDict.Add(lingGanTimeMaxData.id, lingGanTimeMaxData);
						LingGanTimeMaxData.DataList.Add(lingGanTimeMaxData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LingGanTimeMaxData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LingGanTimeMaxData.OnInitFinishAction != null)
			{
				LingGanTimeMaxData.OnInitFinishAction();
			}
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D0B RID: 15627
		public static Dictionary<int, LingGanTimeMaxData> DataDict = new Dictionary<int, LingGanTimeMaxData>();

		// Token: 0x04003D0C RID: 15628
		public static List<LingGanTimeMaxData> DataList = new List<LingGanTimeMaxData>();

		// Token: 0x04003D0D RID: 15629
		public static Action OnInitFinishAction = new Action(LingGanTimeMaxData.OnInitFinish);

		// Token: 0x04003D0E RID: 15630
		public int id;

		// Token: 0x04003D0F RID: 15631
		public int MaxTime;
	}
}
