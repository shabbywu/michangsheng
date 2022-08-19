using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200087F RID: 2175
	public class LingGanMaxData : IJSONClass
	{
		// Token: 0x0600400F RID: 16399 RVA: 0x001B556C File Offset: 0x001B376C
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

		// Token: 0x06004010 RID: 16400 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D06 RID: 15622
		public static Dictionary<int, LingGanMaxData> DataDict = new Dictionary<int, LingGanMaxData>();

		// Token: 0x04003D07 RID: 15623
		public static List<LingGanMaxData> DataList = new List<LingGanMaxData>();

		// Token: 0x04003D08 RID: 15624
		public static Action OnInitFinishAction = new Action(LingGanMaxData.OnInitFinish);

		// Token: 0x04003D09 RID: 15625
		public int id;

		// Token: 0x04003D0A RID: 15626
		public int lingGanShangXian;
	}
}
