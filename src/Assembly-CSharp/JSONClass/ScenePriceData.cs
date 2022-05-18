using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C5F RID: 3167
	public class ScenePriceData : IJSONClass
	{
		// Token: 0x06004CE5 RID: 19685 RVA: 0x00207DCC File Offset: 0x00205FCC
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

		// Token: 0x06004CE6 RID: 19686 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004BE5 RID: 19429
		public static Dictionary<int, ScenePriceData> DataDict = new Dictionary<int, ScenePriceData>();

		// Token: 0x04004BE6 RID: 19430
		public static List<ScenePriceData> DataList = new List<ScenePriceData>();

		// Token: 0x04004BE7 RID: 19431
		public static Action OnInitFinishAction = new Action(ScenePriceData.OnInitFinish);

		// Token: 0x04004BE8 RID: 19432
		public int id;

		// Token: 0x04004BE9 RID: 19433
		public List<int> ItemFlag = new List<int>();

		// Token: 0x04004BEA RID: 19434
		public List<int> percent = new List<int>();
	}
}
