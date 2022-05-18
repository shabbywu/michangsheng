using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BC5 RID: 3013
	public class drawCardJsonData : IJSONClass
	{
		// Token: 0x06004A7C RID: 19068 RVA: 0x001F86AC File Offset: 0x001F68AC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.drawCardJsonData.list)
			{
				try
				{
					drawCardJsonData drawCardJsonData = new drawCardJsonData();
					drawCardJsonData.id = jsonobject["id"].I;
					drawCardJsonData.probability = jsonobject["probability"].I;
					if (drawCardJsonData.DataDict.ContainsKey(drawCardJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典drawCardJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", drawCardJsonData.id));
					}
					else
					{
						drawCardJsonData.DataDict.Add(drawCardJsonData.id, drawCardJsonData);
						drawCardJsonData.DataList.Add(drawCardJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典drawCardJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (drawCardJsonData.OnInitFinishAction != null)
			{
				drawCardJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004611 RID: 17937
		public static Dictionary<int, drawCardJsonData> DataDict = new Dictionary<int, drawCardJsonData>();

		// Token: 0x04004612 RID: 17938
		public static List<drawCardJsonData> DataList = new List<drawCardJsonData>();

		// Token: 0x04004613 RID: 17939
		public static Action OnInitFinishAction = new Action(drawCardJsonData.OnInitFinish);

		// Token: 0x04004614 RID: 17940
		public int id;

		// Token: 0x04004615 RID: 17941
		public int probability;
	}
}
