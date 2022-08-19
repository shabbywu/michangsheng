using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200074D RID: 1869
	public class AllMapOptionJsonData : IJSONClass
	{
		// Token: 0x06003B48 RID: 15176 RVA: 0x00197E08 File Offset: 0x00196008
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.AllMapOptionJsonData.list)
			{
				try
				{
					AllMapOptionJsonData allMapOptionJsonData = new AllMapOptionJsonData();
					allMapOptionJsonData.id = jsonobject["id"].I;
					allMapOptionJsonData.value1 = jsonobject["value1"].I;
					allMapOptionJsonData.value2 = jsonobject["value2"].I;
					allMapOptionJsonData.value3 = jsonobject["value3"].I;
					allMapOptionJsonData.value4 = jsonobject["value4"].I;
					allMapOptionJsonData.value5 = jsonobject["value5"].I;
					allMapOptionJsonData.value8 = jsonobject["value8"].I;
					allMapOptionJsonData.value9 = jsonobject["value9"].I;
					allMapOptionJsonData.value10 = jsonobject["value10"].I;
					allMapOptionJsonData.EventName = jsonobject["EventName"].Str;
					allMapOptionJsonData.desc = jsonobject["desc"].Str;
					allMapOptionJsonData.value6 = jsonobject["value6"].ToList();
					allMapOptionJsonData.value7 = jsonobject["value7"].ToList();
					if (AllMapOptionJsonData.DataDict.ContainsKey(allMapOptionJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典AllMapOptionJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", allMapOptionJsonData.id));
					}
					else
					{
						AllMapOptionJsonData.DataDict.Add(allMapOptionJsonData.id, allMapOptionJsonData);
						AllMapOptionJsonData.DataList.Add(allMapOptionJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典AllMapOptionJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (AllMapOptionJsonData.OnInitFinishAction != null)
			{
				AllMapOptionJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003B49 RID: 15177 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040033E1 RID: 13281
		public static Dictionary<int, AllMapOptionJsonData> DataDict = new Dictionary<int, AllMapOptionJsonData>();

		// Token: 0x040033E2 RID: 13282
		public static List<AllMapOptionJsonData> DataList = new List<AllMapOptionJsonData>();

		// Token: 0x040033E3 RID: 13283
		public static Action OnInitFinishAction = new Action(AllMapOptionJsonData.OnInitFinish);

		// Token: 0x040033E4 RID: 13284
		public int id;

		// Token: 0x040033E5 RID: 13285
		public int value1;

		// Token: 0x040033E6 RID: 13286
		public int value2;

		// Token: 0x040033E7 RID: 13287
		public int value3;

		// Token: 0x040033E8 RID: 13288
		public int value4;

		// Token: 0x040033E9 RID: 13289
		public int value5;

		// Token: 0x040033EA RID: 13290
		public int value8;

		// Token: 0x040033EB RID: 13291
		public int value9;

		// Token: 0x040033EC RID: 13292
		public int value10;

		// Token: 0x040033ED RID: 13293
		public string EventName;

		// Token: 0x040033EE RID: 13294
		public string desc;

		// Token: 0x040033EF RID: 13295
		public List<int> value6 = new List<int>();

		// Token: 0x040033F0 RID: 13296
		public List<int> value7 = new List<int>();
	}
}
