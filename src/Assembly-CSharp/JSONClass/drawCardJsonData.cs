using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000831 RID: 2097
	public class drawCardJsonData : IJSONClass
	{
		// Token: 0x06003ED6 RID: 16086 RVA: 0x001AD844 File Offset: 0x001ABA44
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

		// Token: 0x06003ED7 RID: 16087 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A94 RID: 14996
		public static Dictionary<int, drawCardJsonData> DataDict = new Dictionary<int, drawCardJsonData>();

		// Token: 0x04003A95 RID: 14997
		public static List<drawCardJsonData> DataList = new List<drawCardJsonData>();

		// Token: 0x04003A96 RID: 14998
		public static Action OnInitFinishAction = new Action(drawCardJsonData.OnInitFinish);

		// Token: 0x04003A97 RID: 14999
		public int id;

		// Token: 0x04003A98 RID: 15000
		public int probability;
	}
}
