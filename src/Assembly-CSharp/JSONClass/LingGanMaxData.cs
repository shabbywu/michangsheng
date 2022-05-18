using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C0D RID: 3085
	public class LingGanMaxData : IJSONClass
	{
		// Token: 0x06004B9D RID: 19357 RVA: 0x001FED44 File Offset: 0x001FCF44
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LingGanMaxData.list)
			{
				try
				{
					LingGanMaxData lingGanMaxData = new LingGanMaxData();
					lingGanMaxData.id = jsonobject["id"].I;
					lingGanMaxData.lingGanShangXian = jsonobject["lingGanShangXian"].I;
					if (LingGanMaxData.DataDict.ContainsKey(lingGanMaxData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LingGanMaxData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lingGanMaxData.id));
					}
					else
					{
						LingGanMaxData.DataDict.Add(lingGanMaxData.id, lingGanMaxData);
						LingGanMaxData.DataList.Add(lingGanMaxData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LingGanMaxData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LingGanMaxData.OnInitFinishAction != null)
			{
				LingGanMaxData.OnInitFinishAction();
			}
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400485F RID: 18527
		public static Dictionary<int, LingGanMaxData> DataDict = new Dictionary<int, LingGanMaxData>();

		// Token: 0x04004860 RID: 18528
		public static List<LingGanMaxData> DataList = new List<LingGanMaxData>();

		// Token: 0x04004861 RID: 18529
		public static Action OnInitFinishAction = new Action(LingGanMaxData.OnInitFinish);

		// Token: 0x04004862 RID: 18530
		public int id;

		// Token: 0x04004863 RID: 18531
		public int lingGanShangXian;
	}
}
