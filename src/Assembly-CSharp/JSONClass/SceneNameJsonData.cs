using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C5E RID: 3166
	public class SceneNameJsonData : IJSONClass
	{
		// Token: 0x06004CE1 RID: 19681 RVA: 0x00207C38 File Offset: 0x00205E38
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

		// Token: 0x06004CE2 RID: 19682 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004BDC RID: 19420
		public static Dictionary<string, SceneNameJsonData> DataDict = new Dictionary<string, SceneNameJsonData>();

		// Token: 0x04004BDD RID: 19421
		public static List<SceneNameJsonData> DataList = new List<SceneNameJsonData>();

		// Token: 0x04004BDE RID: 19422
		public static Action OnInitFinishAction = new Action(SceneNameJsonData.OnInitFinish);

		// Token: 0x04004BDF RID: 19423
		public int MapType;

		// Token: 0x04004BE0 RID: 19424
		public int MoneyType;

		// Token: 0x04004BE1 RID: 19425
		public int HighlightID;

		// Token: 0x04004BE2 RID: 19426
		public string id;

		// Token: 0x04004BE3 RID: 19427
		public string EventName;

		// Token: 0x04004BE4 RID: 19428
		public string MapName;
	}
}
