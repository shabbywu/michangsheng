using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007DB RID: 2011
	public class BuffSeidJsonData56 : IJSONClass
	{
		// Token: 0x06003D7E RID: 15742 RVA: 0x001A5558 File Offset: 0x001A3758
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[56].list)
			{
				try
				{
					BuffSeidJsonData56 buffSeidJsonData = new BuffSeidJsonData56();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData56.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData56.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData56.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData56.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData56.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData56.OnInitFinishAction != null)
			{
				BuffSeidJsonData56.OnInitFinishAction();
			}
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003815 RID: 14357
		public static int SEIDID = 56;

		// Token: 0x04003816 RID: 14358
		public static Dictionary<int, BuffSeidJsonData56> DataDict = new Dictionary<int, BuffSeidJsonData56>();

		// Token: 0x04003817 RID: 14359
		public static List<BuffSeidJsonData56> DataList = new List<BuffSeidJsonData56>();

		// Token: 0x04003818 RID: 14360
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData56.OnInitFinish);

		// Token: 0x04003819 RID: 14361
		public int id;

		// Token: 0x0400381A RID: 14362
		public int value1;
	}
}
