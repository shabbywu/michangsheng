using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000759 RID: 1881
	public class BuffSeidJsonData102 : IJSONClass
	{
		// Token: 0x06003B78 RID: 15224 RVA: 0x0019980C File Offset: 0x00197A0C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[102].list)
			{
				try
				{
					BuffSeidJsonData102 buffSeidJsonData = new BuffSeidJsonData102();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData102.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData102.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData102.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData102.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData102.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData102.OnInitFinishAction != null)
			{
				BuffSeidJsonData102.OnInitFinishAction();
			}
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400348B RID: 13451
		public static int SEIDID = 102;

		// Token: 0x0400348C RID: 13452
		public static Dictionary<int, BuffSeidJsonData102> DataDict = new Dictionary<int, BuffSeidJsonData102>();

		// Token: 0x0400348D RID: 13453
		public static List<BuffSeidJsonData102> DataList = new List<BuffSeidJsonData102>();

		// Token: 0x0400348E RID: 13454
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData102.OnInitFinish);

		// Token: 0x0400348F RID: 13455
		public int id;

		// Token: 0x04003490 RID: 13456
		public int value1;
	}
}
