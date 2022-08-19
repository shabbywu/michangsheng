using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200087E RID: 2174
	public class LingGanLevelData : IJSONClass
	{
		// Token: 0x0600400B RID: 16395 RVA: 0x001B5420 File Offset: 0x001B3620
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LingGanLevelData.list)
			{
				try
				{
					LingGanLevelData lingGanLevelData = new LingGanLevelData();
					lingGanLevelData.id = jsonobject["id"].I;
					lingGanLevelData.lingGanQuJian = jsonobject["lingGanQuJian"].I;
					if (LingGanLevelData.DataDict.ContainsKey(lingGanLevelData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LingGanLevelData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lingGanLevelData.id));
					}
					else
					{
						LingGanLevelData.DataDict.Add(lingGanLevelData.id, lingGanLevelData);
						LingGanLevelData.DataList.Add(lingGanLevelData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LingGanLevelData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LingGanLevelData.OnInitFinishAction != null)
			{
				LingGanLevelData.OnInitFinishAction();
			}
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D01 RID: 15617
		public static Dictionary<int, LingGanLevelData> DataDict = new Dictionary<int, LingGanLevelData>();

		// Token: 0x04003D02 RID: 15618
		public static List<LingGanLevelData> DataList = new List<LingGanLevelData>();

		// Token: 0x04003D03 RID: 15619
		public static Action OnInitFinishAction = new Action(LingGanLevelData.OnInitFinish);

		// Token: 0x04003D04 RID: 15620
		public int id;

		// Token: 0x04003D05 RID: 15621
		public int lingGanQuJian;
	}
}
