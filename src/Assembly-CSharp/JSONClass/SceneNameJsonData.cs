using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008D1 RID: 2257
	public class SceneNameJsonData : IJSONClass
	{
		// Token: 0x06004157 RID: 16727 RVA: 0x001BF710 File Offset: 0x001BD910
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SceneNameJsonData.list)
			{
				try
				{
					SceneNameJsonData sceneNameJsonData = new SceneNameJsonData();
					sceneNameJsonData.MapType = jsonobject["MapType"].I;
					sceneNameJsonData.MoneyType = jsonobject["MoneyType"].I;
					sceneNameJsonData.HighlightID = jsonobject["HighlightID"].I;
					sceneNameJsonData.id = jsonobject["id"].Str;
					sceneNameJsonData.EventName = jsonobject["EventName"].Str;
					sceneNameJsonData.MapName = jsonobject["MapName"].Str;
					if (SceneNameJsonData.DataDict.ContainsKey(sceneNameJsonData.id))
					{
						PreloadManager.LogException("!!!错误!!!向字典SceneNameJsonData.DataDict添加数据时出现重复的键，Key:" + sceneNameJsonData.id + "，已跳过，请检查配表");
					}
					else
					{
						SceneNameJsonData.DataDict.Add(sceneNameJsonData.id, sceneNameJsonData);
						SceneNameJsonData.DataList.Add(sceneNameJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SceneNameJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SceneNameJsonData.OnInitFinishAction != null)
			{
				SceneNameJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004092 RID: 16530
		public static Dictionary<string, SceneNameJsonData> DataDict = new Dictionary<string, SceneNameJsonData>();

		// Token: 0x04004093 RID: 16531
		public static List<SceneNameJsonData> DataList = new List<SceneNameJsonData>();

		// Token: 0x04004094 RID: 16532
		public static Action OnInitFinishAction = new Action(SceneNameJsonData.OnInitFinish);

		// Token: 0x04004095 RID: 16533
		public int MapType;

		// Token: 0x04004096 RID: 16534
		public int MoneyType;

		// Token: 0x04004097 RID: 16535
		public int HighlightID;

		// Token: 0x04004098 RID: 16536
		public string id;

		// Token: 0x04004099 RID: 16537
		public string EventName;

		// Token: 0x0400409A RID: 16538
		public string MapName;
	}
}
