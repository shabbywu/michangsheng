using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000972 RID: 2418
	public class TianFuDescJsonData : IJSONClass
	{
		// Token: 0x060043DA RID: 17370 RVA: 0x001CE5F4 File Offset: 0x001CC7F4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.TianFuDescJsonData.list)
			{
				try
				{
					TianFuDescJsonData tianFuDescJsonData = new TianFuDescJsonData();
					tianFuDescJsonData.id = jsonobject["id"].I;
					tianFuDescJsonData.Title = jsonobject["Title"].Str;
					tianFuDescJsonData.Desc = jsonobject["Desc"].Str;
					if (TianFuDescJsonData.DataDict.ContainsKey(tianFuDescJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典TianFuDescJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", tianFuDescJsonData.id));
					}
					else
					{
						TianFuDescJsonData.DataDict.Add(tianFuDescJsonData.id, tianFuDescJsonData);
						TianFuDescJsonData.DataList.Add(tianFuDescJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典TianFuDescJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (TianFuDescJsonData.OnInitFinishAction != null)
			{
				TianFuDescJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x060043DB RID: 17371 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004511 RID: 17681
		public static Dictionary<int, TianFuDescJsonData> DataDict = new Dictionary<int, TianFuDescJsonData>();

		// Token: 0x04004512 RID: 17682
		public static List<TianFuDescJsonData> DataList = new List<TianFuDescJsonData>();

		// Token: 0x04004513 RID: 17683
		public static Action OnInitFinishAction = new Action(TianFuDescJsonData.OnInitFinish);

		// Token: 0x04004514 RID: 17684
		public int id;

		// Token: 0x04004515 RID: 17685
		public string Title;

		// Token: 0x04004516 RID: 17686
		public string Desc;
	}
}
