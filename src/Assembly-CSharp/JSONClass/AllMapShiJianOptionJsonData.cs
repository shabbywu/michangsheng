using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200074F RID: 1871
	public class AllMapShiJianOptionJsonData : IJSONClass
	{
		// Token: 0x06003B50 RID: 15184 RVA: 0x001982A8 File Offset: 0x001964A8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.AllMapShiJianOptionJsonData.list)
			{
				try
				{
					AllMapShiJianOptionJsonData allMapShiJianOptionJsonData = new AllMapShiJianOptionJsonData();
					allMapShiJianOptionJsonData.id = jsonobject["id"].I;
					allMapShiJianOptionJsonData.option1 = jsonobject["option1"].I;
					allMapShiJianOptionJsonData.option2 = jsonobject["option2"].I;
					allMapShiJianOptionJsonData.option3 = jsonobject["option3"].I;
					allMapShiJianOptionJsonData.optionID = jsonobject["optionID"].I;
					allMapShiJianOptionJsonData.EventName = jsonobject["EventName"].Str;
					allMapShiJianOptionJsonData.desc = jsonobject["desc"].Str;
					allMapShiJianOptionJsonData.optionDesc1 = jsonobject["optionDesc1"].Str;
					allMapShiJianOptionJsonData.optionDesc2 = jsonobject["optionDesc2"].Str;
					allMapShiJianOptionJsonData.optionDesc3 = jsonobject["optionDesc3"].Str;
					if (AllMapShiJianOptionJsonData.DataDict.ContainsKey(allMapShiJianOptionJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典AllMapShiJianOptionJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", allMapShiJianOptionJsonData.id));
					}
					else
					{
						AllMapShiJianOptionJsonData.DataDict.Add(allMapShiJianOptionJsonData.id, allMapShiJianOptionJsonData);
						AllMapShiJianOptionJsonData.DataList.Add(allMapShiJianOptionJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典AllMapShiJianOptionJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (AllMapShiJianOptionJsonData.OnInitFinishAction != null)
			{
				AllMapShiJianOptionJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040033FE RID: 13310
		public static Dictionary<int, AllMapShiJianOptionJsonData> DataDict = new Dictionary<int, AllMapShiJianOptionJsonData>();

		// Token: 0x040033FF RID: 13311
		public static List<AllMapShiJianOptionJsonData> DataList = new List<AllMapShiJianOptionJsonData>();

		// Token: 0x04003400 RID: 13312
		public static Action OnInitFinishAction = new Action(AllMapShiJianOptionJsonData.OnInitFinish);

		// Token: 0x04003401 RID: 13313
		public int id;

		// Token: 0x04003402 RID: 13314
		public int option1;

		// Token: 0x04003403 RID: 13315
		public int option2;

		// Token: 0x04003404 RID: 13316
		public int option3;

		// Token: 0x04003405 RID: 13317
		public int optionID;

		// Token: 0x04003406 RID: 13318
		public string EventName;

		// Token: 0x04003407 RID: 13319
		public string desc;

		// Token: 0x04003408 RID: 13320
		public string optionDesc1;

		// Token: 0x04003409 RID: 13321
		public string optionDesc2;

		// Token: 0x0400340A RID: 13322
		public string optionDesc3;
	}
}
