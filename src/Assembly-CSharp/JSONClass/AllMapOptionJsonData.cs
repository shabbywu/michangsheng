using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AE5 RID: 2789
	public class AllMapOptionJsonData : IJSONClass
	{
		// Token: 0x060046FE RID: 18174 RVA: 0x001E5FD4 File Offset: 0x001E41D4
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

		// Token: 0x060046FF RID: 18175 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F7A RID: 16250
		public static Dictionary<int, AllMapOptionJsonData> DataDict = new Dictionary<int, AllMapOptionJsonData>();

		// Token: 0x04003F7B RID: 16251
		public static List<AllMapOptionJsonData> DataList = new List<AllMapOptionJsonData>();

		// Token: 0x04003F7C RID: 16252
		public static Action OnInitFinishAction = new Action(AllMapOptionJsonData.OnInitFinish);

		// Token: 0x04003F7D RID: 16253
		public int id;

		// Token: 0x04003F7E RID: 16254
		public int value1;

		// Token: 0x04003F7F RID: 16255
		public int value2;

		// Token: 0x04003F80 RID: 16256
		public int value3;

		// Token: 0x04003F81 RID: 16257
		public int value4;

		// Token: 0x04003F82 RID: 16258
		public int value5;

		// Token: 0x04003F83 RID: 16259
		public int value8;

		// Token: 0x04003F84 RID: 16260
		public int value9;

		// Token: 0x04003F85 RID: 16261
		public int value10;

		// Token: 0x04003F86 RID: 16262
		public string EventName;

		// Token: 0x04003F87 RID: 16263
		public string desc;

		// Token: 0x04003F88 RID: 16264
		public List<int> value6 = new List<int>();

		// Token: 0x04003F89 RID: 16265
		public List<int> value7 = new List<int>();
	}
}
