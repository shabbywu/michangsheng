using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AE7 RID: 2791
	public class AllMapShiJianOptionJsonData : IJSONClass
	{
		// Token: 0x06004706 RID: 18182 RVA: 0x001E63F0 File Offset: 0x001E45F0
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

		// Token: 0x06004707 RID: 18183 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F97 RID: 16279
		public static Dictionary<int, AllMapShiJianOptionJsonData> DataDict = new Dictionary<int, AllMapShiJianOptionJsonData>();

		// Token: 0x04003F98 RID: 16280
		public static List<AllMapShiJianOptionJsonData> DataList = new List<AllMapShiJianOptionJsonData>();

		// Token: 0x04003F99 RID: 16281
		public static Action OnInitFinishAction = new Action(AllMapShiJianOptionJsonData.OnInitFinish);

		// Token: 0x04003F9A RID: 16282
		public int id;

		// Token: 0x04003F9B RID: 16283
		public int option1;

		// Token: 0x04003F9C RID: 16284
		public int option2;

		// Token: 0x04003F9D RID: 16285
		public int option3;

		// Token: 0x04003F9E RID: 16286
		public int optionID;

		// Token: 0x04003F9F RID: 16287
		public string EventName;

		// Token: 0x04003FA0 RID: 16288
		public string desc;

		// Token: 0x04003FA1 RID: 16289
		public string optionDesc1;

		// Token: 0x04003FA2 RID: 16290
		public string optionDesc2;

		// Token: 0x04003FA3 RID: 16291
		public string optionDesc3;
	}
}
