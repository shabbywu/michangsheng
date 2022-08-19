using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008D2 RID: 2258
	public class ScenePriceData : IJSONClass
	{
		// Token: 0x0600415B RID: 16731 RVA: 0x001BF8CC File Offset: 0x001BDACC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ScenePriceData.list)
			{
				try
				{
					ScenePriceData scenePriceData = new ScenePriceData();
					scenePriceData.id = jsonobject["id"].I;
					scenePriceData.ItemFlag = jsonobject["ItemFlag"].ToList();
					scenePriceData.percent = jsonobject["percent"].ToList();
					if (ScenePriceData.DataDict.ContainsKey(scenePriceData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ScenePriceData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", scenePriceData.id));
					}
					else
					{
						ScenePriceData.DataDict.Add(scenePriceData.id, scenePriceData);
						ScenePriceData.DataList.Add(scenePriceData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ScenePriceData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ScenePriceData.OnInitFinishAction != null)
			{
				ScenePriceData.OnInitFinishAction();
			}
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400409B RID: 16539
		public static Dictionary<int, ScenePriceData> DataDict = new Dictionary<int, ScenePriceData>();

		// Token: 0x0400409C RID: 16540
		public static List<ScenePriceData> DataList = new List<ScenePriceData>();

		// Token: 0x0400409D RID: 16541
		public static Action OnInitFinishAction = new Action(ScenePriceData.OnInitFinish);

		// Token: 0x0400409E RID: 16542
		public int id;

		// Token: 0x0400409F RID: 16543
		public List<int> ItemFlag = new List<int>();

		// Token: 0x040040A0 RID: 16544
		public List<int> percent = new List<int>();
	}
}
