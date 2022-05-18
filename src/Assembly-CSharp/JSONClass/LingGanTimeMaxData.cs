using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C0E RID: 3086
	public class LingGanTimeMaxData : IJSONClass
	{
		// Token: 0x06004BA1 RID: 19361 RVA: 0x001FEE68 File Offset: 0x001FD068
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

		// Token: 0x06004BA2 RID: 19362 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004864 RID: 18532
		public static Dictionary<int, LingGanTimeMaxData> DataDict = new Dictionary<int, LingGanTimeMaxData>();

		// Token: 0x04004865 RID: 18533
		public static List<LingGanTimeMaxData> DataList = new List<LingGanTimeMaxData>();

		// Token: 0x04004866 RID: 18534
		public static Action OnInitFinishAction = new Action(LingGanTimeMaxData.OnInitFinish);

		// Token: 0x04004867 RID: 18535
		public int id;

		// Token: 0x04004868 RID: 18536
		public int MaxTime;
	}
}
